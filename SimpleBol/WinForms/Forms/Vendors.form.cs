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
    public partial class VendorsForm : Form
    {
        private long _pages = 0;
        private int _page = 1;
        private int _show = 50;
        private long _count = 0;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IVendorRepository? vendorRepository;

        public VendorsForm(
            IServiceScopeFactory serviceProvider,
            IVendorRepository vendorRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.vendorRepository = vendorRepository;

        }

        #region Form

        protected async void VendorsFormLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SetListviewVendors();

            int vendorCount = await LoadAllVendors();


        }

        protected void VendorsFormShown(object sender, EventArgs e)
        {
            // Reset the cursor here
            Cursor = Cursors.Default;
        }

        #endregion
        #region ListView

        private void SetListviewVendors()
        {
            listViewVendors.Visible = true;
            listViewVendors.Items.Clear();
            listViewVendors.Columns.Clear();

            // Set ListView Parameters
            listViewVendors.Cursor = Cursors.Hand;
            listViewVendors.View = View.Details;
            listViewVendors.LabelEdit = false;
            listViewVendors.AllowColumnReorder = true;
            listViewVendors.CheckBoxes = false;
            listViewVendors.FullRowSelect = true;
            listViewVendors.GridLines = true;
            listViewVendors.Scrollable = true;
            listViewVendors.MultiSelect = false;
            listViewVendors.OwnerDraw = false;
            listViewVendors.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewVendors.Columns.Add("ShipperId", 0, HorizontalAlignment.Center);
            listViewVendors.Columns.Add("Company Name", 260, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("City", 150, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("RegionName", 150, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("CountryName", 150, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("Phone", 150, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewVendors.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewVendors.ListViewItemSorter = listviewColumnSorter;

        }

        private void ListViewVendors_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewVendors.ListViewItemSorter;
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
            this.listViewVendors.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private async void ListViewVendors_DoubleClick(object sender, EventArgs e)
        {
            if (listViewVendors.SelectedItems.Count == 1)
            {
                var listItem = listViewVendors.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null)
                {
                    using var vendorDialogDIOwned = serviceProvider.CreateOwnedForm<VendorDialog>();
                var vendorDialogDI = vendorDialogDIOwned.Form;
                    vendorDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    vendorDialogDI.VendorId = objectId;
                    if (vendorDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var vendor = vendorDialogDI.Vendor;
                        if (vendor != null)
                        {
                            Application.DoEvents();
                            _ = await this.LoadAllVendors();
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("You must make a vendor selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }


        private void ListViewVendors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        #endregion
        #region LoadSave

        private async Task<int> LoadAllVendors()
        {
            int vendorCount = 0;

            if (vendorRepository != null)
            {
                var getVendors = await vendorRepository.GetAllVendorsAsync();
                if (getVendors != null)
                {
                    var idx = -1;
                    _count = getVendors.Count;
                    vendorCount = getVendors.Count;

                    listViewVendors.Visible = true;
                    listViewVendors.Items.Clear();

                    // Calculate the pages
                    _pages = (_count + _show - 1) / _show;

                    Application.DoEvents();

                    foreach (var vendor in getVendors.OrderBy(order => order.CompanyName))
                    {
                        idx++;
                        var item1 = new ListViewItem(vendor.VendorId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(vendor.CompanyName);
                        item1.SubItems.Add(vendor.City);
                        item1.SubItems.Add(vendor.RegionLongName);
                        item1.SubItems.Add(vendor.CountryLongName);
                        item1.SubItems.Add(vendor.Phone1);
                        item1.SubItems.Add(vendor.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(vendor.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(vendor.Comment);

                        // Make the Favorites a Green X if selected
                        item1.SubItems[6].ForeColor = Color.Green;
                        item1.UseItemStyleForSubItems = false;

                        listViewVendors.Items.Add(item1);

                    }

                }
            }

            return vendorCount;

        }

        #endregion
        #region Buttons

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm" && form.Name != "VendorsForm")
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

        private async void ButtonCreateVendor_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && vendorRepository != null)
            {
                using var vendorDialogDIOwned = serviceProvider.CreateOwnedForm<VendorDialog>();
                var vendorDialogDI = vendorDialogDIOwned.Form;
                vendorDialogDI.StartPosition = FormStartPosition.CenterScreen;
                vendorDialogDI.VendorId = "ADD";
                if (vendorDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var vendor = vendorDialogDI.Vendor;
                    if (vendor != null)
                    {
                        // Populate the list of vendors
                        await Task.Delay(2500);
                        _ = await LoadAllVendors();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditVendor_Click(object sender, EventArgs e)
        {
            if (listViewVendors.SelectedItems.Count == 1)
            {
                var listItem = listViewVendors.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && vendorRepository != null)
                {

                    using var vendorDialogDIOwned = serviceProvider.CreateOwnedForm<VendorDialog>();
                var vendorDialogDI = vendorDialogDIOwned.Form;
                    vendorDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    vendorDialogDI.VendorId = objectId;
                    if (vendorDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var vendor = vendorDialogDI.Vendor;
                        if (vendor != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllVendors();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a vendor selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }


        #endregion


    }
}
