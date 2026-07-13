using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
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

namespace SimpleBol.WinForms.Forms
{
    public partial class BillToAccountsForm : Form
    {
        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IBillToAccountsRepository? billToAccountsRepository;
        private readonly ICustomerRepository? customersRepository;

        public BillToAccountsForm(
            IServiceScopeFactory serviceProvider,
            IBillToAccountsRepository billToAccountsRepository,
            ICustomerRepository customersRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.billToAccountsRepository = billToAccountsRepository;
            this.customersRepository = customersRepository;

        }

        #region Form

        protected async void BillToAccountsFormLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            // Load the Customers Filter
            int customersCount = await LoadComboBoxCustomers();

            SetListViewBillToAccounts();

            int accountsCount = await LoadAllBillToAccounts();
            if (accountsCount == 0) { buttonEditBillToAccount.Enabled = false; }            

        }

        protected void BillToAccountsFormShown(object sender, EventArgs e)
        {
            // Reset the Cursor here
            Cursor = Cursors.Default;

            EnableEventHandlers();
        }

        #endregion
        #region Events

        private void EnableEventHandlers()
        {
            listViewBillToAccounts.SelectedIndexChanged += ListViewBillToAccounts_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;

        }

        private void DisableEventHandlers()
        {
            listViewBillToAccounts.SelectedIndexChanged -= ListViewBillToAccounts_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged -= ComboBoxCustomers_SelectedIndexChanged;

        }

        #endregion
        #region Load

        private async Task<int> LoadAllBillToAccounts()
        {
            int accountsCount = 0;

            if (billToAccountsRepository != null)
            {
                var getBillToAccounts = await billToAccountsRepository.GetAllBillToAccountsAsync();
                if (getBillToAccounts != null)
                {
                    accountsCount = getBillToAccounts.Count;

                    listViewBillToAccounts.Visible = true;
                    LoadListviewByAsyncObject(getBillToAccounts);
                    Application.DoEvents();

                }
            }

            return accountsCount;

        }


        #endregion
        #region ListView

