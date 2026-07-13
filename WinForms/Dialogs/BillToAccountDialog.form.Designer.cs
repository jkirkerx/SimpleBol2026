namespace SimpleBol.WinForms.Dialogs
{
    partial class BillToAccountDialog
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
            OK_Button = new Button();
            Cancel_Button = new Button();
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            groupBoxAccountInfo = new GroupBox();
            labelBindToCustomer = new Label();
            comboBoxBindToCustomer = new ComboBox();
            textBoxAccountNumber = new TextBox();
            labelAccountNumber = new Label();
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
            groupBoxContactInfo = new GroupBox();
            textBoxContactName = new TextBox();
            labelContactName = new Label();
            maskedTextBoxEmailAddress1 = new MaskedTextBox();
            maskedTextBoxPhoneNumber1 = new MaskedTextBox();
            labelEmailAddress1 = new Label();
            labelPhoneNumber1 = new Label();
            labelComments = new Label();
            textBoxComments = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxAccountInfo.SuspendLayout();
            groupBoxCompanyInfo.SuspendLayout();
            groupBoxContactInfo.SuspendLayout();
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
            OK_Button.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(572, 681);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 15;
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
            Cancel_Button.Location = new Point(700, 681);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 16;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
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
            panel1.Size = new Size(846, 70);
            panel1.TabIndex = 47;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(332, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Bill To 3rd Party Editor";
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
            // groupBoxAccountInfo
            // 
            groupBoxAccountInfo.Controls.Add(labelBindToCustomer);
            groupBoxAccountInfo.Controls.Add(comboBoxBindToCustomer);
            groupBoxAccountInfo.Controls.Add(textBoxAccountNumber);
            groupBoxAccountInfo.Controls.Add(labelAccountNumber);
            groupBoxAccountInfo.Font = new Font("Segoe UI Black", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxAccountInfo.ForeColor = Color.White;
            groupBoxAccountInfo.Location = new Point(440, 87);
            groupBoxAccountInfo.Name = "groupBoxAccountInfo";
            groupBoxAccountInfo.Size = new Size(375, 227);
            groupBoxAccountInfo.TabIndex = 8;
            groupBoxAccountInfo.TabStop = false;
            groupBoxAccountInfo.Text = "Account Information";
            // 
            // labelBindToCustomer
            // 
            labelBindToCustomer.AutoSize = true;
            labelBindToCustomer.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelBindToCustomer.ForeColor = Color.White;
            labelBindToCustomer.Location = new Point(28, 105);
            labelBindToCustomer.Name = "labelBindToCustomer";
            labelBindToCustomer.Size = new Size(182, 20);
            labelBindToCustomer.TabIndex = 24;
            labelBindToCustomer.Text = "Bind to Customer Account";
            // 
            // comboBoxBindToCustomer
            // 
            comboBoxBindToCustomer.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxBindToCustomer.FormattingEnabled = true;
            comboBoxBindToCustomer.Location = new Point(29, 133);
            comboBoxBindToCustomer.Name = "comboBoxBindToCustomer";
            comboBoxBindToCustomer.Size = new Size(319, 29);
            comboBoxBindToCustomer.TabIndex = 23;
            comboBoxBindToCustomer.SelectedIndexChanged += ComboBoxBindToCustomer_SelectedIndexChanged;
            // 
            // textBoxAccountNumber
            // 
            textBoxAccountNumber.Location = new Point(28, 64);
            textBoxAccountNumber.Name = "textBoxAccountNumber";
            textBoxAccountNumber.Size = new Size(320, 28);
            textBoxAccountNumber.TabIndex = 9;
            // 
            // labelAccountNumber
            // 
            labelAccountNumber.AutoSize = true;
            labelAccountNumber.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelAccountNumber.Location = new Point(28, 37);
            labelAccountNumber.Name = "labelAccountNumber";
            labelAccountNumber.Size = new Size(124, 20);
            labelAccountNumber.TabIndex = 0;
            labelAccountNumber.Text = "Account Number:";
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
            groupBoxCompanyInfo.Location = new Point(32, 87);
            groupBoxCompanyInfo.Name = "groupBoxCompanyInfo";
            groupBoxCompanyInfo.Size = new Size(375, 546);
            groupBoxCompanyInfo.TabIndex = 0;
            groupBoxCompanyInfo.TabStop = false;
            groupBoxCompanyInfo.Text = "Bill To Address";
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
            // groupBoxContactInfo
            // 
            groupBoxContactInfo.Controls.Add(textBoxContactName);
            groupBoxContactInfo.Controls.Add(labelContactName);
            groupBoxContactInfo.Controls.Add(maskedTextBoxEmailAddress1);
            groupBoxContactInfo.Controls.Add(maskedTextBoxPhoneNumber1);
            groupBoxContactInfo.Controls.Add(labelEmailAddress1);
            groupBoxContactInfo.Controls.Add(labelPhoneNumber1);
            groupBoxContactInfo.Font = new Font("Segoe UI Black", 11F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxContactInfo.ForeColor = Color.White;
            groupBoxContactInfo.Location = new Point(440, 354);
            groupBoxContactInfo.Name = "groupBoxContactInfo";
            groupBoxContactInfo.Size = new Size(375, 279);
            groupBoxContactInfo.TabIndex = 10;
            groupBoxContactInfo.TabStop = false;
            groupBoxContactInfo.Text = "Contact Information";
            // 
            // textBoxContactName
            // 
            textBoxContactName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxContactName.Location = new Point(28, 76);
            textBoxContactName.Name = "textBoxContactName";
            textBoxContactName.Size = new Size(320, 29);
            textBoxContactName.TabIndex = 11;
            // 
            // labelContactName
            // 
            labelContactName.AutoSize = true;
            labelContactName.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelContactName.ForeColor = Color.White;
            labelContactName.Location = new Point(28, 50);
            labelContactName.Name = "labelContactName";
            labelContactName.Size = new Size(174, 20);
            labelContactName.TabIndex = 37;
            labelContactName.Text = "Contact Name (required)";
            // 
            // maskedTextBoxEmailAddress1
            // 
            maskedTextBoxEmailAddress1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxEmailAddress1.Location = new Point(28, 217);
            maskedTextBoxEmailAddress1.Name = "maskedTextBoxEmailAddress1";
            maskedTextBoxEmailAddress1.Size = new Size(320, 29);
            maskedTextBoxEmailAddress1.TabIndex = 13;
            // 
            // maskedTextBoxPhoneNumber1
            // 
            maskedTextBoxPhoneNumber1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPhoneNumber1.Location = new Point(28, 146);
            maskedTextBoxPhoneNumber1.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber1.Name = "maskedTextBoxPhoneNumber1";
            maskedTextBoxPhoneNumber1.Size = new Size(320, 29);
            maskedTextBoxPhoneNumber1.TabIndex = 12;
            maskedTextBoxPhoneNumber1.Enter += MaskedTextBoxPhoneNumber1_Enter;
            maskedTextBoxPhoneNumber1.KeyPress += MaskedTextBoxPhoneNumber1_KeyPress;
            // 
            // labelEmailAddress1
            // 
            labelEmailAddress1.AutoSize = true;
            labelEmailAddress1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelEmailAddress1.ForeColor = Color.White;
            labelEmailAddress1.Location = new Point(28, 189);
            labelEmailAddress1.Name = "labelEmailAddress1";
            labelEmailAddress1.Size = new Size(263, 20);
            labelEmailAddress1.TabIndex = 35;
            labelEmailAddress1.Text = "Main EmailAddress Address (required)";
            // 
            // labelPhoneNumber1
            // 
            labelPhoneNumber1.AutoSize = true;
            labelPhoneNumber1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPhoneNumber1.ForeColor = Color.White;
            labelPhoneNumber1.Location = new Point(28, 118);
            labelPhoneNumber1.Name = "labelPhoneNumber1";
            labelPhoneNumber1.Size = new Size(215, 20);
            labelPhoneNumber1.TabIndex = 34;
            labelPhoneNumber1.Text = "Main Phone Number (required)";
            // 
            // labelComments
            // 
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelComments.ForeColor = Color.White;
            labelComments.Location = new Point(34, 653);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(83, 20);
            labelComments.TabIndex = 53;
            labelComments.Text = "Comments:";
            // 
            // textBoxComments
            // 
            textBoxComments.Font = new Font("Segoe UI Black", 11F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxComments.Location = new Point(34, 681);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(373, 51);
            textBoxComments.TabIndex = 14;
            // 
            // BillToAccountDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            CancelButton = Cancel_Button;
            ClientSize = new Size(846, 748);
            Controls.Add(textBoxComments);
            Controls.Add(labelComments);
            Controls.Add(groupBoxContactInfo);
            Controls.Add(groupBoxCompanyInfo);
            Controls.Add(groupBoxAccountInfo);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            Name = "BillToAccountDialog";
            Text = AppInfo.WindowTitle("Bill to Account");
            Load += BillToAccountDialog_Load;
            Shown += BillToAccountDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxAccountInfo.ResumeLayout(false);
            groupBoxAccountInfo.PerformLayout();
            groupBoxCompanyInfo.ResumeLayout(false);
            groupBoxCompanyInfo.PerformLayout();
            groupBoxContactInfo.ResumeLayout(false);
            groupBoxContactInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OK_Button;
        private Button Cancel_Button;
        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private GroupBox groupBoxAccountInfo;
        private TextBox textBoxAccountNumber;
        private Label labelAccountNumber;
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
        private GroupBox groupBoxContactInfo;
        private MaskedTextBox maskedTextBoxEmailAddress1;
        private MaskedTextBox maskedTextBoxPhoneNumber1;
        private Label labelEmailAddress1;
        private Label labelPhoneNumber1;
        private TextBox textBoxContactName;
        private Label labelContactName;
        private Label labelComments;
        private TextBox textBoxComments;
        private Label labelBindToCustomer;
        private ComboBox comboBoxBindToCustomer;
    }
}
