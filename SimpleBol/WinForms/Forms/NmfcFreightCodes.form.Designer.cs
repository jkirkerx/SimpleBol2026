namespace SimpleBol.WinForms.Forms
{
    partial class NmfcFreightCodesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NmfcFreightCodesForm));
            listViewCodes = new ListView();
            panelRight = new Panel();
            buttonDisableCode = new Button();
            buttonEnableCode = new Button();
            buttonEditNmfcCode = new Button();
            buttonCreateNmfcCode = new Button();
            panelLeft = new Panel();
            groupBoxSearchFilter = new GroupBox();
            buttonSearchFilter = new Button();
            textBoxSearchFilter = new TextBox();
            labelSearchFilterResults = new Label();
            labelSearchFilter = new Label();
            Panel_Header = new Panel();
            panelPaginate = new Panel();
            label1 = new Label();
            numericUpDownShow = new NumericUpDown();
            buttonReturn = new Button();
            Btn_Close = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            buttonSearchFilterReset = new Button();
            panelRight.SuspendLayout();
            panelLeft.SuspendLayout();
            groupBoxSearchFilter.SuspendLayout();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            SuspendLayout();
            // 
            // listViewCodes
            // 
            listViewCodes.Dock = DockStyle.Fill;
            listViewCodes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            listViewCodes.Location = new Point(280, 70);
            listViewCodes.Name = "listViewCodes";
            listViewCodes.Size = new Size(748, 754);
            listViewCodes.TabIndex = 12;
            listViewCodes.UseCompatibleStateImageBehavior = false;
            listViewCodes.ColumnClick += ListViewCodesColumn_Click;
            listViewCodes.SelectedIndexChanged += ListViewCodes_SelectedIndexChanged;
            listViewCodes.DoubleClick += ListViewCodes_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonDisableCode);
            panelRight.Controls.Add(buttonEnableCode);
            panelRight.Controls.Add(buttonEditNmfcCode);
            panelRight.Controls.Add(buttonCreateNmfcCode);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1028, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 754);
            panelRight.TabIndex = 11;
            // 
            // buttonDisableCode
            // 
            buttonDisableCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonDisableCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonDisableCode.FlatAppearance.BorderSize = 0;
            buttonDisableCode.FlatAppearance.MouseDownBackColor = Color.Gold;
            buttonDisableCode.FlatAppearance.MouseOverBackColor = Color.Gold;
            buttonDisableCode.FlatStyle = FlatStyle.Flat;
            buttonDisableCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDisableCode.ForeColor = Color.White;
            buttonDisableCode.Location = new Point(35, 375);
            buttonDisableCode.Name = "buttonDisableCode";
            buttonDisableCode.Size = new Size(130, 51);
            buttonDisableCode.TabIndex = 7;
            buttonDisableCode.Text = "Disable Code";
            buttonDisableCode.UseVisualStyleBackColor = false;
            buttonDisableCode.Click += ButtonDisableCode_Click;
            // 
            // buttonEnableCode
            // 
            buttonEnableCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEnableCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonEnableCode.FlatAppearance.BorderSize = 0;
            buttonEnableCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEnableCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEnableCode.FlatStyle = FlatStyle.Flat;
            buttonEnableCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEnableCode.ForeColor = Color.White;
            buttonEnableCode.Location = new Point(35, 304);
            buttonEnableCode.Name = "buttonEnableCode";
            buttonEnableCode.Size = new Size(130, 51);
            buttonEnableCode.TabIndex = 6;
            buttonEnableCode.Text = "Enable Code";
            buttonEnableCode.UseVisualStyleBackColor = false;
            buttonEnableCode.Click += ButtonEnableCode_Click;
            // 
            // buttonEditNmfcCode
            // 
            buttonEditNmfcCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditNmfcCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditNmfcCode.FlatAppearance.BorderSize = 0;
            buttonEditNmfcCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditNmfcCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditNmfcCode.FlatStyle = FlatStyle.Flat;
            buttonEditNmfcCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditNmfcCode.ForeColor = Color.White;
            buttonEditNmfcCode.Location = new Point(35, 109);
            buttonEditNmfcCode.Name = "buttonEditNmfcCode";
            buttonEditNmfcCode.Size = new Size(130, 51);
            buttonEditNmfcCode.TabIndex = 3;
            buttonEditNmfcCode.Text = "Edit Nmfc";
            buttonEditNmfcCode.UseVisualStyleBackColor = false;
            buttonEditNmfcCode.Click += ButtonEditNmfcCode_Click;
            // 
            // buttonCreateNmfcCode
            // 
            buttonCreateNmfcCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateNmfcCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateNmfcCode.FlatAppearance.BorderSize = 0;
            buttonCreateNmfcCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateNmfcCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateNmfcCode.FlatStyle = FlatStyle.Flat;
            buttonCreateNmfcCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateNmfcCode.ForeColor = Color.White;
            buttonCreateNmfcCode.Location = new Point(35, 31);
            buttonCreateNmfcCode.Name = "buttonCreateNmfcCode";
            buttonCreateNmfcCode.Size = new Size(130, 51);
            buttonCreateNmfcCode.TabIndex = 2;
            buttonCreateNmfcCode.Text = "Create Nmfc";
            buttonCreateNmfcCode.UseVisualStyleBackColor = false;
            buttonCreateNmfcCode.Click += ButtonCreateNmfcCode_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Controls.Add(groupBoxSearchFilter);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 754);
            panelLeft.TabIndex = 10;
            // 
            // groupBoxSearchFilter
            // 
            groupBoxSearchFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxSearchFilter.Controls.Add(buttonSearchFilterReset);
            groupBoxSearchFilter.Controls.Add(buttonSearchFilter);
            groupBoxSearchFilter.Controls.Add(textBoxSearchFilter);
            groupBoxSearchFilter.Controls.Add(labelSearchFilterResults);
            groupBoxSearchFilter.Controls.Add(labelSearchFilter);
            groupBoxSearchFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxSearchFilter.ForeColor = Color.Gainsboro;
            groupBoxSearchFilter.Location = new Point(12, 17);
            groupBoxSearchFilter.Name = "groupBoxSearchFilter";
            groupBoxSearchFilter.Size = new Size(245, 172);
            groupBoxSearchFilter.TabIndex = 4;
            groupBoxSearchFilter.TabStop = false;
            groupBoxSearchFilter.Text = "Search";
            // 
            // buttonSearchFilter
            // 
            buttonSearchFilter.BackColor = Color.FromArgb(60, 60, 60);
            buttonSearchFilter.FlatAppearance.BorderSize = 0;
            buttonSearchFilter.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonSearchFilter.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonSearchFilter.FlatStyle = FlatStyle.Flat;
            buttonSearchFilter.Location = new Point(133, 92);
            buttonSearchFilter.Name = "buttonSearchFilter";
            buttonSearchFilter.Size = new Size(100, 37);
            buttonSearchFilter.TabIndex = 6;
            buttonSearchFilter.Text = "Submit";
            buttonSearchFilter.UseVisualStyleBackColor = false;
            buttonSearchFilter.Click += ButtonSearchFilter_Click;
            // 
            // textBoxSearchFilter
            // 
            textBoxSearchFilter.Location = new Point(15, 55);
            textBoxSearchFilter.Name = "textBoxSearchFilter";
            textBoxSearchFilter.Size = new Size(218, 27);
            textBoxSearchFilter.TabIndex = 5;
            textBoxSearchFilter.KeyDown += TextBoxSearchFilter_KeyDown;
            // 
            // labelSearchFilterResults
            // 
            labelSearchFilterResults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelSearchFilterResults.AutoSize = true;
            labelSearchFilterResults.Location = new Point(15, 142);
            labelSearchFilterResults.Name = "labelSearchFilterResults";
            labelSearchFilterResults.Size = new Size(152, 20);
            labelSearchFilterResults.TabIndex = 4;
            labelSearchFilterResults.Text = "0 Records(s) located";
            // 
            // labelSearchFilter
            // 
            labelSearchFilter.AutoSize = true;
            labelSearchFilter.Location = new Point(15, 27);
            labelSearchFilter.Name = "labelSearchFilter";
            labelSearchFilter.Size = new Size(134, 20);
            labelSearchFilter.TabIndex = 2;
            labelSearchFilter.Text = "Filter by keyword:";
            // 
            // Panel_Header
            // 
            Panel_Header.BackColor = Color.Black;
            Panel_Header.Controls.Add(panelPaginate);
            Panel_Header.Controls.Add(buttonReturn);
            Panel_Header.Controls.Add(Btn_Close);
            Panel_Header.Controls.Add(LabelHeader);
            Panel_Header.Controls.Add(PictureBox_Header);
            Panel_Header.Dock = DockStyle.Top;
            Panel_Header.Location = new Point(0, 0);
            Panel_Header.Name = "Panel_Header";
            Panel_Header.Size = new Size(1228, 70);
            Panel_Header.TabIndex = 9;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(3047, 22);
            panelPaginate.Name = "panelPaginate";
            panelPaginate.Size = new Size(121, 0);
            panelPaginate.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(-152, 11);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(45, 19);
            label1.TabIndex = 6;
            label1.Text = "Show";
            // 
            // numericUpDownShow
            // 
            numericUpDownShow.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numericUpDownShow.BorderStyle = BorderStyle.None;
            numericUpDownShow.Cursor = Cursors.Hand;
            numericUpDownShow.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            numericUpDownShow.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownShow.Location = new Point(-101, 11);
            numericUpDownShow.Name = "numericUpDownShow";
            numericUpDownShow.Size = new Size(56, 23);
            numericUpDownShow.TabIndex = 5;
            numericUpDownShow.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonReturn
            // 
            buttonReturn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReturn.BackgroundImageLayout = ImageLayout.Stretch;
            buttonReturn.Cursor = Cursors.Hand;
            buttonReturn.FlatAppearance.BorderSize = 0;
            buttonReturn.FlatStyle = FlatStyle.Flat;
            buttonReturn.Image = (Image)resources.GetObject("buttonReturn.Image");
            buttonReturn.Location = new Point(2205, 12);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Padding = new Padding(4);
            buttonReturn.Size = new Size(45, 15);
            buttonReturn.TabIndex = 2;
            buttonReturn.UseVisualStyleBackColor = true;
            // 
            // Btn_Close
            // 
            Btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Close.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Close.Cursor = Cursors.Hand;
            Btn_Close.FlatAppearance.BorderSize = 0;
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(1164, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(45, 45);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            Btn_Close.Click += ButtonReturn_Click;
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Point);
            LabelHeader.ForeColor = Color.White;
            LabelHeader.Location = new Point(120, 14);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(531, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "NMFC Codes and Descriptions";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = Properties.Resources.NmfcCodesIcon150;
            PictureBox_Header.InitialImage = null;
            PictureBox_Header.Location = new Point(20, 9);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 55);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 0;
            PictureBox_Header.TabStop = false;
            // 
            // buttonSearchFilterReset
            // 
            buttonSearchFilterReset.BackColor = Color.FromArgb(60, 60, 60);
            buttonSearchFilterReset.FlatAppearance.BorderSize = 0;
            buttonSearchFilterReset.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonSearchFilterReset.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonSearchFilterReset.FlatStyle = FlatStyle.Flat;
            buttonSearchFilterReset.Location = new Point(15, 92);
            buttonSearchFilterReset.Name = "buttonSearchFilterReset";
            buttonSearchFilterReset.Size = new Size(100, 37);
            buttonSearchFilterReset.TabIndex = 7;
            buttonSearchFilterReset.Text = "Reset";
            buttonSearchFilterReset.UseVisualStyleBackColor = false;
            buttonSearchFilterReset.Click += ButtonSearchFilterReset_Click;
            // 
            // NmfcFreightCodesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1228, 824);
            Controls.Add(listViewCodes);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            Name = "NmfcFreightCodesForm";
            Text = "NmfcFreightCodesForm";
            Load += NmfcCodesForm_Load;
            Shown += NmfcCodesForm_Shown;
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            groupBoxSearchFilter.ResumeLayout(false);
            groupBoxSearchFilter.PerformLayout();
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewCodes;
        private Panel panelRight;
        private Button buttonEditNmfcCode;
        private Button buttonCreateNmfcCode;
        private Panel panelLeft;
        private Panel Panel_Header;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Button buttonReturn;
        private Button Btn_Close;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Button buttonDisableCode;
        private Button buttonEnableCode;
        private GroupBox groupBoxSearchFilter;
        private Label labelSearchFilterResults;
        private Label labelSearchFilter;
        private TextBox textBoxSearchFilter;
        private Button buttonSearchFilter;
        private Button buttonSearchFilterReset;
    }
}