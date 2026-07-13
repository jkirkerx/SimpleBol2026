namespace SimpleBol.WinForms.Dialogs
{
    partial class PalletDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PalletDialog));
            panelHeader = new Panel();
            labelUnitOfMeasurement = new Label();
            labelBolTotalWeight = new Label();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            pictureBox1 = new PictureBox();
            groupBoxPalletInformation = new GroupBox();
            labelMnfcCode = new Label();
            comboBoxNmfcCode = new ComboBox();
            labelCurrencyCode = new Label();
            comboBoxCurrencyCode = new ComboBox();
            textBoxEstimatedValue = new TextBox();
            labelEstimatedValue = new Label();
            textBoxDescription = new TextBox();
            labelDescription = new Label();
            textBoxItemCount = new TextBox();
            labelItems = new Label();
            textBoxCartonCount = new TextBox();
            labelCartons = new Label();
            labelClassCode = new Label();
            comboBoxClassCode = new ComboBox();
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
            groupBoxCodes = new GroupBox();
            textBoxRfId = new TextBox();
            labelRfId = new Label();
            labelPalletVolume = new Label();
            labelPalletVolumeValue = new Label();
            checkBoxStackable = new CheckBox();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxPalletInformation.SuspendLayout();
            groupBoxCodes.SuspendLayout();
            SuspendLayout();
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
            panelHeader.TabIndex = 43;
            // 
            // labelUnitOfMeasurement
            // 
            labelUnitOfMeasurement.AutoSize = true;
            labelUnitOfMeasurement.Font = new Font("Segoe UI", 22F);
            labelUnitOfMeasurement.ForeColor = Color.White;
            labelUnitOfMeasurement.Location = new Point(576, 17);
            labelUnitOfMeasurement.Name = "labelUnitOfMeasurement";
            labelUnitOfMeasurement.Size = new Size(65, 41);
            labelUnitOfMeasurement.TabIndex = 7;
            labelUnitOfMeasurement.Text = "LBS";
            // 
            // labelBolTotalWeight
            // 
            labelBolTotalWeight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBolTotalWeight.Font = new Font("Segoe UI", 22F);
            labelBolTotalWeight.ForeColor = Color.White;
            labelBolTotalWeight.Location = new Point(502, 17);
            labelBolTotalWeight.Name = "labelBolTotalWeight";
            labelBolTotalWeight.RightToLeft = RightToLeft.Yes;
            labelBolTotalWeight.Size = new Size(77, 41);
            labelBolTotalWeight.TabIndex = 6;
            labelBolTotalWeight.Text = "0";
            labelBolTotalWeight.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 17);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(190, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Pallet Editor";
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
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(391, 687);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 16;
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
            Cancel_Button.Location = new Point(517, 687);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 17;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(373, 378);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(259, 276);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 51;
            pictureBox1.TabStop = false;
            // 
            // groupBoxPalletInformation
            // 
            groupBoxPalletInformation.Controls.Add(labelMnfcCode);
            groupBoxPalletInformation.Controls.Add(comboBoxNmfcCode);
            groupBoxPalletInformation.Controls.Add(labelCurrencyCode);
            groupBoxPalletInformation.Controls.Add(comboBoxCurrencyCode);
            groupBoxPalletInformation.Controls.Add(textBoxEstimatedValue);
            groupBoxPalletInformation.Controls.Add(labelEstimatedValue);
            groupBoxPalletInformation.Controls.Add(textBoxDescription);
            groupBoxPalletInformation.Controls.Add(labelDescription);
            groupBoxPalletInformation.Controls.Add(textBoxItemCount);
            groupBoxPalletInformation.Controls.Add(labelItems);
            groupBoxPalletInformation.Controls.Add(textBoxCartonCount);
            groupBoxPalletInformation.Controls.Add(labelCartons);
            groupBoxPalletInformation.Controls.Add(labelClassCode);
            groupBoxPalletInformation.Controls.Add(comboBoxClassCode);
            groupBoxPalletInformation.Controls.Add(labelRuler);
            groupBoxPalletInformation.Controls.Add(comboBoxRuler);
            groupBoxPalletInformation.Controls.Add(textBoxWeight);
            groupBoxPalletInformation.Controls.Add(label3);
            groupBoxPalletInformation.Controls.Add(textBoxHeight);
            groupBoxPalletInformation.Controls.Add(label2);
            groupBoxPalletInformation.Controls.Add(textBoxWidth);
            groupBoxPalletInformation.Controls.Add(label1);
            groupBoxPalletInformation.Controls.Add(textBoxLength);
            groupBoxPalletInformation.Controls.Add(labelLength);
            groupBoxPalletInformation.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPalletInformation.ForeColor = Color.White;
            groupBoxPalletInformation.Location = new Point(18, 98);
            groupBoxPalletInformation.Name = "groupBoxPalletInformation";
            groupBoxPalletInformation.Size = new Size(331, 638);
            groupBoxPalletInformation.TabIndex = 0;
            groupBoxPalletInformation.TabStop = false;
            groupBoxPalletInformation.Text = "Pallet Information";
            // 
            // labelMnfcCode
            // 
            labelMnfcCode.AutoSize = true;
            labelMnfcCode.Font = new Font("Segoe UI", 11F);
            labelMnfcCode.ForeColor = Color.White;
            labelMnfcCode.Location = new Point(17, 328);
            labelMnfcCode.Name = "labelMnfcCode";
            labelMnfcCode.Size = new Size(88, 20);
            labelMnfcCode.TabIndex = 77;
            labelMnfcCode.Text = "NMFC Code";
            // 
            // comboBoxNmfcCode
            // 
            comboBoxNmfcCode.FormattingEnabled = true;
            comboBoxNmfcCode.Location = new Point(17, 356);
            comboBoxNmfcCode.Name = "comboBoxNmfcCode";
            comboBoxNmfcCode.Size = new Size(298, 28);
            comboBoxNmfcCode.TabIndex = 7;
            comboBoxNmfcCode.DropDown += ComboBoxNmfcCode_DropDown;
            comboBoxNmfcCode.SelectedIndexChanged += ComboBoxNmfcCode_SelectedIndexChanged;
            comboBoxNmfcCode.DropDownClosed += ComboBoxNmfcCode_DropDownClosed;
            // 
            // labelCurrencyCode
            // 
            labelCurrencyCode.AutoSize = true;
            labelCurrencyCode.Font = new Font("Segoe UI", 11F);
            labelCurrencyCode.ForeColor = Color.White;
            labelCurrencyCode.Location = new Point(186, 554);
            labelCurrencyCode.Name = "labelCurrencyCode";
            labelCurrencyCode.Size = new Size(105, 20);
            labelCurrencyCode.TabIndex = 75;
            labelCurrencyCode.Text = "Currency Code";
            // 
            // comboBoxCurrencyCode
            // 
            comboBoxCurrencyCode.FormattingEnabled = true;
            comboBoxCurrencyCode.Location = new Point(182, 585);
            comboBoxCurrencyCode.Name = "comboBoxCurrencyCode";
            comboBoxCurrencyCode.Size = new Size(133, 28);
            comboBoxCurrencyCode.TabIndex = 12;
            // 
            // textBoxEstimatedValue
            // 
            textBoxEstimatedValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxEstimatedValue.Location = new Point(17, 586);
            textBoxEstimatedValue.Name = "textBoxEstimatedValue";
            textBoxEstimatedValue.Size = new Size(141, 27);
            textBoxEstimatedValue.TabIndex = 11;
            textBoxEstimatedValue.KeyPress += TextBoxEstimatedValue_KeyPress;
            // 
            // labelEstimatedValue
            // 
            labelEstimatedValue.AutoSize = true;
            labelEstimatedValue.Font = new Font("Segoe UI", 11F);
            labelEstimatedValue.ForeColor = Color.White;
            labelEstimatedValue.Location = new Point(19, 554);
            labelEstimatedValue.Name = "labelEstimatedValue";
            labelEstimatedValue.Size = new Size(115, 20);
            labelEstimatedValue.TabIndex = 72;
            labelEstimatedValue.Text = "Estimated Value";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxDescription.Location = new Point(17, 144);
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(298, 27);
            textBoxDescription.TabIndex = 2;
            // 
            // labelDescription
            // 
            labelDescription.AutoSize = true;
            labelDescription.Font = new Font("Segoe UI", 11F);
            labelDescription.ForeColor = Color.White;
            labelDescription.Location = new Point(17, 116);
            labelDescription.Name = "labelDescription";
            labelDescription.Size = new Size(85, 20);
            labelDescription.TabIndex = 70;
            labelDescription.Text = "Description";
            // 
            // textBoxItemCount
            // 
            textBoxItemCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxItemCount.Location = new Point(182, 509);
            textBoxItemCount.Name = "textBoxItemCount";
            textBoxItemCount.Size = new Size(133, 27);
            textBoxItemCount.TabIndex = 10;
            textBoxItemCount.KeyPress += TextBoxItemCount_KeyPress;
            // 
            // labelItems
            // 
            labelItems.AutoSize = true;
            labelItems.Font = new Font("Segoe UI", 11F);
            labelItems.ForeColor = Color.White;
            labelItems.Location = new Point(184, 477);
            labelItems.Name = "labelItems";
            labelItems.Size = new Size(82, 20);
            labelItems.TabIndex = 68;
            labelItems.Text = "Item Count";
            // 
            // textBoxCartonCount
            // 
            textBoxCartonCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxCartonCount.Location = new Point(17, 509);
            textBoxCartonCount.Name = "textBoxCartonCount";
            textBoxCartonCount.Size = new Size(141, 27);
            textBoxCartonCount.TabIndex = 9;
            textBoxCartonCount.KeyPress += TextBoxCartonCount_KeyPress;
            // 
            // labelCartons
            // 
            labelCartons.AutoSize = true;
            labelCartons.Font = new Font("Segoe UI", 11F);
            labelCartons.ForeColor = Color.White;
            labelCartons.Location = new Point(19, 477);
            labelCartons.Name = "labelCartons";
            labelCartons.Size = new Size(96, 20);
            labelCartons.TabIndex = 66;
            labelCartons.Text = "Carton Count";
            // 
            // labelClassCode
            // 
            labelClassCode.AutoSize = true;
            labelClassCode.Font = new Font("Segoe UI", 11F);
            labelClassCode.ForeColor = Color.White;
            labelClassCode.Location = new Point(17, 402);
            labelClassCode.Name = "labelClassCode";
            labelClassCode.Size = new Size(81, 20);
            labelClassCode.TabIndex = 65;
            labelClassCode.Text = "Class Code";
            // 
            // comboBoxClassCode
            // 
            comboBoxClassCode.FormattingEnabled = true;
            comboBoxClassCode.Location = new Point(17, 430);
            comboBoxClassCode.Name = "comboBoxClassCode";
            comboBoxClassCode.Size = new Size(298, 28);
            comboBoxClassCode.TabIndex = 8;
            comboBoxClassCode.DropDown += ComboBoxClassCode_DropDown;
            comboBoxClassCode.SelectedIndexChanged += ComboBoxClassCode_SelectedIndexChanged;
            comboBoxClassCode.DropDownClosed += ComboBoxClassCode_DropDownClosed;
            comboBoxClassCode.KeyPress += TextBoxClassCode_KeyPress;
            // 
            // labelRuler
            // 
            labelRuler.AutoSize = true;
            labelRuler.Font = new Font("Segoe UI", 11F);
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
            comboBoxRuler.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
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
            textBoxWeight.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxWeight.Location = new Point(17, 280);
            textBoxWeight.Name = "textBoxWeight";
            textBoxWeight.Size = new Size(298, 27);
            textBoxWeight.TabIndex = 6;
            textBoxWeight.KeyPress += TextBoxWeight_KeyPress;
            textBoxWeight.Leave += TextBoxWeight_Leave;
            textBoxWeight.MouseLeave += TextBoxWeight_Leave;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(17, 252);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 60;
            label3.Text = "Weight";
            // 
            // textBoxHeight
            // 
            textBoxHeight.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxHeight.Location = new Point(227, 210);
            textBoxHeight.Name = "textBoxHeight";
            textBoxHeight.Size = new Size(88, 27);
            textBoxHeight.TabIndex = 5;
            textBoxHeight.KeyPress += TextBoxHeight_KeyPress;
            textBoxHeight.Leave += TextBoxHeight_Leave;
            textBoxHeight.MouseLeave += TextBoxHeight_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(227, 182);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 58;
            label2.Text = "Height";
            // 
            // textBoxWidth
            // 
            textBoxWidth.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxWidth.Location = new Point(121, 210);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.Size = new Size(88, 27);
            textBoxWidth.TabIndex = 4;
            textBoxWidth.KeyPress += TextBoxWidth_KeyPress;
            textBoxWidth.Leave += TextBoxWidth_Leave;
            textBoxWidth.MouseLeave += TextBoxWidth_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.White;
            label1.Location = new Point(121, 182);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 56;
            label1.Text = "Width";
            // 
            // textBoxLength
            // 
            textBoxLength.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxLength.Location = new Point(15, 210);
            textBoxLength.Name = "textBoxLength";
            textBoxLength.Size = new Size(88, 27);
            textBoxLength.TabIndex = 3;
            textBoxLength.KeyPress += TextBoxLength_KeyPress;
            textBoxLength.Leave += TextBoxLength_Leave;
            textBoxLength.MouseLeave += TextBoxLength_Leave;
            // 
            // labelLength
            // 
            labelLength.AutoSize = true;
            labelLength.Font = new Font("Segoe UI", 11F);
            labelLength.ForeColor = Color.White;
            labelLength.Location = new Point(15, 182);
            labelLength.Name = "labelLength";
            labelLength.Size = new Size(54, 20);
            labelLength.TabIndex = 54;
            labelLength.Text = "Length";
            // 
            // groupBoxCodes
            // 
            groupBoxCodes.Controls.Add(textBoxRfId);
            groupBoxCodes.Controls.Add(labelRfId);
            groupBoxCodes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxCodes.ForeColor = Color.White;
            groupBoxCodes.Location = new Point(373, 98);
            groupBoxCodes.Name = "groupBoxCodes";
            groupBoxCodes.Size = new Size(259, 111);
            groupBoxCodes.TabIndex = 13;
            groupBoxCodes.TabStop = false;
            groupBoxCodes.Text = "Tracking Codes";
            // 
            // textBoxRfId
            // 
            textBoxRfId.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            textBoxRfId.Location = new Point(18, 68);
            textBoxRfId.Name = "textBoxRfId";
            textBoxRfId.Size = new Size(223, 27);
            textBoxRfId.TabIndex = 14;
            // 
            // labelRfId
            // 
            labelRfId.AutoSize = true;
            labelRfId.Font = new Font("Segoe UI", 11F);
            labelRfId.ForeColor = Color.White;
            labelRfId.Location = new Point(18, 40);
            labelRfId.Name = "labelRfId";
            labelRfId.Size = new Size(142, 20);
            labelRfId.TabIndex = 56;
            labelRfId.Text = "RF ID Tracking Code";
            // 
            // labelPalletVolume
            // 
            labelPalletVolume.AutoSize = true;
            labelPalletVolume.ForeColor = Color.White;
            labelPalletVolume.Location = new Point(377, 246);
            labelPalletVolume.Name = "labelPalletVolume";
            labelPalletVolume.Size = new Size(102, 20);
            labelPalletVolume.TabIndex = 52;
            labelPalletVolume.Text = "Pallet Volume:";
            // 
            // labelPalletVolumeValue
            // 
            labelPalletVolumeValue.AutoSize = true;
            labelPalletVolumeValue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelPalletVolumeValue.ForeColor = Color.White;
            labelPalletVolumeValue.Location = new Point(379, 279);
            labelPalletVolumeValue.Name = "labelPalletVolumeValue";
            labelPalletVolumeValue.Size = new Size(26, 30);
            labelPalletVolumeValue.TabIndex = 53;
            labelPalletVolumeValue.Text = "0";
            // 
            // checkBoxStackable
            // 
            checkBoxStackable.AutoSize = true;
            checkBoxStackable.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxStackable.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            checkBoxStackable.ForeColor = Color.White;
            checkBoxStackable.Location = new Point(379, 331);
            checkBoxStackable.Name = "checkBoxStackable";
            checkBoxStackable.Size = new Size(137, 24);
            checkBoxStackable.TabIndex = 15;
            checkBoxStackable.Text = "Stackable Pallet";
            checkBoxStackable.UseVisualStyleBackColor = true;
            // 
            // PalletDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            CancelButton = Cancel_Button;
            ClientSize = new Size(651, 770);
            Controls.Add(checkBoxStackable);
            Controls.Add(labelPalletVolumeValue);
            Controls.Add(labelPalletVolume);
            Controls.Add(groupBoxCodes);
            Controls.Add(groupBoxPalletInformation);
            Controls.Add(pictureBox1);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panelHeader);
            Font = new Font("Segoe UI", 11F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "PalletDialog";
            Text = AppInfo.WindowTitle("Pallet Editor");
            TopMost = true;
            Load += PalletDialog_Load;
            Shown += PalletDialog_Shown;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxPalletInformation.ResumeLayout(false);
            groupBoxPalletInformation.PerformLayout();
            groupBoxCodes.ResumeLayout(false);
            groupBoxCodes.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private Button OK_Button;
        private Button Cancel_Button;
        private PictureBox pictureBox1;
        private GroupBox groupBoxPalletInformation;
        private TextBox textBoxLength;
        private Label labelLength;
        private Label labelRuler;
        private ComboBox comboBoxRuler;
        private TextBox textBoxWeight;
        private Label label3;
        private TextBox textBoxHeight;
        private Label label2;
        private TextBox textBoxWidth;
        private Label label1;
        private Label labelClassCode;
        private ComboBox comboBoxClassCode;
        private TextBox textBoxItemCount;
        private Label labelItems;
        private TextBox textBoxCartonCount;
        private Label labelCartons;
        private TextBox textBoxDescription;
        private Label labelDescription;
        private GroupBox groupBoxCodes;
        private TextBox textBoxRfId;
        private Label labelRfId;
        private TextBox textBoxEstimatedValue;
        private Label labelEstimatedValue;
        private Label labelCurrencyCode;
        private ComboBox comboBoxCurrencyCode;
        private Label labelMnfcCode;
        private ComboBox comboBoxNmfcCode;
        private Label labelUnitOfMeasurement;
        private Label labelBolTotalWeight;
        private Label labelPalletVolume;
        private Label labelPalletVolumeValue;
        private CheckBox checkBoxStackable;
    }
}
