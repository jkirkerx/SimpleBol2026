using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System.Data;

namespace SimpleBol.WinForms
{
    public partial class ShippersForm : Form
    {

        private long _pages = 0;
        private int _page = 1;
        private int _show = 50;
        private long _count = 0;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IShipperRepository? shipperRepository;

        public ShippersForm(
            IServiceScopeFactory serviceProvider,
            IShipperRepository shipperRepository)
        {

            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.shipperRepository = shipperRepository;

        }

        #region Form

        protected async void ShippersFormLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            LoadComboBoxServices();
            comboBoxServices.SelectedIndex = 0;

            SetListviewShippers();

            int shippersCount = await LoadAllShippers();
            if (shippersCount == 0) { buttonEditShipper.Enabled = false; }

        }

        protected void ShippersFormShown(object sender, EventArgs e)
        {
            // Reset the Cursor here
            Cursor = Cursors.Default;
        }

        #endregion
        #region ListView

        private void SetListviewShippers()
        {
            listViewShippers.Visible = true;
            listViewShippers.Items.Clear();
            listViewShippers.Columns.Clear();

            // Set ListView Parameters
            listViewShippers.Cursor = Cursors.Hand;
            listViewShippers.View = View.Details;
            listViewShippers.LabelEdit = false;
            listViewShippers.AllowColumnReorder = true;
            listViewShippers.CheckBoxes = false;
            listViewShippers.FullRowSelect = true;
            listViewShippers.GridLines = true;
            listViewShippers.Scrollable = true;
            listViewShippers.MultiSelect = false;
            listViewShippers.OwnerDraw = false;
            listViewShippers.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewShippers.Columns.Add("ShipperId", 0, HorizontalAlignment.Center);
            listViewShippers.Columns.Add("Company Name", 260, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("City", 150, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("RegionName", 150, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("CountryName", 150, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("Phone", 150, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("Fav", 40, HorizontalAlignment.Center);
            listViewShippers.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewShippers.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewShippers.ListViewItemSorter = listviewColumnSorter;

        }

        private void ListViewShippers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            if (listViewShippers.ListViewItemSorter is not ListViewColumnSorter comparer)
            {
                Cursor = Cursors.Default;
                return;
            }
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
            this.listViewShippers.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewShippers_DoubleClick(object sender, EventArgs e)
        {
            buttonEditShipper.PerformClick();
        }


        private void ListViewShippers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            buttonEditShipper.Enabled = true;
        }

        #endregion
        #region Buttons

        private async void ButtonCreateShipper_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && shipperRepository != null)
            {
                using var shipperDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperDialog>();
                var shipperDialogDI = shipperDialogDIOwned.Form;
                shipperDialogDI.StartPosition = FormStartPosition.CenterScreen;
                shipperDialogDI.ShipperId = "ADD";
                if (shipperDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var shipper = shipperDialogDI.Shipper;
                    if (shipper != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllShippers();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditShipper_Click(object sender, EventArgs e)
        {
            if (listViewShippers.SelectedItems.Count == 1)
            {
                var listItem = listViewShippers.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && shipperRepository != null)
                {

                    using var shipperDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperDialog>();
                var shipperDialogDI = shipperDialogDIOwned.Form;
                    shipperDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    shipperDialogDI.ShipperId = objectId;
                    if (shipperDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var shipper = shipperDialogDI.Shipper;
                        if (shipper != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllShippers();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a shipper selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();

        }

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form != null && form.Name != "MainMdiForm" && form.Name != "ShippersForm")
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

        private async Task<int> LoadAllShippers()
        {
            int shipperCount = 0;

            if (shipperRepository != null)
            {
                var getShippers = await shipperRepository.GetAllShippersAsync();
                if (getShippers != null)
                {
                    var idx = -1;
                    _count = getShippers.Count;
                    shipperCount = getShippers.Count;

                    listViewShippers.Visible = true;
                    listViewShippers.Items.Clear();

                    // Calculate the pages
                    _pages = (_count + _show - 1) / _show;

                    Application.DoEvents();

                    foreach (var shipper in getShippers.OrderBy(order => order.CompanyName))
                    {
                        idx++;
                        var item1 = new ListViewItem(shipper.ShipperId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(shipper.CompanyName);
                        item1.SubItems.Add(shipper.City);
                        item1.SubItems.Add(shipper.RegionLongName);
                        item1.SubItems.Add(shipper.CountryLongName);
                        item1.SubItems.Add(shipper.Phone1);
                        item1.SubItems.Add(shipper.Favorite == true ? "X" : "");
                        item1.SubItems.Add(shipper.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(shipper.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(shipper.Comment);

                        // Make the Favorites a Green X if selected
                        item1.SubItems[6].ForeColor = Color.Green;
                        item1.UseItemStyleForSubItems = false;

                        listViewShippers.Items.Add(item1);

                    }

                }
            }

            return shipperCount;

        }

        private async Task<int> LoadAllShippersByService()
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int shipperCount = 0;

            string serviceCode = comboBoxServices.SelectedValue?.ToString() ?? "SHOWALL";
            ShipperServicesEnum serviceEnumerator = ShipperServicesEnum.SHOWALL;
            if (shipperRepository != null)
            {
                switch (serviceCode)
                {
                    case "SHOWALL":
                        serviceEnumerator = ShipperServicesEnum.SHOWALL;
                        break;

                    case "LTL":
                        serviceEnumerator = ShipperServicesEnum.LTL;
                        break;

                    case "FTL":
                        serviceEnumerator = ShipperServicesEnum.FTL;
                        break;

                    case "AIR":
                        serviceEnumerator = ShipperServicesEnum.AIR;
                        break;

                    case "OCEAN":
                        serviceEnumerator = ShipperServicesEnum.OCEAN;
                        break;

                    case "RAIL":
                        serviceEnumerator = ShipperServicesEnum.RAIL;
                        break;

                    case "LASTMILE":
                        serviceEnumerator = ShipperServicesEnum.LASTMILE;
                        break;

                    case "COURIER":
                        serviceEnumerator = ShipperServicesEnum.COURIER;
                        break;

                    case "ARMOURED":
                        serviceEnumerator = ShipperServicesEnum.ARMOURED;
                        break;
                }

                var getShippers = await shipperRepository.GetShippersByServiceCodeAsync(serviceEnumerator);
                if (getShippers != null)
                {
                    var idx = -1;
                    _count = getShippers.Count;
                    shipperCount = getShippers.Count;

                    listViewShippers.Visible = true;
                    listViewShippers.Items.Clear();

                    // Calculate the pages
                    _pages = (_count + _show - 1) / _show;

                    Application.DoEvents();

                    foreach (var shipper in getShippers.OrderBy(order => order.CompanyName))
                    {
                        idx++;
                        var item1 = new ListViewItem(shipper.ShipperId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(shipper.CompanyName);
                        item1.SubItems.Add(shipper.City);
                        item1.SubItems.Add(shipper.RegionLongName);
                        item1.SubItems.Add(shipper.CountryLongName);
                        item1.SubItems.Add(shipper.Phone1);
                        item1.SubItems.Add(shipper.Favorite == true ? "X" : "");
                        item1.SubItems.Add(shipper.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(shipper.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(shipper.Comment);

                        // Make the Favorites a Green X if selected
                        item1.SubItems[6].ForeColor = Color.Green;
                        item1.UseItemStyleForSubItems = false;

                        listViewShippers.Items.Add(item1);

                    }

                }
            }

            EnableEventHandlers();

            Cursor = Cursors.Default;

            return shipperCount;

        }

        #endregion
        #region ComboBoxes

        private void LoadComboBoxServices()
        {
            DisableEventHandlers();

            // New method of clearing the ComboBox               
            if (comboBoxServices.DataSource is DataTable currentDataTable)
            {
                currentDataTable.Clear();
                comboBoxServices.DataSource = currentDataTable;
            }

            var dtServices = new DataTable("dtServices");
            dtServices.Columns.Add(new DataColumn("Key"));
            dtServices.Columns.Add(new DataColumn("Value"));

            var rsDefault = dtServices.NewRow();
            rsDefault[0] = "Show All";
            rsDefault[1] = ShipperServicesEnum.SHOWALL;
            dtServices.Rows.Add(rsDefault);

            var rsLTL = dtServices.NewRow();
            rsLTL[0] = "LTL";
            rsLTL[1] = ShipperServicesEnum.LTL;
            dtServices.Rows.Add(rsLTL);

            var rsFTL = dtServices.NewRow();
            rsFTL[0] = "FTL";
            rsFTL[1] = ShipperServicesEnum.FTL;
            dtServices.Rows.Add(rsFTL);

            var rsAir = dtServices.NewRow();
            rsAir[0] = "Air Freight";
            rsAir[1] = ShipperServicesEnum.AIR;
            dtServices.Rows.Add(rsAir);

            var rsOcean = dtServices.NewRow();
            rsOcean[0] = "Ocean Freight";
            rsOcean[1] = ShipperServicesEnum.OCEAN;
            dtServices.Rows.Add(rsOcean);

            var rsRailRoad = dtServices.NewRow();
            rsRailRoad[0] = "Railroad";
            rsRailRoad[1] = ShipperServicesEnum.RAIL;
            dtServices.Rows.Add(rsRailRoad);

            var rsLastMile = dtServices.NewRow();
            rsLastMile[0] = "Last Mile";
            rsLastMile[1] = ShipperServicesEnum.LASTMILE;
            dtServices.Rows.Add(rsLastMile);

            var rsCourier = dtServices.NewRow();
            rsCourier[0] = "Courier";
            rsCourier[1] = ShipperServicesEnum.COURIER;
            dtServices.Rows.Add(rsCourier);

            var rsArmoured = dtServices.NewRow();
            rsArmoured[0] = "Armoured";
            rsArmoured[1] = ShipperServicesEnum.ARMOURED;
            dtServices.Rows.Add(rsArmoured);
            dtServices.AcceptChanges();

            comboBoxServices.DataSource = dtServices;
            comboBoxServices.DisplayMember = "Key";
            comboBoxServices.ValueMember = "Value";
            comboBoxServices.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxServices.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxServices.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxServices.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxServices.SelectedIndex = 0;

            EnableEventHandlers();

        }

        private async void ComboBoxServices_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int shipperCount = await LoadAllShippersByService();
            labelShipperFilterResults.Text = shipperCount.ToString() + " Result(s) located";
        }

        #endregion
        #region EventHandlers

        private void EnableEventHandlers()
        {
            comboBoxServices.SelectedIndexChanged += ComboBoxServices_SelectedIndexChanged;
            listViewShippers.SelectedIndexChanged += ListViewShippers_SelectedIndexChanged;

        }

        private void DisableEventHandlers()
        {
            comboBoxServices.SelectedIndexChanged -= ComboBoxServices_SelectedIndexChanged;
            listViewShippers.SelectedIndexChanged -= ListViewShippers_SelectedIndexChanged;
        }

        #endregion
    }


}
