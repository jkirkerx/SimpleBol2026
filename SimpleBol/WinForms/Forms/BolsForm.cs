using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System.Data;

namespace SimpleBol.WinForms
{
    public partial class BolsForm : Form
    {
        private long _pages = 0;
        private int _page = 1;
        private int _show = 50;
        private long _count = 0;

        private Filter _filterApplied = Filter.None;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IBolsRepository? bolRepository;
        private readonly IShipperRepository shipperRepository;
        private readonly IVendorRepository vendorRepository;
        private readonly ICustomerRepository customerRepository;
        public BolsForm(
            IServiceScopeFactory serviceProvider,
            IBolsRepository bolRepository,
            IShipperRepository shipperRepository,
            IVendorRepository vendorRepository,
            ICustomerRepository customerRepository)
        {

            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.bolRepository = bolRepository;
            this.shipperRepository = shipperRepository;
            this.vendorRepository = vendorRepository;
            this.customerRepository = customerRepository;
            buttonEmailDialog.Click += ButtonEmailDialog_Click;
        }

        #region Form

        protected void BolsFormLoad(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;

            // Disable Event Handlers
            this.DisableEventHandlers();

            // Intialize the ListView Control
            SetListViewBols();

            // Program the Show record amount
            numericUpDownShow.Value = 25;
            numericUpDownShow.Minimum = 25;
            numericUpDownShow.Maximum = 250;

            // Dispute Button
            buttonDispute.Visible = false;
            buttonDispute.Enabled = false;

            // Assign Pagination
            _show = (int)numericUpDownShow.Value;
            _pages = 1;
            _page = 1;
            _count = 0;

        }

        protected async void BolsFormShown(object sender, EventArgs e)
        {
            // Load the Reconcile Filters
            LoadComboBoxReconcile();

            // Default the BOL list to the current week.
            int bolCount = await LoadAllBolsByReconcile();
            if (bolCount == 0) { buttonEditBol.Enabled = false; }

            // Load the Async Filters            
            _ = await LoadComboBoxShippers();
            _ = await LoadComboBoxVendors();
            _ = await LoadComboBoxCustomers();

            // Populate every filter summary without changing the active This Week list.
            await LoadInitialFilterRecordCountsAsync();

            // Enable Event Handlers
            this.EnableEventHandlers();

            // Return the cursor to default here
            Cursor = Cursors.Default;
        }

        #endregion
        #region Buttons

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm" && form.Name != "CreateBolForm")
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

                Application.DoEvents();

                if (MainMdiForm.ActiveForm != null)
                    MainMdiForm.ActiveForm.Height = MainMdiForm.ActiveForm.Height + 1;

