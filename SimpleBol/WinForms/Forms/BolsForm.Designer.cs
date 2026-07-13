namespace SimpleBol.WinForms
{
    partial class BolsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BolsForm));
            Panel_Header = new Panel();
            panelPaginate = new Panel();
            label1 = new Label();
            numericUpDownShow = new NumericUpDown();
            buttonReturn = new Button();
            Btn_Close = new Button();
            LabelHeader = new Label();
            PictureBox_Header = new PictureBox();
            panelLeft = new Panel();
            buttonClearFilters = new Button();
            groupBoxReconcile = new GroupBox();
            labelReconcileFilterResults = new Label();
            comboBoxReconcile = new ComboBox();
            labelReconcile = new Label();
            groupBoxVendorFilter = new GroupBox();
            labelVendorFilterResults = new Label();
            comboBoxVendors = new ComboBox();
            label3 = new Label();
            groupBoxShipperFilter = new GroupBox();
            labelShipperFilterResults = new Label();
            comboBoxShippers = new ComboBox();
            label2 = new Label();
            groupBoxCustomerFilter = new GroupBox();
            labelCustomerFilterResults = new Label();
            comboBoxCustomers = new ComboBox();
            labelCustomerFilter = new Label();
            groupBoxDateFilter = new GroupBox();
            labelDateFilterResults = new Label();
            labelDateFilter = new Label();
            dateTimePickerFilterByShipDate = new DateTimePicker();
            panelRight = new Panel();
            buttonDispute = new Button();
            buttonPrintDialog = new Button();
            buttonEditBol = new Button();
            buttonCreateBol = new Button();
            listViewBols = new ListView();
            buttonEmailDialog = new Button();
            Panel_Header.SuspendLayout();
            panelPaginate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).BeginInit();
            panelLeft.SuspendLayout();
            groupBoxReconcile.SuspendLayout();
            groupBoxVendorFilter.SuspendLayout();
            groupBoxShipperFilter.SuspendLayout();
            groupBoxCustomerFilter.SuspendLayout();
            groupBoxDateFilter.SuspendLayout();
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
            Panel_Header.Size = new Size(1228, 70);
            Panel_Header.TabIndex = 1;
            // 
            // panelPaginate
            // 
            panelPaginate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelPaginate.Controls.Add(label1);
            panelPaginate.Controls.Add(numericUpDownShow);
            panelPaginate.Location = new Point(905, 22);
            panelPaginate.Name = "panelPaginate";
            panelPaginate.Size = new Size(121, 37);
            panelPaginate.TabIndex = 5;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(6, 11);
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
            numericUpDownShow.Location = new Point(57, 11);
            numericUpDownShow.Name = "numericUpDownShow";
            numericUpDownShow.Size = new Size(56, 23);
            numericUpDownShow.TabIndex = 5;
            numericUpDownShow.TextAlign = HorizontalAlignment.Center;
            numericUpDownShow.ValueChanged += NumericUpDownShow_ValueChanged;
            // 
            // buttonReturn
            // 
            buttonReturn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonReturn.BackgroundImageLayout = ImageLayout.Stretch;
            buttonReturn.Cursor = Cursors.Hand;
            buttonReturn.FlatAppearance.BorderSize = 0;
            buttonReturn.FlatStyle = FlatStyle.Flat;
            buttonReturn.Image = (Image)resources.GetObject("buttonReturn.Image");
            buttonReturn.Location = new Point(1149, 14);
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
            Btn_Close.Location = new Point(1996, 12);
            Btn_Close.Name = "Btn_Close";
            Btn_Close.Padding = new Padding(4);
            Btn_Close.Size = new Size(40, 10);
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
            LabelHeader.Size = new Size(275, 51);
            LabelHeader.TabIndex = 1;
            LabelHeader.Text = "Bill of Laddings";
            // 
            // PictureBox_Header
            // 
            PictureBox_Header.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            PictureBox_Header.BackColor = Color.Transparent;
            PictureBox_Header.Image = Properties.Resources.badgeBol150;
            PictureBox_Header.InitialImage = Properties.Resources.badgeBol150;
            PictureBox_Header.Location = new Point(20, 9);
            PictureBox_Header.Name = "PictureBox_Header";
            PictureBox_Header.Padding = new Padding(6);
            PictureBox_Header.Size = new Size(65, 55);
            PictureBox_Header.SizeMode = PictureBoxSizeMode.Zoom;
            PictureBox_Header.TabIndex = 0;
            PictureBox_Header.TabStop = false;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.FromArgb(38, 38, 38);
            panelLeft.Controls.Add(buttonClearFilters);
            panelLeft.Controls.Add(groupBoxReconcile);
            panelLeft.Controls.Add(groupBoxVendorFilter);
            panelLeft.Controls.Add(groupBoxShipperFilter);
            panelLeft.Controls.Add(groupBoxCustomerFilter);
            panelLeft.Controls.Add(groupBoxDateFilter);
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Location = new Point(0, 70);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(280, 754);
            panelLeft.TabIndex = 2;
            // 
            // buttonClearFilters
            // 
            buttonClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonClearFilters.BackColor = Color.FromArgb(60, 60, 60);
            buttonClearFilters.Cursor = Cursors.Hand;
            buttonClearFilters.FlatAppearance.BorderSize = 0;
            buttonClearFilters.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonClearFilters.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonClearFilters.FlatStyle = FlatStyle.Flat;
            buttonClearFilters.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonClearFilters.ForeColor = Color.White;
            buttonClearFilters.Location = new Point(16, 700);
            buttonClearFilters.Name = "buttonClearFilters";
            buttonClearFilters.Size = new Size(245, 42);
            buttonClearFilters.TabIndex = 6;
            buttonClearFilters.Text = "Clear Filters";
            buttonClearFilters.UseVisualStyleBackColor = false;
            buttonClearFilters.Click += ButtonClearFilters_Click;
            // 
            // groupBoxReconcile
            // 
            groupBoxReconcile.Controls.Add(labelReconcileFilterResults);
            groupBoxReconcile.Controls.Add(comboBoxReconcile);
            groupBoxReconcile.Controls.Add(labelReconcile);
            groupBoxReconcile.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxReconcile.ForeColor = Color.White;
            groupBoxReconcile.Location = new Point(19, 7);
            groupBoxReconcile.Name = "groupBoxReconcile";
            groupBoxReconcile.Size = new Size(245, 130);
            groupBoxReconcile.TabIndex = 5;
            groupBoxReconcile.TabStop = false;
            groupBoxReconcile.Text = "Reconcile / Price Audit";
            // 
            // labelReconcileFilterResults
            // 
            labelReconcileFilterResults.AutoSize = true;
            labelReconcileFilterResults.Location = new Point(12, 95);
            labelReconcileFilterResults.Name = "labelReconcileFilterResults";
            labelReconcileFilterResults.Size = new Size(152, 20);
            labelReconcileFilterResults.TabIndex = 7;
            labelReconcileFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxReconcile
            // 
            comboBoxReconcile.FormattingEnabled = true;
            comboBoxReconcile.Location = new Point(12, 54);
            comboBoxReconcile.Name = "comboBoxReconcile";
            comboBoxReconcile.Size = new Size(218, 28);
            comboBoxReconcile.TabIndex = 6;
            comboBoxReconcile.SelectedIndexChanged += ComboBoxReconcile_SelectedIndexChanged;
            // 
            // labelReconcile
            // 
            labelReconcile.AutoSize = true;
            labelReconcile.Location = new Point(12, 27);
            labelReconcile.Name = "labelReconcile";
            labelReconcile.Size = new Size(100, 20);
            labelReconcile.TabIndex = 5;
            labelReconcile.Text = "Select a filter";
            // 
            // groupBoxVendorFilter
            // 
            groupBoxVendorFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxVendorFilter.Controls.Add(labelVendorFilterResults);
            groupBoxVendorFilter.Controls.Add(comboBoxVendors);
            groupBoxVendorFilter.Controls.Add(label3);
            groupBoxVendorFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxVendorFilter.ForeColor = Color.Gainsboro;
            groupBoxVendorFilter.Location = new Point(16, 420);
            groupBoxVendorFilter.Name = "groupBoxVendorFilter";
            groupBoxVendorFilter.Size = new Size(245, 130);
            groupBoxVendorFilter.TabIndex = 4;
            groupBoxVendorFilter.TabStop = false;
            groupBoxVendorFilter.Text = "Vendor/ Ship From Filter";
            // 
            // labelVendorFilterResults
            // 
            labelVendorFilterResults.AutoSize = true;
            labelVendorFilterResults.Location = new Point(18, 96);
            labelVendorFilterResults.Name = "labelVendorFilterResults";
            labelVendorFilterResults.Size = new Size(152, 20);
            labelVendorFilterResults.TabIndex = 4;
            labelVendorFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxVendors
            // 
            comboBoxVendors.FormattingEnabled = true;
            comboBoxVendors.Location = new Point(18, 54);
            comboBoxVendors.Name = "comboBoxVendors";
            comboBoxVendors.Size = new Size(218, 28);
            comboBoxVendors.TabIndex = 3;
            comboBoxVendors.SelectedIndexChanged += ComboBoxVendors_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 27);
            label3.Name = "label3";
            label3.Size = new Size(124, 20);
            label3.TabIndex = 2;
            label3.Text = "Filter by vendor:";
            // 
            // groupBoxShipperFilter
            // 
            groupBoxShipperFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxShipperFilter.Controls.Add(labelShipperFilterResults);
            groupBoxShipperFilter.Controls.Add(comboBoxShippers);
            groupBoxShipperFilter.Controls.Add(label2);
            groupBoxShipperFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxShipperFilter.ForeColor = Color.Gainsboro;
            groupBoxShipperFilter.Location = new Point(19, 282);
            groupBoxShipperFilter.Name = "groupBoxShipperFilter";
            groupBoxShipperFilter.Size = new Size(245, 130);
            groupBoxShipperFilter.TabIndex = 3;
            groupBoxShipperFilter.TabStop = false;
            groupBoxShipperFilter.Text = "Shipper Filter";
            // 
            // labelShipperFilterResults
            // 
            labelShipperFilterResults.AutoSize = true;
            labelShipperFilterResults.Location = new Point(15, 95);
            labelShipperFilterResults.Name = "labelShipperFilterResults";
            labelShipperFilterResults.Size = new Size(152, 20);
            labelShipperFilterResults.TabIndex = 4;
            labelShipperFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxShippers
            // 
            comboBoxShippers.FormattingEnabled = true;
            comboBoxShippers.Location = new Point(15, 54);
            comboBoxShippers.Name = "comboBoxShippers";
            comboBoxShippers.Size = new Size(218, 28);
            comboBoxShippers.TabIndex = 3;
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 27);
            label2.Name = "label2";
            label2.Size = new Size(127, 20);
            label2.TabIndex = 2;
            label2.Text = "Filter by shipper:";
            // 
            // groupBoxCustomerFilter
            // 
            groupBoxCustomerFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxCustomerFilter.Controls.Add(labelCustomerFilterResults);
            groupBoxCustomerFilter.Controls.Add(comboBoxCustomers);
            groupBoxCustomerFilter.Controls.Add(labelCustomerFilter);
            groupBoxCustomerFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxCustomerFilter.ForeColor = Color.Gainsboro;
            groupBoxCustomerFilter.Location = new Point(16, 558);
            groupBoxCustomerFilter.Name = "groupBoxCustomerFilter";
            groupBoxCustomerFilter.Size = new Size(245, 130);
            groupBoxCustomerFilter.TabIndex = 2;
            groupBoxCustomerFilter.TabStop = false;
            groupBoxCustomerFilter.Text = "Customer / Ship To Filter";
            // 
            // labelCustomerFilterResults
            // 
            labelCustomerFilterResults.AutoSize = true;
            labelCustomerFilterResults.Location = new Point(18, 97);
            labelCustomerFilterResults.Name = "labelCustomerFilterResults";
            labelCustomerFilterResults.Size = new Size(152, 20);
            labelCustomerFilterResults.TabIndex = 4;
            labelCustomerFilterResults.Text = "0 Records(s) located";
            // 
            // comboBoxCustomers
            // 
            comboBoxCustomers.FormattingEnabled = true;
            comboBoxCustomers.Location = new Point(15, 54);
            comboBoxCustomers.Name = "comboBoxCustomers";
            comboBoxCustomers.Size = new Size(218, 28);
            comboBoxCustomers.TabIndex = 3;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;
            // 
            // labelCustomerFilter
            // 
            labelCustomerFilter.AutoSize = true;
            labelCustomerFilter.Location = new Point(15, 27);
            labelCustomerFilter.Name = "labelCustomerFilter";
            labelCustomerFilter.Size = new Size(141, 20);
            labelCustomerFilter.TabIndex = 2;
            labelCustomerFilter.Text = "Filter by customer:";
            // 
            // groupBoxDateFilter
            // 
            groupBoxDateFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxDateFilter.Controls.Add(labelDateFilterResults);
            groupBoxDateFilter.Controls.Add(labelDateFilter);
            groupBoxDateFilter.Controls.Add(dateTimePickerFilterByShipDate);
            groupBoxDateFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxDateFilter.ForeColor = Color.Gainsboro;
            groupBoxDateFilter.Location = new Point(17, 144);
            groupBoxDateFilter.Name = "groupBoxDateFilter";
            groupBoxDateFilter.Size = new Size(245, 130);
            groupBoxDateFilter.TabIndex = 1;
            groupBoxDateFilter.TabStop = false;
            groupBoxDateFilter.Text = "Date Filter";
            // 
            // labelDateFilterResults
            // 
            labelDateFilterResults.AutoSize = true;
            labelDateFilterResults.Location = new Point(16, 94);
            labelDateFilterResults.Name = "labelDateFilterResults";
            labelDateFilterResults.Size = new Size(152, 20);
            labelDateFilterResults.TabIndex = 3;
            labelDateFilterResults.Text = "0 Records(s) located";
            // 
            // labelDateFilter
            // 
            labelDateFilter.AutoSize = true;
            labelDateFilter.Location = new Point(17, 30);
            labelDateFilter.Name = "labelDateFilter";
            labelDateFilter.Size = new Size(138, 20);
            labelDateFilter.TabIndex = 2;
            labelDateFilter.Text = "Filter by ship date:";
            // 
            // dateTimePickerFilterByShipDate
            // 
            dateTimePickerFilterByShipDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dateTimePickerFilterByShipDate.Location = new Point(14, 58);
            dateTimePickerFilterByShipDate.Name = "dateTimePickerFilterByShipDate";
            dateTimePickerFilterByShipDate.Size = new Size(220, 23);
            dateTimePickerFilterByShipDate.TabIndex = 1;
            dateTimePickerFilterByShipDate.ValueChanged += DateTimePickerFilterByShipDate_ValueChanged;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.FromArgb(28, 28, 28);
            panelRight.Controls.Add(buttonEmailDialog);
            panelRight.Controls.Add(buttonDispute);
            panelRight.Controls.Add(buttonPrintDialog);
            panelRight.Controls.Add(buttonEditBol);
            panelRight.Controls.Add(buttonCreateBol);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1028, 70);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(200, 754);
            panelRight.TabIndex = 3;
            // 
            // buttonDispute
            // 
            buttonDispute.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonDispute.BackColor = Color.FromArgb(60, 60, 60);
            buttonDispute.Cursor = Cursors.Hand;
            buttonDispute.FlatAppearance.BorderSize = 0;
            buttonDispute.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonDispute.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonDispute.FlatStyle = FlatStyle.Flat;
            buttonDispute.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonDispute.ForeColor = Color.White;
            buttonDispute.Location = new Point(36, 189);
            buttonDispute.Name = "buttonDispute";
            buttonDispute.Size = new Size(130, 51);
            buttonDispute.TabIndex = 5;
            buttonDispute.Text = "Dispute";
            buttonDispute.UseVisualStyleBackColor = false;
            buttonDispute.Click += ButtonDispute_Click;
            // 
            // buttonPrintDialog
            // 
            buttonPrintDialog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonPrintDialog.BackColor = Color.FromArgb(60, 60, 60);
            buttonPrintDialog.Cursor = Cursors.Hand;
            buttonPrintDialog.FlatAppearance.BorderSize = 0;
            buttonPrintDialog.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonPrintDialog.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonPrintDialog.FlatStyle = FlatStyle.Flat;
            buttonPrintDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonPrintDialog.ForeColor = Color.White;
            buttonPrintDialog.Location = new Point(36, 278);
            buttonPrintDialog.Name = "buttonPrintDialog";
            buttonPrintDialog.Size = new Size(130, 51);
            buttonPrintDialog.TabIndex = 4;
            buttonPrintDialog.Text = "Print";
            buttonPrintDialog.UseVisualStyleBackColor = false;
            buttonPrintDialog.Click += ButtonPrintDialog_Click;
            // 
            // buttonEditBol
            // 
            buttonEditBol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEditBol.BackColor = Color.FromArgb(60, 60, 60);
            buttonEditBol.Cursor = Cursors.Hand;
            buttonEditBol.FlatAppearance.BorderSize = 0;
            buttonEditBol.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEditBol.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEditBol.FlatStyle = FlatStyle.Flat;
            buttonEditBol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonEditBol.ForeColor = Color.White;
            buttonEditBol.Location = new Point(36, 106);
            buttonEditBol.Name = "buttonEditBol";
            buttonEditBol.Size = new Size(130, 51);
            buttonEditBol.TabIndex = 3;
            buttonEditBol.Text = "Edit BOL";
            buttonEditBol.UseVisualStyleBackColor = false;
            buttonEditBol.Click += ButtonEditBol_Click;
            // 
            // buttonCreateBol
            // 
            buttonCreateBol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonCreateBol.BackColor = Color.FromArgb(60, 60, 60);
            buttonCreateBol.Cursor = Cursors.Hand;
            buttonCreateBol.FlatAppearance.BorderSize = 0;
            buttonCreateBol.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonCreateBol.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonCreateBol.FlatStyle = FlatStyle.Flat;
            buttonCreateBol.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonCreateBol.ForeColor = Color.White;
            buttonCreateBol.Location = new Point(36, 28);
            buttonCreateBol.Name = "buttonCreateBol";
            buttonCreateBol.Size = new Size(130, 51);
            buttonCreateBol.TabIndex = 2;
            buttonCreateBol.Text = "Create BOL";
            buttonCreateBol.UseVisualStyleBackColor = false;
            buttonCreateBol.Click += ButtonCreateBol_Click;
            // 
            // listViewBols
            // 
            listViewBols.Dock = DockStyle.Fill;
            listViewBols.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            listViewBols.Location = new Point(280, 70);
            listViewBols.Name = "listViewBols";
            listViewBols.Size = new Size(748, 754);
            listViewBols.TabIndex = 4;
            listViewBols.UseCompatibleStateImageBehavior = false;
            listViewBols.ColumnClick += LvBols_ColumnClick;
            listViewBols.SelectedIndexChanged += ListViewBols_SelectedIndexChanged;
            listViewBols.DoubleClick += ListViewBols_DoubleClick;
            // 
            // buttonEmailDialog
            // 
            buttonEmailDialog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonEmailDialog.BackColor = Color.FromArgb(60, 60, 60);
            buttonEmailDialog.Cursor = Cursors.Hand;
            buttonEmailDialog.FlatAppearance.BorderSize = 0;
            buttonEmailDialog.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonEmailDialog.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonEmailDialog.FlatStyle = FlatStyle.Flat;
            buttonEmailDialog.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonEmailDialog.ForeColor = Color.White;
            buttonEmailDialog.Location = new Point(35, 352);
            buttonEmailDialog.Name = "buttonEmailDialog";
            buttonEmailDialog.Size = new Size(130, 51);
            buttonEmailDialog.TabIndex = 6;
            buttonEmailDialog.Text = "Email";
            buttonEmailDialog.UseVisualStyleBackColor = false;
            // 
            // BolsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            ClientSize = new Size(1228, 824);
            ControlBox = false;
            Controls.Add(listViewBols);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(Panel_Header);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BolsForm";
            Text = "CreateBolForm";
            Load += BolsFormLoad;
            Shown += BolsFormShown;
            Panel_Header.ResumeLayout(false);
            Panel_Header.PerformLayout();
            panelPaginate.ResumeLayout(false);
            panelPaginate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownShow).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureBox_Header).EndInit();
            panelLeft.ResumeLayout(false);
            groupBoxReconcile.ResumeLayout(false);
            groupBoxReconcile.PerformLayout();
            groupBoxVendorFilter.ResumeLayout(false);
            groupBoxVendorFilter.PerformLayout();
            groupBoxShipperFilter.ResumeLayout(false);
            groupBoxShipperFilter.PerformLayout();
            groupBoxCustomerFilter.ResumeLayout(false);
            groupBoxCustomerFilter.PerformLayout();
            groupBoxDateFilter.ResumeLayout(false);
            groupBoxDateFilter.PerformLayout();
            panelRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel Panel_Header;
        private Button Btn_Close;
        private Label LabelHeader;
        private Button buttonReturn;
        private PictureBox PictureBox_Header;
        private Panel panelLeft;
        private Panel panelRight;
        private Button buttonEditBol;
        private Button buttonCreateBol;
        private ListView listViewBols;
        private GroupBox groupBoxDateFilter;
        private DateTimePicker dateTimePickerFilterByShipDate;
        private Label labelDateFilter;
        private GroupBox groupBoxCustomerFilter;
        private ComboBox comboBoxCustomers;
        private Label labelCustomerFilter;
        private GroupBox groupBoxShipperFilter;
        private ComboBox comboBoxShippers;
        private Label label2;
        private GroupBox groupBoxVendorFilter;
        private ComboBox comboBoxVendors;
        private Label label3;
        private Panel panelPaginate;
        private Label label1;
        private NumericUpDown numericUpDownShow;
        private Label labelVendorFilterResults;
        private Label labelShipperFilterResults;
        private Label labelCustomerFilterResults;
        private Label labelDateFilterResults;
        private Button buttonPrintDialog;
        private GroupBox groupBoxReconcile;
        private Label labelReconcileFilterResults;
        private ComboBox comboBoxReconcile;
        private Label labelReconcile;
        private Button buttonClearFilters;
        private Button buttonDispute;
        private Button buttonEmailDialog;
    }
}