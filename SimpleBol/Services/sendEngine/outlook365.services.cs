using Azure.Core;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using NLog;
using SimpleBol.Models.Smtp;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace SimpleBol.Services.sendEngine;

public interface IOutlook365Sender
{
    Task<EmailResponse?> SendEmailAsync(
        SimpleBol.Models.Outlook365 settings,
        SimpleBol.Models.Smtp.MailMessage mailMessage,
        CancellationToken cancellationToken = default);

    Task AuthorizeAsync(
        SimpleBol.Models.Outlook365 settings,
        CancellationToken cancellationToken = default);

    Task<bool> HasAuthorizationAsync(SimpleBol.Models.Outlook365 settings);
}

public sealed class Outlook365Sender : IOutlook365Sender
{
    private const string LegacyAuthorizationMarkerKey = "SimpleBol.Outlook.Authorization";
    private static readonly string[] GraphScopes = ["Mail.Send"];
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public async Task AuthorizeAsync(
        SimpleBol.Models.Outlook365 settings,
        CancellationToken cancellationToken = default)
    {
        var cacheName = CreateFreshTokenCacheName(settings.SentFromEmailAddress);
        var credential = CreateCredential(settings, null, cacheName);
        var authenticationRecord = await credential.AuthenticateAsync(
            CreateGraphTokenRequestContext(), cancellationToken);
        await SaveAuthorizationRecordAsync(
            settings, authenticationRecord, cacheName, cancellationToken);
    }

    public async Task<bool> HasAuthorizationAsync(SimpleBol.Models.Outlook365 settings)
    {
        var marker = await new ProtectedDataStore()
            .GetAsync<OutlookAuthorizationMarker>(CreateAuthorizationMarkerKey(settings));
        return marker != null &&
            !string.IsNullOrWhiteSpace(marker.ClientId) &&
            !string.IsNullOrWhiteSpace(marker.SerializedAuthenticationRecord);
    }

