namespace SimpleBol.WinForms
{
    partial class VendorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VendorsForm));
            listViewVendors = new ListView();
            panelRight = new Panel();
            buttonEditVendor = new Button();
            buttonCreateVendor = new Button();
            panelLeft = new Panel();
            PanelHeader = new Panel();
            buttonReturn = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            Btn_Close = new Button();
            panelRight.SuspendLayout();
            PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            SuspendLayout();
            // 
            // listViewVendors
            // 
            listViewVendors.Dock = DockStyle.Fill;
            listViewVendors.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            listViewVendors.Location = new Point(280, 80);
            listViewVendors.Name = "listViewVendors";
            listViewVendors.Size = new Size(777, 760);
            listViewVendors.TabIndex = 1;
            listViewVendors.UseCompatibleStateImageBehavior = false;
            listViewVendors.ColumnClick += ListViewVendors_ColumnClick;
            listViewVendors.SelectedIndexChanged += ListViewVendors_SelectedIndexChanged;
            listViewVendors.DoubleClick += ListViewVendors_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEditVendor);
            panelRight.Controls.Add(buttonCreateVendor);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1057, 80);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 760);
            panelRight.TabIndex = 9;
            // 
            // buttonEditVendor
            // 
            buttonEditVendor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditVendor.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditVendor.FlatAppearance.BorderSize = 0;
            buttonEditVendor.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditVendor.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditVendor.FlatStyle = FlatStyle.Flat;
            buttonEditVendor.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditVendor.ForeColor = Color.White;
            buttonEditVendor.Location = new Point(35, 109);
            buttonEditVendor.Name = "buttonEditVendor";
            buttonEditVendor.Size = new Size(130, 51);
            buttonEditVendor.TabIndex = 3;
            buttonEditVendor.Text = "Edit Vendor";
            buttonEditVendor.UseVisualStyleBackColor = false;
            buttonEditVendor.Click += ButtonEditVendor_Click;
            // 
            // buttonCreateVendor
            // 
            buttonCreateVendor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateVendor.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateVendor.FlatAppearance.BorderSize = 0;
            buttonCreateVendor.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateVendor.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateVendor.FlatStyle = FlatStyle.Flat;
            buttonCreateVendor.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateVendor.ForeColor = Color.White;
            buttonCreateVendor.Location = new Point(35, 31);
            buttonCreateVendor.Name = "buttonCreateVendor";
            buttonCreateVendor.Size = new Size(130, 51);
            buttonCreateVendor.TabIndex = 2;
            buttonCreateVendor.Text = "Create Vendor";
            buttonCreateVendor.UseVisualStyleBackColor = false;
            buttonCreateVendor.Click += ButtonCreateVendor_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 80);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 760);
            panelLeft.TabIndex = 8;
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
            PanelHeader.Size = new Size(1257, 80);
            PanelHeader.TabIndex = 7;
            // 
            // buttonReturn
            // 
            buttonReturn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReturn.BackgroundImageLayout = ImageLayout.Stretch;
            buttonReturn.Cursor = Cursors.Hand;
            buttonReturn.FlatAppearance.BorderSize = 0;
            buttonReturn.FlatStyle = FlatStyle.Flat;
            buttonReturn.Image = (Image)resources.GetObject("buttonReturn.Image");
            buttonReturn.Location = new Point(1177, 16);
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
            LabelHeader.Size = new Size(364, 51);
            LabelHeader.TabIndex = 4;
            LabelHeader.Text = "Vendors / Ship From";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = Properties.Resources.vendorsIcon65;
            PictureBox_Header.InitialImage = null;
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
            Btn_Close.Location = new Point(3098, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 0);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // VendorsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 840);
            Controls.Add(listViewVendors);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(PanelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VendorsForm";
            Text = "VendorsForm";
            Load += VendorsFormLoad;
            Shown += VendorsFormShown;
            panelRight.ResumeLayout(false);
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewVendors;
        private Panel panelRight;
        private Button buttonEditVendor;
        private Button buttonCreateVendor;
        private Panel panelLeft;
        private Panel PanelHeader;
        private Button buttonReturn;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Button Btn_Close;
    }
}