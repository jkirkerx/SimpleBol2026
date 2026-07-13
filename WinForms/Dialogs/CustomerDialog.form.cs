using SimpleBol.Classes.DI;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;

using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class CustomerDialog : Form
    {

        public string CustomerId { get; set; } = null!;
        public CUSTOMERS Customer { get; set; } = null!;
        public List<SHIPPINGLOCATIONS> ShippingLocations { get; set; } = null!;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly ICustomerRepository? customerRepository;

        public CustomerDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            ICustomerRepository customerRespository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.customerRepository = customerRespository;
        }

        #region Dialog

        protected async void CustomerDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            var countries = LoadComboBoxCountriesAsync();
            await Task.WhenAll(countries);

            SetListViewShippingLocations();
            SetToolTips();

            // Toggle the Location change flag
            labelLocationChangePending.Visible = false;

            if (CustomerId != null)
            {
                if (CustomerId != "ADD")
                {
                    LoadCustomerAsync(CustomerId);
                }
                else
                {                    
                    SetComboBoxesToWindowsSystemRegion();
                    this.Customer = new CUSTOMERS();
                }
            }
        }

        protected void CustomerDialog_Shown(object sender, EventArgs e)
        {           

            // Put the Focus and Select down on Company Name
            textBoxCompanyName.Focus();
            textBoxCompanyName.Select();
            buttonEditLocation.Enabled = false;
            buttonRemoveLocation.Enabled = false;

            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        #endregion
        #region Events

        private void DisableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged -= ComboBoxCountry_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
        }

        #endregion
        #region CompanyInformation

        private async Task<int> LoadComboBoxCountriesAsync()
        {
            int countriesCount = 0;

            Cursor = Cursors.WaitCursor;

            if (commonRepository != null)
            {

                var getCountries = await commonRepository.GetAllCountriesAsync();
                if (getCountries != null)
                {
                    countriesCount = getCountries.Count;
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
                }

            }

            Cursor = Cursors.Default;

            return countriesCount;

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

        #endregion
        #region Locations

        private void SetListViewShippingLocations()
        {
            listViewShippingLocations.Visible = true;
            listViewShippingLocations.Items.Clear();
            listViewShippingLocations.Columns.Clear();

            // Set ListView Parameters
            listViewShippingLocations.Cursor = Cursors.Hand;
            listViewShippingLocations.View = View.Details;
            listViewShippingLocations.LabelEdit = false;
            listViewShippingLocations.AllowColumnReorder = true;
            listViewShippingLocations.CheckBoxes = false;
            listViewShippingLocations.FullRowSelect = true;
            listViewShippingLocations.GridLines = true;
            listViewShippingLocations.Scrollable = true;
            listViewShippingLocations.MultiSelect = false;
            listViewShippingLocations.OwnerDraw = false;
            listViewShippingLocations.Sorting = SortOrder.Ascending;
            listViewShippingLocations.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListView_DrawColumnHeader);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 1
            };

            this.listViewShippingLocations.ListViewItemSorter = listviewColumnSorter;

            // Create Columns and assign the column widths
            listViewShippingLocations.Columns.Add("VendorId", 0, HorizontalAlignment.Center);
            listViewShippingLocations.Columns.Add("LocationName", 130, HorizontalAlignment.Left);
            listViewShippingLocations.Columns.Add("RegionCode", 120, HorizontalAlignment.Left);
            listViewShippingLocations.Columns.Add("CountryCode", 120, HorizontalAlignment.Left);

        }

        private int LoadListViewVendorShippingLocations(List<SHIPPINGLOCATIONS> shippingLocations)
        {
            int locationCount = 0;

            if (shippingLocations != null)
            {
                foreach (var shippingLocation in shippingLocations)
                {
                    // Write the Contact to the ListView Control
                    var item1 = new ListViewItem(shippingLocation.LocationId?.ToString())
                    {
                        Checked = false,
                        ImageIndex = listViewShippingLocations.Items.Count + 1
                    };

                    item1.SubItems.Add(shippingLocation.LocationCode);
                    item1.SubItems.Add(shippingLocation.Name);
                    item1.SubItems.Add(shippingLocation.RegionName);
                    item1.SubItems.Add(shippingLocation.CountryName);
                    listViewShippingLocations.Items.Add(item1);

                }
            }

            return locationCount;
        }

        private void ListView_DrawColumnHeader(object? sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.Header != null && e.Font != null)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds); //Fill header with color

                //Adjust the position of the text to be vertically centered
                int yOffset = (e.Bounds.Height - e.Graphics.MeasureString(e.Header?.Text, e.Font).ToSize().Height) / 2;
                Rectangle newBounds = new Rectangle(e.Bounds.X, e.Bounds.Y + yOffset, e.Bounds.Width, e.Bounds.Height - yOffset);

                e.Graphics.DrawString(e.Header?.Text, e.Font, Brushes.Black, newBounds);
            }

        }

        private void ButtonAddLocation_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var locationDialogDIOwned = serviceProvider.CreateOwnedForm<LocationDialog>();
                var locationDialogDI = locationDialogDIOwned.Form;

                locationDialogDI.StartPosition = FormStartPosition.CenterScreen;
                locationDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                locationDialogDI.TopMost = true;
                locationDialogDI.LocationId = "ADD";
                locationDialogDI.Refresh();

                if (locationDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    if (locationDialogDI.ShippingLocation != null)
                    {
                        if (this.ShippingLocations == null) { this.ShippingLocations = new List<SHIPPINGLOCATIONS>(); }

                        this.ShippingLocations.Add(locationDialogDI.ShippingLocation);
                        if (this.ShippingLocations.Count > 0)
                        {
                            LoadListViewVendorShippingLocations(this.ShippingLocations);
                        }

                    }

                    // Toggle the Location change flag
                    labelLocationChangePending.Visible = true;
                    pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOn65;

                    Cursor = Cursors.Default;

                }
            }
        }

        private void ButtonEditLocation_Click(object sender, EventArgs e)
        {
            if (listViewShippingLocations.SelectedItems.Count == 1)
            {
                var listItem = listViewShippingLocations.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;
                var shippingLocation = this.ShippingLocations.Where(x => x.LocationId == objectId).FirstOrDefault();

                if (serviceProvider != null && shippingLocation != null)
                {

                    using var locationDialogDIOwned = serviceProvider.CreateOwnedForm<LocationDialog>();
                var locationDialogDI = locationDialogDIOwned.Form;
                    locationDialogDI.StartPosition = FormStartPosition.CenterScreen;
                    locationDialogDI.LocationId = objectId;
                    locationDialogDI.ShippingLocation = shippingLocation;
                    if (locationDialogDI.ShowDialog() == DialogResult.OK)
                    {
                        var location = locationDialogDI.ShippingLocation;
                        if (location != null)
                        {
                            int locationIndex = this.ShippingLocations.FindIndex(a => a.LocationId == objectId);
                            this.ShippingLocations.RemoveAt(locationIndex);
                            this.ShippingLocations.Add(location);

                            listViewShippingLocations.Items.Clear();

                            foreach (SHIPPINGLOCATIONS shippingAddress in this.ShippingLocations)
                            {
                                if (shippingAddress != null)
                                {

                                    var item1 = new ListViewItem(shippingAddress.LocationId)
                                    {
                                        Checked = false,
                                        ImageIndex = listViewShippingLocations.Items.Count + 1
                                    };

                                    item1.SubItems.Add(shippingAddress.Name);
                                    item1.SubItems.Add(shippingAddress.CountryName);
                                    item1.SubItems.Add(shippingAddress.RegionName);
                                    listViewShippingLocations.Items.Add(item1);

                                }

                            }

                            // Toggle the Location change flag
                            labelLocationChangePending.Visible = true;
                            pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOn65;

                            Application.DoEvents();

                        }
                    }

                }
            }
        }

        private void ButtonRemoveLocation_Click(object sender, EventArgs e)
        {
            if (listViewShippingLocations.SelectedItems.Count == 1)
            {
                var listItem = listViewShippingLocations.SelectedItems[0];
                string objectId = listItem.SubItems[0].Text;
                string locationName = listItem.SubItems[1].Text;

                var result = MessageBox.Show("Are you sure you want to remove " + locationName + "?", "Shipping Locations", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    int locationIndex = this.ShippingLocations.FindIndex(a => a.LocationId == objectId);
                    this.ShippingLocations.RemoveAt(locationIndex);

                    listViewShippingLocations.Items.Clear();

                    foreach (SHIPPINGLOCATIONS shippingAddress in this.ShippingLocations)
                    {
                        if (shippingAddress != null)
                        {

                            var item1 = new ListViewItem(shippingAddress.LocationId)
                            {
                                Checked = false,
                                ImageIndex = listViewShippingLocations.Items.Count + 1
                            };

                            item1.SubItems.Add(shippingAddress.Name);
                            item1.SubItems.Add(shippingAddress.CountryName);
                            item1.SubItems.Add(shippingAddress.RegionName);
                            listViewShippingLocations.Items.Add(item1);

                        }

                    }

                    Cursor = Cursors.Default;
                }

            }
        }

        private void ListViewShippingLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditLocation.Enabled = true;
            buttonRemoveLocation.Enabled = true;
        }

        private void ListViewShippingLocations_DoubleClick(object sender, EventArgs e)
        {
            buttonEditLocation.PerformClick();
        }

        #endregion
        #region LoadSave

        private async void LoadCustomerAsync(string customerId)
        {
            var customer = new CUSTOMERS();

            if (customerId != null)
            {
                if (customerRepository != null)
                {
                    var getCustomer = await customerRepository.GetOneCustomerAsync(customerId);
                    if (getCustomer != null)
                    {
                        this.Customer = getCustomer;

                        textBoxCompanyName.Text = getCustomer.CompanyName;
                        textBoxAddress1.Text = getCustomer.Address1;
                        textBoxAddress2.Text = getCustomer.Address2;
                        textBoxCity.Text = getCustomer.City;
                        maskedTextBoxPostalCode.Text = getCustomer.PostalCode;
                        maskedTextBoxPhoneNumber1.Text = getCustomer.Phone1;
                        maskedTextBoxEmailAddress1.Text = getCustomer.EmailAddress1;
                        maskedTextBoxPhoneNumber2.Text = getCustomer?.Phone2;
                        maskedTextBoxEmailAddress2.Text = getCustomer?.EmailAddress2;
                        textBoxComments.Text = getCustomer?.Comment;
                        checkBoxLiftgateRequired.Checked = getCustomer?.LiftgateRequired == true ? true : false;
                        checkBoxPalletJackRequired.Checked = getCustomer?.PalletJackRequired == true ? true : false;

                        // Make sure the RegionCode Polulates after the CountryCode Code is set
                        var countryCode = getCustomer?.CountryCode;
                        if (countryCode != null)
                        {
                            var currentCountryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
                            if (currentCountryCode != countryCode)
                            {
                                DisableEventHandlers();

                                // Wait for the regions to load
                                var regions = LoadComboBoxRegionsAsync(countryCode);
                                await Task.WhenAll(regions);

                                // Now set the country and region
                                comboBoxCountry.SelectedValue = countryCode;
                                comboBoxRegion.SelectedValue = getCustomer?.RegionCode;

                                EnableEventHandlers();
                            }

                        }

                        if (getCustomer != null && getCustomer.ShippingLocations != null)
                        {
                            foreach (SHIPPINGLOCATIONS shippingAddress in getCustomer.ShippingLocations)
                            {
                                if (shippingAddress != null)
                                {
                                    var item1 = new ListViewItem(shippingAddress.LocationId)
                                    {
                                        Checked = false,
                                        ImageIndex = listViewShippingLocations.Items.Count + 1
                                    };

                                    item1.SubItems.Add(shippingAddress.Name);
                                    item1.SubItems.Add(shippingAddress.CountryName);
                                    item1.SubItems.Add(shippingAddress.RegionName);
                                    listViewShippingLocations.Items.Add(item1);

                                }

                            }
                        }


                    }


                }


            }

        }

        private async Task<bool> SaveCustomerAsync()
        {

            var vFlag = true;

            // Company Name
            if (textBoxCompanyName.Text.Length == 0)
            {
                vFlag = false;
                textBoxCompanyName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxCompanyName.BackColor = System.Drawing.SystemColors.Window;
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

            // Main Phone Number
            if (maskedTextBoxPhoneNumber1.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxPhoneNumber1.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxPhoneNumber1.BackColor = System.Drawing.SystemColors.Window;
            }

            // Main EmailAddress 1
            if (maskedTextBoxEmailAddress1.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxEmailAddress1.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxEmailAddress1.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {
                string countryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
                string countryName = comboBoxCountry.Text.Trim();
                string regionCode = (comboBoxRegion.SelectedValue as dynamic).ToString();
                string regionName = comboBoxRegion.Text.Trim();

                // Trim the hyphen off the Postal Code if 5 chars
                if (maskedTextBoxPostalCode.Text != null) { if (maskedTextBoxPostalCode.Text.Length == 6) { maskedTextBoxPostalCode.Text = maskedTextBoxPostalCode.Text[..5]; } }

                if (CustomerId == "ADD")
                {

                    var newCustomer = new CUSTOMERS
                    {
                        CompanyName = textBoxCompanyName.Text,
                        Address1 = textBoxAddress1.Text,
                        Address2 = textBoxAddress2.Text,
                        City = textBoxCity.Text,
                        CountryCode = countryCode,
                        CountryLongName = countryName,
                        RegionCode = regionCode,
                        RegionLongName = regionName,
                        PostalCode = maskedTextBoxPostalCode.Text,
                        Phone1 = maskedTextBoxPhoneNumber1.Text,
                        Phone2 = maskedTextBoxPhoneNumber2.Text,
                        EmailAddress1 = maskedTextBoxEmailAddress1.Text,
                        EmailAddress2 = maskedTextBoxEmailAddress2.Text,
                        LiftgateRequired = checkBoxLiftgateRequired.Checked,
                        PalletJackRequired = checkBoxPalletJackRequired.Checked,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                        Comment = textBoxComments.Text,
                        ShippingLocations = this.ShippingLocations
                    };

                    if (customerRepository != null)
                    {
                        // Write the Shipper using the repository
                        await customerRepository.AddCustomerAsync(newCustomer);

                        // Assign the Customer to the retuen object
                        Customer = newCustomer;
                    }

                }
                else
                {
                    if (Customer == null)
                    {
                        Customer = new CUSTOMERS();
                    }

                    // Update the current Shipper
                    Customer.CompanyName = textBoxCompanyName.Text;
                    Customer.Address1 = textBoxAddress1.Text;
                    Customer.Address2 = textBoxAddress2.Text;
                    Customer.City = textBoxCity.Text;
                    Customer.CountryCode = countryCode;
                    Customer.CountryLongName = countryName;
                    Customer.RegionCode = regionCode;
                    Customer.RegionLongName = regionName;
                    Customer.PostalCode = maskedTextBoxPostalCode.Text;
                    Customer.Phone1 = maskedTextBoxPhoneNumber1.Text;
                    Customer.Phone2 = maskedTextBoxPhoneNumber2.Text;
                    Customer.EmailAddress1 = maskedTextBoxEmailAddress1.Text;
                    Customer.EmailAddress2 = maskedTextBoxEmailAddress2.Text;
                    Customer.LiftgateRequired = checkBoxLiftgateRequired.Checked;
                    Customer.PalletJackRequired = checkBoxPalletJackRequired.Checked;
                    Customer.UpdatedOnUtc = DateTime.UtcNow;
                    Customer.Comment = textBoxComments.Text;
                    Customer.ShippingLocations = this.ShippingLocations;

                    if (customerRepository != null)
                    {
                        // Write the Shipper using the repository
                        await customerRepository.UpdateCustomerAsync(Customer, CustomerId);

                    }

                }


            }

            return vFlag;

        }

        #endregion
        #region Buttons

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveCustomerAsync();
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
        #region TextBoxes

        protected void MaskedTextBoxPostalCode_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPostalCode.SelectAll();
            });
        }

        protected void MaskedTextBoxPhoneNumber1_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber1.SelectAll();
            });
        }

        protected void MaskedTextBoxPhoneNumber2_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber2.SelectAll();
            });
        }

        private void MaskedTextBoxPhoneNumber1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void MaskedTextBoxPhoneNumber2_KeyPress(object sender, KeyPressEventArgs e)
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
        #region ToolTips

        private void SetToolTips()
        {
            var ttPictureBoxUpdateFlag = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 1000,
                ReshowDelay = 500,
                ShowAlways = true
            };
            ttPictureBoxUpdateFlag.SetToolTip(this.pictureBoxUpdateFlag, "Indicates a change that requires a save");

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
