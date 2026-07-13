namespace SimpleBol.WinForms.Forms
{
    partial class BillingDisputesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillingDisputesForm));
            listViewDisputes = new ListView();
            panelRight = new Panel();
            buttonEditDispute = new Button();
            buttonCreateDispute = new Button();
            panelLeft = new Panel();
            groupBoxShippers = new GroupBox();
            labelShipperFilterResults = new Label();
            comboBoxShippers = new ComboBox();
            labelShippers = new Label();
            Panel_Header = new Panel();
            panelPaginate = new Panel();
            label1 = new Label();
            numericUpDownShow = new NumericUpDown();
            buttonReturn = new Button();
            Btn_Close = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            panelRight.SuspendLayout();
            panelLeft.SuspendLayout();
            groupBoxShippers.SuspendLayout();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            SuspendLayout();
            // 
            // listViewDisputes
            // 
            listViewDisputes.Dock = DockStyle.Fill;
            listViewDisputes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            listViewDisputes.Location = new Point(280, 70);
            listViewDisputes.Name = "listViewDisputes";
            listViewDisputes.Size = new Size(729, 653);
            listViewDisputes.TabIndex = 20;
            listViewDisputes.UseCompatibleStateImageBehavior = false;
            listViewDisputes.ColumnClick += ListViewDisputes_ColumnClick;
            listViewDisputes.SelectedIndexChanged += ListViewDisputes_SelectedIndexChanged;
            listViewDisputes.DoubleClick += ListViewDisputes_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEditDispute);
            panelRight.Controls.Add(buttonCreateDispute);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1009, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 653);
            panelRight.TabIndex = 19;
            // 
            // buttonEditDispute
            // 
            buttonEditDispute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditDispute.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditDispute.Cursor = Cursors.Hand;
            buttonEditDispute.FlatAppearance.BorderSize = 0;
            buttonEditDispute.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditDispute.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditDispute.FlatStyle = FlatStyle.Flat;
            buttonEditDispute.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditDispute.ForeColor = Color.White;
            buttonEditDispute.Location = new Point(35, 109);
            buttonEditDispute.Name = "buttonEditDispute";
            buttonEditDispute.Size = new Size(130, 51);
            buttonEditDispute.TabIndex = 3;
            buttonEditDispute.Text = "Edit Dispute";
            buttonEditDispute.UseVisualStyleBackColor = false;
            buttonEditDispute.Click += ButtonEditDispute_Click;
            // 
            // buttonCreateDispute
            // 
            buttonCreateDispute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateDispute.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateDispute.Cursor = Cursors.Hand;
            buttonCreateDispute.FlatAppearance.BorderSize = 0;
            buttonCreateDispute.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateDispute.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateDispute.FlatStyle = FlatStyle.Flat;
            buttonCreateDispute.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateDispute.ForeColor = Color.White;
            buttonCreateDispute.Location = new Point(35, 31);
            buttonCreateDispute.Name = "buttonCreateDispute";
            buttonCreateDispute.Size = new Size(130, 51);
            buttonCreateDispute.TabIndex = 2;
            buttonCreateDispute.Text = "Create Dispute";
            buttonCreateDispute.UseVisualStyleBackColor = false;
            buttonCreateDispute.Click += ButtonCreateDispute_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Controls.Add(groupBoxShippers);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 653);
            panelLeft.TabIndex = 18;
            // 
            // groupBoxShippers
            // 
            groupBoxShippers.Controls.Add(labelShipperFilterResults);
            groupBoxShippers.Controls.Add(comboBoxShippers);
            groupBoxShippers.Controls.Add(labelShippers);
            groupBoxShippers.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxShippers.ForeColor = Color.White;
            groupBoxShippers.Location = new Point(19, 7);
            groupBoxShippers.Name = "groupBoxShippers";
            groupBoxShippers.Size = new Size(245, 130);
            groupBoxShippers.TabIndex = 7;
            groupBoxShippers.TabStop = false;
            groupBoxShippers.Text = "Shippers Filter";
            // 
            // labelShipperFilterResults
            // 
            labelShipperFilterResults.AutoSize = true;
            labelShipperFilterResults.Location = new Point(12, 95);
            labelShipperFilterResults.Name = "labelShipperFilterResults";
            labelShipperFilterResults.Size = new Size(152, 20);
            labelShipperFilterResults.TabIndex = 7;
            labelShipperFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxShippers
            // 
            comboBoxShippers.FormattingEnabled = true;
            comboBoxShippers.Location = new Point(12, 54);
            comboBoxShippers.Name = "comboBoxShippers";
            comboBoxShippers.Size = new Size(218, 28);
            comboBoxShippers.TabIndex = 6;
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            // 
            // labelShippers
            // 
            labelShippers.AutoSize = true;
            labelShippers.Location = new Point(12, 27);
            labelShippers.Name = "labelShippers";
            labelShippers.Size = new Size(100, 20);
            labelShippers.TabIndex = 5;
            labelShippers.Text = "Select a filter";
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
            Panel_Header.Size = new Size(1209, 70);
            Panel_Header.TabIndex = 17;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(5094, 22);
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
            label1.Location = new Point(-310, 11);
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
            numericUpDownShow.Location = new Point(-259, 11);
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
            buttonReturn.Location = new Point(1129, 14);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Padding = new Padding(4);
            buttonReturn.Size = new Size(45, 45);
            buttonReturn.TabIndex = 2;
            buttonReturn.UseVisualStyleBackColor = true;
            buttonReturn.Click += ButtonReturn_Click;
            // 
            // Btn_Close
            // 
            Btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Close.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Close.Cursor = Cursors.Hand;
            Btn_Close.FlatAppearance.BorderSize = 0;
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(2154, 14);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(45, 15);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Point);
            LabelHeader.ForeColor = Color.White;
            LabelHeader.Location = new Point(120, 14);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(358, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "BOL Billing Disputes";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = (Image)resources.GetObject("PictureBox_Header.Image");
            PictureBox_Header.InitialImage = null;
            PictureBox_Header.Location = new Point(20, 9);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 65);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 0;
            PictureBox_Header.TabStop = false;
            // 
            // BillingDisputesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1209, 723);
            Controls.Add(listViewDisputes);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BillingDisputesForm";
            Text = "BillingDisputesForm";
            Load += BillingDisputesForm_Load;
            Shown += BillingDisputesForm_Shown;
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            groupBoxShippers.ResumeLayout(false);
            groupBoxShippers.PerformLayout();
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewDisputes;
        private Panel panelRight;
        private Button buttonEditDispute;
        private Button buttonCreateDispute;
        private Panel panelLeft;
        private Panel Panel_Header;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Button buttonReturn;
        private Button Btn_Close;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private GroupBox groupBoxShippers;
        private Label labelShipperFilterResults;
        private ComboBox comboBoxShippers;
        private Label labelShippers;
    }
}