    public async Task<EmailResponse?> SendEmailAsync(
        SimpleBol.Models.Outlook365 settings,
        SimpleBol.Models.Smtp.MailMessage mailMessage,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var credential = await GetCredentialAsync(settings, cancellationToken);
            using var graphClient = new GraphServiceClient(credential, GraphScopes);
            var message = CreateMessage(mailMessage);
            var body = new Microsoft.Graph.Me.SendMail.SendMailPostRequestBody
            {
                Message = message,
                // BOL transmissions are business records and should remain visible
                // in the authenticated Outlook mailbox after Graph accepts them.
                SaveToSentItems = true
            };

            await graphClient.Me.SendMail.PostAsync(body, cancellationToken: cancellationToken);

            var responseBody = new StringContent(
                "Microsoft Graph accepted the message and saved it to Sent Items.");
            return new EmailResponse(
                true,
                HttpStatusCode.Accepted,
                responseBody,
                null,
                SmtpError.None,
                null);
        }
        catch (Microsoft.Graph.Models.ODataErrors.ODataError ex)
        {
            Logger.Error(ex, "Microsoft Graph rejected an Outlook email message.");
            return new EmailResponse(
                false,
                HttpStatusCode.BadRequest,
                null,
                null,
                SmtpError.Smtp_Code,
                ex.Error?.Message ?? ex.Message);
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "An error occurred while sending an Outlook email message.");
            return new EmailResponse(
                false,
                HttpStatusCode.InternalServerError,
                null,
                null,
                SmtpError.Program_Code,
                ex.Message);
        }
    }

    private static async Task<InteractiveBrowserCredential> GetCredentialAsync(
        SimpleBol.Models.Outlook365 settings,
        CancellationToken cancellationToken)
    {
        var dataStore = new ProtectedDataStore();
        var marker = await dataStore
            .GetAsync<OutlookAuthorizationMarker>(CreateAuthorizationMarkerKey(settings));
        marker ??= await dataStore.GetAsync<OutlookAuthorizationMarker>(LegacyAuthorizationMarkerKey);
        AuthenticationRecord? authenticationRecord = null;

        if (marker?.ClientId == settings.ClientId &&
            !string.IsNullOrWhiteSpace(marker.SerializedAuthenticationRecord))
        {
            try
            {
                var bytes = Convert.FromBase64String(marker.SerializedAuthenticationRecord);
                using var stream = new MemoryStream(bytes);
                authenticationRecord = await AuthenticationRecord.DeserializeAsync(
                    stream, cancellationToken);
                ValidateSavedAuthorization(settings, marker);
            }
            catch (Exception ex) when (ex is FormatException or InvalidDataException)
            {
                Logger.Warn(ex, "The saved Outlook authentication record could not be restored.");
            }
        }

        if (authenticationRecord != null)
            return CreateCredential(
                settings,
                authenticationRecord,
                marker?.TokenCacheName ?? CreateLegacyTokenCacheName(settings.SentFromEmailAddress));

        // Existing installations have a token cache but no serialized account record.
        // Authenticate once more, save the account record, then future sends are silent.
        var cacheName = CreateFreshTokenCacheName(settings.SentFromEmailAddress);
        var credential = CreateCredential(settings, null, cacheName);
        authenticationRecord = await credential.AuthenticateAsync(
            CreateGraphTokenRequestContext(), cancellationToken);
        await SaveAuthorizationRecordAsync(
            settings, authenticationRecord, cacheName, cancellationToken);
        return credential;
    }

    private static InteractiveBrowserCredential CreateCredential(
        SimpleBol.Models.Outlook365 settings,
        AuthenticationRecord? authenticationRecord,
        string tokenCacheName)
    {
        if (string.IsNullOrWhiteSpace(settings.ClientId))
            throw new InvalidOperationException("Microsoft OAuth Application Client ID is required.");

        return new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions
        {
            ClientId = settings.ClientId.Trim(),
            TenantId = NormalizeTenant(settings.TenantId),
            RedirectUri = new Uri("http://localhost"),
            AuthenticationRecord = authenticationRecord,
            TokenCachePersistenceOptions = new TokenCachePersistenceOptions
            {
                Name = tokenCacheName
            }
        });
    }

    private static async Task SaveAuthorizationRecordAsync(
        SimpleBol.Models.Outlook365 settings,
        AuthenticationRecord authenticationRecord,
        string tokenCacheName,
        CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await authenticationRecord.SerializeAsync(stream, cancellationToken);
        await new ProtectedDataStore().StoreAsync(
            CreateAuthorizationMarkerKey(settings),
            new OutlookAuthorizationMarker
            {
                ClientId = settings.ClientId,
                TenantId = NormalizeTenant(settings.TenantId),
                AccountEmail = settings.SentFromEmailAddress?.Trim(),
                TokenCacheName = tokenCacheName,
                SerializedAuthenticationRecord = Convert.ToBase64String(stream.ToArray()),
                AuthorizedUtc = DateTime.UtcNow
            });
    }

    private static string NormalizeTenant(string? tenantId) =>
        string.IsNullOrWhiteSpace(tenantId) ? "common" : tenantId.Trim();

    // Microsoft Graph requests Continuous Access Evaluation (CAE) tokens. Azure
    // Identity persists CAE and non-CAE tokens in separate cache files, so the
    // explicit Connect flow must populate the same CAE cache used by SendMail.
    private static TokenRequestContext CreateGraphTokenRequestContext() =>
        new(GraphScopes, isCaeEnabled: true);

    private static string CreateFreshTokenCacheName(string? accountEmail) =>
        $"{CreateLegacyTokenCacheName(accountEmail)}.{Guid.NewGuid():N}";

    private static string CreateLegacyTokenCacheName(string? accountEmail)
    {
        var normalizedEmail = accountEmail?.Trim().ToLowerInvariant() ?? "unknown";
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalizedEmail)));
        return $"SimpleBol.Outlook.{hash[..16]}";
    }

    private static string CreateAuthorizationMarkerKey(SimpleBol.Models.Outlook365 settings) =>
        $"SimpleBol.Outlook.Authorization.{settings.SentFromEmailAddress?.Trim().ToLowerInvariant() ?? "unknown"}";

    private static void ValidateSavedAuthorization(
        SimpleBol.Models.Outlook365 settings,
        OutlookAuthorizationMarker marker)
    {
        if (string.IsNullOrWhiteSpace(settings.SentFromEmailAddress) ||
            string.IsNullOrWhiteSpace(marker.AccountEmail))
            return;

        if (!string.Equals(settings.SentFromEmailAddress.Trim(),
                marker.AccountEmail.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"Microsoft authorized '{marker.AccountEmail}', but Outlook settings " +
                $"are configured for '{settings.SentFromEmailAddress}'. Connect Outlook again " +
                "and sign in with the configured mailbox.");
        }
    }

    private static Microsoft.Graph.Models.Message CreateMessage(
        SimpleBol.Models.Smtp.MailMessage mailMessage)
    {
        var content = !string.IsNullOrWhiteSpace(mailMessage.HtmlContent)
            ? mailMessage.HtmlContent
            : mailMessage.PlainTextContent;
        var contentType = !string.IsNullOrWhiteSpace(mailMessage.HtmlContent)
            ? BodyType.Html
            : BodyType.Text;

        return new Microsoft.Graph.Models.Message
        {
            ToRecipients = CreateRecipients(mailMessage.SendTo),
            CcRecipients = CreateRecipients(mailMessage.CcTo),
            BccRecipients = CreateRecipients(mailMessage.BccTo),
            Subject = mailMessage.Subject,
            Body = new ItemBody { ContentType = contentType, Content = content },
            Attachments = CreateAttachments(mailMessage.Attachments)
        };
    }

    private static List<Recipient> CreateRecipients(IEnumerable<SimpleBol.Models.Smtp.EmailAddress>? addresses) =>
        addresses?
            .Where(address => !string.IsNullOrWhiteSpace(address.Email))
            .Select(address => new Recipient
            {
                EmailAddress = new Microsoft.Graph.Models.EmailAddress
                {
                    Address = address.Email,
                    Name = address.Name
                }
            }).ToList() ?? [];

    private static List<Microsoft.Graph.Models.Attachment> CreateAttachments(
        IEnumerable<SimpleBol.Models.Smtp.Attachment>? attachments) =>
        attachments?
            .Where(attachment => !string.IsNullOrWhiteSpace(attachment.FilePath) &&
                File.Exists(attachment.FilePath))
            .Select(attachment => (Microsoft.Graph.Models.Attachment)new FileAttachment
            {
                Name = attachment.FileName ?? Path.GetFileName(attachment.FilePath),
                ContentType = "application/pdf",
                ContentBytes = File.ReadAllBytes(attachment.FilePath!)
            }).ToList() ?? [];

    private sealed class OutlookAuthorizationMarker
    {
        public string? ClientId { get; set; }
        public string? TenantId { get; set; }
        public string? AccountEmail { get; set; }
        public string? TokenCacheName { get; set; }
        public string? SerializedAuthenticationRecord { get; set; }
        public DateTime AuthorizedUtc { get; set; }
    }
}
