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
    public partial class VendorDialog : Form
    {
        public string VendorId { get; set; } = null!;
        public VENDORS Vendor { get; set; } = null!;
        public List<SHIPPINGLOCATIONS> ShippingLocations { get; set; } = null!;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly IVendorRepository? vendorRepository;

        public VendorDialog(
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

        protected async void VendorDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            var countryCount = LoadComboBoxCountriesAsync();
            await Task.WhenAll(countryCount);

            SetListViewShippingLocations();
            SetToolTips();

            // Toggle the Location change flag
            labelLocationChangePending.Visible = false;

            if (VendorId != null)
            {
                if (VendorId != "ADD")
                {
                    LoadVendorAsync(VendorId);                    
                }
                else
                {
                    this.Vendor = new VENDORS();
                    SetComboBoxesToWindowsSystemRegion();
                }
            }

        }

        protected void VendorDialog_Shown(object sender, EventArgs e)
        {
            EnableEventHandlers();

            // Put the Focus and Select down on Company Name
            textBoxCompanyName.Focus();
            textBoxCompanyName.Select();
            buttonEditLocation.Enabled = false;
            buttonRemoveLocation.Enabled = false;            

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
            Cursor = Cursors.WaitCursor;

            int countriesCount = 0;

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

        private async void ComboBoxCountry_SelectedIndexChanged([AllowNull] object sender, [AllowNull] EventArgs e)
        {
            if (comboBoxCountry.SelectedValue is not null)
            {
                string countryCode = comboBoxCountry.SelectedValue.ToString()!;
                await LoadComboBoxRegionsAsync(countryCode);
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
            listViewShippingLocations.Columns.Add("Code", 0, HorizontalAlignment.Center);
            listViewShippingLocations.Columns.Add("Name", 130, HorizontalAlignment.Left);
            listViewShippingLocations.Columns.Add("RegionCode", 120, HorizontalAlignment.Left);
            listViewShippingLocations.Columns.Add("CountryCode", 120, HorizontalAlignment.Left);

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

                var result = MessageBox.Show("Are you sure you want to delete " + locationName + "?", "Shipping Locations", MessageBoxButtons.YesNo);
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
                            item1.SubItems.Add(shippingAddress.CountryCode);
                            item1.SubItems.Add(shippingAddress.RegionCode);
                            listViewShippingLocations.Items.Add(item1);

                        }

                    }

                    Cursor = Cursors.Default;
                }

            }

        }

        #endregion
        #region LoadSave

        private async void LoadVendorAsync(string vendorId)
        {
            var vendor = new VENDORS();

            if (vendorId != null)
            {
                if (vendorRepository != null)
                {
                    var getVendor = await vendorRepository.GetOneVendorAsync(vendorId);
                    if (getVendor != null)
                    {
                        this.Vendor = getVendor;

                        if (getVendor.ShippingLocations != null)
                        {
                            this.ShippingLocations = getVendor.ShippingLocations;
                        }

                        textBoxCompanyName.Text = getVendor.CompanyName;
                        textBoxAddress1.Text = getVendor.Address1;
                        textBoxAddress2.Text = getVendor.Address2;
                        textBoxCity.Text = getVendor.City;
                        maskedTextBoxPostalCode.Text = getVendor.PostalCode;
                        maskedTextBoxPhoneNumber1.Text = getVendor.Phone1;
                        maskedTextBoxEmailAddress1.Text = getVendor.EmailAddress1;
                        maskedTextBoxPhoneNumber2.Text = getVendor?.Phone2;
                        maskedTextBoxEmailAddress2.Text = getVendor?.EmailAddress2;
                        textBoxComments.Text = getVendor?.Comment;
                        checkBoxLiftgateRequired.Checked = getVendor?.LiftgateRequired == true ? true : false;
                        checkBoxPalletJackRequired.Checked = getVendor?.PalletJackRequired == true ? true : false;

                        // Make sure the RegionCode Polulates after the CountryCode Code is set
                        var countryCode = getVendor?.CountryCode;
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
                                comboBoxRegion.SelectedValue = getVendor?.RegionCode;

                                EnableEventHandlers();
                            }

                        }

                        if (getVendor != null && getVendor.ShippingLocations != null)
                        {
                            foreach (SHIPPINGLOCATIONS shippingAddress in getVendor.ShippingLocations)
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

        private async Task<bool> SaveVendorAsync()
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
            if (comboBoxCountry.SelectedIndex <= 0 || comboBoxCountry.SelectedValue is null)
            {
                vFlag = false;
                comboBoxCountry.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxCountry.BackColor = System.Drawing.SystemColors.Window;
            }

            // RegionCode
            if (comboBoxRegion.SelectedIndex <= 0 || comboBoxRegion.SelectedValue is null)
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
                string countryCode = comboBoxCountry.SelectedValue!.ToString()!;
                string countryLongName = comboBoxCountry.Text;

                string regionCode = comboBoxRegion.SelectedValue!.ToString()!;
                string regionLongName = comboBoxRegion.Text;
                
                // Trim the hyphen off the Postal Code if 5 chars
                if (maskedTextBoxPostalCode.Text != null) { if (maskedTextBoxPostalCode.Text.Length == 6) { maskedTextBoxPostalCode.Text = maskedTextBoxPostalCode.Text[..5]; } }

                if (VendorId == "ADD")
                {
                    var newVendor = new VENDORS
                    {
                        CompanyName = textBoxCompanyName.Text,
                        Address1 = textBoxAddress1.Text,
                        Address2 = textBoxAddress2.Text,
                        City = textBoxCity.Text,
                        CountryCode = countryCode,
                        CountryLongName = countryLongName,
                        RegionCode = regionCode,
                        RegionShortName = "",
                        RegionLongName = regionLongName,
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
                        ShippingLocations = ShippingLocations
                    };

                    if (vendorRepository != null)
                    {
                        // Write the VendorsForm using the repository
                        await vendorRepository.AddVendorAsync(newVendor);

                        // Assign the VendorsForm to the return object
                        this.Vendor = newVendor;
                    }

                }
                else
                {

                    // Update the current Vendor
                    Vendor.CompanyName = textBoxCompanyName.Text;
                    Vendor.Address1 = textBoxAddress1.Text;
                    Vendor.Address2 = textBoxAddress2.Text;
                    Vendor.City = textBoxCity.Text;
                    Vendor.CountryCode = countryCode;
                    Vendor.CountryLongName = countryLongName;
                    Vendor.RegionCode = regionCode;
                    Vendor.RegionShortName = "";
                    Vendor.RegionLongName = regionLongName;
                    Vendor.PostalCode = maskedTextBoxPostalCode.Text;
                    Vendor.Phone1 = maskedTextBoxPhoneNumber1.Text;
                    Vendor.Phone2 = maskedTextBoxPhoneNumber2.Text;
                    Vendor.EmailAddress1 = maskedTextBoxEmailAddress1.Text;
                    Vendor.EmailAddress2 = maskedTextBoxEmailAddress2.Text;
                    Vendor.LiftgateRequired = checkBoxLiftgateRequired.Checked;
                    Vendor.PalletJackRequired = checkBoxPalletJackRequired.Checked;
                    Vendor.UpdatedOnUtc = DateTime.UtcNow;
                    Vendor.Comment = textBoxComments.Text;
                    Vendor.ShippingLocations = ShippingLocations;

                    if (vendorRepository != null)
                    {
                        // Write the Vendor using the vendorRepository
                        await vendorRepository.UpdateVendorAsync(Vendor, VendorId);

                    }

                }

            }

            return vFlag;

        }

        #endregion
        #region Locations

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
        #region OkCancel

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveVendorAsync();
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

        private void MaskedTextBoxPostalCode_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPostalCode.SelectAll();
            });
        }

        private void MaskedTextBoxPhoneNumber1_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber1.SelectAll();
            });
        }

        private void MaskedTextBoxPhoneNumber2_Enter(object sender, EventArgs e)
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
            if (sender is not TextBoxBase textBox)
                return;
           
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
