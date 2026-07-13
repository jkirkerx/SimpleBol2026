using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        // Method to update the progress bar value
        private void SetProgressValue(int value)
        {
            progressBar.Value = value;
        }

        // Method to update the progress bar message
        private void SetProgressMessage(string message)
        {
            labelProgress.Text = message;
        }

        // Perform the time-consuming operation here (this is just a sample)
        private async Task SimulateLongRunningTask()
        {
            int totalSteps = 100;
            for (int i = 0; i <= totalSteps; i++)
            {
                SetProgressValue(i);
                SetProgressMessage($"Processing step {i} of {totalSteps}...");
                await Task.Delay(100); // Simulate some work
            }
        }

        // Method to start the progress bar
        public async Task StartProgress()
        {
            await SimulateLongRunningTask();

            // Close the dialog when the task is completed
            this.Close();
        }

        // You can call this method from your main form or any other form to show the progress bar dialog
        public static async Task ShowProgressDialog()
        {
            using (var progressDialog = new ProgressDialog())
            {
                progressDialog.StartPosition = FormStartPosition.CenterScreen;
                progressDialog.Show();
                await progressDialog.StartProgress();
            }
        }
    }
}
