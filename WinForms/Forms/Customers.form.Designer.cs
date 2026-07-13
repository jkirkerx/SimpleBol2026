namespace SimpleBol.WinForms
{
    partial class CustomersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomersForm));
            listViewCustomers = new ListView();
            panelRight = new Panel();
            buttonEditCustomer = new Button();
            buttonCreateCustomer = new Button();
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
            // listViewCustomers
            // 
            listViewCustomers.Dock = DockStyle.Fill;
            listViewCustomers.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            listViewCustomers.Location = new Point(280, 80);
            listViewCustomers.Name = "listViewCustomers";
            listViewCustomers.Size = new Size(793, 799);
            listViewCustomers.TabIndex = 1;
            listViewCustomers.UseCompatibleStateImageBehavior = false;
            listViewCustomers.ColumnClick += ListViewCustomers_ColumnClick;
            listViewCustomers.SelectedIndexChanged += ListViewCustomers_SelectedIndexChanged;
            listViewCustomers.DoubleClick += ListViewCustomers_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEditCustomer);
            panelRight.Controls.Add(buttonCreateCustomer);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1073, 80);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 799);
            panelRight.TabIndex = 13;
            // 
            // buttonEditCustomer
            // 
            buttonEditCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditCustomer.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditCustomer.Cursor = Cursors.Hand;
            buttonEditCustomer.FlatAppearance.BorderSize = 0;
            buttonEditCustomer.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditCustomer.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditCustomer.FlatStyle = FlatStyle.Flat;
            buttonEditCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditCustomer.ForeColor = Color.White;
            buttonEditCustomer.Location = new Point(35, 109);
            buttonEditCustomer.Name = "buttonEditCustomer";
            buttonEditCustomer.Size = new Size(130, 51);
            buttonEditCustomer.TabIndex = 3;
            buttonEditCustomer.Text = "Edit Customer";
            buttonEditCustomer.UseVisualStyleBackColor = false;
            buttonEditCustomer.Click += ButtonEditCustomer_Click;
            // 
            // buttonCreateCustomer
            // 
            buttonCreateCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateCustomer.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateCustomer.Cursor = Cursors.Hand;
            buttonCreateCustomer.FlatAppearance.BorderSize = 0;
            buttonCreateCustomer.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateCustomer.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateCustomer.FlatStyle = FlatStyle.Flat;
            buttonCreateCustomer.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateCustomer.ForeColor = Color.White;
            buttonCreateCustomer.Location = new Point(35, 31);
            buttonCreateCustomer.Name = "buttonCreateCustomer";
            buttonCreateCustomer.Size = new Size(130, 51);
            buttonCreateCustomer.TabIndex = 2;
            buttonCreateCustomer.Text = "Create Customer";
            buttonCreateCustomer.UseVisualStyleBackColor = false;
            buttonCreateCustomer.Click += ButtonCreateCustomer_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 80);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 799);
            panelLeft.TabIndex = 12;
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
            PanelHeader.TabIndex = 11;
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
            LabelHeader.Size = new Size(357, 51);
            LabelHeader.TabIndex = 4;
            LabelHeader.Text = "Customers / Ship To";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = (Image)resources.GetObject("PictureBox_Header.Image");
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
            Btn_Close.Location = new Point(4171, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 0);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // CustomersForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1273, 879);
            Controls.Add(listViewCustomers);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(PanelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomersForm";
            Text = "Customers";
            Load += CustomersFormLoad;
            Shown += CustomersFormShown;
            panelRight.ResumeLayout(false);
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewCustomers;
        private Panel panelRight;
        private Button buttonEditCustomer;
        private Button buttonCreateCustomer;
        private Panel panelLeft;
        private Panel PanelHeader;
        private Button buttonReturn;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Button Btn_Close;
    }
}