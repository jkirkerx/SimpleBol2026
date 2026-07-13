namespace SimpleBol.WinForms.Dialogs
{
    partial class LoginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            pictureBox1 = new PictureBox();
            lblProductName = new Label();
            labelUserName = new Label();
            textBox1 = new TextBox();
            textBoxPassword = new TextBox();
            labelPassword = new Label();
            buttonSubmit = new Button();
            linkLabelForgotPassword = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(17, 17);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(350, 90);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Font = new Font("Segoe UI", 22F);
            lblProductName.ForeColor = Color.White;
            lblProductName.Location = new Point(134, 127);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(109, 41);
            lblProductName.TabIndex = 13;
            lblProductName.Text = "Sign In";
            lblProductName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelUserName
            // 
            labelUserName.AutoSize = true;
            labelUserName.Font = new Font("Segoe UI", 12F);
            labelUserName.ForeColor = Color.White;
            labelUserName.Location = new Point(25, 202);
            labelUserName.Name = "labelUserName";
            labelUserName.Size = new Size(88, 21);
            labelUserName.TabIndex = 14;
            labelUserName.Text = "User Name";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F);
            textBox1.Location = new Point(25, 234);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(327, 29);
            textBox1.TabIndex = 1;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Segoe UI", 12F);
            textBoxPassword.Location = new Point(25, 308);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PasswordChar = '●';
            textBoxPassword.Size = new Size(327, 29);
            textBoxPassword.TabIndex = 2;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Segoe UI", 12F);
            labelPassword.ForeColor = Color.White;
            labelPassword.Location = new Point(25, 278);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(76, 21);
            labelPassword.TabIndex = 16;
            labelPassword.Text = "Password";
            // 
            // buttonSubmit
            // 
            buttonSubmit.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonSubmit.BackColor = Color.FromArgb(60, 60, 60);
            buttonSubmit.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            buttonSubmit.FlatAppearance.BorderSize = 0;
            buttonSubmit.FlatAppearance.MouseDownBackColor = Color.RoyalBlue;
            buttonSubmit.FlatAppearance.MouseOverBackColor = Color.RoyalBlue;
            buttonSubmit.FlatStyle = FlatStyle.Flat;
            buttonSubmit.Font = new Font("Segoe UI", 12F);
            buttonSubmit.ForeColor = Color.White;
            buttonSubmit.Location = new Point(237, 385);
            buttonSubmit.Name = "buttonSubmit";
            buttonSubmit.Size = new Size(115, 51);
            buttonSubmit.TabIndex = 3;
            buttonSubmit.Text = "Sign In";
            buttonSubmit.UseVisualStyleBackColor = false;
            buttonSubmit.Click += ButtonSubmit_Click;
            // 
            // linkLabelForgotPassword
            // 
            linkLabelForgotPassword.ActiveLinkColor = Color.FromArgb(255, 224, 192);
            linkLabelForgotPassword.AutoSize = true;
            linkLabelForgotPassword.LinkColor = Color.FromArgb(255, 224, 192);
            linkLabelForgotPassword.Location = new Point(25, 405);
            linkLabelForgotPassword.Name = "linkLabelForgotPassword";
            linkLabelForgotPassword.Size = new Size(95, 15);
            linkLabelForgotPassword.TabIndex = 17;
            linkLabelForgotPassword.TabStop = true;
            linkLabelForgotPassword.Text = "Forgot Password";
            linkLabelForgotPassword.VisitedLinkColor = Color.FromArgb(255, 224, 192);
            // 
            // LoginDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(38, 38, 38);
            ClientSize = new Size(381, 461);
            Controls.Add(linkLabelForgotPassword);
            Controls.Add(buttonSubmit);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Controls.Add(textBox1);
            Controls.Add(labelUserName);
            Controls.Add(lblProductName);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginDialog";
            Text = "DesignToolsServer - Sign In";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblProductName;
        private Label labelUserName;
        private TextBox textBox1;
        private TextBox textBoxPassword;
        private Label labelPassword;
        private Button buttonSubmit;
        private LinkLabel linkLabelForgotPassword;
    }
}
