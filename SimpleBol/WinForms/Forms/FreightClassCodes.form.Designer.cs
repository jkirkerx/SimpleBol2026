namespace SimpleBol.WinForms.Forms
{
    partial class FreightClassCodesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FreightClassCodesForm));
            listViewCodes = new ListView();
            panelRight = new Panel();
            buttonDisableCode = new Button();
            buttonEnableCode = new Button();
            buttonEditClassCode = new Button();
            buttonCreateClassCode = new Button();
            panelLeft = new Panel();
            Panel_Header = new Panel();
            panelPaginate = new Panel();
            label1 = new Label();
            numericUpDownShow = new NumericUpDown();
            buttonReturn = new Button();
            Btn_Close = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            panelRight.SuspendLayout();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            SuspendLayout();
            // 
            // listViewCodes
            // 
            listViewCodes.Dock = DockStyle.Fill;
            listViewCodes.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            listViewCodes.Location = new Point(200, 70);
            listViewCodes.Name = "listViewCodes";
            listViewCodes.Size = new Size(825, 692);
            listViewCodes.TabIndex = 16;
            listViewCodes.UseCompatibleStateImageBehavior = false;
            listViewCodes.ColumnClick += ListViewCodes_ColumnClick;
            listViewCodes.SelectedIndexChanged += ListViewCodes_SelectedIndexChanged;
            listViewCodes.DoubleClick += ListViewCodes_DoubleClick;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonDisableCode);
            panelRight.Controls.Add(buttonEnableCode);
            panelRight.Controls.Add(buttonEditClassCode);
            panelRight.Controls.Add(buttonCreateClassCode);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1025, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 692);
            panelRight.TabIndex = 15;
            // 
            // buttonDisableCode
            // 
            buttonDisableCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonDisableCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonDisableCode.Cursor = Cursors.Hand;
            buttonDisableCode.FlatAppearance.BorderSize = 0;
            buttonDisableCode.FlatAppearance.MouseDownBackColor = Color.Gold;
            buttonDisableCode.FlatAppearance.MouseOverBackColor = Color.Gold;
            buttonDisableCode.FlatStyle = FlatStyle.Flat;
            buttonDisableCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDisableCode.ForeColor = Color.White;
            buttonDisableCode.Location = new Point(35, 336);
            buttonDisableCode.Name = "buttonDisableCode";
            buttonDisableCode.Size = new Size(130, 51);
            buttonDisableCode.TabIndex = 5;
            buttonDisableCode.Text = "Disable Code";
            buttonDisableCode.UseVisualStyleBackColor = false;
            buttonDisableCode.Click += ButtonDisableCode_Click;
            // 
            // buttonEnableCode
            // 
            buttonEnableCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEnableCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonEnableCode.Cursor = Cursors.Hand;
            buttonEnableCode.FlatAppearance.BorderSize = 0;
            buttonEnableCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEnableCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEnableCode.FlatStyle = FlatStyle.Flat;
            buttonEnableCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEnableCode.ForeColor = Color.White;
            buttonEnableCode.Location = new Point(35, 265);
            buttonEnableCode.Name = "buttonEnableCode";
            buttonEnableCode.Size = new Size(130, 51);
            buttonEnableCode.TabIndex = 4;
            buttonEnableCode.Text = "Enable Code";
            buttonEnableCode.UseVisualStyleBackColor = false;
            buttonEnableCode.Click += ButtonEnableCode_Click;
            // 
            // buttonEditClassCode
            // 
            buttonEditClassCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditClassCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditClassCode.Cursor = Cursors.Hand;
            buttonEditClassCode.FlatAppearance.BorderSize = 0;
            buttonEditClassCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditClassCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditClassCode.FlatStyle = FlatStyle.Flat;
            buttonEditClassCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditClassCode.ForeColor = Color.White;
            buttonEditClassCode.Location = new Point(35, 109);
            buttonEditClassCode.Name = "buttonEditClassCode";
            buttonEditClassCode.Size = new Size(130, 51);
            buttonEditClassCode.TabIndex = 3;
            buttonEditClassCode.Text = "Edit Code";
            buttonEditClassCode.UseVisualStyleBackColor = false;
            buttonEditClassCode.Click += ButtonEditClassCode_Click;
            // 
            // buttonCreateClassCode
            // 
            buttonCreateClassCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateClassCode.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateClassCode.Cursor = Cursors.Hand;
            buttonCreateClassCode.FlatAppearance.BorderSize = 0;
            buttonCreateClassCode.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateClassCode.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateClassCode.FlatStyle = FlatStyle.Flat;
            buttonCreateClassCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCreateClassCode.ForeColor = Color.White;
            buttonCreateClassCode.Location = new Point(35, 31);
            buttonCreateClassCode.Name = "buttonCreateClassCode";
            buttonCreateClassCode.Size = new Size(130, 51);
            buttonCreateClassCode.TabIndex = 2;
            buttonCreateClassCode.Text = "Create Code";
            buttonCreateClassCode.UseVisualStyleBackColor = false;
            buttonCreateClassCode.Click += ButtonCreateClassCode_Click;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(200, 692);
            panelLeft.TabIndex = 14;
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
            Panel_Header.Size = new Size(1225, 70);
            Panel_Header.TabIndex = 13;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(4085, 22);
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
            label1.Location = new Point(-231, 11);
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
            numericUpDownShow.Location = new Point(-180, 11);
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
            buttonReturn.Location = new Point(3243, 12);
            buttonReturn.Name = "buttonReturn";
            buttonReturn.Padding = new Padding(4);
            buttonReturn.Size = new Size(45, 0);
            buttonReturn.TabIndex = 2;
            buttonReturn.UseVisualStyleBackColor = true;
            // 
            // Btn_Close
            // 
            Btn_Close.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Close.BackgroundImageLayout = ImageLayout.Stretch;
            Btn_Close.Cursor = Cursors.Hand;
            Btn_Close.FlatAppearance.BorderSize = 0;
            Btn_Close.FlatStyle = FlatStyle.Flat;
            Btn_Close.Image = (Image)resources.GetObject("Btn_Close.Image");
            Btn_Close.Location = new Point(1145, 14);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(45, 45);
            Btn_Close.TabIndex = 1;
            Btn_Close.UseVisualStyleBackColor = true;
            Btn_Close.Click += ButtonReturn_Click;
            // 
            // LabelHeader
            // 
            LabelHeader.AutoSize = true;
            LabelHeader.Font = new Font("Segoe UI", 28F, FontStyle.Regular, GraphicsUnit.Point);
            LabelHeader.ForeColor = Color.White;
            LabelHeader.Location = new Point(120, 14);
            LabelHeader.Name = "LabelHeader";
            LabelHeader.Size = new Size(638, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "Freight Class Codes and Descriptions";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.ErrorImage = null;
            PictureBox_Header.Image = Properties.Resources.FreightClassCodesIcon150;
            PictureBox_Header.InitialImage = null;
            PictureBox_Header.Location = new Point(20, 9);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 65);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 0;
            PictureBox_Header.TabStop = false;
            // 
            // FreightClassCodesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1225, 762);
            Controls.Add(listViewCodes);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FreightClassCodesForm";
            Text = "FreightClassCodesForm";
            Load += FreightClassCodesForm_Load;
            Shown += FreightClassCodesForm_Shown;
            panelRight.ResumeLayout(false);
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView listViewCodes;
        private Panel panelRight;
        private Button buttonEditClassCode;
        private Button buttonCreateClassCode;
        private Panel panelLeft;
        private Panel Panel_Header;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Button buttonReturn;
        private Button Btn_Close;
        private Label LabelHeader;
        private PictureBox PictureBox_Header;
        private Button buttonDisableCode;
        private Button buttonEnableCode;
    }
}