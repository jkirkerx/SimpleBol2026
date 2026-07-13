namespace SimpleBol.WinForms
{
    partial class ProgramMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramMenuForm));
            panelHeader = new Panel();
            pictureBox2 = new PictureBox();
            Btn_Close = new Button();
            panelSideBar_Left = new Panel();
            buttonPowerDown = new Button();
            panelMenu = new Panel();
            buttonBlank2 = new Button();
            buttonBolDispute = new Button();
            buttonCreateBol = new Button();
            panelProggrams = new Panel();
            buttonFreightClassCodes = new Button();
            buttonNmfcFreightCodes = new Button();
            buttonCustomers = new Button();
            buttonVendors = new Button();
            buttonShippers = new Button();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelSideBar_Left.SuspendLayout();
            panelMenu.SuspendLayout();
            panelProggrams.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Black;
            panelHeader.Controls.Add(pictureBox2);
            panelHeader.Controls.Add(Btn_Close);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(996, 65);
            panelHeader.TabIndex = 2;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(29, 6);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(281, 53);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // Btn_Close
            // 
            Btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Close.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(2919, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 0);
            Btn_Close.TabIndex = 3;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // panelSideBar_Left
            // 
            panelSideBar_Left.BackColor = Color.FromArgb(40, 40, 40);
            panelSideBar_Left.Controls.Add(buttonPowerDown);
            panelSideBar_Left.Dock = DockStyle.Left;
            panelSideBar_Left.Location = new Point(0, 65);
            panelSideBar_Left.Name = "panelSideBar_Left";
            panelSideBar_Left.Size = new Size(160, 653);
            panelSideBar_Left.TabIndex = 3;
            // 
            // buttonPowerDown
            // 
            buttonPowerDown.Cursor = Cursors.Hand;
            buttonPowerDown.FlatAppearance.BorderSize = 0;
            buttonPowerDown.FlatStyle = FlatStyle.Flat;
            buttonPowerDown.Image = (Image)resources.GetObject("buttonPowerDown.Image");
            buttonPowerDown.Location = new Point(43, 20);
            buttonPowerDown.Name = "buttonPowerDown";
            buttonPowerDown.Size = new Size(75, 74);
            buttonPowerDown.TabIndex = 5;
            buttonPowerDown.UseVisualStyleBackColor = true;
            buttonPowerDown.Click += ButtonPowerDown_Click;
            // 
            // panelMenu
            // 
            panelMenu.Controls.Add(buttonBlank2);
            panelMenu.Controls.Add(buttonBolDispute);
            panelMenu.Controls.Add(buttonCreateBol);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Location = new Point(160, 65);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(836, 653);
            panelMenu.TabIndex = 4;
            // 
            // buttonBlank2
            // 
            buttonBlank2.BackColor = Color.Transparent;
            buttonBlank2.BackgroundImageLayout = ImageLayout.Stretch;
            buttonBlank2.Cursor = Cursors.Hand;
            buttonBlank2.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonBlank2.FlatAppearance.BorderSize = 0;
            buttonBlank2.FlatAppearance.MouseDownBackColor = Color.FromArgb(44, 44, 44);
            buttonBlank2.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 44, 44);
            buttonBlank2.FlatStyle = FlatStyle.Flat;
            buttonBlank2.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBlank2.ForeColor = Color.White;
            buttonBlank2.ImageAlign = ContentAlignment.TopCenter;
            buttonBlank2.Location = new Point(540, 32);
            buttonBlank2.Name = "buttonBlank2";
            buttonBlank2.Size = new Size(150, 150);
            buttonBlank2.TabIndex = 9;
            buttonBlank2.TextAlign = ContentAlignment.BottomCenter;
            buttonBlank2.UseVisualStyleBackColor = false;
            // 
            // buttonBolDispute
            // 
            buttonBolDispute.BackColor = Color.Transparent;
            buttonBolDispute.BackgroundImageLayout = ImageLayout.Stretch;
            buttonBolDispute.Cursor = Cursors.Hand;
            buttonBolDispute.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonBolDispute.FlatAppearance.BorderSize = 0;
            buttonBolDispute.FlatAppearance.MouseDownBackColor = Color.FromArgb(44, 44, 44);
            buttonBolDispute.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 44, 44);
            buttonBolDispute.FlatStyle = FlatStyle.Flat;
            buttonBolDispute.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonBolDispute.ForeColor = Color.White;
            buttonBolDispute.Image = (Image)resources.GetObject("buttonBolDispute.Image");
            buttonBolDispute.ImageAlign = ContentAlignment.TopCenter;
            buttonBolDispute.Location = new Point(297, 32);
            buttonBolDispute.Name = "buttonBolDispute";
            buttonBolDispute.Size = new Size(150, 180);
            buttonBolDispute.TabIndex = 8;
            buttonBolDispute.TextAlign = ContentAlignment.BottomCenter;
            buttonBolDispute.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonBolDispute.UseVisualStyleBackColor = false;
            buttonBolDispute.Click += ButtonBolDispute_Click;
            // 
            // buttonCreateBol
            // 
            buttonCreateBol.BackColor = Color.Transparent;
            buttonCreateBol.BackgroundImageLayout = ImageLayout.Zoom;
            buttonCreateBol.Cursor = Cursors.Hand;
            buttonCreateBol.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonCreateBol.FlatAppearance.BorderSize = 0;
            buttonCreateBol.FlatAppearance.MouseDownBackColor = Color.FromArgb(44, 44, 44);
            buttonCreateBol.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 44, 44);
            buttonCreateBol.FlatStyle = FlatStyle.Flat;
            buttonCreateBol.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateBol.ForeColor = Color.White;
            buttonCreateBol.Image = (Image)resources.GetObject("buttonCreateBol.Image");
            buttonCreateBol.ImageAlign = ContentAlignment.TopCenter;
            buttonCreateBol.Location = new Point(54, 32);
            buttonCreateBol.Name = "buttonCreateBol";
            buttonCreateBol.Size = new Size(150, 180);
            buttonCreateBol.TabIndex = 1;
            buttonCreateBol.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonCreateBol.UseVisualStyleBackColor = false;
            buttonCreateBol.Click += ButtonCreateBol_Click;
            // 
            // panelProggrams
            // 
            panelProggrams.BackColor = Color.FromArgb(40, 40, 40);
            panelProggrams.Controls.Add(buttonFreightClassCodes);
            panelProggrams.Controls.Add(buttonNmfcFreightCodes);
            panelProggrams.Controls.Add(buttonCustomers);
            panelProggrams.Controls.Add(buttonVendors);
            panelProggrams.Controls.Add(buttonShippers);
            panelProggrams.Dock = DockStyle.Right;
            panelProggrams.Location = new Point(905, 65);
            panelProggrams.Name = "panelProggrams";
            panelProggrams.Size = new Size(91, 653);
            panelProggrams.TabIndex = 11;
            // 
            // buttonFreightClassCodes
            // 
            buttonFreightClassCodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonFreightClassCodes.BackColor = Color.Transparent;
            buttonFreightClassCodes.BackgroundImage = Properties.Resources.FreightClassCodesIcon150;
            buttonFreightClassCodes.BackgroundImageLayout = ImageLayout.Zoom;
            buttonFreightClassCodes.Cursor = Cursors.Hand;
            buttonFreightClassCodes.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonFreightClassCodes.FlatAppearance.BorderSize = 0;
            buttonFreightClassCodes.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonFreightClassCodes.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonFreightClassCodes.FlatStyle = FlatStyle.Flat;
            buttonFreightClassCodes.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonFreightClassCodes.ForeColor = Color.White;
            buttonFreightClassCodes.ImageAlign = ContentAlignment.TopCenter;
            buttonFreightClassCodes.Location = new Point(8, 384);
            buttonFreightClassCodes.Name = "buttonFreightClassCodes";
            buttonFreightClassCodes.Size = new Size(75, 75);
            buttonFreightClassCodes.TabIndex = 6;
            buttonFreightClassCodes.TextAlign = ContentAlignment.BottomCenter;
            buttonFreightClassCodes.UseVisualStyleBackColor = false;
            buttonFreightClassCodes.Click += ButtonFreightClassCodes_Click;
            // 
            // buttonNmfcFreightCodes
            // 
            buttonNmfcFreightCodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonNmfcFreightCodes.BackColor = Color.Transparent;
            buttonNmfcFreightCodes.BackgroundImage = Properties.Resources.NmfcCodesIcon150;
            buttonNmfcFreightCodes.BackgroundImageLayout = ImageLayout.Zoom;
            buttonNmfcFreightCodes.Cursor = Cursors.Hand;
            buttonNmfcFreightCodes.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonNmfcFreightCodes.FlatAppearance.BorderSize = 0;
            buttonNmfcFreightCodes.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonNmfcFreightCodes.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonNmfcFreightCodes.FlatStyle = FlatStyle.Flat;
            buttonNmfcFreightCodes.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonNmfcFreightCodes.ForeColor = Color.White;
            buttonNmfcFreightCodes.ImageAlign = ContentAlignment.TopCenter;
            buttonNmfcFreightCodes.Location = new Point(8, 289);
            buttonNmfcFreightCodes.Name = "buttonNmfcFreightCodes";
            buttonNmfcFreightCodes.Size = new Size(75, 75);
            buttonNmfcFreightCodes.TabIndex = 5;
            buttonNmfcFreightCodes.TextAlign = ContentAlignment.BottomCenter;
            buttonNmfcFreightCodes.UseVisualStyleBackColor = false;
            buttonNmfcFreightCodes.Click += ButtonNmfcFreightCodes_Click;
            // 
            // buttonCustomers
            // 
            buttonCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCustomers.BackColor = Color.Transparent;
            buttonCustomers.BackgroundImage = (Image)resources.GetObject("buttonCustomers.BackgroundImage");
            buttonCustomers.BackgroundImageLayout = ImageLayout.Zoom;
            buttonCustomers.Cursor = Cursors.Hand;
            buttonCustomers.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonCustomers.FlatAppearance.BorderSize = 0;
            buttonCustomers.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonCustomers.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonCustomers.FlatStyle = FlatStyle.Flat;
            buttonCustomers.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCustomers.ForeColor = Color.White;
            buttonCustomers.ImageAlign = ContentAlignment.TopCenter;
            buttonCustomers.Location = new Point(8, 192);
            buttonCustomers.Name = "buttonCustomers";
            buttonCustomers.Size = new Size(75, 75);
            buttonCustomers.TabIndex = 4;
            buttonCustomers.TextAlign = ContentAlignment.BottomCenter;
            buttonCustomers.TextImageRelation = TextImageRelation.ImageAboveText;
            buttonCustomers.UseVisualStyleBackColor = false;
            buttonCustomers.Click += ButtonCustomers_Click;
            // 
            // buttonVendors
            // 
            buttonVendors.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonVendors.BackColor = Color.Transparent;
            buttonVendors.BackgroundImage = Properties.Resources.vendorsIcon65;
            buttonVendors.BackgroundImageLayout = ImageLayout.Zoom;
            buttonVendors.Cursor = Cursors.Hand;
            buttonVendors.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonVendors.FlatAppearance.BorderSize = 0;
            buttonVendors.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonVendors.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonVendors.FlatStyle = FlatStyle.Flat;
            buttonVendors.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonVendors.ForeColor = Color.White;
            buttonVendors.ImageAlign = ContentAlignment.TopCenter;
            buttonVendors.Location = new Point(8, 100);
            buttonVendors.Name = "buttonVendors";
            buttonVendors.Size = new Size(75, 75);
            buttonVendors.TabIndex = 3;
            buttonVendors.TextAlign = ContentAlignment.BottomCenter;
            buttonVendors.UseVisualStyleBackColor = false;
            buttonVendors.Click += ButtonVendors_Click;
            // 
            // buttonShippers
            // 
            buttonShippers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonShippers.BackColor = Color.Transparent;
            buttonShippers.BackgroundImage = Properties.Resources.btShippers;
            buttonShippers.BackgroundImageLayout = ImageLayout.Zoom;
            buttonShippers.Cursor = Cursors.Hand;
            buttonShippers.FlatAppearance.BorderColor = Color.FromArgb(51, 51, 51);
            buttonShippers.FlatAppearance.BorderSize = 0;
            buttonShippers.FlatAppearance.MouseDownBackColor = Color.Black;
            buttonShippers.FlatAppearance.MouseOverBackColor = Color.Black;
            buttonShippers.FlatStyle = FlatStyle.Flat;
            buttonShippers.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            buttonShippers.ForeColor = Color.White;
            buttonShippers.ImageAlign = ContentAlignment.TopCenter;
            buttonShippers.Location = new Point(8, 19);
            buttonShippers.Name = "buttonShippers";
            buttonShippers.Size = new Size(75, 75);
            buttonShippers.TabIndex = 2;
            buttonShippers.TextAlign = ContentAlignment.BottomCenter;
            buttonShippers.UseVisualStyleBackColor = false;
            buttonShippers.Click += ButtonShippers_Click;
            // 
            // ProgramMenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(996, 718);
            ControlBox = false;
            Controls.Add(panelProggrams);
            Controls.Add(panelMenu);
            Controls.Add(panelSideBar_Left);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgramMenuForm";
            Text = "ProgramMenuForm";
            Load += ProgramMenuFormLoad;
            Shown += ProgramMenuFormShown;
            panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelSideBar_Left.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelProggrams.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Button Btn_Close;
        private Panel panelSideBar_Left;
        private Button buttonPowerDown;
        private Panel panelMenu;
        private Button buttonCreateBol;
        private Button buttonBolDispute;
        private Button buttonBlank2;
        private Panel panelProggrams;
        private Button buttonVendors;
        private Button buttonShippers;
        private Button buttonCustomers;
        private PictureBox pictureBox2;
        private Button buttonFreightClassCodes;
        private Button buttonNmfcFreightCodes;
    }
}