namespace SimpleBol.WinForms.Dialogs
{
    partial class ShipperDialog
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
            panelHeader = new Panel();
            pictureBoxUpdateFlag = new PictureBox();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBoxCompanyInfo = new GroupBox();
            maskedTextBoxPostalCode = new MaskedTextBox();
            labelPostalCode = new Label();
            labelRegion = new Label();
            comboBoxRegion = new ComboBox();
            labelCountry = new Label();
            comboBoxCountry = new ComboBox();
            textBoxCity = new TextBox();
            labelCity = new Label();
            textBoxAddress2 = new TextBox();
            labelAddress2 = new Label();
            textBoxAddress1 = new TextBox();
            labelAddress1 = new Label();
            textBoxCompanyName = new TextBox();
            labelCompanyName = new Label();
            groupBoxServicesOffered = new GroupBox();
            checkBoxFTL = new CheckBox();
            checkBoxArmouredCar = new CheckBox();
            checkBoxCourier = new CheckBox();
            checkBoxTrackingServices = new CheckBox();
            checkBoxLastMile = new CheckBox();
            checkBoxElectronicQuotes = new CheckBox();
            checkBoxFavorite = new CheckBox();
            checkBoxAirplane = new CheckBox();
            checkBoxRailroad = new CheckBox();
            checkBoxOcean = new CheckBox();
            checkBoxLTL = new CheckBox();
            checkBoxLiftgate = new CheckBox();
            groupBoxMainContact = new GroupBox();
            maskedTextBoxEmailAddress2 = new MaskedTextBox();
            maskedTextBoxEmailAddress1 = new MaskedTextBox();
            maskedTextBoxPhoneNumber2 = new MaskedTextBox();
            maskedTextBoxPhoneNumber1 = new MaskedTextBox();
            labelEmailAddress2 = new Label();
            labelPhoneNumber2 = new Label();
            labelEmailAddress1 = new Label();
            labelPhoneNumber1 = new Label();
            groupBoxComments = new GroupBox();
            buttonEditContact = new Button();
            buttonRemoveContact = new Button();
            buttonAddContact = new Button();
            listViewShipperContacts = new ListView();
            groupBoxCountries = new GroupBox();
            buttonCountryRemove = new Button();
            buttonCountryAdd = new Button();
            comboBoxCountriesSource = new ComboBox();
            listViewCountriesServiced = new ListView();
            textBoxComments = new TextBox();
            labelComments = new Label();
            labelLocationChangePending = new Label();
            errorProvider1 = new ErrorProvider(components);
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxCompanyInfo.SuspendLayout();
            groupBoxServicesOffered.SuspendLayout();
            groupBoxMainContact.SuspendLayout();
            groupBoxComments.SuspendLayout();
            groupBoxCountries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(38, 38, 38);
            panelHeader.Controls.Add(pictureBoxUpdateFlag);
            panelHeader.Controls.Add(Lbl_Header);
            panelHeader.Controls.Add(PbLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1280, 70);
            panelHeader.TabIndex = 2;
            // 
            // pictureBoxUpdateFlag
            // 
            pictureBoxUpdateFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.InitialImage = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.Location = new Point(1204, 13);
            pictureBoxUpdateFlag.Name = "pictureBoxUpdateFlag";
            pictureBoxUpdateFlag.Size = new Size(45, 45);
            pictureBoxUpdateFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUpdateFlag.TabIndex = 3;
            pictureBoxUpdateFlag.TabStop = false;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(129, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Shipper";
            // 
            // PbLogo
            // 
            PbLogo.Image = Properties.Resources.badgeShippers150;
            PbLogo.InitialImage = Properties.Resources.Email2Icon;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
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
            OK_Button.Location = new Point(1013, 747);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 37;
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
            Cancel_Button.Location = new Point(1134, 747);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 38;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // groupBoxCompanyInfo
            // 
            groupBoxCompanyInfo.Controls.Add(maskedTextBoxPostalCode);
            groupBoxCompanyInfo.Controls.Add(labelPostalCode);
            groupBoxCompanyInfo.Controls.Add(labelRegion);
            groupBoxCompanyInfo.Controls.Add(comboBoxRegion);
            groupBoxCompanyInfo.Controls.Add(labelCountry);
            groupBoxCompanyInfo.Controls.Add(comboBoxCountry);
            groupBoxCompanyInfo.Controls.Add(textBoxCity);
            groupBoxCompanyInfo.Controls.Add(labelCity);
            groupBoxCompanyInfo.Controls.Add(textBoxAddress2);
            groupBoxCompanyInfo.Controls.Add(labelAddress2);
            groupBoxCompanyInfo.Controls.Add(textBoxAddress1);
            groupBoxCompanyInfo.Controls.Add(labelAddress1);
            groupBoxCompanyInfo.Controls.Add(textBoxCompanyName);
            groupBoxCompanyInfo.Controls.Add(labelCompanyName);
            groupBoxCompanyInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxCompanyInfo.ForeColor = Color.White;
            groupBoxCompanyInfo.Location = new Point(21, 81);
            groupBoxCompanyInfo.Name = "groupBoxCompanyInfo";
            groupBoxCompanyInfo.Size = new Size(375, 593);
            groupBoxCompanyInfo.TabIndex = 0;
            groupBoxCompanyInfo.TabStop = false;
            groupBoxCompanyInfo.Text = "Company Information";
            // 
            // maskedTextBoxPostalCode
            // 
            maskedTextBoxPostalCode.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPostalCode.Location = new Point(23, 343);
            maskedTextBoxPostalCode.Mask = "00000-9999";
            maskedTextBoxPostalCode.Name = "maskedTextBoxPostalCode";
            maskedTextBoxPostalCode.Size = new Size(322, 29);
            maskedTextBoxPostalCode.TabIndex = 5;
            maskedTextBoxPostalCode.Enter += MaskedTextBoxPostalCode_Enter;
            // 
            // labelPostalCode
            // 
            labelPostalCode.AutoSize = true;
            labelPostalCode.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPostalCode.ForeColor = Color.White;
            labelPostalCode.Location = new Point(23, 319);
            labelPostalCode.Name = "labelPostalCode";
            labelPostalCode.Size = new Size(157, 20);
            labelPostalCode.TabIndex = 25;
            labelPostalCode.Text = "Postal Code (required)";
            // 
            // labelRegion
            // 
            labelRegion.AutoSize = true;
            labelRegion.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRegion.ForeColor = Color.White;
            labelRegion.Location = new Point(25, 465);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(161, 20);
            labelRegion.TabIndex = 24;
            labelRegion.Text = "RegionCode (required)";
            // 
            // comboBoxRegion
            // 
            comboBoxRegion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxRegion.FormattingEnabled = true;
            comboBoxRegion.Location = new Point(26, 493);
            comboBoxRegion.Name = "comboBoxRegion";
            comboBoxRegion.Size = new Size(319, 29);
            comboBoxRegion.TabIndex = 7;
            // 
            // labelCountry
            // 
            labelCountry.AutoSize = true;
            labelCountry.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCountry.ForeColor = Color.White;
            labelCountry.Location = new Point(24, 394);
            labelCountry.Name = "labelCountry";
            labelCountry.Size = new Size(165, 20);
            labelCountry.TabIndex = 22;
            labelCountry.Text = "CountryCode (required)";
            // 
            // comboBoxCountry
            // 
            comboBoxCountry.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxCountry.FormattingEnabled = true;
            comboBoxCountry.Location = new Point(25, 422);
            comboBoxCountry.Name = "comboBoxCountry";
            comboBoxCountry.Size = new Size(319, 29);
            comboBoxCountry.TabIndex = 6;
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
            // 
            // textBoxCity
            // 
            textBoxCity.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxCity.Location = new Point(24, 267);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(320, 29);
            textBoxCity.TabIndex = 4;
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCity.ForeColor = Color.White;
            labelCity.Location = new Point(24, 241);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(104, 20);
            labelCity.TabIndex = 19;
            labelCity.Text = "City (required)";
            // 
            // textBoxAddress2
            // 
            textBoxAddress2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxAddress2.Location = new Point(24, 198);
            textBoxAddress2.Name = "textBoxAddress2";
            textBoxAddress2.Size = new Size(320, 29);
            textBoxAddress2.TabIndex = 3;
            // 
            // labelAddress2
            // 
            labelAddress2.AutoSize = true;
            labelAddress2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelAddress2.ForeColor = Color.White;
            labelAddress2.Location = new Point(24, 172);
            labelAddress2.Name = "labelAddress2";
            labelAddress2.Size = new Size(148, 20);
            labelAddress2.TabIndex = 17;
            labelAddress2.Text = "Street Address Line 2";
            // 
            // textBoxAddress1
            // 
            textBoxAddress1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxAddress1.Location = new Point(24, 131);
            textBoxAddress1.Name = "textBoxAddress1";
            textBoxAddress1.Size = new Size(320, 29);
            textBoxAddress1.TabIndex = 2;
            // 
            // labelAddress1
            // 
            labelAddress1.AutoSize = true;
            labelAddress1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelAddress1.ForeColor = Color.White;
            labelAddress1.Location = new Point(24, 105);
            labelAddress1.Name = "labelAddress1";
            labelAddress1.Size = new Size(175, 20);
            labelAddress1.TabIndex = 15;
            labelAddress1.Text = "Street Address (required)";
            // 
            // textBoxCompanyName
            // 
            textBoxCompanyName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxCompanyName.Location = new Point(23, 63);
            textBoxCompanyName.Name = "textBoxCompanyName";
            textBoxCompanyName.Size = new Size(320, 29);
            textBoxCompanyName.TabIndex = 1;
            // 
            // labelCompanyName
            // 
            labelCompanyName.AutoSize = true;
            labelCompanyName.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCompanyName.ForeColor = Color.White;
            labelCompanyName.Location = new Point(23, 37);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(186, 20);
            labelCompanyName.TabIndex = 13;
            labelCompanyName.Text = "Company Name (required)";
            // 
            // groupBoxServicesOffered
            // 
            groupBoxServicesOffered.Controls.Add(checkBoxFTL);
            groupBoxServicesOffered.Controls.Add(checkBoxArmouredCar);
            groupBoxServicesOffered.Controls.Add(checkBoxCourier);
            groupBoxServicesOffered.Controls.Add(checkBoxTrackingServices);
            groupBoxServicesOffered.Controls.Add(checkBoxLastMile);
            groupBoxServicesOffered.Controls.Add(checkBoxElectronicQuotes);
            groupBoxServicesOffered.Controls.Add(checkBoxFavorite);
            groupBoxServicesOffered.Controls.Add(checkBoxAirplane);
            groupBoxServicesOffered.Controls.Add(checkBoxRailroad);
            groupBoxServicesOffered.Controls.Add(checkBoxOcean);
            groupBoxServicesOffered.Controls.Add(checkBoxLTL);
            groupBoxServicesOffered.Controls.Add(checkBoxLiftgate);
            groupBoxServicesOffered.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxServicesOffered.ForeColor = Color.White;
            groupBoxServicesOffered.Location = new Point(424, 420);
            groupBoxServicesOffered.Name = "groupBoxServicesOffered";
            groupBoxServicesOffered.Size = new Size(375, 254);
            groupBoxServicesOffered.TabIndex = 13;
            groupBoxServicesOffered.TabStop = false;
            groupBoxServicesOffered.Text = "Services Offered";
            // 
            // checkBoxFTL
            // 
            checkBoxFTL.AutoSize = true;
            checkBoxFTL.Location = new Point(109, 35);
            checkBoxFTL.Name = "checkBoxFTL";
            checkBoxFTL.Size = new Size(53, 24);
            checkBoxFTL.TabIndex = 15;
            checkBoxFTL.Text = "FTL";
            checkBoxFTL.UseVisualStyleBackColor = true;
            // 
            // checkBoxArmouredCar
            // 
            checkBoxArmouredCar.AutoSize = true;
            checkBoxArmouredCar.Location = new Point(25, 215);
            checkBoxArmouredCar.Name = "checkBoxArmouredCar";
            checkBoxArmouredCar.Size = new Size(127, 24);
            checkBoxArmouredCar.TabIndex = 21;
            checkBoxArmouredCar.Text = "Armoured Car";
            checkBoxArmouredCar.UseVisualStyleBackColor = true;
            // 
            // checkBoxCourier
            // 
            checkBoxCourier.AutoSize = true;
            checkBoxCourier.Location = new Point(25, 185);
            checkBoxCourier.Name = "checkBoxCourier";
            checkBoxCourier.Size = new Size(79, 24);
            checkBoxCourier.TabIndex = 20;
            checkBoxCourier.Text = "Courier";
            checkBoxCourier.UseVisualStyleBackColor = true;
            // 
            // checkBoxTrackingServices
            // 
            checkBoxTrackingServices.AutoSize = true;
            checkBoxTrackingServices.ForeColor = Color.White;
            checkBoxTrackingServices.Location = new Point(192, 98);
            checkBoxTrackingServices.Name = "checkBoxTrackingServices";
            checkBoxTrackingServices.Size = new Size(148, 24);
            checkBoxTrackingServices.TabIndex = 24;
            checkBoxTrackingServices.Text = "Tracking Services";
            checkBoxTrackingServices.UseVisualStyleBackColor = true;
            // 
            // checkBoxLastMile
            // 
            checkBoxLastMile.AutoSize = true;
            checkBoxLastMile.Location = new Point(25, 155);
            checkBoxLastMile.Name = "checkBoxLastMile";
            checkBoxLastMile.Size = new Size(91, 24);
            checkBoxLastMile.TabIndex = 19;
            checkBoxLastMile.Text = "Last Mile";
            checkBoxLastMile.UseVisualStyleBackColor = true;
            // 
            // checkBoxElectronicQuotes
            // 
            checkBoxElectronicQuotes.AutoSize = true;
            checkBoxElectronicQuotes.ForeColor = Color.White;
            checkBoxElectronicQuotes.Location = new Point(192, 66);
            checkBoxElectronicQuotes.Name = "checkBoxElectronicQuotes";
            checkBoxElectronicQuotes.Size = new Size(150, 24);
            checkBoxElectronicQuotes.TabIndex = 23;
            checkBoxElectronicQuotes.Text = "Electronic Quotes";
            checkBoxElectronicQuotes.UseVisualStyleBackColor = true;
            // 
            // checkBoxFavorite
            // 
            checkBoxFavorite.AutoSize = true;
            checkBoxFavorite.ForeColor = Color.Gold;
            checkBoxFavorite.Location = new Point(192, 128);
            checkBoxFavorite.Name = "checkBoxFavorite";
            checkBoxFavorite.Size = new Size(85, 24);
            checkBoxFavorite.TabIndex = 25;
            checkBoxFavorite.Text = "Favorite";
            checkBoxFavorite.UseVisualStyleBackColor = true;
            // 
            // checkBoxAirplane
            // 
            checkBoxAirplane.AutoSize = true;
            checkBoxAirplane.Location = new Point(25, 125);
            checkBoxAirplane.Name = "checkBoxAirplane";
            checkBoxAirplane.Size = new Size(91, 24);
            checkBoxAirplane.TabIndex = 18;
            checkBoxAirplane.Text = "Air Plane";
            checkBoxAirplane.UseVisualStyleBackColor = true;
            // 
            // checkBoxRailroad
            // 
            checkBoxRailroad.AutoSize = true;
            checkBoxRailroad.Location = new Point(27, 95);
            checkBoxRailroad.Name = "checkBoxRailroad";
            checkBoxRailroad.Size = new Size(86, 24);
            checkBoxRailroad.TabIndex = 17;
            checkBoxRailroad.Text = "Railroad";
            checkBoxRailroad.UseVisualStyleBackColor = true;
            // 
            // checkBoxOcean
            // 
            checkBoxOcean.AutoSize = true;
            checkBoxOcean.Location = new Point(27, 65);
            checkBoxOcean.Name = "checkBoxOcean";
            checkBoxOcean.Size = new Size(71, 24);
            checkBoxOcean.TabIndex = 16;
            checkBoxOcean.Text = "Ocean";
            checkBoxOcean.UseVisualStyleBackColor = true;
            // 
            // checkBoxLTL
            // 
            checkBoxLTL.AutoSize = true;
            checkBoxLTL.Location = new Point(27, 35);
            checkBoxLTL.Name = "checkBoxLTL";
            checkBoxLTL.Size = new Size(52, 24);
            checkBoxLTL.TabIndex = 14;
            checkBoxLTL.Text = "LTL";
            checkBoxLTL.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiftgate
            // 
            checkBoxLiftgate.AutoSize = true;
            checkBoxLiftgate.Location = new Point(192, 35);
            checkBoxLiftgate.Name = "checkBoxLiftgate";
            checkBoxLiftgate.Size = new Size(143, 24);
            checkBoxLiftgate.TabIndex = 22;
            checkBoxLiftgate.Text = "Lift gate offered";
            checkBoxLiftgate.UseVisualStyleBackColor = true;
            // 
            // groupBoxMainContact
            // 
            groupBoxMainContact.Controls.Add(maskedTextBoxEmailAddress2);
            groupBoxMainContact.Controls.Add(maskedTextBoxEmailAddress1);
            groupBoxMainContact.Controls.Add(maskedTextBoxPhoneNumber2);
            groupBoxMainContact.Controls.Add(maskedTextBoxPhoneNumber1);
            groupBoxMainContact.Controls.Add(labelEmailAddress2);
            groupBoxMainContact.Controls.Add(labelPhoneNumber2);
            groupBoxMainContact.Controls.Add(labelEmailAddress1);
            groupBoxMainContact.Controls.Add(labelPhoneNumber1);
            groupBoxMainContact.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxMainContact.ForeColor = Color.White;
            groupBoxMainContact.Location = new Point(424, 81);
            groupBoxMainContact.Name = "groupBoxMainContact";
            groupBoxMainContact.Size = new Size(375, 320);
            groupBoxMainContact.TabIndex = 8;
            groupBoxMainContact.TabStop = false;
            groupBoxMainContact.Text = "Main Contact Information";
            // 
            // maskedTextBoxEmailAddress2
            // 
            maskedTextBoxEmailAddress2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxEmailAddress2.Location = new Point(25, 267);
            maskedTextBoxEmailAddress2.Name = "maskedTextBoxEmailAddress2";
            maskedTextBoxEmailAddress2.Size = new Size(322, 29);
            maskedTextBoxEmailAddress2.TabIndex = 12;
            maskedTextBoxEmailAddress2.Validating += textBoxEmailValidating_Validating;
            // 
            // maskedTextBoxEmailAddress1
            // 
            maskedTextBoxEmailAddress1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxEmailAddress1.Location = new Point(25, 131);
            maskedTextBoxEmailAddress1.Name = "maskedTextBoxEmailAddress1";
            maskedTextBoxEmailAddress1.Size = new Size(322, 29);
            maskedTextBoxEmailAddress1.TabIndex = 10;
            maskedTextBoxEmailAddress1.Validating += textBoxEmailValidating_Validating;
            // 
            // maskedTextBoxPhoneNumber2
            // 
            maskedTextBoxPhoneNumber2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPhoneNumber2.Location = new Point(25, 198);
            maskedTextBoxPhoneNumber2.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber2.Name = "maskedTextBoxPhoneNumber2";
            maskedTextBoxPhoneNumber2.Size = new Size(322, 29);
            maskedTextBoxPhoneNumber2.TabIndex = 11;
            maskedTextBoxPhoneNumber2.Enter += MaskedTextBoxPhoneNumber2_Enter;
            maskedTextBoxPhoneNumber2.KeyPress += MaskedTextBoxPhoneNumber2_KeyPress;
            // 
            // maskedTextBoxPhoneNumber1
            // 
            maskedTextBoxPhoneNumber1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPhoneNumber1.Location = new Point(25, 63);
            maskedTextBoxPhoneNumber1.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber1.Name = "maskedTextBoxPhoneNumber1";
            maskedTextBoxPhoneNumber1.Size = new Size(322, 29);
            maskedTextBoxPhoneNumber1.TabIndex = 9;
            maskedTextBoxPhoneNumber1.Enter += MaskedTextBoxPhoneNumber1_Enter;
            maskedTextBoxPhoneNumber1.KeyPress += MaskedTextBoxPhoneNumber1_KeyPress;
            // 
            // labelEmailAddress2
            // 
            labelEmailAddress2.AutoSize = true;
            labelEmailAddress2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelEmailAddress2.ForeColor = Color.White;
            labelEmailAddress2.Location = new Point(29, 242);
            labelEmailAddress2.Name = "labelEmailAddress2";
            labelEmailAddress2.Size = new Size(200, 20);
            labelEmailAddress2.TabIndex = 35;
            labelEmailAddress2.Text = "Agent EmailAddress Address";
            // 
            // labelPhoneNumber2
            // 
            labelPhoneNumber2.AutoSize = true;
            labelPhoneNumber2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPhoneNumber2.ForeColor = Color.White;
            labelPhoneNumber2.Location = new Point(27, 172);
            labelPhoneNumber2.Name = "labelPhoneNumber2";
            labelPhoneNumber2.Size = new Size(152, 20);
            labelPhoneNumber2.TabIndex = 33;
            labelPhoneNumber2.Text = "Agent Phone Number";
            // 
            // labelEmailAddress1
            // 
            labelEmailAddress1.AutoSize = true;
            labelEmailAddress1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelEmailAddress1.ForeColor = Color.White;
            labelEmailAddress1.Location = new Point(27, 107);
            labelEmailAddress1.Name = "labelEmailAddress1";
            labelEmailAddress1.Size = new Size(263, 20);
            labelEmailAddress1.TabIndex = 31;
            labelEmailAddress1.Text = "Main EmailAddress Address (required)";
            // 
            // labelPhoneNumber1
            // 
            labelPhoneNumber1.AutoSize = true;
            labelPhoneNumber1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPhoneNumber1.ForeColor = Color.White;
            labelPhoneNumber1.Location = new Point(25, 37);
            labelPhoneNumber1.Name = "labelPhoneNumber1";
            labelPhoneNumber1.Size = new Size(215, 20);
            labelPhoneNumber1.TabIndex = 29;
            labelPhoneNumber1.Text = "Main Phone Number (required)";
            // 
            // groupBoxComments
            // 
            groupBoxComments.Controls.Add(buttonEditContact);
            groupBoxComments.Controls.Add(buttonRemoveContact);
            groupBoxComments.Controls.Add(buttonAddContact);
            groupBoxComments.Controls.Add(listViewShipperContacts);
            groupBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxComments.ForeColor = Color.White;
            groupBoxComments.Location = new Point(827, 420);
            groupBoxComments.Name = "groupBoxComments";
            groupBoxComments.Size = new Size(422, 254);
            groupBoxComments.TabIndex = 31;
            groupBoxComments.TabStop = false;
            groupBoxComments.Text = "Contacts";
            // 
            // buttonEditContact
            // 
            buttonEditContact.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditContact.FlatAppearance.BorderSize = 0;
            buttonEditContact.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditContact.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditContact.FlatStyle = FlatStyle.Flat;
            buttonEditContact.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditContact.Location = new Point(248, 37);
            buttonEditContact.Name = "buttonEditContact";
            buttonEditContact.Size = new Size(70, 28);
            buttonEditContact.TabIndex = 33;
            buttonEditContact.Text = "Edit";
            buttonEditContact.UseVisualStyleBackColor = false;
            buttonEditContact.Click += ButtonEditContact_Click;
            // 
            // buttonRemoveContact
            // 
            buttonRemoveContact.BackColor = Color.FromArgb(60, 60, 60);
            buttonRemoveContact.FlatAppearance.BorderSize = 0;
            buttonRemoveContact.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonRemoveContact.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonRemoveContact.FlatStyle = FlatStyle.Flat;
            buttonRemoveContact.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRemoveContact.Location = new Point(331, 37);
            buttonRemoveContact.Name = "buttonRemoveContact";
            buttonRemoveContact.Size = new Size(70, 28);
            buttonRemoveContact.TabIndex = 34;
            buttonRemoveContact.Text = "Remove";
            buttonRemoveContact.UseVisualStyleBackColor = false;
            buttonRemoveContact.Click += ButtonRemoveContact_Click;
            // 
            // buttonAddContact
            // 
            buttonAddContact.BackColor = Color.FromArgb(60, 60, 60);
            buttonAddContact.FlatAppearance.BorderSize = 0;
            buttonAddContact.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAddContact.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAddContact.FlatStyle = FlatStyle.Flat;
            buttonAddContact.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddContact.Location = new Point(165, 37);
            buttonAddContact.Name = "buttonAddContact";
            buttonAddContact.Size = new Size(70, 28);
            buttonAddContact.TabIndex = 32;
            buttonAddContact.Text = "Add";
            buttonAddContact.UseVisualStyleBackColor = false;
            buttonAddContact.Click += ButtonAddContact_Click;
            // 
            // listViewShipperContacts
            // 
            listViewShipperContacts.Location = new Point(17, 83);
            listViewShipperContacts.Name = "listViewShipperContacts";
            listViewShipperContacts.Size = new Size(386, 149);
            listViewShipperContacts.TabIndex = 35;
            listViewShipperContacts.UseCompatibleStateImageBehavior = false;
            listViewShipperContacts.DrawColumnHeader += ListView_DrawColumnHeader;
            listViewShipperContacts.SelectedIndexChanged += listViewShipperContacts_SelectedIndexChanged;
            listViewShipperContacts.DoubleClick += listViewShipperContacts_DoubleClick;
            // 
            // groupBoxCountries
            // 
            groupBoxCountries.Controls.Add(buttonCountryRemove);
            groupBoxCountries.Controls.Add(buttonCountryAdd);
            groupBoxCountries.Controls.Add(comboBoxCountriesSource);
            groupBoxCountries.Controls.Add(listViewCountriesServiced);
            groupBoxCountries.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxCountries.ForeColor = Color.White;
            groupBoxCountries.Location = new Point(827, 81);
            groupBoxCountries.Name = "groupBoxCountries";
            groupBoxCountries.Size = new Size(422, 320);
            groupBoxCountries.TabIndex = 26;
            groupBoxCountries.TabStop = false;
            groupBoxCountries.Text = "Countries Serviced";
            // 
            // buttonCountryRemove
            // 
            buttonCountryRemove.BackColor = Color.FromArgb(60, 60, 60);
            buttonCountryRemove.FlatAppearance.BorderSize = 0;
            buttonCountryRemove.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCountryRemove.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCountryRemove.FlatStyle = FlatStyle.Flat;
            buttonCountryRemove.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCountryRemove.Location = new Point(332, 32);
            buttonCountryRemove.Name = "buttonCountryRemove";
            buttonCountryRemove.Size = new Size(70, 28);
            buttonCountryRemove.TabIndex = 29;
            buttonCountryRemove.Text = "Remove";
            buttonCountryRemove.UseVisualStyleBackColor = false;
            buttonCountryRemove.Click += ButtonCountryRemove_Click;
            // 
            // buttonCountryAdd
            // 
            buttonCountryAdd.BackColor = Color.FromArgb(60, 60, 60);
            buttonCountryAdd.FlatAppearance.BorderSize = 0;
            buttonCountryAdd.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCountryAdd.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCountryAdd.FlatStyle = FlatStyle.Flat;
            buttonCountryAdd.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCountryAdd.Location = new Point(249, 32);
            buttonCountryAdd.Name = "buttonCountryAdd";
            buttonCountryAdd.Size = new Size(70, 28);
            buttonCountryAdd.TabIndex = 28;
            buttonCountryAdd.Text = "Add";
            buttonCountryAdd.UseVisualStyleBackColor = false;
            buttonCountryAdd.Click += ButtonCountryAdd_Click;
            // 
            // comboBoxCountriesSource
            // 
            comboBoxCountriesSource.FormattingEnabled = true;
            comboBoxCountriesSource.ItemHeight = 20;
            comboBoxCountriesSource.Location = new Point(17, 32);
            comboBoxCountriesSource.Name = "comboBoxCountriesSource";
            comboBoxCountriesSource.Size = new Size(223, 28);
            comboBoxCountriesSource.TabIndex = 27;
            // 
            // listViewCountriesServiced
            // 
            listViewCountriesServiced.Location = new Point(17, 76);
            listViewCountriesServiced.Name = "listViewCountriesServiced";
            listViewCountriesServiced.Size = new Size(386, 220);
            listViewCountriesServiced.TabIndex = 30;
            listViewCountriesServiced.UseCompatibleStateImageBehavior = false;
            listViewCountriesServiced.DrawColumnHeader += ListView_DrawColumnHeader;
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxComments.Location = new Point(46, 722);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(753, 76);
            textBoxComments.TabIndex = 36;
            // 
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelComments.ForeColor = Color.White;
            labelComments.Location = new Point(46, 690);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(80, 20);
            labelComments.TabIndex = 26;
            labelComments.Text = "Comments";
            // 
            // labelLocationChangePending
            // 
            labelLocationChangePending.AutoSize = true;
            labelLocationChangePending.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelLocationChangePending.ForeColor = Color.White;
            labelLocationChangePending.Location = new Point(988, 690);
            labelLocationChangePending.Name = "labelLocationChangePending";
            labelLocationChangePending.Size = new Size(261, 20);
            labelLocationChangePending.TabIndex = 42;
            labelLocationChangePending.Text = "A change is pending, save to update";
            labelLocationChangePending.TextAlign = ContentAlignment.TopRight;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ShipperDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(1280, 824);
            Controls.Add(labelLocationChangePending);
            Controls.Add(labelComments);
            Controls.Add(textBoxComments);
            Controls.Add(groupBoxCountries);
            Controls.Add(groupBoxComments);
            Controls.Add(groupBoxMainContact);
            Controls.Add(groupBoxServicesOffered);
            Controls.Add(groupBoxCompanyInfo);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panelHeader);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ShipperDialog";
            Text = AppInfo.WindowTitle("Shipper Editor");
            TopMost = true;
            Load += ShipperDialog_Load;
            Shown += ShipperDialogShown;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxCompanyInfo.ResumeLayout(false);
            groupBoxCompanyInfo.PerformLayout();
            groupBoxServicesOffered.ResumeLayout(false);
            groupBoxServicesOffered.PerformLayout();
            groupBoxMainContact.ResumeLayout(false);
            groupBoxMainContact.PerformLayout();
            groupBoxComments.ResumeLayout(false);
            groupBoxCountries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private Button OK_Button;
        private Button Cancel_Button;
        private GroupBox groupBoxCompanyInfo;
        private TextBox textBoxCompanyName;
        private Label labelCompanyName;
        private TextBox textBoxAddress2;
        private Label labelAddress2;
        private TextBox textBoxAddress1;
        private Label labelAddress1;
        private TextBox textBoxCity;
        private Label labelCity;
        private Label labelCountry;
        private ComboBox comboBoxCountry;
        private Label labelRegion;
        private ComboBox comboBoxRegion;
        private Label labelPostalCode;
        private GroupBox groupBoxServicesOffered;
        private GroupBox groupBoxMainContact;
        private Label labelEmailAddress1;
        private Label labelPhoneNumber1;
        private Label labelEmailAddress2;
        private Label labelPhoneNumber2;
        private CheckBox checkBoxRailroad;
        private CheckBox checkBoxOcean;
        private CheckBox checkBoxLTL;
        private CheckBox checkBoxLiftgate;
        private CheckBox checkBoxAirplane;
        private CheckBox checkBoxFavorite;
        private GroupBox groupBoxComments;
        private GroupBox groupBoxCountries;
        private CheckBox checkBoxElectronicQuotes;
        private CheckBox checkBoxLastMile;
        private CheckBox checkBoxTrackingServices;
        private MaskedTextBox maskedTextBoxPhoneNumber1;
        private MaskedTextBox maskedTextBoxEmailAddress2;
        private MaskedTextBox maskedTextBoxEmailAddress1;
        private MaskedTextBox maskedTextBoxPhoneNumber2;
        private MaskedTextBox maskedTextBoxPostalCode;
        private TextBox textBoxComments;
        private Label labelComments;
        private Button buttonCountryRemove;
        private Button buttonCountryAdd;
        private ComboBox comboBoxCountriesSource;
        private ListView listViewCountriesServiced;
        private Button buttonEditContact;
        private Button buttonRemoveContact;
        private Button buttonAddContact;
        private ListView listViewShipperContacts;
        private CheckBox checkBoxArmouredCar;
        private CheckBox checkBoxCourier;
        private CheckBox checkBoxFTL;
        private PictureBox pictureBoxUpdateFlag;
        private Label labelLocationChangePending;
        private ErrorProvider errorProvider1;
    }
}
