using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System.Data;

namespace SimpleBol.WinForms.Forms
{
    public partial class BillingDisputesForm : Form
    {
        private long _pages = 0;
        private int _page = 1;
        private int _show = 50;
        private long _count = 0;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IBillingDisputesRepository? billingDisputesRepository;
        private readonly IShipperRepository? shipperRepository;

        public BillingDisputesForm(
            IServiceScopeFactory serviceProvider,
            IBillingDisputesRepository billingDisputesRepository,
            IShipperRepository shipperRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.billingDisputesRepository = billingDisputesRepository;
            this.shipperRepository = shipperRepository;
        }

        #region Form

        protected async void BillingDisputesForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            _ = await LoadComboBoxShippers();
            SetlistViewDisputes();

            _count = await LoadAllDisputesAsync();

        }

        protected void BillingDisputesForm_Shown(object sender, EventArgs e)
        {

            EnableEventHandlers();
            Cursor = Cursors.Default;
        }

        #endregion
        #region Load

        private async Task<int> LoadAllDisputesAsync()
        {
            int disputeCount = 0;

            if (billingDisputesRepository != null)
            {

                var getDisputes = await billingDisputesRepository.GetAllBillingDisputesAsync();
                if (getDisputes != null)
                {
                    disputeCount = getDisputes.Count;
                    LoadListViewByAsyncObject(getDisputes);
                }

            }

            return disputeCount;

        }

        private async Task<int> LoaddAllDisputesByShipperIdAsync(string shipperId)
        {
            int disputeCount = 0;

            if (billingDisputesRepository != null)
            {

                var getDisputes = await billingDisputesRepository.GetAllBillingDisputesByShipperAsync(shipperId);
                if (getDisputes != null)
                {
                    disputeCount = getDisputes.Count;
                    LoadListViewByAsyncObject(getDisputes);
                }

            }

            return disputeCount;

        }

        private void LoadListViewByAsyncObject(List<BILLINGDISPUTES> getDisputes)
        {

            var idx = -1;
            _count = getDisputes.Count;

            listViewDisputes.Visible = true;
            listViewDisputes.Items.Clear();

            // Calculate the pages
            _pages = (_count + _show - 1) / _show;

            Application.DoEvents();

            foreach (var dispute in getDisputes.OrderBy(order => order.CreatedOnUtc))
            {
                if (dispute != null)
                {
                    idx++;
                    var item1 = new ListViewItem(dispute.BillingDisputeId)
                    {
                        Checked = false,
                        ImageIndex = idx
                    };

                    item1.SubItems.Add(dispute.Name);
                    item1.SubItems.Add(dispute.DisputeDate.ToLocalTime().ToString());
                    item1.SubItems.Add(dispute.ShipperName);
                    item1.SubItems.Add(dispute.Subject);
                    item1.SubItems.Add(dispute.ShipperReferenceNumber);
                    item1.SubItems.Add(dispute.QuotedPrice.ToString("c"));
                    item1.SubItems.Add(dispute.ActualPrice.ToString("c"));
                    item1.SubItems.Add(dispute.Resolved == true ? "?" : "");
                    item1.SubItems.Add(dispute.UpdatedOnUtc.ToLocalTime().ToString());
                    item1.SubItems.Add(dispute.Remarks);

                    // Make the Favorites a Green X if selected
                    item1.SubItems[8].ForeColor = Color.Green;
                    item1.UseItemStyleForSubItems = false;

                    listViewDisputes.Items.Add(item1);

                }

            }

        }

        #endregion
        #region Buttons

