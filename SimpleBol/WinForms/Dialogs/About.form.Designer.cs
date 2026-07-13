namespace SimpleBol.WinForms
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            lblCompanyName = new Label();
            lblCopyright = new Label();
            lblVersion = new Label();
            lblProductName = new Label();
            pictureBox1 = new PictureBox();
            buttonClose = new Button();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // lblCompanyName
            // 
            lblCompanyName.AutoSize = true;
            lblCompanyName.Font = new Font("Segoe UI", 14F);
            lblCompanyName.ForeColor = Color.White;
            lblCompanyName.Location = new Point(31, 158);
            lblCompanyName.Name = "lblCompanyName";
            lblCompanyName.Size = new Size(122, 25);
            lblCompanyName.TabIndex = 14;
            lblCompanyName.Text = AppInfo.Company;
            lblCompanyName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCopyright
            // 
            lblCopyright.AutoSize = true;
            lblCopyright.Font = new Font("Segoe UI", 12F);
            lblCopyright.ForeColor = Color.White;
            lblCopyright.Location = new Point(31, 219);
            lblCopyright.Name = "lblCopyright";
            lblCopyright.Size = new Size(174, 21);
            lblCopyright.TabIndex = 13;
            lblCopyright.Text = AppInfo.Copyright;
            lblCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Segoe UI", 14F);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(31, 187);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(76, 25);
            lblVersion.TabIndex = 12;
            lblVersion.Text = $"Version {AppInfo.Version}";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 22F);
            lblProductName.ForeColor = Color.White;
            lblProductName.Location = new Point(22, 116);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(261, 41);
            lblProductName.TabIndex = 10;
            lblProductName.Text = "DesignToolsServer";
            lblProductName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(4, 9);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(380, 100);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonClose.BackColor = Color.FromArgb(60, 60, 60);
            buttonClose.Cursor = Cursors.Hand;
            buttonClose.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonClose.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Segoe UI", 12F);
            buttonClose.ForeColor = Color.White;
            buttonClose.Location = new Point(132, 377);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(115, 51);
            buttonClose.TabIndex = 15;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = false;
            buttonClose.Click += buttonClose_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Bottom;
            pictureBox2.BackgroundImageLayout = ImageLayout.None;
            pictureBox2.ErrorImage = null;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(48, 264);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(322, 87);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 16;
            pictureBox2.TabStop = false;
            // 
            // AboutForm
            // 
            AcceptButton = buttonClose;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            CancelButton = buttonClose;
            ClientSize = new Size(382, 450);
            ControlBox = false;
            Controls.Add(pictureBox2);
            Controls.Add(buttonClose);
            Controls.Add(lblCompanyName);
            Controls.Add(lblCopyright);
            Controls.Add(lblVersion);
            Controls.Add(lblProductName);
            Controls.Add(pictureBox1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(120, 150);
            Name = "AboutForm";
            Text = "AboutForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblCompanyName;
        private Label lblCopyright;
        private Label lblVersion;
        private Label lblProductName;
        private PictureBox pictureBox1;
        private Button buttonClose;
        private PictureBox pictureBox2;
    }
}
