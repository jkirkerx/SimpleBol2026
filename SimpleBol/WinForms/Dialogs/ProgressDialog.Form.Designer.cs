namespace SimpleBol.WinForms.Dialogs
{
    partial class ProgressDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressDialog));
            panelBillboard = new Panel();
            progressBar = new ProgressBar();
            labelProgress = new Label();
            SuspendLayout();
            // 
            // panelBillboard
            // 
            panelBillboard.BackgroundImage = (Image)resources.GetObject("panelBillboard.BackgroundImage");
            panelBillboard.BackgroundImageLayout = ImageLayout.None;
            panelBillboard.Location = new Point(14, 12);
            panelBillboard.Name = "panelBillboard";
            panelBillboard.Size = new Size(527, 94);
            panelBillboard.TabIndex = 2;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(14, 163);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(527, 30);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 3;
            // 
            // labelProgress
            // 
            labelProgress.AutoSize = true;
            labelProgress.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelProgress.ForeColor = Color.White;
            labelProgress.Location = new Point(14, 129);
            labelProgress.Name = "labelProgress";
            labelProgress.Size = new Size(104, 20);
            labelProgress.TabIndex = 4;
            labelProgress.Text = "Processing ....";
            // 
            // ProgressDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(553, 218);
            Controls.Add(labelProgress);
            Controls.Add(progressBar);
            Controls.Add(panelBillboard);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ProgressDialog";
            Text = "ProgressDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panelBillboard;
        private ProgressBar progressBar;
        private Label labelProgress;
    }
}