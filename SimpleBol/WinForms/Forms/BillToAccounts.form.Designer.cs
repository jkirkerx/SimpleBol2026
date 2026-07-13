namespace SimpleBol.WinForms.Forms
{
    partial class BillToAccountsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillToAccountsForm));
            listViewBillToAccounts = new ListView();
            panelRight = new Panel();
            buttonEditBillToAccount = new Button();
            buttonCreateBillToAccount = new Button();
            panelLeft = new Panel();
            groupBoxCustomers = new GroupBox();
            labeCustomerFilterResults = new Label();
            comboBoxCustomers = new ComboBox();
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
            groupBoxCustomers.SuspendLayout();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            SuspendLayout();
            // 
            // listViewBillToAccounts
            // 
            listViewBillToAccounts.Dock = DockStyle.Fill;
            listViewBillToAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            listViewBillToAccounts.Location = new Point(280, 70);
            listViewBillToAccounts.Name = "listViewBillToAccounts";
            listViewBillToAccounts.Size = new Size(761, 731);
            listViewBillToAccounts.TabIndex = 12;
            listViewBillToAccounts.UseCompatibleStateImageBehavior = false;
            listViewBillToAccounts.ColumnClick += ListViewBillToAccounts_ColumnClick;
            listViewBillToAccounts.SelectedIndexChanged += ListViewBillToAccounts_SelectedIndexChanged;
            listViewBillToAccounts.DoubleClick += ListViewBillToAccounts_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEditBillToAccount);
            panelRight.Controls.Add(buttonCreateBillToAccount);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1041, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 731);
            panelRight.TabIndex = 11;
            // 
            // buttonEditBillToAccount
            // 
            buttonEditBillToAccount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditBillToAccount.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditBillToAccount.FlatAppearance.BorderSize = 0;
            buttonEditBillToAccount.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditBillToAccount.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditBillToAccount.FlatStyle = FlatStyle.Flat;
            buttonEditBillToAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditBillToAccount.Location = new Point(35, 109);
            buttonEditBillToAccount.Name = "buttonEditBillToAccount";
            buttonEditBillToAccount.Size = new Size(130, 51);
            buttonEditBillToAccount.TabIndex = 3;
            buttonEditBillToAccount.Text = "Edit Account";
            buttonEditBillToAccount.UseVisualStyleBackColor = false;
            buttonEditBillToAccount.Click += ButtonEditBillToAccount_Click;
            // 
            // buttonCreateBillToAccount
            // 
            buttonCreateBillToAccount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateBillToAccount.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateBillToAccount.FlatAppearance.BorderSize = 0;
            buttonCreateBillToAccount.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateBillToAccount.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateBillToAccount.FlatStyle = FlatStyle.Flat;
            buttonCreateBillToAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateBillToAccount.Location = new Point(35, 31);
            buttonCreateBillToAccount.Name = "buttonCreateBillToAccount";
            buttonCreateBillToAccount.Size = new Size(130, 51);
            buttonCreateBillToAccount.TabIndex = 2;
            buttonCreateBillToAccount.Text = "Create Account";
            buttonCreateBillToAccount.UseVisualStyleBackColor = false;
            buttonCreateBillToAccount.Click += ButtonCreateBillToAccount_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Controls.Add(groupBoxCustomers);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 731);
            panelLeft.TabIndex = 10;
            // 
            // groupBoxCustomers
            // 
            groupBoxCustomers.Controls.Add(labeCustomerFilterResults);
            groupBoxCustomers.Controls.Add(comboBoxCustomers);
            groupBoxCustomers.Controls.Add(labelShippers);
            groupBoxCustomers.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxCustomers.ForeColor = Color.White;
            groupBoxCustomers.Location = new Point(19, 7);
            groupBoxCustomers.Name = "groupBoxCustomers";
            groupBoxCustomers.Size = new Size(245, 130);
            groupBoxCustomers.TabIndex = 8;
            groupBoxCustomers.TabStop = false;
            groupBoxCustomers.Text = "Customers Filter";
            // 
            // labeCustomerFilterResults
            // 
            labeCustomerFilterResults.AutoSize = true;
            labeCustomerFilterResults.Location = new Point(12, 95);
            labeCustomerFilterResults.Name = "labeCustomerFilterResults";
            labeCustomerFilterResults.Size = new Size(152, 20);
            labeCustomerFilterResults.TabIndex = 7;
            labeCustomerFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxCustomers
            // 
            comboBoxCustomers.FormattingEnabled = true;
            comboBoxCustomers.Location = new Point(12, 54);
            comboBoxCustomers.Name = "comboBoxCustomers";
            comboBoxCustomers.Size = new Size(218, 28);
            comboBoxCustomers.TabIndex = 6;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;
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
            Panel_Header.Size = new Size(1241, 70);
            Panel_Header.TabIndex = 9;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(3060, 22);
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
            buttonReturn.Location = new Point(1177, 12);
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
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(4094, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 0);
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
            LabelHeader.Size = new Size(440, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "Bill to 3rd Party Accounts";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = Properties.Resources.badgeBol150;
            PictureBox_Header.InitialImage = null;
            PictureBox_Header.Location = new Point(20, 9);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 65);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 0;
            PictureBox_Header.TabStop = false;
            // 
            // BillToAccountsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1241, 801);
            Controls.Add(listViewBillToAccounts);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BillToAccountsForm";
            Text = "BillToAccountsForm";
            Load += BillToAccountsFormLoad;
            Shown += BillToAccountsFormShown;
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            groupBoxCustomers.ResumeLayout(false);
            groupBoxCustomers.PerformLayout();
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewBillToAccounts;
        private Panel panelRight;
        private Button buttonEditBillToAccount;
        private Button buttonCreateBillToAccount;
        private Panel panelLeft;
        private Panel Panel_Header;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Button buttonReturn;
        private Button Btn_Close;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private GroupBox groupBoxCustomers;
        private Label labeCustomerFilterResults;
        private ComboBox comboBoxCustomers;
        private Label labelShippers;
    }
}