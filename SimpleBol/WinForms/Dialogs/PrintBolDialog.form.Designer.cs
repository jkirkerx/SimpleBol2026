namespace SimpleBol.WinForms.Dialogs
{
    partial class PrintBolDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintBolDialog));
            OK_Button = new Button();
            Cancel_Button = new Button();
            panel1 = new Panel();
            pictureBoxUpdateFlag = new PictureBox();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            groupBoxPrintTemplates = new GroupBox();
            panelListViewControls = new Panel();
            labelTemplates = new Label();
            buttonLargeIcons = new Button();
            buttonSmallIcons = new Button();
            listViewPrintTemplates = new ListView();
            printDialog1 = new PrintDialog();
            groupBoxPrintMethod = new GroupBox();
            panelPrintProgress = new Panel();
            labelPrintProgress = new Label();
            progressBarPrint = new ProgressBar();
            buttonPrint = new Button();
            labelDocumentSelection = new Label();
            labelCopies = new Label();
            numericUpDownPrintCopies = new NumericUpDown();
            radioButtonPrintToPDF = new RadioButton();
            radioButtonDirectPrint = new RadioButton();
            checkBoxDuplex = new CheckBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxPrintTemplates.SuspendLayout();
            panelListViewControls.SuspendLayout();
            groupBoxPrintMethod.SuspendLayout();
            panelPrintProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrintCopies).BeginInit();
            SuspendLayout();
            // 
            // OK_Button
            // 
            OK_Button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            OK_Button.BackColor = Color.FromArgb(60, 60, 60);
            OK_Button.Cursor = Cursors.Hand;
            OK_Button.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            OK_Button.FlatAppearance.BorderSize = 0;
            OK_Button.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            OK_Button.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            OK_Button.FlatStyle = FlatStyle.Flat;
            OK_Button.Font = new Font("Segoe UI", 12F);
            OK_Button.ForeColor = Color.White;
            OK_Button.Location = new Point(505, 586);
            OK_Button.Margin = new Padding(3, 4, 3, 4);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(131, 51);
            OK_Button.TabIndex = 9;
            OK_Button.Text = "OK";
            OK_Button.UseVisualStyleBackColor = false;
            OK_Button.Click += OK_Button_Click;
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
            Cancel_Button.Location = new Point(643, 586);
            Cancel_Button.Margin = new Padding(3, 4, 3, 4);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(131, 51);
            Cancel_Button.TabIndex = 10;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            Cancel_Button.Click += Cancel_Button_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(38, 38, 38);
            panel1.Controls.Add(pictureBoxUpdateFlag);
            panel1.Controls.Add(Lbl_Header);
            panel1.Controls.Add(PbLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 7, 3, 7);
            panel1.Name = "panel1";
            panel1.Size = new Size(797, 70);
            panel1.TabIndex = 80;
            // 
            // pictureBoxUpdateFlag
            // 
            pictureBoxUpdateFlag.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxUpdateFlag.ErrorImage = null;
            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.InitialImage = Properties.Resources.updateFlagOff65;
            pictureBoxUpdateFlag.Location = new Point(729, 14);
            pictureBoxUpdateFlag.Name = "pictureBoxUpdateFlag";
            pictureBoxUpdateFlag.Size = new Size(45, 45);
            pictureBoxUpdateFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUpdateFlag.TabIndex = 4;
            pictureBoxUpdateFlag.TabStop = false;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(298, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Print Bill of Ladding";
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
            // groupBoxPrintTemplates
            // 
            groupBoxPrintTemplates.Controls.Add(panelListViewControls);
            groupBoxPrintTemplates.Controls.Add(listViewPrintTemplates);
            groupBoxPrintTemplates.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPrintTemplates.ForeColor = Color.White;
            groupBoxPrintTemplates.Location = new Point(20, 92);
            groupBoxPrintTemplates.Name = "groupBoxPrintTemplates";
            groupBoxPrintTemplates.Size = new Size(407, 463);
            groupBoxPrintTemplates.TabIndex = 1;
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
            buttonLargeIcons.Click += ButtonLargeIcons_Click;
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
            buttonSmallIcons.Click += ButtonSmallIcons_Click;
            // 
            // listViewPrintTemplates
            // 
            listViewPrintTemplates.Location = new Point(20, 83);
            listViewPrintTemplates.MultiSelect = false;
            listViewPrintTemplates.Name = "listViewPrintTemplates";
            listViewPrintTemplates.Size = new Size(368, 358);
            listViewPrintTemplates.TabIndex = 4;
            listViewPrintTemplates.UseCompatibleStateImageBehavior = false;
            listViewPrintTemplates.SelectedIndexChanged += ListViewPrintTemplates_SelectedIndexChanged;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // groupBoxPrintMethod
            // 
            groupBoxPrintMethod.Controls.Add(checkBoxDuplex);
            groupBoxPrintMethod.Controls.Add(panelPrintProgress);
            groupBoxPrintMethod.Controls.Add(buttonPrint);
            groupBoxPrintMethod.Controls.Add(labelDocumentSelection);
            groupBoxPrintMethod.Controls.Add(labelCopies);
            groupBoxPrintMethod.Controls.Add(numericUpDownPrintCopies);
            groupBoxPrintMethod.Controls.Add(radioButtonPrintToPDF);
            groupBoxPrintMethod.Controls.Add(radioButtonDirectPrint);
            groupBoxPrintMethod.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            groupBoxPrintMethod.ForeColor = Color.White;
            groupBoxPrintMethod.Location = new Point(458, 92);
            groupBoxPrintMethod.Name = "groupBoxPrintMethod";
            groupBoxPrintMethod.Size = new Size(320, 463);
            groupBoxPrintMethod.TabIndex = 5;
            groupBoxPrintMethod.TabStop = false;
            groupBoxPrintMethod.Text = "Print Method";
            // 
            // panelPrintProgress
            // 
            panelPrintProgress.Controls.Add(labelPrintProgress);
            panelPrintProgress.Controls.Add(progressBarPrint);
            panelPrintProgress.Location = new Point(6, 357);
            panelPrintProgress.Name = "panelPrintProgress";
            panelPrintProgress.Size = new Size(307, 100);
            panelPrintProgress.TabIndex = 11;
            // 
            // labelPrintProgress
            // 
            labelPrintProgress.AutoSize = true;
            labelPrintProgress.Location = new Point(19, 15);
            labelPrintProgress.Name = "labelPrintProgress";
            labelPrintProgress.Size = new Size(130, 20);
            labelPrintProgress.TabIndex = 1;
            labelPrintProgress.Text = "Printing Progress";
            // 
            // progressBarPrint
            // 
            progressBarPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBarPrint.Location = new Point(19, 52);
            progressBarPrint.Name = "progressBarPrint";
            progressBarPrint.Size = new Size(269, 32);
            progressBarPrint.Style = ProgressBarStyle.Marquee;
            progressBarPrint.TabIndex = 0;
            // 
            // buttonPrint
            // 
            buttonPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonPrint.BackColor = Color.FromArgb(60, 60, 60);
            buttonPrint.Cursor = Cursors.Hand;
            buttonPrint.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonPrint.FlatAppearance.BorderSize = 0;
            buttonPrint.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonPrint.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonPrint.FlatStyle = FlatStyle.Flat;
            buttonPrint.Font = new Font("Segoe UI", 12F);
            buttonPrint.ForeColor = Color.White;
            buttonPrint.Location = new Point(24, 277);
            buttonPrint.Margin = new Padding(3, 4, 3, 4);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(131, 51);
            buttonPrint.TabIndex = 10;
            buttonPrint.Text = "Print";
            buttonPrint.UseVisualStyleBackColor = false;
            buttonPrint.Click += ButtonPrint_Click;
            // 
            // labelDocumentSelection
            // 
            labelDocumentSelection.AutoSize = true;
            labelDocumentSelection.Location = new Point(25, 232);
            labelDocumentSelection.Name = "labelDocumentSelection";
            labelDocumentSelection.Size = new Size(238, 20);
            labelDocumentSelection.TabIndex = 9;
            labelDocumentSelection.Text = "Select a Template first then print";
            // 
            // labelCopies
            // 
            labelCopies.AutoSize = true;
            labelCopies.Location = new Point(25, 136);
            labelCopies.Name = "labelCopies";
            labelCopies.Size = new Size(55, 20);
            labelCopies.TabIndex = 3;
            labelCopies.Text = "Copies";
            // 
            // numericUpDownPrintCopies
            // 
            numericUpDownPrintCopies.Location = new Point(25, 169);
            numericUpDownPrintCopies.Name = "numericUpDownPrintCopies";
            numericUpDownPrintCopies.Size = new Size(120, 27);
            numericUpDownPrintCopies.TabIndex = 8;
            numericUpDownPrintCopies.ValueChanged += NumericUpDownPrintCopies_ValueChanged;
            // 
            // radioButtonPrintToPDF
            // 
            radioButtonPrintToPDF.AutoSize = true;
            radioButtonPrintToPDF.ForeColor = Color.White;
            radioButtonPrintToPDF.Location = new Point(25, 74);
            radioButtonPrintToPDF.Name = "radioButtonPrintToPDF";
            radioButtonPrintToPDF.Size = new Size(198, 24);
            radioButtonPrintToPDF.TabIndex = 7;
            radioButtonPrintToPDF.TabStop = true;
            radioButtonPrintToPDF.Text = "Direct to Adobe Acrobat";
            radioButtonPrintToPDF.UseVisualStyleBackColor = true;
            radioButtonPrintToPDF.CheckedChanged += RadioButtonPrintToPDF_CheckedChanged;
            // 
            // radioButtonDirectPrint
            // 
            radioButtonDirectPrint.AutoSize = true;
            radioButtonDirectPrint.ForeColor = Color.White;
            radioButtonDirectPrint.Location = new Point(25, 40);
            radioButtonDirectPrint.Name = "radioButtonDirectPrint";
            radioButtonDirectPrint.Size = new Size(140, 24);
            radioButtonDirectPrint.TabIndex = 6;
            radioButtonDirectPrint.TabStop = true;
            radioButtonDirectPrint.Text = "Direct to printer";
            radioButtonDirectPrint.UseVisualStyleBackColor = true;
            radioButtonDirectPrint.CheckedChanged += RadioButtonDirectPrint_CheckedChanged;
            // 
            // checkBoxDuplex
            // 
            checkBoxDuplex.AutoSize = true;
            checkBoxDuplex.Location = new Point(193, 169);
            checkBoxDuplex.Name = "checkBoxDuplex";
            checkBoxDuplex.Size = new Size(77, 24);
            checkBoxDuplex.TabIndex = 12;
            checkBoxDuplex.Text = "Duplex";
            checkBoxDuplex.UseVisualStyleBackColor = true;
            // 
            // PrintBolDialog
            // 
            AcceptButton = OK_Button;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 51, 51);
            CancelButton = Cancel_Button;
            ClientSize = new Size(797, 659);
            Controls.Add(groupBoxPrintMethod);
            Controls.Add(groupBoxPrintTemplates);
            Controls.Add(panel1);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PrintBolDialog";
            Text = "DesignToolsServer - Print BOL";
            Load += PrintDialog_Load;
            Shown += PrintDialog_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUpdateFlag).EndInit();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxPrintTemplates.ResumeLayout(false);
            panelListViewControls.ResumeLayout(false);
            panelListViewControls.PerformLayout();
            groupBoxPrintMethod.ResumeLayout(false);
            groupBoxPrintMethod.PerformLayout();
            panelPrintProgress.ResumeLayout(false);
            panelPrintProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownPrintCopies).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button OK_Button;
        private Button Cancel_Button;
        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private GroupBox groupBoxPrintTemplates;
        private PrintDialog printDialog1;
        private GroupBox groupBoxPrintMethod;
        private ListView listViewPrintTemplates;
        private RadioButton radioButtonPrintToPDF;
        private RadioButton radioButtonDirectPrint;
        private Label labelCopies;
        private NumericUpDown numericUpDownPrintCopies;
        private Panel panelListViewControls;
        private Button buttonLargeIcons;
        private Button buttonSmallIcons;
        private Label labelDocumentSelection;
        private PictureBox pictureBoxUpdateFlag;
        private Button buttonPrint;
        private Panel panelPrintProgress;
        private Label labelPrintProgress;
        private ProgressBar progressBarPrint;
        private Label labelTemplates;
        private CheckBox checkBoxDuplex;
    }
}
