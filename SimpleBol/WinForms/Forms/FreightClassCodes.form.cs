using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System.Data;


namespace SimpleBol.WinForms.Forms
{
    public partial class FreightClassCodesForm : Form
    {

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IFreightClassCodesRepository? freightClassCodesRepository;

        public FreightClassCodesForm(
            IServiceScopeFactory serviceProvider,
            IFreightClassCodesRepository freightClassCodesRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.freightClassCodesRepository = freightClassCodesRepository;
        }

        #region Form

        protected async void FreightClassCodesForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SetListViewFreightClassCodes();

            int codesCount = await LoadAllFreightClassCodes();
            if (codesCount == 0) { buttonEditClassCode.Enabled = false; }

        }

        protected void FreightClassCodesForm_Shown(object sender, EventArgs e)
        {
            // Reset the Cursor here
            Cursor = Cursors.Default;
        }

        #endregion
        #region LoadSave

        private async Task<int> LoadAllFreightClassCodes()
        {
            int codesCount = 0;

            if (freightClassCodesRepository != null)
            {
                var getFreightClassCodes = await freightClassCodesRepository.GetAllFreightClassCodesAsync();
                if (getFreightClassCodes != null)
                {
                    var idx = -1;

                    codesCount = getFreightClassCodes.Count;

                    listViewCodes.Visible = true;
                    listViewCodes.Items.Clear();

                    Application.DoEvents();

                    foreach (var code in getFreightClassCodes.OrderBy(order => order.CodeNumber))
                    {
                        idx++;
                        var item1 = new ListViewItem(code.FreightClassCodeId)
                        {
                            Checked = false,
                            ImageIndex = idx
                        };

                        item1.SubItems.Add(code.CodeNumber.ToString());
                        item1.SubItems.Add(code.Name);
                        item1.SubItems.Add(code.Enabled == true ? "?" : "");
                        item1.SubItems.Add(code.Description);
                        item1.SubItems.Add(code.MinWeightPerfoot.ToString());
                        item1.SubItems.Add(code.MaxWeightPerFoot.ToString());
                        item1.SubItems.Add(code.UpdatedOnUtc.ToLocalTime().ToString());
                        item1.SubItems.Add(code.IsDeleted == true ? "?" : "");
                        item1.SubItems.Add(code.Comment);

                        item1.SubItems[3].ForeColor = Color.Green;
                        item1.SubItems[8].ForeColor = Color.Red;
                        item1.UseItemStyleForSubItems = false;

                        listViewCodes.Items.Add(item1);

                    }

                }
            }

            return codesCount;

        }

        #endregion
        #region ListView

        private void SetListViewFreightClassCodes()
        {
            listViewCodes.Visible = true;
            listViewCodes.Items.Clear();
            listViewCodes.Columns.Clear();

            // Set ListView Parameters
            listViewCodes.Cursor = Cursors.Hand;
            listViewCodes.View = View.Details;
            listViewCodes.LabelEdit = false;
            listViewCodes.AllowColumnReorder = true;
            listViewCodes.CheckBoxes = false;
            listViewCodes.FullRowSelect = true;
            listViewCodes.GridLines = true;
            listViewCodes.Scrollable = true;
            listViewCodes.MultiSelect = false;
            listViewCodes.OwnerDraw = false;
            listViewCodes.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewCodes.Columns.Add("FreightClassCodeId", 0, HorizontalAlignment.Center);
            listViewCodes.Columns.Add("Code Number", 140, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Name", 80, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Enabled", 60, HorizontalAlignment.Center);
            listViewCodes.Columns.Add("Description", 460, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Min", 60, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Max", 60, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Updated On", 260, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Deleted", 60, HorizontalAlignment.Center);
            listViewCodes.Columns.Add("Comments", 500, HorizontalAlignment.Left);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 1
            };

            this.listViewCodes.ListViewItemSorter = listviewColumnSorter;

        }

        private void ListViewCodes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewCodes.ListViewItemSorter;
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
            this.listViewCodes.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private void ListViewCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditClassCode.Enabled = true;
            buttonEnableCode.Enabled = true;
            buttonDisableCode.Enabled = true;
        }

        private void ListViewCodes_DoubleClick(object sender, EventArgs e)
        {
            buttonEditClassCode.PerformClick();
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

        private async void ButtonCreateClassCode_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && freightClassCodesRepository != null)
            {
                using var freightClassCodeDialogDIOwned = serviceProvider.CreateOwnedForm<FreightClassCodeDialog>();
                var freightClassCodeDialogDI = freightClassCodeDialogDIOwned.Form;
                freightClassCodeDialogDI.StartPosition = FormStartPosition.CenterScreen;
                freightClassCodeDialogDI.FreightClassCodeId = "ADD";
                if (freightClassCodeDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var freightClassCode = freightClassCodeDialogDI.FreightClassCode;
                    if (freightClassCode != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllFreightClassCodes();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditClassCode_Click(object sender, EventArgs e)
        {
            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && freightClassCodesRepository != null)
                {
                    using var freightClassCodeDialogDIOwned = serviceProvider.CreateOwnedForm<FreightClassCodeDialog>();
                var freightClassCodeDialogDI = freightClassCodeDialogDIOwned.Form;
                    freightClassCodeDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    freightClassCodeDialogDI.FreightClassCodeId = objectId;
                    if (freightClassCodeDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var freightClassCode = freightClassCodeDialogDI.FreightClassCode;
                        if (freightClassCode != null)
                        {
                            await Task.Delay(2500);
                            _ = await this.LoadAllFreightClassCodes();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Freight Class Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private async void ButtonEnableCode_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (freightClassCodesRepository != null)
                {
                    var result = await freightClassCodesRepository.EnableFreightClassCodeAsync(objectId);
                    if (result)
                    {
                        listItem.SubItems[7].Text = "?";
                        Application.DoEvents();
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Freight Class Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor = Cursors.Default;
        }

        private async void ButtonDisableCode_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;

            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (freightClassCodesRepository != null)
                {
                    var result = await freightClassCodesRepository.DisableFreightClassCodeAsync(objectId);
                    if (result)
                    {
                        listItem.SubItems[7].Text = "";
                        Application.DoEvents();
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a Freight Class Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor = Cursors.Default;
        }

        #endregion





    }
}
