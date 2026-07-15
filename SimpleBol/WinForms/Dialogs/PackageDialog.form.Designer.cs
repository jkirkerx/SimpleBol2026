namespace SimpleBol.WinForms.Dialogs
{
    partial class PackageDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageDialog));
            groupBoxPackageInformation = new GroupBox();
            labelMnfcCode = new Label();
            comboBoxNmfcCode = new ComboBox();
            labelClassCode = new Label();
            comboBoxClassCode = new ComboBox();
            labelCurrencyCode = new Label();
            comboBoxCurrencyCode = new ComboBox();
            textBoxEstimatedValue = new TextBox();
            labelEstimatedValue = new Label();
            textBoxDescription = new TextBox();
            labelDescription = new Label();
            textBoxItemCount = new TextBox();
            labelItems = new Label();
            labelRuler = new Label();
            comboBoxRuler = new ComboBox();
            textBoxWeight = new TextBox();
            label3 = new Label();
            textBoxHeight = new TextBox();
            label2 = new Label();
            textBoxWidth = new TextBox();
            label1 = new Label();
            textBoxLength = new TextBox();
            labelLength = new Label();
            OK_Button = new Button();
            Cancel_Button = new Button();
            panelHeader = new Panel();
            labelUnitOfMeasurement = new Label();
            labelBolTotalWeight = new Label();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            pictureBox1 = new PictureBox();
            labelPackageStatus1 = new Label();
            labelPackageStatus2 = new Label();
            labelPackageVolumeValue = new Label();
            labelPackageVolume = new Label();
            groupBoxPackageInformation.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBoxPackageInformation
            // 
            groupBoxPackageInformation.Controls.Add(labelMnfcCode);
            groupBoxPackageInformation.Controls.Add(comboBoxNmfcCode);
            groupBoxPackageInformation.Controls.Add(labelClassCode);
            groupBoxPackageInformation.Controls.Add(comboBoxClassCode);
            groupBoxPackageInformation.Controls.Add(labelCurrencyCode);
            groupBoxPackageInformation.Controls.Add(comboBoxCurrencyCode);
            groupBoxPackageInformation.Controls.Add(textBoxEstimatedValue);
            groupBoxPackageInformation.Controls.Add(labelEstimatedValue);
            groupBoxPackageInformation.Controls.Add(textBoxDescription);
            groupBoxPackageInformation.Controls.Add(labelDescription);
            groupBoxPackageInformation.Controls.Add(textBoxItemCount);
            groupBoxPackageInformation.Controls.Add(labelItems);
            groupBoxPackageInformation.Controls.Add(labelRuler);
            groupBoxPackageInformation.Controls.Add(comboBoxRuler);
            groupBoxPackageInformation.Controls.Add(textBoxWeight);
            groupBoxPackageInformation.Controls.Add(label3);
            groupBoxPackageInformation.Controls.Add(textBoxHeight);
            groupBoxPackageInformation.Controls.Add(label2);
            groupBoxPackageInformation.Controls.Add(textBoxWidth);
            groupBoxPackageInformation.Controls.Add(label1);
            groupBoxPackageInformation.Controls.Add(textBoxLength);
            groupBoxPackageInformation.Controls.Add(labelLength);
            groupBoxPackageInformation.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxPackageInformation.ForeColor = Color.White;
            groupBoxPackageInformation.Location = new Point(18, 98);
            groupBoxPackageInformation.Name = "groupBoxPackageInformation";
            groupBoxPackageInformation.Size = new Size(331, 575);
            groupBoxPackageInformation.TabIndex = 0;
            groupBoxPackageInformation.TabStop = false;
            groupBoxPackageInformation.Text = "Package Information";
            // 
            // labelMnfcCode
            // 
            labelMnfcCode.AutoSize = true;
            labelMnfcCode.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelMnfcCode.ForeColor = Color.White;
            labelMnfcCode.Location = new Point(17, 332);
            labelMnfcCode.Name = "labelMnfcCode";
            labelMnfcCode.Size = new Size(88, 20);
            labelMnfcCode.TabIndex = 81;
            labelMnfcCode.Text = "NMFC Code";
            // 
            // comboBoxNmfcCode
            // 
            comboBoxNmfcCode.FormattingEnabled = true;
            comboBoxNmfcCode.ItemHeight = 20;
            comboBoxNmfcCode.Location = new Point(17, 360);
            comboBoxNmfcCode.Name = "comboBoxNmfcCode";
            comboBoxNmfcCode.Size = new Size(298, 28);
            comboBoxNmfcCode.TabIndex = 8;
            comboBoxNmfcCode.DropDown += ComboBoxNmfcCode_DropDown;
            comboBoxNmfcCode.SelectedIndexChanged += ComboBoxNmfcCode_SelectedIndexChanged;
            comboBoxNmfcCode.DropDownClosed += ComboBoxNmfcCode_DropDownClosed;
            // 
            // labelClassCode
            // 
            labelClassCode.AutoSize = true;
            labelClassCode.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelClassCode.ForeColor = Color.White;
            labelClassCode.Location = new Point(17, 412);
            labelClassCode.Name = "labelClassCode";
            labelClassCode.Size = new Size(81, 20);
            labelClassCode.TabIndex = 80;
            labelClassCode.Text = "Class Code";
            // 
            // comboBoxClassCode
            // 
            comboBoxClassCode.FormattingEnabled = true;
            comboBoxClassCode.Location = new Point(17, 440);
            comboBoxClassCode.Name = "comboBoxClassCode";
            comboBoxClassCode.Size = new Size(298, 28);
            comboBoxClassCode.TabIndex = 9;
            comboBoxClassCode.DropDown += ComboBoxClassCode_DropDown;
            comboBoxClassCode.SelectedIndexChanged += ComboBoxClassCode_SelectedIndexChanged;
            comboBoxClassCode.DropDownClosed += ComboBoxClassCode_DropDownClosed;
            // 
            // labelCurrencyCode
            // 
            labelCurrencyCode.AutoSize = true;
            labelCurrencyCode.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelCurrencyCode.ForeColor = Color.White;
            labelCurrencyCode.Location = new Point(184, 502);
            labelCurrencyCode.Name = "labelCurrencyCode";
            labelCurrencyCode.Size = new Size(105, 20);
            labelCurrencyCode.TabIndex = 75;
            labelCurrencyCode.Text = "Currency Code";
            // 
            // comboBoxCurrencyCode
            // 
            comboBoxCurrencyCode.FormattingEnabled = true;
            comboBoxCurrencyCode.Location = new Point(182, 529);
            comboBoxCurrencyCode.Name = "comboBoxCurrencyCode";
            comboBoxCurrencyCode.Size = new Size(133, 28);
            comboBoxCurrencyCode.TabIndex = 11;
            // 
            // textBoxEstimatedValue
            // 
            textBoxEstimatedValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxEstimatedValue.Location = new Point(17, 530);
            textBoxEstimatedValue.Name = "textBoxEstimatedValue";
            textBoxEstimatedValue.Size = new Size(141, 27);
            textBoxEstimatedValue.TabIndex = 10;
            textBoxEstimatedValue.KeyPress += TextBoxEstimatedValue_KeyPress;
            // 
            // labelEstimatedValue
            // 
            labelEstimatedValue.AutoSize = true;
            labelEstimatedValue.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelEstimatedValue.ForeColor = Color.White;
            labelEstimatedValue.Location = new Point(17, 502);
            labelEstimatedValue.Name = "labelEstimatedValue";
            labelEstimatedValue.Size = new Size(115, 20);
            labelEstimatedValue.TabIndex = 72;
            labelEstimatedValue.Text = "Estimated Value";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxDescription.Location = new Point(17, 144);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(298, 27);
            textBoxDescription.TabIndex = 2;
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelDescription.ForeColor = Color.White;
            labelDescription.Location = new Point(17, 116);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(85, 20);
            labelDescription.TabIndex = 70;
            labelDescription.Text = "Description";
            // 
            // textBoxItemCount
            // 
            textBoxItemCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxItemCount.Location = new Point(182, 285);
            textBoxItemCount.Name = "textBoxItemCount";
            textBoxItemCount.Size = new Size(133, 27);
            textBoxItemCount.TabIndex = 7;
            textBoxItemCount.KeyPress += TextBoxItemCount_KeyPress;
            // 
            // labelItems
            // 
            labelItems.AutoSize = true;
            labelItems.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelItems.ForeColor = Color.White;
            labelItems.Location = new Point(182, 257);
            labelItems.Name = "labelItems";
            labelItems.Size = new Size(82, 20);
            labelItems.TabIndex = 68;
            labelItems.Text = "Item Count";
            // 
            // labelRuler
            // 
            labelRuler.AutoSize = true;
            labelRuler.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelRuler.ForeColor = Color.White;
            labelRuler.Location = new Point(17, 40);
            labelRuler.Name = "labelRuler";
            labelRuler.Size = new Size(198, 20);
            labelRuler.TabIndex = 63;
            labelRuler.Text = "Ruler - Measurement System";
            // 
            // comboBoxRuler
            // 
            comboBoxRuler.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRuler.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            comboBoxRuler.FormattingEnabled = true;
            comboBoxRuler.Items.AddRange(new object[] { "English", "Metric" });
            comboBoxRuler.Location = new Point(19, 70);
            comboBoxRuler.Name = "comboBoxRuler";
            comboBoxRuler.Size = new Size(296, 28);
            comboBoxRuler.TabIndex = 1;
            comboBoxRuler.SelectedIndexChanged += ComboBoxRuler_SelectedIndexChanged;
            // 
            // textBoxWeight
            // 
            textBoxWeight.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxWeight.Location = new Point(15, 285);
            textBoxWeight.Name = "textBoxWeight";
            textBoxWeight.Size = new Size(143, 27);
            textBoxWeight.TabIndex = 6;
            textBoxWeight.KeyPress += TextBoxWeight_KeyPress;
            textBoxWeight.Leave += PackageDialog_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(15, 257);
            label3.Name = "label3";
            label3.Size = new Size(130, 20);
            label3.TabIndex = 60;
            label3.Text = "Weight (round up)";
            // 
            // textBoxHeight
            // 
            textBoxHeight.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxHeight.Location = new Point(227, 210);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(88, 27);
            textBoxHeight.TabIndex = 5;
            textBoxHeight.KeyPress += TextBoxHeight_KeyPress;
            textBoxHeight.Leave += PackageDialog_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(227, 182);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 58;
            label2.Text = "Height";
            // 
            // textBoxWidth
            // 
            textBoxWidth.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxWidth.Location = new Point(121, 210);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(88, 27);
            textBoxWidth.TabIndex = 4;
            textBoxWidth.KeyPress += TextBoxWidth_KeyPress;
            textBoxWidth.Leave += PackageDialog_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(121, 182);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 56;
            label1.Text = "Width";
            // 
            // textBoxLength
            // 
            textBoxLength.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxLength.Location = new Point(15, 210);
            textBoxLength.Name = "textBoxLength";
            textBoxLength.Size = new Size(88, 27);
            textBoxLength.TabIndex = 3;
            textBoxLength.KeyPress += TextBoxLength_KeyPress;
            textBoxLength.Leave += PackageDialog_Leave;
            // 
            // labelLength
            // 
            labelLength.AutoSize = true;
            labelLength.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelLength.ForeColor = Color.White;
            labelLength.Location = new Point(15, 182);
            labelLength.Name = "labelLength";
            labelLength.Size = new Size(54, 20);
            labelLength.TabIndex = 54;
            labelLength.Text = "Length";
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.Cursor = Cursors.Hand;
            // The async click handler sets DialogResult only after SavePackage completes.
            // Setting it here closes the dialog immediately and disposes its DI scope
            // while the MongoDB work is still in progress.
            OK_Button.DialogResult = DialogResult.None;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(391, 622);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 12;
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
            Cancel_Button.Location = new Point(517, 622);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 13;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(38, 38, 38);
            panelHeader.Controls.Add(labelUnitOfMeasurement);
            panelHeader.Controls.Add(labelBolTotalWeight);
            panelHeader.Controls.Add(Lbl_Header);
            panelHeader.Controls.Add(PbLogo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 7, 3, 7);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(651, 70);
            panelHeader.TabIndex = 55;
            // 
            // labelUnitOfMeasurement
            // 
            labelUnitOfMeasurement.AutoSize = true;
            labelUnitOfMeasurement.Font = new Font("Segoe UI", 22F, FontStyle.Regular, GraphicsUnit.Point);
            labelUnitOfMeasurement.ForeColor = Color.White;
            labelUnitOfMeasurement.Location = new Point(574, 23);
            labelUnitOfMeasurement.Name = "labelUnitOfMeasurement";
            labelUnitOfMeasurement.Size = new Size(65, 41);
            labelUnitOfMeasurement.TabIndex = 9;
            labelUnitOfMeasurement.Text = "LBS";
            // 
            // labelBolTotalWeight
            // 
            labelBolTotalWeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBolTotalWeight.Font = new Font("Segoe UI", 22F, FontStyle.Regular, GraphicsUnit.Point);
            labelBolTotalWeight.ForeColor = Color.White;
            labelBolTotalWeight.Location = new Point(500, 23);
            labelBolTotalWeight.Name = "labelBolTotalWeight";
            labelBolTotalWeight.RightToLeft = RightToLeft.Yes;
            labelBolTotalWeight.Size = new Size(77, 41);
            labelBolTotalWeight.TabIndex = 8;
            labelBolTotalWeight.Text = "0";
            labelBolTotalWeight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 17);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(229, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Package Editor";
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
            // pictureBox1
            // 
            pictureBox1.ErrorImage = null;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(373, 98);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(259, 276);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 59;
            pictureBox1.TabStop = false;
            // 
            // labelPackageStatus1
            // 
            labelPackageStatus1.AutoSize = true;
            labelPackageStatus1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPackageStatus1.ForeColor = Color.White;
            labelPackageStatus1.Location = new Point(373, 390);
            labelPackageStatus1.Name = "labelPackageStatus1";
            labelPackageStatus1.Size = new Size(110, 20);
            labelPackageStatus1.TabIndex = 60;
            labelPackageStatus1.Text = "Package Status:";
            // 
            // labelPackageStatus2
            // 
            labelPackageStatus2.AutoSize = true;
            labelPackageStatus2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelPackageStatus2.ForeColor = Color.White;
            labelPackageStatus2.Location = new Point(373, 420);
            labelPackageStatus2.Name = "labelPackageStatus2";
            labelPackageStatus2.Size = new Size(144, 20);
            labelPackageStatus2.TabIndex = 61;
            labelPackageStatus2.Text = "Package acceptable";
            // 
            // labelPackageVolumeValue
            // 
            labelPackageVolumeValue.AutoSize = true;
            labelPackageVolumeValue.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            labelPackageVolumeValue.ForeColor = Color.White;
            labelPackageVolumeValue.Location = new Point(375, 491);
            labelPackageVolumeValue.Name = "labelPackageVolumeValue";
            labelPackageVolumeValue.Size = new Size(26, 30);
            labelPackageVolumeValue.TabIndex = 63;
            labelPackageVolumeValue.Text = "0";
            // 
            // labelPackageVolume
            // 
            labelPackageVolume.AutoSize = true;
            labelPackageVolume.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            labelPackageVolume.ForeColor = Color.White;
            labelPackageVolume.Location = new Point(373, 458);
            labelPackageVolume.Name = "labelPackageVolume";
            labelPackageVolume.Size = new Size(120, 20);
            labelPackageVolume.TabIndex = 62;
            labelPackageVolume.Text = "Package Volume:";
            // 
            // PackageDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            CancelButton = Cancel_Button;
            ClientSize = new Size(651, 689);
            Controls.Add(labelPackageVolumeValue);
            Controls.Add(labelPackageVolume);
            Controls.Add(labelPackageStatus2);
            Controls.Add(labelPackageStatus1);
            Controls.Add(pictureBox1);
            Controls.Add(groupBoxPackageInformation);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PackageDialog";
            Text = AppInfo.WindowTitle("Package Editor");
            Load += PackageDialog_Load;
            Shown += PackageDialog_Shown;
            groupBoxPackageInformation.ResumeLayout(false);
            groupBoxPackageInformation.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxPackageInformation;
        private Label labelCurrencyCode;
        private ComboBox comboBoxCurrencyCode;
        private TextBox textBoxEstimatedValue;
        private Label labelEstimatedValue;
        private TextBox textBoxDescription;
        private Label labelDescription;
        private TextBox textBoxItemCount;
        private Label labelItems;
        private Label labelRuler;
        private ComboBox comboBoxRuler;
        private TextBox textBoxWeight;
        private Label label3;
        private TextBox textBoxHeight;
        private Label label2;
        private TextBox textBoxWidth;
        private Label label1;
        private TextBox textBoxLength;
        private Label labelLength;
        private Button OK_Button;
        private Button Cancel_Button;
        private Panel panelHeader;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private PictureBox pictureBox1;
        private Label labelPackageStatus1;
        private Label labelPackageStatus2;
        private Label labelMnfcCode;
        private ComboBox comboBoxNmfcCode;
        private Label labelClassCode;
        private ComboBox comboBoxClassCode;
        private Label labelUnitOfMeasurement;
        private Label labelBolTotalWeight;
        private Label labelPackageVolumeValue;
        private Label labelPackageVolume;
    }
}
