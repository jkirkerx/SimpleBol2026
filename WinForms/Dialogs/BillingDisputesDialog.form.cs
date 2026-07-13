using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class BillingDisputeDialog : Form
    {
        public string DisputeId { get; set; } = null!;
        public BILLINGDISPUTES Dispute { get; set; } = null!;
        public SHIPPER Shipper { get; set; } = null!;

        public string BolId { get; set; } = null!;
        public BILLOFLADINGS Bol { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IBillingDisputesRepository? billingDisputesRepository;
        private readonly IShipperRepository? shipperRepository;
        private readonly IBolsRepository? bolsRepository;

        public BillingDisputeDialog(
            IServiceScopeFactory serviceProvider,
            IBillingDisputesRepository billingDisputesRepository,
            IShipperRepository shipperRepository,
            IBolsRepository? bolsRepository)
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.billingDisputesRepository = billingDisputesRepository;
            this.shipperRepository = shipperRepository;
            this.bolsRepository = bolsRepository;
        }

        #region Dialog

        protected async void BillingDisputesDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            LoadShipperContactsBlank();

            if (this.BolId != null && this.DisputeId == null)
            {
                // Create a new Dispute Object, Generate the BOL data into a Dispute
                GenerateBillingDispute(this.BolId);
            }
            else if (this.DisputeId != "ADD" && this.BolId == null)
            {
                // Edit an existing Dispute
                comboBoxShippers.Enabled = false;
                LoadBillingDisputeAsync(this.DisputeId);
            }
            else
            {
                // Create a new Dispute
                this.Dispute = new BILLINGDISPUTES();

                // With event handlers disabled
                // Only present a list of shippers if starting from scratch
                _ = await LoadComboBoxShippersAll();

                // Turn off Error Control
                textBoxAdjustedPrice.Validating -= TextBoxCurrencyAmount_Validating;
                textBoxCreditedAmount.Validating -= TextBoxCurrencyAmount_Validating;
            }

        }

        protected void BillingDisputesDialog_Shown(object sender, EventArgs e)
        {

            EnableEventHandlers();

            comboBoxShippers.Enabled = true;
            comboBoxShippers.Focus();

            Cursor = Cursors.Default;
        }

        #endregion
        #region Generate

        /// <summary>
        /// Automatically generate a dispute using data from the BOL
        /// 07/23/2023 by jKirkerx
        /// </summary>
        /// <param name="bolId"></param>
        private async void GenerateBillingDispute(string bolId)
        {
            DisableEventHandlers();

            if (bolsRepository != null)
            {
                var getBol = await bolsRepository.GetOneBillOfLaddingAsync(bolId);
                if (getBol != null)
                {

                    if (this.Dispute == null)
                    {
                        this.Dispute = new BILLINGDISPUTES();
                    }

                    // Populate the Dipute Object
                    this.Dispute.DisputedBolId = getBol.BolId;
                    this.Dispute.DisputedBol = getBol;
                    this.Dispute.ShipperId = getBol.ShipperId;
                    this.Dispute.Shipper = getBol.Shipper;
                    this.Dispute.ShipperName = getBol.ShipperName;
                    this.Dispute.ShipperQuoteNumber = getBol.ShipperQuoteNumber;
                    this.Dispute.ShipperInvoiceNumber = ""; // Mistake in model design
                    this.Dispute.ShipperReferenceNumber = getBol.ShipperReferenceNumber;
                    this.Dispute.ShipperContacts = getBol.Shipper?.ShipperContacts;
                    this.Dispute.CustomerId = getBol.CustomerId;
                    this.Dispute.Customer = getBol.Customer;
                    this.Dispute.ActualPrice = getBol.ActualShippingPrice;
                    this.Dispute.QuotedPrice = getBol.ShipperQuotePrice;
                    this.Dispute.OrderNumber = getBol.OrderNumber;

                    // Populate the Dialog

                    DisableEventHandlers();

                    // Shipper
                    if (getBol.Shipper != null)
                    {
                        LoadComboBoxShipper(getBol.Shipper);
                    }

                    // Assign the Shipper
                    if (comboBoxShippers.Items.Count > 0)
                    {
                        comboBoxShippers.SelectedValue = getBol.ShipperId;

                        if (getBol.Shipper != null)
                        {
                            labelShipperName.Text = getBol.Shipper.CompanyName;
                            labelShipperAddress.Text = getBol.Shipper.Address1;
                            labelShipperCity.Text = getBol.Shipper.City;
                            labelShipperRegion.Text = getBol.Shipper.RegionLongName;
                            labelShipperCountry.Text = getBol.Shipper.CountryLongName;
                            labelShipperPostalCode.Text = getBol.Shipper.PostalCode;
                        }

                    }

                    DisableEventHandlers();

                    // Load and Select the Shipper Contacts
                    if (getBol.Shipper != null)
                    {
                        if (getBol.Shipper.ShipperContacts != null)
                        {
                            LoadShipperContacts(getBol.Shipper.ShipperContacts);
                        }
                    }

                    DisableEventHandlers();

                    // Bol References //

                    // Load and SET Quote Number                    
                    if (getBol.ShipperId != null && getBol.ShipperQuoteNumber != null)
                    {
                        LoadShipperQuoteNumber(getBol.ShipperQuoteNumber);
                    }

                    DisableEventHandlers();

                    // Quoted Price
                    maskedTextBoxQuotedPrice.Text = getBol.ShipperQuotePrice.ToString();

                    // Actual Price
                    maskedTextBoxActualPrice.Text = getBol.ShipperActualPrice.ToString();

                    // Reference Number
                    maskedTextBoxReferenceNumber.Text = getBol.ShipperReferenceNumber;

                    // Order Number
                    maskedTextBoxOrderNumber.Text = getBol.OrderNumber;

                    // Transit Information //

                    // Ship Date
                    dateTimePickerShipDate.Value = getBol.ShipDate;

                    // Delivery Date
                    dateTimePickerDeliveryDate.Value = getBol.DeliveryDate;

                    // Estimated Transit Days
                    textBoxEstimatedTransitDays.Text = getBol.TransitDaysEstimated.ToString();

                    // Actual Transit Days
                    textBoxActualTransitDays.Text = getBol.TransitDaysActual.ToString();

                    // Fill in the subject
                    textBoxDisputeSubject.Text = "Rate dispute for your quote " + getBol.ShipperQuoteNumber;

                    // This dispute is not reolved
                    checkBoxResolved.Checked = false;

                    // Change the DisputeId, so we can save the dispute
                    this.DisputeId = "ADD";

                }

            }

        }

        #endregion
        #region Load Save 

        private async void LoadBillingDisputeAsync(string disputeId)
        {
            if (billingDisputesRepository != null)
            {
                var getDispute = await billingDisputesRepository.GetOneBillingDisputeAsync(disputeId);
                if (getDispute != null)
                {
                    // Dispute
                    this.Dispute = getDispute;

                    // Bol
                    if (getDispute.DisputedBol != null)
                    {
                        this.Bol = getDispute.DisputedBol;
                    }
                    else
                    {
                        if (bolsRepository != null)
                        {
                            this.Bol = await bolsRepository.GetOneBillOfLaddingAsync(getDispute.DisputedBolId);
                        }
                    }

                    DisableEventHandlers();

                    // Shipper


                    if (getDispute.Shipper != null)
                    {
                        this.Shipper = getDispute.Shipper;

                        LoadComboBoxShipper(getDispute.Shipper);
                        comboBoxShippers.SelectedValue = getDispute.ShipperId;

                        labelShipperName.Text = getDispute.Shipper.CompanyName;
                        labelShipperAddress.Text = getDispute.Shipper.Address1;
                        labelShipperCity.Text = getDispute.Shipper.City;
                        labelShipperRegion.Text = getDispute.Shipper.RegionLongName;
                        labelShipperCountry.Text = getDispute.Shipper.CountryLongName;
                        labelShipperPostalCode.Text = getDispute.Shipper.PostalCode;
                    }
                    else
                    {
                        if (shipperRepository != null)
                        {
                            if (getDispute.ShipperId != null)
                            {
                                this.Shipper = await shipperRepository.GetOneShipperAsync(getDispute.ShipperId);
                            }
                        }
                    }

                    DisableEventHandlers();

                    // Shipper Contacts
                    // Load the Shipper Contacts
                    if (getDispute.ShipperContacts != null)
                    {
                        LoadShipperContacts(getDispute.ShipperContacts);
                        comboBoxShipperContacts.SelectedValue = getDispute.ShipperContactId;
                    }
                    else
                    {
                        if (this.Shipper != null)
                        {
                            if (this.Shipper.ShipperContacts != null)
                            {
                                LoadShipperContacts(this.Shipper.ShipperContacts);
                                comboBoxShipperContacts.SelectedValue = getDispute.ShipperContactId;
                            }
                        }
                    }

                    DisableEventHandlers();

                    // Load Shipper Quote Numbers
                    if (getDispute.ShipperQuoteNumber != null)
                    {
                        LoadShipperQuoteNumber(getDispute.ShipperQuoteNumber);
                    }

                    // References
                    comboBoxShipperQuoteNumbers.SelectedValue = getDispute.ShipperQuoteNumber;
                    maskedTextBoxQuotedPrice.Text = getDispute.QuotedPrice.ToString();
                    maskedTextBoxActualPrice.Text = getDispute.ActualPrice.ToString();
                    maskedTextBoxReferenceNumber.Text = getDispute.ShipperReferenceNumber;
                    maskedTextBoxOrderNumber.Text = getDispute.OrderNumber;

                    // Transit
                    if (getDispute.ShipperShipDate != null)
                    {
                        dateTimePickerShipDate.Value = getDispute.ShipperShipDate.Value;
                    }

                    if (getDispute.ShipperDeliveryDate != null)
                    {
                        dateTimePickerDeliveryDate.Value = getDispute.ShipperDeliveryDate.Value;
                    }

                    if (getDispute.DisputedBol != null)
                    {
                        textBoxEstimatedTransitDays.Text = getDispute.DisputedBol.TransitDaysEstimated.ToString();
                        textBoxActualTransitDays.Text = getDispute.DisputedBol.TransitDaysActual.ToString();
                    }

                    // Billing Dispute
                    textBoxDisputeSubject.Text = getDispute.Subject;
                    textBoxArgument.Text = getDispute.Argument;
                    textBoxRemarks.Text = getDispute.Remarks;

                    // Billing Resolution
                    textBoxAdjustedPrice.Text = getDispute.AdjustedPrice.ToString();
                    textBoxCreditedAmount.Text = getDispute.CreditedAmount.ToString();
                    dateTimePickerResolutionDate.Value = getDispute.ResolutionDate;
                    checkBoxResolved.Checked = getDispute.Resolved == true ? true : false;
                    textBoxResolution.Text = getDispute.Resolution;

                }

            }

            await Task.Delay(100);
        }

        private async Task<bool> SaveBillingDisputeAsync()
        {
            bool vFlag = true;

            if (this.Dispute == null) { this.Dispute = new BILLINGDISPUTES();  }

            // Shipper
            if (comboBoxShippers.SelectedIndex > -1)
            {
                this.Dispute.ShipperId = (comboBoxShippers.SelectedValue as dynamic).ToString();
                comboBoxShippers.BackColor = SystemColors.Window;
            }
            else
            {
                comboBoxShippers.BackColor = Color.LightSalmon;
                vFlag = false;
            }

            // Shipper Contact
            if (comboBoxShipperContacts.SelectedIndex > -1)
            {
                this.Dispute.ShipperContactId = (comboBoxShipperContacts.SelectedValue as dynamic).ToString();
                comboBoxShipperContacts.BackColor = SystemColors.Window;
            }
            else
            {
                comboBoxShipperContacts.BackColor = Color.LightSalmon;
                vFlag = false;
            }

            // Shipper Quote Number
            if (comboBoxShipperQuoteNumbers.SelectedIndex > -1)
            {
                this.Dispute.ShipperQuoteNumber = (comboBoxShipperQuoteNumbers.SelectedValue as dynamic).ToString();
                comboBoxShipperQuoteNumbers.BackColor = SystemColors.Window;
            }
            else
            {
                comboBoxShipperQuoteNumbers.BackColor = Color.LightSalmon;
                vFlag = false;
            }

            if (vFlag)
            {
                // SHIPPER - This should stil exist from the Load Dialog
                if (this.Dispute.Shipper == null)
                {
                    if (shipperRepository != null && this.Dispute.ShipperId != null)
                    {
                        this.Dispute.Shipper = await shipperRepository.GetOneShipperAsync(this.Dispute.ShipperId);
                        this.Dispute.ShipperName = this.Dispute.Shipper.CompanyName;
                    }
                }

                // BOL - this should still exists from the Load Dialog
                if (this.Dispute.DisputedBol == null)
                {
                    if (bolsRepository != null && this.Dispute.DisputedBolId != null)
                    {
                        this.Dispute.DisputedBolId = this.BolId;
                        this.Dispute.DisputedBol = await bolsRepository.GetOneBillOfLaddingAsync(this.BolId);
                    }
                }

                // Shipper Contacts - This should exists from the Load Dialog
                if (this.Dispute.ShipperContacts == null)
                {
                    if (this.Shipper != null)
                    {
                        if (this.Shipper.ShipperContacts != null)
                        {
                            this.Dispute.ShipperContacts = this.Shipper.ShipperContacts;
                        }
                    }
                    else
                    {
                        if (shipperRepository != null && this.Dispute.ShipperId != null)
                        {
                            var shipperContacts = await shipperRepository.GetShipperContactsList(this.Dispute.ShipperId);
                            if (shipperContacts != null)
                            {
                                this.Dispute.ShipperContacts = shipperContacts;
                            }
                        }
                    }

                }

                // Numbers First, then save the text controls
                // Used ChatGPT4 to generate this code

                // Quoted Price
                bool eQuotedPriceResult = decimal.TryParse(maskedTextBoxQuotedPrice.Text.Trim(), out decimal eQuotedPriceValue);
                this.Dispute.QuotedPrice = eQuotedPriceResult == true ? eQuotedPriceValue : 0;

                // Actual Price
                bool eActualPriceResult = decimal.TryParse(maskedTextBoxActualPrice.Text.Trim(), out decimal eActualPriceValue);
                this.Dispute.ActualPrice = eActualPriceResult == true ? eActualPriceValue : 0;

                // Adjusted Price
                bool eAdjustedPriceResult = decimal.TryParse(textBoxAdjustedPrice.Text.Trim(), out decimal eAdjustedPriceValue);
                this.Dispute.AdjustedPrice = eAdjustedPriceResult == true ? eAdjustedPriceValue : 0;

                // Credited Amount
                bool eCreditedAmountResult = decimal.TryParse(textBoxCreditedAmount.Text.Trim(), out decimal eCreditedAmountValue);
                this.Dispute.CreditedAmount = eCreditedAmountResult == true ? eCreditedAmountValue : 0;

                // Shipper Invoice Number - sort of a mistake in model design, use the reference number
                this.Dispute.ShipperInvoiceNumber = maskedTextBoxReferenceNumber.Text.Trim();

                // Reference/Order Number
                this.Dispute.ShipperReferenceNumber = maskedTextBoxReferenceNumber.Text.Trim();
                this.Dispute.OrderNumber = maskedTextBoxOrderNumber.Text.Trim();

                // Transit
                this.Dispute.ShipperShipDate = dateTimePickerShipDate.Value;
                this.Dispute.ShipperDeliveryDate = dateTimePickerDeliveryDate.Value;

                // Dispute Name
                this.Dispute.Name = this.Dispute.ShipperName + " - " + this.Dispute.ShipperQuoteNumber;

                // The dispute date is established once, when the dispute is created.
                if (this.DisputeId == "ADD")
                {
                    this.Dispute.DisputeDate = DateTime.UtcNow;
                }

                // Dispute Subject
                this.Dispute.Subject = textBoxDisputeSubject.Text.Trim();

                // Dispute Litigation or Argument
                this.Dispute.Argument = textBoxArgument.Text.Trim();

                // Dispute Remarks or personal thought
                this.Dispute.Remarks = textBoxRemarks.Text.Trim();

                // Resolution Date
                this.Dispute.ResolutionDate = dateTimePickerResolutionDate.Value;

                // Resolution
                this.Dispute.Resolution = textBoxResolution.Text.Trim();

                // Resolved - Don't use the suggested simplify, it doesn't work right
                this.Dispute.Resolved = checkBoxResolved.Checked == true ? true : false;

                // Write the Dispute to the Repository
                if (billingDisputesRepository != null)
                {
                    if (this.DisputeId == "ADD")
                    {
                        this.Dispute.CreatedOnUtc = DateTime.UtcNow;
                        this.Dispute.UpdatedOnUtc = DateTime.UtcNow;

                        vFlag = await billingDisputesRepository.AddBillingDisputeAsync(this.Dispute);


                        if (bolsRepository != null)
                        {
                            // Update the Bol as Disputed
                            _ = await bolsRepository.UpdateBillOfLaddingDisputedFlagAsync(this.BolId, true);
                        }
                        
                    }
                    else
                    {
                        this.Dispute.UpdatedOnUtc = DateTime.UtcNow;
                        vFlag = await billingDisputesRepository.UpdateBillingDisputeAsync(this.Dispute, this.DisputeId);

                        // Mark the BOL as no longer being disputed
                        if (this.Dispute.Resolved == true)
                        {
                            if (bolsRepository != null)
                            {
                                _ = await bolsRepository.UpdateBillOfLaddingDisputedFlagAsync(this.BolId, false);
                            }
                        }
                    }

                }

            }

            return vFlag;
        }


        #endregion
        #region Buttons

        private async void ButtonAddShipperContact_Click(object sender, EventArgs e)
        {
            if (this.Shipper == null)
            {
                MessageBox.Show("Please select a shipper first, then you can select a contact", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (serviceProvider != null)
                {
                    if (this.Shipper != null)
                    {
                        if (this.Shipper.ShipperId != null)
                        {
                            using var shipperContactDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperContactEditorDialog>();
                var shipperContactDialogDI = shipperContactDialogDIOwned.Form;
                            shipperContactDialogDI.StartPosition = FormStartPosition.CenterScreen;
                            shipperContactDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                            shipperContactDialogDI.TopMost = true;
                            shipperContactDialogDI.ContactId = "ADD";
                            shipperContactDialogDI.ShipperId = this.Shipper.ShipperId;
                            shipperContactDialogDI.Shipper = this.Shipper;
                            shipperContactDialogDI.Refresh();

                            if (shipperContactDialogDI.ShowDialog() == DialogResult.OK)
                            {
                                Cursor = Cursors.WaitCursor;

                                if (this.Shipper != null)
                                {
                                    if (this.Shipper.ShipperContacts == null) { this.Shipper.ShipperContacts = new List<ShipperContacts>(); }
                                    this.Shipper.ShipperContacts.Add(shipperContactDialogDI.ShipperContact);

                                    // Update the Shipper in the Repository
                                    if (shipperRepository != null)
                                    {
                                        _ = await shipperRepository.UpdateShipperContactsAsync(this.Shipper.ShipperContacts, this.Shipper.ShipperId);
                                    }

                                    LoadShipperContacts(this.Shipper.ShipperContacts);
                                }

                                Cursor = Cursors.Default;

                            }
                        }
                    }
                }

            }

        }

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveBillingDisputeAsync();
            Cursor = Cursors.Default;

            if (validate)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region ComboBoxes

        private async Task<int> LoadComboBoxShippersAll()
        {

            int shippersCount = 0;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxShippers.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxShippers.DataSource = currentDataTable;
            }

            if (shipperRepository != null)
            {

                var getAllShippers = await shipperRepository.GetAllShippersAsync();
                if (getAllShippers != null)
                {
                    shippersCount = getAllShippers.Count();

                    if (shippersCount > 0)
                    {

                        var dtShippers = new DataTable("dtShippers");
                        dtShippers.Columns.Add(new DataColumn("Key"));
                        dtShippers.Columns.Add(new DataColumn("Value"));

                        var rsDefault = dtShippers.NewRow();
                        rsDefault[0] = "-- Make a selection --";
                        rsDefault[1] = 0;
                        dtShippers.Rows.Add(rsDefault);

                        foreach (var shipperItem in getAllShippers.OrderBy(ob => ob?.CompanyName))
                        {
                            if (shipperItem != null)
                            {
                                var rsShipperItem = dtShippers.NewRow();
                                rsShipperItem[0] = shipperItem.CompanyName;
                                rsShipperItem[1] = shipperItem.ShipperId;
                                dtShippers.Rows.Add(rsShipperItem);
                            }
                        }

                        dtShippers.AcceptChanges();

                        comboBoxShippers.DataSource = dtShippers;
                        comboBoxShippers.DisplayMember = "Key";
                        comboBoxShippers.ValueMember = "Value";
                        comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxShippers.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBoxShippers.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxShippers.SelectedIndex = 0;

                    }
                }

            }

            return shippersCount;

        }

        /// <summary>
        /// Special Function to generate a dispute from BOLs
        /// We need to be compatible for all methods of dispute generation, such as automatic and manual disputes
        /// </summary>
        /// <param name="shipperQuoteNumber"></param>
        private void LoadComboBoxShipper(SHIPPER shipper)
        {
            // Disable this event, but don't enable it, it will be enables at the end of generating the dispute
            comboBoxShippers.SelectedIndexChanged -= ComboBoxShippers_SelectedIndexChanged;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxShippers.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxShippers.DataSource = currentDataTable;
            }

            var dtShippers = new DataTable("dtShippers");
            dtShippers.Columns.Add(new DataColumn("Key"));
            dtShippers.Columns.Add(new DataColumn("Value"));

            var rsShipper = dtShippers.NewRow();
            rsShipper[0] = shipper.CompanyName;
            rsShipper[1] = shipper.ShipperId;
            dtShippers.Rows.Add(rsShipper);

            dtShippers.AcceptChanges();

            comboBoxShippers.DataSource = dtShippers;
            comboBoxShippers.DisplayMember = "Key";
            comboBoxShippers.ValueMember = "Value";
            comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxShippers.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxShippers.AutoCompleteMode = AutoCompleteMode.None;
            comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Set the Index to 0
            comboBoxShippers.SelectedIndex = 0;

        }

        private async void ComboBoxShippers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string shipperId = (comboBoxShippers.SelectedValue as dynamic).ToString();
            if (shipperId != null)
            {
                if (shipperRepository != null)
                {
                    var getShipper = await shipperRepository.GetOneShipperAsync(shipperId);
                    if (getShipper != null)
                    {
                        // Set the Dialogs Shipper Object
                        this.Shipper = getShipper;

                        labelShipperName.Text = getShipper.CompanyName;
                        labelShipperAddress.Text = getShipper.Address1;
                        labelShipperCity.Text = getShipper.City;
                        labelShipperRegion.Text = getShipper.RegionLongName;
                        labelShipperPostalCode.Text = getShipper.PostalCode;
                        labelShipperCountry.Text = getShipper.CountryLongName;
                        panelShipper.Visible = true;

                        if (getShipper.ShipperContacts != null)
                        {
                            if (getShipper.ShipperContacts.Count > 0)
                            {
                                LoadShipperContacts(getShipper.ShipperContacts);
                            }
                        }

                        if (getShipper.ShipperId != null)
                        {
                            _ = await LoadShipperQuoteNumbers(getShipper.ShipperId);
                        }

                    }
                }

            }

        }

        private void LoadShipperContactsBlank()
        {
            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxShipperContacts.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxShipperContacts.DataSource = currentDataTable;
            }

            var dtShipperContacts = new DataTable("dtShipperContacts");
            dtShipperContacts.Columns.Add(new DataColumn("Key"));
            dtShipperContacts.Columns.Add(new DataColumn("Value"));

            var rsDefault = dtShipperContacts.NewRow();
            rsDefault[0] = "-- Select a shipper first --";
            rsDefault[1] = 0;
            dtShipperContacts.Rows.Add(rsDefault);

            dtShipperContacts.AcceptChanges();

            comboBoxShipperContacts.DataSource = dtShipperContacts;
            comboBoxShipperContacts.DisplayMember = "Key";
            comboBoxShipperContacts.ValueMember = "Value";
            comboBoxShipperContacts.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxShipperContacts.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxShipperContacts.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxShipperContacts.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxShipperContacts.SelectedIndex = 0;

        }

        private void LoadShipperContacts(List<ShipperContacts> shipperContacts)
        {
            if (shipperContacts != null)
            {
                // New method of clearing the ComboBox               
                DataTable currentDataTable = (DataTable)comboBoxShipperContacts.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxShipperContacts.DataSource = currentDataTable;
                }

                var dtShipperContacts = new DataTable("dtShipperContacts");
                dtShipperContacts.Columns.Add(new DataColumn("Key"));
                dtShipperContacts.Columns.Add(new DataColumn("Value"));

                var rsDefault = dtShipperContacts.NewRow();
                rsDefault[0] = "-- Make a selection --";
                rsDefault[1] = 0;
                dtShipperContacts.Rows.Add(rsDefault);

                foreach (var shipperContact in shipperContacts.OrderBy(ob => ob?.ContactName))
                {
                    if (shipperContact != null)
                    {
                        var rsShipperContact = dtShipperContacts.NewRow();
                        rsShipperContact[0] = shipperContact.ContactName;
                        rsShipperContact[1] = shipperContact.ContactId;
                        dtShipperContacts.Rows.Add(rsShipperContact);
                    }
                }

                dtShipperContacts.AcceptChanges();

                comboBoxShipperContacts.DataSource = dtShipperContacts;
                comboBoxShipperContacts.DisplayMember = "Key";
                comboBoxShipperContacts.ValueMember = "Value";
                comboBoxShipperContacts.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxShipperContacts.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxShipperContacts.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxShipperContacts.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxShipperContacts.SelectedIndex = 0;

            }

        }

        private async Task<int> LoadShipperQuoteNumbers(string shipperId)
        {
            int bolCount = 0;

            if (bolsRepository != null)
            {
                var getBols = await bolsRepository.GetAllBillOfLaddingsByShipperForLast90DaysAsync(shipperId);
                if (getBols != null)
                {
                    bolCount = getBols.Count;

                    // New method of clearing the ComboBox               
                    DataTable currentDataTable = (DataTable)comboBoxShipperQuoteNumbers.DataSource;
                    if (currentDataTable != null)
                    {
                        currentDataTable.Clear();
                        comboBoxShipperQuoteNumbers.DataSource = currentDataTable;
                    }

                    var dtShipperQuotes = new DataTable("dtShipperQuotes");
                    dtShipperQuotes.Columns.Add(new DataColumn("Key"));
                    dtShipperQuotes.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtShipperQuotes.NewRow();
                    rsDefault[0] = "-- Make a selection --";
                    rsDefault[1] = 0;
                    dtShipperQuotes.Rows.Add(rsDefault);

                    foreach (var getBol in getBols.OrderBy(ob => ob?.ShipperQuoteNumber))
                    {
                        if (getBol != null)
                        {
                            var rsShipperQuote = dtShipperQuotes.NewRow();
                            rsShipperQuote[0] = getBol.ShipperQuoteNumber;
                            rsShipperQuote[1] = getBol.ShipperQuoteNumber;
                            dtShipperQuotes.Rows.Add(rsShipperQuote);
                        }

                    }

                    dtShipperQuotes.AcceptChanges();

                    comboBoxShipperQuoteNumbers.DataSource = dtShipperQuotes;
                    comboBoxShipperQuoteNumbers.DisplayMember = "Key";
                    comboBoxShipperQuoteNumbers.ValueMember = "Value";
                    comboBoxShipperQuoteNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxShipperQuoteNumbers.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxShipperQuoteNumbers.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxShipperQuoteNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxShipperQuoteNumbers.SelectedIndex = 0;
                }

            }

            return bolCount;
        }

        /// <summary>
        /// Special Function to generate a dispute from BOLs
        /// We need to be compatible for all methods of dispute generation, such as automatic and manual disputes
        /// </summary>
        /// <param name="shipperQuoteNumber"></param>
        private void LoadShipperQuoteNumber(string shipperQuoteNumber)
        {
            // Disable this event, but don't enable it, it will be enables at the end of generating the dispute
            comboBoxShipperQuoteNumbers.SelectedIndexChanged -= ComboBoxShipperQuoteNumbers_SelectedIndexChanged;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxShipperQuoteNumbers.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxShipperQuoteNumbers.DataSource = currentDataTable;
            }

            var dtShipperQuotes = new DataTable("dtShipperQuotes");
            dtShipperQuotes.Columns.Add(new DataColumn("Key"));
            dtShipperQuotes.Columns.Add(new DataColumn("Value"));

            var rsQuoteNumber = dtShipperQuotes.NewRow();
            rsQuoteNumber[0] = shipperQuoteNumber;
            rsQuoteNumber[1] = shipperQuoteNumber;
            dtShipperQuotes.Rows.Add(rsQuoteNumber);

            dtShipperQuotes.AcceptChanges();

            comboBoxShipperQuoteNumbers.DataSource = dtShipperQuotes;
            comboBoxShipperQuoteNumbers.DisplayMember = "Key";
            comboBoxShipperQuoteNumbers.ValueMember = "Value";
            comboBoxShipperQuoteNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxShipperQuoteNumbers.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxShipperQuoteNumbers.AutoCompleteMode = AutoCompleteMode.None;
            comboBoxShipperQuoteNumbers.AutoCompleteSource = AutoCompleteSource.ListItems;

            // Set the Index to 0
            comboBoxShipperQuoteNumbers.SelectedIndex = 0;

        }

        private async void ComboBoxShipperContacts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (this.Shipper != null)
            {
                if (this.Shipper.ShipperContacts != null)
                {
                    if (comboBoxShipperContacts.SelectedValue != null)
                    {
                        string contactId = (comboBoxShipperContacts.SelectedValue as dynamic).ToString();
                        if (contactId != null)
                        {
                            if (shipperRepository != null)
                            {
                                if (this.Shipper.ShipperId != null)
                                {
                                    var getShipperContacts = await shipperRepository.GetShipperContactsList(this.Shipper.ShipperId);
                                    if (getShipperContacts != null)
                                    {
                                        var contact = getShipperContacts.Where(s => s.ContactId == contactId).FirstOrDefault();
                                        if (contact != null)
                                        {
                                            if (contact.ContactEmailAddress != null)
                                            {
                                                labelContactEmailAddress.Text = contact.ContactEmailAddress.ToLower();
                                            }
                                            else
                                            {
                                                labelContactEmailAddress.Text = "Contact Record Incomplete";
                                            }

                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {

                    }

                }

            }

        }

        private async void ComboBoxShipperQuoteNumbers_SelectedIndexChanged(object? sender, EventArgs e)
        {

            string bolId = (comboBoxShipperQuoteNumbers.SelectedValue as dynamic).ToString();
            if (bolId != null)
            {
                if (bolsRepository != null)
                {
                    var getBol = await bolsRepository.GetOneBillOfLaddingAsync(bolId);
                    if (getBol != null)
                    {

                        maskedTextBoxQuotedPrice.Text = getBol.ShipperQuotePrice.ToString();
                        maskedTextBoxActualPrice.Text = getBol.ActualShippingPrice.ToString();
                        maskedTextBoxReferenceNumber.Text = getBol.ShipperReferenceNumber;
                        maskedTextBoxOrderNumber.Text = getBol.OrderNumber;

                        dateTimePickerShipDate.Value = getBol.ShipDate;
                        dateTimePickerDeliveryDate.Value = getBol.DeliveryDate;
                        textBoxEstimatedTransitDays.Text = getBol.TransitDaysEstimated.ToString();
                        textBoxActualTransitDays.Text = getBol.TransitDaysActual.ToString();

                        if (getBol.ActualShippingPrice == 0)
                        {
                            DialogResult result = MessageBox.Show("You can't dispute a BOL until the BOL has been completed.\nGo back and finish this BOL first before making a dispute.\n\n Would you like to cancel this dispute?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result == DialogResult.Yes)
                            {
                                Cursor = Cursors.WaitCursor;
                                this.DialogResult = DialogResult.Cancel;
                                this.Close();
                            }
                        }

                    }
                }

            }

        }

        #endregion
        #region TextBoxes

        private void TextBoxCurrencyAmount_Validating(object? sender, CancelEventArgs e)
        {
            TextBox? textBox = sender as TextBox; // Use null-conditional operator here

            // Check if textBox is null before proceeding
            if (textBox == null)
            {
                // Handle the case when the sender is not a TextBox
                // You can log an error or handle it according to your requirement
                return;
            }

            string amountText = textBox.Text;

            // Perform currency amount validation using a regular expression
            string pattern = @"^\d+(\.\d{1,2})?$"; // Matches positive decimal numbers with up to two decimal places
            bool isValid = Regex.IsMatch(amountText, pattern);

            // Set error provider or display an error message if the currency amount is invalid
            if (!isValid)
            {
                errorProvider1.SetError(textBox, "Invalid currency amount format. Please use a valid positive number with up to two decimal places.");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(textBox, ""); // Clear error if valid
            }
        }


        private void MaskedTextBoxCurrencyAmount_Validating(object? sender, CancelEventArgs e)
        {
            MaskedTextBox? maskedTextBox = sender as MaskedTextBox; // Use null-conditional operator here

            // Check if maskedTextBox is null before proceeding
            if (maskedTextBox == null)
            {
                // Handle the case when the sender is not a MaskedTextBox
                // You can log an error or handle it according to your requirement
                return;
            }

            string amountText = maskedTextBox.Text;

            // Perform currency amount validation using a regular expression
            string pattern = @"^\d+(\.\d{1,2})?$"; // Matches positive decimal numbers with up to two decimal places
            bool isValid = Regex.IsMatch(amountText, pattern);

            // Set error provider or display an error message if the currency amount is invalid
            if (!isValid)
            {
                errorProvider1.SetError(maskedTextBox, "Invalid currency amount format. Please use a valid positive number with up to two decimal places.");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(maskedTextBox, ""); // Clear error if valid
            }
        }

        #endregion
        #region EventHandlers

        private void DisableEventHandlers()
        {
            comboBoxShippers.SelectedIndexChanged -= ComboBoxShippers_SelectedIndexChanged;
            comboBoxShipperContacts.SelectedIndexChanged -= ComboBoxShipperContacts_SelectedIndexChanged;
            comboBoxShipperQuoteNumbers.SelectedIndexChanged -= ComboBoxShipperQuoteNumbers_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            comboBoxShipperContacts.SelectedIndexChanged += ComboBoxShipperContacts_SelectedIndexChanged;
            comboBoxShipperQuoteNumbers.SelectedIndexChanged += ComboBoxShipperQuoteNumbers_SelectedIndexChanged;
        }

        #endregion
        #region DisableControls

        private void DisableControls()
        {
            comboBoxShippers.Enabled = false;
            comboBoxShipperQuoteNumbers.Enabled = false;
            maskedTextBoxQuotedPrice.Enabled = false;
            maskedTextBoxActualPrice.Enabled = false;
            maskedTextBoxReferenceNumber.Enabled = false;
            maskedTextBoxOrderNumber.Enabled = false;
        }

        private void EnableControls()
        {
            comboBoxShippers.Enabled = true;
            comboBoxShipperQuoteNumbers.Enabled = true;
            maskedTextBoxQuotedPrice.Enabled = true;
            maskedTextBoxActualPrice.Enabled = true;
            maskedTextBoxReferenceNumber.Enabled = true;
            maskedTextBoxOrderNumber.Enabled = true;
        }

        #endregion
        #region TextBoxControlEvents

        protected void TextBoxControl_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text.Length > 0)
                {
                    _ = BeginInvoke((Action)delegate
                    {
                        textBox.SelectAll();
                    });
                }
            }
        }

        protected void MaskedTextBoxControl_Enter(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            if (maskedTextBox != null)
            {
                if (maskedTextBox.Text.Length > 0)
                {
                    _ = BeginInvoke((Action)delegate
                    {
                        maskedTextBox.SelectAll();
                    });
                }
            }
        }

        protected void TextBoxAdjustedPrice_Leave(object sender, EventArgs e)
        {
            var adjustedPriceResult = decimal.TryParse(textBoxAdjustedPrice.Text, out decimal adjustedPrice);
            var actualPriceResult = decimal.TryParse(maskedTextBoxActualPrice.Text, out decimal actualPrice);

            if (adjustedPriceResult && actualPriceResult)
            {
                decimal creditAmount = actualPrice - adjustedPrice;
                textBoxCreditedAmount.Text = creditAmount.ToString();
                checkBoxResolved.Checked = true;
                textBoxResolution.Text = "A credit of $" + creditAmount.ToString() + " has been issued. ";
            }

        }

        #endregion
        #region Transit Times

        private void DateTimePickerShipDate_ValueChanged(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerShipDate_Leave(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerDeliveryDate_Leave(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void TextBoxEstimatedTransitDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted to represent days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxActualTransitDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted to represent days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion

    }
}
