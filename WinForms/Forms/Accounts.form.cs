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

    public partial class AccountsForm : Form
    {

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IAccountsRepository? accountsRepository;

        public AccountsForm(
            IServiceScopeFactory serviceProvider,
            IAccountsRepository accountsRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.accountsRepository = accountsRepository;
        }

        #region Form

        protected async void AccountsFormLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SetListViewAccounts();

            int accountsCount = await LoadAllAccounts();
            if (accountsCount == 0) { buttonEditAccount.Enabled = false; }

        }

        protected void AccountsFormShown(object sender, EventArgs e)
        {
            // Reset the Cursor here
            Cursor = Cursors.Default;
        }

        #endregion
        #region LoadSave

        private async Task<int> LoadAllAccounts()
        {
            int accountsCount = 0;

            if (accountsRepository != null)
            {
                var getAccounts = await accountsRepository.GetAllAccountsAsync();
                if (getAccounts != null)
                {
                    var idx = -1;

                    var activeAccounts = getAccounts
                        .Where(account => !account.MarkAsDeleted)
                        .OrderBy(account => account.Name)
                        .ToList();

                    accountsCount = activeAccounts.Count;

                    listViewAccounts.Visible = true;
                    listViewAccounts.Items.Clear();

                    Application.DoEvents();

                    foreach (var account in activeAccounts)
                    {
                        idx++;
                        var item1 = new ListViewItem(account.AccountId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(account.LoginId);
                        item1.SubItems.Add(account.Name);
                        item1.SubItems.Add(account.Location);
                        item1.SubItems.Add(account.Phone);
                        item1.SubItems.Add(account.EmailAddress);
                        item1.SubItems.Add(account.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(account.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(account.Comments);

                        listViewAccounts.Items.Add(item1);

                    }

                }
            }

            return accountsCount;

        }

        #endregion
        #region ListView

        private void SetListViewAccounts()
        {
            listViewAccounts.Visible = true;
            listViewAccounts.Items.Clear();
            listViewAccounts.Columns.Clear();

            // Set ListView Parameters
            listViewAccounts.Cursor = Cursors.Hand;
            listViewAccounts.View = View.Details;
            listViewAccounts.LabelEdit = false;
            listViewAccounts.AllowColumnReorder = true;
            listViewAccounts.CheckBoxes = false;
            listViewAccounts.FullRowSelect = true;
            listViewAccounts.GridLines = true;
            listViewAccounts.Scrollable = true;
            listViewAccounts.MultiSelect = false;
            listViewAccounts.OwnerDraw = false;
            listViewAccounts.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewAccounts.Columns.Add("AccountId", 0, HorizontalAlignment.Center);
            listViewAccounts.Columns.Add("Login ID", 160, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Name", 260, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Location", 260, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Phone", 160, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Email Address", 260, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewAccounts.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewAccounts.ListViewItemSorter = listviewColumnSorter;

        }

        private void ListViewAccounts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewAccounts.ListViewItemSorter;
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
            this.listViewAccounts.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewAccounts_DoubleClick(object sender, EventArgs e)
        {
            buttonEditAccount.PerformClick();

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

        private async void ButtonCreateAccount_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && accountsRepository != null)
            {
                using var accountDialogDIOwned = serviceProvider.CreateOwnedForm<AccountDialog>();
                var accountDialogDI = accountDialogDIOwned.Form;
                accountDialogDI.StartPosition = FormStartPosition.CenterScreen;
                accountDialogDI.AccountId = "ADD";
                if (accountDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var account = accountDialogDI.Account;
                    if (account != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllAccounts();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditAccount_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count == 1)
            {
                var listItem = listViewAccounts.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && accountsRepository != null)
                {
                    using var accountDialogDIOwned = serviceProvider.CreateOwnedForm<AccountDialog>();
                var accountDialogDI = accountDialogDIOwned.Form;
                    accountDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    accountDialogDI.AccountId = objectId;
                    if (accountDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var account = accountDialogDI.Account;
                        if (account != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllAccounts();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a account selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private async void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count != 1)
            {
                MessageBox.Show(
                    "You must select an account to delete.",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedItem = listViewAccounts.SelectedItems[0];
            var accountId = selectedItem.SubItems[0].Text;
            var loginId = selectedItem.SubItems[1].Text;
            var confirmation = MessageBox.Show(
                $"Are you sure you want to delete the account '{loginId}'?",
                Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirmation != DialogResult.Yes || accountsRepository is null)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;
                buttonDelete.Enabled = false;

                if (!await accountsRepository.RemoveAccountAsync(accountId))
                {
                    MessageBox.Show(
                        "The account could not be found or deleted.",
                        Application.ProductName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var accountsCount = await LoadAllAccounts();
                buttonEditAccount.Enabled = accountsCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"The account could not be deleted.\n\n{ex.Message}",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                buttonDelete.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}