                this.Close();
                this.Dispose();

            }

        }

        #endregion
        #region ButtonClicks

        private void ButtonClearFilters_Click(object sender, EventArgs e)
        {
            ClearFilters();

        }

        private async void ButtonCreateBol_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && bolRepository != null)
            {
                using var bolDialogDIOwned = serviceProvider.CreateOwnedForm<BolDialog>();
                var bolDialogDI = bolDialogDIOwned.Form;
                bolDialogDI.StartPosition = FormStartPosition.CenterScreen;
                bolDialogDI.BolId = "ADD";

                if (bolDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var bol = bolDialogDI.Bol;
                    if (bol != null)
                    {
                        // SaveBol has completed before the dialog returns OK, so the
                        // active list can be refreshed immediately and consistently.
                        _count = await LoadAllBolsByAssignedFilter();
                    }
                }
            }
        }

        private async void ButtonEditBol_Click(object sender, EventArgs e)
        {
            if (listViewBols.SelectedItems.Count == 1)
            {
                var listItem = listViewBols.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && bolRepository != null)
                {

                    using var bolDialogDIOwned = serviceProvider.CreateOwnedForm<BolDialog>();
                var bolDialogDI = bolDialogDIOwned.Form;
                    bolDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    bolDialogDI.BolId = objectId;
                    if (bolDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var bol = bolDialogDI.Bol;
                        if (bol != null)
                        {
                            await Task.Delay(2500);

                            // Figure out what filter mode we are in
                            _count = await this.LoadAllBolsByAssignedFilter();

                            Application.DoEvents();
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Bill of Ladding (BOL) selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private async void ButtonDispute_Click(object sender, EventArgs e)
        {
            if (listViewBols.SelectedItems.Count == 1)
            {
                var listItem = listViewBols.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && bolRepository != null)
                {

                    using var bolDisputeDialogDIOwned = serviceProvider.CreateOwnedForm<BillingDisputeDialog>();
                var bolDisputeDialogDI = bolDisputeDialogDIOwned.Form;
                    bolDisputeDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    bolDisputeDialogDI.BolId = objectId;                    
                    if (bolDisputeDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var bolDispute = bolDisputeDialogDI.Dispute;
                        if (bolDispute != null)
                        {
                            await Task.Delay(2500);

                            // Figure out what filter mode we are in
                            _count = await this.LoadAllBolsByAssignedFilter();

                            Application.DoEvents();
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Bill of Ladding (BOL) selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();


        }

        private async void ButtonPrintDialog_Click(object sender, EventArgs e)
        {
            if (listViewBols.SelectedItems.Count == 1)
            {
                var listItem = listViewBols.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && bolRepository != null)
                {
                    var getBol = await bolRepository.GetOneBillOfLaddingAsync(objectId);
                    if (getBol != null)
                    {
                        using var printBolDialogDIOwned = serviceProvider.CreateOwnedForm<PrintBolDialog>();
                var printBolDialogDI = printBolDialogDIOwned.Form;
                        printBolDialogDI.StartPosition = FormStartPosition.CenterScreen;
                        printBolDialogDI.PrintBolId = objectId;
                        printBolDialogDI.PrintBol = getBol;

                        if (printBolDialogDI.ShowDialog() == DialogResult.OK)
                        {
                            var printBol = printBolDialogDI.PrintBol;
                            if (printBol != null)
                            {


                            }
                        }

                        Application.DoEvents();
                    }
                }                
            }
        }

        private async void ButtonEmailDialog_Click(object? sender, EventArgs e)
        {
            if (listViewBols.SelectedItems.Count != 1)
            {
                MessageBox.Show("You must make a Bill of Lading (BOL) selection in the list view",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (serviceProvider == null || bolRepository == null)
                return;

            var bolId = listViewBols.SelectedItems[0].SubItems[0].Text;
            var bol = await bolRepository.GetOneBillOfLaddingAsync(bolId);
            if (bol == null)
            {
                MessageBox.Show("The selected BOL could not be loaded.", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customerId = bol.CustomerId ?? bol.ShipToId;
            var customer = bol.Customer ?? bol.ShipToCustomer;
            if (customer == null && !string.IsNullOrWhiteSpace(customerId))
                customer = await customerRepository.GetOneCustomerAsync(customerId);

            if (customer == null || string.IsNullOrWhiteSpace(customerId))
            {
                MessageBox.Show("The selected BOL does not have a customer to email.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var emailBolDialogOwned = serviceProvider.CreateOwnedForm<EmailBolDialog>();
            var emailBolDialog = emailBolDialogOwned.Form;
            emailBolDialog.StartPosition = FormStartPosition.CenterParent;
            emailBolDialog.CustomerId = customerId;
            emailBolDialog.customer = customer;
            emailBolDialog.EmailBolId = bolId;
            emailBolDialog.EmailBol = bol;
            emailBolDialog.ShowDialog(this);
        }

        #endregion
        #region Events

        private void DisableEventHandlers()
        {
            numericUpDownShow.ValueChanged -= NumericUpDownShow_ValueChanged;
            comboBoxReconcile.SelectedIndexChanged -= ComboBoxReconcile_SelectedIndexChanged;
            dateTimePickerFilterByShipDate.ValueChanged -= DateTimePickerFilterByShipDate_ValueChanged;
            comboBoxShippers.SelectedIndexChanged -= ComboBoxShippers_SelectedIndexChanged;
            comboBoxVendors.SelectedIndexChanged -= ComboBoxVendors_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged -= ComboBoxCustomers_SelectedIndexChanged;

        }

        private void EnableEventHandlers()
        {
            numericUpDownShow.ValueChanged += NumericUpDownShow_ValueChanged;
            comboBoxReconcile.SelectedIndexChanged += ComboBoxReconcile_SelectedIndexChanged;
            dateTimePickerFilterByShipDate.ValueChanged += DateTimePickerFilterByShipDate_ValueChanged;
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            comboBoxVendors.SelectedIndexChanged += ComboBoxVendors_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;

        }



        #endregion
        #region ListView

        private void SetListViewBols()
        {

            listViewBols.Visible = true;
            listViewBols.Items.Clear();
            listViewBols.Columns.Clear();

            // Set ListView Parameters
            listViewBols.Cursor = Cursors.Hand;
            listViewBols.View = View.Details;
            listViewBols.LabelEdit = false;
            listViewBols.AllowColumnReorder = true;
            listViewBols.CheckBoxes = false;
            listViewBols.FullRowSelect = true;
            listViewBols.GridLines = true;
            listViewBols.Scrollable = true;
            listViewBols.MultiSelect = false;
            listViewBols.OwnerDraw = false;
            listViewBols.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewBols.Columns.Add("BolId", 0, HorizontalAlignment.Center);
            listViewBols.Columns.Add("Ship Date", 160, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Order #", 80, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Customer", 220, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Shipper", 200, HorizontalAlignment.Left);
            listViewBols.Columns.Add("From", 140, HorizontalAlignment.Left);
            listViewBols.Columns.Add("To", 140, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Quote Price", 100, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Actual Price", 100, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Disputed", 60, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Printed", 60, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Pallets", 60, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Packages", 60, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Weight", 60, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Created On", 160, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewBols.Columns.Add("Comments", 260, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewBols.ListViewItemSorter = listviewColumnSorter;
        }

        private void LvBols_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewBols.ListViewItemSorter;
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
            this.listViewBols.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewBols_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditBol.Enabled = true;

            if (_filterApplied == Filter.Reconcile)
            {
                buttonDispute.Enabled = true;
            }
            else
            {
                buttonDispute.Enabled = false;
            }
        }

        private void ListViewBols_DoubleClick(object sender, EventArgs e)
        {
            buttonEditBol.PerformClick();
            Application.DoEvents();
        }

        #endregion
        #region Load

        private async Task<int> LoadAllBolsDefault()
        {
            int bolCount = 0;
            int idx = -1;

            if (bolRepository != null)
            {
                var getBols = await bolRepository.GetAllBillOfLaddingsForTodayPaginatedAsync(DateTime.Now, _page, _show);
                if (getBols != null)
                {
                    bolCount = getBols.Count();
                    _count = bolCount;

                    listViewBols.Items.Clear();

                    foreach (BILLOFLADINGS getBol in getBols)
                    {
                        idx++;
                        var item1 = new ListViewItem(getBol.BolId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        string shipFromLocationCity = "";
                        string shipToLocationCity = "";
                        int shipPalletsCount = 0;
                        int shipPackagesCount = 0;

                        // ShipFrom Location
                        if (getBol.ShipFromLocation != null)
                        {
                            if (getBol.ShipFromLocation.City != null)
                            {
                                shipFromLocationCity = getBol.ShipFromLocation.City;
                            }
                        }

                        // ShipTo Location
                        if (getBol.ShipToLocation != null)
                        {
                            if (getBol.ShipToLocation.City != null)
                            {
                                shipToLocationCity = getBol.ShipToLocation.City;
                            }
                        }

                        // Pallets
                        if (getBol.Pallets != null)
                        {
                            shipPalletsCount = getBol.Pallets.Count;
                        }

                        // Packages
                        if (getBol.Packages != null)
                        {
                            shipPackagesCount = getBol.Packages.Count;
                        }

                        item1.SubItems.Add(getBol.ShipDate.ToLocalTime().ToString());
                        item1.SubItems.Add(getBol.OrderNumber);
                        item1.SubItems.Add(getBol.CustomerName);
                        item1.SubItems.Add(getBol.ShipperName);
                        item1.SubItems.Add(shipFromLocationCity);
                        item1.SubItems.Add(shipToLocationCity);
                        item1.SubItems.Add(getBol.ShipperQuotePrice.ToString("c"));
                        item1.SubItems.Add(getBol.ShipperActualPrice.ToString("c"));
                        item1.SubItems.Add(getBol.Disputed == true ? "?" : "");
                        item1.SubItems.Add(getBol.Printed == true ? "?" : "");
                        item1.SubItems.Add(shipPalletsCount.ToString());
                        item1.SubItems.Add(shipPackagesCount.ToString());
                        item1.SubItems.Add(getBol.BolEstimatedWeight.ToString());
                        item1.SubItems.Add(getBol.CreatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(getBol.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(getBol.Comments);

                        // Let the user know that the actual price > quote price
                        if (getBol.ShipperActualPrice < getBol.ShipperQuotePrice && getBol.ShipperActualPrice != 0)
                        {
                            item1.SubItems[7].ForeColor = Color.Green;
                            item1.SubItems[8].ForeColor = Color.Green;
                            
                        }
                        else if (getBol.ShipperActualPrice > getBol.ShipperQuotePrice && getBol.ShipperActualPrice != 0)
                        {
                            item1.SubItems[7].ForeColor = Color.Red;
                            item1.SubItems[8].ForeColor = Color.Red;
                        }
                        else
                        {
                            item1.SubItems[7].ForeColor = Color.Black;
                            item1.SubItems[8].ForeColor = Color.Gray;
                        }

                        if (getBol.Disputed == true)
                        {
                            item1.SubItems[9].ForeColor = Color.Red;
                        }

                        if (getBol.Printed == true)
                        {
                            item1.SubItems[10].ForeColor = Color.Green;
                        }

                        item1.UseItemStyleForSubItems = false;

                        listViewBols.Items.Add(item1);
                    }
                }

            }

            return bolCount;

        }

        private int LoadAnyGetBolsObject(List<BILLOFLADINGS> getBols)
        {
            int bolCount = getBols.Count(); ;
            int idx = -1;

            _count = bolCount;

            listViewBols.Items.Clear();

            foreach (BILLOFLADINGS getBol in getBols)
            {
                idx++;
                var item1 = new ListViewItem(getBol.BolId)
                {
                    Checked = false,
                    ImageIndex = idx
                };

                string shipFromLocationCity = "";
                string shipToLocationCity = "";
                int shipPalletsCount = 0;
                int shipPackagesCount = 0;

                // ShipFrom Location
                if (getBol.ShipFromLocation != null)
                {
                    if (getBol.ShipFromLocation.City != null)
                    {
                        shipFromLocationCity = getBol.ShipFromLocation.City;
                    }
                }

                // ShipTo Location
                if (getBol.ShipToLocation != null)
                {
                    if (getBol.ShipToLocation.City != null)
                    {
                        shipToLocationCity = getBol.ShipToLocation.City;
                    }
                }

                // Pallets
                if (getBol.Pallets != null)
                {
                    shipPalletsCount = getBol.Pallets.Count;
                }

                // Packages
                if (getBol.Packages != null)
                {
                    shipPackagesCount = getBol.Packages.Count;
                }

                item1.SubItems.Add(getBol.ShipDate.ToLocalTime().ToString());
                item1.SubItems.Add(getBol.OrderNumber);
                item1.SubItems.Add(getBol.CustomerName);
                item1.SubItems.Add(getBol.ShipperName);
                item1.SubItems.Add(shipFromLocationCity);
                item1.SubItems.Add(shipToLocationCity);
                item1.SubItems.Add(getBol.ShipperQuotePrice.ToString("c"));
                item1.SubItems.Add(getBol.ShipperActualPrice.ToString("c"));
                item1.SubItems.Add(getBol.Disputed == true ? "?" : "");
                item1.SubItems.Add(getBol.Printed == true ? "?" : "");
                item1.SubItems.Add(shipPalletsCount.ToString());
                item1.SubItems.Add(shipPackagesCount.ToString());
                item1.SubItems.Add(getBol.BolEstimatedWeight.ToString());
                item1.SubItems.Add(getBol.CreatedOnUtc.ToLocalTime().ToString());
                item1.SubItems.Add(getBol.UpdatedOnUtc.ToLocalTime().ToString());
                item1.SubItems.Add(getBol.Comments);

                // Let the user know that the actual price > quote price
                if (getBol.ShipperActualPrice < getBol.ShipperQuotePrice && getBol.ShipperActualPrice != 0)
                {
                    item1.SubItems[7].ForeColor = Color.Green;
                    item1.SubItems[8].ForeColor = Color.Green;
                }
                else if (getBol.ShipperActualPrice > getBol.ShipperQuotePrice && getBol.ShipperActualPrice != 0)
                {
                    item1.SubItems[7].ForeColor = Color.Red;
                    item1.SubItems[8].ForeColor = Color.Red;
                }
                else
                {
                    item1.SubItems[7].ForeColor = Color.Black;
                    item1.SubItems[8].ForeColor = Color.Gray;
                }

                if (getBol.Disputed == true)
                {
                    item1.SubItems[9].ForeColor = Color.Red;
                }

                if (getBol.Printed == true)
                {
                    item1.SubItems[10].ForeColor = Color.Green;
                }

                item1.UseItemStyleForSubItems = false;

                listViewBols.Items.Add(item1);
            }


            return bolCount;

        }

        private void AddBol(BILLOFLADINGS bol)
        {
            int idx = listViewBols.Items.Count + 1;
            var item1 = new ListViewItem(bol.BolId)
            {
                Checked = false,
                ImageIndex = idx
            };

            string shipFromLocationCity = "";
            string shipToLocationCity = "";
            int shipPalletsCount = 0;
            int shipPackagesCount = 0;

            // ShipFrom Location
            if (bol.ShipFromLocation != null)
            {
                if (bol.ShipFromLocation.City != null)
                {
                    shipFromLocationCity = bol.ShipFromLocation.City;
                }
            }

            // ShipTo Location
            if (bol.ShipToLocation != null)
            {
                if (bol.ShipToLocation.City != null)
                {
                    shipToLocationCity = bol.ShipToLocation.City;
                }
            }

            // Pallets
            if (bol.Pallets != null)
            {
                shipPalletsCount = bol.Pallets.Count;
            }

            // Packages
            if (bol.Packages != null)
            {
                shipPackagesCount = bol.Packages.Count;
            }

            item1.SubItems.Add(bol.ShipDate.ToString());
            item1.SubItems.Add(bol.OrderNumber);
            item1.SubItems.Add(bol.CustomerName);
            item1.SubItems.Add(bol.ShipperName);
            item1.SubItems.Add(shipFromLocationCity);
            item1.SubItems.Add(shipToLocationCity);
            item1.SubItems.Add(shipPalletsCount.ToString());
            item1.SubItems.Add(shipPackagesCount.ToString());
            item1.SubItems.Add(bol.BolEstimatedWeight.ToString());
            item1.SubItems.Add(bol.UpdatedOnUtc.ToLocalTime().ToString());
            item1.SubItems.Add(bol.Comments);


            // Make the Favorites a Green X if selected
            item1.SubItems[6].ForeColor = Color.Green;
            item1.UseItemStyleForSubItems = false;

            listViewBols.Items.Add(item1);

        }

        private async Task<int> LoadAllBolsByAssignedFilter()
        {
            Cursor = Cursors.WaitCursor;

            _show = (int)numericUpDownShow.Value;
            int bolCount = 0;

            if (bolRepository != null)
            {
                switch (_filterApplied)
                {
                    case Filter.None:
                        bolCount = await LoadAllBolsDefault();
                        break;

                    case Filter.Date:
                        bolCount = await this.LoadAllBolsByDateFilter();
                        break;

                    case Filter.Shipper:
                        bolCount = await this.LoadAllBolsByShipperFilter();
                        break;

                    case Filter.Vendor:
                        bolCount = await this.LoadAllBolsByVendorFilter();
                        break;

                    case Filter.Customer:
                        bolCount = await this.LoadAllBolsByCustomerFilter();
                        break;

                    case Filter.Reconcile:
                        bolCount = await this.LoadAllBolsByReconcile();
                        break;
                }

            }

            Cursor = Cursors.Default;

            return bolCount;
        }

        private async Task<int> LoadAllBolsByReconcile()
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int bolCount = 0;

            string reconcileId = (comboBoxReconcile.SelectedValue as dynamic).ToString();
            if (bolRepository != null)
            {
                var getBols = new List<BILLOFLADINGS>();
                switch (reconcileId)
                {
                    case "1":
                        getBols = await bolRepository.GetAllBillOfLaddingsAsync();
                        buttonDispute.Visible = false;
                        break;

                    case "2":
                        getBols = await bolRepository.GetAllBillOfLaddingsUpdateRateAsync(_page, _show);
                        buttonDispute.Visible = false;
                        break;

                    case "3":
                        getBols = await bolRepository.GetAllBillOfLaddingsAuditRateAsync(_page, _show);
                        buttonDispute.Visible = getBols.Count > 0;
                        break;

                    case "4":
                        getBols = await bolRepository.GetAllBillOfLaddingsFromThisWeekPaginatedAsync(_page, _show);
                        buttonDispute.Visible = false;
                        break;

                    case "5":
                        getBols = await bolRepository.GetAllBillOfLaddingsFromLastWeekPaginatedAsync(_page, _show);
                        buttonDispute.Visible = false;
                        break;
                }

                if (getBols != null)
                {
                    bolCount = LoadAnyGetBolsObject(getBols);
                    _filterApplied = Filter.Reconcile;
                    labelReconcileFilterResults.Text = FormatRecordCount(bolCount);
                }
                else
                {
                    labelReconcileFilterResults.Text = FormatRecordCount(0);
                }
            }

            EnableEventHandlers();
            Cursor = Cursors.Default;

            return bolCount;

        }

        private async Task<int> LoadAllBolsByDateFilter()
        {

            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int bolCount = 0;

            DateTime shipDate = dateTimePickerFilterByShipDate.Value;
            if (bolRepository != null)
            {
                var getBols = await bolRepository.GetAllBillOfLaddingsByShipDateAsync(shipDate);
                if (getBols != null)
                {
                    bolCount = LoadAnyGetBolsObject(getBols);
                    labelDateFilterResults.Text = FormatRecordCount(bolCount);
                }
                else
                {
                    labelDateFilterResults.Text = FormatRecordCount(0);
                }

            }

            EnableEventHandlers();
            Cursor = Cursors.Default;

            return bolCount;

        }

        private async Task LoadInitialFilterRecordCountsAsync()
        {
            if (bolRepository is null)
                return;

            var allBolsTask = bolRepository.GetAllBillOfLaddingsAsync();
            var dateBolsTask = bolRepository.GetAllBillOfLaddingsByShipDateAsync(
                dateTimePickerFilterByShipDate.Value);

            await Task.WhenAll(allBolsTask, dateBolsTask);

            var allBolCount = allBolsTask.Result.Count;
            labelDateFilterResults.Text = FormatRecordCount(dateBolsTask.Result.Count);
            labelShipperFilterResults.Text = FormatRecordCount(allBolCount);
            labelVendorFilterResults.Text = FormatRecordCount(allBolCount);
            labelCustomerFilterResults.Text = FormatRecordCount(allBolCount);
        }

        private static string FormatRecordCount(int count) =>
            $"{count} Record(s) located";

        private async Task<int> LoadAllBolsByShipperFilter()
        {

            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int bolCount = 0;

            string shipperId = (comboBoxShippers.SelectedValue as dynamic).ToString();
            if (bolRepository != null && shipperId != null)
            {
                var getBols = await bolRepository.GetAllBillOfLaddingsByShipperAsync(shipperId);
                if (getBols != null)
                {
                    bolCount = LoadAnyGetBolsObject(getBols);
                    labelShipperFilterResults.Text = FormatRecordCount(bolCount);

                }
                else
                {
                    labelShipperFilterResults.Text = FormatRecordCount(0);
                }
            }

            EnableEventHandlers();
            Cursor = Cursors.Default;

            return bolCount;

        }

        private async Task<int> LoadAllBolsByVendorFilter()
        {

            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int bolCount = 0;

            string vendorId = (comboBoxVendors.SelectedValue as dynamic).ToString();
            if (bolRepository != null && vendorId != null)
            {
                var getBols = await bolRepository.GetAllBillOfLaddingsByVendorAsync(vendorId);
                if (getBols != null)
                {
                    bolCount = LoadAnyGetBolsObject(getBols);
                    labelVendorFilterResults.Text = FormatRecordCount(bolCount);
                    _filterApplied = Filter.Vendor;

                }
                else
                {
                    labelVendorFilterResults.Text = FormatRecordCount(0);
                }
            }

            EnableEventHandlers();
            Cursor = Cursors.Default;

            return bolCount;

        }

        private async Task<int> LoadAllBolsByCustomerFilter()
        {

            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int bolCount = 0;

            string customerId = (comboBoxCustomers.SelectedValue as dynamic).ToString();
            if (bolRepository != null && customerId != null)
            {
                var getBols = await bolRepository.GetAllBillOfLaddingsByCustomerAsync(customerId);
                if (getBols != null)
                {

                    bolCount = LoadAnyGetBolsObject(getBols);
                    labelCustomerFilterResults.Text = FormatRecordCount(bolCount);
                    _filterApplied = Filter.Customer;

                }
                else
                {
                    labelCustomerFilterResults.Text = FormatRecordCount(0);
                }
            }

            Cursor = Cursors.Default;
            return bolCount;

        }

        #endregion
        #region ComboBox Filters

        private void ClearFilters()
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            comboBoxReconcile.SelectedValue = 4;
            dateTimePickerFilterByShipDate.Value = DateTime.Now;
            comboBoxShippers.SelectedIndex = 0;
            comboBoxVendors.SelectedIndex = 0;
            comboBoxCustomers.SelectedIndex = 0;

            listViewBols.Items.Clear();

            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void LoadComboBoxReconcile()
        {
            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxReconcile.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxReconcile.DataSource = currentDataTable;
            }

            var dtFilters = new DataTable("dtFilters");
            dtFilters.Columns.Add(new DataColumn("Key"));
            dtFilters.Columns.Add(new DataColumn("Value"));

            var rsDefault = dtFilters.NewRow();
            rsDefault[0] = "-- Make a selection --";
            rsDefault[1] = 0;
            dtFilters.Rows.Add(rsDefault);

            var rsShowAll = dtFilters.NewRow();
            rsShowAll[0] = "Show All";
            rsShowAll[1] = 1;
            dtFilters.Rows.Add(rsShowAll);

            var rsUpdateActualRates = dtFilters.NewRow();
            rsUpdateActualRates[0] = "Update Actual Rates";
            rsUpdateActualRates[1] = 2;
            dtFilters.Rows.Add(rsUpdateActualRates);

            var rsAuditActualRates = dtFilters.NewRow();
            rsAuditActualRates[0] = "Audit Actual Rates";
            rsAuditActualRates[1] = 3;
            dtFilters.Rows.Add(rsAuditActualRates);

            var rsThisWeek = dtFilters.NewRow();
            rsThisWeek[0] = "This Week";
            rsThisWeek[1] = 4;
            dtFilters.Rows.Add(rsThisWeek);

            var rsLastWeek = dtFilters.NewRow();
            rsLastWeek[0] = "Last Week";
            rsLastWeek[1] = 5;
            dtFilters.Rows.Add(rsLastWeek);

            

            dtFilters.AcceptChanges();

            comboBoxReconcile.DataSource = dtFilters;
            comboBoxReconcile.DisplayMember = "Key";
            comboBoxReconcile.ValueMember = "Value";
            comboBoxReconcile.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxReconcile.AutoCompleteMode = AutoCompleteMode.None;
            comboBoxReconcile.AutoCompleteSource = AutoCompleteSource.None;

            comboBoxReconcile.SelectedValue = 4;

        }

        private async Task<int> LoadComboBoxShippers()
        {
            int shippersCount = 0;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxShippers.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxShippers.DataSource = currentDataTable;
            }

            if (shipperRepository != null)
            {

                var getAllShippers = await shipperRepository.GetAllShippersAsync();
                if (getAllShippers != null)
                {
                    shippersCount = getAllShippers.Count();

                    if (shippersCount > 0)
                    {

                        var dtShippers = new DataTable("dtShippers");
                        dtShippers.Columns.Add(new DataColumn("Key"));
                        dtShippers.Columns.Add(new DataColumn("Value"));

                        var rsDefault = dtShippers.NewRow();
                        rsDefault[0] = "-- Make a selection --";
                        rsDefault[1] = 0;
                        dtShippers.Rows.Add(rsDefault);

                        foreach (var shipperItem in getAllShippers.OrderBy(ob => ob?.CompanyName))
                        {
                            if (shipperItem != null)
                            {
                                var rsShipperItem = dtShippers.NewRow();
                                rsShipperItem[0] = shipperItem.CompanyName;
                                rsShipperItem[1] = shipperItem.ShipperId;
                                dtShippers.Rows.Add(rsShipperItem);
                            }
                        }

                        dtShippers.AcceptChanges();

                        comboBoxShippers.DataSource = dtShippers;
                        comboBoxShippers.DisplayMember = "Key";
                        comboBoxShippers.ValueMember = "Value";
                        comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxShippers.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBoxShippers.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBoxShippers.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBoxShippers.SelectedIndex = 0;

                    }
                }

            }

            return shippersCount;

        }

        private async Task<int> LoadComboBoxVendors()
        {
            int vendorsCount = 0;

            if (vendorRepository != null)
            {
                // New method of clearing the ComboBox               
                DataTable currentDataTable = (DataTable)comboBoxVendors.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxVendors.DataSource = currentDataTable;
                }

                var getAllVendors = await vendorRepository.GetAllVendorsAsync();
                if (getAllVendors != null)
                {

                    foreach (var vendor in getAllVendors)
                    {
                        if (vendor != null)
                        {

                            vendorsCount = getAllVendors.Count();

                            if (vendorsCount > 0)
                            {

                                var dtVendors = new DataTable("dtVendors");
                                dtVendors.Columns.Add(new DataColumn("Key"));
                                dtVendors.Columns.Add(new DataColumn("Value"));

                                var rsDefault = dtVendors.NewRow();
                                rsDefault[0] = "-- Make a selection --";
                                rsDefault[1] = 0;
                                dtVendors.Rows.Add(rsDefault);

                                foreach (var vendorItem in getAllVendors.OrderBy(ob => ob?.CompanyName))
                                {
                                    if (vendorItem != null)
                                    {
                                        var rsVendorItem = dtVendors.NewRow();
                                        rsVendorItem[0] = vendorItem.CompanyName;
                                        rsVendorItem[1] = vendorItem.VendorId;
                                        dtVendors.Rows.Add(rsVendorItem);
                                    }
                                }

                                dtVendors.AcceptChanges();

                                comboBoxVendors.DataSource = dtVendors;
                                comboBoxVendors.DisplayMember = "Key";
                                comboBoxVendors.ValueMember = "Value";
                                comboBoxVendors.AutoCompleteSource = AutoCompleteSource.ListItems;
                                comboBoxVendors.DropDownStyle = ComboBoxStyle.DropDown;
                                comboBoxVendors.AutoCompleteMode = AutoCompleteMode.Suggest;
                                comboBoxVendors.AutoCompleteSource = AutoCompleteSource.ListItems;

                                // Check if have vendors first
                                if (comboBoxVendors.Items.Count > 0)
                                {
                                    comboBoxVendors.SelectedIndex = 0;
                                }

                            }

                        }

                    }

                }

            }

            return vendorsCount;

        }

        private async Task<int> LoadComboBoxCustomers()
        {
            int customersCount = 0;

            if (customerRepository != null)
            {
                // New method of clearing the ComboBox               
                DataTable currentDataTable = (DataTable)comboBoxCustomers.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxCustomers.DataSource = currentDataTable;
                }

                var getAllCustomers = await customerRepository.GetAllCustomersAsync();
                if (getAllCustomers != null)
                {

                    foreach (var customer in getAllCustomers)
                    {
                        if (customer != null)
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
                                        var rsCustomerItem = dtCustomers.NewRow();
                                        rsCustomerItem[0] = customerItem.CompanyName;
                                        rsCustomerItem[1] = customerItem.CustomerId;
                                        dtCustomers.Rows.Add(rsCustomerItem);
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

                }

            }

            return customersCount;

        }

        private async void ComboBoxReconcile_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _count = await this.LoadAllBolsByReconcile();
        }

        private async void DateTimePickerFilterByShipDate_ValueChanged(object? sender, EventArgs e)
        {
            _count = await this.LoadAllBolsByDateFilter();
        }

        private async void ComboBoxShippers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _count = await this.LoadAllBolsByShipperFilter();
        }

        private async void ComboBoxVendors_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _count = await this.LoadAllBolsByVendorFilter();
        }

        private async void ComboBoxCustomers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _count = await this.LoadAllBolsByCustomerFilter();
        }

        #endregion
        #region Paginate

        private async void NumericUpDownShow_ValueChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            _show = (int)numericUpDownShow.Value;

            if (bolRepository != null)
            {
                switch (_filterApplied)
                {
                    case Filter.None:
                        _count = await LoadAllBolsDefault();
                        break;

                    case Filter.Date:
                        _count = await this.LoadAllBolsByDateFilter();
                        break;

                    case Filter.Shipper:
                        _count = await this.LoadAllBolsByShipperFilter();
                        break;

                    case Filter.Vendor:
                        _count = await this.LoadAllBolsByVendorFilter();
                        break;

                    case Filter.Customer:
                        _count = await this.LoadAllBolsByCustomerFilter();
                        break;

                    case Filter.Reconcile:
                        _count = await this.LoadAllBolsByReconcile();
                        break;
                }

            }

            Cursor = Cursors.Default;
        }

        #endregion

        
    }

    enum Filter
    {
        None = 0,
        Date = 1,
        Shipper = 2,
        Vendor = 3,
        Customer = 4,
        Reconcile = 5
    }
}
