namespace SimpleBol.WinForms
{
    partial class ShippersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShippersForm));
            PanelHeader = new Panel();
            buttonReturn = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            Btn_Close = new Button();
            panelRight = new Panel();
            buttonEditShipper = new Button();
            buttonCreateShipper = new Button();
            panelLeft = new Panel();
            groupBoxReconcile = new GroupBox();
            labelShipperFilterResults = new Label();
            comboBoxServices = new ComboBox();
            labelReconcile = new Label();
            listViewShippers = new ListView();
            PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            panelRight.SuspendLayout();
            panelLeft.SuspendLayout();
            groupBoxReconcile.SuspendLayout();
            SuspendLayout();
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.Black;
            PanelHeader.Controls.Add(buttonReturn);
            PanelHeader.Controls.Add(LabelHeader);
            PanelHeader.Controls.Add(PictureBox_Header);
            PanelHeader.Controls.Add(Btn_Close);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(1273, 80);
            PanelHeader.TabIndex = 1;
            // 
            // buttonReturn
            // 
            buttonReturn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReturn.BackgroundImageLayout = ImageLayout.Stretch;
            buttonReturn.Cursor = Cursors.Hand;
            buttonReturn.FlatAppearance.BorderSize = 0;
            buttonReturn.FlatStyle = FlatStyle.Flat;
            buttonReturn.Image = (Image)resources.GetObject("buttonReturn.Image");
            buttonReturn.Location = new Point(1193, 16);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Padding = new Padding(4);
            buttonReturn.Size = new Size(45, 45);
            buttonReturn.TabIndex = 4;
            buttonReturn.UseVisualStyleBackColor = true;
            buttonReturn.Click += ButtonReturn_Click;
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Point);
            LabelHeader.ForeColor = Color.White;
            LabelHeader.Location = new Point(120, 16);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(166, 51);
            LabelHeader.TabIndex = 4;
            LabelHeader.Text = "Shippers";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.Image = Properties.Resources.badgeShippers150;
            PictureBox_Header.InitialImage = Properties.Resources.badgeShippers150;
            PictureBox_Header.Location = new Point(20, 6);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 65);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 3;
            PictureBox_Header.TabStop = false;
            // 
            // Btn_Close
            // 
            Btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Close.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Close.Cursor = Cursors.Hand;
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(2041, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 20);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEditShipper);
            panelRight.Controls.Add(buttonCreateShipper);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1073, 80);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 799);
            panelRight.TabIndex = 5;
            // 
            // buttonEditShipper
            // 
            buttonEditShipper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditShipper.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditShipper.FlatAppearance.BorderSize = 0;
            buttonEditShipper.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditShipper.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditShipper.FlatStyle = FlatStyle.Flat;
            buttonEditShipper.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditShipper.Location = new Point(35, 109);
            buttonEditShipper.Name = "buttonEditShipper";
            buttonEditShipper.Size = new Size(130, 51);
            buttonEditShipper.TabIndex = 3;
            buttonEditShipper.Text = "Edit Shipper";
            buttonEditShipper.UseVisualStyleBackColor = false;
            buttonEditShipper.Click += ButtonEditShipper_Click;
            // 
            // buttonCreateShipper
            // 
            buttonCreateShipper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateShipper.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateShipper.FlatAppearance.BorderSize = 0;
            buttonCreateShipper.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateShipper.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateShipper.FlatStyle = FlatStyle.Flat;
            buttonCreateShipper.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateShipper.Location = new Point(35, 31);
            buttonCreateShipper.Name = "buttonCreateShipper";
            buttonCreateShipper.Size = new Size(130, 51);
            buttonCreateShipper.TabIndex = 2;
            buttonCreateShipper.Text = "Create Shipper";
            buttonCreateShipper.UseVisualStyleBackColor = false;
            buttonCreateShipper.Click += ButtonCreateShipper_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Controls.Add(groupBoxReconcile);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 80);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 799);
            panelLeft.TabIndex = 4;
            // 
            // groupBoxReconcile
            // 
            groupBoxReconcile.Controls.Add(labelShipperFilterResults);
            groupBoxReconcile.Controls.Add(comboBoxServices);
            groupBoxReconcile.Controls.Add(labelReconcile);
            groupBoxReconcile.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxReconcile.ForeColor = Color.White;
            groupBoxReconcile.Location = new Point(19, 7);
            groupBoxReconcile.Name = "groupBoxReconcile";
            groupBoxReconcile.Size = new Size(245, 130);
            groupBoxReconcile.TabIndex = 6;
            groupBoxReconcile.TabStop = false;
            groupBoxReconcile.Text = "Shippers Filter";
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
            // comboBoxServices
            // 
            comboBoxServices.FormattingEnabled = true;
            comboBoxServices.Location = new Point(12, 54);
            comboBoxServices.Name = "comboBoxServices";
            comboBoxServices.Size = new Size(218, 28);
            comboBoxServices.TabIndex = 6;
            comboBoxServices.SelectedIndexChanged += ComboBoxServices_SelectedIndexChanged;
            // 
            // labelReconcile
            // 
            labelReconcile.AutoSize = true;
            labelReconcile.Location = new Point(12, 27);
            labelReconcile.Name = "labelReconcile";
            labelReconcile.Size = new Size(100, 20);
            labelReconcile.TabIndex = 5;
            labelReconcile.Text = "Select a filter";
            // 
            // listViewShippers
            // 
            listViewShippers.Dock = DockStyle.Fill;
            listViewShippers.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            listViewShippers.Location = new Point(280, 80);
            listViewShippers.Name = "listViewShippers";
            listViewShippers.Size = new Size(793, 799);
            listViewShippers.TabIndex = 1;
            listViewShippers.UseCompatibleStateImageBehavior = false;
            listViewShippers.ColumnClick += ListViewShippers_ColumnClick;
            listViewShippers.DoubleClick += ListViewShippers_DoubleClick;
            // 
            // ShippersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(1273, 879);
            ControlBox = false;
            Controls.Add(listViewShippers);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(PanelHeader);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ShippersForm";
            Text = "ShippersForm";
            Load += ShippersFormLoad;
            Shown += ShippersFormShown;
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            groupBoxReconcile.ResumeLayout(false);
            groupBoxReconcile.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelHeader;
        private Button Btn_Close;
        private Button buttonReturn;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Panel panelRight;
        private Panel panelLeft;
        private Button buttonCreateShipper;
        private Button buttonEditShipper;
        private ListView listViewShippers;
        private GroupBox groupBoxReconcile;
        private Label labelShipperFilterResults;
        private ComboBox comboBoxServices;
        private Label labelReconcile;
    }
}