using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SimpleBol.Classes.Errors;
using SimpleBol.Classes.NewtonSoft;
using SimpleBol.Classes.PDFGenerators;
using SimpleBol.Properties;
using System.IO;
using SimpleBol.Classes.Common;
using SimpleBol.Models.Smtp;
using SimpleBol.Services.sendEngine;
using SimpleBol.NewtonSoft;
using SimpleBol.Services;
using System.Text.Json;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class EmailBolDialog : Form
    {
        private const string SquaresDocument = "Squares";
        private const string LinesDocument = "Lines";

        private sealed record EmailContactListItem(string ContactName, string EmailAddress)
        {
            public override string ToString() => $"{ContactName}  <{EmailAddress}>";
        }

        public string CustomerId { get; set; } = null!;
        public CUSTOMERS? customer { get; set; }
        public string EmailBolId { get; set; } = null!;
        public BILLOFLADINGS EmailBol { get; set; } = null!;
        public string? GeneratedPdfPath { get; private set; }
        public IReadOnlyList<string> SelectedEmailAddresses => listBox1.SelectedItems
            .Cast<EmailContactListItem>()
            .Select(contact => contact.EmailAddress)
            .ToList();

        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICustomerRepository? customerRepository;
        private readonly ISendGridSender sendGridSender;
        private readonly IGmailSender gmailSender;
        private readonly IOutlook365Sender outlook365Sender;
        private readonly IEmailTransmissionLogRepository emailLogRepository;
        private readonly ICurrentUserSession currentUserSession;
        private bool _isGeneratingPdf;

        public EmailBolDialog(
            IServiceScopeFactory serviceProvider,
            ICustomerRepository customerRepository,
            ISendGridSender sendGridSender,
            IGmailSender gmailSender,
            IOutlook365Sender outlook365Sender,
            IEmailTransmissionLogRepository emailLogRepository,
            ICurrentUserSession currentUserSession)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.customerRepository = customerRepository;
            this.sendGridSender = sendGridSender;
            this.gmailSender = gmailSender;
            this.outlook365Sender = outlook365Sender;
            this.emailLogRepository = emailLogRepository;
            this.currentUserSession = currentUserSession;
            Shown += EmailBolDialog_Shown;
            OK_Button.DialogResult = DialogResult.None;
            OK_Button.Click += ButtonSend_Click;
            buttonLargeIcons.Click += ButtonLargeIcons_Click;
            buttonSmallIcons.Click += ButtonSmallIcons_Click;
        }

        private async void EmailBolDialog_Shown(object? sender, EventArgs e)
        {
            LoadPrintTemplates();

            if (customer == null && customerRepository != null &&
                !string.IsNullOrWhiteSpace(CustomerId))
            {
                customer = await customerRepository.GetOneCustomerAsync(CustomerId);
            }

            listBox1.Items.Clear();
            listBox1.SelectionMode = SelectionMode.MultiExtended;

            if (customer == null)
                return;

            var contacts = new List<EmailContactListItem>();

            AddContact(contacts, customer.CompanyName, customer.EmailAddress1);
            AddContact(contacts, $"{customer.CompanyName} (Alternate)", customer.EmailAddress2);

            foreach (var location in customer.ShippingLocations ?? [])
            {
                AddContact(contacts, location.Name, location.EmailAddress);
                AddContact(contacts,
                    string.IsNullOrWhiteSpace(location.ContactName)
                        ? $"{location.Name} Contact"
                        : location.ContactName,
                    location.ContactEmailAddress);
            }

            foreach (var contact in contacts
                .DistinctBy(contact => contact.EmailAddress, StringComparer.OrdinalIgnoreCase))
            {
                listBox1.Items.Add(contact);
            }

            if (listBox1.Items.Count == 1)
                listBox1.SetSelected(0, true);
        }

        private void LoadPrintTemplates()
        {
            listViewPrintTemplates.BeginUpdate();
            try
            {
                listViewPrintTemplates.Clear();
                listViewPrintTemplates.MultiSelect = false;
                listViewPrintTemplates.HideSelection = false;
                listViewPrintTemplates.View = View.LargeIcon;

                var largeImages = new ImageList
                {
                    ImageSize = new Size(130, 130),
                    ColorDepth = ColorDepth.Depth32Bit
                };
                largeImages.Images.Add(SquaresDocument, Resources.bolDefault250);
                largeImages.Images.Add(LinesDocument, Resources.bolProfessional250);

                var smallImages = new ImageList
                {
                    ImageSize = new Size(65, 65),
                    ColorDepth = ColorDepth.Depth32Bit
                };
                smallImages.Images.Add(SquaresDocument, Resources.bolDefault250);
                smallImages.Images.Add(LinesDocument, Resources.bolProfessional250);

                listViewPrintTemplates.LargeImageList = largeImages;
                listViewPrintTemplates.SmallImageList = smallImages;
                listViewPrintTemplates.Items.Add(new ListViewItem("Print As Squares")
                {
                    Name = SquaresDocument,
                    ImageKey = SquaresDocument
                });
                listViewPrintTemplates.Items.Add(new ListViewItem("Print As Lines")
                {
                    Name = LinesDocument,
                    ImageKey = LinesDocument
                });

                var savedDocument = PrintSettingsJson.GetSettings()?
                    .PrintSettings?.DefaultDocument;
                var selectedKey = savedDocument is "Lines" or "Professional"
                    ? LinesDocument
                    : SquaresDocument;
                var selectedItem = listViewPrintTemplates.Items[selectedKey];
                selectedItem.Selected = true;
                selectedItem.Focused = true;
                selectedItem.EnsureVisible();
            }
            finally
            {
                listViewPrintTemplates.EndUpdate();
            }
        }

        private void ButtonLargeIcons_Click(object? sender, EventArgs e)
        {
            listViewPrintTemplates.View = View.LargeIcon;
        }

        private void ButtonSmallIcons_Click(object? sender, EventArgs e)
        {
            listViewPrintTemplates.View = View.SmallIcon;
        }

        private static void AddContact(
            ICollection<EmailContactListItem> contacts,
            string? contactName,
            string? emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return;

            contacts.Add(new EmailContactListItem(
                string.IsNullOrWhiteSpace(contactName) ? "Customer Contact" : contactName.Trim(),
                emailAddress.Trim()));
        }

        private async void ButtonSend_Click(object? sender, EventArgs e)
        {
            if (_isGeneratingPdf)
                return;

            if (SelectedEmailAddresses.Count == 0)
            {
                MessageBox.Show("Select at least one customer contact.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listViewPrintTemplates.SelectedItems.Count != 1)
            {
                MessageBox.Show("Select Print As Lines or Print As Squares.",
                    Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            if (EmailBol == null)
            {
                MessageBox.Show("The BOL is not available to generate a PDF.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isGeneratingPdf = true;
            OK_Button.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                var outputFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "SimpleBol");
                Directory.CreateDirectory(outputFolder);

                var documentName = CreateDocumentName(EmailBol);
                const int itemsPerPage = 4;
                var totalItems = (EmailBol.Containers?.Count ?? 0) +
                    (EmailBol.Pallets?.Count ?? 0) +
                    (EmailBol.Packages?.Count ?? 0);
                var totalPages = Math.Max(1,
                    (totalItems + itemsPerPage - 1) / itemsPerPage);

                var selectedDocument = listViewPrintTemplates.SelectedItems[0].Name;
                var useProfessionalLayout = selectedDocument == LinesDocument;
                bool generated;

                if (useProfessionalLayout)
                {
                    var generator = new DirectPrintAsLinesBol
                    {
                        Bol = EmailBol,
                        BolDocumentPath = outputFolder,
                        DocumentName = documentName,
                        CurrentPage = 1,
                        ItemsPerPage = itemsPerPage,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        OpenAfterGeneration = false
                    };
                    generated = await generator.GeneratePdfAsync();
                    GeneratedPdfPath = generator.GeneratedDocumentPath;
                }
                else
                {
                    var generator = new DirectPrintAsSquaresBol
                    {
                        Bol = EmailBol,
                        BolDocumentPath = outputFolder,
                        DocumentName = documentName,
                        CurrentPage = 1,
                        ItemsPerPage = itemsPerPage,
                        TotalPages = totalPages,
                        TotalItems = totalItems,
                        OpenAfterGeneration = false
                    };
                    generated = await generator.GeneratePdfAsync();
                    GeneratedPdfPath = generator.GeneratedDocumentPath;
                }

                if (!generated || string.IsNullOrWhiteSpace(GeneratedPdfPath) ||
                    !File.Exists(GeneratedPdfPath))
                {
                    throw new IOException("The BOL PDF could not be generated.");
                }

                var response = await SendBolAsync(GeneratedPdfPath);
                if (response?.Success != true)
                {
                    var detail = string.IsNullOrWhiteSpace(response?.ExceptionMessage)
                        ? response == null
                            ? "The email provider did not return a response."
                            : $"The email provider returned {response.StatusCode}."
                        : response.ExceptionMessage;
                    throw new InvalidOperationException(detail);
                }

                MessageBox.Show("The BOL was emailed successfully.", "BOL Sent",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "EmailBolDialog send BOL");
                MessageBox.Show("The BOL could not be emailed:" + Environment.NewLine +
                    ex.Message, Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                OK_Button.Enabled = true;
                _isGeneratingPdf = false;
            }
        }

        private async Task<EmailResponse?> SendBolAsync(string pdfPath)
        {
            var settings = AppSettingsJson.GetSmtpApiSettings()
                ?? throw new InvalidOperationException("Email has not been configured.");
            var identifier = EmailBol.BolNumber ?? EmailBol.OrderNumber ?? EmailBol.BolId ?? "Document";
            var mailMessage = new Models.Smtp.MailMessage
            {
                SendTo = listBox1.SelectedItems.Cast<EmailContactListItem>()
                    .Select(contact => new EmailAddress
                    {
                        Email = contact.EmailAddress,
                        Name = contact.ContactName
                    }).ToList(),
                Subject = $"Bill of Lading {identifier}",
                PlainTextContent = $"Please find bill of lading {identifier} attached.",
                HtmlContent = $"<p>Please find bill of lading {System.Net.WebUtility.HtmlEncode(identifier)} attached.</p>",
                Attachments =
                [
                    new Models.Smtp.Attachment
                    {
                        FilePath = pdfPath,
                        FileName = Path.GetFileName(pdfPath)
                    }
                ]
            };

            switch (settings.DefaultId?.ToUpperInvariant())
            {
                case "SENDGRID" when settings.SendGrid != null:
                    DecryptSecret(settings.SendGrid);
                    mailMessage.SendFrom = CreateFromAddress(settings.SendGrid.SentFromEmailAddress,
                        settings.SendGrid.SentFromName, settings.CompanyInfo?.CompanyName);
                    return await sendGridSender.SendEmailMessageAsync(settings.SendGrid, mailMessage);

                case "GMAIL" when settings.Gmail != null:
                    DecryptSecret(settings.Gmail);
                    mailMessage.SendFrom = CreateFromAddress(settings.Gmail.SentFromEmailAddress,
                        settings.Gmail.SentFromName, settings.CompanyInfo?.CompanyName);
                    return await SendGmailWithLoggingAsync(settings.Gmail, mailMessage, pdfPath);

                case "OUTLOOK365" when settings.Outlook365 != null:
                    DecryptSecret(settings.Outlook365);
                    mailMessage.SendFrom = CreateFromAddress(settings.Outlook365.SentFromEmailAddress,
                        settings.Outlook365.SentFromName, settings.CompanyInfo?.CompanyName);
                    return await outlook365Sender.SendEmailAsync(settings.Outlook365, mailMessage);

                default:
                    throw new InvalidOperationException("Select a default email provider in SMTP API Settings.");
            }
        }

        private async Task<EmailResponse?> SendGmailWithLoggingAsync(
            Models.Gmail gmailSettings,
            Models.Smtp.MailMessage mailMessage,
            string pdfPath)
        {
            var transmission = await emailLogRepository.AddAsync(new EmailTransmissionLog
            {
                Provider = EmailProviders.Gmail,
                Status = EmailTransmissionStatuses.Pending,
                UserId = currentUserSession.Account?.LoginId
                    ?? currentUserSession.Account?.AccountId,
                RelatedDocumentType = "BOL",
                RelatedDocumentId = EmailBol.BolId,
                RelatedDocumentNumber = EmailBol.BolNumber ?? EmailBol.OrderNumber,
                From = ToLogAddress(mailMessage.SendFrom),
                To = ToLogAddresses(mailMessage.SendTo),
                Cc = ToLogAddresses(mailMessage.CcTo),
                Bcc = ToLogAddresses(mailMessage.BccTo),
                Subject = mailMessage.Subject,
                Attachments = CreateLogAttachments(mailMessage.Attachments)
            });

            EmailResponse? response;
            try
            {
                response = await gmailSender.SendEmailMessageAsync(gmailSettings, mailMessage);
            }
            catch (Exception ex)
            {
                await emailLogRepository.MarkFailedAsync(
                    transmission.TransmissionId,
                    ex.Message,
                    ex.GetType().Name);
                throw;
            }

            if (response?.Success == true)
            {
                var (messageId, requestId) = await ReadGmailIdsAsync(response);
                await emailLogRepository.MarkAcceptedAsync(
                    transmission.TransmissionId,
                    messageId,
                    requestId,
                    response.StatusCode.ToString(),
                    (int)response.StatusCode);
            }
            else
            {
                await emailLogRepository.MarkFailedAsync(
                    transmission.TransmissionId,
                    response?.ExceptionMessage ?? "Gmail did not return a response.",
                    response?.TypeError.ToString(),
                    providerStatus: response?.StatusCode.ToString(),
                    httpStatusCode: response == null ? null : (int)response.StatusCode);
            }

            return response;
        }

        private static EmailLogAddress? ToLogAddress(EmailAddress? address)
        {
            if (address == null)
                return null;

            return new EmailLogAddress { Email = address.Email, Name = address.Name };
        }

        private static List<EmailLogAddress> ToLogAddresses(IEnumerable<EmailAddress>? addresses) =>
            addresses?.Select(address => new EmailLogAddress
            {
                Email = address.Email,
                Name = address.Name
            }).ToList() ?? [];

        private static List<EmailLogAttachment> CreateLogAttachments(
            IEnumerable<Models.Smtp.Attachment>? attachments) =>
            attachments?.Select(attachment => new EmailLogAttachment
            {
                FileName = attachment.FileName,
                ContentType = "application/pdf",
                SizeBytes = !string.IsNullOrWhiteSpace(attachment.FilePath) &&
                    File.Exists(attachment.FilePath)
                        ? new FileInfo(attachment.FilePath).Length
                        : null
            }).ToList() ?? [];

        private static async Task<(string? MessageId, string? ThreadId)> ReadGmailIdsAsync(
            EmailResponse response)
        {
            if (response.Body == null)
                return (null, null);

            try
            {
                var json = await response.Body.ReadAsStringAsync();
                using var document = JsonDocument.Parse(json);
                var root = document.RootElement;
                var messageId = root.TryGetProperty("MessageId", out var message)
                    ? message.GetString()
                    : null;
                var threadId = root.TryGetProperty("ThreadId", out var thread)
                    ? thread.GetString()
                    : null;
                return (messageId, threadId);
            }
            catch (JsonException)
            {
                return (null, null);
            }
        }

        private static EmailAddress CreateFromAddress(string? email, string? configuredName,
            string? companyName)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new InvalidOperationException("The sender email address is not configured.");

            return new EmailAddress
            {
                Email = email,
                Name = configuredName ?? companyName ?? Application.ProductName
            };
        }

        private static void DecryptSecret(Models.SendGrid settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.ApiKey) && settings.Salt != null)
                settings.ApiKey = EncryptDecryptAes.DecryptText(settings.ApiKey, settings.Salt);
        }

        private static void DecryptSecret(Models.Outlook365 settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.ClientSecret) && settings.Salt != null)
                settings.ClientSecret = EncryptDecryptAes.DecryptText(settings.ClientSecret, settings.Salt);
        }

        private static void DecryptSecret(Models.Gmail settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.ClientSecret) && settings.Salt != null)
                settings.ClientSecret = EncryptDecryptAes.DecryptText(settings.ClientSecret, settings.Salt);
        }

        private static string CreateDocumentName(BILLOFLADINGS bol)
        {
            var identifier = bol.BolNumber ?? bol.OrderNumber ?? bol.BolId ?? "Document";
            var documentName = $"BOL {identifier}";
            foreach (var invalidCharacter in Path.GetInvalidFileNameChars())
                documentName = documentName.Replace(invalidCharacter, '_');

            return documentName;
        }
    }
}
