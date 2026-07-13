using MongoDB.Bson;
using SimpleBol.Classes.Common;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.Data;
using System.Globalization;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class PalletDialog : Form
    {

        public string PalletId { get; set; } = null!;
        public PALLETS Pallet { get; set; } = null!;

        private Size _nmfcCodeSize;
        private Size _classCodeSize;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IFreightClassCodesRepository classCodesRepository;
        private readonly INmfcCodesRepository nmfcCodesRepository;

        public PalletDialog(
            IServiceScopeFactory serviceProvider,
            IFreightClassCodesRepository classCodesRepository,
            INmfcCodesRepository nfmcCodesRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.classCodesRepository = classCodesRepository;
            this.nmfcCodesRepository = nfmcCodesRepository;

        }

        #region Dialog

        private void PalletDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            LoadCurrencyCodes();
            LoadNmfcCodes();
            LoadClassCodes();
            SetComboBoxesToWindowsSystemRegion();

        }

        private void PalletDialog_Shown(object sender, EventArgs e)
        {
            if (this.PalletId != null)
            {
                if (this.PalletId != "ADD")
                {
                    LoadPallet();
                    CalculatePalletVolumeAndWeight();
                }
                else
                {
                    this.Pallet = new PALLETS();
                }
            }

            textBoxDescription.Focus();
            Cursor = Cursors.Default;
        }

        #endregion
        #region Buttons

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SavePallet();
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
        #region LoadSave

        private void LoadCurrencyCodes()
        {

            var currencyCodes = Currencies.GetCurrencyCodes();
            if (currencyCodes != null)
            {
                comboBoxCurrencyCode.Items.Clear();
                var dtCurrencies = new DataTable("dtCurrencies");
                dtCurrencies.Columns.Add(new DataColumn("Key"));
                dtCurrencies.Columns.Add(new DataColumn("Value"));

                var rsDefault = dtCurrencies.NewRow();
                rsDefault[0] = "-- Select --";
                rsDefault[1] = "0";
                dtCurrencies.Rows.Add(rsDefault);

                foreach (var currencyCode in currencyCodes)
                {
                    var rsCurrencyItem = dtCurrencies.NewRow();
                    rsCurrencyItem[0] = currencyCode.Code;
                    rsCurrencyItem[1] = currencyCode.Code;
                    dtCurrencies.Rows.Add(rsCurrencyItem);

                }

                dtCurrencies.AcceptChanges();

                comboBoxCurrencyCode.DataSource = dtCurrencies;
                comboBoxCurrencyCode.DisplayMember = "Key";
                comboBoxCurrencyCode.ValueMember = "Value";
                comboBoxCurrencyCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxCurrencyCode.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxCurrencyCode.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxCurrencyCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxCurrencyCode.SelectedIndex = 0;


            }
        }

        private async void LoadNmfcCodes()
        {
            if (nmfcCodesRepository != null)
            {

                var nmfcCodes = await nmfcCodesRepository.GetAvailableNmfcCodesAsync();
                if (nmfcCodes != null)
                {
                    comboBoxNmfcCode.Items.Clear();
                    var dtNmfcCodes = new DataTable("dtNmfcCodes");
                    dtNmfcCodes.Columns.Add(new DataColumn("Key"));
                    dtNmfcCodes.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtNmfcCodes.NewRow();
                    rsDefault[0] = "-- Select --";
                    rsDefault[1] = "0";
                    dtNmfcCodes.Rows.Add(rsDefault);

                    foreach (var nmfcCode in nmfcCodes)
                    {
                        var rsNmfcCodeItem = dtNmfcCodes.NewRow();
                        rsNmfcCodeItem[0] = nmfcCode.Name + " - " + nmfcCode.Description + " - Class " + nmfcCode.FreightClass.ToString();
                        rsNmfcCodeItem[1] = nmfcCode.NmfcCodeId;
                        dtNmfcCodes.Rows.Add(rsNmfcCodeItem);

                    }

                    dtNmfcCodes.AcceptChanges();

                    comboBoxNmfcCode.DataSource = dtNmfcCodes;
                    comboBoxNmfcCode.DisplayMember = "Key";
                    comboBoxNmfcCode.ValueMember = "Value";
                    comboBoxNmfcCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxNmfcCode.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxNmfcCode.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxNmfcCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxNmfcCode.SelectedIndex = 0;

                }

            }
        }

        private async void LoadClassCodes()
        {
            if (classCodesRepository != null)
            {

                var classCodes = await classCodesRepository.GetAvailableFreightClassCodesAsync();
                if (classCodes != null)
                {
                    comboBoxClassCode.Items.Clear();
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
                        rsClassCodeItem[1] = classCode.FreightClassCodeId;
                        dtClassCodes.Rows.Add(rsClassCodeItem);

                    }

                    dtClassCodes.AcceptChanges();

                    comboBoxClassCode.DataSource = dtClassCodes;
                    comboBoxClassCode.DisplayMember = "Key";
                    comboBoxClassCode.ValueMember = "Value";
                    comboBoxClassCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxClassCode.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxClassCode.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxClassCode.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxClassCode.SelectedIndex = 0;

                }

            }
        }

        private bool LoadPallet()
        {
            bool palletTask = true;

            if (this.Pallet != null)
            {
                DisableEventHandlers();

                comboBoxRuler.SelectedValue = this.Pallet.MeasurementCode;
                textBoxDescription.Text = this.Pallet.PalletDescription;
                textBoxLength.Text = this.Pallet.Length.ToString();
                textBoxWidth.Text = this.Pallet.Width.ToString();
                textBoxHeight.Text = this.Pallet.Height.ToString();
                textBoxWeight.Text = this.Pallet.Weight.ToString();
                textBoxCartonCount.Text = this.Pallet.BoxCount.ToString();
                textBoxItemCount.Text = this.Pallet.UnitCount.ToString();
                textBoxEstimatedValue.Text = this.Pallet.EstimatedValue.ToString();
                comboBoxCurrencyCode.SelectedValue = this.Pallet.CurrencyCode;
                textBoxRfId.Text = this.Pallet.RfCodeNumber;

                // Set the Combo Boxes - We need to wait for them to load
                comboBoxNmfcCode.SelectedValue = this.Pallet.NmfcCodeId;
                comboBoxClassCode.SelectedValue = this.Pallet.ClassCodeId;

                switch (this.Pallet.MeasurementCode)
                {
                    case "English":

                        // Post the Volume
                        labelPalletVolumeValue.Text = Math.Round(this.Pallet.Volume, MidpointRounding.AwayFromZero).ToString() + " Cubic Feet";

                        // Post the weight
                        labelBolTotalWeight.Text = this.Pallet.Weight.ToString() + " Lbs";

                        break;

                    case "Metric":

                        // Post the Volume
                        labelPalletVolumeValue.Text = Math.Round(this.Pallet.Volume, MidpointRounding.AwayFromZero).ToString() + " Cubic Meters";

                        // Post the weight
                        labelBolTotalWeight.Text = this.Pallet.Weight.ToString() + " Kgs";

                        break;

                    default:

                        // Post the Volume
                        labelPalletVolumeValue.Text = Math.Round(this.Pallet.Volume, MidpointRounding.AwayFromZero).ToString() + " Cubic Feet";

                        // Post the weight
                        labelBolTotalWeight.Text = this.Pallet.Weight.ToString() + " Lbs";

                        break;
                }

                // Stackable Pallet
                if (this.Pallet.StackablePallet > 0)
                {
                    checkBoxStackable.Checked = true;
                }                

                EnableEventHandlers();

            }

            return palletTask;

        }

        private async Task<bool> SavePallet()
        {

            bool vFlag = true;

            if (this.Pallet == null)
            {
                this.Pallet = new PALLETS();
            }

            // Pallet Description
            if (textBoxDescription.Text.Length == 0)
            {
                vFlag = false;
                textBoxDescription.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
                this.Pallet.PalletDescription = textBoxDescription.Text.Trim();
            }

            // Pallet Length
            if (textBoxLength.Text.Length == 0)
            {
                vFlag = false;
                textBoxLength.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxLength.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxLength.Text = roundedValue.ToString();
                    textBoxLength.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.Length = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxLength.BackColor = Color.LightSalmon;
                }

            }

            // Pallet Width
            if (textBoxWidth.Text.Length == 0)
            {
                vFlag = false;
                textBoxWidth.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxWidth.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxWidth.Text = roundedValue.ToString();
                    textBoxWidth.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.Width = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxWidth.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Height
            if (textBoxHeight.Text.Length == 0)
            {
                vFlag = false;
                textBoxHeight.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxHeight.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxHeight.Text = roundedValue.ToString();
                    textBoxHeight.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.Height = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxHeight.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Weight
            if (textBoxWeight.Text.Length == 0)
            {
                vFlag = false;
                textBoxWeight.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxWeight.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxWeight.Text = roundedValue.ToString();
                    textBoxWeight.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.Weight = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxWeight.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Carton Count
            if (textBoxCartonCount.Text.Length == 0)
            {
                vFlag = false;
                textBoxCartonCount.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxCartonCount.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxCartonCount.Text = roundedValue.ToString();
                    textBoxCartonCount.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.BoxCount = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxCartonCount.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Item Count
            if (textBoxItemCount.Text.Length == 0)
            {
                vFlag = false;
                textBoxItemCount.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = float.TryParse(textBoxItemCount.Text, out float value);
                if (result)
                {
                    int roundedValue = (int)Math.Round(value, MidpointRounding.AwayFromZero);
                    textBoxItemCount.Text = roundedValue.ToString();
                    textBoxItemCount.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.UnitCount = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxItemCount.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Estimated Value
            if (textBoxEstimatedValue.Text.Length == 0)
            {
                vFlag = false;
                textBoxEstimatedValue.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if currency
                bool result = float.TryParse(textBoxEstimatedValue.Text, out float value);
                if (result)
                {
                    decimal roundedValue = (decimal)Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    textBoxEstimatedValue.BackColor = System.Drawing.SystemColors.Window;
                    this.Pallet.EstimatedValue = roundedValue;

                }
                else
                {
                    vFlag = false;
                    textBoxEstimatedValue.BackColor = Color.LightSalmon;
                }
            }

            // Pallet Currency Code
            if (comboBoxCurrencyCode.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxCurrencyCode.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxCurrencyCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // Pallet Nmfc Code
            if (comboBoxNmfcCode.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxNmfcCode.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxNmfcCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // Pallet Class Code
            if (comboBoxClassCode.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxClassCode.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxClassCode.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {
                // Check for a new Pallet, or a pallet without an ObjectId
                if (this.Pallet.PalletID == null)
                {
                    this.Pallet.PalletID = ObjectId.GenerateNewId().ToString();
                }

                this.Pallet.MeasurementCode = comboBoxRuler.SelectedItem.ToString();
                this.Pallet.CurrencyCode = (comboBoxCurrencyCode.SelectedValue as dynamic).ToString();
                this.Pallet.RfCodeNumber = textBoxRfId.Text.Trim();
                this.Pallet.CreatedOnUtc = DateTime.UtcNow;
                this.Pallet.UpdatedOnUtc = DateTime.UtcNow;
                this.Pallet.PalletCode = ObjectId.GenerateNewId().ToString();

                // Calculate the Girth
                this.Pallet.Girth = this.Pallet.Length + (this.Pallet.Width * 2) + (this.Pallet.Height * 2);

                // Calculate the Volume
                switch (this.Pallet.MeasurementCode)
                {
                    case "English":
                        this.Pallet.WeightType = "LBS";
                        this.Pallet.Volume = (this.Pallet.Length / 12) * (this.Pallet.Width / 12) * (this.Pallet.Height / 12);
                        this.Pallet.VolumeType = "Cubic Feet";
                        break;

                    case "Metric":
                        this.Pallet.WeightType = "KGS";
                        this.Pallet.Volume = (this.Pallet.Length / 100) * (this.Pallet.Width / 100) * (this.Pallet.Height / 100);
                        this.Pallet.VolumeType = "Cubic Meters";
                        break;

                    default:
                        this.Pallet.WeightType = "LBS";
                        this.Pallet.Volume = (this.Pallet.Length / 12) * (this.Pallet.Width / 12) * (this.Pallet.Height / 12);
                        this.Pallet.VolumeType = "Cubic Feet";
                        break;

                }

                // Get the NmfcCode
                this.Pallet.NmfcCodeId = (comboBoxNmfcCode.SelectedValue as dynamic).ToString();
                if (nmfcCodesRepository != null)
                {
                    this.Pallet.NmfcCode = await nmfcCodesRepository.GetOneNmfcCodeAsync(this.Pallet.NmfcCodeId);
                }

                // Get the ClassCode
                this.Pallet.ClassCodeId = (comboBoxClassCode.SelectedValue as dynamic).ToString();
                if (classCodesRepository != null)
                {
                    this.Pallet.ClassCode = await classCodesRepository.GetOneFreightClassCodeAsync(this.Pallet.ClassCodeId);
                }

                // Stackable Pallet
                if (checkBoxStackable.Checked == true)
                {
                    this.Pallet.StackablePallet = 1;
                    this.Pallet.NonStackablePallet = 0;
                }
                else
                {
                    this.Pallet.StackablePallet = 0;
                    this.Pallet.NonStackablePallet = 1;
                }

            }

            return vFlag;
        }


        #endregion
        #region ComboBoxes

        private void SetComboBoxesToWindowsSystemRegion()
        {

            string countryName = RegionInfo.CurrentRegion.DisplayName;
            if (countryName == "United States")
            {
                comboBoxRuler.SelectedIndex = comboBoxRuler.FindString("English");
                comboBoxCurrencyCode.SelectedIndex = comboBoxCurrencyCode.FindString("USD");
                labelUnitOfMeasurement.Text = "LBS";
            }
            else
            {
                comboBoxRuler.SelectedIndex = comboBoxRuler.FindString("Metric");
                labelUnitOfMeasurement.Text = "KG";
            }

        }

        private void ComboBoxRuler_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var unitOfMeasurement = comboBoxRuler.SelectedItem;
            if (unitOfMeasurement != null)
            {
                if (unitOfMeasurement.ToString() == "English")
                {
                    labelUnitOfMeasurement.Text = "LBS";
                }
                else
                {
                    labelUnitOfMeasurement.Text = "KGs";
                }
            }

        }

        private async void ComboBoxNmfcCode_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string nmfcCodeId = (comboBoxNmfcCode.SelectedValue as dynamic).ToString();
            if (nmfcCodeId != null)
            {
                if (nmfcCodesRepository != null)
                {
                    var getNmfcCode = await nmfcCodesRepository.GetOneNmfcCodeAsync(nmfcCodeId);
                    if (getNmfcCode != null)
                    {
                        comboBoxClassCode.SelectedIndex = comboBoxClassCode.FindString(getNmfcCode.FreightClass.ToString());
                    }
                }
            }
        }

        private void ComboBoxNmfcCode_DropDown(object sender, EventArgs e)
        {
            _nmfcCodeSize = comboBoxNmfcCode.Size;
            Size size = _nmfcCodeSize;
            size.Width = 580;
            comboBoxNmfcCode.Size = size;
            Application.DoEvents();
        }

        private void ComboBoxNmfcCode_DropDownClosed(object sender, EventArgs e)
        {
            comboBoxNmfcCode.Width = 0;
            comboBoxNmfcCode.Size = _nmfcCodeSize;
            Application.DoEvents();
        }

        private void ComboBoxClassCode_DropDown(object sender, EventArgs e)
        {
            _classCodeSize = comboBoxClassCode.Size;
            Size size = _classCodeSize;
            size.Width = 580;
            comboBoxClassCode.Size = size;
            Application.DoEvents();
        }

        private void ComboBoxClassCode_DropDownClosed(object sender, EventArgs e)
        {
            comboBoxClassCode.Width = 0;
            comboBoxClassCode.Size = _classCodeSize;
            Application.DoEvents();
        }

        private void ComboBoxClassCode_SelectedIndexChanged(object? sender, EventArgs e)
        {

        }

        #endregion
        #region TextBoxes

        private void TextBoxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxClassCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxCartonCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxItemCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxEstimatedValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxLength_Leave(object sender, EventArgs e)
        {
            CalculatePalletVolumeAndWeight();
        }

        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            CalculatePalletVolumeAndWeight();
        }

        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            CalculatePalletVolumeAndWeight();
        }
        private void TextBoxWeight_Leave(object sender, EventArgs e)
        {
            CalculatePalletVolumeAndWeight();
        }

        #endregion
        #region Calculators

        private void CalculatePalletVolumeAndWeight()
        {
            PALLETS pallet = new PALLETS();

            // Convert length to interger
            bool result1 = float.TryParse(textBoxLength.Text, out float lengthValue);
            int roundedLengthValue = (int)Math.Round(lengthValue, MidpointRounding.AwayFromZero);
            pallet.Length = roundedLengthValue;

            // Convert width to integer
            bool result2 = float.TryParse(textBoxWidth.Text, out float widthValue);
            int roundedWidthValue = (int)Math.Round(widthValue, MidpointRounding.AwayFromZero);
            pallet.Width = roundedWidthValue;

            // Convert height to integer
            bool result3 = float.TryParse(textBoxHeight.Text, out float heightValue);
            int roundedHeightValue = (int)Math.Round(heightValue, MidpointRounding.AwayFromZero);
            pallet.Height = roundedHeightValue;

            // Convert weight to integer
            bool result4 = float.TryParse(textBoxWeight.Text, out float weightValue);
            int roundedWeightValue = (int)Math.Round(weightValue, MidpointRounding.AwayFromZero);
            pallet.Weight = roundedWeightValue;

            if (result1 && result2 && result3 && result4)
            {
                if (comboBoxRuler.SelectedItem != null)
                {
                    var measurementType = (string)comboBoxRuler.SelectedItem;

                    // Post the Density
                    var palletVolume = CalculateFreight.CalculatePalletVolume(pallet, measurementType);

                    switch (measurementType)
                    {
                        case "English":

                            // Post the Volume
                            labelPalletVolumeValue.Text = Math.Round(palletVolume, MidpointRounding.AwayFromZero).ToString() + " Cubic Feet";

                            // Post the weight
                            labelBolTotalWeight.Text = roundedWeightValue.ToString() + " Lbs";

                            break;

                        case "Metric":

                            // Post the Volume
                            labelPalletVolumeValue.Text = Math.Round(palletVolume, MidpointRounding.AwayFromZero).ToString() + " Cubic Meters";

                            // Post the weight
                            labelBolTotalWeight.Text = roundedWeightValue.ToString() + " Kgs";

                            break;

                        default:

                            // Post the Volume
                            labelPalletVolumeValue.Text = Math.Round(palletVolume, MidpointRounding.AwayFromZero).ToString() + " Cubic Feet";

                            // Post the weight
                            labelBolTotalWeight.Text = roundedWeightValue.ToString() + " Lbs";

                            break;
                    }

                }


            }

        }

        #endregion
        #region EventHandlers

        private void EnableEventHandlers()
        {

            comboBoxRuler.SelectedIndexChanged += ComboBoxRuler_SelectedIndexChanged;
            comboBoxNmfcCode.SelectedIndexChanged += ComboBoxNmfcCode_SelectedIndexChanged;
            comboBoxClassCode.SelectedIndexChanged += ComboBoxClassCode_SelectedIndexChanged;

        }

        private void DisableEventHandlers()
        {
            comboBoxRuler.SelectedIndexChanged -= ComboBoxRuler_SelectedIndexChanged;
            comboBoxNmfcCode.SelectedIndexChanged -= ComboBoxNmfcCode_SelectedIndexChanged;
            comboBoxClassCode.SelectedIndexChanged -= ComboBoxClassCode_SelectedIndexChanged;

        }

        #endregion




    }
}
