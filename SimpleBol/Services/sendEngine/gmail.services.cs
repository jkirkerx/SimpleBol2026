using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using static Google.Apis.Gmail.v1.UsersResource.DraftsResource;
using SimpleBol.Models.Smtp;
using MongoDB.Bson;
using Microsoft.VisualBasic.Logging;
using NLog;
using MimeKit;
using SendGrid.Helpers.Mail;
using SimpleBol.Models;

namespace SimpleBol.Services.sendEngine
{    

    public interface IGmailSender
    {
        Task<EmailResponse?> SendEmailMessageAsync(
            SimpleBol.Models.Gmail settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default);

        Task AuthorizeAsync(SimpleBol.Models.Gmail settings,
            CancellationToken cancellationToken = default);

        Task<bool> HasAuthorizationAsync(SimpleBol.Models.Gmail settings);
        
    }    

    public class GmailSender : IGmailSender
    {
        // Initialize the logger
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<EmailResponse?> SendEmailMessageAsync(
            SimpleBol.Models.Gmail settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage,
            CancellationToken cancellationToken = default)
        {
            EmailResponse? smtpResponse = null;
            Google.Apis.Gmail.v1.Data.Message? response = null;

            {
                try
                {
                    // Initialize the Gmail service
                    var service = await GetGmailService(settings, cancellationToken);

                    // Create a new email message
                    var message = CreateMessage(settings, mailMessage);
                    if (message != null)
                    {
                        // Send the email
                        response = await service.Users.Messages.Send(message, "me").ExecuteAsync(cancellationToken);

                        // Email sent successfully
                        smtpResponse = BuildSendGripSmtpResponse(response, null);
                    }

                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    Console.WriteLine(ex.ToString());

                    if (response != null)
                    {
                        // Set smtpResponse according to the error
                        smtpResponse = BuildSendGripSmtpResponse(response, ex);
                    }

                    // Log the error message using NLog
                    logger.Error(ex, "An error occurred while sending email message: " + ex.Message.ToString());
                }

            }            

            return smtpResponse;
        }

        public async Task AuthorizeAsync(SimpleBol.Models.Gmail settings,
            CancellationToken cancellationToken = default)
        {
            ValidateOAuthSettings(settings);
            await new ProtectedDataStore().DeleteAsync<TokenResponse>(CreateAuthorizationKey(settings));
            _ = await GetCredentialAsync(settings, cancellationToken);
        }

        public async Task<bool> HasAuthorizationAsync(SimpleBol.Models.Gmail settings)
        {
            var token = await new ProtectedDataStore()
                .GetAsync<TokenResponse>(CreateAuthorizationKey(settings));
            return !string.IsNullOrWhiteSpace(token?.RefreshToken);
        }

