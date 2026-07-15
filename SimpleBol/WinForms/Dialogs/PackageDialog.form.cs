using MongoDB.Bson;
using SimpleBol.Classes.Common;
using SimpleBol.Models.MongoDb;
using SimpleBol.NewtonSoft;
using SimpleBol.Repository.MongoDb;
using System.Data;
using System.Globalization;
namespace SimpleBol.WinForms.Dialogs
{
    public partial class PackageDialog : Form
    {
        private const string EnglishMeasurementCode = "English";
        private const string MetricMeasurementCode = "Metric";

        public string PackageId { get; set; } = null!;
        public PACKAGES Package { get; set; } = null!;

        private Size _nmfcCodeSize;
        private Size _classCodeSize;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IFreightClassCodesRepository classCodesRepository;
        private readonly INmfcCodesRepository nmfcCodesRepository;

        public PackageDialog(
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

        private async void PackageDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            LoadCurrencyCodes();
            LoadNmfcCodes();
            LoadClassCodes();
            LoadPackageMeasurementPreference();

        }

        private void PackageDialog_Shown(object sender, EventArgs e)
        {
            if (this.PackageId != null)
            {
                if (this.PackageId != "ADD")
                {
                    LoadPackage();
                    CalculatePackageVolumeAndWeight();
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
            var validate = await SavePackage();
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
                        rsNmfcCodeItem[0] = nmfcCode.Name + " - " + nmfcCode.Description + " - Class " + nmfcCode.CodeNumber.ToString();
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

        private bool LoadPackage()
        {
            bool taskResult = true;

            if (this.Package != null)
            {
                DisableEventHandlers();

                comboBoxRuler.SelectedValue = Package.MeasurementCode;
                textBoxDescription.Text = Package.PackageDescription;
                textBoxLength.Text = Package.Length.ToString();
                textBoxWidth.Text = Package.Width.ToString();
                textBoxHeight.Text = Package.Height.ToString();
                textBoxWeight.Text = Package.Weight.ToString();

                comboBoxNmfcCode.SelectedValue = Package.NmfcCodeId;
                comboBoxClassCode.SelectedValue = Package.ClassCodeId;

                textBoxItemCount.Text = Package.UnitCount.ToString();
                textBoxEstimatedValue.Text = Package.EstimatedValue.ToString();
                comboBoxCurrencyCode.SelectedValue = Package.CurrencyCode;

                switch (Package.MeasurementCode)
                {
                    case "English":
                        labelPackageVolumeValue.Text = Package.Volume.ToString() + " Cubic Feet";
                        break;

                    case "Metric":
                        labelPackageVolumeValue.Text = Package.Volume.ToString() + " Cubic Meters";
                        break;

                    default:
                        labelPackageVolumeValue.Text = Package.Volume.ToString() + " Cubic Feet";
                        break;
                }                

                EnableEventHandlers();

            }

            return taskResult;

        }

        private async Task<bool> SavePackage()
        {

            bool vFlag = true;

            if (this.Package == null)
            {
                this.Package = new PACKAGES();
            }

            // Package Description
            if (textBoxDescription.Text.Length == 0)
            {
                vFlag = false;
                textBoxDescription.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
                this.Package.PackageDescription = textBoxDescription.Text.Trim();
            }

            // Package Length
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
                    this.Package.Length = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxLength.BackColor = Color.LightSalmon;
                }

            }

            // Package Width
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
                    this.Package.Width = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxWidth.BackColor = Color.LightSalmon;
                }
            }

