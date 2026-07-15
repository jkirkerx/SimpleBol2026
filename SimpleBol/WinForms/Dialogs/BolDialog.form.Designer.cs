namespace SimpleBol.WinForms.Dialogs
{
    partial class BolDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            labelUnitOfMeasurement = new Label();
            labelBolTotalWeight = new Label();
            pictureBoxUpdateFlag = new PictureBox();
            labelHeader = new Label();
            PbLogo = new PictureBox();
            labelComments = new Label();
            textBoxComments = new TextBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBoxCustomer = new GroupBox();
            checkBoxShipToPalletJackRequired = new CheckBox();
            panelShipToAppointment = new Panel();
            labelShiptoAppointmentTime = new Label();
            maskedTextBoxShipToAppointmentTime = new MaskedTextBox();
            labelShipToAppointmentDAte = new Label();
            dateTimePickerShipToAppointmentDate = new DateTimePicker();
            checkBoxShipToAppointmentRequired = new CheckBox();
            checkBoxShipToLiftGateRequired = new CheckBox();
            panelShipToLocations = new Panel();
            labelShipToLocation = new Label();
            comboBoxCustomerLocations = new ComboBox();
            panelShipTo = new Panel();
            labelShipToPostalCode = new Label();
            labelShipToCountry = new Label();
            labelShipToRegion = new Label();
            labelShipToCity = new Label();
            labelShipToAddress = new Label();
            labelShipToName = new Label();
            buttonAddShipTo = new Button();
            labelSelectCustomer = new Label();
            comboBoxCustomers = new ComboBox();
            groupBoxShipper = new GroupBox();
            panelShipper = new Panel();
            labelShipperPostalCode = new Label();
            labelShipperCountry = new Label();
            labelShipperRegion = new Label();
            labelShipperCity = new Label();
            labelShipperAddress = new Label();
            labelShipperName = new Label();
            buttonAddShipper = new Button();
            labelShippers = new Label();
            comboBoxShippers = new ComboBox();
            groupBoxBillTo = new GroupBox();
            panelBillTo = new Panel();
            labelBillToPostalCode = new Label();
            labelBillToCountry = new Label();
            labelBillToRegion = new Label();
            labelBillToCity = new Label();
            labelBillToAddress = new Label();
            labelBillToName = new Label();
            buttonAddBilling = new Button();
            labelBillTo = new Label();
            comboBox3rdPartyBillling = new ComboBox();
            groupBoxPallets = new GroupBox();
            buttonPalletsExpandCollapse = new Button();
            buttonEditPallet = new Button();
            buttonRemovePallet = new Button();
            buttonAddPallet = new Button();
            listViewPallets = new ListView();
            groupBoxPackages = new GroupBox();
            buttonPackagesExpandCollapse = new Button();
            buttonEditPackage = new Button();
            buttonRemovePackage = new Button();
            buttonAddPackage = new Button();
            listViewPackages = new ListView();
            groupBoxShipFrom = new GroupBox();
            checkBoxShipFromPalletJackRequired = new CheckBox();
            panelShipFromAppointment = new Panel();
            labelShipFromAppointmentTime = new Label();
            maskedTextBoxShipFromAppointmentTime = new MaskedTextBox();
            labelShipFromAppointmentDate = new Label();
            dateTimePickerShipFromAppointmentDate = new DateTimePicker();
            checkBoxShipFromAppointmentRequired = new CheckBox();
            checkBoxShipFromLiftGateRequired = new CheckBox();
            panelShipFromLocations = new Panel();
            labelShipFromLocations = new Label();
            comboBoxVendorLocations = new ComboBox();
            panelShipFrom = new Panel();
            labelShipFromPostalCode = new Label();
            labelShipFromCountry = new Label();
            labelShipFromRegion = new Label();
            labelShipFromCity = new Label();
            labelShipFromAddress = new Label();
            labelShipFromName = new Label();
            buttonAddShipFrom = new Button();
            labelShipFromVendor = new Label();
            comboBoxVendors = new ComboBox();
            groupBoxPayment = new GroupBox();
            labelEstimatedBolWeight = new Label();
            textBoxEstimatedBolWeight = new TextBox();
            labelEstimatedBolValue = new Label();
            textBoxBolEstimatedValue = new TextBox();
            checkBoxCustomerInvoice = new CheckBox();
            checkBoxFreightPrepaid = new CheckBox();
            maskedTextBoxCodAmount = new MaskedTextBox();
            checkBoxPaymentCOD = new CheckBox();
            groupBoxReferences = new GroupBox();
            textBoxBolNumber = new TextBox();
            labelBolNumber = new Label();
            maskedTextBoxOrderNumber = new MaskedTextBox();
            labelOrderNumber = new Label();
            maskedTextBoxShipperQuoteNumber = new MaskedTextBox();
            labelShipperQuoteNumber = new Label();
            maskedTextBoxReferenceNumber = new MaskedTextBox();
            labelReferenceNumber = new Label();
            maskedTextBoxActualPrice = new MaskedTextBox();
            labelActualPrice = new Label();
            maskedTextBoxQuotedPrice = new MaskedTextBox();
            labelQuotedPrice = new Label();
            groupBoxTransitTimes = new GroupBox();
            textBoxActualTransitDays = new TextBox();
            labelActualTransitTime = new Label();
            textBoxEstimatedTransitDays = new TextBox();
            labelEstimatedTransitDays = new Label();
            labelDeliveryDate = new Label();
            dateTimePickerDeliveryDate = new DateTimePicker();
            labelTransitShipDate = new Label();
            dateTimePickerShipDate = new DateTimePicker();
            labelSpecialInstructions = new Label();
            textBoxSpecialInstructions = new TextBox();
            groupBoxServiceTypes = new GroupBox();
            radioButtonLTL = new RadioButton();
            radioButtonShowAll = new RadioButton();
            radioButtonArmouredCar = new RadioButton();
            radioButtonCourier = new RadioButton();
            radioButtonLastMile = new RadioButton();
            radioButtonRailroad = new RadioButton();
            radioButtonOcean = new RadioButton();
            radioButtonAir = new RadioButton();
            radioButtonFTL = new RadioButton();
            groupBoxContainers = new GroupBox();
            buttonContainersExpandCollapse = new Button();
            buttonEditContainer = new Button();
            buttonRemoveContainer = new Button();
            buttonAddContainer = new Button();
            listViewContainers = new ListView();
            groupBoxFilters = new GroupBox();
            buttonReloadDestinationCountryShippers = new Button();
            comboBoxDestinationCountries = new ComboBox();
            labelChangePending = new Label();
            errorProvider1 = new ErrorProvider(components);
            buttonPrint = new Button();
            buttonEmail = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxCustomer.SuspendLayout();
            panelShipToAppointment.SuspendLayout();
            panelShipToLocations.SuspendLayout();
            panelShipTo.SuspendLayout();
            groupBoxShipper.SuspendLayout();
            panelShipper.SuspendLayout();
            groupBoxBillTo.SuspendLayout();
            panelBillTo.SuspendLayout();
            groupBoxPallets.SuspendLayout();
            groupBoxPackages.SuspendLayout();
            groupBoxShipFrom.SuspendLayout();
            panelShipFromAppointment.SuspendLayout();
            panelShipFromLocations.SuspendLayout();
            panelShipFrom.SuspendLayout();
            groupBoxPayment.SuspendLayout();
            groupBoxReferences.SuspendLayout();
            groupBoxTransitTimes.SuspendLayout();
            groupBoxServiceTypes.SuspendLayout();
            groupBoxContainers.SuspendLayout();
            groupBoxFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(labelUnitOfMeasurement);
            panel1.Controls.Add(labelBolTotalWeight);
            panel1.Controls.Add(pictureBoxUpdateFlag);
            panel1.Controls.Add(labelHeader);
            panel1.Controls.Add(PbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1634, 70);
            panel1.TabIndex = 3;
            // 
            // labelUnitOfMeasurement
            // 
            labelUnitOfMeasurement.AutoSize = true;
            labelUnitOfMeasurement.Font = new Font("Segoe UI", 22F);
            labelUnitOfMeasurement.Location = new Point(1460, 14);
            labelUnitOfMeasurement.Name = "labelUnitOfMeasurement";
            labelUnitOfMeasurement.Size = new Size(65, 41);
            labelUnitOfMeasurement.TabIndex = 5;
            labelUnitOfMeasurement.Text = "LBS";
            // 
            // labelBolTotalWeight
            // 
            labelBolTotalWeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBolTotalWeight.Font = new Font("Segoe UI", 22F);
            labelBolTotalWeight.Location = new Point(1145, 14);
            labelBolTotalWeight.Name = "labelBolTotalWeight";
            labelBolTotalWeight.RightToLeft = RightToLeft.Yes;
            labelBolTotalWeight.Size = new Size(318, 41);
            labelBolTotalWeight.TabIndex = 4;
            labelBolTotalWeight.Text = "0";
            labelBolTotalWeight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBoxUpdateFlag
            // 
            pictureBoxUpdateFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.InitialImage = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.Location = new Point(1560, 14);
            pictureBoxUpdateFlag.Name = "pictureBoxUpdateFlag";
            pictureBoxUpdateFlag.Size = new Size(45, 45);
            pictureBoxUpdateFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUpdateFlag.TabIndex = 3;
            pictureBoxUpdateFlag.TabStop = false;
            // 
            // labelHeader
            // 
            labelHeader.AutoSize = true;
            labelHeader.Font = new Font("Segoe UI", 24F);
            labelHeader.ForeColor = Color.White;
            labelHeader.Location = new Point(120, 14);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new Size(302, 45);
            labelHeader.TabIndex = 1;
            labelHeader.Text = "Bill Of Lading Editor";
            // 
            // PbLogo
            // 
            PbLogo.Image = Properties.Resources.btCreateBol;
            PbLogo.InitialImage = Properties.Resources.btCreateBol;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 4, 3, 4);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F);
            labelComments.ForeColor = Color.White;
            labelComments.Location = new Point(32, 952);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(80, 20);
            labelComments.TabIndex = 32;
            labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxComments.Location = new Point(27, 982);
            textBoxComments.Margin = new Padding(3, 4, 3, 4);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(333, 100);
            textBoxComments.TabIndex = 74;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.Cursor = Cursors.Hand;
            // SaveBol is asynchronous. The click handler sets DialogResult only after
            // the database operation completes successfully.
            OK_Button.DialogResult = DialogResult.None;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(1336, 1031);
            OK_Button.Margin = new Padding(3, 4, 3, 4);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(131, 51);
            OK_Button.TabIndex = 76;
            OK_Button.Text = "Save";
            OK_Button.UseVisualStyleBackColor = false;
            OK_Button.Click += OK_Button_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel_Button.BackColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.CausesValidation = false;
            Cancel_Button.Cursor = Cursors.Hand;
            Cancel_Button.DialogResult = DialogResult.Cancel;
            Cancel_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            Cancel_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Segoe UI", 12F);
            Cancel_Button.ForeColor = Color.White;
            Cancel_Button.Location = new Point(1474, 1031);
            Cancel_Button.Margin = new Padding(3, 4, 3, 4);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(131, 51);
            Cancel_Button.TabIndex = 77;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // groupBoxCustomer
            // 
            groupBoxCustomer.Controls.Add(checkBoxShipToPalletJackRequired);
            groupBoxCustomer.Controls.Add(panelShipToAppointment);
            groupBoxCustomer.Controls.Add(checkBoxShipToAppointmentRequired);
            groupBoxCustomer.Controls.Add(checkBoxShipToLiftGateRequired);
            groupBoxCustomer.Controls.Add(panelShipToLocations);
            groupBoxCustomer.Controls.Add(panelShipTo);
            groupBoxCustomer.Controls.Add(buttonAddShipTo);
            groupBoxCustomer.Controls.Add(labelSelectCustomer);
            groupBoxCustomer.Controls.Add(comboBoxCustomers);
            groupBoxCustomer.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxCustomer.ForeColor = Color.LightBlue;
            groupBoxCustomer.Location = new Point(382, 418);
            groupBoxCustomer.Name = "groupBoxCustomer";
            groupBoxCustomer.Size = new Size(333, 513);
            groupBoxCustomer.TabIndex = 29;
            groupBoxCustomer.TabStop = false;
            groupBoxCustomer.Text = "Ship To:";
            // 
            // checkBoxShipToPalletJackRequired
            // 
            checkBoxShipToPalletJackRequired.AutoSize = true;
            checkBoxShipToPalletJackRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipToPalletJackRequired.ForeColor = Color.White;
            checkBoxShipToPalletJackRequired.Location = new Point(201, 306);
            checkBoxShipToPalletJackRequired.Name = "checkBoxShipToPalletJackRequired";
            checkBoxShipToPalletJackRequired.Size = new Size(95, 24);
            checkBoxShipToPalletJackRequired.TabIndex = 34;
            checkBoxShipToPalletJackRequired.Text = "Pallet Jack";
            checkBoxShipToPalletJackRequired.UseVisualStyleBackColor = true;
            // 
            // panelShipToAppointment
            // 
            panelShipToAppointment.Controls.Add(labelShiptoAppointmentTime);
            panelShipToAppointment.Controls.Add(maskedTextBoxShipToAppointmentTime);
            panelShipToAppointment.Controls.Add(labelShipToAppointmentDAte);
            panelShipToAppointment.Controls.Add(dateTimePickerShipToAppointmentDate);
            panelShipToAppointment.Location = new Point(6, 367);
            panelShipToAppointment.Name = "panelShipToAppointment";
            panelShipToAppointment.Size = new Size(321, 138);
            panelShipToAppointment.TabIndex = 37;
            // 
            // labelShiptoAppointmentTime
            // 
            labelShiptoAppointmentTime.AutoSize = true;
            labelShiptoAppointmentTime.Font = new Font("Segoe UI", 11F);
            labelShiptoAppointmentTime.ForeColor = Color.White;
            labelShiptoAppointmentTime.Location = new Point(12, 75);
            labelShiptoAppointmentTime.Name = "labelShiptoAppointmentTime";
            labelShiptoAppointmentTime.Size = new Size(187, 20);
            labelShiptoAppointmentTime.TabIndex = 11;
            labelShiptoAppointmentTime.Text = "Ship To Appointment Time";
            // 
            // maskedTextBoxShipToAppointmentTime
            // 
            maskedTextBoxShipToAppointmentTime.Location = new Point(12, 103);
            maskedTextBoxShipToAppointmentTime.Mask = "90:00";
            maskedTextBoxShipToAppointmentTime.Name = "maskedTextBoxShipToAppointmentTime";
            maskedTextBoxShipToAppointmentTime.Size = new Size(290, 27);
            maskedTextBoxShipToAppointmentTime.TabIndex = 37;
            maskedTextBoxShipToAppointmentTime.ValidatingType = typeof(DateTime);
            maskedTextBoxShipToAppointmentTime.Enter += MaskedTextBoxShipToAppointmentTime_Enter;
            // 
            // labelShipToAppointmentDAte
            // 
            labelShipToAppointmentDAte.AutoSize = true;
            labelShipToAppointmentDAte.Font = new Font("Segoe UI", 11F);
            labelShipToAppointmentDAte.ForeColor = Color.White;
            labelShipToAppointmentDAte.Location = new Point(15, 8);
            labelShipToAppointmentDAte.Name = "labelShipToAppointmentDAte";
            labelShipToAppointmentDAte.Size = new Size(186, 20);
            labelShipToAppointmentDAte.TabIndex = 9;
            labelShipToAppointmentDAte.Text = "Ship To Appointment Date";
            // 
            // dateTimePickerShipToAppointmentDate
            // 
            dateTimePickerShipToAppointmentDate.Location = new Point(12, 37);
            dateTimePickerShipToAppointmentDate.Name = "dateTimePickerShipToAppointmentDate";
            dateTimePickerShipToAppointmentDate.Size = new Size(290, 27);
            dateTimePickerShipToAppointmentDate.TabIndex = 36;
            // 
            // checkBoxShipToAppointmentRequired
            // 
            checkBoxShipToAppointmentRequired.AutoSize = true;
            checkBoxShipToAppointmentRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipToAppointmentRequired.ForeColor = Color.White;
            checkBoxShipToAppointmentRequired.Location = new Point(22, 337);
            checkBoxShipToAppointmentRequired.Name = "checkBoxShipToAppointmentRequired";
            checkBoxShipToAppointmentRequired.Size = new Size(180, 24);
            checkBoxShipToAppointmentRequired.TabIndex = 35;
            checkBoxShipToAppointmentRequired.Text = "Appointment Required";
            checkBoxShipToAppointmentRequired.UseVisualStyleBackColor = true;
            checkBoxShipToAppointmentRequired.CheckedChanged += CheckBoxCustomerAppointment_CheckedChanged;
            // 
            // checkBoxShipToLiftGateRequired
            // 
            checkBoxShipToLiftGateRequired.AutoSize = true;
            checkBoxShipToLiftGateRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipToLiftGateRequired.ForeColor = Color.White;
            checkBoxShipToLiftGateRequired.Location = new Point(22, 306);
            checkBoxShipToLiftGateRequired.Name = "checkBoxShipToLiftGateRequired";
            checkBoxShipToLiftGateRequired.Size = new Size(148, 24);
            checkBoxShipToLiftGateRequired.TabIndex = 33;
            checkBoxShipToLiftGateRequired.Text = "Lift Gate Required";
            checkBoxShipToLiftGateRequired.UseVisualStyleBackColor = true;
            // 
            // panelShipToLocations
            // 
            panelShipToLocations.Controls.Add(labelShipToLocation);
            panelShipToLocations.Controls.Add(comboBoxCustomerLocations);
            panelShipToLocations.Location = new Point(19, 100);
            panelShipToLocations.Name = "panelShipToLocations";
            panelShipToLocations.Size = new Size(292, 72);
            panelShipToLocations.TabIndex = 34;
            // 
            // labelShipToLocation
            // 
            labelShipToLocation.AutoSize = true;
            labelShipToLocation.Font = new Font("Segoe UI", 11F);
            labelShipToLocation.ForeColor = Color.White;
            labelShipToLocation.Location = new Point(3, 4);
            labelShipToLocation.Name = "labelShipToLocation";
            labelShipToLocation.Size = new Size(119, 20);
            labelShipToLocation.TabIndex = 1;
            labelShipToLocation.Text = "Select a location";
            // 
            // comboBoxCustomerLocations
            // 
            comboBoxCustomerLocations.FormattingEnabled = true;
            comboBoxCustomerLocations.Location = new Point(3, 37);
            comboBoxCustomerLocations.Name = "comboBoxCustomerLocations";
            comboBoxCustomerLocations.Size = new Size(289, 28);
            comboBoxCustomerLocations.TabIndex = 32;
            comboBoxCustomerLocations.SelectedIndexChanged += ComboBoxCustomerLocations_SelectedIndexChanged;
            // 
            // panelShipTo
            // 
            panelShipTo.Controls.Add(labelShipToPostalCode);
            panelShipTo.Controls.Add(labelShipToCountry);
            panelShipTo.Controls.Add(labelShipToRegion);
            panelShipTo.Controls.Add(labelShipToCity);
            panelShipTo.Controls.Add(labelShipToAddress);
            panelShipTo.Controls.Add(labelShipToName);
            panelShipTo.ForeColor = Color.White;
            panelShipTo.Location = new Point(21, 178);
            panelShipTo.Name = "panelShipTo";
            panelShipTo.Size = new Size(290, 116);
            panelShipTo.TabIndex = 31;
            // 
            // labelShipToPostalCode
            // 
            labelShipToPostalCode.AutoSize = true;
            labelShipToPostalCode.Font = new Font("Segoe UI", 11F);
            labelShipToPostalCode.Location = new Point(17, 89);
            labelShipToPostalCode.Name = "labelShipToPostalCode";
            labelShipToPostalCode.Size = new Size(48, 20);
            labelShipToPostalCode.TabIndex = 19;
            labelShipToPostalCode.Text = "Postal";
            // 
            // labelShipToCountry
            // 
            labelShipToCountry.AutoSize = true;
            labelShipToCountry.Font = new Font("Segoe UI", 11F);
            labelShipToCountry.Location = new Point(184, 89);
            labelShipToCountry.Name = "labelShipToCountry";
            labelShipToCountry.Size = new Size(95, 20);
            labelShipToCountry.TabIndex = 18;
            labelShipToCountry.Text = "CountryCode";
            // 
            // labelShipToRegion
            // 
            labelShipToRegion.AutoSize = true;
            labelShipToRegion.Font = new Font("Segoe UI", 11F);
            labelShipToRegion.Location = new Point(184, 59);
            labelShipToRegion.Name = "labelShipToRegion";
            labelShipToRegion.Size = new Size(91, 20);
            labelShipToRegion.TabIndex = 17;
            labelShipToRegion.Text = "RegionCode";
            // 
            // labelShipToCity
            // 
            labelShipToCity.AutoSize = true;
            labelShipToCity.Font = new Font("Segoe UI", 11F);
            labelShipToCity.Location = new Point(17, 59);
            labelShipToCity.Name = "labelShipToCity";
            labelShipToCity.Size = new Size(34, 20);
            labelShipToCity.TabIndex = 16;
            labelShipToCity.Text = "City";
            // 
            // labelShipToAddress
            // 
            labelShipToAddress.AutoSize = true;
            labelShipToAddress.Font = new Font("Segoe UI", 11F);
            labelShipToAddress.Location = new Point(17, 33);
            labelShipToAddress.Name = "labelShipToAddress";
            labelShipToAddress.Size = new Size(62, 20);
            labelShipToAddress.TabIndex = 15;
            labelShipToAddress.Text = "Address";
            // 
            // labelShipToName
            // 
            labelShipToName.AutoSize = true;
            labelShipToName.Font = new Font("Segoe UI", 11F);
            labelShipToName.Location = new Point(17, 7);
            labelShipToName.Name = "labelShipToName";
            labelShipToName.Size = new Size(49, 20);
            labelShipToName.TabIndex = 14;
            labelShipToName.Text = "Name";
            // 
            // buttonAddShipTo
            // 
            buttonAddShipTo.BackColor = Color.SteelBlue;
            buttonAddShipTo.Cursor = Cursors.Hand;
            buttonAddShipTo.FlatAppearance.BorderSize = 0;
            buttonAddShipTo.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonAddShipTo.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAddShipTo.FlatStyle = FlatStyle.Flat;
            buttonAddShipTo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonAddShipTo.Location = new Point(282, 24);
            buttonAddShipTo.Name = "buttonAddShipTo";
            buttonAddShipTo.Size = new Size(29, 28);
            buttonAddShipTo.TabIndex = 30;
            buttonAddShipTo.Text = "+";
            buttonAddShipTo.UseVisualStyleBackColor = false;
            buttonAddShipTo.Click += ButtonAddShipTo_Click;
            // 
            // labelSelectCustomer
            // 
            labelSelectCustomer.AutoSize = true;
            labelSelectCustomer.Font = new Font("Segoe UI", 11F);
            labelSelectCustomer.ForeColor = Color.White;
            labelSelectCustomer.Location = new Point(22, 32);
            labelSelectCustomer.Name = "labelSelectCustomer";
            labelSelectCustomer.Size = new Size(116, 20);
            labelSelectCustomer.TabIndex = 1;
            labelSelectCustomer.Text = "Select Customer";
            // 
            // comboBoxCustomers
            // 
            comboBoxCustomers.FormattingEnabled = true;
            comboBoxCustomers.Location = new Point(21, 61);
            comboBoxCustomers.Name = "comboBoxCustomers";
            comboBoxCustomers.Size = new Size(290, 28);
            comboBoxCustomers.TabIndex = 31;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;
            // 
            // groupBoxShipper
            // 
            groupBoxShipper.Controls.Add(panelShipper);
            groupBoxShipper.Controls.Add(buttonAddShipper);
            groupBoxShipper.Controls.Add(labelShippers);
            groupBoxShipper.Controls.Add(comboBoxShippers);
            groupBoxShipper.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxShipper.ForeColor = Color.AntiqueWhite;
            groupBoxShipper.Location = new Point(27, 173);
            groupBoxShipper.Name = "groupBoxShipper";
            groupBoxShipper.Size = new Size(333, 226);
            groupBoxShipper.TabIndex = 13;
            groupBoxShipper.TabStop = false;
            groupBoxShipper.Text = "Shippers by service type";
            // 
            // panelShipper
            // 
            panelShipper.Controls.Add(labelShipperPostalCode);
            panelShipper.Controls.Add(labelShipperCountry);
            panelShipper.Controls.Add(labelShipperRegion);
            panelShipper.Controls.Add(labelShipperCity);
            panelShipper.Controls.Add(labelShipperAddress);
            panelShipper.Controls.Add(labelShipperName);
            panelShipper.ForeColor = Color.White;
            panelShipper.Location = new Point(19, 99);
            panelShipper.Name = "panelShipper";
            panelShipper.Size = new Size(292, 121);
            panelShipper.TabIndex = 30;
            // 
            // labelShipperPostalCode
            // 
            labelShipperPostalCode.AutoSize = true;
            labelShipperPostalCode.Font = new Font("Segoe UI", 11F);
            labelShipperPostalCode.Location = new Point(18, 91);
            labelShipperPostalCode.Name = "labelShipperPostalCode";
            labelShipperPostalCode.Size = new Size(48, 20);
            labelShipperPostalCode.TabIndex = 13;
            labelShipperPostalCode.Text = "Postal";
            // 
            // labelShipperCountry
            // 
            labelShipperCountry.AutoSize = true;
            labelShipperCountry.Font = new Font("Segoe UI", 11F);
            labelShipperCountry.Location = new Point(185, 91);
            labelShipperCountry.Name = "labelShipperCountry";
            labelShipperCountry.Size = new Size(95, 20);
            labelShipperCountry.TabIndex = 12;
            labelShipperCountry.Text = "CountryCode";
            // 
            // labelShipperRegion
            // 
            labelShipperRegion.AutoSize = true;
            labelShipperRegion.Font = new Font("Segoe UI", 11F);
            labelShipperRegion.Location = new Point(185, 61);
            labelShipperRegion.Name = "labelShipperRegion";
            labelShipperRegion.Size = new Size(91, 20);
            labelShipperRegion.TabIndex = 11;
            labelShipperRegion.Text = "RegionCode";
            // 
            // labelShipperCity
            // 
            labelShipperCity.AutoSize = true;
            labelShipperCity.Font = new Font("Segoe UI", 11F);
            labelShipperCity.Location = new Point(18, 61);
            labelShipperCity.Name = "labelShipperCity";
            labelShipperCity.Size = new Size(34, 20);
            labelShipperCity.TabIndex = 10;
            labelShipperCity.Text = "City";
            // 
            // labelShipperAddress
            // 
            labelShipperAddress.AutoSize = true;
            labelShipperAddress.Font = new Font("Segoe UI", 11F);
            labelShipperAddress.Location = new Point(18, 35);
            labelShipperAddress.Name = "labelShipperAddress";
            labelShipperAddress.Size = new Size(62, 20);
            labelShipperAddress.TabIndex = 9;
            labelShipperAddress.Text = "Address";
            // 
            // labelShipperName
            // 
            labelShipperName.AutoSize = true;
            labelShipperName.Font = new Font("Segoe UI", 11F);
            labelShipperName.Location = new Point(18, 9);
            labelShipperName.Name = "labelShipperName";
            labelShipperName.Size = new Size(49, 20);
            labelShipperName.TabIndex = 8;
            labelShipperName.Text = "Name";
            // 
            // buttonAddShipper
            // 
            buttonAddShipper.BackColor = Color.SaddleBrown;
            buttonAddShipper.Cursor = Cursors.Hand;
            buttonAddShipper.FlatAppearance.BorderSize = 0;
            buttonAddShipper.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonAddShipper.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAddShipper.FlatStyle = FlatStyle.Flat;
            buttonAddShipper.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonAddShipper.Location = new Point(282, 24);
            buttonAddShipper.Name = "buttonAddShipper";
            buttonAddShipper.Size = new Size(29, 28);
            buttonAddShipper.TabIndex = 14;
            buttonAddShipper.Text = "+";
            buttonAddShipper.UseVisualStyleBackColor = false;
            buttonAddShipper.Click += ButtonAddShipper_Click;
            // 
            // labelShippers
            // 
            labelShippers.AutoSize = true;
            labelShippers.Font = new Font("Segoe UI", 11F);
            labelShippers.ForeColor = Color.White;
            labelShippers.Location = new Point(22, 32);
            labelShippers.Name = "labelShippers";
            labelShippers.Size = new Size(104, 20);
            labelShippers.TabIndex = 1;
            labelShippers.Text = "Select Shipper";
            // 
            // comboBoxShippers
            // 
            comboBoxShippers.FormattingEnabled = true;
            comboBoxShippers.Location = new Point(21, 61);
            comboBoxShippers.Name = "comboBoxShippers";
            comboBoxShippers.Size = new Size(290, 28);
            comboBoxShippers.TabIndex = 15;
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            // 
            // groupBoxBillTo
            // 
            groupBoxBillTo.Controls.Add(panelBillTo);
            groupBoxBillTo.Controls.Add(buttonAddBilling);
            groupBoxBillTo.Controls.Add(labelBillTo);
            groupBoxBillTo.Controls.Add(comboBox3rdPartyBillling);
            groupBoxBillTo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxBillTo.ForeColor = Color.AntiqueWhite;
            groupBoxBillTo.Location = new Point(382, 173);
            groupBoxBillTo.Name = "groupBoxBillTo";
            groupBoxBillTo.Size = new Size(333, 226);
            groupBoxBillTo.TabIndex = 16;
            groupBoxBillTo.TabStop = false;
            groupBoxBillTo.Text = "Bill To:";
            // 
            // panelBillTo
            // 
            panelBillTo.Controls.Add(labelBillToPostalCode);
            panelBillTo.Controls.Add(labelBillToCountry);
            panelBillTo.Controls.Add(labelBillToRegion);
            panelBillTo.Controls.Add(labelBillToCity);
            panelBillTo.Controls.Add(labelBillToAddress);
            panelBillTo.Controls.Add(labelBillToName);
            panelBillTo.ForeColor = Color.White;
            panelBillTo.Location = new Point(21, 98);
            panelBillTo.Name = "panelBillTo";
            panelBillTo.Size = new Size(290, 122);
            panelBillTo.TabIndex = 31;
            // 
            // labelBillToPostalCode
            // 
            labelBillToPostalCode.AutoSize = true;
            labelBillToPostalCode.Font = new Font("Segoe UI", 11F);
            labelBillToPostalCode.Location = new Point(17, 92);
            labelBillToPostalCode.Name = "labelBillToPostalCode";
            labelBillToPostalCode.Size = new Size(48, 20);
            labelBillToPostalCode.TabIndex = 19;
            labelBillToPostalCode.Text = "Postal";
            // 
            // labelBillToCountry
            // 
            labelBillToCountry.AutoSize = true;
            labelBillToCountry.Font = new Font("Segoe UI", 11F);
            labelBillToCountry.Location = new Point(184, 92);
            labelBillToCountry.Name = "labelBillToCountry";
            labelBillToCountry.Size = new Size(95, 20);
            labelBillToCountry.TabIndex = 18;
            labelBillToCountry.Text = "CountryCode";
            // 
            // labelBillToRegion
            // 
            labelBillToRegion.AutoSize = true;
            labelBillToRegion.Font = new Font("Segoe UI", 11F);
            labelBillToRegion.Location = new Point(184, 62);
            labelBillToRegion.Name = "labelBillToRegion";
            labelBillToRegion.Size = new Size(91, 20);
            labelBillToRegion.TabIndex = 17;
            labelBillToRegion.Text = "RegionCode";
            // 
            // labelBillToCity
            // 
            labelBillToCity.AutoSize = true;
            labelBillToCity.Font = new Font("Segoe UI", 11F);
            labelBillToCity.Location = new Point(17, 62);
            labelBillToCity.Name = "labelBillToCity";
            labelBillToCity.Size = new Size(34, 20);
            labelBillToCity.TabIndex = 16;
            labelBillToCity.Text = "City";
            // 
            // labelBillToAddress
            // 
            labelBillToAddress.AutoSize = true;
            labelBillToAddress.Font = new Font("Segoe UI", 11F);
            labelBillToAddress.Location = new Point(17, 36);
            labelBillToAddress.Name = "labelBillToAddress";
            labelBillToAddress.Size = new Size(62, 20);
            labelBillToAddress.TabIndex = 15;
            labelBillToAddress.Text = "Address";
            // 
            // labelBillToName
            // 
            labelBillToName.AutoSize = true;
            labelBillToName.Font = new Font("Segoe UI", 11F);
            labelBillToName.Location = new Point(17, 10);
            labelBillToName.Name = "labelBillToName";
            labelBillToName.Size = new Size(49, 20);
            labelBillToName.TabIndex = 14;
            labelBillToName.Text = "Name";
            // 
            // buttonAddBilling
            // 
            buttonAddBilling.BackColor = Color.SaddleBrown;
            buttonAddBilling.Cursor = Cursors.Hand;
            buttonAddBilling.FlatAppearance.BorderSize = 0;
            buttonAddBilling.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonAddBilling.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAddBilling.FlatStyle = FlatStyle.Flat;
            buttonAddBilling.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonAddBilling.Location = new Point(282, 24);
            buttonAddBilling.Name = "buttonAddBilling";
            buttonAddBilling.Size = new Size(29, 28);
            buttonAddBilling.TabIndex = 17;
            buttonAddBilling.Text = "+";
            buttonAddBilling.UseVisualStyleBackColor = false;
            buttonAddBilling.Click += ButtonAddBilling_Click;
            // 
            // labelBillTo
            // 
            labelBillTo.AutoSize = true;
            labelBillTo.Font = new Font("Segoe UI", 11F);
            labelBillTo.ForeColor = Color.White;
            labelBillTo.Location = new Point(22, 32);
            labelBillTo.Name = "labelBillTo";
            labelBillTo.Size = new Size(157, 20);
            labelBillTo.TabIndex = 1;
            labelBillTo.Text = "Select 3rd Party Billing";
            // 
            // comboBox3rdPartyBillling
            // 
            comboBox3rdPartyBillling.FormattingEnabled = true;
            comboBox3rdPartyBillling.Location = new Point(21, 61);
            comboBox3rdPartyBillling.Name = "comboBox3rdPartyBillling";
            comboBox3rdPartyBillling.Size = new Size(290, 28);
            comboBox3rdPartyBillling.TabIndex = 18;
            comboBox3rdPartyBillling.SelectedIndexChanged += ComboBox3rdPartyBillling_SelectedIndexChanged;
            // 
            // groupBoxPallets
            // 
            groupBoxPallets.Controls.Add(buttonPalletsExpandCollapse);
            groupBoxPallets.Controls.Add(buttonEditPallet);
            groupBoxPallets.Controls.Add(buttonRemovePallet);
            groupBoxPallets.Controls.Add(buttonAddPallet);
            groupBoxPallets.Controls.Add(listViewPallets);
            groupBoxPallets.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPallets.ForeColor = Color.DarkKhaki;
            groupBoxPallets.Location = new Point(739, 418);
            groupBoxPallets.Name = "groupBoxPallets";
            groupBoxPallets.Size = new Size(460, 231);
            groupBoxPallets.TabIndex = 44;
            groupBoxPallets.TabStop = false;
            groupBoxPallets.Text = "Pallets";
            // 
            // buttonPalletsExpandCollapse
            // 
            buttonPalletsExpandCollapse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonPalletsExpandCollapse.BackColor = Color.FromArgb(60, 60, 60);
            buttonPalletsExpandCollapse.Cursor = Cursors.Hand;
            buttonPalletsExpandCollapse.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonPalletsExpandCollapse.FlatAppearance.BorderSize = 0;
            buttonPalletsExpandCollapse.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonPalletsExpandCollapse.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonPalletsExpandCollapse.FlatStyle = FlatStyle.Flat;
            buttonPalletsExpandCollapse.ForeColor = Color.Transparent;
            buttonPalletsExpandCollapse.Image = Properties.Resources.Expand28;
            buttonPalletsExpandCollapse.Location = new Point(415, 31);
            buttonPalletsExpandCollapse.Name = "buttonPalletsExpandCollapse";
            buttonPalletsExpandCollapse.Size = new Size(28, 28);
            buttonPalletsExpandCollapse.TabIndex = 48;
            buttonPalletsExpandCollapse.UseVisualStyleBackColor = false;
            buttonPalletsExpandCollapse.Click += ButtonPalletsExpandCollapse_Click;
            // 
            // buttonEditPallet
            // 
            buttonEditPallet.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditPallet.Cursor = Cursors.Hand;
            buttonEditPallet.FlatAppearance.BorderSize = 0;
            buttonEditPallet.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditPallet.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditPallet.FlatStyle = FlatStyle.Flat;
            buttonEditPallet.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonEditPallet.ForeColor = Color.White;
            buttonEditPallet.Location = new Point(100, 32);
            buttonEditPallet.Name = "buttonEditPallet";
            buttonEditPallet.Size = new Size(70, 28);
            buttonEditPallet.TabIndex = 46;
            buttonEditPallet.Text = "Edit";
            buttonEditPallet.UseVisualStyleBackColor = false;
            buttonEditPallet.Click += ButtonEditPallet_Click;
            // 
            // buttonRemovePallet
            // 
            buttonRemovePallet.BackColor = Color.FromArgb(60, 60, 60);
            buttonRemovePallet.Cursor = Cursors.Hand;
            buttonRemovePallet.FlatAppearance.BorderSize = 0;
            buttonRemovePallet.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonRemovePallet.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonRemovePallet.FlatStyle = FlatStyle.Flat;
            buttonRemovePallet.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonRemovePallet.ForeColor = Color.White;
            buttonRemovePallet.Location = new Point(183, 32);
            buttonRemovePallet.Name = "buttonRemovePallet";
            buttonRemovePallet.Size = new Size(70, 28);
            buttonRemovePallet.TabIndex = 47;
            buttonRemovePallet.Text = "Remove";
            buttonRemovePallet.UseVisualStyleBackColor = false;
            buttonRemovePallet.Click += ButtonRemovePallet_Click;
            // 
            // buttonAddPallet
            // 
            buttonAddPallet.BackColor = Color.FromArgb(60, 60, 60);
            buttonAddPallet.Cursor = Cursors.Hand;
            buttonAddPallet.FlatAppearance.BorderSize = 0;
            buttonAddPallet.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAddPallet.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAddPallet.FlatStyle = FlatStyle.Flat;
            buttonAddPallet.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonAddPallet.ForeColor = Color.White;
            buttonAddPallet.Location = new Point(17, 32);
            buttonAddPallet.Name = "buttonAddPallet";
            buttonAddPallet.Size = new Size(70, 28);
            buttonAddPallet.TabIndex = 45;
            buttonAddPallet.Text = "Add";
            buttonAddPallet.UseVisualStyleBackColor = false;
            buttonAddPallet.Click += ButtonAddPallet_Click;
            // 
            // listViewPallets
            // 
            listViewPallets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewPallets.Location = new Point(17, 66);
            listViewPallets.Name = "listViewPallets";
            listViewPallets.Size = new Size(428, 152);
            listViewPallets.TabIndex = 49;
            listViewPallets.UseCompatibleStateImageBehavior = false;
            listViewPallets.ColumnClick += ListViewPalletsColumn_Click;
            listViewPallets.SelectedIndexChanged += ListViewPallets_SelectedIndexChanged;
            listViewPallets.DoubleClick += ListViewPallets_DoubleClick;
            // 
            // groupBoxPackages
            // 
            groupBoxPackages.Controls.Add(buttonPackagesExpandCollapse);
            groupBoxPackages.Controls.Add(buttonEditPackage);
            groupBoxPackages.Controls.Add(buttonRemovePackage);
            groupBoxPackages.Controls.Add(buttonAddPackage);
            groupBoxPackages.Controls.Add(listViewPackages);
            groupBoxPackages.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPackages.ForeColor = Color.DarkKhaki;
            groupBoxPackages.Location = new Point(739, 672);
            groupBoxPackages.Name = "groupBoxPackages";
            groupBoxPackages.Size = new Size(460, 259);
            groupBoxPackages.TabIndex = 50;
            groupBoxPackages.TabStop = false;
            groupBoxPackages.Text = "Packages";
            // 
            // buttonPackagesExpandCollapse
            // 
            buttonPackagesExpandCollapse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonPackagesExpandCollapse.BackColor = Color.FromArgb(60, 60, 60);
            buttonPackagesExpandCollapse.Cursor = Cursors.Hand;
            buttonPackagesExpandCollapse.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonPackagesExpandCollapse.FlatAppearance.BorderSize = 0;
            buttonPackagesExpandCollapse.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonPackagesExpandCollapse.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonPackagesExpandCollapse.FlatStyle = FlatStyle.Flat;
            buttonPackagesExpandCollapse.ForeColor = Color.Transparent;
            buttonPackagesExpandCollapse.Image = Properties.Resources.Expand28;
            buttonPackagesExpandCollapse.Location = new Point(415, 32);
            buttonPackagesExpandCollapse.Name = "buttonPackagesExpandCollapse";
            buttonPackagesExpandCollapse.Size = new Size(28, 28);
            buttonPackagesExpandCollapse.TabIndex = 54;
            buttonPackagesExpandCollapse.UseVisualStyleBackColor = false;
            buttonPackagesExpandCollapse.Click += ButtonPackagesExpandCollapse_Click;
            // 
            // buttonEditPackage
            // 
            buttonEditPackage.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditPackage.Cursor = Cursors.Hand;
            buttonEditPackage.FlatAppearance.BorderSize = 0;
            buttonEditPackage.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditPackage.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditPackage.FlatStyle = FlatStyle.Flat;
            buttonEditPackage.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonEditPackage.ForeColor = Color.White;
            buttonEditPackage.Location = new Point(99, 32);
            buttonEditPackage.Name = "buttonEditPackage";
            buttonEditPackage.Size = new Size(70, 28);
            buttonEditPackage.TabIndex = 52;
            buttonEditPackage.Text = "Edit";
            buttonEditPackage.UseVisualStyleBackColor = false;
            buttonEditPackage.Click += ButtonEditPackage_Click;
            // 
            // buttonRemovePackage
            // 
            buttonRemovePackage.BackColor = Color.FromArgb(60, 60, 60);
            buttonRemovePackage.Cursor = Cursors.Hand;
            buttonRemovePackage.FlatAppearance.BorderSize = 0;
            buttonRemovePackage.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonRemovePackage.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonRemovePackage.FlatStyle = FlatStyle.Flat;
            buttonRemovePackage.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonRemovePackage.ForeColor = Color.White;
            buttonRemovePackage.Location = new Point(182, 32);
            buttonRemovePackage.Name = "buttonRemovePackage";
            buttonRemovePackage.Size = new Size(70, 28);
            buttonRemovePackage.TabIndex = 53;
            buttonRemovePackage.Text = "Remove";
            buttonRemovePackage.UseVisualStyleBackColor = false;
            buttonRemovePackage.Click += ButtonRemovePackage_Click;
            // 
            // buttonAddPackage
            // 
            buttonAddPackage.BackColor = Color.FromArgb(60, 60, 60);
            buttonAddPackage.Cursor = Cursors.Hand;
            buttonAddPackage.FlatAppearance.BorderSize = 0;
            buttonAddPackage.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAddPackage.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAddPackage.FlatStyle = FlatStyle.Flat;
            buttonAddPackage.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonAddPackage.ForeColor = Color.White;
            buttonAddPackage.Location = new Point(16, 32);
            buttonAddPackage.Name = "buttonAddPackage";
            buttonAddPackage.Size = new Size(70, 28);
            buttonAddPackage.TabIndex = 51;
            buttonAddPackage.Text = "Add";
            buttonAddPackage.UseVisualStyleBackColor = false;
            buttonAddPackage.Click += ButtonAddPackage_Click;
            // 
            // listViewPackages
            // 
            listViewPackages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewPackages.Location = new Point(16, 66);
            listViewPackages.Name = "listViewPackages";
            listViewPackages.Size = new Size(428, 177);
            listViewPackages.TabIndex = 55;
            listViewPackages.UseCompatibleStateImageBehavior = false;
            listViewPackages.ColumnClick += ListViewPackagesColumn_Click;
            listViewPackages.SelectedIndexChanged += ListViewPackages_SelectedIndexChanged;
            listViewPackages.DoubleClick += ListViewPackages_DoubleClick;
            // 
            // groupBoxShipFrom
            // 
            groupBoxShipFrom.Controls.Add(checkBoxShipFromPalletJackRequired);
            groupBoxShipFrom.Controls.Add(panelShipFromAppointment);
            groupBoxShipFrom.Controls.Add(checkBoxShipFromAppointmentRequired);
            groupBoxShipFrom.Controls.Add(checkBoxShipFromLiftGateRequired);
            groupBoxShipFrom.Controls.Add(panelShipFromLocations);
            groupBoxShipFrom.Controls.Add(panelShipFrom);
            groupBoxShipFrom.Controls.Add(buttonAddShipFrom);
            groupBoxShipFrom.Controls.Add(labelShipFromVendor);
            groupBoxShipFrom.Controls.Add(comboBoxVendors);
            groupBoxShipFrom.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxShipFrom.ForeColor = Color.LightSkyBlue;
            groupBoxShipFrom.Location = new Point(27, 418);
            groupBoxShipFrom.Name = "groupBoxShipFrom";
            groupBoxShipFrom.Size = new Size(333, 513);
            groupBoxShipFrom.TabIndex = 19;
            groupBoxShipFrom.TabStop = false;
            groupBoxShipFrom.Text = "Ship From:";
            // 
            // checkBoxShipFromPalletJackRequired
            // 
            checkBoxShipFromPalletJackRequired.AutoSize = true;
            checkBoxShipFromPalletJackRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipFromPalletJackRequired.ForeColor = Color.White;
            checkBoxShipFromPalletJackRequired.Location = new Point(204, 306);
            checkBoxShipFromPalletJackRequired.Name = "checkBoxShipFromPalletJackRequired";
            checkBoxShipFromPalletJackRequired.Size = new Size(95, 24);
            checkBoxShipFromPalletJackRequired.TabIndex = 25;
            checkBoxShipFromPalletJackRequired.Text = "Pallet Jack";
            checkBoxShipFromPalletJackRequired.UseVisualStyleBackColor = true;
            // 
            // panelShipFromAppointment
            // 
            panelShipFromAppointment.Controls.Add(labelShipFromAppointmentTime);
            panelShipFromAppointment.Controls.Add(maskedTextBoxShipFromAppointmentTime);
            panelShipFromAppointment.Controls.Add(labelShipFromAppointmentDate);
            panelShipFromAppointment.Controls.Add(dateTimePickerShipFromAppointmentDate);
            panelShipFromAppointment.Location = new Point(9, 367);
            panelShipFromAppointment.Name = "panelShipFromAppointment";
            panelShipFromAppointment.Size = new Size(321, 138);
            panelShipFromAppointment.TabIndex = 36;
            // 
            // labelShipFromAppointmentTime
            // 
            labelShipFromAppointmentTime.AutoSize = true;
            labelShipFromAppointmentTime.Font = new Font("Segoe UI", 11F);
            labelShipFromAppointmentTime.ForeColor = Color.White;
            labelShipFromAppointmentTime.Location = new Point(15, 75);
            labelShipFromAppointmentTime.Name = "labelShipFromAppointmentTime";
            labelShipFromAppointmentTime.Size = new Size(205, 20);
            labelShipFromAppointmentTime.TabIndex = 11;
            labelShipFromAppointmentTime.Text = "Ship From Appointment Time";
            // 
            // maskedTextBoxShipFromAppointmentTime
            // 
            maskedTextBoxShipFromAppointmentTime.Location = new Point(15, 103);
            maskedTextBoxShipFromAppointmentTime.Mask = "90:00";
            maskedTextBoxShipFromAppointmentTime.Name = "maskedTextBoxShipFromAppointmentTime";
            maskedTextBoxShipFromAppointmentTime.Size = new Size(290, 27);
            maskedTextBoxShipFromAppointmentTime.TabIndex = 28;
            maskedTextBoxShipFromAppointmentTime.ValidatingType = typeof(DateTime);
            maskedTextBoxShipFromAppointmentTime.Enter += MaskedTextBoxShipFromAppointmentTime_Enter;
            // 
            // labelShipFromAppointmentDate
            // 
            labelShipFromAppointmentDate.AutoSize = true;
            labelShipFromAppointmentDate.Font = new Font("Segoe UI", 11F);
            labelShipFromAppointmentDate.ForeColor = Color.White;
            labelShipFromAppointmentDate.Location = new Point(18, 8);
            labelShipFromAppointmentDate.Name = "labelShipFromAppointmentDate";
            labelShipFromAppointmentDate.Size = new Size(204, 20);
            labelShipFromAppointmentDate.TabIndex = 9;
            labelShipFromAppointmentDate.Text = "Ship From Appointment Date";
            // 
            // dateTimePickerShipFromAppointmentDate
            // 
            dateTimePickerShipFromAppointmentDate.Location = new Point(15, 37);
            dateTimePickerShipFromAppointmentDate.Name = "dateTimePickerShipFromAppointmentDate";
            dateTimePickerShipFromAppointmentDate.Size = new Size(290, 27);
            dateTimePickerShipFromAppointmentDate.TabIndex = 27;
            // 
            // checkBoxShipFromAppointmentRequired
            // 
            checkBoxShipFromAppointmentRequired.AutoSize = true;
            checkBoxShipFromAppointmentRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipFromAppointmentRequired.ForeColor = Color.White;
            checkBoxShipFromAppointmentRequired.Location = new Point(24, 337);
            checkBoxShipFromAppointmentRequired.Name = "checkBoxShipFromAppointmentRequired";
            checkBoxShipFromAppointmentRequired.Size = new Size(180, 24);
            checkBoxShipFromAppointmentRequired.TabIndex = 24;
            checkBoxShipFromAppointmentRequired.Text = "Appointment Required";
            checkBoxShipFromAppointmentRequired.UseVisualStyleBackColor = true;
            checkBoxShipFromAppointmentRequired.CheckedChanged += CheckBoxShipFromAppointment_CheckedChanged;
            // 
            // checkBoxShipFromLiftGateRequired
            // 
            checkBoxShipFromLiftGateRequired.AutoSize = true;
            checkBoxShipFromLiftGateRequired.Font = new Font("Segoe UI", 11F);
            checkBoxShipFromLiftGateRequired.ForeColor = Color.White;
            checkBoxShipFromLiftGateRequired.Location = new Point(24, 306);
            checkBoxShipFromLiftGateRequired.Name = "checkBoxShipFromLiftGateRequired";
            checkBoxShipFromLiftGateRequired.Size = new Size(148, 24);
            checkBoxShipFromLiftGateRequired.TabIndex = 23;
            checkBoxShipFromLiftGateRequired.Text = "Lift Gate Required";
            checkBoxShipFromLiftGateRequired.UseVisualStyleBackColor = true;
            // 
            // panelShipFromLocations
            // 
            panelShipFromLocations.Controls.Add(labelShipFromLocations);
            panelShipFromLocations.Controls.Add(comboBoxVendorLocations);
            panelShipFromLocations.Location = new Point(19, 100);
            panelShipFromLocations.Name = "panelShipFromLocations";
            panelShipFromLocations.Size = new Size(292, 72);
            panelShipFromLocations.TabIndex = 33;
            // 
            // labelShipFromLocations
            // 
            labelShipFromLocations.AutoSize = true;
            labelShipFromLocations.Font = new Font("Segoe UI", 11F);
            labelShipFromLocations.ForeColor = Color.White;
            labelShipFromLocations.Location = new Point(9, 4);
            labelShipFromLocations.Name = "labelShipFromLocations";
            labelShipFromLocations.Size = new Size(119, 20);
            labelShipFromLocations.TabIndex = 1;
            labelShipFromLocations.Text = "Select a location";
            // 
            // comboBoxVendorLocations
            // 
            comboBoxVendorLocations.FormattingEnabled = true;
            comboBoxVendorLocations.Location = new Point(3, 37);
            comboBoxVendorLocations.Name = "comboBoxVendorLocations";
            comboBoxVendorLocations.Size = new Size(289, 28);
            comboBoxVendorLocations.TabIndex = 22;
            comboBoxVendorLocations.SelectedIndexChanged += ComboBoxVendorLocations_SelectedIndexChanged;
            // 
            // panelShipFrom
            // 
            panelShipFrom.Controls.Add(labelShipFromPostalCode);
            panelShipFrom.Controls.Add(labelShipFromCountry);
            panelShipFrom.Controls.Add(labelShipFromRegion);
            panelShipFrom.Controls.Add(labelShipFromCity);
            panelShipFrom.Controls.Add(labelShipFromAddress);
            panelShipFrom.Controls.Add(labelShipFromName);
            panelShipFrom.ForeColor = Color.White;
            panelShipFrom.Location = new Point(19, 171);
            panelShipFrom.Name = "panelShipFrom";
            panelShipFrom.Size = new Size(292, 123);
            panelShipFrom.TabIndex = 31;
            // 
            // labelShipFromPostalCode
            // 
            labelShipFromPostalCode.AutoSize = true;
            labelShipFromPostalCode.Font = new Font("Segoe UI", 11F);
            labelShipFromPostalCode.Location = new Point(18, 92);
            labelShipFromPostalCode.Name = "labelShipFromPostalCode";
            labelShipFromPostalCode.Size = new Size(48, 20);
            labelShipFromPostalCode.TabIndex = 19;
            labelShipFromPostalCode.Text = "Postal";
            // 
            // labelShipFromCountry
            // 
            labelShipFromCountry.AutoSize = true;
            labelShipFromCountry.Font = new Font("Segoe UI", 11F);
            labelShipFromCountry.Location = new Point(185, 92);
            labelShipFromCountry.Name = "labelShipFromCountry";
            labelShipFromCountry.Size = new Size(95, 20);
            labelShipFromCountry.TabIndex = 18;
            labelShipFromCountry.Text = "CountryCode";
            // 
            // labelShipFromRegion
            // 
            labelShipFromRegion.AutoSize = true;
            labelShipFromRegion.Font = new Font("Segoe UI", 11F);
            labelShipFromRegion.Location = new Point(185, 62);
            labelShipFromRegion.Name = "labelShipFromRegion";
            labelShipFromRegion.Size = new Size(91, 20);
            labelShipFromRegion.TabIndex = 17;
            labelShipFromRegion.Text = "RegionCode";
            // 
            // labelShipFromCity
            // 
            labelShipFromCity.AutoSize = true;
            labelShipFromCity.Font = new Font("Segoe UI", 11F);
            labelShipFromCity.Location = new Point(18, 62);
            labelShipFromCity.Name = "labelShipFromCity";
            labelShipFromCity.Size = new Size(34, 20);
            labelShipFromCity.TabIndex = 16;
            labelShipFromCity.Text = "City";
            // 
            // labelShipFromAddress
            // 
            labelShipFromAddress.AutoSize = true;
            labelShipFromAddress.Font = new Font("Segoe UI", 11F);
            labelShipFromAddress.Location = new Point(18, 36);
            labelShipFromAddress.Name = "labelShipFromAddress";
            labelShipFromAddress.Size = new Size(62, 20);
            labelShipFromAddress.TabIndex = 15;
            labelShipFromAddress.Text = "Address";
            // 
            // labelShipFromName
            // 
            labelShipFromName.AutoSize = true;
            labelShipFromName.Font = new Font("Segoe UI", 11F);
            labelShipFromName.Location = new Point(18, 10);
            labelShipFromName.Name = "labelShipFromName";
            labelShipFromName.Size = new Size(49, 20);
            labelShipFromName.TabIndex = 14;
            labelShipFromName.Text = "Name";
            // 
            // buttonAddShipFrom
            // 
            buttonAddShipFrom.BackColor = Color.SteelBlue;
            buttonAddShipFrom.Cursor = Cursors.Hand;
            buttonAddShipFrom.FlatAppearance.BorderSize = 0;
            buttonAddShipFrom.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonAddShipFrom.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAddShipFrom.FlatStyle = FlatStyle.Flat;
            buttonAddShipFrom.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            buttonAddShipFrom.Location = new Point(282, 28);
            buttonAddShipFrom.Name = "buttonAddShipFrom";
            buttonAddShipFrom.Size = new Size(29, 28);
            buttonAddShipFrom.TabIndex = 20;
            buttonAddShipFrom.Text = "+";
            buttonAddShipFrom.UseVisualStyleBackColor = false;
            buttonAddShipFrom.Click += ButtonAddVendorFrom_Click;
            // 
            // labelShipFromVendor
            // 
            labelShipFromVendor.AutoSize = true;
            labelShipFromVendor.Font = new Font("Segoe UI", 11F);
            labelShipFromVendor.ForeColor = Color.White;
            labelShipFromVendor.Location = new Point(22, 32);
            labelShipFromVendor.Name = "labelShipFromVendor";
            labelShipFromVendor.Size = new Size(100, 20);
            labelShipFromVendor.TabIndex = 1;
            labelShipFromVendor.Text = "Select Vendor";
            // 
            // comboBoxVendors
            // 
            comboBoxVendors.FormattingEnabled = true;
            comboBoxVendors.Location = new Point(21, 61);
            comboBoxVendors.Name = "comboBoxVendors";
            comboBoxVendors.Size = new Size(290, 28);
            comboBoxVendors.TabIndex = 21;
            comboBoxVendors.SelectedIndexChanged += ComboBoxVendors_SelectedIndexChanged;
            // 
            // groupBoxPayment
            // 
            groupBoxPayment.Controls.Add(labelEstimatedBolWeight);
            groupBoxPayment.Controls.Add(textBoxEstimatedBolWeight);
            groupBoxPayment.Controls.Add(labelEstimatedBolValue);
            groupBoxPayment.Controls.Add(textBoxBolEstimatedValue);
            groupBoxPayment.Controls.Add(checkBoxCustomerInvoice);
            groupBoxPayment.Controls.Add(checkBoxFreightPrepaid);
            groupBoxPayment.Controls.Add(maskedTextBoxCodAmount);
            groupBoxPayment.Controls.Add(checkBoxPaymentCOD);
            groupBoxPayment.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPayment.ForeColor = Color.AntiqueWhite;
            groupBoxPayment.Location = new Point(1223, 173);
            groupBoxPayment.Name = "groupBoxPayment";
            groupBoxPayment.Size = new Size(382, 226);
            groupBoxPayment.TabIndex = 56;
            groupBoxPayment.TabStop = false;
            groupBoxPayment.Text = "Payment and values";
            // 
            // labelEstimatedBolWeight
            // 
            labelEstimatedBolWeight.AutoSize = true;
            labelEstimatedBolWeight.Font = new Font("Segoe UI", 11F);
            labelEstimatedBolWeight.ForeColor = Color.White;
            labelEstimatedBolWeight.Location = new Point(209, 32);
            labelEstimatedBolWeight.Name = "labelEstimatedBolWeight";
            labelEstimatedBolWeight.Size = new Size(157, 20);
            labelEstimatedBolWeight.TabIndex = 7;
            labelEstimatedBolWeight.Text = "Estimated BOL Weight";
            // 
            // textBoxEstimatedBolWeight
            // 
            textBoxEstimatedBolWeight.Location = new Point(209, 61);
            textBoxEstimatedBolWeight.Name = "textBoxEstimatedBolWeight";
            textBoxEstimatedBolWeight.Size = new Size(152, 27);
            textBoxEstimatedBolWeight.TabIndex = 58;
            textBoxEstimatedBolWeight.Enter += TextBoxControl_Enter;
            textBoxEstimatedBolWeight.KeyPress += TextBoxEstimatedBolWeight_KeyPress;
            // 
            // labelEstimatedBolValue
            // 
            labelEstimatedBolValue.AutoSize = true;
            labelEstimatedBolValue.Font = new Font("Segoe UI", 11F);
            labelEstimatedBolValue.ForeColor = Color.White;
            labelEstimatedBolValue.Location = new Point(23, 32);
            labelEstimatedBolValue.Name = "labelEstimatedBolValue";
            labelEstimatedBolValue.Size = new Size(146, 20);
            labelEstimatedBolValue.TabIndex = 5;
            labelEstimatedBolValue.Text = "Estimated BOL Value";
            // 
            // textBoxBolEstimatedValue
            // 
            textBoxBolEstimatedValue.Location = new Point(23, 61);
            textBoxBolEstimatedValue.Name = "textBoxBolEstimatedValue";
            textBoxBolEstimatedValue.Size = new Size(152, 27);
            textBoxBolEstimatedValue.TabIndex = 57;
            textBoxBolEstimatedValue.Enter += TextBoxControl_Enter;
            textBoxBolEstimatedValue.KeyPress += TextBoxBolEstimatedValue_KeyPress;
            textBoxBolEstimatedValue.Validating += TextBoxCurrencyAmount_Validating;
            // 
            // checkBoxCustomerInvoice
            // 
            checkBoxCustomerInvoice.AutoSize = true;
            checkBoxCustomerInvoice.CausesValidation = false;
            checkBoxCustomerInvoice.Font = new Font("Segoe UI", 11F);
            checkBoxCustomerInvoice.ForeColor = Color.White;
            checkBoxCustomerInvoice.Location = new Point(205, 104);
            checkBoxCustomerInvoice.Name = "checkBoxCustomerInvoice";
            checkBoxCustomerInvoice.Size = new Size(151, 24);
            checkBoxCustomerInvoice.TabIndex = 60;
            checkBoxCustomerInvoice.Text = "Customer Invoiced";
            checkBoxCustomerInvoice.UseVisualStyleBackColor = true;
            checkBoxCustomerInvoice.CheckedChanged += CheckBoxCustomerInvoice_CheckedChanged;
            // 
            // checkBoxFreightPrepaid
            // 
            checkBoxFreightPrepaid.AutoSize = true;
            checkBoxFreightPrepaid.CausesValidation = false;
            checkBoxFreightPrepaid.Font = new Font("Segoe UI", 11F);
            checkBoxFreightPrepaid.ForeColor = Color.White;
            checkBoxFreightPrepaid.Location = new Point(23, 104);
            checkBoxFreightPrepaid.Name = "checkBoxFreightPrepaid";
            checkBoxFreightPrepaid.Size = new Size(129, 24);
            checkBoxFreightPrepaid.TabIndex = 59;
            checkBoxFreightPrepaid.Text = "Freight Prepaid";
            checkBoxFreightPrepaid.UseVisualStyleBackColor = true;
            checkBoxFreightPrepaid.CheckedChanged += CheckBoxFreightPrepaid_CheckedChanged;
            // 
            // maskedTextBoxCodAmount
            // 
            maskedTextBoxCodAmount.Enabled = false;
            maskedTextBoxCodAmount.Location = new Point(18, 165);
            maskedTextBoxCodAmount.Name = "maskedTextBoxCodAmount";
            maskedTextBoxCodAmount.Size = new Size(338, 27);
            maskedTextBoxCodAmount.TabIndex = 62;
            maskedTextBoxCodAmount.Enter += MaskedTextBoxControl_Enter;
            maskedTextBoxCodAmount.KeyPress += MaskedTextBoxCodAmount_KeyPress;
            maskedTextBoxCodAmount.Validating += MaskedTextBoxCurrencyAmount_Validating;
            // 
            // checkBoxPaymentCOD
            // 
            checkBoxPaymentCOD.AutoSize = true;
            checkBoxPaymentCOD.CausesValidation = false;
            checkBoxPaymentCOD.Font = new Font("Segoe UI", 11F);
            checkBoxPaymentCOD.ForeColor = Color.White;
            checkBoxPaymentCOD.Location = new Point(23, 134);
            checkBoxPaymentCOD.Name = "checkBoxPaymentCOD";
            checkBoxPaymentCOD.Size = new Size(198, 24);
            checkBoxPaymentCOD.TabIndex = 61;
            checkBoxPaymentCOD.Text = "Collect on Delivery (COD)";
            checkBoxPaymentCOD.UseVisualStyleBackColor = true;
            checkBoxPaymentCOD.CheckedChanged += CheckBoxPaymentCOD_CheckedChanged;
            // 
            // groupBoxReferences
            // 
            groupBoxReferences.Controls.Add(textBoxBolNumber);
            groupBoxReferences.Controls.Add(labelBolNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxOrderNumber);
            groupBoxReferences.Controls.Add(labelOrderNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxShipperQuoteNumber);
            groupBoxReferences.Controls.Add(labelShipperQuoteNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxReferenceNumber);
            groupBoxReferences.Controls.Add(labelReferenceNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxActualPrice);
            groupBoxReferences.Controls.Add(labelActualPrice);
            groupBoxReferences.Controls.Add(maskedTextBoxQuotedPrice);
            groupBoxReferences.Controls.Add(labelQuotedPrice);
            groupBoxReferences.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxReferences.ForeColor = Color.Thistle;
            groupBoxReferences.Location = new Point(1223, 423);
            groupBoxReferences.Name = "groupBoxReferences";
            groupBoxReferences.Size = new Size(382, 226);
            groupBoxReferences.TabIndex = 63;
            groupBoxReferences.TabStop = false;
            groupBoxReferences.Text = "References";
            // 
            // textBoxBolNumber
            // 
            textBoxBolNumber.Location = new Point(207, 59);
            textBoxBolNumber.Name = "textBoxBolNumber";
            textBoxBolNumber.ReadOnly = true;
            textBoxBolNumber.Size = new Size(157, 27);
            textBoxBolNumber.TabIndex = 74;
            // 
            // labelBolNumber
            // 
            labelBolNumber.AutoSize = true;
            labelBolNumber.Font = new Font("Segoe UI", 11F);
            labelBolNumber.ForeColor = Color.White;
            labelBolNumber.Location = new Point(209, 30);
            labelBolNumber.Name = "labelBolNumber";
            labelBolNumber.Size = new Size(128, 20);
            labelBolNumber.TabIndex = 73;
            labelBolNumber.Text = "BOL Number (RO)";
            // 
            // maskedTextBoxOrderNumber
            // 
            maskedTextBoxOrderNumber.Location = new Point(209, 183);
            maskedTextBoxOrderNumber.Name = "maskedTextBoxOrderNumber";
            maskedTextBoxOrderNumber.Size = new Size(152, 27);
            maskedTextBoxOrderNumber.TabIndex = 68;
            maskedTextBoxOrderNumber.Enter += MaskedTextBoxControl_Enter;
            // 
            // labelOrderNumber
            // 
            labelOrderNumber.AutoSize = true;
            labelOrderNumber.Font = new Font("Segoe UI", 11F);
            labelOrderNumber.ForeColor = Color.White;
            labelOrderNumber.Location = new Point(209, 160);
            labelOrderNumber.Name = "labelOrderNumber";
            labelOrderNumber.Size = new Size(138, 20);
            labelOrderNumber.TabIndex = 8;
            labelOrderNumber.Text = "Your Order Number";
            // 
            // maskedTextBoxShipperQuoteNumber
            // 
            maskedTextBoxShipperQuoteNumber.Location = new Point(23, 61);
            maskedTextBoxShipperQuoteNumber.Name = "maskedTextBoxShipperQuoteNumber";
            maskedTextBoxShipperQuoteNumber.Size = new Size(152, 27);
            maskedTextBoxShipperQuoteNumber.TabIndex = 64;
            maskedTextBoxShipperQuoteNumber.Enter += MaskedTextBoxControl_Enter;
            // 
            // labelShipperQuoteNumber
            // 
            labelShipperQuoteNumber.AutoSize = true;
            labelShipperQuoteNumber.Font = new Font("Segoe UI", 11F);
            labelShipperQuoteNumber.ForeColor = Color.White;
            labelShipperQuoteNumber.Location = new Point(28, 32);
            labelShipperQuoteNumber.Name = "labelShipperQuoteNumber";
            labelShipperQuoteNumber.Size = new Size(108, 20);
            labelShipperQuoteNumber.TabIndex = 6;
            labelShipperQuoteNumber.Text = "Quote Number";
            // 
            // maskedTextBoxReferenceNumber
            // 
            maskedTextBoxReferenceNumber.Location = new Point(23, 185);
            maskedTextBoxReferenceNumber.Name = "maskedTextBoxReferenceNumber";
            maskedTextBoxReferenceNumber.Size = new Size(152, 27);
            maskedTextBoxReferenceNumber.TabIndex = 67;
            maskedTextBoxReferenceNumber.Enter += MaskedTextBoxControl_Enter;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.AutoSize = true;
            labelReferenceNumber.Font = new Font("Segoe UI", 11F);
            labelReferenceNumber.ForeColor = Color.White;
            labelReferenceNumber.Location = new Point(28, 160);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(142, 20);
            labelReferenceNumber.TabIndex = 4;
            labelReferenceNumber.Text = "Your Reference/PO#";
            // 
            // maskedTextBoxActualPrice
            // 
            maskedTextBoxActualPrice.Location = new Point(209, 123);
            maskedTextBoxActualPrice.Name = "maskedTextBoxActualPrice";
            maskedTextBoxActualPrice.Size = new Size(152, 27);
            maskedTextBoxActualPrice.TabIndex = 66;
            maskedTextBoxActualPrice.Enter += MaskedTextBoxControl_Enter;
            maskedTextBoxActualPrice.Validating += MaskedTextBoxCurrencyAmount_Validating;
            // 
            // labelActualPrice
            // 
            labelActualPrice.AutoSize = true;
            labelActualPrice.Font = new Font("Segoe UI", 11F);
            labelActualPrice.ForeColor = Color.White;
            labelActualPrice.Location = new Point(209, 97);
            labelActualPrice.Name = "labelActualPrice";
            labelActualPrice.Size = new Size(87, 20);
            labelActualPrice.TabIndex = 2;
            labelActualPrice.Text = "Actual Price";
            // 
            // maskedTextBoxQuotedPrice
            // 
            maskedTextBoxQuotedPrice.Location = new Point(23, 124);
            maskedTextBoxQuotedPrice.Name = "maskedTextBoxQuotedPrice";
            maskedTextBoxQuotedPrice.Size = new Size(152, 27);
            maskedTextBoxQuotedPrice.TabIndex = 65;
            maskedTextBoxQuotedPrice.Enter += MaskedTextBoxControl_Enter;
            maskedTextBoxQuotedPrice.Validating += MaskedTextBoxCurrencyAmount_Validating;
            // 
            // labelQuotedPrice
            // 
            labelQuotedPrice.AutoSize = true;
            labelQuotedPrice.Font = new Font("Segoe UI", 11F);
            labelQuotedPrice.ForeColor = Color.White;
            labelQuotedPrice.Location = new Point(28, 97);
            labelQuotedPrice.Name = "labelQuotedPrice";
            labelQuotedPrice.Size = new Size(95, 20);
            labelQuotedPrice.TabIndex = 0;
            labelQuotedPrice.Text = "Quoted Price";
            // 
            // groupBoxTransitTimes
            // 
            groupBoxTransitTimes.Controls.Add(textBoxActualTransitDays);
            groupBoxTransitTimes.Controls.Add(labelActualTransitTime);
            groupBoxTransitTimes.Controls.Add(textBoxEstimatedTransitDays);
            groupBoxTransitTimes.Controls.Add(labelEstimatedTransitDays);
            groupBoxTransitTimes.Controls.Add(labelDeliveryDate);
            groupBoxTransitTimes.Controls.Add(dateTimePickerDeliveryDate);
            groupBoxTransitTimes.Controls.Add(labelTransitShipDate);
            groupBoxTransitTimes.Controls.Add(dateTimePickerShipDate);
            groupBoxTransitTimes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxTransitTimes.ForeColor = Color.Thistle;
            groupBoxTransitTimes.Location = new Point(1223, 672);
            groupBoxTransitTimes.Name = "groupBoxTransitTimes";
            groupBoxTransitTimes.Size = new Size(382, 259);
            groupBoxTransitTimes.TabIndex = 69;
            groupBoxTransitTimes.TabStop = false;
            groupBoxTransitTimes.Text = "Transit ";
            // 
            // textBoxActualTransitDays
            // 
            textBoxActualTransitDays.Location = new Point(204, 191);
            textBoxActualTransitDays.Name = "textBoxActualTransitDays";
            textBoxActualTransitDays.Size = new Size(157, 27);
            textBoxActualTransitDays.TabIndex = 73;
            textBoxActualTransitDays.Enter += TextBoxControl_Enter;
            textBoxActualTransitDays.KeyPress += TextBoxActualTransitDays_KeyPress;
            // 
            // labelActualTransitTime
            // 
            labelActualTransitTime.AutoSize = true;
            labelActualTransitTime.Font = new Font("Segoe UI", 11F);
            labelActualTransitTime.ForeColor = Color.White;
            labelActualTransitTime.Location = new Point(209, 163);
            labelActualTransitTime.Name = "labelActualTransitTime";
            labelActualTransitTime.Size = new Size(114, 20);
            labelActualTransitTime.TabIndex = 6;
            labelActualTransitTime.Text = "Act Transit Days";
            // 
            // textBoxEstimatedTransitDays
            // 
            textBoxEstimatedTransitDays.Location = new Point(18, 191);
            textBoxEstimatedTransitDays.Name = "textBoxEstimatedTransitDays";
            textBoxEstimatedTransitDays.Size = new Size(157, 27);
            textBoxEstimatedTransitDays.TabIndex = 72;
            textBoxEstimatedTransitDays.Enter += TextBoxControl_Enter;
            textBoxEstimatedTransitDays.KeyPress += TextBoxEstimatedTransitDays_KeyPress;
            // 
            // labelEstimatedTransitDays
            // 
            labelEstimatedTransitDays.AutoSize = true;
            labelEstimatedTransitDays.Font = new Font("Segoe UI", 11F);
            labelEstimatedTransitDays.ForeColor = Color.White;
            labelEstimatedTransitDays.Location = new Point(23, 163);
            labelEstimatedTransitDays.Name = "labelEstimatedTransitDays";
            labelEstimatedTransitDays.Size = new Size(111, 20);
            labelEstimatedTransitDays.TabIndex = 4;
            labelEstimatedTransitDays.Text = "Est Transit Days";
            // 
            // labelDeliveryDate
            // 
            labelDeliveryDate.AutoSize = true;
            labelDeliveryDate.Font = new Font("Segoe UI", 11F);
            labelDeliveryDate.ForeColor = Color.White;
            labelDeliveryDate.Location = new Point(23, 96);
            labelDeliveryDate.Name = "labelDeliveryDate";
            labelDeliveryDate.Size = new Size(99, 20);
            labelDeliveryDate.TabIndex = 3;
            labelDeliveryDate.Text = "Delivery Date";
            // 
            // dateTimePickerDeliveryDate
            // 
            dateTimePickerDeliveryDate.Location = new Point(18, 125);
            dateTimePickerDeliveryDate.Name = "dateTimePickerDeliveryDate";
            dateTimePickerDeliveryDate.Size = new Size(343, 27);
            dateTimePickerDeliveryDate.TabIndex = 71;
            dateTimePickerDeliveryDate.ValueChanged += DateTimePickerDeliveryDate_ValueChanged;
            dateTimePickerDeliveryDate.Leave += DateTimePickerDeliveryDate_Leave;
            // 
            // labelTransitShipDate
            // 
            labelTransitShipDate.AutoSize = true;
            labelTransitShipDate.Font = new Font("Segoe UI", 11F);
            labelTransitShipDate.ForeColor = Color.White;
            labelTransitShipDate.Location = new Point(23, 31);
            labelTransitShipDate.Name = "labelTransitShipDate";
            labelTransitShipDate.Size = new Size(74, 20);
            labelTransitShipDate.TabIndex = 1;
            labelTransitShipDate.Text = "Ship Date";
            // 
            // dateTimePickerShipDate
            // 
            dateTimePickerShipDate.Location = new Point(18, 60);
            dateTimePickerShipDate.Name = "dateTimePickerShipDate";
            dateTimePickerShipDate.Size = new Size(343, 27);
            dateTimePickerShipDate.TabIndex = 70;
            dateTimePickerShipDate.ValueChanged += DateTimePickerShipDate_ValueChanged;
            // 
            // labelSpecialInstructions
            // 
            labelSpecialInstructions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelSpecialInstructions.AutoSize = true;
            labelSpecialInstructions.Font = new Font("Segoe UI", 11F);
            labelSpecialInstructions.ForeColor = Color.White;
            labelSpecialInstructions.Location = new Point(388, 952);
            labelSpecialInstructions.Name = "labelSpecialInstructions";
            labelSpecialInstructions.Size = new Size(136, 20);
            labelSpecialInstructions.TabIndex = 45;
            labelSpecialInstructions.Text = "Special Instructions";
            // 
            // textBoxSpecialInstructions
            // 
            textBoxSpecialInstructions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxSpecialInstructions.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxSpecialInstructions.Location = new Point(382, 982);
            textBoxSpecialInstructions.Margin = new Padding(3, 4, 3, 4);
            textBoxSpecialInstructions.Multiline = true;
            textBoxSpecialInstructions.Name = "textBoxSpecialInstructions";
            textBoxSpecialInstructions.Size = new Size(333, 100);
            textBoxSpecialInstructions.TabIndex = 75;
            // 
            // groupBoxServiceTypes
            // 
            groupBoxServiceTypes.Controls.Add(radioButtonLTL);
            groupBoxServiceTypes.Controls.Add(radioButtonShowAll);
            groupBoxServiceTypes.Controls.Add(radioButtonArmouredCar);
            groupBoxServiceTypes.Controls.Add(radioButtonCourier);
            groupBoxServiceTypes.Controls.Add(radioButtonLastMile);
            groupBoxServiceTypes.Controls.Add(radioButtonRailroad);
            groupBoxServiceTypes.Controls.Add(radioButtonOcean);
            groupBoxServiceTypes.Controls.Add(radioButtonAir);
            groupBoxServiceTypes.Controls.Add(radioButtonFTL);
            groupBoxServiceTypes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxServiceTypes.ForeColor = Color.Gold;
            groupBoxServiceTypes.Location = new Point(27, 92);
            groupBoxServiceTypes.Name = "groupBoxServiceTypes";
            groupBoxServiceTypes.Size = new Size(1172, 67);
            groupBoxServiceTypes.TabIndex = 0;
            groupBoxServiceTypes.TabStop = false;
            groupBoxServiceTypes.Text = "Choose shipper service type first";
            // 
            // radioButtonLTL
            // 
            radioButtonLTL.AutoSize = true;
            radioButtonLTL.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 128, 0);
            radioButtonLTL.FlatStyle = FlatStyle.Popup;
            radioButtonLTL.Font = new Font("Segoe UI", 11F);
            radioButtonLTL.ForeColor = Color.White;
            radioButtonLTL.Location = new Point(24, 26);
            radioButtonLTL.Name = "radioButtonLTL";
            radioButtonLTL.Size = new Size(97, 24);
            radioButtonLTL.TabIndex = 1;
            radioButtonLTL.TabStop = true;
            radioButtonLTL.Tag = "LTL";
            radioButtonLTL.Text = "LTL Freight";
            radioButtonLTL.UseVisualStyleBackColor = true;
            radioButtonLTL.CheckedChanged += RadioButtonLTL_CheckedChanged;
            // 
            // radioButtonShowAll
            // 
            radioButtonShowAll.AutoSize = true;
            radioButtonShowAll.FlatStyle = FlatStyle.Popup;
            radioButtonShowAll.Font = new Font("Segoe UI", 11F);
            radioButtonShowAll.ForeColor = Color.White;
            radioButtonShowAll.Location = new Point(876, 26);
            radioButtonShowAll.Name = "radioButtonShowAll";
            radioButtonShowAll.Size = new Size(145, 24);
            radioButtonShowAll.TabIndex = 9;
            radioButtonShowAll.TabStop = true;
            radioButtonShowAll.Tag = "ShowAllShippers";
            radioButtonShowAll.Text = "Show All Shippers";
            radioButtonShowAll.UseVisualStyleBackColor = true;
            radioButtonShowAll.CheckedChanged += RadioButtonShowAll_CheckedChanged;
            // 
            // radioButtonArmouredCar
            // 
            radioButtonArmouredCar.AutoSize = true;
            radioButtonArmouredCar.FlatStyle = FlatStyle.Popup;
            radioButtonArmouredCar.Font = new Font("Segoe UI", 11F);
            radioButtonArmouredCar.ForeColor = Color.White;
            radioButtonArmouredCar.Location = new Point(751, 26);
            radioButtonArmouredCar.Name = "radioButtonArmouredCar";
            radioButtonArmouredCar.Size = new Size(93, 24);
            radioButtonArmouredCar.TabIndex = 8;
            radioButtonArmouredCar.TabStop = true;
            radioButtonArmouredCar.Tag = "ArmouredCar";
            radioButtonArmouredCar.Text = "Armoured";
            radioButtonArmouredCar.UseVisualStyleBackColor = true;
            radioButtonArmouredCar.CheckedChanged += RadioButtonArmouredCar_CheckedChanged;
            // 
            // radioButtonCourier
            // 
            radioButtonCourier.AutoSize = true;
            radioButtonCourier.FlatStyle = FlatStyle.Popup;
            radioButtonCourier.Font = new Font("Segoe UI", 11F);
            radioButtonCourier.ForeColor = Color.White;
            radioButtonCourier.Location = new Point(647, 26);
            radioButtonCourier.Name = "radioButtonCourier";
            radioButtonCourier.Size = new Size(74, 24);
            radioButtonCourier.TabIndex = 7;
            radioButtonCourier.TabStop = true;
            radioButtonCourier.Tag = "Courier";
            radioButtonCourier.Text = "Courier";
            radioButtonCourier.UseVisualStyleBackColor = true;
            radioButtonCourier.CheckedChanged += RadioButtonCourier_CheckedChanged;
            // 
            // radioButtonLastMile
            // 
            radioButtonLastMile.AutoSize = true;
            radioButtonLastMile.FlatStyle = FlatStyle.Popup;
            radioButtonLastMile.Font = new Font("Segoe UI", 11F);
            radioButtonLastMile.ForeColor = Color.White;
            radioButtonLastMile.Location = new Point(531, 26);
            radioButtonLastMile.Name = "radioButtonLastMile";
            radioButtonLastMile.Size = new Size(85, 24);
            radioButtonLastMile.TabIndex = 6;
            radioButtonLastMile.TabStop = true;
            radioButtonLastMile.Tag = "LastMile";
            radioButtonLastMile.Text = "Last Mile";
            radioButtonLastMile.UseVisualStyleBackColor = true;
            radioButtonLastMile.CheckedChanged += RadioButtonLastMile_CheckedChanged;
            // 
            // radioButtonRailroad
            // 
            radioButtonRailroad.AutoSize = true;
            radioButtonRailroad.FlatStyle = FlatStyle.Popup;
            radioButtonRailroad.Font = new Font("Segoe UI", 11F);
            radioButtonRailroad.ForeColor = Color.White;
            radioButtonRailroad.Location = new Point(452, 26);
            radioButtonRailroad.Name = "radioButtonRailroad";
            radioButtonRailroad.Size = new Size(51, 24);
            radioButtonRailroad.TabIndex = 5;
            radioButtonRailroad.TabStop = true;
            radioButtonRailroad.Tag = "Rail";
            radioButtonRailroad.Text = "Rail";
            radioButtonRailroad.UseVisualStyleBackColor = true;
            radioButtonRailroad.CheckedChanged += RadioButtonRailroad_CheckedChanged;
            // 
            // radioButtonOcean
            // 
            radioButtonOcean.AutoSize = true;
            radioButtonOcean.FlatStyle = FlatStyle.Popup;
            radioButtonOcean.Font = new Font("Segoe UI", 11F);
            radioButtonOcean.ForeColor = Color.White;
            radioButtonOcean.Location = new Point(356, 26);
            radioButtonOcean.Name = "radioButtonOcean";
            radioButtonOcean.Size = new Size(68, 24);
            radioButtonOcean.TabIndex = 4;
            radioButtonOcean.TabStop = true;
            radioButtonOcean.Tag = "Ocean";
            radioButtonOcean.Text = "Ocean";
            radioButtonOcean.UseVisualStyleBackColor = true;
            radioButtonOcean.CheckedChanged += RadioButtonOcean_CheckedChanged;
            // 
            // radioButtonAir
            // 
            radioButtonAir.AutoSize = true;
            radioButtonAir.FlatStyle = FlatStyle.Popup;
            radioButtonAir.Font = new Font("Segoe UI", 11F);
            radioButtonAir.ForeColor = Color.White;
            radioButtonAir.Location = new Point(282, 26);
            radioButtonAir.Name = "radioButtonAir";
            radioButtonAir.Size = new Size(45, 24);
            radioButtonAir.TabIndex = 3;
            radioButtonAir.TabStop = true;
            radioButtonAir.Tag = "Air";
            radioButtonAir.Text = "Air";
            radioButtonAir.UseVisualStyleBackColor = true;
            radioButtonAir.CheckedChanged += RadioButtonAir_CheckedChanged;
            // 
            // radioButtonFTL
            // 
            radioButtonFTL.AutoSize = true;
            radioButtonFTL.FlatStyle = FlatStyle.Popup;
            radioButtonFTL.Font = new Font("Segoe UI", 11F);
            radioButtonFTL.ForeColor = Color.White;
            radioButtonFTL.Location = new Point(150, 26);
            radioButtonFTL.Name = "radioButtonFTL";
            radioButtonFTL.Size = new Size(98, 24);
            radioButtonFTL.TabIndex = 2;
            radioButtonFTL.TabStop = true;
            radioButtonFTL.Tag = "FTL";
            radioButtonFTL.Text = "FTL Freight";
            radioButtonFTL.UseVisualStyleBackColor = true;
            radioButtonFTL.CheckedChanged += RadioButtonFTL_CheckedChanged;
            // 
            // groupBoxContainers
            // 
            groupBoxContainers.Controls.Add(buttonContainersExpandCollapse);
            groupBoxContainers.Controls.Add(buttonEditContainer);
            groupBoxContainers.Controls.Add(buttonRemoveContainer);
            groupBoxContainers.Controls.Add(buttonAddContainer);
            groupBoxContainers.Controls.Add(listViewContainers);
            groupBoxContainers.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxContainers.ForeColor = Color.DarkKhaki;
            groupBoxContainers.Location = new Point(739, 173);
            groupBoxContainers.Name = "groupBoxContainers";
            groupBoxContainers.Size = new Size(460, 226);
            groupBoxContainers.TabIndex = 38;
            groupBoxContainers.TabStop = false;
            groupBoxContainers.Text = "Containers";
            // 
            // buttonContainersExpandCollapse
            // 
            buttonContainersExpandCollapse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonContainersExpandCollapse.BackColor = Color.FromArgb(60, 60, 60);
            buttonContainersExpandCollapse.Cursor = Cursors.Hand;
            buttonContainersExpandCollapse.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonContainersExpandCollapse.FlatAppearance.BorderSize = 0;
            buttonContainersExpandCollapse.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonContainersExpandCollapse.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonContainersExpandCollapse.FlatStyle = FlatStyle.Flat;
            buttonContainersExpandCollapse.ForeColor = Color.Transparent;
            buttonContainersExpandCollapse.Image = Properties.Resources.Expand28;
            buttonContainersExpandCollapse.Location = new Point(415, 30);
            buttonContainersExpandCollapse.Name = "buttonContainersExpandCollapse";
            buttonContainersExpandCollapse.Size = new Size(28, 28);
            buttonContainersExpandCollapse.TabIndex = 42;
            buttonContainersExpandCollapse.UseVisualStyleBackColor = false;
            buttonContainersExpandCollapse.Click += ButtonContainersExpandCollapse_Click;
            // 
            // buttonEditContainer
            // 
            buttonEditContainer.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditContainer.Cursor = Cursors.Hand;
            buttonEditContainer.FlatAppearance.BorderSize = 0;
            buttonEditContainer.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditContainer.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditContainer.FlatStyle = FlatStyle.Flat;
            buttonEditContainer.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonEditContainer.ForeColor = Color.White;
            buttonEditContainer.Location = new Point(99, 31);
            buttonEditContainer.Name = "buttonEditContainer";
            buttonEditContainer.Size = new Size(70, 28);
            buttonEditContainer.TabIndex = 40;
            buttonEditContainer.Text = "Edit";
            buttonEditContainer.UseVisualStyleBackColor = false;
            // 
            // buttonRemoveContainer
            // 
            buttonRemoveContainer.BackColor = Color.FromArgb(60, 60, 60);
            buttonRemoveContainer.Cursor = Cursors.Hand;
            buttonRemoveContainer.FlatAppearance.BorderSize = 0;
            buttonRemoveContainer.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonRemoveContainer.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonRemoveContainer.FlatStyle = FlatStyle.Flat;
            buttonRemoveContainer.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonRemoveContainer.ForeColor = Color.White;
            buttonRemoveContainer.Location = new Point(182, 31);
            buttonRemoveContainer.Name = "buttonRemoveContainer";
            buttonRemoveContainer.Size = new Size(70, 28);
            buttonRemoveContainer.TabIndex = 41;
            buttonRemoveContainer.Text = "Remove";
            buttonRemoveContainer.UseVisualStyleBackColor = false;
            // 
            // buttonAddContainer
            // 
            buttonAddContainer.BackColor = Color.FromArgb(60, 60, 60);
            buttonAddContainer.Cursor = Cursors.Hand;
            buttonAddContainer.FlatAppearance.BorderSize = 0;
            buttonAddContainer.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAddContainer.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAddContainer.FlatStyle = FlatStyle.Flat;
            buttonAddContainer.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonAddContainer.ForeColor = Color.White;
            buttonAddContainer.Location = new Point(16, 31);
            buttonAddContainer.Name = "buttonAddContainer";
            buttonAddContainer.Size = new Size(70, 28);
            buttonAddContainer.TabIndex = 39;
            buttonAddContainer.Text = "Add";
            buttonAddContainer.UseVisualStyleBackColor = false;
            buttonAddContainer.Click += ButtonAddContainer_Click;
            // 
            // listViewContainers
            // 
            listViewContainers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewContainers.Location = new Point(17, 65);
            listViewContainers.Name = "listViewContainers";
            listViewContainers.Size = new Size(428, 148);
            listViewContainers.TabIndex = 43;
            listViewContainers.UseCompatibleStateImageBehavior = false;
            // 
            // groupBoxFilters
            // 
            groupBoxFilters.Controls.Add(buttonReloadDestinationCountryShippers);
            groupBoxFilters.Controls.Add(comboBoxDestinationCountries);
            groupBoxFilters.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxFilters.ForeColor = Color.Gold;
            groupBoxFilters.Location = new Point(1223, 92);
            groupBoxFilters.Name = "groupBoxFilters";
            groupBoxFilters.Size = new Size(382, 67);
            groupBoxFilters.TabIndex = 10;
            groupBoxFilters.TabStop = false;
            groupBoxFilters.Text = "Filter shippers by destination country";
            // 
            // buttonReloadDestinationCountryShippers
            // 
            buttonReloadDestinationCountryShippers.BackColor = Color.FromArgb(60, 60, 60);
            buttonReloadDestinationCountryShippers.Cursor = Cursors.Hand;
            buttonReloadDestinationCountryShippers.FlatAppearance.BorderSize = 0;
            buttonReloadDestinationCountryShippers.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonReloadDestinationCountryShippers.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonReloadDestinationCountryShippers.FlatStyle = FlatStyle.Flat;
            buttonReloadDestinationCountryShippers.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            buttonReloadDestinationCountryShippers.ForeColor = Color.White;
            buttonReloadDestinationCountryShippers.Location = new Point(278, 26);
            buttonReloadDestinationCountryShippers.Name = "buttonReloadDestinationCountryShippers";
            buttonReloadDestinationCountryShippers.Size = new Size(70, 28);
            buttonReloadDestinationCountryShippers.TabIndex = 12;
            buttonReloadDestinationCountryShippers.Text = "Apply";
            buttonReloadDestinationCountryShippers.UseVisualStyleBackColor = false;
            buttonReloadDestinationCountryShippers.Click += ButtonReloadDestinationCountryShippers_ClickAsync;
            // 
            // comboBoxDestinationCountries
            // 
            comboBoxDestinationCountries.FormattingEnabled = true;
            comboBoxDestinationCountries.Location = new Point(19, 25);
            comboBoxDestinationCountries.Name = "comboBoxDestinationCountries";
            comboBoxDestinationCountries.Size = new Size(253, 28);
            comboBoxDestinationCountries.TabIndex = 11;
            comboBoxDestinationCountries.SelectedIndexChanged += ComboBoxDestinationCountries_SelectedIndexChanged;
            // 
            // labelChangePending
            // 
            labelChangePending.AutoSize = true;
            labelChangePending.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            labelChangePending.ForeColor = Color.White;
            labelChangePending.Location = new Point(1344, 952);
            labelChangePending.Name = "labelChangePending";
            labelChangePending.Size = new Size(261, 20);
            labelChangePending.TabIndex = 50;
            labelChangePending.Text = "A change is pending, save to update";
            labelChangePending.TextAlign = ContentAlignment.TopRight;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // buttonPrint
            // 
            buttonPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPrint.BackColor = Color.FromArgb(60, 60, 60);
            buttonPrint.Cursor = Cursors.Hand;
            buttonPrint.DialogResult = DialogResult.OK;
            buttonPrint.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonPrint.FlatAppearance.BorderSize = 0;
            buttonPrint.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonPrint.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonPrint.FlatStyle = FlatStyle.Flat;
            buttonPrint.Font = new Font("Segoe UI", 12F);
            buttonPrint.ForeColor = Color.White;
            buttonPrint.Location = new Point(1189, 1031);
            buttonPrint.Margin = new Padding(3, 4, 3, 4);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(131, 51);
            buttonPrint.TabIndex = 78;
            buttonPrint.Text = "Print";
            buttonPrint.UseVisualStyleBackColor = false;
            buttonPrint.Click += ButtonPrint_Click;
            // 
            // buttonEmail
            // 
            buttonEmail.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonEmail.BackColor = Color.FromArgb(60, 60, 60);
            buttonEmail.Cursor = Cursors.Hand;
            buttonEmail.DialogResult = DialogResult.OK;
            buttonEmail.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonEmail.FlatAppearance.BorderSize = 0;
            buttonEmail.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEmail.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEmail.FlatStyle = FlatStyle.Flat;
            buttonEmail.Font = new Font("Segoe UI", 12F);
            buttonEmail.ForeColor = Color.White;
            buttonEmail.Location = new Point(1040, 1031);
            buttonEmail.Margin = new Padding(3, 4, 3, 4);
            buttonEmail.Name = "buttonEmail";
            buttonEmail.Size = new Size(131, 51);
            buttonEmail.TabIndex = 79;
            buttonEmail.Text = "Email";
            buttonEmail.UseVisualStyleBackColor = false;
            // 
            // BolDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(1634, 1119);
            Controls.Add(buttonEmail);
            Controls.Add(buttonPrint);
            Controls.Add(labelChangePending);
            Controls.Add(groupBoxFilters);
            Controls.Add(groupBoxContainers);
            Controls.Add(groupBoxServiceTypes);
            Controls.Add(labelSpecialInstructions);
            Controls.Add(groupBoxTransitTimes);
            Controls.Add(groupBoxPayment);
            Controls.Add(groupBoxReferences);
            Controls.Add(textBoxSpecialInstructions);
            Controls.Add(groupBoxShipFrom);
            Controls.Add(groupBoxPackages);
            Controls.Add(groupBoxPallets);
            Controls.Add(groupBoxBillTo);
            Controls.Add(groupBoxShipper);
            Controls.Add(groupBoxCustomer);
            Controls.Add(labelComments);
            Controls.Add(textBoxComments);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 11F);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            Name = "BolDialog";
            Text = "DesignToolsServer - BOL Editor";
            Load += BolDialog_Load;
            Shown += BolDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxCustomer.ResumeLayout(false);
            groupBoxCustomer.PerformLayout();
            panelShipToAppointment.ResumeLayout(false);
            panelShipToAppointment.PerformLayout();
            panelShipToLocations.ResumeLayout(false);
            panelShipToLocations.PerformLayout();
            panelShipTo.ResumeLayout(false);
            panelShipTo.PerformLayout();
            groupBoxShipper.ResumeLayout(false);
            groupBoxShipper.PerformLayout();
            panelShipper.ResumeLayout(false);
            panelShipper.PerformLayout();
            groupBoxBillTo.ResumeLayout(false);
            groupBoxBillTo.PerformLayout();
            panelBillTo.ResumeLayout(false);
            panelBillTo.PerformLayout();
            groupBoxPallets.ResumeLayout(false);
            groupBoxPackages.ResumeLayout(false);
            groupBoxShipFrom.ResumeLayout(false);
            groupBoxShipFrom.PerformLayout();
            panelShipFromAppointment.ResumeLayout(false);
            panelShipFromAppointment.PerformLayout();
            panelShipFromLocations.ResumeLayout(false);
            panelShipFromLocations.PerformLayout();
            panelShipFrom.ResumeLayout(false);
            panelShipFrom.PerformLayout();
            groupBoxPayment.ResumeLayout(false);
            groupBoxPayment.PerformLayout();
            groupBoxReferences.ResumeLayout(false);
            groupBoxReferences.PerformLayout();
            groupBoxTransitTimes.ResumeLayout(false);
            groupBoxTransitTimes.PerformLayout();
            groupBoxServiceTypes.ResumeLayout(false);
            groupBoxServiceTypes.PerformLayout();
            groupBoxContainers.ResumeLayout(false);
            groupBoxFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label labelHeader;
        private PictureBox PbLogo;
        private Label labelComments;
        private TextBox textBoxComments;
        private Button OK_Button;
        private Button Cancel_Button;
        private GroupBox groupBoxCustomer;
        private ComboBox comboBoxCustomers;
        private Label labelSelectCustomer;
        private GroupBox groupBoxShipper;
        private Label labelShippers;
        private ComboBox comboBoxShippers;
        private GroupBox groupBoxBillTo;
        private Label labelBillTo;
        private ComboBox comboBox3rdPartyBillling;
        private GroupBox groupBoxPallets;
        private GroupBox groupBoxPackages;
        private GroupBox groupBoxShipFrom;
        private Label labelShipFromVendor;
        private ComboBox comboBoxVendors;
        private ListView listViewPallets;
        private ListView listViewPackages;
        private Button buttonEditPallet;
        private Button buttonRemovePallet;
        private Button buttonAddPallet;
        private Button buttonEditPackage;
        private Button buttonRemovePackage;
        private Button buttonAddPackage;
        private GroupBox groupBoxPayment;
        private GroupBox groupBoxReferences;
        private GroupBox groupBoxTransitTimes;
        private Label labelSpecialInstructions;
        private TextBox textBoxSpecialInstructions;
        private Button buttonAddShipper;
        private Button buttonAddShipTo;
        private Button buttonAddBilling;
        private Button buttonAddShipFrom;
        private DateTimePicker dateTimePickerShipDate;
        private Label labelTransitShipDate;
        private Label labelEstimatedTransitDays;
        private Label labelDeliveryDate;
        private DateTimePicker dateTimePickerDeliveryDate;
        private MaskedTextBox maskedTextBoxReferenceNumber;
        private Label labelReferenceNumber;
        private MaskedTextBox maskedTextBoxActualPrice;
        private Label labelActualPrice;
        private MaskedTextBox maskedTextBoxQuotedPrice;
        private Label labelQuotedPrice;
        private MaskedTextBox maskedTextBoxShipperQuoteNumber;
        private Label labelShipperQuoteNumber;
        private MaskedTextBox maskedTextBoxCodAmount;
        private CheckBox checkBoxPaymentCOD;
        private CheckBox checkBoxFreightPrepaid;
        private GroupBox groupBoxServiceTypes;
        private RadioButton radioButtonAir;
        private RadioButton radioButtonFTL;
        private RadioButton radioButtonLastMile;
        private RadioButton radioButtonRailroad;
        private RadioButton radioButtonOcean;
        private RadioButton radioButtonArmouredCar;
        private RadioButton radioButtonCourier;
        private TextBox textBoxEstimatedTransitDays;
        private RadioButton radioButtonShowAll;
        private GroupBox groupBoxContainers;
        private Button buttonEditContainer;
        private Button buttonRemoveContainer;
        private Button buttonAddContainer;
        private ListView listViewContainers;
        private Panel panelShipper;
        private Label labelShipperPostalCode;
        private Label labelShipperCountry;
        private Label labelShipperRegion;
        private Label labelShipperCity;
        private Label labelShipperAddress;
        private Label labelShipperName;
        private Panel panelShipTo;
        private Label labelShipToPostalCode;
        private Label labelShipToCountry;
        private Label labelShipToRegion;
        private Label labelShipToCity;
        private Label labelShipToAddress;
        private Label labelShipToName;
        private Panel panelBillTo;
        private Label labelBillToPostalCode;
        private Label labelBillToCountry;
        private Label labelBillToRegion;
        private Label labelBillToCity;
        private Label labelBillToAddress;
        private Label labelBillToName;
        private Panel panelShipFrom;
        private Label labelShipFromPostalCode;
        private Label labelShipFromCountry;
        private Label labelShipFromRegion;
        private Label labelShipFromCity;
        private Label labelShipFromAddress;
        private Label labelShipFromName;
        private CheckBox checkBoxCustomerInvoice;
        private RadioButton radioButtonLTL;
        private GroupBox groupBoxFilters;
        private ComboBox comboBoxDestinationCountries;
        private Button buttonReloadDestinationCountryShippers;
        private Label labelEstimatedBolValue;
        private TextBox textBoxBolEstimatedValue;
        private Panel panelShipFromLocations;
        private Label labelShipFromLocations;
        private ComboBox comboBoxVendorLocations;
        private Panel panelShipToLocations;
        private Label labelShipToLocation;
        private ComboBox comboBoxCustomerLocations;
        private Label labelEstimatedBolWeight;
        private TextBox textBoxEstimatedBolWeight;
        private Panel panelShipToAppointment;
        private Label labelShiptoAppointmentTime;
        private MaskedTextBox maskedTextBoxShipToAppointmentTime;
        private Label labelShipToAppointmentDAte;
        private DateTimePicker dateTimePickerShipToAppointmentDate;
        private CheckBox checkBoxShipToAppointmentRequired;
        private CheckBox checkBoxShipToLiftGateRequired;
        private Panel panelShipFromAppointment;
        private Label labelShipFromAppointmentTime;
        private MaskedTextBox maskedTextBoxShipFromAppointmentTime;
        private Label labelShipFromAppointmentDate;
        private DateTimePicker dateTimePickerShipFromAppointmentDate;
        private CheckBox checkBoxShipFromAppointmentRequired;
        private CheckBox checkBoxShipFromLiftGateRequired;
        private PictureBox pictureBoxUpdateFlag;
        private Label labelChangePending;
        private Button buttonContainersExpandCollapse;
        private Button buttonPalletsExpandCollapse;
        private Button buttonPackagesExpandCollapse;
        private TextBox textBoxActualTransitDays;
        private Label labelActualTransitTime;
        private Label labelBolTotalWeight;
        private Label labelUnitOfMeasurement;
        private MaskedTextBox maskedTextBoxOrderNumber;
        private Label labelOrderNumber;
        private CheckBox checkBoxShipToPalletJackRequired;
        private CheckBox checkBoxShipFromPalletJackRequired;
        private ErrorProvider errorProvider1;
        private Button buttonPrint;
        private TextBox textBoxBolNumber;
        private Label labelBolNumber;
        private Button buttonEmail;
    }
}
