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
        private bool _isGeneratingPdf;

        public EmailBolDialog(
            IServiceScopeFactory serviceProvider,
            ICustomerRepository customerRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.customerRepository = customerRepository;
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

                MessageBox.Show(
                    "The BOL PDF is ready to attach:" + Environment.NewLine +
                    GeneratedPdfPath,
                    "BOL PDF Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "EmailBolDialog PDF generation");
                MessageBox.Show("The BOL PDF could not be generated:" + Environment.NewLine +
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
