namespace SimpleBol.WinForms.Dialogs
{
    partial class HoursOfOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HoursOfOperation));
            panel1 = new Panel();
            Lbl_Header = new Label();
            PbLogo = new PictureBox();
            OK_Button = new Button();
            Cancel_Button = new Button();
            groupBoxSunday = new GroupBox();
            checkBoxSundayClosed = new CheckBox();
            labelSundayClose = new Label();
            labelSundayOpen = new Label();
            dateTimePickerSundayClose = new DateTimePicker();
            dateTimePickerSundayOpen = new DateTimePicker();
            groupBoxMonday = new GroupBox();
            checkBoxMondayClosed = new CheckBox();
            labelMondayClose = new Label();
            labelMondayOpen = new Label();
            dateTimePickerMondayClose = new DateTimePicker();
            dateTimePickerMondayOpen = new DateTimePicker();
            groupBoxTuesday = new GroupBox();
            checkBoxTuesdayClosed = new CheckBox();
            labelTuesdayClose = new Label();
            labelTuesdayOpen = new Label();
            dateTimePickerTuesdayClose = new DateTimePicker();
            dateTimePickerTuesdayOpen = new DateTimePicker();
            groupBoxWednesday = new GroupBox();
            checkBoxWednesdayClosed = new CheckBox();
            labelWednesdayClose = new Label();
            labelWednesdayOpen = new Label();
            dateTimePickerWednesdayClose = new DateTimePicker();
            dateTimePickerWednesdayOpen = new DateTimePicker();
            groupBoxThursday = new GroupBox();
            checkBoxThursdayClosed = new CheckBox();
            labelThursdayClose = new Label();
            labelThursdayOpen = new Label();
            dateTimePickerThursdayClose = new DateTimePicker();
            dateTimePickerThursdayOpen = new DateTimePicker();
            groupBoxFriday = new GroupBox();
            checkBoxFridayClosed = new CheckBox();
            labelFridayClose = new Label();
            labelFridayOpen = new Label();
            dateTimePickerFridayClose = new DateTimePicker();
            dateTimePickerFridayOpen = new DateTimePicker();
            groupBoxSaturday = new GroupBox();
            checkBoxSaturdayClosed = new CheckBox();
            labelSaturdayClose = new Label();
            labelSaturdayOpen = new Label();
            dateTimePickerSaturdayClose = new DateTimePicker();
            dateTimePickerSaturdayOpen = new DateTimePicker();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).BeginInit();
            groupBoxSunday.SuspendLayout();
            groupBoxMonday.SuspendLayout();
            groupBoxTuesday.SuspendLayout();
            groupBoxWednesday.SuspendLayout();
            groupBoxThursday.SuspendLayout();
            groupBoxFriday.SuspendLayout();
            groupBoxSaturday.SuspendLayout();
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
            panel1.Size = new Size(564, 70);
            panel1.TabIndex = 45;
            // 
            // Lbl_Header
            // 
            Lbl_Header.AutoSize = true;
            Lbl_Header.Font = new Font("Segoe UI", 24F);
            Lbl_Header.ForeColor = Color.White;
            Lbl_Header.Location = new Point(120, 13);
            Lbl_Header.Name = "Lbl_Header";
            Lbl_Header.Size = new Size(295, 45);
            Lbl_Header.TabIndex = 1;
            Lbl_Header.Text = "Hours of Operation";
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
            OK_Button.Location = new Point(295, 666);
            OK_Button.Margin = new Padding(3, 7, 3, 7);
            OK_Button.Name = "OK_Button";
            OK_Button.Size = new Size(115, 51);
            OK_Button.TabIndex = 46;
            OK_Button.Text = "Save";
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
            Cancel_Button.Location = new Point(423, 666);
            Cancel_Button.Margin = new Padding(3, 7, 3, 7);
            Cancel_Button.Name = "Cancel_Button";
            Cancel_Button.Size = new Size(115, 51);
            Cancel_Button.TabIndex = 47;
            Cancel_Button.Text = "Cancel";
            Cancel_Button.UseVisualStyleBackColor = false;
            // 
            // groupBoxSunday
            // 
            groupBoxSunday.Controls.Add(checkBoxSundayClosed);
            groupBoxSunday.Controls.Add(labelSundayClose);
            groupBoxSunday.Controls.Add(labelSundayOpen);
            groupBoxSunday.Controls.Add(dateTimePickerSundayClose);
            groupBoxSunday.Controls.Add(dateTimePickerSundayOpen);
            groupBoxSunday.Font = new Font("Segoe UI", 11F);
            groupBoxSunday.ForeColor = Color.White;
            groupBoxSunday.Location = new Point(20, 88);
            groupBoxSunday.Name = "groupBoxSunday";
            groupBoxSunday.Size = new Size(518, 68);
            groupBoxSunday.TabIndex = 48;
            groupBoxSunday.TabStop = false;
            groupBoxSunday.Text = "Sunday";
            // 
            // checkBoxSundayClosed
            // 
            checkBoxSundayClosed.AutoSize = true;
            checkBoxSundayClosed.Location = new Point(406, 27);
            checkBoxSundayClosed.Name = "checkBoxSundayClosed";
            checkBoxSundayClosed.Size = new Size(73, 24);
            checkBoxSundayClosed.TabIndex = 30;
            checkBoxSundayClosed.Text = "Closed";
            checkBoxSundayClosed.UseVisualStyleBackColor = true;
            checkBoxSundayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelSundayClose
            // 
            labelSundayClose.AutoSize = true;
            labelSundayClose.Location = new Point(200, 29);
            labelSundayClose.Name = "labelSundayClose";
            labelSundayClose.Size = new Size(45, 20);
            labelSundayClose.TabIndex = 15;
            labelSundayClose.Text = "Close";
            // 
            // labelSundayOpen
            // 
            labelSundayOpen.AutoSize = true;
            labelSundayOpen.Location = new Point(20, 29);
            labelSundayOpen.Name = "labelSundayOpen";
            labelSundayOpen.Size = new Size(45, 20);
            labelSundayOpen.TabIndex = 14;
            labelSundayOpen.Text = "Open";
            // 
            // dateTimePickerSundayClose
            // 
            dateTimePickerSundayClose.CustomFormat = "h:mm tt";
            dateTimePickerSundayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerSundayClose.Location = new Point(250, 25);
            dateTimePickerSundayClose.Name = "dateTimePickerSundayClose";
            dateTimePickerSundayClose.ShowUpDown = true;
            dateTimePickerSundayClose.Size = new Size(115, 27);
            dateTimePickerSundayClose.TabIndex = 1;
            // 
            // dateTimePickerSundayOpen
            // 
            dateTimePickerSundayOpen.CustomFormat = "h:mm tt";
            dateTimePickerSundayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerSundayOpen.Location = new Point(70, 25);
            dateTimePickerSundayOpen.Name = "dateTimePickerSundayOpen";
            dateTimePickerSundayOpen.ShowUpDown = true;
            dateTimePickerSundayOpen.Size = new Size(115, 27);
            dateTimePickerSundayOpen.TabIndex = 0;
            // 
            // groupBoxMonday
            // 
            groupBoxMonday.Controls.Add(checkBoxMondayClosed);
            groupBoxMonday.Controls.Add(labelMondayClose);
            groupBoxMonday.Controls.Add(labelMondayOpen);
            groupBoxMonday.Controls.Add(dateTimePickerMondayClose);
            groupBoxMonday.Controls.Add(dateTimePickerMondayOpen);
            groupBoxMonday.Font = new Font("Segoe UI", 11F);
            groupBoxMonday.ForeColor = Color.White;
            groupBoxMonday.Location = new Point(20, 169);
            groupBoxMonday.Name = "groupBoxMonday";
            groupBoxMonday.Size = new Size(518, 68);
            groupBoxMonday.TabIndex = 49;
            groupBoxMonday.TabStop = false;
            groupBoxMonday.Text = "Monday";
            // 
            // checkBoxMondayClosed
            // 
            checkBoxMondayClosed.AutoSize = true;
            checkBoxMondayClosed.Location = new Point(406, 27);
            checkBoxMondayClosed.Name = "checkBoxMondayClosed";
            checkBoxMondayClosed.Size = new Size(73, 24);
            checkBoxMondayClosed.TabIndex = 30;
            checkBoxMondayClosed.Text = "Closed";
            checkBoxMondayClosed.UseVisualStyleBackColor = true;
            checkBoxMondayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelMondayClose
            // 
            labelMondayClose.AutoSize = true;
            labelMondayClose.Location = new Point(200, 29);
            labelMondayClose.Name = "labelMondayClose";
            labelMondayClose.Size = new Size(45, 20);
            labelMondayClose.TabIndex = 17;
            labelMondayClose.Text = "Close";
            // 
            // labelMondayOpen
            // 
            labelMondayOpen.AutoSize = true;
            labelMondayOpen.Location = new Point(20, 29);
            labelMondayOpen.Name = "labelMondayOpen";
            labelMondayOpen.Size = new Size(45, 20);
            labelMondayOpen.TabIndex = 16;
            labelMondayOpen.Text = "Open";
            // 
            // dateTimePickerMondayClose
            // 
            dateTimePickerMondayClose.CustomFormat = "h:mm tt";
            dateTimePickerMondayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerMondayClose.Location = new Point(250, 25);
            dateTimePickerMondayClose.Name = "dateTimePickerMondayClose";
            dateTimePickerMondayClose.ShowUpDown = true;
            dateTimePickerMondayClose.Size = new Size(115, 27);
            dateTimePickerMondayClose.TabIndex = 3;
            // 
            // dateTimePickerMondayOpen
            // 
            dateTimePickerMondayOpen.CustomFormat = "h:mm tt";
            dateTimePickerMondayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerMondayOpen.Location = new Point(70, 25);
            dateTimePickerMondayOpen.Name = "dateTimePickerMondayOpen";
            dateTimePickerMondayOpen.ShowUpDown = true;
            dateTimePickerMondayOpen.Size = new Size(115, 27);
            dateTimePickerMondayOpen.TabIndex = 2;
            // 
            // groupBoxTuesday
            // 
            groupBoxTuesday.Controls.Add(checkBoxTuesdayClosed);
            groupBoxTuesday.Controls.Add(labelTuesdayClose);
            groupBoxTuesday.Controls.Add(labelTuesdayOpen);
            groupBoxTuesday.Controls.Add(dateTimePickerTuesdayClose);
            groupBoxTuesday.Controls.Add(dateTimePickerTuesdayOpen);
            groupBoxTuesday.Font = new Font("Segoe UI", 11F);
            groupBoxTuesday.ForeColor = Color.White;
            groupBoxTuesday.Location = new Point(20, 250);
            groupBoxTuesday.Name = "groupBoxTuesday";
            groupBoxTuesday.Size = new Size(518, 68);
            groupBoxTuesday.TabIndex = 50;
            groupBoxTuesday.TabStop = false;
            groupBoxTuesday.Text = "Tuesday";
            // 
            // checkBoxTuesdayClosed
            // 
            checkBoxTuesdayClosed.AutoSize = true;
            checkBoxTuesdayClosed.Location = new Point(406, 27);
            checkBoxTuesdayClosed.Name = "checkBoxTuesdayClosed";
            checkBoxTuesdayClosed.Size = new Size(73, 24);
            checkBoxTuesdayClosed.TabIndex = 30;
            checkBoxTuesdayClosed.Text = "Closed";
            checkBoxTuesdayClosed.UseVisualStyleBackColor = true;
            checkBoxTuesdayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelTuesdayClose
            // 
            labelTuesdayClose.AutoSize = true;
            labelTuesdayClose.Location = new Point(200, 29);
            labelTuesdayClose.Name = "labelTuesdayClose";
            labelTuesdayClose.Size = new Size(45, 20);
            labelTuesdayClose.TabIndex = 19;
            labelTuesdayClose.Text = "Close";
            // 
            // labelTuesdayOpen
            // 
            labelTuesdayOpen.AutoSize = true;
            labelTuesdayOpen.Location = new Point(20, 29);
            labelTuesdayOpen.Name = "labelTuesdayOpen";
            labelTuesdayOpen.Size = new Size(45, 20);
            labelTuesdayOpen.TabIndex = 18;
            labelTuesdayOpen.Text = "Open";
            // 
            // dateTimePickerTuesdayClose
            // 
            dateTimePickerTuesdayClose.CustomFormat = "h:mm tt";
            dateTimePickerTuesdayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerTuesdayClose.Location = new Point(250, 25);
            dateTimePickerTuesdayClose.Name = "dateTimePickerTuesdayClose";
            dateTimePickerTuesdayClose.ShowUpDown = true;
            dateTimePickerTuesdayClose.Size = new Size(115, 27);
            dateTimePickerTuesdayClose.TabIndex = 5;
            // 
            // dateTimePickerTuesdayOpen
            // 
            dateTimePickerTuesdayOpen.CustomFormat = "h:mm tt";
            dateTimePickerTuesdayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerTuesdayOpen.Location = new Point(70, 25);
            dateTimePickerTuesdayOpen.Name = "dateTimePickerTuesdayOpen";
            dateTimePickerTuesdayOpen.ShowUpDown = true;
            dateTimePickerTuesdayOpen.Size = new Size(115, 27);
            dateTimePickerTuesdayOpen.TabIndex = 4;
            // 
            // groupBoxWednesday
            // 
            groupBoxWednesday.Controls.Add(checkBoxWednesdayClosed);
            groupBoxWednesday.Controls.Add(labelWednesdayClose);
            groupBoxWednesday.Controls.Add(labelWednesdayOpen);
            groupBoxWednesday.Controls.Add(dateTimePickerWednesdayClose);
            groupBoxWednesday.Controls.Add(dateTimePickerWednesdayOpen);
            groupBoxWednesday.Font = new Font("Segoe UI", 11F);
            groupBoxWednesday.ForeColor = Color.White;
            groupBoxWednesday.Location = new Point(20, 331);
            groupBoxWednesday.Name = "groupBoxWednesday";
            groupBoxWednesday.Size = new Size(518, 68);
            groupBoxWednesday.TabIndex = 51;
            groupBoxWednesday.TabStop = false;
            groupBoxWednesday.Text = "Wednesday";
            // 
            // checkBoxWednesdayClosed
            // 
            checkBoxWednesdayClosed.AutoSize = true;
            checkBoxWednesdayClosed.Location = new Point(406, 27);
            checkBoxWednesdayClosed.Name = "checkBoxWednesdayClosed";
            checkBoxWednesdayClosed.Size = new Size(73, 24);
            checkBoxWednesdayClosed.TabIndex = 30;
            checkBoxWednesdayClosed.Text = "Closed";
            checkBoxWednesdayClosed.UseVisualStyleBackColor = true;
            checkBoxWednesdayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelWednesdayClose
            // 
            labelWednesdayClose.AutoSize = true;
            labelWednesdayClose.Location = new Point(200, 29);
            labelWednesdayClose.Name = "labelWednesdayClose";
            labelWednesdayClose.Size = new Size(45, 20);
            labelWednesdayClose.TabIndex = 21;
            labelWednesdayClose.Text = "Close";
            // 
            // labelWednesdayOpen
            // 
            labelWednesdayOpen.AutoSize = true;
            labelWednesdayOpen.Location = new Point(20, 29);
            labelWednesdayOpen.Name = "labelWednesdayOpen";
            labelWednesdayOpen.Size = new Size(45, 20);
            labelWednesdayOpen.TabIndex = 20;
            labelWednesdayOpen.Text = "Open";
            // 
            // dateTimePickerWednesdayClose
            // 
            dateTimePickerWednesdayClose.CustomFormat = "h:mm tt";
            dateTimePickerWednesdayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerWednesdayClose.Location = new Point(250, 25);
            dateTimePickerWednesdayClose.Name = "dateTimePickerWednesdayClose";
            dateTimePickerWednesdayClose.ShowUpDown = true;
            dateTimePickerWednesdayClose.Size = new Size(115, 27);
            dateTimePickerWednesdayClose.TabIndex = 7;
            // 
            // dateTimePickerWednesdayOpen
            // 
            dateTimePickerWednesdayOpen.CustomFormat = "h:mm tt";
            dateTimePickerWednesdayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerWednesdayOpen.Location = new Point(70, 25);
            dateTimePickerWednesdayOpen.Name = "dateTimePickerWednesdayOpen";
            dateTimePickerWednesdayOpen.ShowUpDown = true;
            dateTimePickerWednesdayOpen.Size = new Size(115, 27);
            dateTimePickerWednesdayOpen.TabIndex = 6;
            // 
            // groupBoxThursday
            // 
            groupBoxThursday.Controls.Add(checkBoxThursdayClosed);
            groupBoxThursday.Controls.Add(labelThursdayClose);
            groupBoxThursday.Controls.Add(labelThursdayOpen);
            groupBoxThursday.Controls.Add(dateTimePickerThursdayClose);
            groupBoxThursday.Controls.Add(dateTimePickerThursdayOpen);
            groupBoxThursday.Font = new Font("Segoe UI", 11F);
            groupBoxThursday.ForeColor = Color.White;
            groupBoxThursday.Location = new Point(20, 412);
            groupBoxThursday.Name = "groupBoxThursday";
            groupBoxThursday.Size = new Size(518, 68);
            groupBoxThursday.TabIndex = 52;
            groupBoxThursday.TabStop = false;
            groupBoxThursday.Text = "Thursday";
            // 
            // checkBoxThursdayClosed
            // 
            checkBoxThursdayClosed.AutoSize = true;
            checkBoxThursdayClosed.Location = new Point(406, 27);
            checkBoxThursdayClosed.Name = "checkBoxThursdayClosed";
            checkBoxThursdayClosed.Size = new Size(73, 24);
            checkBoxThursdayClosed.TabIndex = 30;
            checkBoxThursdayClosed.Text = "Closed";
            checkBoxThursdayClosed.UseVisualStyleBackColor = true;
            checkBoxThursdayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelThursdayClose
            // 
            labelThursdayClose.AutoSize = true;
            labelThursdayClose.Location = new Point(200, 29);
            labelThursdayClose.Name = "labelThursdayClose";
            labelThursdayClose.Size = new Size(45, 20);
            labelThursdayClose.TabIndex = 23;
            labelThursdayClose.Text = "Close";
            // 
            // labelThursdayOpen
            // 
            labelThursdayOpen.AutoSize = true;
            labelThursdayOpen.Location = new Point(20, 29);
            labelThursdayOpen.Name = "labelThursdayOpen";
            labelThursdayOpen.Size = new Size(45, 20);
            labelThursdayOpen.TabIndex = 22;
            labelThursdayOpen.Text = "Open";
            // 
            // dateTimePickerThursdayClose
            // 
            dateTimePickerThursdayClose.CustomFormat = "h:mm tt";
            dateTimePickerThursdayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerThursdayClose.Location = new Point(250, 25);
            dateTimePickerThursdayClose.Name = "dateTimePickerThursdayClose";
            dateTimePickerThursdayClose.ShowUpDown = true;
            dateTimePickerThursdayClose.Size = new Size(115, 27);
            dateTimePickerThursdayClose.TabIndex = 9;
            // 
            // dateTimePickerThursdayOpen
            // 
            dateTimePickerThursdayOpen.CustomFormat = "h:mm tt";
            dateTimePickerThursdayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerThursdayOpen.Location = new Point(70, 25);
            dateTimePickerThursdayOpen.Name = "dateTimePickerThursdayOpen";
            dateTimePickerThursdayOpen.ShowUpDown = true;
            dateTimePickerThursdayOpen.Size = new Size(115, 27);
            dateTimePickerThursdayOpen.TabIndex = 8;
            // 
            // groupBoxFriday
            // 
            groupBoxFriday.Controls.Add(checkBoxFridayClosed);
            groupBoxFriday.Controls.Add(labelFridayClose);
            groupBoxFriday.Controls.Add(labelFridayOpen);
            groupBoxFriday.Controls.Add(dateTimePickerFridayClose);
            groupBoxFriday.Controls.Add(dateTimePickerFridayOpen);
            groupBoxFriday.Font = new Font("Segoe UI", 11F);
            groupBoxFriday.ForeColor = Color.White;
            groupBoxFriday.Location = new Point(20, 493);
            groupBoxFriday.Name = "groupBoxFriday";
            groupBoxFriday.Size = new Size(518, 68);
            groupBoxFriday.TabIndex = 53;
            groupBoxFriday.TabStop = false;
            groupBoxFriday.Text = "Friday";
            // 
            // checkBoxFridayClosed
            // 
            checkBoxFridayClosed.AutoSize = true;
            checkBoxFridayClosed.Location = new Point(406, 27);
            checkBoxFridayClosed.Name = "checkBoxFridayClosed";
            checkBoxFridayClosed.Size = new Size(73, 24);
            checkBoxFridayClosed.TabIndex = 30;
            checkBoxFridayClosed.Text = "Closed";
            checkBoxFridayClosed.UseVisualStyleBackColor = true;
            checkBoxFridayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelFridayClose
            // 
            labelFridayClose.AutoSize = true;
            labelFridayClose.Location = new Point(200, 29);
            labelFridayClose.Name = "labelFridayClose";
            labelFridayClose.Size = new Size(45, 20);
            labelFridayClose.TabIndex = 25;
            labelFridayClose.Text = "Close";
            // 
            // labelFridayOpen
            // 
            labelFridayOpen.AutoSize = true;
            labelFridayOpen.Location = new Point(20, 29);
            labelFridayOpen.Name = "labelFridayOpen";
            labelFridayOpen.Size = new Size(45, 20);
            labelFridayOpen.TabIndex = 24;
            labelFridayOpen.Text = "Open";
            // 
            // dateTimePickerFridayClose
            // 
            dateTimePickerFridayClose.CustomFormat = "h:mm tt";
            dateTimePickerFridayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerFridayClose.Location = new Point(250, 25);
            dateTimePickerFridayClose.Name = "dateTimePickerFridayClose";
            dateTimePickerFridayClose.ShowUpDown = true;
            dateTimePickerFridayClose.Size = new Size(115, 27);
            dateTimePickerFridayClose.TabIndex = 11;
            // 
            // dateTimePickerFridayOpen
            // 
            dateTimePickerFridayOpen.CustomFormat = "h:mm tt";
            dateTimePickerFridayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerFridayOpen.Location = new Point(70, 25);
            dateTimePickerFridayOpen.Name = "dateTimePickerFridayOpen";
            dateTimePickerFridayOpen.ShowUpDown = true;
            dateTimePickerFridayOpen.Size = new Size(115, 27);
            dateTimePickerFridayOpen.TabIndex = 10;
            // 
            // groupBoxSaturday
            // 
            groupBoxSaturday.Controls.Add(checkBoxSaturdayClosed);
            groupBoxSaturday.Controls.Add(labelSaturdayClose);
            groupBoxSaturday.Controls.Add(labelSaturdayOpen);
            groupBoxSaturday.Controls.Add(dateTimePickerSaturdayClose);
            groupBoxSaturday.Controls.Add(dateTimePickerSaturdayOpen);
            groupBoxSaturday.Font = new Font("Segoe UI", 11F);
            groupBoxSaturday.ForeColor = Color.White;
            groupBoxSaturday.Location = new Point(20, 574);
            groupBoxSaturday.Name = "groupBoxSaturday";
            groupBoxSaturday.Size = new Size(518, 68);
            groupBoxSaturday.TabIndex = 54;
            groupBoxSaturday.TabStop = false;
            groupBoxSaturday.Text = "Saturday";
            // 
            // checkBoxSaturdayClosed
            // 
            checkBoxSaturdayClosed.AutoSize = true;
            checkBoxSaturdayClosed.Location = new Point(406, 27);
            checkBoxSaturdayClosed.Name = "checkBoxSaturdayClosed";
            checkBoxSaturdayClosed.Size = new Size(73, 24);
            checkBoxSaturdayClosed.TabIndex = 30;
            checkBoxSaturdayClosed.Text = "Closed";
            checkBoxSaturdayClosed.UseVisualStyleBackColor = true;
            checkBoxSaturdayClosed.CheckedChanged += Closed_CheckedChanged;
            // 
            // labelSaturdayClose
            // 
            labelSaturdayClose.AutoSize = true;
            labelSaturdayClose.Location = new Point(200, 29);
            labelSaturdayClose.Name = "labelSaturdayClose";
            labelSaturdayClose.Size = new Size(45, 20);
            labelSaturdayClose.TabIndex = 27;
            labelSaturdayClose.Text = "Close";
            // 
            // labelSaturdayOpen
            // 
            labelSaturdayOpen.AutoSize = true;
            labelSaturdayOpen.Location = new Point(20, 29);
            labelSaturdayOpen.Name = "labelSaturdayOpen";
            labelSaturdayOpen.Size = new Size(45, 20);
            labelSaturdayOpen.TabIndex = 26;
            labelSaturdayOpen.Text = "Open";
            // 
            // dateTimePickerSaturdayClose
            // 
            dateTimePickerSaturdayClose.CustomFormat = "h:mm tt";
            dateTimePickerSaturdayClose.Format = DateTimePickerFormat.Custom;
            dateTimePickerSaturdayClose.Location = new Point(250, 25);
            dateTimePickerSaturdayClose.Name = "dateTimePickerSaturdayClose";
            dateTimePickerSaturdayClose.ShowUpDown = true;
            dateTimePickerSaturdayClose.Size = new Size(115, 27);
            dateTimePickerSaturdayClose.TabIndex = 13;
            // 
            // dateTimePickerSaturdayOpen
            // 
            dateTimePickerSaturdayOpen.CustomFormat = "h:mm tt";
            dateTimePickerSaturdayOpen.Format = DateTimePickerFormat.Custom;
            dateTimePickerSaturdayOpen.Location = new Point(70, 25);
            dateTimePickerSaturdayOpen.Name = "dateTimePickerSaturdayOpen";
            dateTimePickerSaturdayOpen.ShowUpDown = true;
            dateTimePickerSaturdayOpen.Size = new Size(115, 27);
            dateTimePickerSaturdayOpen.TabIndex = 12;
            // 
            // HoursOfOperation
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(564, 733);
            Controls.Add(groupBoxSunday);
            Controls.Add(groupBoxMonday);
            Controls.Add(groupBoxTuesday);
            Controls.Add(groupBoxWednesday);
            Controls.Add(groupBoxThursday);
            Controls.Add(groupBoxFriday);
            Controls.Add(groupBoxSaturday);
            Controls.Add(OK_Button);
            Controls.Add(Cancel_Button);
            Controls.Add(panel1);
            Name = "HoursOfOperation";
            StartPosition = FormStartPosition.Manual;
            Text = "HoursOfOperation";
            Load += HoursOfOperation_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)PbLogo).EndInit();
            groupBoxSunday.ResumeLayout(false);
            groupBoxSunday.PerformLayout();
            groupBoxMonday.ResumeLayout(false);
            groupBoxMonday.PerformLayout();
            groupBoxTuesday.ResumeLayout(false);
            groupBoxTuesday.PerformLayout();
            groupBoxWednesday.ResumeLayout(false);
            groupBoxWednesday.PerformLayout();
            groupBoxThursday.ResumeLayout(false);
            groupBoxThursday.PerformLayout();
            groupBoxFriday.ResumeLayout(false);
            groupBoxFriday.PerformLayout();
            groupBoxSaturday.ResumeLayout(false);
            groupBoxSaturday.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label Lbl_Header;
        private PictureBox PbLogo;
        private Button OK_Button;
        private Button Cancel_Button;
        private GroupBox groupBoxSunday;
        private GroupBox groupBoxMonday;
        private GroupBox groupBoxTuesday;
        private GroupBox groupBoxWednesday;
        private GroupBox groupBoxThursday;
        private GroupBox groupBoxFriday;
        private GroupBox groupBoxSaturday;
        private DateTimePicker dateTimePickerSundayOpen;
        private DateTimePicker dateTimePickerSundayClose;
        private DateTimePicker dateTimePickerMondayOpen;
        private DateTimePicker dateTimePickerMondayClose;
        private DateTimePicker dateTimePickerTuesdayOpen;
        private DateTimePicker dateTimePickerTuesdayClose;
        private DateTimePicker dateTimePickerWednesdayOpen;
        private DateTimePicker dateTimePickerWednesdayClose;
        private DateTimePicker dateTimePickerThursdayOpen;
        private DateTimePicker dateTimePickerThursdayClose;
        private DateTimePicker dateTimePickerFridayOpen;
        private DateTimePicker dateTimePickerFridayClose;
        private DateTimePicker dateTimePickerSaturdayOpen;
        private DateTimePicker dateTimePickerSaturdayClose;
        private Label labelSundayOpen;
        private Label labelSundayClose;
        private Label labelMondayOpen;
        private Label labelMondayClose;
        private Label labelTuesdayOpen;
        private Label labelTuesdayClose;
        private Label labelWednesdayOpen;
        private Label labelWednesdayClose;
        private Label labelThursdayOpen;
        private Label labelThursdayClose;
        private Label labelFridayOpen;
        private Label labelFridayClose;
        private Label labelSaturdayOpen;
        private Label labelSaturdayClose;
        private CheckBox checkBoxSundayClosed;
        private CheckBox checkBoxMondayClosed;
        private CheckBox checkBoxTuesdayClosed;
        private CheckBox checkBoxWednesdayClosed;
        private CheckBox checkBoxThursdayClosed;
        private CheckBox checkBoxFridayClosed;
        private CheckBox checkBoxSaturdayClosed;
    }
}
