using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.Data;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class NmfcFreightCodeDialog : Form
    {
        public string NmfcCodeId { get; set; } = null!;
        public NMFCCODES NmfcCode { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly INmfcCodesRepository? nmfcCodesRepository;
        private readonly IFreightClassCodesRepository? freightClassCodesRepository;

        public NmfcFreightCodeDialog(
            IServiceScopeFactory serviceProvider,
            INmfcCodesRepository nmfcCodesRepository,
            IFreightClassCodesRepository? freightClassCodesRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.nmfcCodesRepository = nmfcCodesRepository;
            this.freightClassCodesRepository = freightClassCodesRepository;
        }

        #region Dialog

        protected async void NmfcFreightCodeDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            _ = await LoadFreightClassCodes();

            if (this.NmfcCodeId != "ADD")
            {
                Load_NmfcFreightCode(this.NmfcCodeId);
            }
            else
            {
                this.NmfcCode = new NMFCCODES();
            }

        }

        protected void NmfcFreightCodeDialog_Shown(object sender, EventArgs e)
        {
            this.textBoxName.Focus();

            Cursor = Cursors.Default;
        }

        #endregion
        #region LoadSave

        private async void Load_NmfcFreightCode(string nmfcCodeId)
        {
            if (nmfcCodesRepository != null)
            {
                var getNmfcCode = await nmfcCodesRepository.GetOneNmfcCodeAsync(nmfcCodeId);
                if (getNmfcCode != null)
                {
                    this.NmfcCode = getNmfcCode;
                    textBoxName.Text = getNmfcCode.Name;
                    textBoxDescription.Text = getNmfcCode.Description;
                    textBoxCodeNumber.Text = getNmfcCode.CodeNumber.ToString();
                    checkBoxEnabled.Checked = getNmfcCode.Enabled == true ? true : false;
                    checkBoxMarkedAsDeleted.Checked = getNmfcCode.IsDeleted == true ? true : false;
                    textBoxComments.Text = getNmfcCode.Comment;

                    // Work a little harder to select the Freight Class Code
                    comboBoxFreightClassCodes.SelectedValue = getNmfcCode.FreightClass;
                    if (comboBoxFreightClassCodes.SelectedIndex < 0)
                    {
                        // See if we can find it by string
                        comboBoxFreightClassCodes.SelectedIndex = comboBoxFreightClassCodes.FindString(getNmfcCode.FreightClass.ToString());
                    }

                }

            }

        }

        private async Task<bool> SaveNmfcFreightCode()
        {
            bool vFlag = true;

            if (this.NmfcCode == null) { this.NmfcCode = new NMFCCODES(); }

            // Nmfc Code Name
            if (textBoxName.Text.Length == 0)
            {
                vFlag = false;
                textBoxName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxName.BackColor = System.Drawing.SystemColors.Window;
            }

            // Nmfc Code Description
            if (textBoxDescription.Text.Length == 0)
            {
                vFlag = false;
                textBoxDescription.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
            }

            // Nmfc Code Number
            if (textBoxCodeNumber.Text.Length == 0)
            {
                vFlag = false;
                textBoxCodeNumber.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxCodeNumber.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxCodeNumber.Text = roundedValue.ToString();
                    textBoxCodeNumber.BackColor = System.Drawing.SystemColors.Window;
                    this.NmfcCode.CodeNumber = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxCodeNumber.BackColor = Color.LightSalmon;
                }
            }

            // Nmfc Freight Class
            if (comboBoxFreightClassCodes.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxFreightClassCodes.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxFreightClassCodes.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {
                this.NmfcCode.Name = textBoxName.Text.Trim();
                this.NmfcCode.Description = textBoxDescription.Text.Trim();
                this.NmfcCode.Comment = textBoxComments.Text.Trim();

                // Convert the Code Number to a double
                bool result = double.TryParse(textBoxCodeNumber.Text, out double codeNumber);
                if (result) { this.NmfcCode.CodeNumber = codeNumber; }

                this.NmfcCode.Enabled = checkBoxEnabled.Checked;
                this.NmfcCode.IsDeleted = checkBoxMarkedAsDeleted.Checked;
                if (double.TryParse(comboBoxFreightClassCodes.SelectedValue?.ToString(),
                    out var freightClass))
                {
                    this.NmfcCode.FreightClass = freightClass;
                }
                this.NmfcCode.UpdatedOnUtc = DateTime.UtcNow;

                if (nmfcCodesRepository != null)
                {
                    if (this.NmfcCodeId == "ADD")
                    {
                        this.NmfcCode.CreatedOnUtc = DateTime.UtcNow;
                        await nmfcCodesRepository.AddNmfcCodeAsync(this.NmfcCode);
                    }
                    else
                    {
                        await nmfcCodesRepository.UpdateNmfcCodeAsync(this.NmfcCode, this.NmfcCodeId);
                    }
                }

            }

            return vFlag;
        }

        #endregion
        #region ComboBoxes

        private async Task<int> LoadFreightClassCodes()
        {
            int codeCount = 0;

            if (freightClassCodesRepository != null)
            {

                var classCodes = await freightClassCodesRepository.GetAvailableFreightClassCodesAsync();
                if (classCodes != null)
                {
                    codeCount = classCodes.Count;

                    comboBoxFreightClassCodes.Items.Clear();
                    var dtClassCodes = new DataTable("dtClassCodes");
                    dtClassCodes.Columns.Add(new DataColumn("Key"));
                    dtClassCodes.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtClassCodes.NewRow();
                    rsDefault[0] = "-- Select --";
                    rsDefault[1] = "0";
                    dtClassCodes.Rows.Add(rsDefault);

                    foreach (var classCode in classCodes)
                    {
                        var rsClassCodeItem = dtClassCodes.NewRow();
                        rsClassCodeItem[0] = classCode.Name + " - " + classCode.Description;
                        rsClassCodeItem[1] = classCode.CodeNumber;
                        dtClassCodes.Rows.Add(rsClassCodeItem);

                    }

                    dtClassCodes.AcceptChanges();

                    comboBoxFreightClassCodes.DataSource = dtClassCodes;
                    comboBoxFreightClassCodes.DisplayMember = "Key";
                    comboBoxFreightClassCodes.ValueMember = "Value";
                    comboBoxFreightClassCodes.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxFreightClassCodes.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxFreightClassCodes.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxFreightClassCodes.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxFreightClassCodes.SelectedIndex = 0;

                }

            }

            return codeCount;
        }


        #endregion
        #region TextBoxes

        private void TextBoxCodeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
        #region Buttons

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveNmfcFreightCode();
            Cursor = Cursors.Default;

            if (validate)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region CheckBoxes

        private void CheckBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBoxMarkedAsDeleted_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
