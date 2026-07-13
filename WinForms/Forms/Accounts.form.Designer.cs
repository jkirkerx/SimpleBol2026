namespace SimpleBol.WinForms
{
    partial class AccountsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsForm));
            Panel_Header = new Panel();
            panelPaginate = new Panel();
            label1 = new Label();
            numericUpDownShow = new NumericUpDown();
            buttonReturn = new Button();
            Btn_Close = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            panelRight = new Panel();
            buttonEditAccount = new Button();
            buttonCreateAccount = new Button();
            panelLeft = new Panel();
            listViewAccounts = new ListView();
            buttonDelete = new Button();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            panelRight.SuspendLayout();
            SuspendLayout();
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
            Panel_Header.Size = new Size(1257, 70);
            Panel_Header.TabIndex = 2;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(2019, 22);
            panelPaginate.Name = "panelPaginate";
            panelPaginate.Size = new Size(121, 7);
            panelPaginate.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(-73, 11);
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
            numericUpDownShow.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            numericUpDownShow.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownShow.Location = new Point(-22, 11);
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
            Btn_Close.Location = new Point(3053, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 0);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 28F);
            LabelHeader.ForeColor = Color.White;
            LabelHeader.Location = new Point(120, 14);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(324, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "Users or Accounts";
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
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonDelete);
            panelRight.Controls.Add(buttonEditAccount);
            panelRight.Controls.Add(buttonCreateAccount);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1057, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 770);
            panelRight.TabIndex = 7;
            // 
            // buttonEditAccount
            // 
            buttonEditAccount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditAccount.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditAccount.FlatAppearance.BorderSize = 0;
            buttonEditAccount.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditAccount.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditAccount.FlatStyle = FlatStyle.Flat;
            buttonEditAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonEditAccount.Location = new Point(35, 109);
            buttonEditAccount.Name = "buttonEditAccount";
            buttonEditAccount.Size = new Size(130, 51);
            buttonEditAccount.TabIndex = 3;
            buttonEditAccount.Text = "Edit Account";
            buttonEditAccount.UseVisualStyleBackColor = false;
            buttonEditAccount.Click += ButtonEditAccount_Click;
            // 
            // buttonCreateAccount
            // 
            buttonCreateAccount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateAccount.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateAccount.FlatAppearance.BorderSize = 0;
            buttonCreateAccount.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateAccount.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateAccount.FlatStyle = FlatStyle.Flat;
            buttonCreateAccount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCreateAccount.Location = new Point(35, 31);
            buttonCreateAccount.Name = "buttonCreateAccount";
            buttonCreateAccount.Size = new Size(130, 51);
            buttonCreateAccount.TabIndex = 2;
            buttonCreateAccount.Text = "Create Account";
            buttonCreateAccount.UseVisualStyleBackColor = false;
            buttonCreateAccount.Click += ButtonCreateAccount_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 770);
            panelLeft.TabIndex = 6;
            // 
            // listViewAccounts
            // 
            listViewAccounts.Dock = DockStyle.Fill;
            listViewAccounts.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            listViewAccounts.Location = new Point(280, 70);
            listViewAccounts.Name = "listViewAccounts";
            listViewAccounts.Size = new Size(777, 770);
            listViewAccounts.TabIndex = 8;
            listViewAccounts.UseCompatibleStateImageBehavior = false;
            listViewAccounts.ColumnClick += ListViewAccounts_ColumnClick;
            listViewAccounts.DoubleClick += ListViewAccounts_DoubleClick;
            // 
            // buttonDelete
            // 
            buttonDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonDelete.BackColor = Color.FromArgb(60, 60, 60);
            buttonDelete.FlatAppearance.BorderSize = 0;
            buttonDelete.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonDelete.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonDelete.Location = new Point(35, 696);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(130, 51);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Delete Account";
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // AccountsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 840);
            Controls.Add(listViewAccounts);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AccountsForm";
            Text = "AccountsForm";
            Load += AccountsFormLoad;
            Shown += AccountsFormShown;
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            panelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Panel_Header;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Button buttonReturn;
        private Button Btn_Close;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Panel panelRight;
        private Button buttonEditAccount;
        private Button buttonCreateAccount;
        private Panel panelLeft;
        private ListView listViewAccounts;
        private Button buttonDelete;
    }
}