        private async Task<GmailService> GetGmailService(SimpleBol.Models.Gmail settings,
            CancellationToken cancellationToken)
        {
            var credential = await GetCredentialAsync(settings, cancellationToken);
            return new GmailService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "SimpleBol"
            });
        }

        private static async Task<UserCredential> GetCredentialAsync(
            SimpleBol.Models.Gmail settings, CancellationToken cancellationToken)
        {
            ValidateOAuthSettings(settings);
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = settings.ClientId,
                    ClientSecret = settings.ClientSecret
                },
                [GmailService.Scope.GmailSend],
                CreateAuthorizationKey(settings),
                cancellationToken,
                new ProtectedDataStore());
        }

        private static string CreateAuthorizationKey(SimpleBol.Models.Gmail settings) =>
            $"SimpleBol.Gmail.{settings.SentFromEmailAddress?.Trim().ToLowerInvariant() ?? "unknown"}";

        private static void ValidateOAuthSettings(SimpleBol.Models.Gmail settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ClientId) ||
                string.IsNullOrWhiteSpace(settings.ClientSecret))
            {
                throw new InvalidOperationException(
                    "Gmail OAuth Client ID and Client Secret are required.");
            }
        }

        private Google.Apis.Gmail.v1.Data.Message? CreateMessage(
            SimpleBol.Models.Gmail settings,
            SimpleBol.Models.Smtp.MailMessage mailMessage)
        {

            Google.Apis.Gmail.v1.Data.Message? rawMessage = null;

            {
                // Create the email message
                var msg = new MimeKit.MimeMessage();

                // Create the SendFrom object
                var senderEmailAddress = settings.SentFromEmailAddress ?? mailMessage.SendFrom?.Email;
                if (!string.IsNullOrWhiteSpace(senderEmailAddress))
                {
                    var senderName = settings.SentFromName ?? mailMessage.SendFrom?.Name ?? "SimpleBol";
                    msg.From.Add(new MimeKit.MailboxAddress(senderName, senderEmailAddress));
                }

                // Create the SendTo object
                if (mailMessage.SendTo != null)
                {
                    foreach (var sendTo in mailMessage.SendTo)
                    {
                        if (!string.IsNullOrWhiteSpace(sendTo.Email))
                        {
                            msg.To.Add(new MimeKit.MailboxAddress(sendTo.Name, sendTo.Email));
                        }
                    }
                }

                if (mailMessage.Subject != null)
                {
                    msg.Subject = mailMessage.Subject;
                }

                // Add both plain text and HTML versions of the email body
                var builder = new BodyBuilder();
                if (mailMessage.PlainTextContent != null)
                {
                    builder.TextBody = mailMessage.PlainTextContent;
                }

                if (mailMessage.HtmlContent != null)
                {
                    builder.HtmlBody = mailMessage.HtmlContent;
                }

                // Add attachments if available
                if (mailMessage.Attachments != null && mailMessage.Attachments.Count > 0)
                {
                    foreach (var attachment in mailMessage.Attachments)
                    {
                        if (!string.IsNullOrWhiteSpace(attachment.FilePath) &&
                            !string.IsNullOrWhiteSpace(attachment.FileName) &&
                            File.Exists(attachment.FilePath))
                        {
                            byte[] attachmentData = File.ReadAllBytes(attachment.FilePath);
                            builder.Attachments.Add(attachment.FileName, attachmentData);
                        }
                    }
                };

                // Write both message formats and attachments to the Message.Body
                msg.Body = builder.ToMessageBody();

                // Convert the MimeMessage to base64url encoded string
                var msgStream = new MemoryStream();
                msg.WriteTo(msgStream);
                var base64EncodedEmail = Convert.ToBase64String(msgStream.ToArray());
                rawMessage = new Google.Apis.Gmail.v1.Data.Message
                {
                    Raw = base64EncodedEmail
                };

            }            

            return rawMessage;
        }

        private EmailResponse BuildSendGripSmtpResponse(Google.Apis.Gmail.v1.Data.Message response, Exception? ex)
        {
            bool transmissionSuccess = response != null;
            HttpContent? responseBody = null;
            string exceptionMessage = ex?.ToString() ?? "";

            // Calculate SmtpError
            SmtpError errorType = SmtpError.None;
            if (!transmissionSuccess)
            {
                errorType = SmtpError.Smtp_Code; // Consider other status codes for different errors
            }

            if (ex != null)
            {
                errorType = SmtpError.Program_Code;
            }

            HttpStatusCode statusCode = transmissionSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;

            // Construct the response body content if transmission was successful
            if (transmissionSuccess)
            {
                // Convert the message ID and thread ID to JSON string
                string jsonBody = $"{{\"MessageId\": \"{response.Id}\", \"ThreadId\": \"{response.ThreadId}\"}}";
                // Create StringContent with JSON body
                responseBody = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            // Process the response if needed
            var smtpResponse = new EmailResponse(
                success: transmissionSuccess,
                statusCode: statusCode,
                body: responseBody,
                headers: null, // Gmail API response doesn't contain headers
                typeError: errorType,
                exceptionMessage: exceptionMessage
            );

            return smtpResponse;
        }

    }


}
