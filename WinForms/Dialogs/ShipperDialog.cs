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
    public partial class ShipperDialog : Form
    {
        public string ShipperId { get; set; } = null!;
        public SHIPPER Shipper { get; set; } = null!;

        private ListViewColumnSorter? listviewColumnSorter;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly IShipperRepository? shipperRepository;

        public ShipperDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            IShipperRepository shipperRespository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.shipperRepository = shipperRespository;

        }

        #region Dialog

        protected async void ShipperDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            var countries = LoadComboBoxCountryAsync();
            var countriesSourced = LoadComboBoxCountriesSourcedAsync();
            await Task.WhenAll(countries, countriesSourced);

            // Intialize the Countries Serviced Listview
            SetListviewCountriesServiced();

            // Initialize Shipper Contacts ListView
            SetListviewShipperContacts();

            if (this.ShipperId != null)
            {
                if (this.ShipperId == "ADD")
                {
                    // Were creating a new Shipper
                    SetComboBoxesToWindowsSystemRegion();
                }
                else
                {
                    // Were loading a shipper to update
                    this.Shipper = await LoadShipperAsync(this.ShipperId);
                }
            }

            EnableEventHandlers();

            // Toggle the Location change flag
            labelLocationChangePending.Visible = false;

        }

        protected void ShipperDialogShown(object sender, EventArgs e)
        {
            textBoxCompanyName.Select();
            Cursor = Cursors.Default;
        }

        #endregion
        #region Buttons

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveShipperAsync();
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
        #region Events

        private void ComboBoxCountry_SelectedIndexChanged([AllowNull] object sender, [AllowNull] EventArgs e)
        {

            var countryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
            if (countryCode != null)
            {
                LoadComboBoxRegionsAsync(countryCode);
            }

        }

        private void EnableEventHandlers()
        {
            this.comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
        }

        private void DisableEventHandlers()
        {
            this.comboBoxCountry.SelectedIndexChanged -= ComboBoxCountry_SelectedIndexChanged;
        }

        #endregion
        #region ComboBoxLoads

        private async Task<int> LoadComboBoxCountryAsync()
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
        #region LoadSave

        private async Task<SHIPPER> LoadShipperAsync(string shipperId)
        {
            var shipper = new SHIPPER();

            if (shipperRepository != null)
            {
                var getShipper = await shipperRepository.GetOneShipperAsync(ShipperId);
                if (getShipper != null)
                {
                    DisableEventHandlers();

                    // Set the Dialog Global for Shipper
                    shipper = getShipper;

                    textBoxCompanyName.Text = getShipper.CompanyName;
                    textBoxAddress1.Text = getShipper.Address1;
                    textBoxAddress2.Text = getShipper.Address2;
                    textBoxCity.Text = getShipper.City;
                    maskedTextBoxPostalCode.Text = getShipper.PostalCode;
                    maskedTextBoxPhoneNumber1.Text = getShipper.Phone1;
                    maskedTextBoxEmailAddress1.Text = getShipper.EmailAddress1;
                    maskedTextBoxPhoneNumber2.Text = getShipper?.Phone2;
                    maskedTextBoxEmailAddress2.Text = getShipper?.EmailAddress2;
                    textBoxComments.Text = getShipper?.Comment;
                    checkBoxLiftgate.Checked = getShipper?.Liftgate == true ? true : false;
                    checkBoxElectronicQuotes.Checked = getShipper?.ElectronicQuotesAvailable == true ? true : false;
                    checkBoxTrackingServices.Checked = getShipper?.TrackingService == true ? true : false;
                    checkBoxFavorite.Checked = getShipper?.Favorite == true ? true : false;

                    // Make sure the RegionCode Polulates after the CountryCode Code is set
                    var countryCode = getShipper?.CountryCode;
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
                            comboBoxRegion.SelectedValue = getShipper?.RegionCode;

                            EnableEventHandlers();
                        }

                    }

                    if (getShipper?.ShipperServices != null)
                    {

                        checkBoxLTL.Checked = getShipper.ShipperServices?.ServiceLTL == true ? true : false;
                        checkBoxOcean.Checked = getShipper.ShipperServices?.ServiceOcean == true ? true : false;
                        checkBoxRailroad.Checked = getShipper.ShipperServices?.ServiceRailroad == true ? true : false;
                        checkBoxAirplane.Checked = getShipper.ShipperServices?.ServiceAirplane == true ? true : false;
                        checkBoxLastMile.Checked = getShipper.ShipperServices?.ServiceLastMile == true ? true : false;
                    }

                    // Add any Service Countries
                    if (getShipper?.ServiceCountries != null)
                    {
                        if (getShipper.ServiceCountries.Count > 0)
                        {
                            foreach (var serviceCountry in getShipper.ServiceCountries)
                            {
                                if (serviceCountry != null)
                                {
                                    var item1 = new ListViewItem(serviceCountry.CountryCode)
                                    {
                                        Checked = false,
                                        ImageIndex = listViewCountriesServiced.Items.Count + 1
                                    };

                                    item1.SubItems.Add(serviceCountry.LongName);
                                    listViewCountriesServiced.Items.Add(item1);
                                }

                            }
                        }
                    }

                    // Add any Shipper Contacts
                    if (getShipper?.ShipperContacts != null)
                    {
                        if (getShipper?.ShipperContacts.Count > 0)
                        {
                            foreach (var shipperContact in getShipper.ShipperContacts)
                            {
                                if (shipperContact != null)
                                {
                                    var item1 = new ListViewItem(shipperContact.ContactId)
                                    {
                                        Checked = false,
                                        ImageIndex = listViewShipperContacts.Items.Count + 1
                                    };

                                    item1.SubItems.Add(shipperContact.ContactName);
                                    item1.SubItems.Add(shipperContact.ContactEmailAddress);
                                    item1.SubItems.Add(shipperContact.ContactName);
                                    listViewShipperContacts.Items.Add(item1);
                                }

                            }
                        }
                    }

                    EnableEventHandlers();

                }

            }

            return shipper;

        }

        private async Task<bool> SaveShipperAsync()
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
                var countryCode = (comboBoxCountry.SelectedValue as dynamic).ToString();
                var regionCode = (comboBoxRegion.SelectedValue as dynamic).ToString();
                var serviceCountries = GetListViewServiceCountries();
                var shipperContacts = GetListViewShipperContacts();

                // Trim the hyphen off the Postal Code if 5 chars
                if (maskedTextBoxPostalCode.Text != null) { if (maskedTextBoxPostalCode.Text.Length == 6) { maskedTextBoxPostalCode.Text = maskedTextBoxPostalCode.Text[..5]; } }

                var shipperServices = new ShipperServices
                {
                    ServiceLTL = checkBoxLTL.Checked,
                    ServiceFTL = checkBoxFTL.Checked,
                    ServiceOcean = checkBoxOcean.Checked,
                    ServiceAirplane = checkBoxAirplane.Checked,
                    ServiceRailroad = checkBoxRailroad.Checked,
                    ServiceLastMile = checkBoxLastMile.Checked,
                    ServiceCourier = checkBoxCourier.Checked,
                    ServiceArmouredCar = checkBoxArmouredCar.Checked,
                };

                if (ShipperId == "ADD")
                {

                    var newShipper = new SHIPPER
                    {
                        CompanyName = textBoxCompanyName.Text,
                        Address1 = textBoxAddress1.Text,
                        Address2 = textBoxAddress2.Text,
                        City = textBoxCity.Text,
                        CountryCode = countryCode,
                        RegionCode = regionCode,
                        PostalCode = maskedTextBoxPostalCode.Text,
                        Phone1 = maskedTextBoxPhoneNumber1.Text,
                        Phone2 = maskedTextBoxPhoneNumber2.Text,
                        EmailAddress1 = maskedTextBoxEmailAddress1.Text,
                        EmailAddress2 = maskedTextBoxEmailAddress2.Text,
                        TrackingService = checkBoxTrackingServices.Checked,
                        ElectronicQuotesAvailable = checkBoxElectronicQuotes.Checked,
                        Favorite = checkBoxFavorite.Checked,
                        ShipperServices = shipperServices,
                        Liftgate = checkBoxLiftgate.Checked,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                        Comment = textBoxComments.Text,
                        ServiceCountries = serviceCountries,
                        ShipperContacts = shipperContacts
                    };

                    if (shipperRepository != null)
                    {
                        // Write the Shipper using the repository
                        await shipperRepository.AddShipperAsync(newShipper);

                        // Assign the Shipper to the retuen object
                        Shipper = newShipper;
                    }

                }
                else
                {
                    if (Shipper == null)
                    {
                        Shipper = new SHIPPER();
                    }

                    // Update the current Shipper
                    Shipper.CompanyName = textBoxCompanyName.Text;
                    Shipper.Address1 = textBoxAddress1.Text;
                    Shipper.Address2 = textBoxAddress2.Text;
                    Shipper.City = textBoxCity.Text;
                    Shipper.CountryCode = countryCode;
                    Shipper.RegionCode = regionCode;
                    Shipper.PostalCode = maskedTextBoxPostalCode.Text;
                    Shipper.Phone1 = maskedTextBoxPhoneNumber1.Text;
                    Shipper.Phone2 = maskedTextBoxPhoneNumber2.Text;
                    Shipper.EmailAddress1 = maskedTextBoxEmailAddress1.Text;
                    Shipper.EmailAddress2 = maskedTextBoxEmailAddress2.Text;
                    Shipper.TrackingService = checkBoxTrackingServices.Checked;
                    Shipper.ElectronicQuotesAvailable = checkBoxElectronicQuotes.Checked;
                    Shipper.Favorite = checkBoxFavorite.Checked;
                    Shipper.ShipperServices = shipperServices;
                    Shipper.Liftgate = checkBoxLiftgate.Checked;
                    Shipper.UpdatedOnUtc = DateTime.UtcNow;
                    Shipper.Comment = textBoxComments.Text;
                    Shipper.ServiceCountries = serviceCountries;
                    Shipper.ShipperContacts = shipperContacts;

                    if (shipperRepository != null)
                    {
                        // Write the Shipper using the repository
                        await shipperRepository.UpdateShipperAsync(Shipper, ShipperId);

                    }

                }

            }

            return vFlag;
        }

        #endregion
        #region CountriesServiced

        private void SetListviewCountriesServiced()
        {
            listViewCountriesServiced.Visible = true;
            listViewCountriesServiced.Items.Clear();
            listViewCountriesServiced.Columns.Clear();

            // Set ListView Parameters
            listViewCountriesServiced.Cursor = Cursors.Hand;
            listViewCountriesServiced.View = View.Details;
            listViewCountriesServiced.LabelEdit = false;
            listViewCountriesServiced.AllowColumnReorder = true;
            listViewCountriesServiced.CheckBoxes = false;
            listViewCountriesServiced.FullRowSelect = true;
            listViewCountriesServiced.GridLines = true;
            listViewCountriesServiced.Scrollable = true;
            listViewCountriesServiced.MultiSelect = false;
            listViewCountriesServiced.OwnerDraw = false;
            listViewCountriesServiced.Sorting = SortOrder.Ascending;
            listViewCountriesServiced.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListView_DrawColumnHeader);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 1
            };

            this.listViewCountriesServiced.ListViewItemSorter = listviewColumnSorter;

            // Create Columns and assign the column widths
            listViewCountriesServiced.Columns.Add("CountryId", 0, HorizontalAlignment.Center);
            listViewCountriesServiced.Columns.Add("CountryCode", 350, HorizontalAlignment.Left);

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

        private async Task<int> LoadComboBoxCountriesSourcedAsync()
        {

            int countriesCount = 0;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxCountriesSource.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxCountriesSource.DataSource = currentDataTable;
            }

            var getCountriesSource = await CountryRegion.Region.GetCountries();
            if (getCountriesSource != null)
            {
                countriesCount = getCountriesSource.Count();

                var dtCountriesSourced = new DataTable("dtCountriesSourced");
                dtCountriesSourced.Columns.Add(new DataColumn("Key"));
                dtCountriesSourced.Columns.Add(new DataColumn("Value"));

                var rsDefault = dtCountriesSourced.NewRow();
                rsDefault[0] = "-- Make a selection --";
                rsDefault[1] = 0;
                dtCountriesSourced.Rows.Add(rsDefault);

                foreach (var countrySourceItem in getCountriesSource.OrderBy(ob => ob?.Name))
                {
                    if (countrySourceItem != null)
                    {
                        var rsCountryItem = dtCountriesSourced.NewRow();
                        rsCountryItem[0] = countrySourceItem.Name;
                        rsCountryItem[1] = countrySourceItem.Id;
                        dtCountriesSourced.Rows.Add(rsCountryItem);
                    }
                }

                comboBoxCountriesSource.DataSource = dtCountriesSourced;
                comboBoxCountriesSource.DisplayMember = "Key";
                comboBoxCountriesSource.ValueMember = "Value";
                comboBoxCountriesSource.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxCountriesSource.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxCountriesSource.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxCountriesSource.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxCountriesSource.SelectedIndex = 0;
            }

            return countriesCount;

        }

        private List<ServiceCountries> GetListViewServiceCountries()
        {
            var list = new List<ServiceCountries>();

            // Construct the ServiceCountries First
            if (listViewCountriesServiced.Items.Count > 0)
            {

                ListView.ListViewItemCollection serviceCountriesAsItems = listViewCountriesServiced.Items;
                foreach (ListViewItem item in serviceCountriesAsItems)
                {
                    if (item != null)
                    {
                        Console.WriteLine(item.ToString());

                        var subItem = item.SubItems;
                        var cCode = subItem[0].Text;
                        var cName = subItem[1].Text;
                        var serviceCountry = new ServiceCountries
                        {
                            CountryCode = cCode,
                            LongName = cName,
                            ShortName = cName
                        };
                        list.Add(serviceCountry);

                    }

                }
            }

            return list;

        }

        #endregion
        #region Contacts

        private List<ShipperContacts> GetListViewShipperContacts()
        {
            var list = new List<ShipperContacts>();

            // Construct the ServiceCountries First
            if (listViewShipperContacts.Items.Count > 0)
            {

                ListView.ListViewItemCollection shipperContactsAsItems = listViewShipperContacts.Items;
                foreach (ListViewItem item in shipperContactsAsItems)
                {
                    if (item != null)
                    {

                        var subItem = item.SubItems;
                        var shipperContact = new ShipperContacts()
                        {
                            ShipperId = ShipperId,
                            ContactId = subItem[0].Text,
                            ContactName = subItem[1].Text,
                            ContactEmailAddress = subItem[2].Text.ToLower(),
                            ContactPhone = subItem[3].Text
                        };
                        list.Add(shipperContact);

                    }

                }
            }

            return list;

        }

        private async void ButtonAddContact_Click(object sender, EventArgs e)
        {

            if (serviceProvider != null && shipperRepository != null)
            {
                using var contactEditorDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperContactEditorDialog>();
                var contactEditorDialogDI = contactEditorDialogDIOwned.Form;
                contactEditorDialogDI.StartPosition = FormStartPosition.CenterScreen;
                contactEditorDialogDI.ContactId = "ADD";
                if (contactEditorDialogDI.ShowDialog() == DialogResult.OK)
                {
                    var shipperContact = contactEditorDialogDI.ShipperContact;
                    if (shipperContact != null)
                    {
                        Cursor = Cursors.WaitCursor;

                        // Write the Contact to the ListView Control
                        var item1 = new ListViewItem(shipperContact.ContactId?.ToString())
                        {
                            Checked = false,
                            ImageIndex = listViewCountriesServiced.Items.Count + 1
                        };

                        item1.SubItems.Add(shipperContact.ContactName);
                        item1.SubItems.Add(shipperContact.ContactEmailAddress);
                        item1.SubItems.Add(shipperContact.ContactPhone);
                        listViewShipperContacts.Items.Add(item1);

                        // Consider writing the shipper contact to the database document
                        if (ShipperId != "ADD")
                        {
                            var shipperContacts = new List<ShipperContacts>();
                            var listViewItems = listViewShipperContacts.Items;
                            foreach (ListViewItem item in listViewItems)
                            {
                                if (item != null)
                                {
                                    var newShipperContact = new ShipperContacts
                                    {
                                        ShipperId = ShipperId,
                                        ContactId = item.SubItems[0].Text,
                                        ContactName = item.SubItems[1].Text,
                                        ContactEmailAddress = item.SubItems[2].Text,
                                        ContactPhone = item.SubItems[3].Text
                                    };
                                    shipperContacts.Add(newShipperContact);
                                }
                            }

                            await shipperRepository.UpdateShipperContactsAsync(shipperContacts, ShipperId);

                        }

                        LoadAllShipperContactsFromShipper();
                        Application.DoEvents();

                        Cursor = Cursors.Default;

                    }
                }

                Application.DoEvents();
            }

        }

        private void ButtonEditContact_Click(object sender, EventArgs e)
        {
            if (listViewShipperContacts.SelectedItems.Count == 1)
            {
                var listItem = listViewShipperContacts.SelectedItems[0];
                var contactId = listItem.SubItems[0].Text;

                if (serviceProvider != null && shipperRepository != null)
                {
                    if (contactId != null && Shipper.ShipperContacts != null)
                    {
                        var selectedContact = Shipper.ShipperContacts.FirstOrDefault(cid => cid.ContactId == contactId);
                        if (selectedContact != null)
                        {
                            using var shipperContactsDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperContactEditorDialog>();
                var shipperContactsDialogDI = shipperContactsDialogDIOwned.Form;
                            shipperContactsDialogDI.StartPosition = FormStartPosition.CenterScreen;
                            shipperContactsDialogDI.ContactId = contactId;
                            shipperContactsDialogDI.ShipperContact = selectedContact;
                            if (shipperContactsDialogDI.ShowDialog() == DialogResult.OK)
                            {
                                var shipperContact = shipperContactsDialogDI.ShipperContact;
                                if (shipperContact != null)
                                {
                                    ListView.ListViewItemCollection shipperContactListItems = listViewShipperContacts.Items;
                                    if (shipperContactListItems != null)
                                    {
                                        var selectedShipperContact = shipperContactListItems.Cast<ListViewItem>().Where(x => x.SubItems[0].Text == contactId).FirstOrDefault();
                                        if (selectedShipperContact != null)
                                        {
                                            selectedShipperContact.SubItems[1].Text = shipperContact.ContactName;
                                            selectedShipperContact.SubItems[2].Text = shipperContact.ContactEmailAddress;
                                            selectedShipperContact.SubItems[3].Text = shipperContact.ContactPhone;
                                        }
                                    }

                                }

                            }
                        }

                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a shipper selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private void ButtonRemoveContact_Click(object sender, EventArgs e)
        {
            if (listViewShipperContacts.SelectedItems.Count > 0)
            {

                ListView.SelectedListViewItemCollection selectedShipperContactItems = listViewShipperContacts.SelectedItems;
                if (selectedShipperContactItems != null)
                {
                    foreach (ListViewItem selectedShipperContactItem in selectedShipperContactItems)
                    {
                        if (selectedShipperContactItem != null)
                        {
                            listViewShipperContacts.Items.Remove(selectedShipperContactItem);
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must select a shipper contact in the list view first, by using your mouse to highlight a country", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetListviewShipperContacts()
        {
            listViewShipperContacts.Visible = true;
            listViewShipperContacts.Items.Clear();
            listViewShipperContacts.Columns.Clear();

            // Set ListView Parameters
            listViewShipperContacts.Cursor = Cursors.Hand;
            listViewShipperContacts.View = View.Details;
            listViewShipperContacts.LabelEdit = false;
            listViewShipperContacts.AllowColumnReorder = true;
            listViewShipperContacts.CheckBoxes = false;
            listViewShipperContacts.FullRowSelect = true;
            listViewShipperContacts.GridLines = true;
            listViewShipperContacts.Scrollable = true;
            listViewShipperContacts.MultiSelect = false;
            listViewShipperContacts.OwnerDraw = false;
            listViewShipperContacts.Sorting = SortOrder.Ascending;
            listViewShipperContacts.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListView_DrawColumnHeader);

            // Program the ListView Column Sorter
            listviewColumnSorter = new ListViewColumnSorter()
            {
                Order = SortOrder.Ascending,
                SortColumn = 1
            };

            this.listViewCountriesServiced.ListViewItemSorter = listviewColumnSorter;

            // Create Columns and assign the column widths
            listViewShipperContacts.Columns.Add("ContactId", 0, HorizontalAlignment.Center);
            listViewShipperContacts.Columns.Add("Name", 160, HorizontalAlignment.Left);
            listViewShipperContacts.Columns.Add("EmailAddress", 225, HorizontalAlignment.Left);
            listViewShipperContacts.Columns.Add("Phone", 160, HorizontalAlignment.Left);

        }

        private void LoadAllShipperContactsFromShipper()
        {
            // Add any Shipper Contacts
            if (Shipper?.ShipperContacts != null)
            {
                if (Shipper?.ShipperContacts.Count > 0)
                {
                    foreach (var shipperContact in Shipper.ShipperContacts)
                    {
                        if (shipperContact != null)
                        {
                            var item1 = new ListViewItem(shipperContact.ContactId)
                            {
                                Checked = false,
                                ImageIndex = listViewShipperContacts.Items.Count + 1
                            };

                            item1.SubItems.Add(shipperContact.ContactName);
                            item1.SubItems.Add(shipperContact.ContactEmailAddress);
                            item1.SubItems.Add(shipperContact.ContactPhone);
                            listViewShipperContacts.Items.Add(item1);
                        }

                    }
                }
            }

        }

        private void listViewShipperContacts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void listViewShipperContacts_DoubleClick(object sender, EventArgs e)
        {
            if (listViewShipperContacts.SelectedItems.Count == 1)
            {
                var listItem = listViewShipperContacts.SelectedItems[0];
                var contactId = listItem.SubItems[0].Text;

                if (serviceProvider != null && shipperRepository != null)
                {
                    if (contactId != null && Shipper.ShipperContacts != null)
                    {
                        var selectedContact = Shipper.ShipperContacts.FirstOrDefault(cid => cid.ContactId == contactId);
                        if (selectedContact != null)
                        {
                            using var shipperContactsDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperContactEditorDialog>();
                var shipperContactsDialogDI = shipperContactsDialogDIOwned.Form;
                            shipperContactsDialogDI.StartPosition = FormStartPosition.CenterScreen;
                            shipperContactsDialogDI.ContactId = contactId;
                            shipperContactsDialogDI.ShipperContact = selectedContact;
                            if (shipperContactsDialogDI.ShowDialog() == DialogResult.OK)
                            {
                                var shipperContact = shipperContactsDialogDI.ShipperContact;
                                if (shipperContact != null)
                                {
                                    ListView.ListViewItemCollection shipperContactListItems = listViewShipperContacts.Items;
                                    if (shipperContactListItems != null)
                                    {
                                        var selectedShipperContact = shipperContactListItems.Cast<ListViewItem>().Where(x => x.SubItems[0].Text == contactId).FirstOrDefault();
                                        if (selectedShipperContact != null)
                                        {
                                            selectedShipperContact.SubItems[1].Text = shipperContact.ContactName;
                                            selectedShipperContact.SubItems[2].Text = shipperContact.ContactEmailAddress;
                                            selectedShipperContact.SubItems[3].Text = shipperContact.ContactPhone;
                                        }
                                    }

                                }

                            }
                        }

                    }

                }
            }
        }

        #endregion
        #region Countries

        private async void ButtonCountryAdd_Click(object sender, EventArgs e)
        {
            int countryCode = int.Parse(comboBoxCountriesSource.SelectedValue as dynamic);
            var getCountry = await CountryRegion.Region.GetCountry(countryCode);
            if (getCountry != null)
            {
                if (!listViewCountriesServiced.Items.ContainsKey(countryCode.ToString()))
                {
                    var item1 = new ListViewItem(getCountry.Id.ToString())
                    {
                        Checked = false,
                        ImageIndex = listViewCountriesServiced.Items.Count + 1
                    };

                    item1.SubItems.Add(getCountry.Name);
                    listViewCountriesServiced.Items.Add(item1);

                    // Remove this country from the ComboBox DataTable
                    DataTable dtCountriesSourced = (DataTable)comboBoxCountriesSource.DataSource;
                    if (dtCountriesSourced != null)
                    {
                        if (dtCountriesSourced.Rows.Count > 0)
                        {

                            var countryRow = dtCountriesSourced.AsEnumerable().FirstOrDefault(predicate: row => row.ItemArray.Equals(countryCode.ToString()));
                            if (countryRow != null)
                            {
                                var rsCountryIndex = dtCountriesSourced.Rows.IndexOf(countryRow);
                                dtCountriesSourced.Rows.RemoveAt(rsCountryIndex);
                                dtCountriesSourced.AcceptChanges();
                                comboBoxCountriesSource.DataSource = dtCountriesSourced;

                            }

                        }

                    }

                }
            }

        }

        private void ButtonCountryRemove_Click(object sender, EventArgs e)
        {

            if (listViewCountriesServiced.SelectedItems.Count > 0)
            {

                ListView.SelectedListViewItemCollection selectedServiceCountryItems = listViewCountriesServiced.SelectedItems;
                if (selectedServiceCountryItems != null)
                {
                    foreach (ListViewItem selectedServiceCountryItem in selectedServiceCountryItems)
                    {
                        if (selectedServiceCountryItem != null)
                        {
                            listViewCountriesServiced.Items.Remove(selectedServiceCountryItem);
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must select country in the list view first, by using your mouse to highlight a country", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
            BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber1.SelectAll();
            });
        }

        private void MaskedTextBoxPhoneNumber2_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
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
