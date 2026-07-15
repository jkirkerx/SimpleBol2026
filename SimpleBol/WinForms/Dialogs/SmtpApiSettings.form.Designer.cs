namespace SimpleBol.WinForms.Dialogs
{
    partial class SmtpApiSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmtpApiSettingsDialog));
            panelHeeader = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            tabControlSmtpApiSettings = new TabControl();
            tabPageCompanyInfo = new TabPage();
            groupBoxCompanyInfo = new GroupBox();
            comboBoxRegion = new ComboBox();
            maskedTextBoxPostalCode = new MaskedTextBox();
            labelPostalCode = new Label();
            labelRegion = new Label();
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
            tabPageSendGrid = new TabPage();
            groupBoxSendGridAPI = new GroupBox();
            checkBoxSendGridDefault = new CheckBox();
            textBoxSendGridSentFrom = new TextBox();
            labelSendGridSentFrom = new Label();
            textBoxSendGridApiKey = new TextBox();
            labelSendGridApiKey = new Label();
            tabPageGmail = new TabPage();
            groupBox1 = new GroupBox();
            buttonConnectGmail = new Button();
            labelGmailConnectionStatus = new Label();
            checkBoxGmailDefault = new CheckBox();
            textBoxGmailSentFromAddress = new TextBox();
            label2 = new Label();
            textBoxGmailClientSecret = new TextBox();
            labelGmailClientSecret = new Label();
            textBoxGmailClientId = new TextBox();
            labelGmailClientId = new Label();
            tabPageOutlook = new TabPage();
            groupBox2 = new GroupBox();
            labelOutlookConnectionStatus = new Label();
            buttonConnectOutlook = new Button();
            textBoxOutlookTenantId = new TextBox();
            labelOutlookTenantId = new Label();
            checkBoxOutlookDefault = new CheckBox();
            textBoxOutlookSentFromAddress = new TextBox();
            label1 = new Label();
            textBoxOutlookClientId = new TextBox();
            labelOutlookClientId = new Label();
            OK_Button = new Button();
            Cancel_Button = new Button();
            errorProvider1 = new ErrorProvider(components);
            comboBoxDefaultApi = new ComboBox();
            labelDefaultApi = new Label();
            comboBoxEmailConnection = new ComboBox();
            labelConnection = new Label();
            panelHeeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            tabControlSmtpApiSettings.SuspendLayout();
            tabPageCompanyInfo.SuspendLayout();
            groupBoxCompanyInfo.SuspendLayout();
            tabPageSendGrid.SuspendLayout();
            groupBoxSendGridAPI.SuspendLayout();
            tabPageGmail.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPageOutlook.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // panelHeeader
            // 
            panelHeeader.BackColor = Color.FromArgb(38, 38, 38);
            panelHeeader.Controls.Add(Lbl_Header);
            panelHeeader.Controls.Add(PbLogo);
            panelHeeader.Dock = DockStyle.Top;
            panelHeeader.Location = new Point(0, 0);
            panelHeeader.Name = "panelHeeader";
            panelHeeader.Size = new Size(583, 70);
            panelHeeader.TabIndex = 1;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(85, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(380, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Email API/OAuth Settings";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = (Image)resources.GetObject("PbLogo.Image");
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(12, 8);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // tabControlSmtpApiSettings
            // 
            tabControlSmtpApiSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabControlSmtpApiSettings.Controls.Add(tabPageCompanyInfo);
            tabControlSmtpApiSettings.Controls.Add(tabPageSendGrid);
            tabControlSmtpApiSettings.Controls.Add(tabPageGmail);
            tabControlSmtpApiSettings.Controls.Add(tabPageOutlook);
            tabControlSmtpApiSettings.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabControlSmtpApiSettings.Location = new Point(22, 158);
            tabControlSmtpApiSettings.Name = "tabControlSmtpApiSettings";
            tabControlSmtpApiSettings.SelectedIndex = 0;
            tabControlSmtpApiSettings.Size = new Size(538, 611);
            tabControlSmtpApiSettings.TabIndex = 1;
            // 
            // tabPageCompanyInfo
            // 
            tabPageCompanyInfo.BackColor = Color.Black;
            tabPageCompanyInfo.Controls.Add(groupBoxCompanyInfo);
            tabPageCompanyInfo.Location = new Point(4, 24);
            tabPageCompanyInfo.Name = "tabPageCompanyInfo";
            tabPageCompanyInfo.Size = new Size(530, 583);
            tabPageCompanyInfo.TabIndex = 3;
            tabPageCompanyInfo.Text = "Company Info";
            // 
            // groupBoxCompanyInfo
            // 
            groupBoxCompanyInfo.Controls.Add(comboBoxRegion);
            groupBoxCompanyInfo.Controls.Add(maskedTextBoxPostalCode);
            groupBoxCompanyInfo.Controls.Add(labelPostalCode);
            groupBoxCompanyInfo.Controls.Add(labelRegion);
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
            groupBoxCompanyInfo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxCompanyInfo.ForeColor = Color.White;
            groupBoxCompanyInfo.Location = new Point(21, 22);
            groupBoxCompanyInfo.Name = "groupBoxCompanyInfo";
            groupBoxCompanyInfo.Size = new Size(488, 549);
            groupBoxCompanyInfo.TabIndex = 2;
            groupBoxCompanyInfo.TabStop = false;
            groupBoxCompanyInfo.Text = "Company Info for SPAM regulations";
            // 
            // comboBoxRegion
            // 
            comboBoxRegion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            comboBoxRegion.FormattingEnabled = true;
            comboBoxRegion.Location = new Point(30, 494);
            comboBoxRegion.Name = "comboBoxRegion";
            comboBoxRegion.Size = new Size(419, 29);
            comboBoxRegion.TabIndex = 9;
            // 
            // maskedTextBoxPostalCode
            // 
            maskedTextBoxPostalCode.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            maskedTextBoxPostalCode.Location = new Point(30, 339);
            maskedTextBoxPostalCode.Mask = "00000-9999";
            maskedTextBoxPostalCode.Name = "maskedTextBoxPostalCode";
            maskedTextBoxPostalCode.Size = new Size(417, 29);
            maskedTextBoxPostalCode.TabIndex = 7;
            // 
            // labelPostalCode
            // 
            labelPostalCode.AutoSize = true;
            labelPostalCode.Font = new Font("Segoe UI", 11F);
            labelPostalCode.ForeColor = Color.White;
            labelPostalCode.Location = new Point(30, 316);
            labelPostalCode.Name = "labelPostalCode";
            labelPostalCode.Size = new Size(157, 20);
            labelPostalCode.TabIndex = 38;
            labelPostalCode.Text = "Postal Code (required)";
            // 
            // labelRegion
            // 
            labelRegion.AutoSize = true;
            labelRegion.Font = new Font("Segoe UI", 11F);
            labelRegion.ForeColor = Color.White;
            labelRegion.Location = new Point(30, 471);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(161, 20);
            labelRegion.TabIndex = 37;
            labelRegion.Text = "RegionCode (required)";
            // 
            // labelCountry
            // 
            labelCountry.AutoSize = true;
            labelCountry.Font = new Font("Segoe UI", 11F);
            labelCountry.ForeColor = Color.White;
            labelCountry.Location = new Point(30, 395);
            labelCountry.Name = "labelCountry";
            labelCountry.Size = new Size(165, 20);
            labelCountry.TabIndex = 36;
            labelCountry.Text = "CountryCode (required)";
            // 
            // comboBoxCountry
            // 
            comboBoxCountry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            comboBoxCountry.FormattingEnabled = true;
            comboBoxCountry.Location = new Point(30, 418);
            comboBoxCountry.Name = "comboBoxCountry";
            comboBoxCountry.Size = new Size(419, 29);
            comboBoxCountry.TabIndex = 8;
            // 
            // textBoxCity
            // 
            textBoxCity.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            textBoxCity.Location = new Point(30, 263);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(418, 29);
            textBoxCity.TabIndex = 6;
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Font = new Font("Segoe UI", 11F);
            labelCity.ForeColor = Color.White;
            labelCity.Location = new Point(30, 240);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(104, 20);
            labelCity.TabIndex = 35;
            labelCity.Text = "City (required)";
            // 
            // textBoxAddress2
            // 
            textBoxAddress2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            textBoxAddress2.Location = new Point(30, 194);
            textBoxAddress2.Name = "textBoxAddress2";
            textBoxAddress2.Size = new Size(418, 29);
            textBoxAddress2.TabIndex = 5;
            // 
            // labelAddress2
            // 
            labelAddress2.AutoSize = true;
            labelAddress2.Font = new Font("Segoe UI", 11F);
            labelAddress2.ForeColor = Color.White;
            labelAddress2.Location = new Point(30, 171);
            labelAddress2.Name = "labelAddress2";
            labelAddress2.Size = new Size(148, 20);
            labelAddress2.TabIndex = 34;
            labelAddress2.Text = "Street Address Line 2";
            // 
            // textBoxAddress1
            // 
            textBoxAddress1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            textBoxAddress1.Location = new Point(30, 127);
            textBoxAddress1.Name = "textBoxAddress1";
            textBoxAddress1.Size = new Size(418, 29);
            textBoxAddress1.TabIndex = 4;
            // 
            // labelAddress1
            // 
            labelAddress1.AutoSize = true;
            labelAddress1.Font = new Font("Segoe UI", 11F);
            labelAddress1.ForeColor = Color.White;
            labelAddress1.Location = new Point(30, 104);
            labelAddress1.Name = "labelAddress1";
            labelAddress1.Size = new Size(175, 20);
            labelAddress1.TabIndex = 33;
            labelAddress1.Text = "Street Address (required)";
            // 
            // textBoxCompanyName
            // 
            textBoxCompanyName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            textBoxCompanyName.Location = new Point(30, 59);
            textBoxCompanyName.Name = "textBoxCompanyName";
            textBoxCompanyName.Size = new Size(417, 29);
            textBoxCompanyName.TabIndex = 3;
            // 
            // labelCompanyName
            // 
            labelCompanyName.AutoSize = true;
            labelCompanyName.Font = new Font("Segoe UI", 11F);
            labelCompanyName.ForeColor = Color.White;
            labelCompanyName.Location = new Point(30, 36);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(186, 20);
            labelCompanyName.TabIndex = 32;
            labelCompanyName.Text = "Company Name (required)";
            // 
            // tabPageSendGrid
            // 
            tabPageSendGrid.BackColor = Color.Black;
            tabPageSendGrid.Controls.Add(groupBoxSendGridAPI);
            tabPageSendGrid.ForeColor = Color.White;
            tabPageSendGrid.Location = new Point(4, 24);
            tabPageSendGrid.Name = "tabPageSendGrid";
            tabPageSendGrid.Padding = new Padding(3);
            tabPageSendGrid.Size = new Size(530, 583);
            tabPageSendGrid.TabIndex = 0;
            tabPageSendGrid.Text = "Twilio SendGrid";
            // 
            // groupBoxSendGridAPI
            // 
            groupBoxSendGridAPI.Controls.Add(checkBoxSendGridDefault);
            groupBoxSendGridAPI.Controls.Add(textBoxSendGridSentFrom);
            groupBoxSendGridAPI.Controls.Add(labelSendGridSentFrom);
            groupBoxSendGridAPI.Controls.Add(textBoxSendGridApiKey);
            groupBoxSendGridAPI.Controls.Add(labelSendGridApiKey);
            groupBoxSendGridAPI.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxSendGridAPI.ForeColor = Color.White;
            groupBoxSendGridAPI.Location = new Point(21, 22);
            groupBoxSendGridAPI.Name = "groupBoxSendGridAPI";
            groupBoxSendGridAPI.Size = new Size(488, 549);
            groupBoxSendGridAPI.TabIndex = 2;
            groupBoxSendGridAPI.TabStop = false;
            groupBoxSendGridAPI.Text = "SendGrid API";
            // 
            // checkBoxSendGridDefault
            // 
            checkBoxSendGridDefault.AutoSize = true;
            checkBoxSendGridDefault.Location = new Point(28, 180);
            checkBoxSendGridDefault.Name = "checkBoxSendGridDefault";
            checkBoxSendGridDefault.Size = new Size(197, 24);
            checkBoxSendGridDefault.TabIndex = 5;
            checkBoxSendGridDefault.Text = "Use SendGrid as Default";
            checkBoxSendGridDefault.UseVisualStyleBackColor = true;
            checkBoxSendGridDefault.CheckedChanged += CheckBoxSendGridDefault_CheckedChanged;
            // 
            // textBoxSendGridSentFrom
            // 
            textBoxSendGridSentFrom.Location = new Point(28, 128);
            textBoxSendGridSentFrom.Name = "textBoxSendGridSentFrom";
            textBoxSendGridSentFrom.Size = new Size(432, 27);
            textBoxSendGridSentFrom.TabIndex = 4;
            textBoxSendGridSentFrom.Validating += textBoxEmailValidating_Validating;
            // 
            // labelSendGridSentFrom
            // 
            labelSendGridSentFrom.AutoSize = true;
            labelSendGridSentFrom.Location = new Point(38, 105);
            labelSendGridSentFrom.Name = "labelSendGridSentFrom";
            labelSendGridSentFrom.Size = new Size(290, 20);
            labelSendGridSentFrom.TabIndex = 3;
            labelSendGridSentFrom.Text = "Special SendGrid Account Email Address";
            // 
            // textBoxSendGridApiKey
            // 
            textBoxSendGridApiKey.Location = new Point(28, 57);
            textBoxSendGridApiKey.Name = "textBoxSendGridApiKey";
            textBoxSendGridApiKey.Size = new Size(432, 27);
            textBoxSendGridApiKey.TabIndex = 3;
            // 
            // labelSendGridApiKey
            // 
            labelSendGridApiKey.AutoSize = true;
            labelSendGridApiKey.Location = new Point(38, 34);
            labelSendGridApiKey.Name = "labelSendGridApiKey";
            labelSendGridApiKey.Size = new Size(132, 20);
            labelSendGridApiKey.TabIndex = 1;
            labelSendGridApiKey.Text = "SendGrid API Key";
            // 
            // tabPageGmail
            // 
            tabPageGmail.BackColor = Color.Black;
            tabPageGmail.Controls.Add(groupBox1);
            tabPageGmail.ForeColor = Color.White;
            tabPageGmail.Location = new Point(4, 24);
            tabPageGmail.Name = "tabPageGmail";
            tabPageGmail.Padding = new Padding(3);
            tabPageGmail.Size = new Size(530, 583);
            tabPageGmail.TabIndex = 1;
            tabPageGmail.Text = "Google Gmail";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonConnectGmail);
            groupBox1.Controls.Add(labelGmailConnectionStatus);
            groupBox1.Controls.Add(checkBoxGmailDefault);
            groupBox1.Controls.Add(textBoxGmailSentFromAddress);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBoxGmailClientSecret);
            groupBox1.Controls.Add(labelGmailClientSecret);
            groupBox1.Controls.Add(textBoxGmailClientId);
            groupBox1.Controls.Add(labelGmailClientId);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(21, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(488, 549);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Google Gmail API";
            // 
            // buttonConnectGmail
            // 
            buttonConnectGmail.BackColor = Color.FromArgb(60, 60, 60);
            buttonConnectGmail.FlatAppearance.BorderSize = 0;
            buttonConnectGmail.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonConnectGmail.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonConnectGmail.FlatStyle = FlatStyle.Flat;
            buttonConnectGmail.Font = new Font("Segoe UI", 11F);
            buttonConnectGmail.Location = new Point(38, 302);
            buttonConnectGmail.Name = "buttonConnectGmail";
            buttonConnectGmail.Size = new Size(165, 34);
            buttonConnectGmail.TabIndex = 11;
            buttonConnectGmail.Text = "Connect Gmail";
            buttonConnectGmail.UseVisualStyleBackColor = false;
            buttonConnectGmail.Click += ButtonConnectGmail_Click;
            // 
            // labelGmailConnectionStatus
            // 
            labelGmailConnectionStatus.AutoSize = true;
            labelGmailConnectionStatus.Location = new Point(38, 352);
            labelGmailConnectionStatus.Name = "labelGmailConnectionStatus";
            labelGmailConnectionStatus.Size = new Size(112, 20);
            labelGmailConnectionStatus.TabIndex = 12;
            labelGmailConnectionStatus.Text = "Not connected";
            // 
            // checkBoxGmailDefault
            // 
            checkBoxGmailDefault.AutoSize = true;
            checkBoxGmailDefault.Location = new Point(271, 308);
            checkBoxGmailDefault.Name = "checkBoxGmailDefault";
            checkBoxGmailDefault.Size = new Size(174, 24);
            checkBoxGmailDefault.TabIndex = 6;
            checkBoxGmailDefault.Text = "Use Gmail as Default";
            checkBoxGmailDefault.UseVisualStyleBackColor = true;
            checkBoxGmailDefault.CheckedChanged += CheckBoxGmailDefault_CheckedChanged;
            // 
            // textBoxGmailSentFromAddress
            // 
            textBoxGmailSentFromAddress.Location = new Point(38, 250);
            textBoxGmailSentFromAddress.Name = "textBoxGmailSentFromAddress";
            textBoxGmailSentFromAddress.Size = new Size(407, 27);
            textBoxGmailSentFromAddress.TabIndex = 5;
            textBoxGmailSentFromAddress.Validating += textBoxEmailValidating_Validating;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 227);
            label2.Name = "label2";
            label2.Size = new Size(215, 20);
            label2.TabIndex = 5;
            label2.Text = "Gmail Account Email Address";
            // 
            // textBoxGmailClientSecret
            // 
            textBoxGmailClientSecret.Location = new Point(38, 148);
            textBoxGmailClientSecret.Multiline = true;
            textBoxGmailClientSecret.Name = "textBoxGmailClientSecret";
            textBoxGmailClientSecret.Size = new Size(407, 50);
            textBoxGmailClientSecret.TabIndex = 4;
            // 
            // labelGmailClientSecret
            // 
            labelGmailClientSecret.AutoSize = true;
            labelGmailClientSecret.Location = new Point(38, 125);
            labelGmailClientSecret.Name = "labelGmailClientSecret";
            labelGmailClientSecret.Size = new Size(208, 20);
            labelGmailClientSecret.TabIndex = 3;
            labelGmailClientSecret.Text = "OAuth Desktop Client Secret";
            // 
            // textBoxGmailClientId
            // 
            textBoxGmailClientId.Location = new Point(38, 57);
            textBoxGmailClientId.Multiline = true;
            textBoxGmailClientId.Name = "textBoxGmailClientId";
            textBoxGmailClientId.Size = new Size(407, 50);
            textBoxGmailClientId.TabIndex = 3;
            // 
            // labelGmailClientId
            // 
            labelGmailClientId.AutoSize = true;
            labelGmailClientId.Location = new Point(38, 34);
            labelGmailClientId.Name = "labelGmailClientId";
            labelGmailClientId.Size = new Size(181, 20);
            labelGmailClientId.TabIndex = 1;
            labelGmailClientId.Text = "OAuth Desktop Client ID";
            // 
            // tabPageOutlook
            // 
            tabPageOutlook.BackColor = Color.Black;
            tabPageOutlook.Controls.Add(groupBox2);
            tabPageOutlook.ForeColor = Color.White;
            tabPageOutlook.Location = new Point(4, 24);
            tabPageOutlook.Name = "tabPageOutlook";
            tabPageOutlook.Size = new Size(530, 583);
            tabPageOutlook.TabIndex = 2;
            tabPageOutlook.Text = "Microsoft Outlook 365";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(labelOutlookConnectionStatus);
            groupBox2.Controls.Add(buttonConnectOutlook);
            groupBox2.Controls.Add(textBoxOutlookTenantId);
            groupBox2.Controls.Add(labelOutlookTenantId);
            groupBox2.Controls.Add(checkBoxOutlookDefault);
            groupBox2.Controls.Add(textBoxOutlookSentFromAddress);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(textBoxOutlookClientId);
            groupBox2.Controls.Add(labelOutlookClientId);
            groupBox2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(21, 22);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(488, 549);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Microsoft Outlook OAuth";
            // 
            // labelOutlookConnectionStatus
            // 
            labelOutlookConnectionStatus.AutoSize = true;
            labelOutlookConnectionStatus.Location = new Point(29, 337);
            labelOutlookConnectionStatus.Name = "labelOutlookConnectionStatus";
            labelOutlookConnectionStatus.Size = new Size(112, 20);
            labelOutlookConnectionStatus.TabIndex = 11;
            labelOutlookConnectionStatus.Text = "Not connected";
            // 
            // buttonConnectOutlook
            // 
            buttonConnectOutlook.BackColor = Color.FromArgb(60, 60, 60);
            buttonConnectOutlook.FlatAppearance.BorderSize = 0;
            buttonConnectOutlook.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonConnectOutlook.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonConnectOutlook.FlatStyle = FlatStyle.Flat;
            buttonConnectOutlook.Font = new Font("Segoe UI", 10F);
            buttonConnectOutlook.ForeColor = Color.White;
            buttonConnectOutlook.Location = new Point(29, 282);
            buttonConnectOutlook.Name = "buttonConnectOutlook";
            buttonConnectOutlook.Size = new Size(165, 35);
            buttonConnectOutlook.TabIndex = 8;
            buttonConnectOutlook.Text = "Connect Outlook";
            buttonConnectOutlook.UseVisualStyleBackColor = false;
            buttonConnectOutlook.Click += ButtonConnectOutlook_Click;
            // 
            // textBoxOutlookTenantId
            // 
            textBoxOutlookTenantId.Location = new Point(29, 148);
            textBoxOutlookTenantId.Name = "textBoxOutlookTenantId";
            textBoxOutlookTenantId.Size = new Size(431, 27);
            textBoxOutlookTenantId.TabIndex = 5;
            textBoxOutlookTenantId.Text = "common";
            // 
            // labelOutlookTenantId
            // 
            labelOutlookTenantId.AutoSize = true;
            labelOutlookTenantId.Location = new Point(29, 125);
            labelOutlookTenantId.Name = "labelOutlookTenantId";
            labelOutlookTenantId.Size = new Size(314, 20);
            labelOutlookTenantId.TabIndex = 8;
            labelOutlookTenantId.Text = "Tenant ID (use common for most accounts)";
            // 
            // checkBoxOutlookDefault
            // 
            checkBoxOutlookDefault.AutoSize = true;
            checkBoxOutlookDefault.Location = new Point(215, 288);
            checkBoxOutlookDefault.Name = "checkBoxOutlookDefault";
            checkBoxOutlookDefault.Size = new Size(220, 24);
            checkBoxOutlookDefault.TabIndex = 7;
            checkBoxOutlookDefault.Text = "Use Outlook 365 as Default";
            checkBoxOutlookDefault.UseVisualStyleBackColor = true;
            checkBoxOutlookDefault.CheckedChanged += CheckBoxOutlookDefault_CheckedChanged;
            // 
            // textBoxOutlookSentFromAddress
            // 
            textBoxOutlookSentFromAddress.Location = new Point(29, 227);
            textBoxOutlookSentFromAddress.Name = "textBoxOutlookSentFromAddress";
            textBoxOutlookSentFromAddress.Size = new Size(431, 27);
            textBoxOutlookSentFromAddress.TabIndex = 6;
            textBoxOutlookSentFromAddress.Validating += textBoxEmailValidating_Validating;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 204);
            label1.Name = "label1";
            label1.Size = new Size(242, 20);
            label1.TabIndex = 5;
            label1.Text = "Microsoft Account Email Address";
            // 
            // textBoxOutlookClientId
            // 
            textBoxOutlookClientId.Location = new Point(29, 57);
            textBoxOutlookClientId.Multiline = true;
            textBoxOutlookClientId.Name = "textBoxOutlookClientId";
            textBoxOutlookClientId.Size = new Size(431, 50);
            textBoxOutlookClientId.TabIndex = 3;
            // 
            // labelOutlookClientId
            // 
            labelOutlookClientId.AutoSize = true;
            labelOutlookClientId.Location = new Point(29, 34);
            labelOutlookClientId.Name = "labelOutlookClientId";
            labelOutlookClientId.Size = new Size(163, 20);
            labelOutlookClientId.TabIndex = 1;
            labelOutlookClientId.Text = "Application (client) ID";
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.DialogResult = DialogResult.Ignore;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(320, 788);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 2;
            OK_Button.Text = "Save";
            OK_Button.UseVisualStyleBackColor = false;
            OK_Button.Click += OK_Button_Click;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel_Button.BackColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            Cancel_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Segoe UI", 12F);
            Cancel_Button.ForeColor = Color.White;
            Cancel_Button.Location = new Point(441, 788);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 3;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // comboBoxDefaultApi
            // 
            comboBoxDefaultApi.Font = new Font("Segoe UI", 11F);
            comboBoxDefaultApi.FormattingEnabled = true;
            comboBoxDefaultApi.Items.AddRange(new object[] { "Email Disabled", "Twilio SendGrid", "Google Gmail", "Microsoft Outlook 365" });
            comboBoxDefaultApi.Location = new Point(310, 111);
            comboBoxDefaultApi.Name = "comboBoxDefaultApi";
            comboBoxDefaultApi.Size = new Size(250, 28);
            comboBoxDefaultApi.TabIndex = 4;
            comboBoxDefaultApi.SelectedIndexChanged += ComboBoxDefaultApi_SelectedIndexChanged;
            // 
            // labelDefaultApi
            // 
            labelDefaultApi.AutoSize = true;
            labelDefaultApi.Font = new Font("Segoe UI", 11F);
            labelDefaultApi.ForeColor = Color.White;
            labelDefaultApi.Location = new Point(316, 82);
            labelDefaultApi.Name = "labelDefaultApi";
            labelDefaultApi.Size = new Size(149, 20);
            labelDefaultApi.TabIndex = 5;
            labelDefaultApi.Text = "Default API Selection";
            // 
            // comboBoxEmailConnection
            // 
            comboBoxEmailConnection.Font = new Font("Segoe UI", 11F);
            comboBoxEmailConnection.FormattingEnabled = true;
            comboBoxEmailConnection.Location = new Point(26, 111);
            comboBoxEmailConnection.Name = "comboBoxEmailConnection";
            comboBoxEmailConnection.Size = new Size(250, 28);
            comboBoxEmailConnection.TabIndex = 7;
            // 
            // labelConnection
            // 
            labelConnection.AutoSize = true;
            labelConnection.Font = new Font("Segoe UI", 11F);
            labelConnection.ForeColor = Color.White;
            labelConnection.Location = new Point(26, 82);
            labelConnection.Name = "labelConnection";
            labelConnection.Size = new Size(84, 20);
            labelConnection.TabIndex = 8;
            labelConnection.Text = "Connection";
            // 
            // SmtpApiSettingsDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(583, 861);
            Controls.Add(labelConnection);
            Controls.Add(comboBoxEmailConnection);
            Controls.Add(labelDefaultApi);
            Controls.Add(comboBoxDefaultApi);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(tabControlSmtpApiSettings);
            Controls.Add(panelHeeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "SmtpApiSettingsDialog";
            Text = "DesignToolsServer - SMTP API Settings";
            Load += SmtpCredentialsDialogLoad;
            Shown += SmtpCredentialsDialogShown;
            panelHeeader.ResumeLayout(false);
            panelHeeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            tabControlSmtpApiSettings.ResumeLayout(false);
            tabPageCompanyInfo.ResumeLayout(false);
            groupBoxCompanyInfo.ResumeLayout(false);
            groupBoxCompanyInfo.PerformLayout();
            tabPageSendGrid.ResumeLayout(false);
            groupBoxSendGridAPI.ResumeLayout(false);
            groupBoxSendGridAPI.PerformLayout();
            tabPageGmail.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPageOutlook.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeeader;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private TabControl tabControlSmtpApiSettings;
        private TabPage tabPageSendGrid;
        private TabPage tabPageGmail;
        private Button OK_Button;
        private Button Cancel_Button;
        private TabPage tabPageOutlook;
        private GroupBox groupBoxSendGridAPI;
        private TextBox textBoxSendGridApiKey;
        private Label labelSendGridApiKey;
        private TextBox textBoxSendGridSentFrom;
        private Label labelSendGridSentFrom;
        private GroupBox groupBox1;
        private TextBox textBoxGmailSentFromAddress;
        private Label label2;
        private TextBox textBoxGmailClientSecret;
        private Label labelGmailClientSecret;
        private TextBox textBoxGmailClientId;
        private Label labelGmailClientId;
        private CheckBox checkBoxSendGridDefault;
        private CheckBox checkBoxGmailDefault;
        private GroupBox groupBox2;
        private Label labelOutlookConnectionStatus;
        private Button buttonConnectOutlook;
        private CheckBox checkBoxOutlookDefault;
        private TextBox textBoxOutlookSentFromAddress;
        private Label label1;
        private TextBox textBoxOutlookClientId;
        private Label labelOutlookClientId;
        private TextBox textBoxOutlookTenantId;
        private Label labelOutlookTenantId;
        private TabPage tabPageCompanyInfo;
        private GroupBox groupBoxCompanyInfo;
        private MaskedTextBox maskedTextBoxPostalCode;
        private Label labelPostalCode;
        private Label labelRegion;
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
        private ComboBox comboBoxRegion;
        private ErrorProvider errorProvider1;
        private Label labelDefaultApi;
        private ComboBox comboBoxDefaultApi;
        private Button buttonConnectGmail;
        private Label labelGmailConnectionStatus;
        private Label labelConnection;
        private ComboBox comboBoxEmailConnection;
    }
}
