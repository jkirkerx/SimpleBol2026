namespace SimpleBol.WinForms.Dialogs
{
    partial class ShipperContactEditorDialog
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
            groupBoxContact = new GroupBox();
            maskedTextBoxPhoneNumber = new MaskedTextBox();
            labelPhoneNumber = new Label();
            labelEmailAddress = new Label();
            textBoxEmailAddress = new TextBox();
            labelName = new Label();
            textBoxName = new TextBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            panelHeader = new Panel();
            Lbl_Header = new Label();
            PBLogo = new PictureBox();
            errorProvider1 = new ErrorProvider(components);
            groupBoxContact.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PBLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // groupBoxContact
            // 
            groupBoxContact.Controls.Add(maskedTextBoxPhoneNumber);
            groupBoxContact.Controls.Add(labelPhoneNumber);
            groupBoxContact.Controls.Add(labelEmailAddress);
            groupBoxContact.Controls.Add(textBoxEmailAddress);
            groupBoxContact.Controls.Add(labelName);
            groupBoxContact.Controls.Add(textBoxName);
            groupBoxContact.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxContact.ForeColor = Color.White;
            groupBoxContact.Location = new Point(23, 98);
            groupBoxContact.Name = "groupBoxContact";
            groupBoxContact.Size = new Size(377, 252);
            groupBoxContact.TabIndex = 0;
            groupBoxContact.TabStop = false;
            groupBoxContact.Text = "Contact";
            // 
            // maskedTextBoxPhoneNumber
            // 
            maskedTextBoxPhoneNumber.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            maskedTextBoxPhoneNumber.Location = new Point(24, 198);
            maskedTextBoxPhoneNumber.Mask = "(999) 000-0000";
            maskedTextBoxPhoneNumber.Name = "maskedTextBoxPhoneNumber";
            maskedTextBoxPhoneNumber.Size = new Size(327, 27);
            maskedTextBoxPhoneNumber.TabIndex = 3;
            maskedTextBoxPhoneNumber.Enter += TextBoxPhoneNumber_Enter;
            maskedTextBoxPhoneNumber.KeyPress += TextBoxPhoneNumber_KeyPress;
            // 
            // labelPhoneNumber
            // 
            labelPhoneNumber.AutoSize = true;
            labelPhoneNumber.Location = new Point(26, 169);
            labelPhoneNumber.Name = "labelPhoneNumber";
            labelPhoneNumber.Size = new Size(111, 20);
            labelPhoneNumber.TabIndex = 4;
            labelPhoneNumber.Text = "Phone Number:";
            // 
            // labelEmailAddress
            // 
            labelEmailAddress.AutoSize = true;
            labelEmailAddress.Location = new Point(29, 99);
            labelEmailAddress.Name = "labelEmailAddress";
            labelEmailAddress.Size = new Size(159, 20);
            labelEmailAddress.TabIndex = 3;
            labelEmailAddress.Text = "EmailAddress Address:";
            // 
            // textBoxEmailAddress
            // 
            textBoxEmailAddress.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxEmailAddress.Location = new Point(26, 126);
            textBoxEmailAddress.Name = "textBoxEmailAddress";
            textBoxEmailAddress.Size = new Size(325, 27);
            textBoxEmailAddress.TabIndex = 2;
            textBoxEmailAddress.Enter += TextBoxEmailAddress_Enter;
            textBoxEmailAddress.Validating += textBoxEmailValidating_Validating;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(27, 31);
            labelName.Name = "labelName";
            labelName.Size = new Size(52, 20);
            labelName.TabIndex = 1;
            labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            textBoxName.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxName.Location = new Point(24, 58);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(325, 27);
            textBoxName.TabIndex = 1;
            textBoxName.Enter += TextBoxName_Enter;
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
            OK_Button.Location = new Point(162, 380);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 4;
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
            Cancel_Button.Location = new Point(283, 380);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 5;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(38, 38, 38);
            panelHeader.Controls.Add(Lbl_Header);
            panelHeader.Controls.Add(PBLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(426, 70);
            panelHeader.TabIndex = 5;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 14);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(248, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Shipper Contact";
            // 
            // PBLogo
            // 
            PBLogo.Image = Properties.Resources.CustomersIcon;
            PBLogo.InitialImage = Properties.Resources.CustomersIcon;
            PBLogo.Location = new Point(20, 14);
            PBLogo.Name = "PBLogo";
            PBLogo.Size = new Size(50, 50);
            PBLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            PBLogo.TabIndex = 0;
            PBLogo.TabStop = false;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // ShipperContactEditorDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(426, 452);
            Controls.Add(panelHeader);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(groupBoxContact);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ShipperContactEditorDialog";
            Text = AppInfo.WindowTitle("Contact Editor");
            TopMost = true;
            Load += ShipperContactEditorDialog_Load;
            Shown += ShipperContactEditorDialog_Shown;
            groupBoxContact.ResumeLayout(false);
            groupBoxContact.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PBLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxContact;
        private Button OK_Button;
        private Button Cancel_Button;
        private MaskedTextBox maskedTextBoxPhoneNumber;
        private Label labelPhoneNumber;
        private Label labelEmailAddress;
        private TextBox textBoxEmailAddress;
        private Label labelName;
        private TextBox textBoxName;
        private Panel panelHeader;
        private Label Lbl_Header;
        private PictureBox PBLogo;
        private ErrorProvider errorProvider1;
    }
}
