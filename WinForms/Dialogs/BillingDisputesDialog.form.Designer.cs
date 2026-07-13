namespace SimpleBol.WinForms.Dialogs
{
    partial class BillingDisputeDialog
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
            textBoxRemarks = new TextBox();
            label1 = new Label();
            groupBoxShipperContacts = new GroupBox();
            panelContactEmailAddress = new Panel();
            labelContactEmailAddress = new Label();
            labelDisputeDate = new Label();
            dateTimePickerDisputeDate = new DateTimePicker();
            buttonAddShipperContact = new Button();
            labelShipperContact = new Label();
            comboBoxShipperContacts = new ComboBox();
            groupBoxTransitTimes = new GroupBox();
            textBoxActualTransitDays = new TextBox();
            labelActualTransitTime = new Label();
            textBoxEstimatedTransitDays = new TextBox();
            labelEstimatedTransitDays = new Label();
            labelDeliveryDate = new Label();
            dateTimePickerDeliveryDate = new DateTimePicker();
            labelTransitShipDate = new Label();
            dateTimePickerShipDate = new DateTimePicker();
            labelRemarks = new Label();
            groupBoxResolution = new GroupBox();
            labelResolutionDate = new Label();
            dateTimePickerResolutionDate = new DateTimePicker();
            labelResolution = new Label();
            textBoxResolution = new TextBox();
            checkBoxResolved = new CheckBox();
            labelCreditAmount = new Label();
            textBoxCreditedAmount = new TextBox();
            textBoxAdjustedPrice = new TextBox();
            labelDisputeSubject = new Label();
            groupBoxDispute = new GroupBox();
            textBoxDisputeSubject = new TextBox();
            labelComposeArgument = new Label();
            textBoxArgument = new TextBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            panelHeader = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            groupBoxShipper = new GroupBox();
            panelShipper = new Panel();
            labelShipperPostalCode = new Label();
            labelShipperCountry = new Label();
            labelShipperRegion = new Label();
            labelShipperCity = new Label();
            labelShipperAddress = new Label();
            labelShipperName = new Label();
            labelShippers = new Label();
            comboBoxShippers = new ComboBox();
            groupBoxReferences = new GroupBox();
            comboBoxShipperQuoteNumbers = new ComboBox();
            maskedTextBoxOrderNumber = new MaskedTextBox();
            labelOrderNumber = new Label();
            labelShipperQuoteNumber = new Label();
            maskedTextBoxReferenceNumber = new MaskedTextBox();
            labelReferenceNumber = new Label();
            maskedTextBoxActualPrice = new MaskedTextBox();
            labelActualPrice = new Label();
            maskedTextBoxQuotedPrice = new MaskedTextBox();
            labelQuotedPrice = new Label();
            errorProvider1 = new ErrorProvider(components);
            groupBoxShipperContacts.SuspendLayout();
            panelContactEmailAddress.SuspendLayout();
            groupBoxTransitTimes.SuspendLayout();
            groupBoxResolution.SuspendLayout();
            groupBoxDispute.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxShipper.SuspendLayout();
            panelShipper.SuspendLayout();
            groupBoxReferences.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // textBoxRemarks
            // 
            textBoxRemarks.Location = new Point(26, 560);
            textBoxRemarks.Multiline = true;
            textBoxRemarks.Name = "textBoxRemarks";
            textBoxRemarks.Size = new Size(732, 66);
            textBoxRemarks.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(28, 36);
            label1.Name = "label1";
            label1.Size = new Size(104, 20);
            label1.TabIndex = 6;
            label1.Text = "Adjusted Price";
            // 
            // groupBoxShipperContacts
            // 
            groupBoxShipperContacts.Controls.Add(panelContactEmailAddress);
            groupBoxShipperContacts.Controls.Add(labelDisputeDate);
            groupBoxShipperContacts.Controls.Add(dateTimePickerDisputeDate);
            groupBoxShipperContacts.Controls.Add(buttonAddShipperContact);
            groupBoxShipperContacts.Controls.Add(labelShipperContact);
            groupBoxShipperContacts.Controls.Add(comboBoxShipperContacts);
            groupBoxShipperContacts.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxShipperContacts.ForeColor = Color.White;
            groupBoxShipperContacts.Location = new Point(420, 101);
            groupBoxShipperContacts.Name = "groupBoxShipperContacts";
            groupBoxShipperContacts.Size = new Size(382, 259);
            groupBoxShipperContacts.TabIndex = 3;
            groupBoxShipperContacts.TabStop = false;
            groupBoxShipperContacts.Text = "Shipper Contacts";
            // 
            // panelContactEmailAddress
            // 
            panelContactEmailAddress.Controls.Add(labelContactEmailAddress);
            panelContactEmailAddress.Location = new Point(19, 99);
            panelContactEmailAddress.Name = "panelContactEmailAddress";
            panelContactEmailAddress.Size = new Size(339, 51);
            panelContactEmailAddress.TabIndex = 73;
            // 
            // labelContactEmailAddress
            // 
            labelContactEmailAddress.AutoSize = true;
            labelContactEmailAddress.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelContactEmailAddress.ImeMode = ImeMode.NoControl;
            labelContactEmailAddress.Location = new Point(12, 15);
            labelContactEmailAddress.Name = "labelContactEmailAddress";
            labelContactEmailAddress.Size = new Size(160, 20);
            labelContactEmailAddress.TabIndex = 0;
            labelContactEmailAddress.Text = "Pending Email Address";
            // 
            // labelDisputeDate
            // 
            labelDisputeDate.AutoSize = true;
            labelDisputeDate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelDisputeDate.ForeColor = Color.White;
            labelDisputeDate.ImeMode = ImeMode.NoControl;
            labelDisputeDate.Location = new Point(19, 160);
            labelDisputeDate.Name = "labelDisputeDate";
            labelDisputeDate.Size = new Size(96, 20);
            labelDisputeDate.TabIndex = 71;
            labelDisputeDate.Text = "Dispute Date";
            // 
            // dateTimePickerDisputeDate
            // 
            dateTimePickerDisputeDate.Location = new Point(14, 189);
            dateTimePickerDisputeDate.Name = "dateTimePickerDisputeDate";
            dateTimePickerDisputeDate.Size = new Size(343, 27);
            dateTimePickerDisputeDate.TabIndex = 6;
            // 
            // buttonAddShipperContact
            // 
            buttonAddShipperContact.BackColor = Color.SaddleBrown;
            buttonAddShipperContact.Cursor = Cursors.Hand;
            buttonAddShipperContact.FlatAppearance.BorderSize = 0;
            buttonAddShipperContact.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonAddShipperContact.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonAddShipperContact.FlatStyle = FlatStyle.Flat;
            buttonAddShipperContact.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddShipperContact.ImeMode = ImeMode.NoControl;
            buttonAddShipperContact.Location = new Point(329, 28);
            buttonAddShipperContact.Name = "buttonAddShipperContact";
            buttonAddShipperContact.Size = new Size(29, 28);
            buttonAddShipperContact.TabIndex = 4;
            buttonAddShipperContact.Text = "+";
            buttonAddShipperContact.UseVisualStyleBackColor = false;
            buttonAddShipperContact.Click += ButtonAddShipperContact_Click;
            // 
            // labelShipperContact
            // 
            labelShipperContact.AutoSize = true;
            labelShipperContact.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperContact.ForeColor = Color.White;
            labelShipperContact.ImeMode = ImeMode.NoControl;
            labelShipperContact.Location = new Point(19, 32);
            labelShipperContact.Name = "labelShipperContact";
            labelShipperContact.Size = new Size(171, 20);
            labelShipperContact.TabIndex = 16;
            labelShipperContact.Text = "Select Contact - Send To";
            // 
            // comboBoxShipperContacts
            // 
            comboBoxShipperContacts.FormattingEnabled = true;
            comboBoxShipperContacts.Location = new Point(18, 61);
            comboBoxShipperContacts.Name = "comboBoxShipperContacts";
            comboBoxShipperContacts.Size = new Size(336, 28);
            comboBoxShipperContacts.TabIndex = 5;
            comboBoxShipperContacts.SelectedIndexChanged += ComboBoxShipperContacts_SelectedIndexChanged;
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
            groupBoxTransitTimes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxTransitTimes.ForeColor = Color.Thistle;
            groupBoxTransitTimes.Location = new Point(1223, 101);
            groupBoxTransitTimes.Name = "groupBoxTransitTimes";
            groupBoxTransitTimes.Size = new Size(382, 259);
            groupBoxTransitTimes.TabIndex = 13;
            groupBoxTransitTimes.TabStop = false;
            groupBoxTransitTimes.Text = "Transit ";
            // 
            // textBoxActualTransitDays
            // 
            textBoxActualTransitDays.Location = new Point(204, 191);
            textBoxActualTransitDays.Name = "textBoxActualTransitDays";
            textBoxActualTransitDays.Size = new Size(157, 27);
            textBoxActualTransitDays.TabIndex = 17;
            textBoxActualTransitDays.Enter += TextBoxControl_Enter;
            textBoxActualTransitDays.KeyPress += TextBoxActualTransitDays_KeyPress;
            // 
            // labelActualTransitTime
            // 
            labelActualTransitTime.AutoSize = true;
            labelActualTransitTime.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelActualTransitTime.ForeColor = Color.White;
            labelActualTransitTime.ImeMode = ImeMode.NoControl;
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
            textBoxEstimatedTransitDays.TabIndex = 16;
            textBoxEstimatedTransitDays.Enter += TextBoxControl_Enter;
            textBoxEstimatedTransitDays.KeyPress += TextBoxEstimatedTransitDays_KeyPress;
            // 
            // labelEstimatedTransitDays
            // 
            labelEstimatedTransitDays.AutoSize = true;
            labelEstimatedTransitDays.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelEstimatedTransitDays.ForeColor = Color.White;
            labelEstimatedTransitDays.ImeMode = ImeMode.NoControl;
            labelEstimatedTransitDays.Location = new Point(23, 163);
            labelEstimatedTransitDays.Name = "labelEstimatedTransitDays";
            labelEstimatedTransitDays.Size = new Size(111, 20);
            labelEstimatedTransitDays.TabIndex = 4;
            labelEstimatedTransitDays.Text = "Est Transit Days";
            // 
            // labelDeliveryDate
            // 
            labelDeliveryDate.AutoSize = true;
            labelDeliveryDate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelDeliveryDate.ForeColor = Color.White;
            labelDeliveryDate.ImeMode = ImeMode.NoControl;
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
            dateTimePickerDeliveryDate.TabIndex = 15;
            dateTimePickerDeliveryDate.ValueChanged += DateTimePickerDeliveryDate_ValueChanged;
            // 
            // labelTransitShipDate
            // 
            labelTransitShipDate.AutoSize = true;
            labelTransitShipDate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelTransitShipDate.ForeColor = Color.White;
            labelTransitShipDate.ImeMode = ImeMode.NoControl;
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
            dateTimePickerShipDate.TabIndex = 14;
            dateTimePickerShipDate.ValueChanged += DateTimePickerShipDate_ValueChanged;
            // 
            // labelRemarks
            // 
            labelRemarks.AutoSize = true;
            labelRemarks.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRemarks.ForeColor = Color.White;
            labelRemarks.ImeMode = ImeMode.NoControl;
            labelRemarks.Location = new Point(26, 523);
            labelRemarks.Name = "labelRemarks";
            labelRemarks.Size = new Size(289, 20);
            labelRemarks.TabIndex = 6;
            labelRemarks.Text = "Private Remarks - Your thoughts about this";
            // 
            // groupBoxResolution
            // 
            groupBoxResolution.Controls.Add(labelResolutionDate);
            groupBoxResolution.Controls.Add(dateTimePickerResolutionDate);
            groupBoxResolution.Controls.Add(labelResolution);
            groupBoxResolution.Controls.Add(textBoxResolution);
            groupBoxResolution.Controls.Add(checkBoxResolved);
            groupBoxResolution.Controls.Add(labelCreditAmount);
            groupBoxResolution.Controls.Add(textBoxCreditedAmount);
            groupBoxResolution.Controls.Add(label1);
            groupBoxResolution.Controls.Add(textBoxAdjustedPrice);
            groupBoxResolution.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxResolution.ForeColor = Color.White;
            groupBoxResolution.Location = new Point(821, 375);
            groupBoxResolution.Name = "groupBoxResolution";
            groupBoxResolution.Size = new Size(784, 645);
            groupBoxResolution.TabIndex = 22;
            groupBoxResolution.TabStop = false;
            groupBoxResolution.Text = "Billing Resolution";
            // 
            // labelResolutionDate
            // 
            labelResolutionDate.AutoSize = true;
            labelResolutionDate.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelResolutionDate.ForeColor = Color.White;
            labelResolutionDate.ImeMode = ImeMode.NoControl;
            labelResolutionDate.Location = new Point(420, 36);
            labelResolutionDate.Name = "labelResolutionDate";
            labelResolutionDate.Size = new Size(115, 20);
            labelResolutionDate.TabIndex = 73;
            labelResolutionDate.Text = "Resolution Date";
            // 
            // dateTimePickerResolutionDate
            // 
            dateTimePickerResolutionDate.Location = new Point(415, 65);
            dateTimePickerResolutionDate.Name = "dateTimePickerResolutionDate";
            dateTimePickerResolutionDate.Size = new Size(343, 27);
            dateTimePickerResolutionDate.TabIndex = 25;
            // 
            // labelResolution
            // 
            labelResolution.AutoSize = true;
            labelResolution.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelResolution.ForeColor = Color.White;
            labelResolution.ImeMode = ImeMode.NoControl;
            labelResolution.Location = new Point(23, 120);
            labelResolution.Name = "labelResolution";
            labelResolution.Size = new Size(302, 20);
            labelResolution.TabIndex = 11;
            labelResolution.Text = "Resolution - How was the litigation resolved";
            // 
            // textBoxResolution
            // 
            textBoxResolution.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxResolution.Location = new Point(27, 155);
            textBoxResolution.Multiline = true;
            textBoxResolution.Name = "textBoxResolution";
            textBoxResolution.Size = new Size(732, 471);
            textBoxResolution.TabIndex = 27;
            textBoxResolution.Enter += TextBoxControl_Enter;
            // 
            // checkBoxResolved
            // 
            checkBoxResolved.AutoSize = true;
            checkBoxResolved.ImeMode = ImeMode.NoControl;
            checkBoxResolved.Location = new Point(420, 116);
            checkBoxResolved.Name = "checkBoxResolved";
            checkBoxResolved.Size = new Size(211, 24);
            checkBoxResolved.TabIndex = 26;
            checkBoxResolved.Text = "Dispute has been resolved";
            checkBoxResolved.UseVisualStyleBackColor = true;
            // 
            // labelCreditAmount
            // 
            labelCreditAmount.AutoSize = true;
            labelCreditAmount.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCreditAmount.ForeColor = Color.White;
            labelCreditAmount.ImeMode = ImeMode.NoControl;
            labelCreditAmount.Location = new Point(209, 36);
            labelCreditAmount.Name = "labelCreditAmount";
            labelCreditAmount.Size = new Size(123, 20);
            labelCreditAmount.TabIndex = 8;
            labelCreditAmount.Text = "Credited Amount";
            // 
            // textBoxCreditedAmount
            // 
            textBoxCreditedAmount.Location = new Point(204, 73);
            textBoxCreditedAmount.Name = "textBoxCreditedAmount";
            textBoxCreditedAmount.Size = new Size(152, 27);
            textBoxCreditedAmount.TabIndex = 24;
            textBoxCreditedAmount.Enter += TextBoxControl_Enter;
            textBoxCreditedAmount.Validating += TextBoxCurrencyAmount_Validating;
            // 
            // textBoxAdjustedPrice
            // 
            textBoxAdjustedPrice.Location = new Point(23, 73);
            textBoxAdjustedPrice.Name = "textBoxAdjustedPrice";
            textBoxAdjustedPrice.Size = new Size(152, 27);
            textBoxAdjustedPrice.TabIndex = 23;
            textBoxAdjustedPrice.Enter += TextBoxControl_Enter;
            textBoxAdjustedPrice.Leave += TextBoxAdjustedPrice_Leave;
            textBoxAdjustedPrice.Validating += TextBoxCurrencyAmount_Validating;
            // 
            // labelDisputeSubject
            // 
            labelDisputeSubject.AutoSize = true;
            labelDisputeSubject.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelDisputeSubject.ForeColor = Color.White;
            labelDisputeSubject.ImeMode = ImeMode.NoControl;
            labelDisputeSubject.Location = new Point(22, 36);
            labelDisputeSubject.Name = "labelDisputeSubject";
            labelDisputeSubject.Size = new Size(156, 20);
            labelDisputeSubject.TabIndex = 4;
            labelDisputeSubject.Text = "Compose your subject";
            // 
            // groupBoxDispute
            // 
            groupBoxDispute.Controls.Add(labelRemarks);
            groupBoxDispute.Controls.Add(textBoxRemarks);
            groupBoxDispute.Controls.Add(labelDisputeSubject);
            groupBoxDispute.Controls.Add(textBoxDisputeSubject);
            groupBoxDispute.Controls.Add(labelComposeArgument);
            groupBoxDispute.Controls.Add(textBoxArgument);
            groupBoxDispute.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxDispute.ForeColor = Color.White;
            groupBoxDispute.Location = new Point(20, 375);
            groupBoxDispute.Name = "groupBoxDispute";
            groupBoxDispute.Size = new Size(782, 645);
            groupBoxDispute.TabIndex = 18;
            groupBoxDispute.TabStop = false;
            groupBoxDispute.Text = "Billing Dispute";
            // 
            // textBoxDisputeSubject
            // 
            textBoxDisputeSubject.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxDisputeSubject.Location = new Point(22, 73);
            textBoxDisputeSubject.Name = "textBoxDisputeSubject";
            textBoxDisputeSubject.Size = new Size(732, 29);
            textBoxDisputeSubject.TabIndex = 19;
            textBoxDisputeSubject.Enter += TextBoxControl_Enter;
            // 
            // labelComposeArgument
            // 
            labelComposeArgument.AutoSize = true;
            labelComposeArgument.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelComposeArgument.ForeColor = Color.White;
            labelComposeArgument.ImeMode = ImeMode.NoControl;
            labelComposeArgument.Location = new Point(22, 120);
            labelComposeArgument.Name = "labelComposeArgument";
            labelComposeArgument.Size = new Size(297, 20);
            labelComposeArgument.TabIndex = 2;
            labelComposeArgument.Text = "Compose your argument or begin litigation";
            // 
            // textBoxArgument
            // 
            textBoxArgument.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxArgument.Location = new Point(26, 155);
            textBoxArgument.Multiline = true;
            textBoxArgument.Name = "textBoxArgument";
            textBoxArgument.Size = new Size(732, 350);
            textBoxArgument.TabIndex = 20;
            textBoxArgument.Enter += TextBoxControl_Enter;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.Cursor = Cursors.Hand;
            OK_Button.DialogResult = DialogResult.OK;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OK_Button.ForeColor = Color.White;
            OK_Button.ImeMode = ImeMode.NoControl;
            OK_Button.Location = new Point(1364, 1046);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 28;
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
            Cancel_Button.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Cancel_Button.ForeColor = Color.White;
            Cancel_Button.ImeMode = ImeMode.NoControl;
            Cancel_Button.Location = new Point(1490, 1046);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 29;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(38, 38, 38);
            panelHeader.Controls.Add(Lbl_Header);
            panelHeader.Controls.Add(PbLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 7, 3, 7);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1634, 70);
            panelHeader.TabIndex = 55;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.ImeMode = ImeMode.NoControl;
            Lbl_Header.Location = new Point(120, 17);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(318, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Billing Dispute Editor";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = Properties.Resources.badgeBolDispute150;
            PbLogo.ImeMode = ImeMode.NoControl;
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 7, 3, 7);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // groupBoxShipper
            // 
            groupBoxShipper.Controls.Add(panelShipper);
            groupBoxShipper.Controls.Add(labelShippers);
            groupBoxShipper.Controls.Add(comboBoxShippers);
            groupBoxShipper.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxShipper.ForeColor = Color.AntiqueWhite;
            groupBoxShipper.Location = new Point(20, 101);
            groupBoxShipper.Name = "groupBoxShipper";
            groupBoxShipper.Size = new Size(382, 259);
            groupBoxShipper.TabIndex = 1;
            groupBoxShipper.TabStop = false;
            groupBoxShipper.Text = "Select a Shipper";
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
            panelShipper.Size = new Size(342, 143);
            panelShipper.TabIndex = 30;
            // 
            // labelShipperPostalCode
            // 
            labelShipperPostalCode.AutoSize = true;
            labelShipperPostalCode.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperPostalCode.ImeMode = ImeMode.NoControl;
            labelShipperPostalCode.Location = new Point(18, 91);
            labelShipperPostalCode.Name = "labelShipperPostalCode";
            labelShipperPostalCode.Size = new Size(48, 20);
            labelShipperPostalCode.TabIndex = 13;
            labelShipperPostalCode.Text = "Postal";
            // 
            // labelShipperCountry
            // 
            labelShipperCountry.AutoSize = true;
            labelShipperCountry.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperCountry.ImeMode = ImeMode.NoControl;
            labelShipperCountry.Location = new Point(185, 91);
            labelShipperCountry.Name = "labelShipperCountry";
            labelShipperCountry.Size = new Size(95, 20);
            labelShipperCountry.TabIndex = 12;
            labelShipperCountry.Text = "CountryCode";
            // 
            // labelShipperRegion
            // 
            labelShipperRegion.AutoSize = true;
            labelShipperRegion.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperRegion.ImeMode = ImeMode.NoControl;
            labelShipperRegion.Location = new Point(185, 61);
            labelShipperRegion.Name = "labelShipperRegion";
            labelShipperRegion.Size = new Size(91, 20);
            labelShipperRegion.TabIndex = 11;
            labelShipperRegion.Text = "RegionCode";
            // 
            // labelShipperCity
            // 
            labelShipperCity.AutoSize = true;
            labelShipperCity.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperCity.ImeMode = ImeMode.NoControl;
            labelShipperCity.Location = new Point(18, 61);
            labelShipperCity.Name = "labelShipperCity";
            labelShipperCity.Size = new Size(34, 20);
            labelShipperCity.TabIndex = 10;
            labelShipperCity.Text = "City";
            // 
            // labelShipperAddress
            // 
            labelShipperAddress.AutoSize = true;
            labelShipperAddress.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperAddress.ImeMode = ImeMode.NoControl;
            labelShipperAddress.Location = new Point(18, 35);
            labelShipperAddress.Name = "labelShipperAddress";
            labelShipperAddress.Size = new Size(62, 20);
            labelShipperAddress.TabIndex = 9;
            labelShipperAddress.Text = "Address";
            // 
            // labelShipperName
            // 
            labelShipperName.AutoSize = true;
            labelShipperName.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperName.ImeMode = ImeMode.NoControl;
            labelShipperName.Location = new Point(18, 9);
            labelShipperName.Name = "labelShipperName";
            labelShipperName.Size = new Size(49, 20);
            labelShipperName.TabIndex = 8;
            labelShipperName.Text = "Name";
            // 
            // labelShippers
            // 
            labelShippers.AutoSize = true;
            labelShippers.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShippers.ForeColor = Color.White;
            labelShippers.ImeMode = ImeMode.NoControl;
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
            comboBoxShippers.Size = new Size(340, 28);
            comboBoxShippers.TabIndex = 2;
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            // 
            // groupBoxReferences
            // 
            groupBoxReferences.Controls.Add(comboBoxShipperQuoteNumbers);
            groupBoxReferences.Controls.Add(maskedTextBoxOrderNumber);
            groupBoxReferences.Controls.Add(labelOrderNumber);
            groupBoxReferences.Controls.Add(labelShipperQuoteNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxReferenceNumber);
            groupBoxReferences.Controls.Add(labelReferenceNumber);
            groupBoxReferences.Controls.Add(maskedTextBoxActualPrice);
            groupBoxReferences.Controls.Add(labelActualPrice);
            groupBoxReferences.Controls.Add(maskedTextBoxQuotedPrice);
            groupBoxReferences.Controls.Add(labelQuotedPrice);
            groupBoxReferences.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxReferences.ForeColor = Color.Thistle;
            groupBoxReferences.Location = new Point(821, 101);
            groupBoxReferences.Name = "groupBoxReferences";
            groupBoxReferences.Size = new Size(382, 259);
            groupBoxReferences.TabIndex = 7;
            groupBoxReferences.TabStop = false;
            groupBoxReferences.Text = "References";
            // 
            // comboBoxShipperQuoteNumbers
            // 
            comboBoxShipperQuoteNumbers.FormattingEnabled = true;
            comboBoxShipperQuoteNumbers.Location = new Point(23, 62);
            comboBoxShipperQuoteNumbers.Name = "comboBoxShipperQuoteNumbers";
            comboBoxShipperQuoteNumbers.Size = new Size(340, 28);
            comboBoxShipperQuoteNumbers.TabIndex = 8;
            comboBoxShipperQuoteNumbers.SelectedIndexChanged += ComboBoxShipperQuoteNumbers_SelectedIndexChanged;
            // 
            // maskedTextBoxOrderNumber
            // 
            maskedTextBoxOrderNumber.Location = new Point(209, 183);
            maskedTextBoxOrderNumber.Name = "maskedTextBoxOrderNumber";
            maskedTextBoxOrderNumber.Size = new Size(152, 27);
            maskedTextBoxOrderNumber.TabIndex = 12;
            maskedTextBoxOrderNumber.Enter += MaskedTextBoxControl_Enter;
            // 
            // labelOrderNumber
            // 
            labelOrderNumber.AutoSize = true;
            labelOrderNumber.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelOrderNumber.ForeColor = Color.White;
            labelOrderNumber.ImeMode = ImeMode.NoControl;
            labelOrderNumber.Location = new Point(209, 160);
            labelOrderNumber.Name = "labelOrderNumber";
            labelOrderNumber.Size = new Size(138, 20);
            labelOrderNumber.TabIndex = 8;
            labelOrderNumber.Text = "Your Order Number";
            // 
            // labelShipperQuoteNumber
            // 
            labelShipperQuoteNumber.AutoSize = true;
            labelShipperQuoteNumber.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelShipperQuoteNumber.ForeColor = Color.White;
            labelShipperQuoteNumber.ImeMode = ImeMode.NoControl;
            labelShipperQuoteNumber.Location = new Point(28, 32);
            labelShipperQuoteNumber.Name = "labelShipperQuoteNumber";
            labelShipperQuoteNumber.Size = new Size(163, 20);
            labelShipperQuoteNumber.TabIndex = 6;
            labelShipperQuoteNumber.Text = "Shipper Quote Number";
            // 
            // maskedTextBoxReferenceNumber
            // 
            maskedTextBoxReferenceNumber.Location = new Point(23, 185);
            maskedTextBoxReferenceNumber.Name = "maskedTextBoxReferenceNumber";
            maskedTextBoxReferenceNumber.Size = new Size(152, 27);
            maskedTextBoxReferenceNumber.TabIndex = 11;
            maskedTextBoxReferenceNumber.Enter += MaskedTextBoxControl_Enter;
            // 
            // labelReferenceNumber
            // 
            labelReferenceNumber.AutoSize = true;
            labelReferenceNumber.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelReferenceNumber.ForeColor = Color.White;
            labelReferenceNumber.ImeMode = ImeMode.NoControl;
            labelReferenceNumber.Location = new Point(28, 160);
            labelReferenceNumber.Name = "labelReferenceNumber";
            labelReferenceNumber.Size = new Size(133, 20);
            labelReferenceNumber.TabIndex = 4;
            labelReferenceNumber.Text = "Reference Number";
            // 
            // maskedTextBoxActualPrice
            // 
            maskedTextBoxActualPrice.Location = new Point(209, 123);
            maskedTextBoxActualPrice.Name = "maskedTextBoxActualPrice";
            maskedTextBoxActualPrice.Size = new Size(152, 27);
            maskedTextBoxActualPrice.TabIndex = 10;
            maskedTextBoxActualPrice.Enter += MaskedTextBoxControl_Enter;
            maskedTextBoxActualPrice.Validating += MaskedTextBoxCurrencyAmount_Validating;
            // 
            // labelActualPrice
            // 
            labelActualPrice.AutoSize = true;
            labelActualPrice.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelActualPrice.ForeColor = Color.White;
            labelActualPrice.ImeMode = ImeMode.NoControl;
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
            maskedTextBoxQuotedPrice.TabIndex = 9;
            maskedTextBoxQuotedPrice.Enter += MaskedTextBoxControl_Enter;
            maskedTextBoxQuotedPrice.Validating += MaskedTextBoxCurrencyAmount_Validating;
            // 
            // labelQuotedPrice
            // 
            labelQuotedPrice.AutoSize = true;
            labelQuotedPrice.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelQuotedPrice.ForeColor = Color.White;
            labelQuotedPrice.ImeMode = ImeMode.NoControl;
            labelQuotedPrice.Location = new Point(28, 97);
            labelQuotedPrice.Name = "labelQuotedPrice";
            labelQuotedPrice.Size = new Size(95, 20);
            labelQuotedPrice.TabIndex = 0;
            labelQuotedPrice.Text = "Quoted Price";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // BillingDisputeDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            CancelButton = Cancel_Button;
            ClientSize = new Size(1634, 1126);
            Controls.Add(groupBoxShipperContacts);
            Controls.Add(groupBoxTransitTimes);
            Controls.Add(groupBoxResolution);
            Controls.Add(groupBoxDispute);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panelHeader);
            Controls.Add(groupBoxShipper);
            Controls.Add(groupBoxReferences);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "BillingDisputeDialog";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "BillingDisputeDialog";
            Load += BillingDisputesDialog_Load;
            Shown += BillingDisputesDialog_Shown;
            groupBoxShipperContacts.ResumeLayout(false);
            groupBoxShipperContacts.PerformLayout();
            panelContactEmailAddress.ResumeLayout(false);
            panelContactEmailAddress.PerformLayout();
            groupBoxTransitTimes.ResumeLayout(false);
            groupBoxTransitTimes.PerformLayout();
            groupBoxResolution.ResumeLayout(false);
            groupBoxResolution.PerformLayout();
            groupBoxDispute.ResumeLayout(false);
            groupBoxDispute.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxShipper.ResumeLayout(false);
            groupBoxShipper.PerformLayout();
            panelShipper.ResumeLayout(false);
            panelShipper.PerformLayout();
            groupBoxReferences.ResumeLayout(false);
            groupBoxReferences.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxRemarks;
        private Label label1;
        private GroupBox groupBoxShipperContacts;
        private Panel panelContactEmailAddress;
        private Label labelContactEmailAddress;
        private Label labelDisputeDate;
        private DateTimePicker dateTimePickerDisputeDate;
        private Button buttonAddShipperContact;
        private Label labelShipperContact;
        private ComboBox comboBoxShipperContacts;
        private GroupBox groupBoxTransitTimes;
        private TextBox textBoxActualTransitDays;
        private Label labelActualTransitTime;
        private TextBox textBoxEstimatedTransitDays;
        private Label labelEstimatedTransitDays;
        private Label labelDeliveryDate;
        private DateTimePicker dateTimePickerDeliveryDate;
        private Label labelTransitShipDate;
        private DateTimePicker dateTimePickerShipDate;
        private Label labelRemarks;
        private GroupBox groupBoxResolution;
        private Label labelResolutionDate;
        private DateTimePicker dateTimePickerResolutionDate;
        private Label labelResolution;
        private TextBox textBoxResolution;
        private CheckBox checkBoxResolved;
        private Label labelCreditAmount;
        private TextBox textBoxCreditedAmount;
        private TextBox textBoxAdjustedPrice;
        private Label labelDisputeSubject;
        private GroupBox groupBoxDispute;
        private TextBox textBoxDisputeSubject;
        private Label labelComposeArgument;
        private TextBox textBoxArgument;
        private Button OK_Button;
        private Button Cancel_Button;
        private Panel panelHeader;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private GroupBox groupBoxShipper;
        private Panel panelShipper;
        private Label labelShipperPostalCode;
        private Label labelShipperCountry;
        private Label labelShipperRegion;
        private Label labelShipperCity;
        private Label labelShipperAddress;
        private Label labelShipperName;
        private Label labelShippers;
        private ComboBox comboBoxShippers;
        private GroupBox groupBoxReferences;
        private MaskedTextBox maskedTextBoxOrderNumber;
        private Label labelOrderNumber;
        private Label labelShipperQuoteNumber;
        private MaskedTextBox maskedTextBoxReferenceNumber;
        private Label labelReferenceNumber;
        private MaskedTextBox maskedTextBoxActualPrice;
        private Label labelActualPrice;
        private Label labelQuotedPrice;
        private ComboBox comboBoxShipperQuoteNumbers;
        private ErrorProvider errorProvider1;
        private MaskedTextBox maskedTextBoxQuotedPrice;
    }
}