            // Package Height
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
                    this.Package.Height = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxHeight.BackColor = Color.LightSalmon;
                }
            }

            // Package Weight
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
                    this.Package.Weight = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxWeight.BackColor = Color.LightSalmon;
                }
            }

            // Package Item Count
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
                    this.Package.UnitCount = roundedValue;
                }
                else
                {
                    vFlag = false;
                    textBoxItemCount.BackColor = Color.LightSalmon;
                }
            }

            // Package Estimated Value
            if (textBoxEstimatedValue.Text.Length == 0)
            {
                vFlag = false;
                textBoxEstimatedValue.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if currency
                bool result = decimal.TryParse(textBoxEstimatedValue.Text, out decimal value);
                if (result)
                {
                    decimal roundedValue = (decimal)Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    textBoxEstimatedValue.BackColor = System.Drawing.SystemColors.Window;
                    this.Package.EstimatedValue = roundedValue;

                }
                else
                {
                    vFlag = false;
                    textBoxEstimatedValue.BackColor = Color.LightSalmon;
                }
            }

            // Package Currency Code
            if (comboBoxCurrencyCode.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxCurrencyCode.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxCurrencyCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // Package Nmfc Code
            if (comboBoxNmfcCode.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxNmfcCode.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxNmfcCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // Package Class Code
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
                // Assign an objectId if this is a new Package
                if (this.Package.PackageID == null)
                {
                    this.Package.PackageID = ObjectId.GenerateNewId().ToString();
                }

                this.Package.MeasurementCode = comboBoxRuler.SelectedItem.ToString();
                this.Package.CurrencyCode = (comboBoxCurrencyCode.SelectedValue as dynamic).ToString();
                this.Package.CreatedOnUtc = DateTime.UtcNow;
                this.Package.UpdatedOnUtc = DateTime.UtcNow;
                this.Package.PackageCode = ObjectId.GenerateNewId().ToString();

                // Calculate the Package Girth and volume
                this.Package.Girth = this.Package.Length + (this.Package.Width * 2) + (this.Package.Height * 2);                

                // Caclulate the Package Volume
                switch (this.Package.MeasurementCode)
                {
                    case "English":
                        this.Package.WeightType = "LBS";
                        this.Package.Volume = (this.Package.Length / 12) * (this.Package.Width / 12) * (this.Package.Height / 12);
                        this.Package.VolumeType = "Cubic Feet";
                        break;

                    case "Metric":
                        this.Package.WeightType = "KGS";
                        this.Package.Volume = (this.Package.Length / 100) * (this.Package.Width / 100) * (this.Package.Height / 100);
                        this.Package.VolumeType = "Cubic Meters";
                        break;

                    default:
                        this.Package.WeightType = "LBS";
                        this.Package.Volume = (this.Package.Length / 12) * (this.Package.Width / 12) * (this.Package.Height / 12);
                        this.Package.VolumeType = "Cubic Feet";
                        break;
                }

                // Get the NmfcCode
                this.Package.NmfcCodeId = (comboBoxNmfcCode.SelectedValue as dynamic).ToString();
                if (nmfcCodesRepository != null)
                {
                    this.Package.NmfcCode = await nmfcCodesRepository.GetOneNmfcCodeAsync(this.Package.NmfcCodeId);
                }

                // Get the ClassCode
                this.Package.ClassCodeId = (comboBoxClassCode.SelectedValue as dynamic).ToString();
                if (classCodesRepository != null)
                {
                    this.Package.ClassCode = await classCodesRepository.GetOneFreightClassCodeAsync(this.Package.ClassCodeId);
                }

            }

            return vFlag;

        }


        #endregion
        #region ComboBoxes

        private void LoadPackageMeasurementPreference()
        {
            var savedMeasurementCode = AppSettingsJson.GetSettings()?.Settings?.PackageMeasurementCode;
            var measurementCode = string.Equals(
                savedMeasurementCode,
                MetricMeasurementCode,
                StringComparison.OrdinalIgnoreCase)
                ? MetricMeasurementCode
                : EnglishMeasurementCode;

            comboBoxRuler.SelectedItem = measurementCode;

            if (RegionInfo.CurrentRegion.DisplayName == "United States")
                comboBoxCurrencyCode.SelectedIndex = comboBoxCurrencyCode.FindString("USD");

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

        private void ComboBoxRuler_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxRuler.SelectedItem is not string measurementCode)
                return;

            labelUnitOfMeasurement.Text = measurementCode == EnglishMeasurementCode
                ? "LBS"
                : "KGs";

            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject?.Settings == null
                || rootObject.Settings.PackageMeasurementCode == measurementCode)
                return;

            rootObject.Settings.PackageMeasurementCode = measurementCode;
            AppSettingsJson.WriteSettings(rootObject);
        }

        private void ComboBoxClassCode_SelectedIndexChanged(object? sender, EventArgs e)
        {

        }

        #endregion
        #region PackageStatus

        private void PackageDialog_Leave(object sender, EventArgs e)
        {
            bool result = float.TryParse(textBoxWidth.Text, out float width);
            result = float.TryParse(textBoxHeight.Text, out float height);
            result = float.TryParse(textBoxLength.Text, out float length);
            result = float.TryParse(textBoxWeight.Text, out float weight);


            if (result)
            {

                float girth = length + (width * 2) + (height * 2);

                switch (true)
                {

                    case true when weight > 75:
                        labelPackageStatus2.Text = "Over weight package";
                        break;

                    case true when girth > 165:
                        labelPackageStatus2.Text = "Girth Exceeded";
                        break;

                    case true when length > 108:
                        labelPackageStatus2.Text = "Length too long";
                        break;

                    default:
                        labelPackageStatus2.Text = "Package acceptable";
                        break;
                }


            }
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
        #region Calculators

        private void CalculatePackageVolumeAndWeight()
        {
            PACKAGES package = new PACKAGES();            

            // Convert length to interger
            bool result1 = float.TryParse(textBoxLength.Text, out float lengthValue);
            int roundedLengthValue = (int)Math.Round(lengthValue, MidpointRounding.AwayFromZero);
            package.Length = roundedLengthValue;

            // Convert width to integer
            bool result2 = float.TryParse(textBoxWidth.Text, out float widthValue);
            int roundedWidthValue = (int)Math.Round(widthValue, MidpointRounding.AwayFromZero);
            package.Width = roundedWidthValue;

            // Convert height to integer
            bool result3 = float.TryParse(textBoxHeight.Text, out float heightValue);
            int roundedHeightValue = (int)Math.Round(heightValue, MidpointRounding.AwayFromZero);
            package.Height = roundedHeightValue;

            // Convert weight to integer
            bool result4 = float.TryParse(textBoxWeight.Text, out float weightValue);
            int roundedWeightValue = (int)Math.Round(weightValue, MidpointRounding.AwayFromZero);
            package.Weight = roundedWeightValue;

            if (result1 && result2 && result3 && result4)
            {
                if (comboBoxRuler.SelectedItem != null)
                {
                    var measurementType = (string)comboBoxRuler.SelectedItem;

                    // Post the Density
                    var packageVolume = CalculateFreight.CalculatePackageVolume(package, measurementType);

                    switch (Package.MeasurementCode)
                    {
                        case "English":

                            // Post the Volume
                            labelPackageVolumeValue.Text = Math.Round(packageVolume, MidpointRounding.AwayFromZero).ToString() + " Cubic Feet";

                            // Post the weight
                            labelBolTotalWeight.Text = roundedWeightValue.ToString() + " Lbs";

                            break;

                        case "Metric":
                            labelPackageVolumeValue.Text = Math.Round(packageVolume, MidpointRounding.AwayFromZero).ToString() + " Cubic Meters";

                            // Post the weight
                            labelBolTotalWeight.Text = roundedWeightValue.ToString() + " Kgs";
                            break;
                    }

                }                      

            }

        }

        #endregion


    }
}
