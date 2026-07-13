namespace SimpleBol.WinForms.Dialogs
{
    partial class AccountDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountDialog));
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBoxUserInformation = new GroupBox();
            comboBoxTimeZone = new ComboBox();
            labelTimeZone = new Label();
            maskedTextBoxPhoneNumber = new MaskedTextBox();
            labelPhoneNUmber = new Label();
            textBoxEmailAddress = new TextBox();
            labelEmailAddress = new Label();
            textBoxAccountName = new TextBox();
            labelAccountName = new Label();
            textBoxLoginId = new TextBox();
            labelAccountId = new Label();
            groupBox1 = new GroupBox();
            labelPasswordHelp = new Label();
            checkBoxMarkedAsDeleted = new CheckBox();
            textBoxLocation = new TextBox();
            labelLocation = new Label();
            buttonSetPassword = new Button();
            textBoxPassword = new TextBox();
            labelPassword = new Label();
            comboBoxSecurityLevels = new ComboBox();
            labelSecurityLevels = new Label();
            labelComments = new Label();
            textBoxComments = new TextBox();
            errorProvider1 = new ErrorProvider(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxUserInformation.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
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
            panel1.Size = new Size(800, 70);
            panel1.TabIndex = 44;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(342, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Account or User Editor";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = (Image)resources.GetObject("PbLogo.Image");
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 7, 3, 7);
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
            OK_Button.Location = new Point(525, 590);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 13;
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
            Cancel_Button.Location = new Point(653, 590);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 14;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // groupBoxUserInformation
            // 
            groupBoxUserInformation.Controls.Add(comboBoxTimeZone);
            groupBoxUserInformation.Controls.Add(labelTimeZone);
            groupBoxUserInformation.Controls.Add(maskedTextBoxPhoneNumber);
            groupBoxUserInformation.Controls.Add(labelPhoneNUmber);
            groupBoxUserInformation.Controls.Add(textBoxEmailAddress);
            groupBoxUserInformation.Controls.Add(labelEmailAddress);
            groupBoxUserInformation.Controls.Add(textBoxAccountName);
            groupBoxUserInformation.Controls.Add(labelAccountName);
            groupBoxUserInformation.Controls.Add(textBoxLoginId);
            groupBoxUserInformation.Controls.Add(labelAccountId);
            groupBoxUserInformation.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxUserInformation.ForeColor = Color.White;
            groupBoxUserInformation.Location = new Point(32, 104);
            groupBoxUserInformation.Name = "groupBoxUserInformation";
            groupBoxUserInformation.Size = new Size(350, 437);
            groupBoxUserInformation.TabIndex = 0;
            groupBoxUserInformation.TabStop = false;
            groupBoxUserInformation.Text = "User Information";
            // 
            // comboBoxTimeZone
            // 
            comboBoxTimeZone.FormattingEnabled = true;
            comboBoxTimeZone.Location = new Point(19, 392);
            comboBoxTimeZone.Name = "comboBoxTimeZone";
            comboBoxTimeZone.Size = new Size(311, 28);
            comboBoxTimeZone.TabIndex = 5;
            // 
            // labelTimeZone
            // 
            labelTimeZone.AutoSize = true;
            labelTimeZone.Location = new Point(19, 358);
            labelTimeZone.Name = "labelTimeZone";
            labelTimeZone.Size = new Size(83, 20);
            labelTimeZone.TabIndex = 10;
            labelTimeZone.Text = "Time Zone";
            // 
            // maskedTextBoxPhoneNumber
            // 
            maskedTextBoxPhoneNumber.Location = new Point(19, 231);
            maskedTextBoxPhoneNumber.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber.Name = "maskedTextBoxPhoneNumber";
            maskedTextBoxPhoneNumber.Size = new Size(311, 27);
            maskedTextBoxPhoneNumber.TabIndex = 3;
            maskedTextBoxPhoneNumber.Enter += MaskedTextBoxPhone_Enter;
            // 
            // labelPhoneNUmber
            // 
            labelPhoneNUmber.AutoSize = true;
            labelPhoneNUmber.Location = new Point(19, 203);
            labelPhoneNUmber.Name = "labelPhoneNUmber";
            labelPhoneNUmber.Size = new Size(115, 20);
            labelPhoneNUmber.TabIndex = 6;
            labelPhoneNUmber.Text = "Phone Number";
            // 
            // textBoxEmailAddress
            // 
            textBoxEmailAddress.Location = new Point(19, 310);
            textBoxEmailAddress.Name = "textBoxEmailAddress";
            textBoxEmailAddress.Size = new Size(311, 27);
            textBoxEmailAddress.TabIndex = 4;
            textBoxEmailAddress.Validating += textBoxEmailValidating_Validating;
            // 
            // labelEmailAddress
            // 
            labelEmailAddress.AutoSize = true;
            labelEmailAddress.Location = new Point(19, 275);
            labelEmailAddress.Name = "labelEmailAddress";
            labelEmailAddress.Size = new Size(108, 20);
            labelEmailAddress.TabIndex = 4;
            labelEmailAddress.Text = "Email Address";
            // 
            // textBoxAccountName
            // 
            textBoxAccountName.Location = new Point(19, 153);
            textBoxAccountName.Name = "textBoxAccountName";
            textBoxAccountName.Size = new Size(311, 27);
            textBoxAccountName.TabIndex = 2;
            // 
            // labelAccountName
            // 
            labelAccountName.AutoSize = true;
            labelAccountName.Location = new Point(19, 118);
            labelAccountName.Name = "labelAccountName";
            labelAccountName.Size = new Size(113, 20);
            labelAccountName.TabIndex = 2;
            labelAccountName.Text = "Account Name";
            // 
            // textBoxLoginId
            // 
            textBoxLoginId.Location = new Point(19, 72);
            textBoxLoginId.Name = "textBoxLoginId";
            textBoxLoginId.Size = new Size(311, 27);
            textBoxLoginId.TabIndex = 1;
            // 
            // labelAccountId
            // 
            labelAccountId.AutoSize = true;
            labelAccountId.Location = new Point(19, 37);
            labelAccountId.Name = "labelAccountId";
            labelAccountId.Size = new Size(205, 20);
            labelAccountId.TabIndex = 0;
            labelAccountId.Text = "Account Login or AccountId";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelPasswordHelp);
            groupBox1.Controls.Add(checkBoxMarkedAsDeleted);
            groupBox1.Controls.Add(textBoxLocation);
            groupBox1.Controls.Add(labelLocation);
            groupBox1.Controls.Add(buttonSetPassword);
            groupBox1.Controls.Add(textBoxPassword);
            groupBox1.Controls.Add(labelPassword);
            groupBox1.Controls.Add(comboBoxSecurityLevels);
            groupBox1.Controls.Add(labelSecurityLevels);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(418, 104);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(350, 437);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "User Securuty";
            // 
            // labelPasswordHelp
            // 
            labelPasswordHelp.AutoSize = true;
            labelPasswordHelp.Location = new Point(19, 358);
            labelPasswordHelp.Name = "labelPasswordHelp";
            labelPasswordHelp.Size = new Size(300, 20);
            labelPasswordHelp.TabIndex = 12;
            labelPasswordHelp.Text = "Leave blank to keep the current password";
            // 
            // checkBoxMarkedAsDeleted
            // 
            checkBoxMarkedAsDeleted.AutoSize = true;
            checkBoxMarkedAsDeleted.Location = new Point(19, 203);
            checkBoxMarkedAsDeleted.Name = "checkBoxMarkedAsDeleted";
            checkBoxMarkedAsDeleted.Size = new Size(223, 24);
            checkBoxMarkedAsDeleted.TabIndex = 9;
            checkBoxMarkedAsDeleted.Text = "This user marked as deleted";
            checkBoxMarkedAsDeleted.UseVisualStyleBackColor = true;
            // 
            // textBoxLocation
            // 
            textBoxLocation.Location = new Point(19, 153);
            textBoxLocation.Name = "textBoxLocation";
            textBoxLocation.Size = new Size(311, 27);
            textBoxLocation.TabIndex = 8;
            // 
            // labelLocation
            // 
            labelLocation.AutoSize = true;
            labelLocation.Location = new Point(19, 118);
            labelLocation.Name = "labelLocation";
            labelLocation.Size = new Size(119, 20);
            labelLocation.TabIndex = 10;
            labelLocation.Text = "Location or City";
            // 
            // buttonSetPassword
            // 
            buttonSetPassword.BackColor = Color.RoyalBlue;
            buttonSetPassword.FlatAppearance.BorderSize = 0;
            buttonSetPassword.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonSetPassword.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonSetPassword.FlatStyle = FlatStyle.Flat;
            buttonSetPassword.Location = new Point(19, 388);
            buttonSetPassword.Name = "buttonSetPassword";
            buttonSetPassword.Size = new Size(311, 32);
            buttonSetPassword.TabIndex = 11;
            buttonSetPassword.Text = "Set Password Only";
            buttonSetPassword.TextAlign = ContentAlignment.MiddleLeft;
            buttonSetPassword.UseVisualStyleBackColor = false;
            buttonSetPassword.Click += ButtonSetPassword_Click;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxPassword.Location = new Point(19, 310);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '⬤';
            textBoxPassword.Size = new Size(311, 27);
            textBoxPassword.TabIndex = 10;
            textBoxPassword.WordWrap = false;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(19, 275);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(207, 20);
            labelPassword.TabIndex = 2;
            labelPassword.Text = "Password (5 char minimum)";
            // 
            // comboBoxSecurityLevels
            // 
            comboBoxSecurityLevels.FormattingEnabled = true;
            comboBoxSecurityLevels.Location = new Point(19, 72);
            comboBoxSecurityLevels.Name = "comboBoxSecurityLevels";
            comboBoxSecurityLevels.Size = new Size(311, 28);
            comboBoxSecurityLevels.TabIndex = 7;
            // 
            // labelSecurityLevels
            // 
            labelSecurityLevels.AutoSize = true;
            labelSecurityLevels.Location = new Point(19, 37);
            labelSecurityLevels.Name = "labelSecurityLevels";
            labelSecurityLevels.Size = new Size(112, 20);
            labelSecurityLevels.TabIndex = 0;
            labelSecurityLevels.Text = "Security Levels";
            // 
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelComments.Location = new Point(51, 558);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(85, 20);
            labelComments.TabIndex = 49;
            labelComments.Text = "Comments";
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxComments.Location = new Point(51, 590);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(311, 51);
            textBoxComments.TabIndex = 12;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // AccountDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            CancelButton = Cancel_Button;
            ClientSize = new Size(800, 667);
            Controls.Add(textBoxComments);
            Controls.Add(labelComments);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxUserInformation);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AccountDialog";
            Text = AppInfo.WindowTitle("Account or User Editor");
            TopMost = true;
            Load += AccountDialog_Load;
            Shown += AccountDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxUserInformation.ResumeLayout(false);
            groupBoxUserInformation.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
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
        private GroupBox groupBoxUserInformation;
        private GroupBox groupBox1;
        private TextBox textBoxLoginId;
        private Label labelAccountId;
        private TextBox textBoxAccountName;
        private Label labelAccountName;
        private TextBox textBoxEmailAddress;
        private Label labelEmailAddress;
        private MaskedTextBox maskedTextBoxPhoneNumber;
        private Label labelPhoneNUmber;
        private Label labelSecurityLevels;
        private ComboBox comboBoxSecurityLevels;
        private TextBox textBoxPassword;
        private Label labelPassword;
        private Label labelComments;
        private TextBox textBoxComments;
        private Button buttonSetPassword;
        private ComboBox comboBoxTimeZone;
        private Label labelTimeZone;
        private TextBox textBoxLocation;
        private Label labelLocation;
        private CheckBox checkBoxMarkedAsDeleted;
        private Label labelPasswordHelp;
        private ErrorProvider errorProvider1;
    }
}