        private void SetListViewBillToAccounts()
        {
            listViewBillToAccounts.Visible = true;
            listViewBillToAccounts.Items.Clear();
            listViewBillToAccounts.Columns.Clear();

            // Set ListView Parameters
            listViewBillToAccounts.Cursor = Cursors.Hand;
            listViewBillToAccounts.View = View.Details;
            listViewBillToAccounts.LabelEdit = false;
            listViewBillToAccounts.AllowColumnReorder = true;
            listViewBillToAccounts.CheckBoxes = false;
            listViewBillToAccounts.FullRowSelect = true;
            listViewBillToAccounts.GridLines = true;
            listViewBillToAccounts.Scrollable = true;
            listViewBillToAccounts.MultiSelect = false;
            listViewBillToAccounts.OwnerDraw = false;
            listViewBillToAccounts.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewBillToAccounts.Columns.Add("AccountId", 0, HorizontalAlignment.Center);
            listViewBillToAccounts.Columns.Add("Company Name", 250, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("City", 260, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Region", 160, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Phone", 160, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Email Address", 260, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewBillToAccounts.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewBillToAccounts.ListViewItemSorter = listviewColumnSorter;

        }

        private void LoadListviewByAsyncObject(List<BILLTOACCOUNTS> getBillToAccounts)
        {

            listViewBillToAccounts.Items.Clear();

            int idx = -1;
            foreach (var account in getBillToAccounts.OrderBy(order => order.CompanyName))
            {
                idx++;
                var item1 = new ListViewItem(account.BillToAccountId)
                {
                    Checked = false,
                    ImageIndex = idx
                };

                item1.SubItems.Add(account.CompanyName);
                item1.SubItems.Add(account.City);
                item1.SubItems.Add(account.RegionLongName);
                item1.SubItems.Add(account.ContactPhone1);
                item1.SubItems.Add(account.ContactEmailAddress1);
                item1.SubItems.Add(account.CreatedOnUtc.ToLocalTime().ToString());
                item1.SubItems.Add(account.UpdatedOnUtc.ToLocalTime().ToString());
                item1.SubItems.Add(account.Comment);

                listViewBillToAccounts.Items.Add(item1);

            }

        }

        private void ListViewBillToAccounts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewBillToAccounts.ListViewItemSorter;
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
            this.listViewBillToAccounts.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewBillToAccounts_DoubleClick(object? sender, EventArgs e)
        {
            buttonEditBillToAccount.PerformClick();

        }

        private void ListViewBillToAccounts_SelectedIndexChanged(object? sender, EventArgs e)
        {
            buttonEditBillToAccount.Enabled = true;
        }

        #endregion
        #region ComboBoxes

        private async Task<int> LoadComboBoxCustomers()
        {
            int customersCount = 0;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxCustomers.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxCustomers.DataSource = currentDataTable;
            }

            if (customersRepository != null)
            {

                var getAllCustomers = await customersRepository.GetAllCustomersAsync();
                if (getAllCustomers != null)
                {
                    customersCount = getAllCustomers.Count();

                    if (customersCount > 0)
                    {

                        var dtCustomers = new DataTable("dtCustomers");
                        dtCustomers.Columns.Add(new DataColumn("Key"));
                        dtCustomers.Columns.Add(new DataColumn("Value"));

                        var rsDefault = dtCustomers.NewRow();
                        rsDefault[0] = "-- Make a selection --";
                        rsDefault[1] = 0;
                        dtCustomers.Rows.Add(rsDefault);

                        foreach (var customerItem in getAllCustomers.OrderBy(ob => ob?.CompanyName))
                        {
                            if (customerItem != null)
                            {
                                var rsShipperItem = dtCustomers.NewRow();
                                rsShipperItem[0] = customerItem.CompanyName;
                                rsShipperItem[1] = customerItem.CustomerId;
                                dtCustomers.Rows.Add(rsShipperItem);
                            }
                        }

                        dtCustomers.AcceptChanges();

                        comboBoxCustomers.DataSource = dtCustomers;
                        comboBoxCustomers.DisplayMember = "Key";
                        comboBoxCustomers.ValueMember = "Value";
                        comboBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxCustomers.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBoxCustomers.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxCustomers.SelectedIndex = 0;

                    }
                }

            }

            return customersCount;

        }

        #endregion
        #region Buttons

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm" && form.Name != "ShippersForm")
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

        private async void ButtonCreateBillToAccount_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && billToAccountsRepository != null)
            {
                using var billToaccountDialogDIOwned = serviceProvider.CreateOwnedForm<BillToAccountDialog>();
                var billToaccountDialogDI = billToaccountDialogDIOwned.Form;
                billToaccountDialogDI.StartPosition = FormStartPosition.CenterScreen;
                billToaccountDialogDI.BillToAccountId = "ADD";
                if (billToaccountDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var billToAccount = billToaccountDialogDI.BillToAccount;
                    if (billToAccount != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllBillToAccounts();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditBillToAccount_Click(object sender, EventArgs e)
        {
            if (listViewBillToAccounts.SelectedItems.Count == 1)
            {
                var listItem = listViewBillToAccounts.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && billToAccountsRepository != null)
                {
                    using var billToAccountDialogDIOwned = serviceProvider.CreateOwnedForm<BillToAccountDialog>();
                var billToAccountDialogDI = billToAccountDialogDIOwned.Form;
                    billToAccountDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    billToAccountDialogDI.BillToAccountId = objectId;
                    if (billToAccountDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var billToAccount = billToAccountDialogDI.BillToAccount;
                        if (billToAccount != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllBillToAccounts();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Bill to 3rd Party account selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        #endregion
        #region Filters

        private async void ComboBoxCustomers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int billToAccountsCount = 0;

            if (billToAccountsRepository != null)
            {
                string customerId = (comboBoxCustomers.SelectedValue as dynamic).ToString();
                if (customerId != "0")
                {
                    var getBillToAccounts = await billToAccountsRepository.GetBillToAccountsByCustomerIdAsync(customerId);
                    if (getBillToAccounts != null)
                    {
                        billToAccountsCount = getBillToAccounts.Count;
                        LoadListviewByAsyncObject(getBillToAccounts);
                        labeCustomerFilterResults.Text = billToAccountsCount.ToString() + " Result(s) located";
                    }
                    else
                    {
                        labeCustomerFilterResults.Text = "0 Result(s) located";
                    }
                }
                else if (customerId == "0")
                {
                    var getBillToAccounts = await billToAccountsRepository.GetAllBillToAccountsAsync();
                    if (getBillToAccounts != null)
                    {
                        billToAccountsCount = getBillToAccounts.Count;
                        LoadListviewByAsyncObject(getBillToAccounts);
                        labeCustomerFilterResults.Text = billToAccountsCount.ToString() + " Result(s) located";
                    }
                    else
                    {
                        labeCustomerFilterResults.Text = "0 Result(s) located";
                    }
                }

            }

            EnableEventHandlers();
            Cursor = Cursors.Default;
        }


        #endregion

    }
}