        private async void ButtonCreateDispute_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && billingDisputesRepository != null)
            {
                using var billingDisputesDialogDIOwned = serviceProvider.CreateOwnedForm<BillingDisputeDialog>();
                var billingDisputesDialogDI = billingDisputesDialogDIOwned.Form;
                billingDisputesDialogDI.StartPosition = FormStartPosition.CenterScreen;
                billingDisputesDialogDI.DisputeId = "ADD";
                if (billingDisputesDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var dispute = billingDisputesDialogDI.Dispute;
                    if (dispute != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllDisputesAsync();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditDispute_Click(object sender, EventArgs e)
        {
            if (listViewDisputes.SelectedItems.Count == 1)
            {
                var listItem = listViewDisputes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && billingDisputesRepository != null)
                {

                    using var billingDisputesDialogDIOwned = serviceProvider.CreateOwnedForm<BillingDisputeDialog>();
                var billingDisputesDialogDI = billingDisputesDialogDIOwned.Form;
                    billingDisputesDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    billingDisputesDialogDI.DisputeId = objectId;
                    if (billingDisputesDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var dispute = billingDisputesDialogDI.Dispute;
                        if (dispute != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllDisputesAsync();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a dispute selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();

        }

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

        #endregion
        #region ListViews

        private void SetlistViewDisputes()
        {
            listViewDisputes.Visible = true;
            listViewDisputes.Items.Clear();
            listViewDisputes.Columns.Clear();

            // Set ListView Parameters
            listViewDisputes.Cursor = Cursors.Hand;
            listViewDisputes.View = View.Details;
            listViewDisputes.LabelEdit = false;
            listViewDisputes.AllowColumnReorder = true;
            listViewDisputes.CheckBoxes = false;
            listViewDisputes.FullRowSelect = true;
            listViewDisputes.GridLines = true;
            listViewDisputes.Scrollable = true;
            listViewDisputes.MultiSelect = false;
            listViewDisputes.OwnerDraw = false;
            listViewDisputes.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewDisputes.Columns.Add("DisputeId", 0, HorizontalAlignment.Center);
            listViewDisputes.Columns.Add("Name", 260, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Date", 150, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Shipper", 260, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Subject", 150, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Reference", 150, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Quoted Price", 150, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("ActualPrice", 150, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Resolved", 60, HorizontalAlignment.Center);
            listViewDisputes.Columns.Add("Updated On", 160, HorizontalAlignment.Left);
            listViewDisputes.Columns.Add("Comments", 360, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            this.listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 2
            };

            this.listViewDisputes.ListViewItemSorter = this.listviewColumnSorter;

        }

        private void ListViewDisputes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewDisputes.ListViewItemSorter;
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
            this.listViewDisputes.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewDisputes_DoubleClick(object sender, EventArgs e)
        {
            buttonEditDispute.PerformClick();
        }


        private void ListViewDisputes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            buttonEditDispute.Enabled = true;
        }


        #endregion
        #region EventHandlers

        private void EnableEventHandlers()
        {
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            listViewDisputes.SelectedIndexChanged += ListViewDisputes_SelectedIndexChanged;

        }

        private void DisableEventHandlers()
        {
            comboBoxShippers.SelectedIndexChanged -= ComboBoxShippers_SelectedIndexChanged;
            listViewDisputes.SelectedIndexChanged -= ListViewDisputes_SelectedIndexChanged;
        }

        #endregion
        #region ComboBoxes

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
                        rsDefault[0] = "-- Show All --";
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

        private async void ComboBoxShippers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            int disputeCount = 0;

            if (billingDisputesRepository != null) {

                string shipperId = (comboBoxShippers.SelectedValue as dynamic).ToString();
                if (shipperId != "0")
                {
                    var getDisputes = await billingDisputesRepository.GetAllBillingDisputesByShipperAsync(shipperId);
                    if (getDisputes != null)
                    {
                        disputeCount = getDisputes.Count;
                        LoadListViewByAsyncObject(getDisputes);
                        labelShipperFilterResults.Text = disputeCount.ToString() + " Result(s) located";
                    }
                    else
                    {
                        labelShipperFilterResults.Text = "0 Result(s) located";
                    }
                }
                else if (shipperId == "0")
                {
                    var getDisputes = await billingDisputesRepository.GetAllBillingDisputesAsync();
                    if (getDisputes != null)
                    {
                        disputeCount = getDisputes.Count;
                        LoadListViewByAsyncObject(getDisputes);
                        labelShipperFilterResults.Text = disputeCount.ToString() + " Result(s) located";
                    }
                    else
                    {
                        labelShipperFilterResults.Text = "0 Result(s) located";
                    }

                }

            }

            

            EnableEventHandlers();
            Cursor = Cursors.Default;
        }

        #endregion




    }
}
