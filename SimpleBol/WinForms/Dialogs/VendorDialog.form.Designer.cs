namespace SimpleBol.WinForms.Dialogs
{
    partial class VendorDialog
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
            pictureBoxUpdateFlag = new PictureBox();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBoxMainContact = new GroupBox();
            maskedTextBoxEmailAddress2 = new MaskedTextBox();
            maskedTextBoxEmailAddress1 = new MaskedTextBox();
            maskedTextBoxPhoneNumber2 = new MaskedTextBox();
            maskedTextBoxPhoneNumber1 = new MaskedTextBox();
            labelEmailAddress2 = new Label();
            labelPhoneNumber2 = new Label();
            labelEmailAddress1 = new Label();
            labelPhoneNumber1 = new Label();
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
            labelComments = new Label();
            textBoxComments = new TextBox();
            groupBox1 = new GroupBox();
            buttonEditLocation = new Button();
            buttonRemoveLocation = new Button();
            buttonAddLocation = new Button();
            listViewShippingLocations = new ListView();
            groupBox2 = new GroupBox();
            checkBoxPalletJackRequired = new CheckBox();
            checkBoxLiftgateRequired = new CheckBox();
            labelLocationChangePending = new Label();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxMainContact.SuspendLayout();
            groupBoxCompanyInfo.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(pictureBoxUpdateFlag);
            panel1.Controls.Add(Lbl_Header);
            panel1.Controls.Add(PbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 5, 3, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1217, 70);
            panel1.TabIndex = 4;
            // 
            // pictureBoxUpdateFlag
            // 
            pictureBoxUpdateFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.InitialImage = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.Location = new Point(1152, 15);
            pictureBoxUpdateFlag.Name = "pictureBoxUpdateFlag";
            pictureBoxUpdateFlag.Size = new Size(45, 45);
            pictureBoxUpdateFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUpdateFlag.TabIndex = 2;
            pictureBoxUpdateFlag.TabStop = false;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 15);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(215, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Vendor Editor";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = Properties.Resources.vendorsIcon65;
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 5, 3, 5);
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
            OK_Button.Location = new Point(953, 753);
            OK_Button.Margin = new Padding(3, 5, 3, 5);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 22;
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
            Cancel_Button.Location = new Point(1082, 753);
            Cancel_Button.Margin = new Padding(3, 5, 3, 5);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 23;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
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
            groupBoxMainContact.Location = new Point(419, 85);
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
            labelEmailAddress2.Size = new Size(233, 20);
            labelEmailAddress2.TabIndex = 35;
            labelEmailAddress2.Text = "Warehouse EmailAddress Address";
            // 
            // labelPhoneNumber2
            // 
            labelPhoneNumber2.AutoSize = true;
            labelPhoneNumber2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPhoneNumber2.ForeColor = Color.White;
            labelPhoneNumber2.Location = new Point(27, 172);
            labelPhoneNumber2.Name = "labelPhoneNumber2";
            labelPhoneNumber2.Size = new Size(185, 20);
            labelPhoneNumber2.TabIndex = 33;
            labelPhoneNumber2.Text = "Warehouse Phone Number";
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
            groupBoxCompanyInfo.Location = new Point(16, 85);
            groupBoxCompanyInfo.Name = "groupBoxCompanyInfo";
            groupBoxCompanyInfo.Size = new Size(375, 593);
            groupBoxCompanyInfo.TabIndex = 0;
            groupBoxCompanyInfo.TabStop = false;
            groupBoxCompanyInfo.Text = "Company Information";
            // 
            // maskedTextBoxPostalCode
            // 
            maskedTextBoxPostalCode.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPostalCode.Location = new Point(23, 341);
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
            labelPostalCode.Location = new Point(23, 317);
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
            labelRegion.Location = new Point(24, 456);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(161, 20);
            labelRegion.TabIndex = 24;
            labelRegion.Text = "RegionCode (required)";
            // 
            // comboBoxRegion
            // 
            comboBoxRegion.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxRegion.FormattingEnabled = true;
            comboBoxRegion.Location = new Point(25, 484);
            comboBoxRegion.Name = "comboBoxRegion";
            comboBoxRegion.Size = new Size(319, 29);
            comboBoxRegion.TabIndex = 7;
            // 
            // labelCountry
            // 
            labelCountry.AutoSize = true;
            labelCountry.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCountry.ForeColor = Color.White;
            labelCountry.Location = new Point(23, 385);
            labelCountry.Name = "labelCountry";
            labelCountry.Size = new Size(165, 20);
            labelCountry.TabIndex = 22;
            labelCountry.Text = "CountryCode (required)";
            // 
            // comboBoxCountry
            // 
            comboBoxCountry.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxCountry.FormattingEnabled = true;
            comboBoxCountry.Location = new Point(24, 413);
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
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelComments.ForeColor = Color.White;
            labelComments.Location = new Point(16, 698);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(80, 20);
            labelComments.TabIndex = 40;
            labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxComments.Location = new Point(16, 728);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(778, 76);
            textBoxComments.TabIndex = 21;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonEditLocation);
            groupBox1.Controls.Add(buttonRemoveLocation);
            groupBox1.Controls.Add(buttonAddLocation);
            groupBox1.Controls.Add(listViewShippingLocations);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(822, 85);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(375, 593);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Shipping Locations";
            // 
            // buttonEditLocation
            // 
            buttonEditLocation.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditLocation.Cursor = Cursors.Hand;
            buttonEditLocation.FlatAppearance.BorderSize = 0;
            buttonEditLocation.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditLocation.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditLocation.FlatStyle = FlatStyle.Flat;
            buttonEditLocation.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditLocation.Location = new Point(206, 34);
            buttonEditLocation.Name = "buttonEditLocation";
            buttonEditLocation.Size = new Size(70, 28);
            buttonEditLocation.TabIndex = 18;
            buttonEditLocation.Text = "Edit";
            buttonEditLocation.UseVisualStyleBackColor = false;
            buttonEditLocation.Click += ButtonEditLocation_Click;
            // 
            // buttonRemoveLocation
            // 
            buttonRemoveLocation.BackColor = Color.FromArgb(60, 60, 60);
            buttonRemoveLocation.Cursor = Cursors.Hand;
            buttonRemoveLocation.FlatAppearance.BorderSize = 0;
            buttonRemoveLocation.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonRemoveLocation.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonRemoveLocation.FlatStyle = FlatStyle.Flat;
            buttonRemoveLocation.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRemoveLocation.Location = new Point(289, 34);
            buttonRemoveLocation.Name = "buttonRemoveLocation";
            buttonRemoveLocation.Size = new Size(70, 28);
            buttonRemoveLocation.TabIndex = 19;
            buttonRemoveLocation.Text = "Remove";
            buttonRemoveLocation.UseVisualStyleBackColor = false;
            buttonRemoveLocation.Click += ButtonRemoveLocation_Click;
            // 
            // buttonAddLocation
            // 
            buttonAddLocation.BackColor = Color.FromArgb(60, 60, 60);
            buttonAddLocation.Cursor = Cursors.Hand;
            buttonAddLocation.FlatAppearance.BorderSize = 0;
            buttonAddLocation.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAddLocation.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAddLocation.FlatStyle = FlatStyle.Flat;
            buttonAddLocation.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonAddLocation.Location = new Point(123, 34);
            buttonAddLocation.Name = "buttonAddLocation";
            buttonAddLocation.Size = new Size(70, 28);
            buttonAddLocation.TabIndex = 17;
            buttonAddLocation.Text = "Add";
            buttonAddLocation.UseVisualStyleBackColor = false;
            buttonAddLocation.Click += ButtonAddLocation_Click;
            // 
            // listViewShippingLocations
            // 
            listViewShippingLocations.Location = new Point(17, 74);
            listViewShippingLocations.Name = "listViewShippingLocations";
            listViewShippingLocations.Size = new Size(342, 500);
            listViewShippingLocations.TabIndex = 20;
            listViewShippingLocations.UseCompatibleStateImageBehavior = false;
            listViewShippingLocations.SelectedIndexChanged += ListViewShippingLocations_SelectedIndexChanged;
            listViewShippingLocations.DoubleClick += ListViewShippingLocations_DoubleClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBoxPalletJackRequired);
            groupBox2.Controls.Add(checkBoxLiftgateRequired);
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(419, 440);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(375, 238);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Options";
            // 
            // checkBoxPalletJackRequired
            // 
            checkBoxPalletJackRequired.AutoSize = true;
            checkBoxPalletJackRequired.Location = new Point(29, 85);
            checkBoxPalletJackRequired.Name = "checkBoxPalletJackRequired";
            checkBoxPalletJackRequired.Size = new Size(168, 24);
            checkBoxPalletJackRequired.TabIndex = 15;
            checkBoxPalletJackRequired.Text = "Pallet Jack Required";
            checkBoxPalletJackRequired.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiftgateRequired
            // 
            checkBoxLiftgateRequired.AutoSize = true;
            checkBoxLiftgateRequired.Location = new Point(29, 43);
            checkBoxLiftgateRequired.Name = "checkBoxLiftgateRequired";
            checkBoxLiftgateRequired.Size = new Size(213, 24);
            checkBoxLiftgateRequired.TabIndex = 14;
            checkBoxLiftgateRequired.Text = "LiftgateRequired Required";
            checkBoxLiftgateRequired.UseVisualStyleBackColor = true;
            // 
            // labelLocationChangePending
            // 
            labelLocationChangePending.AutoSize = true;
            labelLocationChangePending.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelLocationChangePending.ForeColor = Color.White;
            labelLocationChangePending.Location = new Point(876, 698);
            labelLocationChangePending.Name = "labelLocationChangePending";
            labelLocationChangePending.Size = new Size(321, 20);
            labelLocationChangePending.TabIndex = 41;
            labelLocationChangePending.Text = "A location change is pending, save to update";
            labelLocationChangePending.TextAlign = ContentAlignment.TopRight;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // VendorDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(1217, 830);
            Controls.Add(labelLocationChangePending);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(labelComments);
            Controls.Add(textBoxComments);
            Controls.Add(groupBoxMainContact);
            Controls.Add(groupBoxCompanyInfo);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            Margin = new Padding(3, 4, 3, 4);
            Name = "VendorDialog";
            Text = AppInfo.WindowTitle("Vendor Editor");
            Load += VendorDialog_Load;
            Shown += VendorDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxMainContact.ResumeLayout(false);
            groupBoxMainContact.PerformLayout();
            groupBoxCompanyInfo.ResumeLayout(false);
            groupBoxCompanyInfo.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private Button OK_Button;
        private Button Cancel_Button;
        private GroupBox groupBoxMainContact;
        private MaskedTextBox maskedTextBoxEmailAddress2;
        private MaskedTextBox maskedTextBoxEmailAddress1;
        private MaskedTextBox maskedTextBoxPhoneNumber2;
        private MaskedTextBox maskedTextBoxPhoneNumber1;
        private Label labelEmailAddress2;
        private Label labelPhoneNumber2;
        private Label labelEmailAddress1;
        private Label labelPhoneNumber1;
        private GroupBox groupBoxCompanyInfo;
        private MaskedTextBox maskedTextBoxPostalCode;
        private Label labelPostalCode;
        private Label labelRegion;
        private ComboBox comboBoxRegion;
        private Label labelCountry;
        private ComboBox comboBoxCountry;
        private TextBox textBoxCity;
        private Label labelCity;
        private TextBox textBoxAddress2;
        private Label labelAddress2;
        private TextBox textBoxAddress1;
        private Label labelAddress1;
        private TextBox textBoxCompanyName;
        private Label labelCompanyName;
        private Label labelComments;
        private TextBox textBoxComments;
        private GroupBox groupBox1;
        private ListView listViewShippingLocations;
        private Button buttonEditLocation;
        private Button buttonRemoveLocation;
        private Button buttonAddLocation;
        private GroupBox groupBox2;
        private CheckBox checkBoxLiftgateRequired;
        private CheckBox checkBoxPalletJackRequired;
        private Label labelLocationChangePending;
        private PictureBox pictureBoxUpdateFlag;
        private ErrorProvider errorProvider1;
    }
}
