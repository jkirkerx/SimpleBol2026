namespace SimpleBol.WinForms
{
    partial class MongoDbCredentialsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MongoDbCredentialsDialog));
            panel1 = new Panel();
            Lbl_Header = new Label();
            PBLogo = new PictureBox();
            OK_Button = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            Cancel_Button = new Button();
            Btn_Test = new Button();
            groupBoxConnections = new GroupBox();
            buttonDelete = new Button();
            buttonAdd = new Button();
            labelConnections = new Label();
            comboBoxConnections = new ComboBox();
            groupBoxProperties = new GroupBox();
            labelProfileName = new Label();
            textBoxProfileName = new TextBox();
            labelDatabaseName = new Label();
            Txt_DatabaseName = new TextBox();
            labelAuthDatabase = new Label();
            Txt_AuthDatabase = new TextBox();
            ComboBoxAuthMechanism = new ComboBox();
            labelAuthMechanism = new Label();
            Lbl_ConnStr = new Label();
            Lbl_Password = new Label();
            Lbl_UserName = new Label();
            Lbl_Port = new Label();
            Lbl_HostName = new Label();
            Txt_ConnString = new TextBox();
            Txt_Password = new TextBox();
            Txt_UserName = new TextBox();
            Txt_PortNumber = new TextBox();
            Txt_ServerAddress = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PBLogo).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            groupBoxConnections.SuspendLayout();
            groupBoxProperties.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(Lbl_Header);
            panel1.Controls.Add(PBLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(627, 54);
            panel1.TabIndex = 0;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 18F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 12);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(354, 32);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "MongoDB Database Credentials";
            // 
            // PBLogo
            // 
            PBLogo.Image = (Image)resources.GetObject("PBLogo.Image");
            PBLogo.InitialImage = (Image)resources.GetObject("PBLogo.InitialImage");
            PBLogo.Location = new Point(20, 3);
            PBLogo.Name = "PBLogo";
            PBLogo.Size = new Size(67, 50);
            PBLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            PBLogo.TabIndex = 0;
            PBLogo.TabStop = false;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.DialogResult = DialogResult.Ignore;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(3, 3);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 6;
            OK_Button.Text = "Save";
            OK_Button.UseVisualStyleBackColor = false;
            OK_Button.Click += OK_Button_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(OK_Button, 0, 0);
            tableLayoutPanel1.Controls.Add(Cancel_Button, 1, 0);
            tableLayoutPanel1.Location = new Point(368, 630);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(242, 57);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel_Button.BackColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            Cancel_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Segoe UI", 12F);
            Cancel_Button.ForeColor = Color.White;
            Cancel_Button.Location = new Point(124, 3);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 7;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // Btn_Test
            // 
            Btn_Test.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Btn_Test.BackColor = Color.FromArgb(60, 60, 60);
            Btn_Test.DialogResult = DialogResult.Ignore;
            Btn_Test.FlatAppearance.BorderColor = SystemColors.WindowFrame;
            Btn_Test.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            Btn_Test.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            Btn_Test.FlatStyle = FlatStyle.Flat;
            Btn_Test.Font = new Font("Segoe UI", 12F);
            Btn_Test.ForeColor = Color.White;
            Btn_Test.Location = new Point(20, 636);
            Btn_Test.Name = "Btn_Test";
            Btn_Test.Size = new Size(275, 51);
            Btn_Test.TabIndex = 5;
            Btn_Test.Text = "Build and Test";
            Btn_Test.UseVisualStyleBackColor = false;
            Btn_Test.Click += Btn_Test_Click;
            // 
            // groupBoxConnections
            // 
            groupBoxConnections.Controls.Add(buttonDelete);
            groupBoxConnections.Controls.Add(buttonAdd);
            groupBoxConnections.Controls.Add(labelConnections);
            groupBoxConnections.Controls.Add(comboBoxConnections);
            groupBoxConnections.Font = new Font("Segoe UI", 11F);
            groupBoxConnections.ForeColor = Color.White;
            groupBoxConnections.Location = new Point(21, 73);
            groupBoxConnections.Name = "groupBoxConnections";
            groupBoxConnections.Size = new Size(594, 117);
            groupBoxConnections.TabIndex = 23;
            groupBoxConnections.TabStop = false;
            groupBoxConnections.Text = "MongoDB Connections";
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.FromArgb(60, 60, 60);
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonDelete.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Segoe UI", 8F);
            buttonDelete.Location = new Point(499, 28);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(75, 23);
            buttonDelete.TabIndex = 26;
            buttonDelete.Text = "Delete -";
            buttonDelete.UseVisualStyleBackColor = false;
            // 
            // buttonAdd
            // 
            buttonAdd.BackColor = Color.FromArgb(60, 60, 60);
            buttonAdd.FlatAppearance.BorderSize = 0;
            buttonAdd.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonAdd.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonAdd.FlatStyle = FlatStyle.Flat;
            buttonAdd.Font = new Font("Segoe UI", 8F);
            buttonAdd.Location = new Point(402, 28);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 25;
            buttonAdd.Text = "Add +";
            buttonAdd.UseVisualStyleBackColor = false;
            // 
            // labelConnections
            // 
            labelConnections.AutoSize = true;
            labelConnections.Font = new Font("Segoe UI", 11F);
            labelConnections.ForeColor = Color.White;
            labelConnections.Location = new Point(16, 33);
            labelConnections.Name = "labelConnections";
            labelConnections.Size = new Size(90, 20);
            labelConnections.TabIndex = 24;
            labelConnections.Text = "Connections";
            // 
            // comboBoxConnections
            // 
            comboBoxConnections.Font = new Font("Segoe UI", 12F);
            comboBoxConnections.FormattingEnabled = true;
            comboBoxConnections.Location = new Point(16, 69);
            comboBoxConnections.Name = "comboBoxConnections";
            comboBoxConnections.Size = new Size(563, 29);
            comboBoxConnections.TabIndex = 23;
            // 
            // groupBoxProperties
            // 
            groupBoxProperties.Controls.Add(labelProfileName);
            groupBoxProperties.Controls.Add(textBoxProfileName);
            groupBoxProperties.Controls.Add(labelDatabaseName);
            groupBoxProperties.Controls.Add(Txt_DatabaseName);
            groupBoxProperties.Controls.Add(labelAuthDatabase);
            groupBoxProperties.Controls.Add(Txt_AuthDatabase);
            groupBoxProperties.Controls.Add(ComboBoxAuthMechanism);
            groupBoxProperties.Controls.Add(labelAuthMechanism);
            groupBoxProperties.Controls.Add(Lbl_ConnStr);
            groupBoxProperties.Controls.Add(Lbl_Password);
            groupBoxProperties.Controls.Add(Lbl_UserName);
            groupBoxProperties.Controls.Add(Lbl_Port);
            groupBoxProperties.Controls.Add(Lbl_HostName);
            groupBoxProperties.Controls.Add(Txt_ConnString);
            groupBoxProperties.Controls.Add(Txt_Password);
            groupBoxProperties.Controls.Add(Txt_UserName);
            groupBoxProperties.Controls.Add(Txt_PortNumber);
            groupBoxProperties.Controls.Add(Txt_ServerAddress);
            groupBoxProperties.Font = new Font("Segoe UI", 11F);
            groupBoxProperties.ForeColor = Color.White;
            groupBoxProperties.Location = new Point(21, 206);
            groupBoxProperties.Name = "groupBoxProperties";
            groupBoxProperties.Size = new Size(594, 409);
            groupBoxProperties.TabIndex = 24;
            groupBoxProperties.TabStop = false;
            groupBoxProperties.Text = "Connection Properties";
            // 
            // labelProfileName
            // 
            labelProfileName.AutoSize = true;
            labelProfileName.Font = new Font("Segoe UI", 11F);
            labelProfileName.ForeColor = Color.White;
            labelProfileName.Location = new Point(24, 32);
            labelProfileName.Name = "labelProfileName";
            labelProfileName.Size = new Size(96, 20);
            labelProfileName.TabIndex = 36;
            labelProfileName.Text = "Profile Name";
            // 
            // textBoxProfileName
            // 
            textBoxProfileName.Font = new Font("Segoe UI", 12F);
            textBoxProfileName.Location = new Point(20, 59);
            textBoxProfileName.Name = "textBoxProfileName";
            textBoxProfileName.Size = new Size(559, 29);
            textBoxProfileName.TabIndex = 35;
            // 
            // labelDatabaseName
            // 
            labelDatabaseName.AutoSize = true;
            labelDatabaseName.Font = new Font("Segoe UI", 11F);
            labelDatabaseName.ForeColor = Color.White;
            labelDatabaseName.Location = new Point(319, 239);
            labelDatabaseName.Name = "labelDatabaseName";
            labelDatabaseName.Size = new Size(116, 20);
            labelDatabaseName.TabIndex = 34;
            labelDatabaseName.Text = "Database Name";
            // 
            // Txt_DatabaseName
            // 
            Txt_DatabaseName.Font = new Font("Segoe UI", 12F);
            Txt_DatabaseName.Location = new Point(315, 263);
            Txt_DatabaseName.MaxLength = 63;
            Txt_DatabaseName.Name = "Txt_DatabaseName";
            Txt_DatabaseName.Size = new Size(264, 29);
            Txt_DatabaseName.TabIndex = 32;
            // 
            // labelAuthDatabase
            // 
            labelAuthDatabase.AutoSize = true;
            labelAuthDatabase.Font = new Font("Segoe UI", 11F);
            labelAuthDatabase.ForeColor = Color.White;
            labelAuthDatabase.Location = new Point(319, 175);
            labelAuthDatabase.Name = "labelAuthDatabase";
            labelAuthDatabase.Size = new Size(107, 20);
            labelAuthDatabase.TabIndex = 33;
            labelAuthDatabase.Text = "Auth Database";
            // 
            // Txt_AuthDatabase
            // 
            Txt_AuthDatabase.Font = new Font("Segoe UI", 12F);
            Txt_AuthDatabase.Location = new Point(315, 199);
            Txt_AuthDatabase.Name = "Txt_AuthDatabase";
            Txt_AuthDatabase.Size = new Size(264, 29);
            Txt_AuthDatabase.TabIndex = 31;
            // 
            // ComboBoxAuthMechanism
            // 
            ComboBoxAuthMechanism.Font = new Font("Segoe UI", 12F);
            ComboBoxAuthMechanism.FormattingEnabled = true;
            ComboBoxAuthMechanism.Items.AddRange(new object[] {
                SimpleBol.Setup.MongoDbDefaults.ScramSha256,
                SimpleBol.Setup.MongoDbDefaults.ScramSha1,
                SimpleBol.Setup.MongoDbDefaults.NoAuthentication });
            ComboBoxAuthMechanism.Location = new Point(316, 130);
            ComboBoxAuthMechanism.Name = "ComboBoxAuthMechanism";
            ComboBoxAuthMechanism.Size = new Size(263, 29);
            ComboBoxAuthMechanism.TabIndex = 30;
            // 
            // labelAuthMechanism
            // 
            labelAuthMechanism.AutoSize = true;
            labelAuthMechanism.Font = new Font("Segoe UI", 11F);
            labelAuthMechanism.ForeColor = Color.White;
            labelAuthMechanism.Location = new Point(319, 103);
            labelAuthMechanism.Name = "labelAuthMechanism";
            labelAuthMechanism.Size = new Size(119, 20);
            labelAuthMechanism.TabIndex = 29;
            labelAuthMechanism.Text = "Auth Mechanism";
            // 
            // Lbl_ConnStr
            // 
            Lbl_ConnStr.AutoSize = true;
            Lbl_ConnStr.Font = new Font("Segoe UI", 11F);
            Lbl_ConnStr.ForeColor = Color.White;
            Lbl_ConnStr.Location = new Point(24, 306);
            Lbl_ConnStr.Name = "Lbl_ConnStr";
            Lbl_ConnStr.Size = new Size(127, 20);
            Lbl_ConnStr.TabIndex = 28;
            Lbl_ConnStr.Text = "Connection String";
            // 
            // Lbl_Password
            // 
            Lbl_Password.AutoSize = true;
            Lbl_Password.Font = new Font("Segoe UI", 11F);
            Lbl_Password.ForeColor = Color.White;
            Lbl_Password.Location = new Point(24, 239);
            Lbl_Password.Name = "Lbl_Password";
            Lbl_Password.Size = new Size(70, 20);
            Lbl_Password.TabIndex = 27;
            Lbl_Password.Text = "Password";
            // 
            // Lbl_UserName
            // 
            Lbl_UserName.AutoSize = true;
            Lbl_UserName.Font = new Font("Segoe UI", 11F);
            Lbl_UserName.ForeColor = Color.White;
            Lbl_UserName.Location = new Point(24, 175);
            Lbl_UserName.Name = "Lbl_UserName";
            Lbl_UserName.Size = new Size(82, 20);
            Lbl_UserName.TabIndex = 26;
            Lbl_UserName.Text = "User Name";
            // 
            // Lbl_Port
            // 
            Lbl_Port.AutoSize = true;
            Lbl_Port.Font = new Font("Segoe UI", 11F);
            Lbl_Port.ForeColor = Color.White;
            Lbl_Port.Location = new Point(201, 103);
            Lbl_Port.Name = "Lbl_Port";
            Lbl_Port.Size = new Size(93, 20);
            Lbl_Port.TabIndex = 25;
            Lbl_Port.Text = "Port Number";
            // 
            // Lbl_HostName
            // 
            Lbl_HostName.AutoSize = true;
            Lbl_HostName.Font = new Font("Segoe UI", 11F);
            Lbl_HostName.ForeColor = Color.White;
            Lbl_HostName.Location = new Point(24, 103);
            Lbl_HostName.Name = "Lbl_HostName";
            Lbl_HostName.Size = new Size(84, 20);
            Lbl_HostName.TabIndex = 24;
            Lbl_HostName.Text = "Host Name";
            // 
            // Txt_ConnString
            // 
            Txt_ConnString.Font = new Font("Segoe UI", 12F);
            Txt_ConnString.Location = new Point(20, 332);
            Txt_ConnString.MaxLength = 1024;
            Txt_ConnString.Multiline = true;
            Txt_ConnString.Name = "Txt_ConnString";
            Txt_ConnString.ScrollBars = ScrollBars.Vertical;
            Txt_ConnString.Size = new Size(559, 57);
            Txt_ConnString.TabIndex = 23;
            // 
            // Txt_Password
            // 
            Txt_Password.Font = new Font("Segoe UI", 12F);
            Txt_Password.Location = new Point(20, 263);
            Txt_Password.Name = "Txt_Password";
            Txt_Password.Size = new Size(274, 29);
            Txt_Password.TabIndex = 22;
            // 
            // Txt_UserName
            // 
            Txt_UserName.Font = new Font("Segoe UI", 12F);
            Txt_UserName.Location = new Point(20, 199);
            Txt_UserName.Name = "Txt_UserName";
            Txt_UserName.Size = new Size(274, 29);
            Txt_UserName.TabIndex = 21;
            // 
            // Txt_PortNumber
            // 
            Txt_PortNumber.Font = new Font("Segoe UI", 12F);
            Txt_PortNumber.Location = new Point(201, 130);
            Txt_PortNumber.Name = "Txt_PortNumber";
            Txt_PortNumber.Size = new Size(93, 29);
            Txt_PortNumber.TabIndex = 20;
            // 
            // Txt_ServerAddress
            // 
            Txt_ServerAddress.Font = new Font("Segoe UI", 12F);
            Txt_ServerAddress.Location = new Point(20, 130);
            Txt_ServerAddress.Name = "Txt_ServerAddress";
            Txt_ServerAddress.Size = new Size(175, 29);
            Txt_ServerAddress.TabIndex = 19;
            // 
            // MongoDbCredentialsDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            CancelButton = Cancel_Button;
            ClientSize = new Size(627, 701);
            Controls.Add(groupBoxProperties);
            Controls.Add(groupBoxConnections);
            Controls.Add(Btn_Test);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MongoDbCredentialsDialog";
            Text = "Connect to database";
            TopMost = true;
            Deactivate += DbCredentialsDialog_Deactivate;
            Load += DbCredentialsDialog_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PBLogo).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            groupBoxConnections.ResumeLayout(false);
            groupBoxConnections.PerformLayout();
            groupBoxProperties.ResumeLayout(false);
            groupBoxProperties.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PBLogo;
        private Button OK_Button;
        private TableLayoutPanel tableLayoutPanel1;
        private Button Cancel_Button;
        private Button Btn_Test;
        private GroupBox groupBoxConnections;
        private Button buttonDelete;
        private Button buttonAdd;
        private Label labelConnections;
        private ComboBox comboBoxConnections;
        private GroupBox groupBoxProperties;
        private Label labelDatabaseName;
        private TextBox Txt_DatabaseName;
        private Label labelAuthDatabase;
        private TextBox Txt_AuthDatabase;
        private ComboBox ComboBoxAuthMechanism;
        private Label labelAuthMechanism;
        private Label Lbl_ConnStr;
        private Label Lbl_Password;
        private Label Lbl_UserName;
        private Label Lbl_Port;
        private Label Lbl_HostName;
        private TextBox Txt_ConnString;
        private TextBox Txt_Password;
        private TextBox Txt_UserName;
        private TextBox Txt_PortNumber;
        private TextBox Txt_ServerAddress;
        private Label labelProfileName;
        private TextBox textBoxProfileName;
    }
}
