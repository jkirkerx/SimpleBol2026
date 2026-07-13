using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.WinForms.Forms;

namespace SimpleBol.WinForms
{
    public partial class ProgramMenuForm : Form
    {
        private readonly IServiceScopeFactory serviceProvider;

        public ProgramMenuForm(IServiceScopeFactory serviceProvider)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
        }

        protected void ProgramMenuFormLoad(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ResumeLayout(false);

            this.Cursor = Cursors.Arrow;
        }

        protected void ProgramMenuFormShown(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            SetToolTips();
            this.Cursor = Cursors.Arrow;

        }

        #region Resize

        private void PanelMenuResize(object sender, EventArgs e)
        {

            var width = panelMenu.Width;
            var height = panelMenu.Height;

            foreach (Control control in panelMenu.Controls)
            {

                if ((control) is Button)
                {
                    var button = (Button)control;
                    button.Size = (Size)new Point((width / 3) - 50, (height / 3) + 50);

                    switch (button.Name)
                    {
                        case "btnCreateBol":
                            button.Location = new Point(20, 20);
                            break;

                        case "btnBlank1":
                            button.Location = new Point((width / 3) + 50, 50);
                            break;

                        case "btnBlank2":
                            button.Location = new Point((width / 3) * 2 + 50, 50);
                            break;
                    }

                }

                Application.DoEvents();

            }

        }

        #endregion
        #region Buttons

        private void ButtonPowerDown_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Power down application", Application.ProductName, MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                // Close all WinForms and Dialogs
                for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
                {
                    var form = Application.OpenForms[i];
                    form.Close();
                }

                Application.Exit();
            }

        }

        private void ButtonCreateBol_Click(object sender, EventArgs e)
        {
            NavigateTo<BolsForm>();
        }

        private void ButtonBolDispute_Click(object sender, EventArgs e)
        {
            NavigateTo<BillingDisputesForm>();
        }

        private void ButtonShippers_Click(object sender, EventArgs e)
        {
            NavigateTo<ShippersForm>();
        }

        private void ButtonVendors_Click(object sender, EventArgs e)
        {

            NavigateTo<VendorsForm>();
        }

        private void ButtonCustomers_Click(object sender, EventArgs e)
        {
            NavigateTo<CustomersForm>();
        }

        private void ButtonNmfcFreightCodes_Click(object sender, EventArgs e)
        {
            NavigateTo<NmfcFreightCodesForm>();
        }

        private void ButtonFreightClassCodes_Click(object sender, EventArgs e)
        {
            NavigateTo<FreightClassCodesForm>();
        }

        private void NavigateTo<TForm>() where TForm : Form
        {
            // Keep this menu alive until the destination form has its own scope.
            for (var i = Application.OpenForms.Count - 1; i >= 1; i -= 1)
            {
                var form = Application.OpenForms[i];
                if (form != this && form.Name != "MainMdiForm")
                    form.Close();
            }

            var ownedForm = serviceProvider.CreateOwnedForm<TForm>();
            ownedForm.Form.MdiParent = MainMdiForm.ActiveForm;
            ownedForm.Form.Dock = DockStyle.Fill;
            ownedForm.Show();

            Close();
        }

        #endregion
        #region ToolTips

        private void SetToolTips()
        {
            var ttPowerDown = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttPowerDown.SetToolTip(this.buttonPowerDown, "Close this application");

            var ttCreateBol = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttCreateBol.SetToolTip(this.buttonCreateBol, "Create or Edit your Bill of Ladding");

            var ttCreateBolDispute = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttCreateBolDispute.SetToolTip(this.buttonBolDispute, "Create or Edit your Billing Dispute");

            var ttShippers = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttShippers.SetToolTip(this.buttonShippers, "Manage your shippers, choose your favorites");

            var ttVendors = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttVendors.SetToolTip(this.buttonVendors, "Manage your vendors, edit their shipping locations");

            var ttCustomers = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttCustomers.SetToolTip(this.buttonCustomers, "Manage your customers, edit their shipping locations");

            var ttNmfcCodes = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttNmfcCodes.SetToolTip(this.buttonNmfcFreightCodes, "Manage your NMFC Freight Codes, edit their details");

            var ttFreightClassCodes = new ToolTip()
            {
                AutoPopDelay = 1000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttFreightClassCodes.SetToolTip(this.buttonFreightClassCodes, "Manage your Freight Class Codes, edit their details");
        }

        #endregion





    }
}
