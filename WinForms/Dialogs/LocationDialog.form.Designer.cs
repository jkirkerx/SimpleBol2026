namespace SimpleBol.WinForms.Dialogs
{
    partial class LocationDialog
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
            OK_Button = new Button();
            Cancel_Button = new Button();
            labelComments = new Label();
            textBoxComments = new TextBox();
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            groupBoxShippingLocation = new GroupBox();
            buttonHoursOfOperation = new Button();
            comboBoxTimeZone = new ComboBox();
            labelTimeZone = new Label();
            maskedTextBoxPostalCode = new MaskedTextBox();
            labelPostalCode = new Label();
            labelRegion = new Label();
            comboBoxRegion = new ComboBox();
            labelCountry = new Label();
            comboBoxCountry = new ComboBox();
            textBoxCode = new TextBox();
            labelCode = new Label();
            textBoxCity = new TextBox();
            labelCity = new Label();
            textBoxAddress2 = new TextBox();
            labelAddress2 = new Label();
            textBoxAddress1 = new TextBox();
            labelAddress1 = new Label();
            textBoxName = new TextBox();
            labelName = new Label();
            groupBoxLoadingOptions = new GroupBox();
            checkBoxPalletJackRequired = new CheckBox();
            checkBoxLiftgateRequired = new CheckBox();
            groupBoxContactInformation = new GroupBox();
            maskedTextBoxContactMobilePhone = new MaskedTextBox();
            labelContactMobilePhone = new Label();
            maskedTextBoxContactPhone = new MaskedTextBox();
            textBoxContactEmailAddress = new TextBox();
            label6 = new Label();
            labelContactPhone = new Label();
            textBoxContactName = new TextBox();
            label5 = new Label();
            errorProvider1 = new ErrorProvider(components);
            checkBoxNormalHours = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxShippingLocation.SuspendLayout();
            groupBoxLoadingOptions.SuspendLayout();
            groupBoxContactInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
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
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(652, 638);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 20;
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
            Cancel_Button.Location = new Point(780, 638);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 21;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F);
            labelComments.ForeColor = Color.White;
            labelComments.Location = new Point(24, 605);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(80, 20);
            labelComments.TabIndex = 42;
            labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxComments.Location = new Point(24, 638);
            textBoxComments.Margin = new Padding(3, 4, 3, 4);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(561, 51);
            textBoxComments.TabIndex = 19;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(Lbl_Header);
            panel1.Controls.Add(PbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 7, 3, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(915, 70);
            panel1.TabIndex = 43;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(369, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Shipping Location Editor";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = Properties.Resources.BtDistribution;
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 7, 3, 7);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // groupBoxShippingLocation
            // 
            groupBoxShippingLocation.Controls.Add(checkBoxNormalHours);
            groupBoxShippingLocation.Controls.Add(buttonHoursOfOperation);
            groupBoxShippingLocation.Controls.Add(comboBoxTimeZone);
            groupBoxShippingLocation.Controls.Add(labelTimeZone);
            groupBoxShippingLocation.Controls.Add(maskedTextBoxPostalCode);
            groupBoxShippingLocation.Controls.Add(labelPostalCode);
            groupBoxShippingLocation.Controls.Add(labelRegion);
            groupBoxShippingLocation.Controls.Add(comboBoxRegion);
            groupBoxShippingLocation.Controls.Add(labelCountry);
            groupBoxShippingLocation.Controls.Add(comboBoxCountry);
            groupBoxShippingLocation.Controls.Add(textBoxCode);
            groupBoxShippingLocation.Controls.Add(labelCode);
            groupBoxShippingLocation.Controls.Add(textBoxCity);
            groupBoxShippingLocation.Controls.Add(labelCity);
            groupBoxShippingLocation.Controls.Add(textBoxAddress2);
            groupBoxShippingLocation.Controls.Add(labelAddress2);
            groupBoxShippingLocation.Controls.Add(textBoxAddress1);
            groupBoxShippingLocation.Controls.Add(labelAddress1);
            groupBoxShippingLocation.Controls.Add(textBoxName);
            groupBoxShippingLocation.Controls.Add(labelName);
            groupBoxShippingLocation.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxShippingLocation.ForeColor = Color.White;
            groupBoxShippingLocation.Location = new Point(24, 101);
            groupBoxShippingLocation.Margin = new Padding(3, 4, 3, 4);
            groupBoxShippingLocation.Name = "groupBoxShippingLocation";
            groupBoxShippingLocation.Padding = new Padding(3, 4, 3, 4);
            groupBoxShippingLocation.Size = new Size(561, 488);
            groupBoxShippingLocation.TabIndex = 1;
            groupBoxShippingLocation.TabStop = false;
            groupBoxShippingLocation.Text = "Location Information";
            // 
            // buttonHoursOfOperation
            // 
            buttonHoursOfOperation.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonHoursOfOperation.BackColor = Color.FromArgb(60, 60, 60);
            buttonHoursOfOperation.CausesValidation = false;
            buttonHoursOfOperation.Cursor = Cursors.Hand;
            buttonHoursOfOperation.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonHoursOfOperation.FlatAppearance.BorderSize = 0;
            buttonHoursOfOperation.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonHoursOfOperation.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonHoursOfOperation.FlatStyle = FlatStyle.Flat;
            buttonHoursOfOperation.Font = new Font("Segoe UI", 12F);
            buttonHoursOfOperation.ForeColor = Color.White;
            buttonHoursOfOperation.Location = new Point(296, 409);
            buttonHoursOfOperation.Margin = new Padding(3, 7, 3, 7);
            buttonHoursOfOperation.Name = "buttonHoursOfOperation";
            buttonHoursOfOperation.Size = new Size(240, 51);
            buttonHoursOfOperation.TabIndex = 34;
            buttonHoursOfOperation.Text = "Custom Hours of Operation";
            buttonHoursOfOperation.UseVisualStyleBackColor = false;
            buttonHoursOfOperation.Click += ButtonHoursOfOperation_Click;
            // 
            // comboBoxTimeZone
            // 
            comboBoxTimeZone.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            comboBoxTimeZone.FormattingEnabled = true;
            comboBoxTimeZone.Location = new Point(296, 274);
            comboBoxTimeZone.Name = "comboBoxTimeZone";
            comboBoxTimeZone.Size = new Size(240, 29);
            comboBoxTimeZone.TabIndex = 10;
            // 
            // labelTimeZone
            // 
            labelTimeZone.AutoSize = true;
            labelTimeZone.Font = new Font("Segoe UI", 11F);
            labelTimeZone.Location = new Point(296, 247);
            labelTimeZone.Name = "labelTimeZone";
            labelTimeZone.Size = new Size(80, 20);
            labelTimeZone.TabIndex = 33;
            labelTimeZone.Text = "Time Zone";
            // 
            // maskedTextBoxPostalCode
            // 
            maskedTextBoxPostalCode.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            maskedTextBoxPostalCode.Location = new Point(296, 68);
            maskedTextBoxPostalCode.Mask = "00000-9999";
            maskedTextBoxPostalCode.Name = "maskedTextBoxPostalCode";
            maskedTextBoxPostalCode.Size = new Size(240, 29);
            maskedTextBoxPostalCode.TabIndex = 7;
            maskedTextBoxPostalCode.Enter += MaskedTextBoxPostalCode_Enter;
            // 
            // labelPostalCode
            // 
            labelPostalCode.AutoSize = true;
            labelPostalCode.Font = new Font("Segoe UI", 11F);
            labelPostalCode.ForeColor = Color.White;
            labelPostalCode.Location = new Point(296, 42);
            labelPostalCode.Name = "labelPostalCode";
            labelPostalCode.Size = new Size(157, 20);
            labelPostalCode.TabIndex = 31;
            labelPostalCode.Text = "Postal Code (required)";
            // 
            // labelRegion
            // 
            labelRegion.AutoSize = true;
            labelRegion.Font = new Font("Segoe UI", 11F);
            labelRegion.ForeColor = Color.White;
            labelRegion.Location = new Point(296, 178);
            labelRegion.Name = "labelRegion";
            labelRegion.Size = new Size(161, 20);
            labelRegion.TabIndex = 30;
            labelRegion.Text = "RegionCode (required)";
            // 
            // comboBoxRegion
            // 
            comboBoxRegion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            comboBoxRegion.FormattingEnabled = true;
            comboBoxRegion.Location = new Point(296, 204);
            comboBoxRegion.Name = "comboBoxRegion";
            comboBoxRegion.Size = new Size(240, 29);
            comboBoxRegion.TabIndex = 9;
            // 
            // labelCountry
            // 
            labelCountry.AutoSize = true;
            labelCountry.Font = new Font("Segoe UI", 11F);
            labelCountry.ForeColor = Color.White;
            labelCountry.Location = new Point(296, 109);
            labelCountry.Name = "labelCountry";
            labelCountry.Size = new Size(165, 20);
            labelCountry.TabIndex = 29;
            labelCountry.Text = "CountryCode (required)";
            // 
            // comboBoxCountry
            // 
            comboBoxCountry.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            comboBoxCountry.FormattingEnabled = true;
            comboBoxCountry.Location = new Point(296, 135);
            comboBoxCountry.Name = "comboBoxCountry";
            comboBoxCountry.Size = new Size(240, 29);
            comboBoxCountry.TabIndex = 8;
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
            // 
            // textBoxCode
            // 
            textBoxCode.Location = new Point(25, 69);
            textBoxCode.Name = "textBoxCode";
            textBoxCode.Size = new Size(240, 27);
            textBoxCode.TabIndex = 2;
            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Font = new Font("Segoe UI", 11F);
            labelCode.Location = new Point(25, 42);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(105, 20);
            labelCode.TabIndex = 8;
            labelCode.Text = "Location Code";
            // 
            // textBoxCity
            // 
            textBoxCity.Location = new Point(25, 345);
            textBoxCity.Name = "textBoxCity";
            textBoxCity.Size = new Size(240, 27);
            textBoxCity.TabIndex = 6;
            // 
            // labelCity
            // 
            labelCity.AutoSize = true;
            labelCity.Font = new Font("Segoe UI", 11F);
            labelCity.Location = new Point(30, 315);
            labelCity.Name = "labelCity";
            labelCity.Size = new Size(34, 20);
            labelCity.TabIndex = 6;
            labelCity.Text = "City";
            // 
            // textBoxAddress2
            // 
            textBoxAddress2.Location = new Point(25, 274);
            textBoxAddress2.Name = "textBoxAddress2";
            textBoxAddress2.Size = new Size(240, 27);
            textBoxAddress2.TabIndex = 5;
            // 
            // labelAddress2
            // 
            labelAddress2.AutoSize = true;
            labelAddress2.Font = new Font("Segoe UI", 11F);
            labelAddress2.Location = new Point(25, 247);
            labelAddress2.Name = "labelAddress2";
            labelAddress2.Size = new Size(117, 20);
            labelAddress2.TabIndex = 4;
            labelAddress2.Text = "Street Address 2";
            // 
            // textBoxAddress1
            // 
            textBoxAddress1.Location = new Point(25, 205);
            textBoxAddress1.Name = "textBoxAddress1";
            textBoxAddress1.Size = new Size(240, 27);
            textBoxAddress1.TabIndex = 4;
            // 
            // labelAddress1
            // 
            labelAddress1.AutoSize = true;
            labelAddress1.Font = new Font("Segoe UI", 11F);
            labelAddress1.Location = new Point(25, 178);
            labelAddress1.Name = "labelAddress1";
            labelAddress1.Size = new Size(117, 20);
            labelAddress1.TabIndex = 2;
            labelAddress1.Text = "Street Address 1";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(25, 136);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(240, 27);
            textBoxName.TabIndex = 3;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Segoe UI", 11F);
            labelName.Location = new Point(25, 109);
            labelName.Name = "labelName";
            labelName.Size = new Size(110, 20);
            labelName.TabIndex = 0;
            labelName.Text = "Location Name";
            // 
            // groupBoxLoadingOptions
            // 
            groupBoxLoadingOptions.Controls.Add(checkBoxPalletJackRequired);
            groupBoxLoadingOptions.Controls.Add(checkBoxLiftgateRequired);
            groupBoxLoadingOptions.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxLoadingOptions.ForeColor = Color.White;
            groupBoxLoadingOptions.Location = new Point(611, 101);
            groupBoxLoadingOptions.Name = "groupBoxLoadingOptions";
            groupBoxLoadingOptions.Size = new Size(279, 115);
            groupBoxLoadingOptions.TabIndex = 11;
            groupBoxLoadingOptions.TabStop = false;
            groupBoxLoadingOptions.Text = "Location Requirements";
            // 
            // checkBoxPalletJackRequired
            // 
            checkBoxPalletJackRequired.AutoSize = true;
            checkBoxPalletJackRequired.Location = new Point(30, 73);
            checkBoxPalletJackRequired.Name = "checkBoxPalletJackRequired";
            checkBoxPalletJackRequired.Size = new Size(168, 24);
            checkBoxPalletJackRequired.TabIndex = 13;
            checkBoxPalletJackRequired.Text = "Pallet Jack Required";
            checkBoxPalletJackRequired.UseVisualStyleBackColor = true;
            // 
            // checkBoxLiftgateRequired
            // 
            checkBoxLiftgateRequired.AutoSize = true;
            checkBoxLiftgateRequired.Location = new Point(30, 33);
            checkBoxLiftgateRequired.Name = "checkBoxLiftgateRequired";
            checkBoxLiftgateRequired.Size = new Size(213, 24);
            checkBoxLiftgateRequired.TabIndex = 12;
            checkBoxLiftgateRequired.Text = "LiftgateRequired Required";
            checkBoxLiftgateRequired.UseVisualStyleBackColor = true;
            // 
            // groupBoxContactInformation
            // 
            groupBoxContactInformation.Controls.Add(maskedTextBoxContactMobilePhone);
            groupBoxContactInformation.Controls.Add(labelContactMobilePhone);
            groupBoxContactInformation.Controls.Add(maskedTextBoxContactPhone);
            groupBoxContactInformation.Controls.Add(textBoxContactEmailAddress);
            groupBoxContactInformation.Controls.Add(label6);
            groupBoxContactInformation.Controls.Add(labelContactPhone);
            groupBoxContactInformation.Controls.Add(textBoxContactName);
            groupBoxContactInformation.Controls.Add(label5);
            groupBoxContactInformation.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxContactInformation.ForeColor = Color.White;
            groupBoxContactInformation.Location = new Point(611, 236);
            groupBoxContactInformation.Name = "groupBoxContactInformation";
            groupBoxContactInformation.Size = new Size(280, 353);
            groupBoxContactInformation.TabIndex = 14;
            groupBoxContactInformation.TabStop = false;
            groupBoxContactInformation.Text = "Contact Information";
            // 
            // maskedTextBoxContactMobilePhone
            // 
            maskedTextBoxContactMobilePhone.Location = new Point(19, 210);
            maskedTextBoxContactMobilePhone.Mask = "(999) 000-0000";
            maskedTextBoxContactMobilePhone.Name = "maskedTextBoxContactMobilePhone";
            maskedTextBoxContactMobilePhone.Size = new Size(240, 27);
            maskedTextBoxContactMobilePhone.TabIndex = 17;
            maskedTextBoxContactMobilePhone.Enter += MaskedTextBoxContactMobilePhone_Enter;
            maskedTextBoxContactMobilePhone.KeyPress += MaskedTextBoxContactMobilePhone_KeyPress;
            // 
            // labelContactMobilePhone
            // 
            labelContactMobilePhone.AutoSize = true;
            labelContactMobilePhone.Font = new Font("Segoe UI", 11F);
            labelContactMobilePhone.Location = new Point(19, 181);
            labelContactMobilePhone.Name = "labelContactMobilePhone";
            labelContactMobilePhone.Size = new Size(111, 20);
            labelContactMobilePhone.TabIndex = 42;
            labelContactMobilePhone.Text = "Phone (Mobile)";
            // 
            // maskedTextBoxContactPhone
            // 
            maskedTextBoxContactPhone.Location = new Point(19, 141);
            maskedTextBoxContactPhone.Mask = "(999) 000-0000";
            maskedTextBoxContactPhone.Name = "maskedTextBoxContactPhone";
            maskedTextBoxContactPhone.Size = new Size(240, 27);
            maskedTextBoxContactPhone.TabIndex = 16;
            maskedTextBoxContactPhone.Enter += MaskedTextBoxContactPhone_Enter;
            maskedTextBoxContactPhone.KeyPress += MaskedTextBoxContactPhone_KeyPress;
            // 
            // textBoxContactEmailAddress
            // 
            textBoxContactEmailAddress.Location = new Point(19, 287);
            textBoxContactEmailAddress.Name = "textBoxContactEmailAddress";
            textBoxContactEmailAddress.Size = new Size(240, 27);
            textBoxContactEmailAddress.TabIndex = 18;
            textBoxContactEmailAddress.Validating += textBoxEmailValidating_Validating;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F);
            label6.Location = new Point(19, 257);
            label6.Name = "label6";
            label6.Size = new Size(156, 20);
            label6.TabIndex = 40;
            label6.Text = "EmailAddress Address";
            // 
            // labelContactPhone
            // 
            labelContactPhone.AutoSize = true;
            labelContactPhone.Font = new Font("Segoe UI", 11F);
            labelContactPhone.Location = new Point(19, 112);
            labelContactPhone.Name = "labelContactPhone";
            labelContactPhone.Size = new Size(127, 20);
            labelContactPhone.TabIndex = 38;
            labelContactPhone.Text = "Phone (Land LIne)";
            // 
            // textBoxContactName
            // 
            textBoxContactName.Location = new Point(19, 70);
            textBoxContactName.Name = "textBoxContactName";
            textBoxContactName.Size = new Size(240, 27);
            textBoxContactName.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(19, 43);
            label5.Name = "label5";
            label5.Size = new Size(49, 20);
            label5.TabIndex = 36;
            label5.Text = "Name";
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // checkBoxNormalHours
            // 
            checkBoxNormalHours.AutoSize = true;
            checkBoxNormalHours.Location = new Point(296, 345);
            checkBoxNormalHours.Name = "checkBoxNormalHours";
            checkBoxNormalHours.Size = new Size(226, 24);
            checkBoxNormalHours.TabIndex = 35;
            checkBoxNormalHours.Text = "Normal - M-F-  8AM to 5PM";
            checkBoxNormalHours.UseVisualStyleBackColor = true;
            // 
            // LocationDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            CancelButton = Cancel_Button;
            ClientSize = new Size(915, 708);
            Controls.Add(groupBoxContactInformation);
            Controls.Add(groupBoxLoadingOptions);
            Controls.Add(groupBoxShippingLocation);
            Controls.Add(panel1);
            Controls.Add(labelComments);
            Controls.Add(textBoxComments);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            Name = "LocationDialog";
            Text = "DesignToolsServer - Shipping Location Editor";
            TopMost = true;
            Load += LocationDialog_Load;
            Shown += LocationDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxShippingLocation.ResumeLayout(false);
            groupBoxShippingLocation.PerformLayout();
            groupBoxLoadingOptions.ResumeLayout(false);
            groupBoxLoadingOptions.PerformLayout();
            groupBoxContactInformation.ResumeLayout(false);
            groupBoxContactInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OK_Button;
        private Button Cancel_Button;
        private Label labelComments;
        private TextBox textBoxComments;
        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private GroupBox groupBoxShippingLocation;
        private TextBox textBoxCode;
        private Label labelCode;
        private TextBox textBoxCity;
        private Label labelCity;
        private TextBox textBoxAddress2;
        private Label labelAddress2;
        private TextBox textBoxAddress1;
        private Label labelAddress1;
        private TextBox textBoxName;
        private Label labelName;
        private GroupBox groupBoxLoadingOptions;
        private CheckBox checkBoxLiftgateRequired;
        private MaskedTextBox maskedTextBoxPostalCode;
        private Label labelPostalCode;
        private Label labelRegion;
        private ComboBox comboBoxRegion;
        private Label labelCountry;
        private ComboBox comboBoxCountry;
        private GroupBox groupBoxContactInformation;
        private TextBox textBoxContactEmailAddress;
        private Label label6;
        private Label labelContactPhone;
        private TextBox textBoxContactName;
        private Label label5;
        private CheckBox checkBoxPalletJackRequired;
        private ComboBox comboBoxTimeZone;
        private Label labelTimeZone;
        private MaskedTextBox maskedTextBoxContactPhone;
        private MaskedTextBox maskedTextBoxContactMobilePhone;
        private Label labelContactMobilePhone;
        private ErrorProvider errorProvider1;
        private Button buttonHoursOfOperation;
        private CheckBox checkBoxNormalHours;
    }
}
