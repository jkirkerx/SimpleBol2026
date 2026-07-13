using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using System.Data;

namespace SimpleBol.WinForms.Forms
{
    public partial class NmfcFreightCodesForm : Form
    {
        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly INmfcCodesRepository? nmfcCodesRepository;

        public NmfcFreightCodesForm(
            IServiceScopeFactory serviceProvider,
            INmfcCodesRepository nmfcCodesRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.nmfcCodesRepository = nmfcCodesRepository;
        }

        #region Form

        protected async void NmfcCodesForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            SetListViewFreightCodes();

            int codesCount = await LoadAllNmfcCodes();
            if (codesCount == 0) { buttonEditNmfcCode.Enabled = false; }

        }

        protected void NmfcCodesForm_Shown(object sender, EventArgs e)
        {
            // Reset the Cursor here
            Cursor = Cursors.Default;
        }

        #endregion
        #region LoadSave

        private async Task<int> LoadAllNmfcCodes()
        {
            int codesCount = 0;

            if (nmfcCodesRepository != null)
            {
                var getCodes = await nmfcCodesRepository.GetAllNmfcCodesAsync();
                if (getCodes != null)
                {
                    codesCount = LoadAnyNmfcGetCodesObject(getCodes);
                }
            }

            return codesCount;

        }

        private int LoadAnyNmfcGetCodesObject(List<NMFCCODES> getCodes)
        {
            int codesCount = 0;
            var idx = -1;

            codesCount = getCodes.Count;

            listViewCodes.Visible = true;
            listViewCodes.Items.Clear();

            Application.DoEvents();

            foreach (var code in getCodes.OrderBy(order => order.CodeNumber))
            {
                idx++;
                var item1 = new ListViewItem(code.NmfcCodeId)
                {
                    Checked = false,
                    ImageIndex = idx
                };

                item1.SubItems.Add(code.CodeNumber.ToString());
                item1.SubItems.Add(code.Name);
                item1.SubItems.Add(code.FreightClass.ToString());
                item1.SubItems.Add(code.Enabled == true ? "?" : "");
                item1.SubItems.Add(code.Description);
                item1.SubItems.Add(code.UpdatedOnUtc.ToLocalTime().ToString());
                item1.SubItems.Add(code.IsDeleted == true ? "?" : "");
                item1.SubItems.Add(code.Comment);
                item1.SubItems.Add(code.FreightClass.ToString());

                item1.SubItems[4].ForeColor = Color.Green;
                item1.SubItems[7].ForeColor = Color.Red;
                item1.UseItemStyleForSubItems = false;

                listViewCodes.Items.Add(item1);

            }

            return codesCount;

        }

        #endregion
        #region ListView

        private void SetListViewFreightCodes()
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
            listViewCodes.ColumnClick += ListViewCodesColumn_Click;

            // Create Columns and assign the column widths
            listViewCodes.Columns.Add("FreightClassCodeId", 0, HorizontalAlignment.Center);
            listViewCodes.Columns.Add("Code Number", 140, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Name", 80, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Class", 80, HorizontalAlignment.Left);
            listViewCodes.Columns.Add("Enabled", 60, HorizontalAlignment.Center);
            listViewCodes.Columns.Add("Description", 460, HorizontalAlignment.Left);
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

        private void ListViewCodesColumn_Click(object? sender, ColumnClickEventArgs e)
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

        private void ListViewCodes_DoubleClick(object sender, EventArgs e)
        {
            buttonEditNmfcCode.PerformClick();

        }

        private void ListViewCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditNmfcCode.Enabled = true;
            buttonEnableCode.Enabled = true;
            buttonDisableCode.Enabled = true;
        }

        #endregion
        #region Buttons

        private async void ButtonSearchFilterReset_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (nmfcCodesRepository != null)
            {
                var getNmfcCodes = await nmfcCodesRepository.GetAllNmfcCodesAsync();
                if (getNmfcCodes != null)
                {
                    int codeCount = LoadAnyNmfcGetCodesObject(getNmfcCodes);
                    labelSearchFilterResults.Text = codeCount.ToString() + " Result(s) were found";
                    textBoxSearchFilter.Text = "";
                }
            }

            Cursor = Cursors.Default;
        }

        private async void ButtonSearchFilter_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            string searchText = textBoxSearchFilter.Text.Trim();
            if (searchText.Length > 0)
            {
                if (nmfcCodesRepository != null)
                {
                    var getNmfcCodes = await nmfcCodesRepository.SearchForNmfcCodesInDescription(searchText);
                    if (getNmfcCodes != null)
                    {
                        int codeCount = this.LoadAnyNmfcGetCodesObject(getNmfcCodes);
                        labelSearchFilterResults.Text = codeCount.ToString() + " Result(s) found";
                    }
                }
            }

            Cursor = Cursors.Default;

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

        private async void ButtonCreateNmfcCode_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && nmfcCodesRepository != null)
            {
                using var nmfcFreightCodeDialogDIOwned = serviceProvider.CreateOwnedForm<NmfcFreightCodeDialog>();
                var nmfcFreightCodeDialogDI = nmfcFreightCodeDialogDIOwned.Form;
                nmfcFreightCodeDialogDI.StartPosition = FormStartPosition.CenterScreen;
                nmfcFreightCodeDialogDI.NmfcCodeId = "ADD";
                if (nmfcFreightCodeDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var nmfcCode = nmfcFreightCodeDialogDI.NmfcCode;
                    if (nmfcCode != null)
                    {
                        // Populate the list of shippers
                        await Task.Delay(2500);
                        _ = await LoadAllNmfcCodes();
                        Application.DoEvents();

                    }
                }

                Application.DoEvents();
            }
        }

        private async void ButtonEditNmfcCode_Click(object sender, EventArgs e)
        {
            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && nmfcCodesRepository != null)
                {
                    using var nmfcFreightCodeDialogDIOwned = serviceProvider.CreateOwnedForm<NmfcFreightCodeDialog>();
                var nmfcFreightCodeDialogDI = nmfcFreightCodeDialogDIOwned.Form;
                    nmfcFreightCodeDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    nmfcFreightCodeDialogDI.NmfcCodeId = objectId;
                    if (nmfcFreightCodeDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var nmfcFreightCode = nmfcFreightCodeDialogDI.NmfcCode;
                        if (nmfcFreightCode != null)
                        {

                            await Task.Delay(2500);
                            _ = await this.LoadAllNmfcCodes();
                            Application.DoEvents();

                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a NMFC Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private async void ButtonEnableCode_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (nmfcCodesRepository != null)
                {
                    var result = await nmfcCodesRepository.EnableNmfcFreightCodeAsync(objectId);
                    if (result)
                    {
                        listItem.SubItems[7].Text = "?";
                        Application.DoEvents();
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a NMFC Class Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Cursor = Cursors.Default;
        }

        private async void ButtonDisableCode_Click(object sender, EventArgs e)
        {
            if (listViewCodes.SelectedItems.Count == 1)
            {
                var listItem = listViewCodes.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (nmfcCodesRepository != null)
                {
                    var result = await nmfcCodesRepository.DisableNmfcFreightCodeAsync(objectId);
                    if (result)
                    {
                        listItem.SubItems[7].Text = "";
                        Application.DoEvents();
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a NMFC Class Code selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
        #region SearchFilter

        private void TextBoxSearchFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearchFilter.PerformClick();
            }
        }

        #endregion


        
    }
}
