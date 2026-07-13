using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleBol.WinForms
{
    public partial class CustomersForm : Form
    {

        private long _pages = 0;
        private int _page = 1;
        private int _show = 50;
        private long _count = 0;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICustomerRepository? customerRepository;

        public CustomersForm(
            IServiceScopeFactory serviceProvider,
            ICustomerRepository customerRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.customerRepository = customerRepository;

        }

        #region Form

        protected async void CustomersFormLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SetListviewCustomers();

            int customerCount = await LoadAllCustomers();
            if (customerCount == 0) { buttonEditCustomer.Enabled = false; }


        }

        protected void CustomersFormShown(object sender, EventArgs e)
        {

            // Return the cursor to default here
            Cursor = Cursors.Default;
        }

        #endregion
        #region ListView

        private void SetListviewCustomers()
        {
            listViewCustomers.Visible = true;
            listViewCustomers.Items.Clear();
            listViewCustomers.Columns.Clear();

            // Set ListView Parameters
            listViewCustomers.Cursor = Cursors.Hand;
            listViewCustomers.View = View.Details;
            listViewCustomers.LabelEdit = false;
            listViewCustomers.AllowColumnReorder = true;
            listViewCustomers.CheckBoxes = false;
            listViewCustomers.FullRowSelect = true;
            listViewCustomers.GridLines = true;
            listViewCustomers.Scrollable = true;
            listViewCustomers.MultiSelect = false;
            listViewCustomers.OwnerDraw = false;
            listViewCustomers.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewCustomers.Columns.Add("BindToCustomerId", 0, HorizontalAlignment.Center);
            listViewCustomers.Columns.Add("Company Name", 260, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("City", 150, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("RegionName", 150, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("CountryName", 150, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("Phone", 150, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewCustomers.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewCustomers.ListViewItemSorter = listviewColumnSorter;

        }

        private void ListViewCustomers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewCustomers.ListViewItemSorter;
            if (!e.Column.Equals(comparer.SortColumn))
            {
                // Set the column number that is to be sorted; default to ascending.
                comparer.SortColumn = e.Column;
                comparer.Order = SortOrder.Ascending;
            }
            else
            {
                // Reverse the current sort direction for this column.
                if (comparer.Order.Equals(SortOrder.Ascending))
                    comparer.Order = SortOrder.Descending;
                else
                    comparer.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            comparer.SortColumn = e.Column;
            this.listViewCustomers.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private async void ListViewCustomers_DoubleClick(object sender, EventArgs e)
        {
            if (listViewCustomers.SelectedItems.Count == 1)
            {
                var listItem = listViewCustomers.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null)
                {
                    using var customerDialogDIOwned = serviceProvider.CreateOwnedForm<CustomerDialog>();
                var customerDialogDI = customerDialogDIOwned.Form;
                    customerDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    customerDialogDI.CustomerId = objectId;
                    if (customerDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var customer = customerDialogDI.Customer;
                        if (customer != null)
                        {
                            Application.DoEvents();
                            _ = await this.LoadAllCustomers();
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("You must make a customer selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }


        private void ListViewCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditCustomer.Enabled = true;
        }

        #endregion
        #region Buttons

        private async void ButtonCreateCustomer_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && customerRepository != null)
            {
                using var customerDialogDIOwned = serviceProvider.CreateOwnedForm<CustomerDialog>();
                var customerDialogDI = customerDialogDIOwned.Form;
                customerDialogDI.StartPosition = FormStartPosition.CenterScreen;
                customerDialogDI.CustomerId = "ADD";
                if (customerDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var customer = customerDialogDI.Customer;
                    if (customer != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllCustomers();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditCustomer_Click(object sender, EventArgs e)
        {
            if (listViewCustomers.SelectedItems.Count == 1)
            {
                var listItem = listViewCustomers.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && customerRepository != null)
                {

                    using var customerDialogDIOwned = serviceProvider.CreateOwnedForm<CustomerDialog>();
                var customerDialogDI = customerDialogDIOwned.Form;
                    customerDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    customerDialogDI.CustomerId = objectId;
                    if (customerDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var shipper = customerDialogDI.Customer;
                        if (shipper != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllCustomers();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a customer selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();

        }

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm" && form.Name != "CustomersForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var programMenuFormDIOwned = serviceProvider.CreateOwnedForm<ProgramMenuForm>();
                var programMenuFormDI = programMenuFormDIOwned.Form;
                programMenuFormDI.MdiParent = MainMdiForm.ActiveForm;
                programMenuFormDI.Dock = DockStyle.Fill;
                programMenuFormDI.StartPosition = FormStartPosition.CenterParent;
                programMenuFormDI.WindowState = FormWindowState.Maximized;
                programMenuFormDIOwned.Show();
            }

            Application.DoEvents();

            if (MainMdiForm.ActiveForm != null)
                MainMdiForm.ActiveForm.Height = MainMdiForm.ActiveForm.Height + 1;

            this.Close();
            this.Dispose();
        }

        #endregion
        #region LoadSave

        private async Task<int> LoadAllCustomers()
        {
            int customerCount = 0;

            if (customerRepository != null)
            {
                var getCustomers = await customerRepository.GetAllCustomersAsync();
                if (getCustomers != null)
                {
                    var idx = -1;
                    _count = getCustomers.Count;
                    customerCount = getCustomers.Count;

                    listViewCustomers.Visible = true;
                    listViewCustomers.Items.Clear();

                    // Calculate the pages
                    _pages = (_count + _show - 1) / _show;

                    Application.DoEvents();

                    foreach (var customer in getCustomers.OrderBy(order => order.CompanyName))
                    {
                        idx++;
                        var item1 = new ListViewItem(customer.CustomerId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(customer.CompanyName);
                        item1.SubItems.Add(customer.City);
                        item1.SubItems.Add(customer.RegionLongName);
                        item1.SubItems.Add(customer.CountryLongName);
                        item1.SubItems.Add(customer.Phone1);
                        item1.SubItems.Add(customer.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(customer.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(customer.Comment);

                        // Make the Favorites a Green X if selected
                        item1.SubItems[6].ForeColor = Color.Green;
                        item1.UseItemStyleForSubItems = false;

                        listViewCustomers.Items.Add(item1);

                    }

                }
            }

            return customerCount;

        }

        #endregion

    }

}
