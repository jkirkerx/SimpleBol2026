namespace SimpleBol.WinForms.Dialogs
{
    partial class EmailBolDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailBolDialog));
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBox1 = new GroupBox();
            listBox1 = new ListBox();
            groupBoxPrintTemplates = new GroupBox();
            panelListViewControls = new Panel();
            labelTemplates = new Label();
            buttonLargeIcons = new Button();
            buttonSmallIcons = new Button();
            listViewPrintTemplates = new ListView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBox1.SuspendLayout();
            groupBoxPrintTemplates.SuspendLayout();
            panelListViewControls.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(Lbl_Header);
            panel1.Controls.Add(PbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 7, 3, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 70);
            panel1.TabIndex = 45;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(76, 14);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(282, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Email to Customer";
            // 
            // PbLogo
            // 
            PbLogo.ErrorImage = null;
            PbLogo.Image = (Image)resources.GetObject("PbLogo.Image");
            PbLogo.InitialImage = null;
            PbLogo.Location = new Point(20, 14);
            PbLogo.Margin = new Padding(3, 7, 3, 7);
            PbLogo.Name = "PbLogo";
            PbLogo.Size = new Size(50, 50);
            PbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            PbLogo.TabIndex = 0;
            PbLogo.TabStop = false;
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.Cursor = Cursors.Hand;
            OK_Button.DialogResult = DialogResult.OK;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(625, 618);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 46;
            OK_Button.Text = "Send";
            OK_Button.UseVisualStyleBackColor = false;
            // 
            // Cancel_Button
            // 
            Cancel_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Cancel_Button.BackColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.CausesValidation = false;
            Cancel_Button.Cursor = Cursors.Hand;
            Cancel_Button.DialogResult = DialogResult.Cancel;
            Cancel_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            Cancel_Button.FlatAppearance.BorderSize = 0;
            Cancel_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            Cancel_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            Cancel_Button.FlatStyle = FlatStyle.Flat;
            Cancel_Button.Font = new Font("Segoe UI", 12F);
            Cancel_Button.ForeColor = Color.White;
            Cancel_Button.Location = new Point(753, 618);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 47;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.Controls.Add(listBox1);
            groupBox1.Font = new Font("Segoe UI", 11F);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(447, 98);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(421, 499);
            groupBox1.TabIndex = 48;
            groupBox1.TabStop = false;
            groupBox1.Text = "Contacts";
            // 
            // listBox1
            // 
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(23, 39);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(374, 424);
            listBox1.TabIndex = 0;
            // 
            // groupBoxPrintTemplates
            // 
            groupBoxPrintTemplates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxPrintTemplates.Controls.Add(panelListViewControls);
            groupBoxPrintTemplates.Controls.Add(listViewPrintTemplates);
            groupBoxPrintTemplates.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPrintTemplates.ForeColor = Color.White;
            groupBoxPrintTemplates.Location = new Point(20, 98);
            groupBoxPrintTemplates.Name = "groupBoxPrintTemplates";
            groupBoxPrintTemplates.Size = new Size(407, 499);
            groupBoxPrintTemplates.TabIndex = 49;
            groupBoxPrintTemplates.TabStop = false;
            groupBoxPrintTemplates.Text = "Print Templates";
            // 
            // panelListViewControls
            // 
            panelListViewControls.BackColor = Color.FromArgb(38, 38, 38);
            panelListViewControls.Controls.Add(labelTemplates);
            panelListViewControls.Controls.Add(buttonLargeIcons);
            panelListViewControls.Controls.Add(buttonSmallIcons);
            panelListViewControls.Location = new Point(20, 40);
            panelListViewControls.Name = "panelListViewControls";
            panelListViewControls.Size = new Size(368, 40);
            panelListViewControls.TabIndex = 2;
            // 
            // labelTemplates
            // 
            labelTemplates.AutoSize = true;
            labelTemplates.Location = new Point(14, 11);
            labelTemplates.Name = "labelTemplates";
            labelTemplates.Size = new Size(149, 20);
            labelTemplates.TabIndex = 4;
            labelTemplates.Text = "Available Templates";
            // 
            // buttonLargeIcons
            // 
            buttonLargeIcons.FlatAppearance.BorderSize = 0;
            buttonLargeIcons.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonLargeIcons.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonLargeIcons.FlatStyle = FlatStyle.Flat;
            buttonLargeIcons.Image = (Image)resources.GetObject("buttonLargeIcons.Image");
            buttonLargeIcons.Location = new Point(295, 5);
            buttonLargeIcons.Name = "buttonLargeIcons";
            buttonLargeIcons.Size = new Size(32, 32);
            buttonLargeIcons.TabIndex = 2;
            buttonLargeIcons.UseVisualStyleBackColor = true;
            // 
            // buttonSmallIcons
            // 
            buttonSmallIcons.FlatAppearance.BorderSize = 0;
            buttonSmallIcons.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonSmallIcons.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonSmallIcons.FlatStyle = FlatStyle.Flat;
            buttonSmallIcons.Image = (Image)resources.GetObject("buttonSmallIcons.Image");
            buttonSmallIcons.Location = new Point(333, 5);
            buttonSmallIcons.Name = "buttonSmallIcons";
            buttonSmallIcons.Size = new Size(32, 32);
            buttonSmallIcons.TabIndex = 3;
            buttonSmallIcons.UseVisualStyleBackColor = true;
            // 
            // listViewPrintTemplates
            // 
            listViewPrintTemplates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewPrintTemplates.Location = new Point(20, 83);
            listViewPrintTemplates.MultiSelect = false;
            listViewPrintTemplates.Name = "listViewPrintTemplates";
            listViewPrintTemplates.Size = new Size(368, 394);
            listViewPrintTemplates.TabIndex = 4;
            listViewPrintTemplates.UseCompatibleStateImageBehavior = false;
            // 
            // EmailBolDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(886, 685);
            Controls.Add(groupBoxPrintTemplates);
            Controls.Add(groupBox1);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EmailBolDialog";
            Text = "EmailBolDialog";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBox1.ResumeLayout(false);
            groupBoxPrintTemplates.ResumeLayout(false);
            panelListViewControls.ResumeLayout(false);
            panelListViewControls.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private Button OK_Button;
        private Button Cancel_Button;
        private GroupBox groupBox1;
        private ListBox listBox1;
        private GroupBox groupBoxPrintTemplates;
        private Panel panelListViewControls;
        private Label labelTemplates;
        private Button buttonLargeIcons;
        private Button buttonSmallIcons;
        private ListView listViewPrintTemplates;
    }
}