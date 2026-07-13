using SimpleBol.Classes.DirectPrint;
using SimpleBol.Classes.NewtonSoft;
using SimpleBol.Classes.PDFGenerators;
using SimpleBol.Models;
using SimpleBol.Models.MongoDb;
using SimpleBol.Properties;
using SimpleBol.Repository.MongoDb;
using System.Drawing.Printing;
using System.Windows.Documents;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class PrintBolDialog : Form
    {
        private const string SquaresDocument = "Squares";
        private const string LinesDocument = "Lines";

        public string PrintBolId { get; set; } = null!;
        public string DocumentName { get; set; } = null!;
        public BILLOFLADINGS PrintBol { get; set; } = null!;
        public PrintObject PrintObject { get; set; } = null!;
        public bool PrintComplete { get; set; }
        private bool _isPrinting;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IBolsRepository? bolRepository;

        public PrintBolDialog(
            IServiceScopeFactory serviceProvider,
            IBolsRepository? bolRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.bolRepository = bolRepository;

            var printObject = PrintSettingsJson.GetSettings();
            if (printObject != null)
            {
                string normalizedDocument = NormalizeDocumentName(
                    printObject.PrintSettings.DefaultDocument);
                if (printObject.PrintSettings.DefaultDocument != normalizedDocument)
                {
                    printObject.PrintSettings.DefaultDocument = normalizedDocument;
                    PrintSettingsJson.WriteSettings(printObject);
                }

                this.PrintObject = printObject;
                this.PrintComplete = false;
            }

            // Hide the Print Progress Panel
            panelPrintProgress.Visible = false;
        }

        #region Dialog

        protected void PrintDialog_Load(object sender, EventArgs e)
        {
            DisableEventHandlers();

            SetListViewPrintTemplates();
            LoadListViewPrintTemplates();
            LoadRadioButtons(this.PrintObject);
            LoadPrintCopies(this.PrintObject);
            SetDefaultDocument(this.PrintObject);
            SetToolTips();

            LoadBol();

            EnableEventHandlers();
        }

        protected void PrintDialog_Shown(object sender, EventArgs e)
        {
            this.PrintComplete = false;
            buttonPrint.Enabled = listViewPrintTemplates.SelectedItems.Count == 1;

        }

        #endregion
        #region Load        

        // I removed the code here because it was useless
        // But consider generating a Direct Print Document here

        private void LoadBol()
        {
            if (PrintBol != null && PrintBolId != null)
            {                
                this.DocumentName = "BOL " + PrintBol.CustomerName + " " + PrintBol.OrderNumber;
            }

        }

        #endregion
        #region ListView

        private void SetListViewPrintTemplates()
        {
            // Clear the Listview Items and Columns (if needed)
            listViewPrintTemplates.Items.Clear();
            listViewPrintTemplates.Columns.Clear();

            // Set ListView Parameters
            listViewPrintTemplates.Cursor = Cursors.Hand;
            listViewPrintTemplates.LabelEdit = false;
            listViewPrintTemplates.AllowColumnReorder = false;
            listViewPrintTemplates.CheckBoxes = false;
            listViewPrintTemplates.FullRowSelect = false;
            listViewPrintTemplates.GridLines = false;
            listViewPrintTemplates.Scrollable = true;
            listViewPrintTemplates.MultiSelect = false;
            listViewPrintTemplates.OwnerDraw = false;
            listViewPrintTemplates.Sorting = SortOrder.Ascending;

            if (this.PrintObject != null)
            {
                if (this.PrintObject.PrintSettings != null)
                {
                    if (this.PrintObject.PrintSettings.ListViewIconSize == "LARGE")
                    {
                        listViewPrintTemplates.View = View.LargeIcon;
                    }
                    else
                    {
                        listViewPrintTemplates.View = View.SmallIcon;
                    }
                }
            }

            // Add the owner drawn event function 
            // listViewPrintTemplates.DrawItem += ListViewPrintTemplates_DrawItem;

        }

        private void LoadListViewPrintTemplates()
        {
            // Large Image List
            ImageList largeImageList = new()
            {
                ImageSize = new System.Drawing.Size(130, 130)
            };

            largeImageList.Images.Clear();
            largeImageList.Images.Add(SquaresDocument, Resources.bolDefault250);
            largeImageList.Images.Add(LinesDocument, Resources.bolProfessional250);

            listViewPrintTemplates.LargeImageList = largeImageList;

            // Small Image List
            ImageList smallImageList = new()
            {
                ImageSize = new System.Drawing.Size(65, 65)
            };

            smallImageList.Images.Add(SquaresDocument, Resources.bolDefault250);
            smallImageList.Images.Add(LinesDocument, Resources.bolProfessional250);

            listViewPrintTemplates.SmallImageList = smallImageList;

            // Clear the ListView Items
            listViewPrintTemplates.Items.Clear();

            // Load the ListView Items            
            var squaresTemplate = new ListViewItem("Print As Squares")
            {
                Checked = false,
                ImageKey = SquaresDocument,
                ImageIndex = 0,
                Name = SquaresDocument,
                Text = "Print As Squares",
            };

            listViewPrintTemplates.Items.Add(squaresTemplate);

            // Load the ListView Items            
            var linesTemplate = new ListViewItem("Print As Lines")
            {
                Checked = false,
                ImageKey = LinesDocument,
                ImageIndex = 1,
                Name = LinesDocument,
                Text = "Print As Lines",
            };

            listViewPrintTemplates.Items.Add(linesTemplate);

        }

        private void ListViewPrintTemplates_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false; // Disable ListView's default drawing

            // Get the bounds of the item's icon and text
            Rectangle iconBounds = e.Bounds;
            iconBounds.Width = listViewPrintTemplates.LargeImageList.ImageSize.Width;
            iconBounds.Height = listViewPrintTemplates.LargeImageList.ImageSize.Height; // Set iconBounds height to match the icon

            Rectangle textBounds = e.Bounds;
            textBounds.Y = iconBounds.Bottom; // Position the text below the icon
            textBounds.Height -= iconBounds.Height; // Adjust the height to fit the text

            // Calculate the width required for the text
            System.Drawing.Size textSize = TextRenderer.MeasureText(e.Item.Text, e.Item.Font, new System.Drawing.Size(int.MaxValue, textBounds.Height), TextFormatFlags.WordBreak);
            int textWidth = textSize.Width;

            // Center the text horizontally within the item
            textBounds.X += (textBounds.Width - textWidth) / 2;
            textBounds.Width = textWidth;

            // Draw the item's icon
            if (e.Item.ImageIndex >= 0 && e.Item.ImageIndex < listViewPrintTemplates.LargeImageList.Images.Count)
            {
                Image icon = listViewPrintTemplates.LargeImageList.Images[e.Item.ImageIndex];
                e.Graphics.DrawImage(icon, iconBounds.Location);
            }

            // Draw the item's text with center alignment below the icon
            using (var textBrush = new SolidBrush(e.Item.ForeColor))
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center; // Center the text horizontally
                stringFormat.LineAlignment = StringAlignment.Center; // Center the text vertically

                e.Graphics.DrawString(e.Item.Text, e.Item.Font, textBrush, textBounds, stringFormat);
            }
        }

        private static string NormalizeDocumentName(string? documentName)
        {
            return documentName switch
            {
                "Professional" => LinesDocument,
                "Default" => SquaresDocument,
                LinesDocument => LinesDocument,
                SquaresDocument => SquaresDocument,
                _ => SquaresDocument
            };
        }

        private void SetDefaultDocument(PrintObject printObject)
        {
            var documentKey = NormalizeDocumentName(
                printObject?.PrintSettings?.DefaultDocument);
            if (string.IsNullOrWhiteSpace(documentKey) ||
                !listViewPrintTemplates.Items.ContainsKey(documentKey))
                return;

            var item = listViewPrintTemplates.Items[documentKey];
            item.Selected = true;
            item.Focused = true;
            item.EnsureVisible();

            labelDocumentSelection.Text = documentKey == LinesDocument
                ? "Print As Lines Selected"
                : "Print As Squares Selected";
            buttonPrint.Enabled = true;
        }

        private void ListViewPrintTemplates_SelectedIndexChanged(object? sender, EventArgs e)
        {
            buttonPrint.Enabled = listViewPrintTemplates.SelectedItems.Count == 1;
            if (!buttonPrint.Enabled)
                return;

            var listItem = listViewPrintTemplates.SelectedItems[0];
            var objectId = listItem.Name;

            switch (objectId)
            {
                case SquaresDocument:
                    if (this.PrintObject?.PrintSettings is not null)
                        this.PrintObject.PrintSettings.DefaultDocument = SquaresDocument;
                    labelDocumentSelection.Text = "Print As Squares Selected";
                    break;

                case LinesDocument:
                    if (this.PrintObject?.PrintSettings is not null)
                        this.PrintObject.PrintSettings.DefaultDocument = LinesDocument;
                    labelDocumentSelection.Text = "Print As Lines Selected";
                    break;

                default:
                {
                    buttonPrint.Enabled = false;
                    return;
                }
            }

            // Set the flag
            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOn65;

        }

        #endregion
        #region Buttons

        private async void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (_isPrinting)
                return;

            _isPrinting = true;
            Cursor = Cursors.WaitCursor;

            // Generate PDF
            if (this.PrintObject != null)
            {
                if (this.PrintObject.PrintSettings != null)
                {

                    if (this.PrintObject.PrintSettings.PrintToMethod == PrintMethod.PrintDirect)
                    {
                        // Initialize the native dialog from a PrintDocument. The Windows 11
                        // dialog otherwise starts from the Windows default printer and copies.
                        using var printDocument = new PrintDocument();
                        printDocument.PrinterSettings.Copies =
                            (short)Math.Max(1, this.PrintObject.PrintSettings.PrintNumberOfCopies);

                        string? savedPrinter = this.PrintObject.PrintSettings.DefaultPrinter;
                        if (!string.IsNullOrWhiteSpace(savedPrinter))
                        {
                            var savedPrinterSettings = new PrinterSettings
                            {
                                PrinterName = savedPrinter,
                                Copies = printDocument.PrinterSettings.Copies
                            };

                            if (savedPrinterSettings.IsValid)
                            {
                                savedPrinterSettings.Duplex = savedPrinterSettings.CanDuplex
                                    ? this.PrintObject.PrintSettings.PrintDuplex
                                    : Duplex.Simplex;
                                printDocument.PrinterSettings = savedPrinterSettings;
                            }
                        }

                        printDocument.DefaultPageSettings.Landscape = false;                       

                        // Create the Print Dialog
                        using var printDialog = new PrintDialog
                        {
                            Document = printDocument,
                            ShowNetwork = true,
                            UseEXDialog = false                            
                        };

                        try
                        {

                                // Keep the native printer dialog owned by and in front of this BOL dialog.
                                // Without an owner Windows can place it behind the main application window.
                                Activate();
                                BringToFront();
                                DialogResult result = printDialog.ShowDialog(this);
                                if (result == DialogResult.OK)
                                {
                                    // Persist the dialog selection immediately. This also keeps
                                    // the choice if the spooler later reports a printing error.
                                    this.PrintObject.PrintSettings.DefaultPrinter =
                                        printDocument.PrinterSettings.PrinterName;
                                    this.PrintObject.PrintSettings.PrintNumberOfCopies =
                                        Math.Max(1, (int)printDocument.PrinterSettings.Copies);
                                    this.PrintObject.PrintSettings.PrintDuplex =
                                        printDocument.PrinterSettings.Duplex;
                                    PrintSettingsJson.WriteSettings(this.PrintObject);

                                    // The dialog and the print operation share this same document.
                                    printDocument.DefaultPageSettings.Landscape = false;
                                    printDocument.PrinterSettings.DefaultPageSettings.Landscape = false;

                                    // Assign the Document Name, created in this.LoadBol
                                    printDocument.DocumentName = this.DocumentName;

                                    // Calculate how many pages we need
                                    // We can have 4 pallets and packages per page
                                    int totalItemsCount = 0;
                                    int itemsPerPage = 4;
                                    if (this.PrintBol.Pallets != null) { totalItemsCount = this.PrintBol.Pallets.Count; }
                                    if (this.PrintBol.Packages != null) { totalItemsCount += this.PrintBol.Packages.Count; }

                                    // Integer Division
                                    int totalPages = (totalItemsCount + itemsPerPage - 1) / itemsPerPage;

                                    if (this.PrintObject.PrintSettings.DefaultDocument != null)
                                    {
                                        switch (this.PrintObject.PrintSettings.DefaultDocument)
                                        {
                                            case SquaresDocument:

                                                var printSquares = new DirectPrintBillOfLadingAsSquares
                                                {
                                                    Bol = this.PrintBol,
                                                    CurrentPage = 1,
                                                    BolAppointmentContacts = [],
                                                };

                                                // Subscribe to the PrintPage event of your custom PrintDocument.
                                                printDocument.PrintPage += new PrintPageEventHandler(printSquares.PrintDocument_PrintPage);
                                                printDocument.EndPrint += new PrintEventHandler(printSquares.PrintDocument_EndPrint);

                                                break;

                                            case LinesDocument:

                                                var lineItemCount =
                                                    (this.PrintBol.Pallets?.Count ?? 0) +
                                                    (this.PrintBol.Packages?.Count ?? 0) +
                                                    (this.PrintBol.Containers?.Count ?? 0);
                                                var lineTotalPages = Math.Max(1,
                                                    (lineItemCount + 14) / 15);

                                                var printLines = new DirectPrintBillOfLadingAsLines
                                                {
                                                    Bol = this.PrintBol,
                                                    CurrentPage = 1,
                                                    TotalPages = lineTotalPages,
                                                    BolAppointmentContacts = [],
                                                };

                                                // Subscribe to the PrintPage event of your custom PrintDocument.
                                                printDocument.PrintPage += new PrintPageEventHandler(printLines.PrintDocument_PrintPage);
                                                printDocument.EndPrint += new PrintEventHandler(printLines.PrintDocument_EndPrint);

                                                break;
                                        }

                                        // Start the helper before spooling so it is ready to find this exact job.
                                        await PrintNotificationClient.EnsureRunningAsync();

                                        // Start the printing process.
                                        printDocument.Print();
                                        await PrintNotificationClient.NotifySubmittedAsync(
                                            printDocument.PrinterSettings.PrinterName,
                                            printDocument.DocumentName,
                                            printDocument.PrinterSettings.Copies);
                                        Application.DoEvents();

                                        this.PrintComplete = true;

                                        Application.DoEvents();                                       
                                        

                                    }
                                }
                        } 
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(
                                $"PrintBolDialog_DirectPrint failure:{Environment.NewLine}{ex}");
                            SimpleBol.Classes.Errors.ErrorLogging.NLogException(
                                ex,
                                "PrintBolDialog_DirectPrint");
                            System.Windows.Forms.MessageBox.Show(
                                "An error occurred while printing: " + ex.Message,
                                System.Windows.Forms.Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                    }
                    else if (this.PrintObject.PrintSettings.PrintToMethod == PrintMethod.PrintPdf)
                    {

                        // Calculate how many pages we need
                        // We can have 4 pallets and packages per page
                        int totalItemsCount = 0;
                        int itemsPerPage = 4;
                        if (this.PrintBol.Pallets != null) { totalItemsCount = this.PrintBol.Pallets.Count; }
                        if (this.PrintBol.Packages != null) { totalItemsCount += this.PrintBol.Packages.Count; }

                        // Integer Division
                        int totalPages = (totalItemsCount + itemsPerPage - 1) / itemsPerPage;

                        string documentPath = Resources.PathBolDocuments;

                        switch (this.PrintObject.PrintSettings.DefaultDocument)
                        {
                            case SquaresDocument:

                                var printSquaresPdf = new DirectPrintAsSquaresBol()
                                {
                                    Bol = this.PrintBol,
                                    BolDocumentPath = documentPath,
                                    DocumentName = this.DocumentName,
                                    CurrentPage = 1,
                                    ItemsPerPage = itemsPerPage,
                                    TotalPages = totalPages,
                                    TotalItems = totalItemsCount,
                                };

                                var printSquaresTask = await printSquaresPdf.GeneratePdfAsync();


                                break;

                            case LinesDocument:

                                var printLinesPdf = new DirectPrintAsLinesBol()
                                {
                                    Bol = this.PrintBol,
                                    BolDocumentPath = documentPath,
                                    DocumentName = this.DocumentName,
                                    CurrentPage = 1,
                                    ItemsPerPage = itemsPerPage,
                                    TotalPages = totalPages,
                                    TotalItems = totalItemsCount,
                                };

                                var printLinesTask = await printLinesPdf.GeneratePdfAsync();

                                break;

                        }

                        this.PrintComplete = true;

                    }

                }

            }

            // Save the Settings for the Print Dialog
            if (this.PrintObject != null)
            {
                PrintSettingsJson.WriteSettings(this.PrintObject);
            }            

            Cursor = Cursors.Default;
            _isPrinting = false;
            Application.DoEvents();

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            // Write the Print Settings for this PC User
            PrintSettingsJson.WriteSettings(this.PrintObject);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            if (_isPrinting)
            {
                var result = System.Windows.Forms.MessageBox.Show(
                    "Are you sure you want to cancel this print job?",
                    "Print BOL",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region RadioButtons

        private void LoadRadioButtons(PrintObject printObject)
        {
            if (printObject != null)
            {
                if (printObject.PrintSettings != null)
                {
                    if (printObject.PrintSettings.PrintToMethod == PrintMethod.PrintDirect)
                    {
                        radioButtonDirectPrint.Checked = true;
                        radioButtonPrintToPDF.Checked = false;
                    }
                    else if (printObject.PrintSettings.PrintToMethod == PrintMethod.PrintPdf)
                    {
                        radioButtonDirectPrint.Checked = false;
                        radioButtonPrintToPDF.Checked = true;
                    }
                }
                else
                {
                    radioButtonDirectPrint.Checked = true;
                    radioButtonPrintToPDF.Checked = false;
                }
            }

        }

        private void RadioButtonDirectPrint_CheckedChanged(object? sender, EventArgs e)
        {
            if (radioButtonDirectPrint.Checked == true)
            {
                radioButtonPrintToPDF.Checked = false;

                if (this.PrintObject != null)
                {
                    if (this.PrintObject.PrintSettings != null)
                    {
                        this.PrintObject.PrintSettings.PrintToMethod = PrintMethod.PrintDirect;
                    }
                }
            }
        }

        private void RadioButtonPrintToPDF_CheckedChanged(object? sender, EventArgs e)
        {
            if (radioButtonPrintToPDF.Checked == true)
            {
                radioButtonDirectPrint.Checked = false;

                if (this.PrintObject != null)
                {
                    if (this.PrintObject.PrintSettings != null)
                    {
                        this.PrintObject.PrintSettings.PrintToMethod = PrintMethod.PrintPdf;
                    }
                }
            }
        }

        private void NumericUpDownPrintCopies_ValueChanged(object? sender, EventArgs e)
        {
            if (this.PrintObject != null)
            {
                if (this.PrintObject.PrintSettings != null)
                {
                    if (sender != null)
                    {
                        NumericUpDown numericControl = (NumericUpDown)sender;
                        int numericValue = (int)Math.Round(numericControl.Value);
                        this.PrintObject.PrintSettings.PrintNumberOfCopies = numericValue;
                    }

                }
            }
        }

        #endregion
        #region IconSizeButtons

        private void ButtonLargeIcons_Click(object sender, EventArgs e)
        {
            if (listViewPrintTemplates != null)
            {
                listViewPrintTemplates.View = View.LargeIcon;
                buttonLargeIcons.BackColor = Color.Black;
                buttonSmallIcons.BackColor = Color.FromArgb(38, 38, 38);

                if (this.PrintObject != null)
                {
                    if (this.PrintObject.PrintSettings != null)
                    {
                        this.PrintObject.PrintSettings.ListViewIconSize = "LARGE";
                    }
                }

            }
        }

        private void ButtonSmallIcons_Click(object sender, EventArgs e)
        {
            if (listViewPrintTemplates != null)
            {
                listViewPrintTemplates.View = View.SmallIcon;
                buttonSmallIcons.BackColor = Color.Black;
                buttonLargeIcons.BackColor = Color.FromArgb(38, 38, 38);

                if (this.PrintObject != null)
                {
                    if (this.PrintObject.PrintSettings != null)
                    {
                        this.PrintObject.PrintSettings.ListViewIconSize = "SMALL";
                    }
                }
            }
        }

        #endregion
        #region ToolTips

        private void SetToolTips()
        {
            var ttLargeIcons = new ToolTip()
            {
                ToolTipTitle = "View as Large Icons",
                AutoPopDelay = 10000,
                InitialDelay = 200,
                ReshowDelay = 15000,
                ShowAlways = true
            };
            ttLargeIcons.SetToolTip(this.buttonLargeIcons, "View using Large Icons in a vertical grid");

            var ttSmallIcons = new ToolTip()
            {
                ToolTipTitle = "View as Small Icons",
                AutoPopDelay = 10000,
                InitialDelay = 200,
                ReshowDelay = 15000,
                ShowAlways = true
            };
            ttSmallIcons.SetToolTip(this.buttonSmallIcons, "View using Small Icons organized in vertical rows");

            var ttListView = new ToolTip()
            {
                ToolTipTitle = "Select a Printer Template",
                AutoPopDelay = 10000,
                InitialDelay = 200,
                ReshowDelay = 15000,
                ShowAlways = true
            };
            ttListView.SetToolTip(this.listViewPrintTemplates, "Select a suitable print template");

        }
        #endregion
        #region PrintCopies

        private void LoadPrintCopies(PrintObject printObject)
        {
            if (printObject != null)
            {
                if (printObject.PrintSettings != null)
                {
                    numericUpDownPrintCopies.Value = printObject.PrintSettings.PrintNumberOfCopies;
                }
            }
        }

        #endregion        
        #region EventHandlers

        private void EnableEventHandlers()
        {
            radioButtonDirectPrint.CheckedChanged += RadioButtonDirectPrint_CheckedChanged;
            radioButtonPrintToPDF.CheckedChanged += RadioButtonPrintToPDF_CheckedChanged;
            listViewPrintTemplates.SelectedIndexChanged += ListViewPrintTemplates_SelectedIndexChanged;
            numericUpDownPrintCopies.ValueChanged += NumericUpDownPrintCopies_ValueChanged;
        }

        private void DisableEventHandlers()
        {
            radioButtonDirectPrint.CheckedChanged -= RadioButtonDirectPrint_CheckedChanged;
            radioButtonPrintToPDF.CheckedChanged -= RadioButtonPrintToPDF_CheckedChanged;
            listViewPrintTemplates.SelectedIndexChanged -= ListViewPrintTemplates_SelectedIndexChanged;
            numericUpDownPrintCopies.ValueChanged -= NumericUpDownPrintCopies_ValueChanged;
        }

        #endregion
        #region ProgressBar

        private void UpdateProgressBar(string message, int pbValue)
        {
            panelPrintProgress.Visible = true;
            progressBarPrint.Enabled = true;
            progressBarPrint.Value = pbValue;
            labelPrintProgress.Text = message;
            System.Windows.Forms.Application.DoEvents();
        }


        #endregion

    }
}
