namespace SimpleBol.WinForms
{
    partial class SplashScreenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            panel1 = new Panel();
            applicationTitle = new Label();
            applicationVersion = new Label();
            copyright = new Label();
            lblStatus = new Label();
            PanelDownload = new Panel();
            lblDownloadBytes = new Label();
            lblDownloadUrl = new Label();
            pbDownload = new ProgressBar();
            PanelDownload.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Center;
            panel1.Location = new Point(0, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(388, 85);
            panel1.TabIndex = 0;
            // 
            // applicationTitle
            // 
            applicationTitle.AutoSize = true;
            applicationTitle.Font = new Font("Segoe UI", 22F, FontStyle.Regular, GraphicsUnit.Point);
            applicationTitle.Location = new Point(12, 107);
            applicationTitle.Name = "applicationTitle";
            applicationTitle.Size = new Size(232, 41);
            applicationTitle.TabIndex = 1;
            applicationTitle.Text = "Application Title";
            // 
            // applicationVersion
            // 
            applicationVersion.AutoSize = true;
            applicationVersion.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            applicationVersion.Location = new Point(12, 156);
            applicationVersion.Name = "applicationVersion";
            applicationVersion.Size = new Size(189, 32);
            applicationVersion.TabIndex = 2;
            applicationVersion.Text = "Version {0}.{1.00}";
            // 
            // copyright
            // 
            copyright.AutoSize = true;
            copyright.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            copyright.Location = new Point(12, 398);
            copyright.Name = "copyright";
            copyright.Size = new Size(193, 21);
            copyright.TabIndex = 3;
            copyright.Text = AppInfo.Copyright;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(12, 211);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(89, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status Label";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelDownload
            // 
            PanelDownload.Controls.Add(lblDownloadBytes);
            PanelDownload.Controls.Add(lblDownloadUrl);
            PanelDownload.Controls.Add(pbDownload);
            PanelDownload.Location = new Point(12, 250);
            PanelDownload.Name = "PanelDownload";
            PanelDownload.Size = new Size(357, 100);
            PanelDownload.TabIndex = 7;
            PanelDownload.Visible = false;
            // 
            // lblDownloadBytes
            // 
            lblDownloadBytes.AutoSize = true;
            lblDownloadBytes.ForeColor = Color.White;
            lblDownloadBytes.Location = new Point(25, 72);
            lblDownloadBytes.Name = "lblDownloadBytes";
            lblDownloadBytes.Size = new Size(78, 15);
            lblDownloadBytes.TabIndex = 2;
            lblDownloadBytes.Text = "Downloading";
            // 
            // lblDownloadUrl
            // 
            lblDownloadUrl.AutoSize = true;
            lblDownloadUrl.ForeColor = Color.White;
            lblDownloadUrl.Location = new Point(25, 18);
            lblDownloadUrl.Name = "lblDownloadUrl";
            lblDownloadUrl.Size = new Size(110, 15);
            lblDownloadUrl.TabIndex = 1;
            lblDownloadUrl.Text = "http://domain.com";
            // 
            // pbDownload
            // 
            pbDownload.Location = new Point(25, 39);
            pbDownload.Name = "pbDownload";
            pbDownload.Size = new Size(318, 23);
            pbDownload.TabIndex = 0;
            // 
            // SplashScreenForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(388, 443);
            ControlBox = false;
            Controls.Add(PanelDownload);
            Controls.Add(lblStatus);
            Controls.Add(copyright);
            Controls.Add(applicationVersion);
            Controls.Add(applicationTitle);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SplashScreenForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreenForm";
            TopMost = true;
            Load += FormSplashScreenLoad;
            Shown += FormSplashScreenShown;
            PanelDownload.ResumeLayout(false);
            PanelDownload.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label applicationTitle;
        private Label applicationVersion;
        private Label copyright;
        private Label lblStatus;
        private Panel PanelDownload;
        private Label lblDownloadBytes;
        private Label lblDownloadUrl;
        private ProgressBar pbDownload;
    }
}
