namespace SimpleBol.WinForms
{
    partial class MainMdiForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMdiForm));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelConnectivity = new ToolStripStatusLabel();
            timerEvent = new System.Windows.Forms.Timer(components);
            companyConnectionToolStripMenuItem = new ToolStripMenuItem();
            simpleBolToolStripMenuItem = new ToolStripMenuItem();
            bolsToolStripMenuItem = new ToolStripMenuItem();
            shippersToolStripMenuItem = new ToolStripMenuItem();
            vendorsToolStripMenuItem = new ToolStripMenuItem();
            customersToolStripMenuItem = new ToolStripMenuItem();
            nMFCCodesToolStripMenuItem = new ToolStripMenuItem();
            freightClassCodesToolStripMenuItem = new ToolStripMenuItem();
            billingDisputesToolStripMenuItem = new ToolStripMenuItem();
            billTo3rdPartyToolStripMenuItem = new ToolStripMenuItem();
            utilitiesToolStripMenuItem = new ToolStripMenuItem();
            errorLoggingToolStripMenuItem = new ToolStripMenuItem();
            mongoDbCredentialsToolStripMenuItem = new ToolStripMenuItem();
            smtpCredentialsToolStripMenuItem = new ToolStripMenuItem();
            accountsCredentialsToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = SystemColors.Control;
            statusStrip1.ForeColor = Color.White;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelConnectivity });
            statusStrip1.Location = new Point(0, 578);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(914, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelConnectivity
            // 
            toolStripStatusLabelConnectivity.Name = "toolStripStatusLabelConnectivity";
            toolStripStatusLabelConnectivity.Size = new Size(42, 17);
            toolStripStatusLabelConnectivity.Text = "Online";
            // 
            // simpleBolToolStripMenuItem
            // 
            simpleBolToolStripMenuItem.BackColor = SystemColors.Control;
            simpleBolToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            simpleBolToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { bolsToolStripMenuItem, shippersToolStripMenuItem, vendorsToolStripMenuItem, customersToolStripMenuItem, nMFCCodesToolStripMenuItem, freightClassCodesToolStripMenuItem, billingDisputesToolStripMenuItem, billTo3rdPartyToolStripMenuItem });
            simpleBolToolStripMenuItem.ForeColor = SystemColors.ControlText;
            simpleBolToolStripMenuItem.Name = "simpleBolToolStripMenuItem";
            simpleBolToolStripMenuItem.Size = new Size(74, 23);
            simpleBolToolStripMenuItem.Text = "Modules";
            // 
            // companyConnectionToolStripMenuItem
            // 
            companyConnectionToolStripMenuItem.BackColor = SystemColors.Control;
            companyConnectionToolStripMenuItem.ForeColor = SystemColors.ControlText;
            companyConnectionToolStripMenuItem.Name = "companyConnectionToolStripMenuItem";
            companyConnectionToolStripMenuItem.Size = new Size(166, 23);
            companyConnectionToolStripMenuItem.Text = "Connection";
            // 
            // bolsToolStripMenuItem
            // 
            bolsToolStripMenuItem.Image = Properties.Resources.badgeBol150;
            bolsToolStripMenuItem.Name = "bolsToolStripMenuItem";
            bolsToolStripMenuItem.Size = new Size(198, 24);
            bolsToolStripMenuItem.Text = "Bols";
            bolsToolStripMenuItem.Click += BolsToolStripMenuItem_Click;
            // 
            // shippersToolStripMenuItem
            // 
            shippersToolStripMenuItem.Image = (Image)resources.GetObject("shippersToolStripMenuItem.Image");
            shippersToolStripMenuItem.Name = "shippersToolStripMenuItem";
            shippersToolStripMenuItem.Size = new Size(198, 24);
            shippersToolStripMenuItem.Text = "Shippers";
            shippersToolStripMenuItem.Click += ShippersToolStripMenuItem_Click;
            // 
            // vendorsToolStripMenuItem
            // 
            vendorsToolStripMenuItem.Image = Properties.Resources.vendorsIcon65;
            vendorsToolStripMenuItem.Name = "vendorsToolStripMenuItem";
            vendorsToolStripMenuItem.Size = new Size(198, 24);
            vendorsToolStripMenuItem.Text = "Vendors";
            vendorsToolStripMenuItem.Click += VendorsToolStripMenuItem_Click;
            // 
            // customersToolStripMenuItem
            // 
            customersToolStripMenuItem.Image = (Image)resources.GetObject("customersToolStripMenuItem.Image");
            customersToolStripMenuItem.Name = "customersToolStripMenuItem";
            customersToolStripMenuItem.Size = new Size(198, 24);
            customersToolStripMenuItem.Text = "Customers";
            customersToolStripMenuItem.Click += CustomersToolStripMenuItem_Click;
            // 
            // nMFCCodesToolStripMenuItem
            // 
            nMFCCodesToolStripMenuItem.Image = (Image)resources.GetObject("nMFCCodesToolStripMenuItem.Image");
            nMFCCodesToolStripMenuItem.Name = "nMFCCodesToolStripMenuItem";
            nMFCCodesToolStripMenuItem.Size = new Size(198, 24);
            nMFCCodesToolStripMenuItem.Text = "NMFC Codes";
            nMFCCodesToolStripMenuItem.Click += NMFCCodesToolStripMenuItem_Click;
            // 
            // freightClassCodesToolStripMenuItem
            // 
            freightClassCodesToolStripMenuItem.Image = (Image)resources.GetObject("freightClassCodesToolStripMenuItem.Image");
            freightClassCodesToolStripMenuItem.Name = "freightClassCodesToolStripMenuItem";
            freightClassCodesToolStripMenuItem.Size = new Size(198, 24);
            freightClassCodesToolStripMenuItem.Text = "Freight Class Codes";
            freightClassCodesToolStripMenuItem.Click += FreightClassCodesToolStripMenuItem_Click;
            // 
            // billingDisputesToolStripMenuItem
            // 
            billingDisputesToolStripMenuItem.Name = "billingDisputesToolStripMenuItem";
            billingDisputesToolStripMenuItem.Size = new Size(198, 24);
            billingDisputesToolStripMenuItem.Text = "Billing Disputes";
            billingDisputesToolStripMenuItem.Click += BillingDisputesToolStripMenuItem_Click;
            // 
            // billTo3rdPartyToolStripMenuItem
            // 
            billTo3rdPartyToolStripMenuItem.Name = "billTo3rdPartyToolStripMenuItem";
            billTo3rdPartyToolStripMenuItem.Size = new Size(198, 24);
            billTo3rdPartyToolStripMenuItem.Text = "Bill to 3rd Party";
            billTo3rdPartyToolStripMenuItem.Click += BillTo3rdPartyToolStripMenuItem_Click;
            // 
            // utilitiesToolStripMenuItem
            // 
            utilitiesToolStripMenuItem.BackColor = SystemColors.Control;
            utilitiesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { errorLoggingToolStripMenuItem, mongoDbCredentialsToolStripMenuItem, smtpCredentialsToolStripMenuItem, accountsCredentialsToolStripMenuItem });
            utilitiesToolStripMenuItem.ForeColor = SystemColors.ControlText;
            utilitiesToolStripMenuItem.Image = Properties.Resources.utilitiesIcon;
            utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            utilitiesToolStripMenuItem.Size = new Size(82, 23);
            utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // errorLoggingToolStripMenuItem
            // 
            errorLoggingToolStripMenuItem.BackColor = SystemColors.Control;
            errorLoggingToolStripMenuItem.CheckOnClick = true;
            errorLoggingToolStripMenuItem.ForeColor = SystemColors.ControlText;
            errorLoggingToolStripMenuItem.Image = Properties.Resources.ErrorIcon;
            errorLoggingToolStripMenuItem.Name = "errorLoggingToolStripMenuItem";
            errorLoggingToolStripMenuItem.Size = new Size(213, 24);
            errorLoggingToolStripMenuItem.Text = "Error Logging";
            errorLoggingToolStripMenuItem.Click += ErrorLoggingToolStripMenuItem_Click;
            // 
            // mongoDbCredentialsToolStripMenuItem
            // 
            mongoDbCredentialsToolStripMenuItem.BackColor = SystemColors.Control;
            mongoDbCredentialsToolStripMenuItem.ForeColor = SystemColors.ControlText;
            mongoDbCredentialsToolStripMenuItem.Image = Properties.Resources.mongoLogo;
            mongoDbCredentialsToolStripMenuItem.Name = "mongoDbCredentialsToolStripMenuItem";
            mongoDbCredentialsToolStripMenuItem.Size = new Size(213, 24);
            mongoDbCredentialsToolStripMenuItem.Text = "MongoDb Credentials";
            mongoDbCredentialsToolStripMenuItem.Click += MongoDbCredentialsToolStripMenuItem_Click;
            // 
            // smtpCredentialsToolStripMenuItem
            // 
            smtpCredentialsToolStripMenuItem.Image = (Image)resources.GetObject("smtpCredentialsToolStripMenuItem.Image");
            smtpCredentialsToolStripMenuItem.Name = "smtpCredentialsToolStripMenuItem";
            smtpCredentialsToolStripMenuItem.Size = new Size(213, 24);
            smtpCredentialsToolStripMenuItem.Text = "SMTPl API Settings";
            smtpCredentialsToolStripMenuItem.Click += SmtpCredentialsToolStripMenuItem_Click;
            // 
            // accountsCredentialsToolStripMenuItem
            // 
            accountsCredentialsToolStripMenuItem.Image = (Image)resources.GetObject("accountsCredentialsToolStripMenuItem.Image");
            accountsCredentialsToolStripMenuItem.Name = "accountsCredentialsToolStripMenuItem";
            accountsCredentialsToolStripMenuItem.Size = new Size(213, 24);
            accountsCredentialsToolStripMenuItem.Text = "User Accounts";
            accountsCredentialsToolStripMenuItem.Click += AccountsCredentialsToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.BackColor = SystemColors.Control;
            aboutToolStripMenuItem.ForeColor = SystemColors.ControlText;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(59, 23);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = SystemColors.Control;
            exitToolStripMenuItem.ForeColor = SystemColors.ControlText;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(42, 23);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.Font = new Font("Segoe UI", 10F);
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Items.AddRange(new ToolStripItem[] { companyConnectionToolStripMenuItem, simpleBolToolStripMenuItem, utilitiesToolStripMenuItem, aboutToolStripMenuItem, exitToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(914, 27);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // MainMdiForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(914, 600);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 11F);
            ForeColor = SystemColors.ControlLightLight;
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainMdiForm";
            Text = "DesignToolsServer";
            FormClosed += MainMdiFormClosed;
            Load += MainMdiFormLoad;
            Shown += MainMdiFormShown;
            LocationChanged += MainMdiFormLocationChanged;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelConnectivity;
        private System.Windows.Forms.Timer timerEvent;
        private ToolStripMenuItem simpleBolToolStripMenuItem;
        private ToolStripMenuItem companyConnectionToolStripMenuItem;
        private ToolStripMenuItem utilitiesToolStripMenuItem;
        private ToolStripMenuItem errorLoggingToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mongoDbCredentialsToolStripMenuItem;
        private ToolStripMenuItem shippersToolStripMenuItem;
        private ToolStripMenuItem bolsToolStripMenuItem;
        private ToolStripMenuItem smtpCredentialsToolStripMenuItem;
        private ToolStripMenuItem accountsCredentialsToolStripMenuItem;
        private ToolStripMenuItem vendorsToolStripMenuItem;
        private ToolStripMenuItem customersToolStripMenuItem;
        private ToolStripMenuItem nMFCCodesToolStripMenuItem;
        private ToolStripMenuItem freightClassCodesToolStripMenuItem;
        private ToolStripMenuItem billingDisputesToolStripMenuItem;
        private ToolStripMenuItem billTo3rdPartyToolStripMenuItem;
    }
}
