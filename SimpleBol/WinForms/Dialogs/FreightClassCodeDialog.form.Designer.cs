namespace SimpleBol.WinForms.Dialogs
{
    partial class FreightClassCodeDialog
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
            textBoxComments = new TextBox();
            labelComments = new Label();
            OK_Button = new Button();
            Cancel_Button = new Button();
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            groupBox1 = new GroupBox();
            labelWeightType = new Label();
            comboBoxWeightType = new ComboBox();
            textBoxMaximumWeightPerFoot = new TextBox();
            labelMaximumWeightPerFoot = new Label();
            textBoxMinimumWeightPerFoot = new TextBox();
            checkBoxMarkedAsDeleted = new CheckBox();
            checkBoxEnabled = new CheckBox();
            labelMinimumWeightPerFoot = new Label();
            textBoxCodeNumber = new TextBox();
            labelCodeNumber = new Label();
            textBoxDescription = new TextBox();
            labelDescription = new Label();
            textBoxName = new TextBox();
            labelName = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxComments
            // 
            textBoxComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBoxComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxComments.Location = new Point(50, 556);
            textBoxComments.Multiline = true;
            textBoxComments.Name = "textBoxComments";
            textBoxComments.Size = new Size(281, 51);
            textBoxComments.TabIndex = 50;
            // 
            // labelComments
            // 
            labelComments.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelComments.AutoSize = true;
            labelComments.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelComments.Location = new Point(54, 520);
            labelComments.Name = "labelComments";
            labelComments.Size = new Size(85, 20);
            labelComments.TabIndex = 54;
            labelComments.Text = "Comments";
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
            OK_Button.Location = new Point(427, 556);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 51;
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
            Cancel_Button.Location = new Point(548, 556);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 52;
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
            panel1.Size = new Size(685, 70);
            panel1.TabIndex = 53;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(378, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Freight Class Code Editor";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = Properties.Resources.FreightClassCodesIcon150;
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 7, 3, 7);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(labelWeightType);
            groupBox1.Controls.Add(comboBoxWeightType);
            groupBox1.Controls.Add(textBoxMaximumWeightPerFoot);
            groupBox1.Controls.Add(labelMaximumWeightPerFoot);
            groupBox1.Controls.Add(textBoxMinimumWeightPerFoot);
            groupBox1.Controls.Add(checkBoxMarkedAsDeleted);
            groupBox1.Controls.Add(checkBoxEnabled);
            groupBox1.Controls.Add(labelMinimumWeightPerFoot);
            groupBox1.Controls.Add(textBoxCodeNumber);
            groupBox1.Controls.Add(labelCodeNumber);
            groupBox1.Controls.Add(textBoxDescription);
            groupBox1.Controls.Add(labelDescription);
            groupBox1.Controls.Add(textBoxName);
            groupBox1.Controls.Add(labelName);
            groupBox1.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(20, 92);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(643, 409);
            groupBox1.TabIndex = 56;
            groupBox1.TabStop = false;
            groupBox1.Text = "NMFC Freight Code";
            // 
            // labelWeightType
            // 
            labelWeightType.AutoSize = true;
            labelWeightType.Location = new Point(338, 115);
            labelWeightType.Name = "labelWeightType";
            labelWeightType.Size = new Size(218, 20);
            labelWeightType.TabIndex = 14;
            labelWeightType.Text = "Unit of Weight Measurement:";
            // 
            // comboBoxWeightType
            // 
            comboBoxWeightType.FormattingEnabled = true;
            comboBoxWeightType.Items.AddRange(new object[] { "KG", "LBS" });
            comboBoxWeightType.Location = new Point(338, 144);
            comboBoxWeightType.Name = "comboBoxWeightType";
            comboBoxWeightType.Size = new Size(281, 28);
            comboBoxWeightType.TabIndex = 13;
            // 
            // textBoxMaximumWeightPerFoot
            // 
            textBoxMaximumWeightPerFoot.Location = new Point(338, 291);
            textBoxMaximumWeightPerFoot.Name = "textBoxMaximumWeightPerFoot";
            textBoxMaximumWeightPerFoot.Size = new Size(281, 27);
            textBoxMaximumWeightPerFoot.TabIndex = 12;
            textBoxMaximumWeightPerFoot.KeyPress += TextBoxMaximumWeightPerFoot_KeyPress;
            // 
            // labelMaximumWeightPerFoot
            // 
            labelMaximumWeightPerFoot.AutoSize = true;
            labelMaximumWeightPerFoot.Location = new Point(338, 262);
            labelMaximumWeightPerFoot.Name = "labelMaximumWeightPerFoot";
            labelMaximumWeightPerFoot.Size = new Size(198, 20);
            labelMaximumWeightPerFoot.TabIndex = 11;
            labelMaximumWeightPerFoot.Text = "Maximum Weight Per Foot";
            // 
            // textBoxMinimumWeightPerFoot
            // 
            textBoxMinimumWeightPerFoot.Location = new Point(338, 222);
            textBoxMinimumWeightPerFoot.Name = "textBoxMinimumWeightPerFoot";
            textBoxMinimumWeightPerFoot.Size = new Size(281, 27);
            textBoxMinimumWeightPerFoot.TabIndex = 10;
            textBoxMinimumWeightPerFoot.KeyPress += TextBoxMinimumWeightPerFoot_KeyPress;
            // 
            // checkBoxMarkedAsDeleted
            // 
            checkBoxMarkedAsDeleted.AutoSize = true;
            checkBoxMarkedAsDeleted.Location = new Point(338, 360);
            checkBoxMarkedAsDeleted.Name = "checkBoxMarkedAsDeleted";
            checkBoxMarkedAsDeleted.Size = new Size(158, 24);
            checkBoxMarkedAsDeleted.TabIndex = 9;
            checkBoxMarkedAsDeleted.Text = "Marked as Deleted";
            checkBoxMarkedAsDeleted.UseVisualStyleBackColor = true;
            checkBoxMarkedAsDeleted.CheckedChanged += CheckBoxMarkedAsDeleted_CheckedChanged;
            // 
            // checkBoxEnabled
            // 
            checkBoxEnabled.AutoSize = true;
            checkBoxEnabled.Location = new Point(338, 330);
            checkBoxEnabled.Name = "checkBoxEnabled";
            checkBoxEnabled.Size = new Size(237, 24);
            checkBoxEnabled.TabIndex = 8;
            checkBoxEnabled.Text = "Enabled - Use this NMFC code";
            checkBoxEnabled.UseVisualStyleBackColor = true;
            checkBoxEnabled.CheckedChanged += CheckBoxEnabled_CheckedChanged;
            // 
            // labelMinimumWeightPerFoot
            // 
            labelMinimumWeightPerFoot.AutoSize = true;
            labelMinimumWeightPerFoot.Location = new Point(338, 193);
            labelMinimumWeightPerFoot.Name = "labelMinimumWeightPerFoot";
            labelMinimumWeightPerFoot.Size = new Size(195, 20);
            labelMinimumWeightPerFoot.TabIndex = 6;
            labelMinimumWeightPerFoot.Text = "Minimum Weight Per Foot";
            // 
            // textBoxCodeNumber
            // 
            textBoxCodeNumber.Location = new Point(338, 69);
            textBoxCodeNumber.Name = "textBoxCodeNumber";
            textBoxCodeNumber.Size = new Size(281, 27);
            textBoxCodeNumber.TabIndex = 5;
            // 
            // labelCodeNumber
            // 
            labelCodeNumber.AutoSize = true;
            labelCodeNumber.Location = new Point(338, 40);
            labelCodeNumber.Name = "labelCodeNumber";
            labelCodeNumber.Size = new Size(215, 20);
            labelCodeNumber.TabIndex = 4;
            labelCodeNumber.Text = "Code Number (dots allowed):";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(30, 144);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(281, 240);
            textBoxDescription.TabIndex = 3;
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Location = new Point(30, 115);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(89, 20);
            labelDescription.TabIndex = 2;
            labelDescription.Text = "Description";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(30, 69);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(281, 27);
            textBoxName.TabIndex = 1;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(30, 40);
            labelName.Name = "labelName";
            labelName.Size = new Size(55, 20);
            labelName.TabIndex = 0;
            labelName.Text = "Name:";
            // 
            // FreightClassCodeDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(685, 631);
            Controls.Add(groupBox1);
            Controls.Add(textBoxComments);
            Controls.Add(labelComments);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FreightClassCodeDialog";
            Text = AppInfo.WindowTitle("Freight Class Code Editor");
            Load += FreightClassCodeDialog_Load;
            Shown += FreightClassCodeDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxComments;
        private Label labelComments;
        private Button OK_Button;
        private Button Cancel_Button;
        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private GroupBox groupBox1;
        private CheckBox checkBoxMarkedAsDeleted;
        private CheckBox checkBoxEnabled;
        private ComboBox comboBoxFreightClass;
        private Label labelMinimumWeightPerFoot;
        private TextBox textBoxCodeNumber;
        private Label labelCodeNumber;
        private TextBox textBoxDescription;
        private Label labelDescription;
        private TextBox textBoxName;
        private Label labelName;
        private TextBox textBoxMinimumWeightPerFoot;
        private TextBox textBoxMaximumWeightPerFoot;
        private Label labelMaximumWeightPerFoot;
        private Label labelWeightType;
        private ComboBox comboBoxWeightType;
    }
}
