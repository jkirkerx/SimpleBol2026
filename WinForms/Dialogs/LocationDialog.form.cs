using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Bson;
using System.Globalization;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class LocationDialog : Form
    {
        public string LocationId { get; set; } = null!;
        public SHIPPINGLOCATIONS ShippingLocation { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly IVendorRepository? vendorRepository;
        private SimpleBol.Models.MongoDb.HoursOfOperation? customHoursOfOperation;

        public LocationDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            IVendorRepository vendorRespository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.vendorRepository = vendorRespository;

        }

        #region Dialog

        protected async void LocationDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            LoadComboBoxTimeZones();
            var countriesLoaded = LoadComboBoxCountriesAsync();
            await countriesLoaded;

            if (LocationId != null)
            {
                if (LocationId != "ADD")
                {
                    var locationLoaded = LoadShippingLocationAsync();
                    await locationLoaded;                    
                }
                else
                {
                    SetComboBoxesToWindowsSystemRegion();
                }
            }

            EnableEventHandlers();
        }

        protected void LocationDialog_Shown(object sender, EventArgs e)
        {

            // Put the Focus and Select down on Location Code
            textBoxCode.Focus();
            textBoxCode.Select();

            Cursor = Cursors.Default;
        }

        #endregion
        #region Events

        private void ButtonHoursOfOperation_Click(object sender, EventArgs e)
        {
            using var hoursOfOperationDialog = new HoursOfOperation
            {
                Schedule = customHoursOfOperation ?? ShippingLocation?.HoursOfOperation
            };

            if (hoursOfOperationDialog.ShowDialog(this) == DialogResult.OK)
            {
                customHoursOfOperation = hoursOfOperationDialog.Schedule;
                checkBoxNormalHours.Checked = false;
            }
        }

        private void DisableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged -= ComboBoxCountry_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
        }

        #endregion
        #region Timezones

        private void LoadComboBoxTimeZones()
        {
            var timeZones = TimeZoneInfo.GetSystemTimeZones();

            if (timeZones != null)
            {

                // New method of clearing the ComboBox, which is not working                
                DataTable currentDataTable = (DataTable)comboBoxTimeZone.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxTimeZone.DataSource = currentDataTable;
                }

                var dtTimezones = new DataTable("dtTimezones");
                dtTimezones.Columns.Add(new DataColumn("Key"));
                dtTimezones.Columns.Add(new DataColumn("Value"));

                var rsDefault = dtTimezones.NewRow();
                rsDefault[0] = "-- Make a selection --";
                rsDefault[1] = "0";
                dtTimezones.Rows.Add(rsDefault);

                foreach (var timeZone in timeZones)
                {
                    if (timeZone != null)
                    {

                        var rsTimezoneItem = dtTimezones.NewRow();
                        rsTimezoneItem[0] = timeZone.DisplayName != "" ? timeZone.DisplayName : timeZone.DaylightName;
                        rsTimezoneItem[1] = timeZone.Id;
                        dtTimezones.Rows.Add(rsTimezoneItem);

                    }

                }

                dtTimezones.AcceptChanges();

                comboBoxTimeZone.DataSource = dtTimezones;
                comboBoxTimeZone.DisplayMember = "Key";
                comboBoxTimeZone.ValueMember = "Value";
                comboBoxTimeZone.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxTimeZone.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxTimeZone.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxTimeZone.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxTimeZone.SelectedIndex = 0;

            }

        }

        #endregion
        #region Countries

        private async Task<bool> LoadComboBoxCountriesAsync()
        {
            bool success = false;

            Cursor = Cursors.WaitCursor;

            if (commonRepository != null)
            {

                var getCountries = await commonRepository.GetAllCountriesAsync();
                if (getCountries != null)
                {
                    comboBoxCountry.Items.Clear();
                    var dtCountries = new DataTable("dtCountries");
                    dtCountries.Columns.Add(new DataColumn("Key"));
                    dtCountries.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtCountries.NewRow();
                    rsDefault[0] = "-- Make a selection --";
                    rsDefault[1] = "0";
                    dtCountries.Rows.Add(rsDefault);

                    foreach (var countryItem in getCountries.OrderBy(ob => ob.LongName))
                    {
                        var rsCountryItem = dtCountries.NewRow();
                        rsCountryItem[0] = countryItem.LongName != "" ? countryItem.LongName : countryItem.ShortName;
                        rsCountryItem[1] = countryItem.CountryId;
                        dtCountries.Rows.Add(rsCountryItem);
                    }

                    dtCountries.AcceptChanges();

                    comboBoxCountry.DataSource = dtCountries;
                    comboBoxCountry.DisplayMember = "Key";
                    comboBoxCountry.ValueMember = "Value";
                    comboBoxCountry.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxCountry.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxCountry.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxCountry.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxCountry.SelectedIndex = 0;

                    // Set the Task Flag
                    success = true;

                }

            }

            Cursor = Cursors.Default;
            return success;

        }

        private void ComboBoxCountry_SelectedIndexChanged([AllowNull] object sender, [AllowNull] EventArgs e)
        {

            var countryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
            if (countryCode != null)
            {
                LoadComboBoxRegionsAsync(countryCode);

            }

        }

        private async Task<int> LoadComboBoxRegionsAsync(string countryId)
        {
            int regionsCount = 0;

            if (commonRepository != null)
            {

                // New method of clearing the ComboBox, which is not working                
                DataTable currentDataTable = (DataTable)comboBoxRegion.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxRegion.DataSource = currentDataTable;
                }

                var getRegions = await commonRepository.GetAllRegionsByCountryAsync(countryId);
                if (getRegions != null)
                {

                    regionsCount = getRegions.Count;

                    var dtRegions = new DataTable("dtRegions");
                    dtRegions.Columns.Add(new DataColumn("Key"));
                    dtRegions.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtRegions.NewRow();
                    rsDefault[0] = "-- Make a selection --";
                    rsDefault[1] = "0";
                    dtRegions.Rows.Add(rsDefault);

                    foreach (var regionItem in getRegions.OrderBy(ob => ob.LongName))
                    {
                        var rsRegionItem = dtRegions.NewRow();
                        rsRegionItem[0] = regionItem.LongName != "" ? regionItem.LongName : regionItem.ShortName;
                        rsRegionItem[1] = regionItem.RegionId;
                        dtRegions.Rows.Add(rsRegionItem);
                    }

                    comboBoxRegion.DataSource = dtRegions;
                    comboBoxRegion.DisplayMember = "Key";
                    comboBoxRegion.ValueMember = "Value";
                    comboBoxRegion.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxRegion.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxRegion.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxRegion.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxRegion.SelectedIndex = 0;

                }


            }

            return regionsCount;

        }

        private void ComboBoxRegion_SelectedIndexChanged()
        {
            var regionCode = (comboBoxRegion.SelectedValue as dynamic).ToString();
            if (regionCode != null)
            {
                if (regionCode != 0)
                {

                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(regionCode);
                }
            }

        }

        #endregion
        #region Buttons

        private void OK_Button_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            var validate = SaveShippingLocation();
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

        private async Task<bool> LoadShippingLocationAsync()
        {
            bool success = false;

            if (this.ShippingLocation != null)
            {
                DisableEventHandlers();

                textBoxCode.Text = ShippingLocation.LocationCode;
                textBoxName.Text = ShippingLocation.Name;
                textBoxAddress1.Text = ShippingLocation.Address1;
                textBoxAddress2.Text = ShippingLocation.Address2;
                textBoxCity.Text = ShippingLocation.City;
                maskedTextBoxPostalCode.Text = ShippingLocation.PostalCode;
                textBoxComments.Text = this.ShippingLocation.Comment;
                checkBoxLiftgateRequired.Checked = ShippingLocation.LiftGateRequired == null ? false : true;
                checkBoxPalletJackRequired.Checked = ShippingLocation.PalletJackRequired == null ? false : true;
                customHoursOfOperation = ShippingLocation.HoursOfOperation;
                checkBoxNormalHours.Checked = IsNormalHours(customHoursOfOperation);
                textBoxContactName.Text = ShippingLocation.ContactName;
                maskedTextBoxContactPhone.Text = ShippingLocation.ContactPhone;
                maskedTextBoxContactMobilePhone.Text = ShippingLocation.MobilePhone;
                textBoxContactEmailAddress.Text = ShippingLocation.ContactEmailAddress;

                // Make sure the RegionCode Polulates after the CountryCode Code is set
                var countryCode = ShippingLocation.CountryCode;
                if (countryCode != null)
                {

                    // Wait for the regions to load
                    var regions = LoadComboBoxRegionsAsync(countryCode);
                    await regions;

                    // Now set the country and region
                    comboBoxCountry.SelectedValue = countryCode;
                    comboBoxRegion.SelectedValue = ShippingLocation?.RegionCode;

                }

                // Select the stored timeZoneCode
                var timeZoneCode = ShippingLocation?.TimeZoneCode;
                if (timeZoneCode != null)
                {
                    comboBoxTimeZone.SelectedValue = timeZoneCode;
                }

                EnableEventHandlers();

                success = true;
            }

            return success;

        }

        private bool SaveShippingLocation()
        {
            bool vFlag = true;

            // Location Code
            if (textBoxCode.Text.Length == 0)
            {
                vFlag = false;
                textBoxCode.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // Location Name
            if (textBoxName.Text.Length == 0)
            {
                vFlag = false;
                textBoxName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxName.BackColor = System.Drawing.SystemColors.Window;
            }

            // Address 1
            if (textBoxAddress1.Text.Length == 0)
            {
                vFlag = false;
                textBoxAddress1.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxAddress1.BackColor = System.Drawing.SystemColors.Window;
            }

            // City
            if (textBoxCity.Text.Length == 0)
            {
                vFlag = false;
                textBoxCity.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxCity.BackColor = System.Drawing.SystemColors.Window;
            }

            // CountryCode
            if (comboBoxCountry.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxCountry.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxCountry.BackColor = System.Drawing.SystemColors.Window;
            }

            // RegionCode
            if (comboBoxRegion.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxRegion.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxRegion.BackColor = System.Drawing.SystemColors.Window;
            }

            // PostalCode
            if (maskedTextBoxPostalCode.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxPostalCode.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxPostalCode.BackColor = System.Drawing.SystemColors.Window;
            }

            // TimeZoneCode


            // Contact Name
            if (textBoxContactName.Text.Length == 0)
            {
                vFlag = false;
                textBoxContactName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxContactName.BackColor = System.Drawing.SystemColors.Window;
            }

            // Contact Phone Number
            if (maskedTextBoxContactPhone.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxContactPhone.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxContactPhone.BackColor = System.Drawing.SystemColors.Window;
            }

            // Contact EmailAddress
            if (textBoxContactEmailAddress.Text.Length == 0)
            {
                vFlag = false;
                textBoxContactEmailAddress.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxContactEmailAddress.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {

                string countryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
                string countryName = comboBoxCountry.Text;
                string regionCode = (comboBoxRegion.SelectedValue as dynamic).ToString();
                string regionName = comboBoxRegion.Text;
                string timeZoneCode = (comboBoxTimeZone.SelectedValue as dynamic).ToString();
                string timeZoneName = comboBoxTimeZone.Text;

                // Trim the hyphen off the Postal Code if 5 chars
                if (maskedTextBoxPostalCode.Text != null) { if (maskedTextBoxPostalCode.Text.Length == 6) { maskedTextBoxPostalCode.Text = maskedTextBoxPostalCode.Text[..5]; } }


                if (LocationId == "ADD")
                {
                    // Add a record
                    string locationId = ObjectId.GenerateNewId().ToString();
                    var newLocation = new SHIPPINGLOCATIONS
                    {
                        LocationId = locationId,
                        LocationCode = textBoxCode.Text,
                        Type = "Warehouse",
                        Name = textBoxName.Text,
                        Address1 = textBoxAddress1.Text,
                        Address2 = textBoxAddress2.Text,
                        City = textBoxCity.Text,
                        RegionCode = regionCode,
                        RegionName = regionName,
                        CountryCode = countryCode,
                        CountryName = countryName,
                        PostalCode = maskedTextBoxPostalCode.Text,
                        TimeZoneCode = timeZoneCode,
                        TimeZoneName = timeZoneName,
                        ContactName = textBoxContactName.Text,
                        ContactEmailAddress = textBoxContactEmailAddress.Text.ToLower(),
                        ContactPhone = maskedTextBoxContactPhone.Text,
                        MobilePhone = maskedTextBoxContactMobilePhone.Text,
                        HoursOfOperation = GetHoursOfOperationForSave(timeZoneCode),
                        Comment = textBoxComments.Text,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow
                    };

                    this.ShippingLocation = newLocation;

                    // We are sending this object back to the calling form, and not doing a database write
                    // Let the calling form add this object to the calling form object

                }
                else
                {
                    // Update a record
                    if (this.ShippingLocation != null)
                    {
                        ShippingLocation.LocationCode = textBoxCode.Text;
                        ShippingLocation.Type = "Warehouse";
                        ShippingLocation.Name = textBoxName.Text;
                        ShippingLocation.Address1 = textBoxAddress1.Text;
                        ShippingLocation.Address2 = textBoxAddress2.Text;
                        ShippingLocation.City = textBoxCity.Text;
                        ShippingLocation.CountryCode = countryCode;
                        ShippingLocation.CountryName = countryName;
                        ShippingLocation.RegionCode = regionCode;
                        ShippingLocation.RegionName = regionName;
                        ShippingLocation.PostalCode = maskedTextBoxPostalCode.Text;
                        ShippingLocation.TimeZoneCode = timeZoneCode;
                        ShippingLocation.TimeZoneName = timeZoneName;
                        ShippingLocation.ContactName = textBoxContactName.Text;
                        ShippingLocation.ContactEmailAddress = textBoxContactEmailAddress.Text;
                        ShippingLocation.ContactPhone = maskedTextBoxContactPhone.Text;
                        ShippingLocation.MobilePhone = maskedTextBoxContactMobilePhone.Text;
                        ShippingLocation.HoursOfOperation = GetHoursOfOperationForSave(timeZoneCode);
                        ShippingLocation.Comment = textBoxComments.Text;
                        ShippingLocation.UpdatedOnUtc = DateTime.UtcNow;
                    }

                    // We are sending this object back to the calling form, and not doing a database write
                    // Let the calling form add this object to the calling form object

                }


            }

            return vFlag;
        }

        private SimpleBol.Models.MongoDb.HoursOfOperation? GetHoursOfOperationForSave(string timeZoneCode)
        {
            var schedule = checkBoxNormalHours.Checked
                ? CreateNormalHours()
                : customHoursOfOperation;

            if (schedule != null)
            {
                schedule.TimeZoneId = timeZoneCode;
            }

            return schedule;
        }

        private static SimpleBol.Models.MongoDb.HoursOfOperation CreateNormalHours()
        {
            return new SimpleBol.Models.MongoDb.HoursOfOperation
            {
                Days = Enum.GetValues<DayOfWeek>()
                    .Select(day => new DailyHours
                    {
                        Day = day,
                        IsClosed = day is DayOfWeek.Saturday or DayOfWeek.Sunday,
                        Open = day is DayOfWeek.Saturday or DayOfWeek.Sunday ? null : "08:00",
                        Close = day is DayOfWeek.Saturday or DayOfWeek.Sunday ? null : "17:00"
                    })
                    .ToList()
            };
        }

        private static bool IsNormalHours(SimpleBol.Models.MongoDb.HoursOfOperation? schedule)
        {
            if (schedule?.Days == null || schedule.Days.Count != 7)
            {
                return false;
            }

            return Enum.GetValues<DayOfWeek>().All(day =>
            {
                var hours = schedule.Days.FirstOrDefault(item => item.Day == day);
                var isWeekend = day is DayOfWeek.Saturday or DayOfWeek.Sunday;
                return hours != null && (isWeekend
                    ? hours.IsClosed
                    : !hours.IsClosed && hours.Open == "08:00" && hours.Close == "17:00");
            });
        }

        #endregion
        #region Textboxes

        private void MaskedTextBoxPostalCode_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPostalCode.SelectAll();
            });
        }

        private void MaskedTextBoxContactPhone_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxContactPhone.SelectAll();
            });
        }

        private void MaskedTextBoxContactMobilePhone_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxContactMobilePhone.SelectAll();
            });
        }

        private void MaskedTextBoxContactPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void MaskedTextBoxContactMobilePhone_KeyPress(object sender, KeyPressEventArgs e)
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
        #region TextBox Validators

        private void textBoxEmailValidating_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string email = textBox.Text;

            // Perform email address validation using regular expressions
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValid = Regex.IsMatch(email, pattern);

            // Set error provider or display an error message if the email is invalid
            if (!isValid)
            {
                errorProvider1.SetError(textBox, "Invalid email address format");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(textBox, ""); // Clear error if valid
            }
        }

        #endregion
        #region ComboBoxes

        private async void SetComboBoxesToWindowsSystemRegion()
        {

            string countryName = RegionInfo.CurrentRegion.DisplayName;
            if (countryName == "United States")
            {
                comboBoxCountry.SelectedIndex = comboBoxCountry.FindString("United States");

                if (comboBoxCountry.SelectedValue != null)
                {

                    string countryId = (comboBoxCountry.SelectedValue as dynamic).ToString();
                    _ = await LoadComboBoxRegionsAsync(countryId);

                }

            }

        }

        #endregion


    }
}
