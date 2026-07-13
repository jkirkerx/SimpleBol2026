using SimpleBol.Classes.DI;
using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using SimpleBol.Classes.Locale;
using System.Windows.Input;
using System.Windows.Forms;
using Cursors = System.Windows.Forms.Cursors;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class BolDialog : Form
    {
        public string BolId { get; set; } = null!;
        public string BolNumber { get; set; } = null!;
        public BILLOFLADINGS Bol { get; set; } = null!;
        public List<VENDORS> Vendors { get; set; } = null!;
        public List<PACKAGES> Packages { get; set; } = null!;
        public List<PALLETS> Pallets { get; set; } = null!;
        public List<CONTAINERS> Containers { get; set; } = null!;
        private ShipperServicesEnum? _shipperServices { get; set; }
        private int bolTotalWeight { get; set; }
        private string bolUnitOfMeasurement { get; set; }
        private bool groupBoxContainersExpand { get; set; }
        private bool groupBoxPalletsExpand { get; set; }
        private bool groupBoxPackagesExpand { get; set; }

        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly IShipperRepository? shipperRepository;
        private readonly IBillToAccountsRepository? billToAccountsRepository;
        private readonly IVendorRepository? vendorRepository;
        private readonly ICustomerRepository? customerRepository;
        private readonly IBolsRepository? bolRepository;

        public BolDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            IShipperRepository shipperRespository,
            IBillToAccountsRepository billToAccountsRespository,
            IVendorRepository vendorRepository,
            ICustomerRepository customerRepository,
            IBolsRepository? bolRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.shipperRepository = shipperRespository;
            this.billToAccountsRepository = billToAccountsRespository;
            this.vendorRepository = vendorRepository;
            this.customerRepository = customerRepository;
            this.bolRepository = bolRepository;

            // Satisfies the Compiler, but we set this in BolDialog_Load
            this.bolUnitOfMeasurement = "";

        }

        #region Dialog

        protected async void BolDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Disable Event Handlers
            DisableEventHandlers();

            // Initialize ListViews
            SetListviewContainers();
            SetListviewPallets();
            SetListviewPackages();

            // Load Destination Countries            
            LoadComboBoxDestinationCountriesAsync();

            // Load Shippers
            _ = await LoadComboBoxShippersAll();

            // Load 3rd Party Billing
            _ = await LoadComboBox3rdPartyBillingAll();

            // Load Vendors with discard
            _ = await LoadComboBoxVendorsAsync();

            // Load CustomersForm with discard
            _ = await LoadComboBoxCustomersAsync();

            // Turn off the Address panels
            panelShipper.Visible = false;
            panelShipFrom.Visible = false;
            panelBillTo.Visible = false;
            panelShipTo.Visible = false;

            // Turn off Location Panels
            panelShipFromLocations.Visible = false;
            panelShipToLocations.Visible = false;
            panelShipFromAppointment.Visible = false;
            panelShipToAppointment.Visible = false;

            // Turn Off Controls
            maskedTextBoxCodAmount.Visible = false;
            buttonEditPackage.Enabled = false;
            buttonRemovePackage.Enabled = false;
            buttonEditPallet.Enabled = false;
            buttonRemovePallet.Enabled = false;
            buttonEditContainer.Enabled = false;
            buttonRemoveContainer.Enabled = false;

            // Toggle the Location change flag
            labelChangePending.Visible = false;

            // Place Focus on the LTL Radio Button, and just check it off so the user doesn't get lost
            radioButtonLTL.Focus();
            radioButtonLTL.Checked = true;

            // Set the Group Boxes Expand/Collapse tracker flags
            groupBoxContainersExpand = false;
            groupBoxPalletsExpand = false;
            groupBoxPackagesExpand = false;

            // Print Button
            buttonPrint.Enabled = false;

            if (this.BolId == "ADD")
            {
                // Hide the COD Amount TextBox
                maskedTextBoxCodAmount.Visible = false;
                setUnitOfMeasurement();

                // Set the BOL Currencies
                textBoxBolEstimatedValue.Text = "0.00";
                maskedTextBoxCodAmount.Text = "0.00";
                maskedTextBoxQuotedPrice.Text = "0.00";
                maskedTextBoxActualPrice.Text = "0.00";
                textBoxEstimatedBolWeight.Text = "0";

                // Set the actual transit days
                textBoxEstimatedTransitDays.Text = "1";
                textBoxActualTransitDays.Text = "0";

                // Set the default payment type
                checkBoxFreightPrepaid.Checked = true;
                checkBoxCustomerInvoice.Checked = true;
                checkBoxPaymentCOD.Checked = false;

                // Were adding a new Bol, so intialize Containers, Pallets, Packages
                this.Containers = new List<CONTAINERS>();
                this.Pallets = new List<PALLETS>();
                this.Packages = new List<PACKAGES>();

                // Generate a unique BOL number
                this.BolNumber = Classes.Common.BolNumberGenerator.GenerateBOLNumber();
                textBoxBolNumber.Text = this.BolNumber;

            }
            else
            {
                LoadBol(this.BolId);
                buttonPrint.Enabled = true;
            }

            // Enable Event Handlers
            EnableEventHandlers();

        }

        protected void BolDialog_Shown(object sender, EventArgs e)
        {

            // Turn off the wait cursor
            Cursor = Cursors.Default;
        }

        #endregion
        #region Countries

        private async void LoadComboBoxDestinationCountriesAsync()
        {
            // Disable the Event Handler
            comboBoxDestinationCountries.SelectedIndexChanged -= ComboBoxDestinationCountries_SelectedIndexChanged;

            int countriesSourceCount = 0;

            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBoxDestinationCountries.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxDestinationCountries.DataSource = currentDataTable;
            }

            var getCountriesSource = await CountryRegion.Region.GetCountries();
            if (getCountriesSource != null)
            {
                countriesSourceCount = getCountriesSource.Count();

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

                dtCountriesSourced.AcceptChanges();

                comboBoxDestinationCountries.DataSource = dtCountriesSourced;
                comboBoxDestinationCountries.DisplayMember = "Key";
                comboBoxDestinationCountries.ValueMember = "Value";
                comboBoxDestinationCountries.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxDestinationCountries.DropDownStyle = ComboBoxStyle.DropDown;
                comboBoxDestinationCountries.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxDestinationCountries.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxDestinationCountries.SelectedIndex = 0;
            }

            // Enable the Event Handler
            comboBoxDestinationCountries.SelectedIndexChanged += ComboBoxDestinationCountries_SelectedIndexChanged;

        }

        protected async void ComboBoxDestinationCountries_SelectedIndexChanged(object? sender, EventArgs e)
        {
            DisableEventHandlers();

            if (comboBoxDestinationCountries.Items.Count > 0)
            {
                if (comboBoxDestinationCountries.SelectedIndex > 0)
                {
                    var countryCode = (comboBoxDestinationCountries.SelectedValue as dynamic).ToString();
                    if (countryCode != null)
                    {
                        countryCode = int.Parse(countryCode);
                        var shippers = LoadComboBoxShipperFromCountryCode(countryCode);
                        await Task.WhenAll(shippers);
                    }

                }

            }

            EnableEventHandlers();

        }

        protected async void ButtonReloadDestinationCountryShippers_ClickAsync(object? sender, EventArgs e)
        {
            DisableEventHandlers();

            var countryCode = (comboBoxDestinationCountries.SelectedValue as dynamic).ToString();
            if (countryCode != null)
            {
                var shippers = LoadComboBoxShipperFromCountryCode(countryCode);
                await Task.WhenAll(shippers);

            }

            EnableEventHandlers();
        }

        private async Task<int> LoadComboBoxShipperFromCountryCode(string? countryCode)
        {
            Cursor = Cursors.WaitCursor;

            int shipperCount = 0;

            if (shipperRepository != null)
            {
                List<SHIPPER> getCountryDestinationShippers = await shipperRepository.GetShippersByDestinationCountryAsync(countryCode);
                if (getCountryDestinationShippers != null)
                {

                    shipperCount = getCountryDestinationShippers.Count;

                    // New method of clearing the ComboBox               
                    DataTable currentDataTable = (DataTable)comboBoxShippers.DataSource;
                    if (currentDataTable != null)
                    {
                        currentDataTable.Clear();
                        comboBoxShippers.DataSource = currentDataTable;
                    }

                    var dtShippers = new DataTable("dtShippers");
                    dtShippers.Columns.Add(new DataColumn("Key"));
                    dtShippers.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtShippers.NewRow();
                    rsDefault[0] = "-- Make a selection --";
                    rsDefault[1] = 0;
                    dtShippers.Rows.Add(rsDefault);

                    foreach (var shipperItem in getCountryDestinationShippers)
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

            Cursor = Cursors.Default;

            return shipperCount;

        }

        #endregion
        #region RadioButtons

        private void RadioButtonLTL_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = true;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.LTL;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.LTL);

            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonFTL_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = true;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.FTL;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.FTL);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonAir_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = true;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.AIR;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.AIR);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonOcean_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = true;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.OCEAN;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.OCEAN);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonRailroad_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = true;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.RAIL;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.RAIL);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonLastMile_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = true;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.LASTMILE;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.LASTMILE);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonCourier_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = true;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.COURIER;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.COURIER);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private void RadioButtonArmouredCar_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = true;
            radioButtonShowAll.Checked = false;

            _shipperServices = ShipperServicesEnum.ARMOURED;
            _ = LoadComboBoxShippersByService(ShipperServicesEnum.ARMOURED);
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        private async void RadioButtonShowAll_CheckedChanged(object? sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();
            radioButtonLTL.Checked = false;
            radioButtonFTL.Checked = false;
            radioButtonAir.Checked = false;
            radioButtonOcean.Checked = false;
            radioButtonRailroad.Checked = false;
            radioButtonLastMile.Checked = false;
            radioButtonCourier.Checked = false;
            radioButtonArmouredCar.Checked = false;
            radioButtonShowAll.Checked = true;

            // Load up the ComboBox with All Shippers
            await LoadComboBoxShippersAll();
            EnableEventHandlers();

            Cursor = Cursors.Default;
        }

        #endregion
        #region EventHandlers

        private void DisableEventHandlers()
        {
            // Radio Buttons
            radioButtonLTL.CheckedChanged -= RadioButtonLTL_CheckedChanged;
            radioButtonFTL.CheckedChanged -= RadioButtonFTL_CheckedChanged;
            radioButtonAir.CheckedChanged -= RadioButtonAir_CheckedChanged;
            radioButtonOcean.CheckedChanged -= RadioButtonOcean_CheckedChanged;
            radioButtonRailroad.CheckedChanged -= RadioButtonRailroad_CheckedChanged;
            radioButtonLastMile.CheckedChanged -= RadioButtonLastMile_CheckedChanged;
            radioButtonCourier.CheckedChanged -= RadioButtonCourier_CheckedChanged;
            radioButtonArmouredCar.CheckedChanged -= RadioButtonArmouredCar_CheckedChanged;
            radioButtonShowAll.CheckedChanged -= RadioButtonShowAll_CheckedChanged;

            // ComboBoxes
            comboBoxShippers.SelectedIndexChanged -= ComboBoxShippers_SelectedIndexChanged;
            comboBoxDestinationCountries.SelectedIndexChanged -= ComboBoxDestinationCountries_SelectedIndexChanged;
            comboBoxVendors.SelectedIndexChanged -= ComboBoxVendors_SelectedIndexChanged;
            comboBoxVendorLocations.SelectedIndexChanged -= ComboBoxVendorLocations_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged -= ComboBoxCustomers_SelectedIndexChanged;
            comboBoxCustomerLocations.SelectedIndexChanged -= ComboBoxCustomerLocations_SelectedIndexChanged;
            comboBox3rdPartyBillling.SelectedIndexChanged -= ComboBox3rdPartyBillling_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            // Radio Buttons
            radioButtonLTL.CheckedChanged += RadioButtonLTL_CheckedChanged;
            radioButtonFTL.CheckedChanged += RadioButtonFTL_CheckedChanged;
            radioButtonAir.CheckedChanged += RadioButtonAir_CheckedChanged;
            radioButtonOcean.CheckedChanged += RadioButtonOcean_CheckedChanged;
            radioButtonRailroad.CheckedChanged += RadioButtonRailroad_CheckedChanged;
            radioButtonLastMile.CheckedChanged += RadioButtonLastMile_CheckedChanged;
            radioButtonCourier.CheckedChanged += RadioButtonCourier_CheckedChanged;
            radioButtonArmouredCar.CheckedChanged += RadioButtonArmouredCar_CheckedChanged;
            radioButtonShowAll.CheckedChanged += RadioButtonShowAll_CheckedChanged;

            // ComboBoxes
            comboBoxShippers.SelectedIndexChanged += ComboBoxShippers_SelectedIndexChanged;
            comboBoxDestinationCountries.SelectedIndexChanged += ComboBoxDestinationCountries_SelectedIndexChanged;
            comboBoxVendors.SelectedIndexChanged += ComboBoxVendors_SelectedIndexChanged;
            comboBoxVendorLocations.SelectedIndexChanged += ComboBoxVendorLocations_SelectedIndexChanged;
            comboBoxCustomers.SelectedIndexChanged += ComboBoxCustomers_SelectedIndexChanged;
            comboBoxCustomerLocations.SelectedIndexChanged += ComboBoxCustomerLocations_SelectedIndexChanged;
            comboBox3rdPartyBillling.SelectedIndexChanged += ComboBox3rdPartyBillling_SelectedIndexChanged;
        }



        #endregion
        #region Shipper

        private async void ButtonAddShipper_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var shipperDialogDIOwned = serviceProvider.CreateOwnedForm<ShipperDialog>();
                var shipperDialogDI = shipperDialogDIOwned.Form;

                shipperDialogDI.StartPosition = FormStartPosition.CenterScreen;
                shipperDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                shipperDialogDI.TopMost = true;
                shipperDialogDI.ShipperId = "ADD";
                shipperDialogDI.Refresh();

                if (shipperDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;
                    await LoadComboBoxShippersByService(_shipperServices);
                    Cursor = Cursors.Default;

                }
            }

        }

        private async Task<int> LoadComboBoxShippersAll()
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
                        rsDefault[0] = "-- Make a selection --";
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

        private async Task LoadComboBoxShippersByService(ShipperServicesEnum? serviceCode)
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

                var getShippers = await shipperRepository.GetShippersByServiceCodeAsync(serviceCode);
                if (getShippers != null)
                {
                    shippersCount = getShippers.Count();

                    var dtShippers = new DataTable("dtShippers");
                    dtShippers.Columns.Add(new DataColumn("Key"));
                    dtShippers.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtShippers.NewRow();
                    rsDefault[0] = shippersCount > 0 ? "-- Make a selection --" : "-- No shippers found --";
                    rsDefault[1] = 0;
                    dtShippers.Rows.Add(rsDefault);

                    foreach (var shipperItem in getShippers.OrderBy(ob => ob?.CompanyName))
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

        private async void ComboBoxShippers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxShippers.SelectedValue != null)
            {
                var shipperCode = (comboBoxShippers.SelectedValue as dynamic).ToString();
                if (shipperCode != null)
                {
                    if (shipperRepository != null)
                    {
                        var getShipper = await shipperRepository.GetOneShipperAsync(shipperCode);
                        if (getShipper != null)
                        {
                            labelShipperName.Text = getShipper.CompanyName;
                            labelShipperAddress.Text = getShipper.Address1;
                            labelShipperCity.Text = getShipper.City;
                            labelShipperRegion.Text = getShipper.RegionLongName;
                            labelShipperPostalCode.Text = getShipper.PostalCode;
                            labelShipperCountry.Text = getShipper.CountryLongName;
                            panelShipper.Visible = true;

                        }
                        else
                        {
                            panelShipper.Visible = false;
                        }
                    }

                }
            }
        }

        #endregion
        #region 3rdPartyBilling

        private async Task<int> LoadComboBox3rdPartyBillingAll()
        {

            int billingCount = 0;
            // New method of clearing the ComboBox               
            DataTable currentDataTable = (DataTable)comboBox3rdPartyBillling.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBox3rdPartyBillling.DataSource = currentDataTable;
            }

            if (billToAccountsRepository != null)
            {

                var getAllBillingAccounts = await billToAccountsRepository.GetAllBillToAccountsAsync();
                if (getAllBillingAccounts != null)
                {
                    billingCount = getAllBillingAccounts.Count();

                    if (billingCount > 0)
                    {

                        var dtBillToAccounts = new DataTable("dtBillToAccounts");
                        dtBillToAccounts.Columns.Add(new DataColumn("Key"));
                        dtBillToAccounts.Columns.Add(new DataColumn("Value"));

                        var rsDefault = dtBillToAccounts.NewRow();
                        rsDefault[0] = "-- None --";
                        rsDefault[1] = 0;
                        dtBillToAccounts.Rows.Add(rsDefault);

                        foreach (var billToItem in getAllBillingAccounts.OrderBy(ob => ob?.CompanyName))
                        {
                            if (billToItem != null)
                            {
                                var rsBillToItem = dtBillToAccounts.NewRow();
                                rsBillToItem[0] = billToItem.CompanyName;
                                rsBillToItem[1] = billToItem.BillToAccountId;
                                dtBillToAccounts.Rows.Add(rsBillToItem);
                            }
                        }

                        dtBillToAccounts.AcceptChanges();

                        comboBox3rdPartyBillling.DataSource = dtBillToAccounts;
                        comboBox3rdPartyBillling.DisplayMember = "Key";
                        comboBox3rdPartyBillling.ValueMember = "Value";
                        comboBox3rdPartyBillling.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBox3rdPartyBillling.DropDownStyle = ComboBoxStyle.DropDown;
                        comboBox3rdPartyBillling.AutoCompleteMode = AutoCompleteMode.Suggest;
                        comboBox3rdPartyBillling.AutoCompleteSource = AutoCompleteSource.ListItems;
                        comboBox3rdPartyBillling.SelectedIndex = 0;

                    }
                }

            }

            return billingCount;

        }

        private async void ButtonAddBilling_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var billToAccountDialogDIOwned = serviceProvider.CreateOwnedForm<BillToAccountDialog>();
                var billToAccountDialogDI = billToAccountDialogDIOwned.Form;
                billToAccountDialogDI.StartPosition = FormStartPosition.CenterScreen;
                billToAccountDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                billToAccountDialogDI.TopMost = true;
                billToAccountDialogDI.BillToAccountId = "ADD";
                billToAccountDialogDI.Refresh();

                if (billToAccountDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    var billToAccount = billToAccountDialogDI.BillToAccount;
                    string billToAccountId = billToAccountDialogDI.BillToAccountId;
                    if (billToAccountsRepository != null)
                    {
                        Task<int> loading = LoadComboBox3rdPartyBillingAll();
                        int billToAccountCount = await loading;
                        if (billToAccountCount > 0)
                        {
                            comboBox3rdPartyBillling.SelectedItem = billToAccountId;
                        }

                    }

                    Cursor = Cursors.Default;

                }
            }


        }

        private async void ComboBox3rdPartyBillling_SelectedIndexChanged(Object? sender, EventArgs e)
        {
            if (comboBox3rdPartyBillling.SelectedIndex > 0)
            {
                var billingPartyCode = (comboBox3rdPartyBillling.SelectedValue as dynamic).ToString();
                if (billingPartyCode != null)
                {
                    if (billToAccountsRepository != null)
                    {
                        var getBillToAccount = await billToAccountsRepository.GetOneBillToAccountAsync(billingPartyCode);
                        if (getBillToAccount != null)
                        {
                            this.Bol.BillToId = billingPartyCode;
                            this.Bol.BillToAccount = getBillToAccount;

                            labelBillToName.Text = getBillToAccount.CompanyName;
                            labelBillToAddress.Text = getBillToAccount.Address1;
                            labelBillToCity.Text = getBillToAccount.City;
                            labelBillToRegion.Text = getBillToAccount.RegionLongName;
                            labelBillToPostalCode.Text = getBillToAccount.PostalCode;
                            labelBillToCountry.Text = getBillToAccount.CountryLongName;

                            panelBillTo.Visible = true;

                            // Toggle the Payment Checkboxes
                            checkBoxFreightPrepaid.Checked = false;
                            checkBoxCustomerInvoice.Checked = false;
                            checkBoxPaymentCOD.Checked = false;

                        }

                    }
                }
            }
            else if (comboBox3rdPartyBillling.SelectedIndex == 0)
            {
                this.Bol.BillToId = null;
                this.Bol.BillToAccount = null;

                labelBillToName.Text = "";
                labelBillToAddress.Text = "";
                labelBillToCity.Text = "";
                labelBillToRegion.Text = "";
                labelBillToPostalCode.Text = "";
                labelBillToCountry.Text = "";

                panelBillTo.Visible = false;

                // Toggle the Payment Checkboxes
                checkBoxFreightPrepaid.Checked = true;
                checkBoxCustomerInvoice.Checked = true;
                checkBoxPaymentCOD.Checked = false;

            }

        }

        #endregion
        #region Payments

        private void CheckBoxFreightPrepaid_CheckedChanged(global::System.Object sender, global::System.EventArgs e)
        {
            if (checkBoxFreightPrepaid.Checked)
            {
                // COD
                checkBoxPaymentCOD.Checked = false;
                maskedTextBoxCodAmount.Visible = false;

                // Customer Invoices
                checkBoxCustomerInvoice.Checked = false;
            }
        }

        private void CheckBoxPaymentCOD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPaymentCOD.Checked == true)
            {
                // COD
                maskedTextBoxCodAmount.Visible = true;
                maskedTextBoxCodAmount.Enabled = true;

                // Freight Prepaid
                checkBoxFreightPrepaid.Checked = false;

                // Customer Invoices
                checkBoxCustomerInvoice.Checked = false;
            }
            else
            {
                maskedTextBoxCodAmount.Visible = false;
                maskedTextBoxCodAmount.Enabled = true;

                // Freight Prepaid
                checkBoxFreightPrepaid.Checked = true;

                // Customer Invoiced
                checkBoxCustomerInvoice.Checked = false;
            }
        }

        private void CheckBoxCustomerInvoice_CheckedChanged(global::System.Object sender, global::System.EventArgs e)
        {
            if (checkBoxCustomerInvoice.Checked == true)
            {
                // Freight Prepaid
                checkBoxFreightPrepaid.Checked = false;

                // COD
                checkBoxPaymentCOD.Checked = false;
                maskedTextBoxCodAmount.Visible = false;
                maskedTextBoxCodAmount.Enabled = false;

                // Customer Invoiced
                checkBoxCustomerInvoice.Checked = true;
            }
            else
            {
                checkBoxCustomerInvoice.Checked = false;
            }
        }

        private void CheckBoxShipFromAppointment_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShipFromAppointmentRequired.Checked == true)
            {
                panelShipFromAppointment.Visible = true;
                dateTimePickerShipFromAppointmentDate.Focus();
            }
            else
            {
                panelShipFromAppointment.Visible = false;
            }
        }

        private void TextBoxBolEstimatedValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only currency amounts are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxEstimatedBolWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted to represent weight.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void MaskedTextBoxCodAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only Currency Amounts are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
        #region Vendors/ShipFrom

        private async void ButtonAddVendorFrom_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var vendorDialogDIOwned = serviceProvider.CreateOwnedForm<VendorDialog>();
                var vendorDialogDI = vendorDialogDIOwned.Form;
                vendorDialogDI.StartPosition = FormStartPosition.CenterScreen;
                vendorDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                vendorDialogDI.TopMost = true;
                vendorDialogDI.VendorId = "ADD";
                vendorDialogDI.Refresh();

                if (vendorDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    var vendor = vendorDialogDI.Vendor;
                    string vendorId = vendorDialogDI.VendorId;
                    if (vendorRepository != null)
                    {
                        Task<int> loading = LoadComboBoxVendorsAsync();
                        int vendorsCount = await loading;
                        if (vendorsCount > 0)
                        {
                            comboBoxVendors.SelectedItem = vendorId;
                        }

                    }

                    Cursor = Cursors.Default;

                }
            }
        }

        private async Task<int> LoadComboBoxVendorsAsync()
        {
            int vendorsCount = 0;

            if (vendorRepository != null)
            {
                // New method of clearing the ComboBox               
                DataTable currentDataTable = (DataTable)comboBoxVendors.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxVendors.DataSource = currentDataTable;
                }

                var getAllVendors = await vendorRepository.GetAllVendorsAsync();
                if (getAllVendors != null)
                {

                    foreach (var vendor in getAllVendors)
                    {
                        if (vendor != null)
                        {

                            vendorsCount = getAllVendors.Count();

                            if (vendorsCount > 0)
                            {

                                var dtVendors = new DataTable("dtVendors");
                                dtVendors.Columns.Add(new DataColumn("Key"));
                                dtVendors.Columns.Add(new DataColumn("Value"));

                                var rsDefault = dtVendors.NewRow();
                                rsDefault[0] = "-- Make a selection --";
                                rsDefault[1] = 0;
                                dtVendors.Rows.Add(rsDefault);

                                foreach (var vendorItem in getAllVendors.OrderBy(ob => ob?.CompanyName))
                                {
                                    if (vendorItem != null)
                                    {
                                        var rsVendorItem = dtVendors.NewRow();
                                        rsVendorItem[0] = vendorItem.CompanyName;
                                        rsVendorItem[1] = vendorItem.VendorId;
                                        dtVendors.Rows.Add(rsVendorItem);
                                    }
                                }

                                dtVendors.AcceptChanges();

                                comboBoxVendors.DataSource = dtVendors;
                                comboBoxVendors.DisplayMember = "Key";
                                comboBoxVendors.ValueMember = "Value";
                                comboBoxVendors.AutoCompleteSource = AutoCompleteSource.ListItems;
                                comboBoxVendors.DropDownStyle = ComboBoxStyle.DropDown;
                                comboBoxVendors.AutoCompleteMode = AutoCompleteMode.Suggest;
                                comboBoxVendors.AutoCompleteSource = AutoCompleteSource.ListItems;

                                // Check if have vendors first
                                if (comboBoxVendors.Items.Count > 0)
                                {
                                    comboBoxVendors.SelectedIndex = 0;
                                }

                            }

                        }

                    }

                }

            }

            return vendorsCount;

        }

        private async void ComboBoxVendors_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxVendors.SelectedIndex > 0)
            {
                var vendorCode = (comboBoxVendors.SelectedValue as dynamic).ToString();
                if (vendorCode != null)
                {
                    if (vendorRepository != null)
                    {
                        var getVendor = await vendorRepository.GetOneVendorAsync(vendorCode);
                        if (getVendor != null)
                        {

                            // The new way to clear the list
                            DataTable currentDataTable = (DataTable)comboBoxVendorLocations.DataSource;
                            if (currentDataTable != null)
                            {
                                currentDataTable.Clear();
                                comboBoxVendorLocations.DataSource = currentDataTable;
                            }

                            var dtVendorShippingLocations = new DataTable("dtVendorShippingLocations");
                            dtVendorShippingLocations.Columns.Add(new DataColumn("Key"));
                            dtVendorShippingLocations.Columns.Add(new DataColumn("Value"));

                            if (getVendor.ShippingLocations != null)
                            {
                                List<SHIPPINGLOCATIONS> shippingLocations = getVendor.ShippingLocations;
                                if (shippingLocations.Count > 0)
                                {
                                    comboBoxVendorLocations.SelectedIndexChanged -= ComboBoxVendorLocations_SelectedIndexChanged;

                                    foreach (SHIPPINGLOCATIONS shippingLocation in shippingLocations)
                                    {
                                        if (shippingLocation != null)
                                        {
                                            var rsLocationItem = dtVendorShippingLocations.NewRow();
                                            rsLocationItem[0] = shippingLocation.Name;
                                            rsLocationItem[1] = shippingLocation.LocationId;
                                            dtVendorShippingLocations.Rows.Add(rsLocationItem);

                                            dtVendorShippingLocations.AcceptChanges();
                                            comboBoxVendorLocations.DataSource = dtVendorShippingLocations;

                                        }

                                    }

                                    if (shippingLocations.Count == 1)
                                    {
                                        comboBoxVendorLocations.SelectedIndex = 0;

                                        SHIPPINGLOCATIONS singleLocation = shippingLocations[0];
                                        if (singleLocation != null)
                                        {
                                            labelShipFromName.Text = singleLocation.Name;
                                            labelShipFromAddress.Text = singleLocation.Address1;
                                            labelShipFromCity.Text = singleLocation.City;
                                            labelShipFromRegion.Text = singleLocation.RegionName;
                                            labelShipFromPostalCode.Text = singleLocation.PostalCode;
                                            labelShipFromCountry.Text = singleLocation.CountryName;

                                            if (singleLocation.LiftGateRequired != null)
                                            {
                                                checkBoxShipFromLiftGateRequired.Checked = (bool)singleLocation.LiftGateRequired;
                                            }
                                        }

                                    }

                                    panelShipFromLocations.Visible = shippingLocations.Count > 0;
                                    panelShipFrom.Visible = true;

                                    comboBoxVendorLocations.SelectedIndexChanged += ComboBoxVendorLocations_SelectedIndexChanged;

                                }
                                else
                                {
                                    // Just assign the labels
                                    labelShipFromName.Text = getVendor.CompanyName;
                                    labelShipFromAddress.Text = getVendor.Address1;
                                    labelShipFromCity.Text = getVendor.City;
                                    labelShipFromRegion.Text = getVendor.RegionLongName;
                                    labelShipFromPostalCode.Text = getVendor.PostalCode;
                                    labelShipFromCountry.Text = getVendor.CountryLongName;

                                    checkBoxShipFromLiftGateRequired.Checked = getVendor.LiftgateRequired;

                                    // Generate a temporary shipping location called Main Location
                                    var rsLocationItem = dtVendorShippingLocations.NewRow();
                                    rsLocationItem[0] = "Main Location";
                                    rsLocationItem[1] = ObjectId.GenerateNewId().ToString();
                                    dtVendorShippingLocations.Rows.Add(rsLocationItem);

                                    dtVendorShippingLocations.AcceptChanges();
                                    comboBoxVendorLocations.DataSource = dtVendorShippingLocations;

                                    panelShipFromLocations.Visible = true;
                                    panelShipFrom.Visible = true;
                                }

                            }
                            else
                            {

                                labelShipFromName.Text = getVendor.CompanyName;
                                labelShipFromAddress.Text = getVendor.Address1;
                                labelShipFromCity.Text = getVendor.City;
                                labelShipFromRegion.Text = getVendor.RegionLongName;
                                labelShipFromPostalCode.Text = getVendor.PostalCode;
                                labelShipFromCountry.Text = getVendor.CountryLongName;

                                checkBoxShipFromLiftGateRequired.Checked = getVendor.LiftgateRequired;

                                // Generate a temporary shipping location called Main Location
                                var rsLocationItem = dtVendorShippingLocations.NewRow();
                                rsLocationItem[0] = "Main Location";
                                rsLocationItem[1] = ObjectId.GenerateNewId().ToString();
                                dtVendorShippingLocations.Rows.Add(rsLocationItem);

                                dtVendorShippingLocations.AcceptChanges();
                                comboBoxVendorLocations.DataSource = dtVendorShippingLocations;

                                panelShipFromLocations.Visible = true;
                                panelShipFrom.Visible = true;

                            }

                            dtVendorShippingLocations.AcceptChanges();

                            comboBoxVendorLocations.DataSource = dtVendorShippingLocations;
                            comboBoxVendorLocations.DisplayMember = "Key";
                            comboBoxVendorLocations.ValueMember = "Value";
                            comboBoxVendorLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                            comboBoxVendorLocations.DropDownStyle = ComboBoxStyle.DropDown;
                            comboBoxVendorLocations.AutoCompleteMode = AutoCompleteMode.Suggest;
                            comboBoxVendorLocations.AutoCompleteSource = AutoCompleteSource.ListItems;

                            if (comboBoxVendorLocations.Items.Count > 0)
                            {
                                comboBoxVendorLocations.SelectedIndex = 0;
                            }

                        }
                        else
                        {
                            panelShipFrom.Visible = false;
                        }
                    }

                }

            }
            else if (comboBoxVendors.SelectedIndex == 0)
            {
                // Clear the locations combobox               
                DataTable currentDataTable = (DataTable)comboBoxVendors.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxVendors.DataSource = currentDataTable;
                }

                labelShipFromName.Text = "";
                labelShipFromAddress.Text = "";
                labelShipFromCity.Text = "";
                labelShipFromRegion.Text = "";
                labelShipFromPostalCode.Text = "";
                labelShipFromCountry.Text = "";

                checkBoxShipFromLiftGateRequired.Checked = false;

                panelShipFromLocations.Visible = false;
                panelShipFrom.Visible = false;
            }

        }

        private void ComboBoxCustomerLocations_SelectedIndexChanged1(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void ComboBoxVendorLocations_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Make sure we have a user selected choice, and not an event driven choice
            if (comboBoxVendorLocations.SelectedIndex > 0)
            {
                var vendorId = (comboBoxVendors.SelectedValue as dynamic).ToString();
                var locationId = (comboBoxVendorLocations.SelectedValue as dynamic).ToString();

                if (vendorId != null && locationId != null)
                {

                    if (vendorRepository != null)
                    {
                        var getShippingLocation = await vendorRepository.GetVendorShippingLocationByLocationId(vendorId, locationId);
                        if (getShippingLocation != null)
                        {
                            // The ShippingAddress Model is diffrent than the Vendor Model defaults
                            labelShipFromName.Text = getShippingLocation.Name;
                            labelShipFromAddress.Text = getShippingLocation.Address1;
                            labelShipFromCity.Text = getShippingLocation.City;
                            labelShipFromRegion.Text = getShippingLocation.RegionName;
                            labelShipFromPostalCode.Text = getShippingLocation.PostalCode;
                            labelShipFromCountry.Text = getShippingLocation.CountryName;

                            panelShipFromLocations.Visible = true;
                            panelShipFrom.Visible = true;

                        }
                    }

                }
            }

        }

        private void MaskedTextBoxShipFromAppointmentTime_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxShipFromAppointmentTime.SelectAll();
            });
        }

        #endregion
        #region Customers/ShipTo

        private async void ButtonAddShipTo_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var customerDialogDIOwned = serviceProvider.CreateOwnedForm<CustomerDialog>();
                var customerDialogDI = customerDialogDIOwned.Form;
                customerDialogDI.StartPosition = FormStartPosition.CenterScreen;
                customerDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                customerDialogDI.TopMost = true;
                customerDialogDI.CustomerId = "ADD";
                customerDialogDI.Refresh();

                if (customerDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    var customer = customerDialogDI.Customer;
                    string customerId = customerDialogDI.CustomerId;
                    if (customerRepository != null)
                    {
                        Task<int> loading = LoadComboBoxCustomersAsync();
                        int customersCount = await loading;
                        if (customersCount > 0)
                        {
                            comboBoxCustomers.SelectedItem = customerId;
                        }
                    }

                    Cursor = Cursors.Default;

                }
            }
        }


        private void CheckBoxCustomerAppointment_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShipToAppointmentRequired.Checked)
            {
                panelShipToAppointment.Visible = true;
                dateTimePickerShipToAppointmentDate.Focus();
            }
            else
            {
                panelShipToAppointment.Visible = false;
            }
        }

        private async Task<int> LoadComboBoxCustomersAsync()
        {
            int customersCount = 0;

            if (customerRepository != null)
            {
                // New method of clearing the ComboBox               
                DataTable currentDataTable = (DataTable)comboBoxCustomers.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxCustomers.DataSource = currentDataTable;
                }

                var getAllCustomers = await customerRepository.GetAllCustomersAsync();
                if (getAllCustomers != null)
                {

                    foreach (var customer in getAllCustomers)
                    {
                        if (customer != null)
                        {

                            customersCount = getAllCustomers.Count();

                            if (customersCount > 0)
                            {

                                var dtCustomers = new DataTable("dtCustomers");
                                dtCustomers.Columns.Add(new DataColumn("Key"));
                                dtCustomers.Columns.Add(new DataColumn("Value"));

                                var rsDefault = dtCustomers.NewRow();
                                rsDefault[0] = "-- Make a selection --";
                                rsDefault[1] = 0;
                                dtCustomers.Rows.Add(rsDefault);

                                foreach (var customerItem in getAllCustomers.OrderBy(ob => ob?.CompanyName))
                                {
                                    if (customerItem != null)
                                    {
                                        var rsCustomerItem = dtCustomers.NewRow();
                                        rsCustomerItem[0] = customerItem.CompanyName;
                                        rsCustomerItem[1] = customerItem.CustomerId;
                                        dtCustomers.Rows.Add(rsCustomerItem);
                                    }
                                }

                                dtCustomers.AcceptChanges();

                                comboBoxCustomers.DataSource = dtCustomers;
                                comboBoxCustomers.DisplayMember = "Key";
                                comboBoxCustomers.ValueMember = "Value";
                                comboBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                                comboBoxCustomers.DropDownStyle = ComboBoxStyle.DropDown;
                                comboBoxCustomers.AutoCompleteMode = AutoCompleteMode.Suggest;
                                comboBoxCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;
                                comboBoxCustomers.SelectedIndex = 0;

                            }
                        }

                    }

                }

            }

            return customersCount;

        }

        private async void ComboBoxCustomers_SelectedIndexChanged(object? sender, EventArgs e)
        {

            if (comboBoxCustomers.SelectedIndex > 0)
            {
                var customerCode = (comboBoxCustomers.SelectedValue as dynamic).ToString();
                if (customerCode != null)
                {
                    if (customerRepository != null)
                    {
                        var getCustomer = await customerRepository.GetOneCustomerAsync(customerCode);
                        if (getCustomer != null)
                        {
                            // The new way to clear the list
                            DataTable currentDataTable = (DataTable)comboBoxCustomerLocations.DataSource;
                            if (currentDataTable != null)
                            {
                                currentDataTable.Clear();
                                comboBoxCustomerLocations.DataSource = currentDataTable;
                            }

                            var dtCustomerShippingLocations = new DataTable("dtCustomerShippingLocations");
                            dtCustomerShippingLocations.Columns.Add(new DataColumn("Key"));
                            dtCustomerShippingLocations.Columns.Add(new DataColumn("Value"));

                            if (getCustomer.ShippingLocations != null)
                            {
                                List<SHIPPINGLOCATIONS> shippingLocations = getCustomer.ShippingLocations;
                                if (shippingLocations.Count > 0)
                                {
                                    comboBoxCustomerLocations.SelectedIndexChanged -= ComboBoxCustomerLocations_SelectedIndexChanged;

                                    foreach (SHIPPINGLOCATIONS shippingLocation in shippingLocations)
                                    {
                                        if (shippingLocation != null)
                                        {
                                            var rsLocationItem = dtCustomerShippingLocations.NewRow();
                                            rsLocationItem[0] = shippingLocation.Name;
                                            rsLocationItem[1] = shippingLocation.LocationId;
                                            dtCustomerShippingLocations.Rows.Add(rsLocationItem);

                                            dtCustomerShippingLocations.AcceptChanges();
                                            comboBoxCustomerLocations.DataSource = dtCustomerShippingLocations;
                                        }

                                    }

                                    if (shippingLocations.Count == 1)
                                    {
                                        comboBoxCustomerLocations.SelectedIndex = 0;

                                        SHIPPINGLOCATIONS singleLocation = shippingLocations[0];
                                        labelShipToName.Text = singleLocation.Name;
                                        labelShipToAddress.Text = singleLocation.Address1;
                                        labelShipToCity.Text = singleLocation.City;
                                        labelShipToRegion.Text = singleLocation.RegionName;
                                        labelShipToPostalCode.Text = singleLocation.PostalCode;
                                        labelShipToCountry.Text = singleLocation.CountryName;

                                        if (singleLocation.LiftGateRequired != null)
                                        {
                                            checkBoxShipToLiftGateRequired.Checked = (bool)singleLocation.LiftGateRequired;
                                        }

                                    }

                                    panelShipToLocations.Visible = shippingLocations.Count > 0;
                                    panelShipTo.Visible = true;

                                    comboBoxCustomerLocations.SelectedIndexChanged += ComboBoxCustomerLocations_SelectedIndexChanged;

                                }
                                else
                                {

                                    // Just populate the labels
                                    labelShipToName.Text = getCustomer.CompanyName;
                                    labelShipToAddress.Text = getCustomer.Address1;
                                    labelShipToCity.Text = getCustomer.City;
                                    labelShipToRegion.Text = getCustomer.RegionLongName;
                                    labelShipToPostalCode.Text = getCustomer.PostalCode;
                                    labelShipToCountry.Text = getCustomer.CountryLongName;

                                    checkBoxShipToLiftGateRequired.Checked = getCustomer.LiftgateRequired;

                                    // Generate a temporary shipping location here called Main Location
                                    var rsLocationItem = dtCustomerShippingLocations.NewRow();
                                    rsLocationItem[0] = "Main Location";
                                    rsLocationItem[1] = ObjectId.GenerateNewId().ToString();
                                    dtCustomerShippingLocations.Rows.Add(rsLocationItem);

                                    dtCustomerShippingLocations.AcceptChanges();
                                    comboBoxCustomerLocations.DataSource = dtCustomerShippingLocations;

                                    panelShipToLocations.Visible = true;
                                    panelShipTo.Visible = true;
                                }

                            }
                            else
                            {
                                labelShipToName.Text = getCustomer.CompanyName;
                                labelShipToAddress.Text = getCustomer.Address1;
                                labelShipToCity.Text = getCustomer.City;
                                labelShipToRegion.Text = getCustomer.RegionLongName;
                                labelShipToPostalCode.Text = getCustomer.PostalCode;
                                labelShipToCountry.Text = getCustomer.CountryLongName;

                                checkBoxShipToLiftGateRequired.Checked = getCustomer.LiftgateRequired;

                                // Consider adding a shipping location here
                                var rsLocationItem = dtCustomerShippingLocations.NewRow();
                                rsLocationItem[0] = "Main Location";
                                rsLocationItem[1] = ObjectId.GenerateNewId().ToString();
                                dtCustomerShippingLocations.Rows.Add(rsLocationItem);

                                dtCustomerShippingLocations.AcceptChanges();
                                comboBoxCustomerLocations.DataSource = dtCustomerShippingLocations;

                                panelShipToLocations.Visible = true;
                                panelShipTo.Visible = true;
                            }

                            dtCustomerShippingLocations.AcceptChanges();

                            comboBoxCustomerLocations.DataSource = dtCustomerShippingLocations;
                            comboBoxCustomerLocations.DisplayMember = "Key";
                            comboBoxCustomerLocations.ValueMember = "Value";
                            comboBoxCustomerLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                            comboBoxCustomerLocations.DropDownStyle = ComboBoxStyle.DropDown;
                            comboBoxCustomerLocations.AutoCompleteMode = AutoCompleteMode.Suggest;
                            comboBoxCustomerLocations.AutoCompleteSource = AutoCompleteSource.ListItems;

                            if (comboBoxCustomerLocations.Items.Count > 0)
                            {
                                comboBoxCustomerLocations.SelectedIndex = 0;
                            }

                        }
                        else
                        {
                            panelShipTo.Visible = false;
                        }
                    }

                }

            }
            else if (comboBoxCustomers.SelectedIndex == 0)
            {
                // Clear the lcoation list
                DataTable currentDataTable = (DataTable)comboBoxCustomerLocations.DataSource;
                if (currentDataTable != null)
                {
                    currentDataTable.Clear();
                    comboBoxCustomerLocations.DataSource = currentDataTable;
                }

                labelShipToName.Text = "";
                labelShipToAddress.Text = "";
                labelShipToCity.Text = "";
                labelShipToRegion.Text = "";
                labelShipToPostalCode.Text = "";
                labelShipToCountry.Text = "";

                checkBoxShipToLiftGateRequired.Checked = false;

                panelShipToLocations.Visible = false;
                panelShipTo.Visible = false;
            }

        }

        private async void ComboBoxCustomerLocations_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Make sure we have a user selected choice, and not an event driven choice
            if (comboBoxCustomerLocations.SelectedIndex > 0)
            {
                var customerId = (comboBoxCustomers.SelectedValue as dynamic).ToString();
                var locationId = (comboBoxCustomerLocations.SelectedValue as dynamic).ToString();

                if (customerId != null && locationId != null)
                {

                    if (customerRepository != null)
                    {
                        var getShippingLocation = await customerRepository.GetCustomerShippingLocationByLocationId(customerId, locationId);
                        if (getShippingLocation != null)
                        {
                            // The ShippingAddress Model is diffrent than the Vendor Model defaults
                            labelShipToName.Text = getShippingLocation.Name;
                            labelShipToAddress.Text = getShippingLocation.Address1;
                            labelShipToCity.Text = getShippingLocation.City;
                            labelShipToRegion.Text = getShippingLocation.RegionName;
                            labelShipToPostalCode.Text = getShippingLocation.PostalCode;
                            labelShipToCountry.Text = getShippingLocation.CountryName;

                            panelShipToLocations.Visible = true;
                            panelShipTo.Visible = true;

                        }
                    }

                }
            }

        }

        private void MaskedTextBoxShipToAppointmentTime_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxShipToAppointmentTime.SelectAll();
            });
        }

        #endregion
        #region TransitTimes

        private void DateTimePickerShipDate_ValueChanged(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerDeliveryDate_ValueChanged(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerShipDate_Leave(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void DateTimePickerDeliveryDate_Leave(object sender, EventArgs e)
        {
            var pickupDate = dateTimePickerShipDate.Value.Date;
            var deliveryDate = dateTimePickerDeliveryDate.Value.Date;

            var days = (deliveryDate - pickupDate).TotalDays;
            textBoxEstimatedTransitDays.Text = days.ToString();

        }

        private void TextBoxEstimatedTransitDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted to represent days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxActualTransitDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted to represent days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
        #region Packages

        private void ButtonAddPackage_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var packageDialogDIOwned = serviceProvider.CreateOwnedForm<PackageDialog>();
                var packageDialogDI = packageDialogDIOwned.Form;
                packageDialogDI.StartPosition = FormStartPosition.CenterScreen;
                packageDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                packageDialogDI.TopMost = true;
                packageDialogDI.PackageId = "ADD";
                packageDialogDI.Refresh();

                if (packageDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    var package = packageDialogDI.Package;
                    string packageId = packageDialogDI.PackageId;
                    if (package != null)
                    {
                        this.Packages ??= new List<PACKAGES>();
                        this.Packages.Add(package);

                        // Calculate the total weight
                        var estimatedPackageWeight = this.Packages.Sum(p => p.Weight);
                        var estimatedPalletWeight = this.Pallets.Sum(p => p.Weight);
                        var estimatedWeight = estimatedPackageWeight + estimatedPalletWeight;
                        textBoxEstimatedBolWeight.Text = estimatedWeight.ToString();
                        labelBolTotalWeight.Text = estimatedWeight.ToString();

                        LoadListViewPackages(this.Packages);
                        setCalculatedTotalWeight();
                        setCalculatedBolValue();

                    }

                    // Toggle the Location change flag
                    labelChangePending.Visible = true;
                    pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOn65;

                    Cursor = Cursors.Default;

                }
            }
        }

        private void ButtonEditPackage_Click(object sender, EventArgs e)
        {
            if (listViewPackages.SelectedItems.Count == 1)
            {
                var listItem = listViewPackages.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && this.Packages != null)
                {
                    if (this.Packages.Count > 0)
                    {
                        var packageObject = this.Packages.Where(p => p.PackageID == objectId).FirstOrDefault();
                        if (packageObject != null)
                        {
                            using var packageDialogDIOwned = serviceProvider.CreateOwnedForm<PackageDialog>();
                var packageDialogDI = packageDialogDIOwned.Form;
                            packageDialogDI.StartPosition = FormStartPosition.CenterScreen;
                            packageDialogDI.PackageId = objectId;
                            packageDialogDI.Package = packageObject;
                            if (packageDialogDI.ShowDialog() == DialogResult.OK)
                            {
                                var package = packageDialogDI.Package;
                                if (package != null)
                                {
                                    // We need to update this package in this.Packages
                                    this.Packages.Remove(packageObject);
                                    this.Packages.Add(package);

                                    // Calculate the total weight
                                    var estimatedPackageWeight = this.Packages.Sum(p => p.Weight);
                                    var estimatedPalletWeight = this.Pallets.Sum(p => p.Weight);
                                    var estimatedWeight = estimatedPackageWeight + estimatedPalletWeight;
                                    textBoxEstimatedBolWeight.Text = estimatedWeight.ToString();
                                    labelBolTotalWeight.Text = estimatedWeight.ToString();

                                    Application.DoEvents();
                                    this.LoadListViewPackages(this.Packages);
                                    setCalculatedTotalWeight();
                                    setCalculatedBolValue();
                                }
                            }
                        }

                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a package selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.DoEvents();
        }

        private void ButtonRemovePackage_Click(object sender, EventArgs e)
        {
            if (listViewPackages.SelectedItems.Count == 1)
            {
                var listItem = listViewPackages.SelectedItems[0];
                string objectId = listItem.SubItems[0].Text;
                string packageName = listItem.SubItems[1].Text;

                var result = MessageBox.Show("Are you sure you want to remove " + packageName + "?", "Packages", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    int packageIndex = this.Packages.FindIndex(a => a.PackageID == objectId);
                    this.Packages.RemoveAt(packageIndex);
                    setCalculatedTotalWeight();
                    setCalculatedBolValue();

                    listViewPackages.Items.Clear();

                    foreach (PACKAGES package in this.Packages)
                    {
                        if (package != null)
                        {

                            var item1 = new ListViewItem(package.PackageID)
                            {
                                Checked = false,
                                ImageIndex = listViewPallets.Items.Count + 1
                            };

                            item1.SubItems.Add(package.PackageDescription);
                            item1.SubItems.Add(package.UnitCount.ToString());
                            item1.SubItems.Add(package.Weight.ToString());

                            listViewPackages.Items.Add(item1);

                        }

                    }

                    Cursor = Cursors.Default;
                }

            }
        }

        private void ListViewPackages_DoubleClick(object sender, EventArgs e)
        {
            buttonEditPackage.PerformClick();
            Application.DoEvents();
        }

        private void ListViewPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditPackage.Enabled = true;
            buttonRemovePackage.Enabled = true;
        }

        private void ListViewPackagesColumn_Click(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewPackages.ListViewItemSorter;
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
            this.listViewPackages.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private int LoadListViewPackages(List<PACKAGES>? packages)
        {
            int packageCount = 0;

            if (packages != null)
            {
                if (packages.Count > 0)
                {
                    listViewPackages.Items.Clear();

                    foreach (var package in packages)
                    {
                        if (package != null)
                        {
                            packageCount++;

                            var item1 = new ListViewItem(package.PackageID)
                            {
                                Checked = false,
                                ImageIndex = packageCount
                            };

                            item1.SubItems.Add(package.PackageDescription);
                            item1.SubItems.Add(package.ClassCode != null ? package.ClassCode.CodeNumber.ToString() : "NA");
                            item1.SubItems.Add(package.UnitCount.ToString());
                            item1.SubItems.Add(package.Weight.ToString());
                            listViewPackages.Items.Add(item1);

                        }

                    }
                }
            }

            return packageCount;

        }

        #endregion
        #region Pallets

        private void ButtonAddPallet_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var palletDialogDIOwned = serviceProvider.CreateOwnedForm<PalletDialog>();
                var palletDialogDI = palletDialogDIOwned.Form;
                palletDialogDI.StartPosition = FormStartPosition.CenterScreen;
                palletDialogDI.FormBorderStyle = FormBorderStyle.FixedDialog;
                palletDialogDI.TopMost = true;
                palletDialogDI.PalletId = "ADD";
                palletDialogDI.Refresh();

                if (palletDialogDI.ShowDialog() == DialogResult.OK)
                {

                    Cursor = Cursors.WaitCursor;

                    var pallet = palletDialogDI.Pallet;
                    string palletId = palletDialogDI.PalletId;

                    if (pallet != null)
                    {

                        this.Pallets ??= new List<PALLETS>();
                        this.Pallets.Add(pallet);

                        // Calculate the total weight
                        var estimatedPackageWeight = this.Packages.Sum(p => p.Weight);
                        var estimatedPalletWeight = this.Pallets.Sum(p => p.Weight);
                        var estimatedWeight = estimatedPackageWeight + estimatedPalletWeight;
                        textBoxEstimatedBolWeight.Text = estimatedWeight.ToString();
                        labelBolTotalWeight.Text = estimatedWeight.ToString();

                        LoadListViewPallets(this.Pallets);
                        setCalculatedTotalWeight();
                        setCalculatedBolValue();

                    }

                    // Toggle the Location change flag
                    labelChangePending.Visible = true;
                    pictureBoxUpdateFlag.Image = Properties.Resources.updateFlagOn65;

                    Cursor = Cursors.Default;

                }
            }
        }

        private void ButtonEditPallet_Click(object sender, EventArgs e)
        {
            if (listViewPallets.SelectedItems.Count == 1)
            {
                var listItem = listViewPallets.SelectedItems[0];
                var objectId = listItem.SubItems[0].Text;

                if (serviceProvider != null && this.Pallets != null)
                {
                    if (this.Pallets.Count > 0)
                    {
                        var palletObject = this.Pallets.Where(p => p.PalletID == objectId).FirstOrDefault();
                        if (palletObject != null)
                        {
                            using var palletDialogDIOwned = serviceProvider.CreateOwnedForm<PalletDialog>();
                var palletDialogDI = palletDialogDIOwned.Form;
                            palletDialogDI.StartPosition = FormStartPosition.CenterScreen;
                            palletDialogDI.PalletId = objectId;
                            palletDialogDI.Pallet = palletObject;
                            if (palletDialogDI.ShowDialog() == DialogResult.OK)
                            {
                                var pallet = palletDialogDI.Pallet;
                                if (pallet != null)
                                {
                                    // We need to update the pallet source
                                    this.Pallets.Remove(palletObject);
                                    this.Pallets.Add(pallet);

                                    // Calculate the total weight
                                    var estimatedPackageWeight = this.Packages.Sum(p => p.Weight);
                                    var estimatedPalletWeight = this.Pallets.Sum(p => p.Weight);
                                    var estimatedWeight = estimatedPackageWeight + estimatedPalletWeight;
                                    textBoxEstimatedBolWeight.Text = estimatedWeight.ToString();
                                    labelBolTotalWeight.Text = estimatedWeight.ToString();

                                    Application.DoEvents();
                                    this.LoadListViewPallets(this.Pallets);
                                    setCalculatedTotalWeight();
                                    setCalculatedBolValue();
                                }
                            }
                        }
                    }

                }

            }
            else
            {
                MessageBox.Show("You must make a pallet selection in the list view", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ButtonRemovePallet_Click(object sender, EventArgs e)
        {
            if (listViewPallets.SelectedItems.Count == 1)
            {
                var listItem = listViewPallets.SelectedItems[0];
                string objectId = listItem.SubItems[0].Text;
                string palletName = listItem.SubItems[1].Text;

                var result = MessageBox.Show("Are you sure you want to remove " + palletName + "?", "Pallets", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    int palletIndex = this.Pallets.FindIndex(a => a.PalletID == objectId);
                    this.Pallets.RemoveAt(palletIndex);
                    setCalculatedTotalWeight();
                    setCalculatedBolValue();

                    listViewPallets.Items.Clear();

                    foreach (PALLETS pallet in this.Pallets)
                    {
                        if (pallet != null)
                        {

                            var item1 = new ListViewItem(pallet.PalletID)
                            {
                                Checked = false,
                                ImageIndex = listViewPallets.Items.Count + 1
                            };

                            item1.SubItems.Add(pallet.PalletDescription);
                            item1.SubItems.Add(pallet.BoxCount.ToString());
                            item1.SubItems.Add(pallet.UnitCount.ToString());
                            item1.SubItems.Add(pallet.Weight.ToString());

                            listViewPallets.Items.Add(item1);

                        }

                    }

                    Cursor = Cursors.Default;
                }

            }

        }

        private void ListViewPallets_DoubleClick(object sender, EventArgs e)
        {
            buttonEditPallet.PerformClick();
        }


        private void ListViewPallets_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditPallet.Enabled = true;
            buttonRemovePallet.Enabled = true;
        }

        private void ListViewPalletsColumn_Click(object sender, ColumnClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Determine whether the column is the same as the last column clicked.
            var comparer = (ListViewColumnSorter)listViewPallets.ListViewItemSorter;
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
            this.listViewPallets.Sort();
            Application.DoEvents();

            Cursor = Cursors.Arrow;
        }

        private int LoadListViewPallets(List<PALLETS>? pallets)
        {
            int palletCount = 0;

            if (pallets != null)
            {
                if (pallets.Count > 0)
                {
                    listViewPallets.Items.Clear();

                    foreach (PALLETS pallet in pallets)
                    {
                        if (pallet != null)
                        {

                            var item1 = new ListViewItem(pallet.PalletID)
                            {
                                Checked = false,
                                ImageIndex = listViewPallets.Items.Count + 1
                            };

                            item1.SubItems.Add(pallet.PalletDescription);
                            item1.SubItems.Add(pallet.ClassCode != null ? pallet.ClassCode.CodeNumber.ToString() : "NA");
                            item1.SubItems.Add(pallet.BoxCount.ToString());
                            item1.SubItems.Add(pallet.UnitCount.ToString());
                            item1.SubItems.Add(pallet.Weight.ToString());

                            listViewPallets.Items.Add(item1);

                        }

                    }
                }
            }

            return palletCount;

        }

        #endregion
        #region Containers

        private void LoadListViewContainers(List<CONTAINERS> containers)
        {


        }

        private void ButtonAddContainer_Click(object sender, EventArgs e)
        {

        }

        #endregion
        #region ComboBoxes



        #endregion
        #region ListViews

        private void SetListviewContainers()
        {
            listViewContainers.Clear();
            listViewContainers.Visible = true;
            listViewContainers.Items.Clear();
            listViewContainers.Columns.Clear();

            // Set ListView Parameters
            listViewContainers.Cursor = Cursors.Hand;
            listViewContainers.View = View.Details;
            listViewContainers.LabelEdit = false;
            listViewContainers.AllowColumnReorder = true;
            listViewContainers.CheckBoxes = false;
            listViewContainers.FullRowSelect = true;
            listViewContainers.GridLines = true;
            listViewContainers.Scrollable = true;
            listViewContainers.MultiSelect = false;
            listViewContainers.OwnerDraw = false;
            listViewContainers.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewContainers.Columns.Add("ContainerId", 0, HorizontalAlignment.Center);
            listViewContainers.Columns.Add("Description", 200, HorizontalAlignment.Left);
            listViewContainers.Columns.Add("Boxes", 60, HorizontalAlignment.Left);
            listViewContainers.Columns.Add("Items", 60, HorizontalAlignment.Left);
            listViewContainers.Columns.Add("Weight", 80, HorizontalAlignment.Left);

        }

        private void SetListviewPallets()
        {
            listViewPallets.Clear();
            listViewPallets.Visible = true;
            listViewPallets.Items.Clear();
            listViewPallets.Columns.Clear();

            // Set ListView Parameters
            listViewPallets.Cursor = Cursors.Hand;
            listViewPallets.View = View.Details;
            listViewPallets.LabelEdit = false;
            listViewPallets.AllowColumnReorder = true;
            listViewPallets.CheckBoxes = false;
            listViewPallets.FullRowSelect = true;
            listViewPallets.GridLines = true;
            listViewPallets.Scrollable = true;
            listViewPallets.MultiSelect = false;
            listViewPallets.OwnerDraw = false;
            listViewPallets.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewPallets.Columns.Add("PackageId", 0, HorizontalAlignment.Center);
            listViewPallets.Columns.Add("Description", 180, HorizontalAlignment.Left);
            listViewPallets.Columns.Add("Class", 60, HorizontalAlignment.Left);
            listViewPallets.Columns.Add("Boxes", 60, HorizontalAlignment.Left);
            listViewPallets.Columns.Add("Items", 60, HorizontalAlignment.Left);
            listViewPallets.Columns.Add("Weight", 80, HorizontalAlignment.Left);

        }

        private void SetListviewPackages()
        {
            listViewPackages.Clear();
            listViewPackages.Visible = true;
            listViewPackages.Items.Clear();
            listViewPackages.Columns.Clear();

            // Set ListView Parameters
            listViewPackages.Cursor = Cursors.Hand;
            listViewPackages.View = View.Details;
            listViewPackages.LabelEdit = false;
            listViewPackages.AllowColumnReorder = true;
            listViewPackages.CheckBoxes = false;
            listViewPackages.FullRowSelect = true;
            listViewPackages.GridLines = true;
            listViewPackages.Scrollable = true;
            listViewPackages.MultiSelect = false;
            listViewPackages.OwnerDraw = false;
            listViewPackages.Sorting = SortOrder.Ascending;

            // Create Columns and assign the column widths
            listViewPackages.Columns.Add("PackageId", 0, HorizontalAlignment.Center);
            listViewPackages.Columns.Add("Name", 200, HorizontalAlignment.Left);
            listViewPackages.Columns.Add("Class", 60, HorizontalAlignment.Left);
            listViewPackages.Columns.Add("Items", 60, HorizontalAlignment.Left);
            listViewPackages.Columns.Add("Weight", 80, HorizontalAlignment.Left);

        }

        private void ButtonContainersExpandCollapse_Click(object sender, EventArgs e)
        {
            if (!groupBoxContainersExpand)
            {
                groupBoxContainers.Location = new System.Drawing.Point(739, 173);
                groupBoxContainers.Size = new System.Drawing.Size(460, 758);
                groupBoxContainers.BringToFront();
                buttonContainersExpandCollapse.Image = Properties.Resources.Collapse28;
                groupBoxContainersExpand = true;
            }
            else
            {
                groupBoxContainers.Location = new System.Drawing.Point(739, 173);
                groupBoxContainers.Size = new System.Drawing.Size(460, 226);
                groupBoxContainers.SendToBack();
                buttonContainersExpandCollapse.Image = Properties.Resources.Expand28;
                groupBoxContainersExpand = false;

            }

            Application.DoEvents();

        }

        private void ButtonPalletsExpandCollapse_Click(object sender, EventArgs e)
        {
            if (!groupBoxPalletsExpand)
            {
                groupBoxPallets.Location = new System.Drawing.Point(739, 173);
                groupBoxPallets.Size = new System.Drawing.Size(460, 758);
                groupBoxPallets.BringToFront();
                buttonPalletsExpandCollapse.Image = Properties.Resources.Collapse28;
                groupBoxPalletsExpand = true;
            }
            else
            {
                groupBoxPallets.Location = new System.Drawing.Point(739, 418);
                groupBoxPallets.Size = new System.Drawing.Size(460, 231);
                buttonPalletsExpandCollapse.Image = Properties.Resources.Expand28;
                groupBoxPalletsExpand = false;
            }

            Application.DoEvents();

        }

        private void ButtonPackagesExpandCollapse_Click(object sender, EventArgs e)
        {
            if (!groupBoxPackagesExpand)
            {
                groupBoxPackages.Location = new System.Drawing.Point(739, 173);
                groupBoxPackages.Size = new System.Drawing.Size(460, 758);
                groupBoxPackages.BringToFront();
                buttonPackagesExpandCollapse.Image = Properties.Resources.Collapse28;
                groupBoxPackagesExpand = true;
            }
            else
            {
                groupBoxPackages.Location = new System.Drawing.Point(739, 672);
                groupBoxPackages.Size = new System.Drawing.Size(460, 259);
                buttonPackagesExpandCollapse.Image = Properties.Resources.Expand28;
                groupBoxPackagesExpand = false;
            }

            Application.DoEvents();

        }

        #endregion
        #region OKCancel

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            bool validate = await SaveBol();
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

        private async void LoadBol(string bolId)
        {
            if (bolId != null)
            {
                if (bolRepository != null)
                {
                    var getBol = await bolRepository.GetOneBillOfLaddingAsync(bolId);
                    if (getBol != null)
                    {

                        // Hold a copy of this Bol in memory
                        this.Bol = getBol;

                        // Assign the Bol Number
                        if (getBol.BolNumber != null)
                        {
                            this.BolNumber = getBol.BolNumber;
                            textBoxBolNumber.Text = getBol.BolNumber.ToString();
                        }
                        else
                        {
                            this.BolNumber = Classes.Common.BolNumberGenerator.GenerateBOLNumber();
                            if (this.BolNumber != null)
                            {
                                textBoxBolNumber.Text = this.BolNumber;
                            }
                        }

                        // Shipment Type
                        if (this.Bol.ShipmentType != null)
                        {                            
                            switch (this.Bol.ShipmentType)
                            {
                                case "LTL":
                                    radioButtonLTL.Checked = true;
                                    break;

                                case "FTL":
                                    radioButtonFTL.Checked = true;
                                    break;

                                case "AIR":
                                    radioButtonAir.Checked = true;
                                    break;

                                case "OCEAN":
                                    radioButtonOcean.Checked = true;
                                    break;

                                case "RAIL":
                                    radioButtonRailroad.Checked = true;
                                    break;

                                case "LAST MILE":
                                    radioButtonLastMile.Checked = true;
                                    break;

                                case "COURIER":
                                    radioButtonCourier.Checked = true;
                                    break;

                                case "ARMOURED CAR":
                                    radioButtonArmouredCar.Checked = true;
                                    break;
                            }
                        }

                        // Shipper
                        comboBoxShippers.SelectedValue = getBol.ShipperId;
                        if (getBol.Shipper != null)
                        {
                            labelShipperName.Text = getBol.Shipper.CompanyName;
                            labelShipperAddress.Text = getBol.Shipper.Address1;
                            labelShipperCity.Text = getBol.Shipper.City;
                            labelShipperRegion.Text = getBol.Shipper.RegionLongName;
                            labelShipperPostalCode.Text = getBol.Shipper.PostalCode;
                            labelShipperCountry.Text = getBol.Shipper.CountryLongName;
                            panelShipper.Visible = true;
                        }

                        // 3rd Party Billing, doesn't have to be selected, could be null
                        if (getBol.BillToId != null)
                        {
                            comboBox3rdPartyBillling.SelectedValue = getBol.BillToId;
                            if (getBol.BillToAccount != null)
                            {
                                labelBillToName.Text = getBol.BillToAccount.CompanyName;
                                labelBillToAddress.Text = getBol.BillToAccount.Address1;
                                labelBillToCity.Text = getBol.BillToAccount.City;
                                labelBillToRegion.Text = getBol.BillToAccount.RegionLongName;
                                labelBillToPostalCode.Text = getBol.BillToAccount.PostalCode;
                                labelBillToCountry.Text = getBol.BillToAccount.CountryLongName;
                                panelBillTo.Visible = true;

                            }
                        }

                        // ShipFromLocation or Vendors
                        comboBoxVendors.SelectedValue = getBol.ShipFromId;
                        if (getBol.ShipFromLocation != null)
                        {
                            if (getBol.ShipFromLocation.LocationId != null)
                            {
                                // Set the Location ComboBox, and make the panel visible
                                if (comboBoxVendorLocations.Items.Count > 1)
                                {
                                    comboBoxVendorLocations.SelectedValue = getBol.ShipFromLocation.LocationId;
                                    panelShipFromLocations.Visible = true;
                                }
                                else
                                {
                                    // We must have at least one item to save
                                    // I tried to code around this, to elimiate duplicate code - jkirkerx on 07/07/2023
                                    var dtVendorShippingLocations = new DataTable("dtVendorShippingLocations");
                                    dtVendorShippingLocations.Columns.Add(new DataColumn("Key"));
                                    dtVendorShippingLocations.Columns.Add(new DataColumn("Value"));

                                    if (getBol.ShipFromLocation != null)
                                    {
                                        var rsMainLocation = dtVendorShippingLocations.NewRow();
                                        rsMainLocation[0] = getBol.ShipFromLocation.Name;
                                        rsMainLocation[1] = getBol.ShipFromLocation.LocationId;
                                        dtVendorShippingLocations.Rows.Add(rsMainLocation);

                                        dtVendorShippingLocations.AcceptChanges();

                                        comboBoxVendorLocations.DataSource = dtVendorShippingLocations;
                                        comboBoxVendorLocations.DisplayMember = "Key";
                                        comboBoxVendorLocations.ValueMember = "Value";
                                        comboBoxVendorLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                                        comboBoxVendorLocations.DropDownStyle = ComboBoxStyle.DropDown;
                                        comboBoxVendorLocations.AutoCompleteMode = AutoCompleteMode.Suggest;
                                        comboBoxVendorLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                                        comboBoxVendorLocations.SelectedIndex = 0;

                                        panelShipFromLocations.Visible = false;

                                        // Set the Ship From Address
                                        labelShipFromName.Text = getBol.ShipFromLocation.Name;
                                        labelShipFromAddress.Text = getBol.ShipFromLocation.Address1;
                                        labelShipFromCity.Text = getBol.ShipFromLocation.City;
                                        labelShipFromRegion.Text = getBol.ShipFromLocation.RegionName;
                                        labelShipFromPostalCode.Text = getBol.ShipFromLocation.PostalCode;
                                        labelShipFromCountry.Text = getBol.ShipFromLocation.CountryName;
                                        panelShipFrom.Visible = true;

                                        // Liftgate Required
                                        if (getBol.ShipFromLocation.LiftGateRequired != null)
                                        {
                                            if (getBol.ShipFromLocation.LiftGateRequired == true)
                                            {
                                                checkBoxShipFromLiftGateRequired.Checked = true;
                                            }
                                        }

                                        // Appointment Required
                                        if (getBol.ShipFromLocation.AppointmentRequired != null)
                                        {
                                            if (getBol.ShipFromLocation.AppointmentRequired == true)
                                            {
                                                checkBoxShipFromAppointmentRequired.Checked = true;
                                                panelShipFromAppointment.Visible = true;
                                            }

                                            if (getBol.ShipFromLocation.AppointmentDate != null)
                                            {
                                                dateTimePickerShipFromAppointmentDate.Value = (DateTime)getBol.ShipFromLocation.AppointmentDate;
                                            }

                                            maskedTextBoxShipFromAppointmentTime.Text = getBol.ShipFromLocation.AppointmentTime;
                                        }

                                    }


                                }

                            }

                        }

                        // ShipToLocation or Customers
                        comboBoxCustomers.SelectedValue = getBol.CustomerId;
                        if (getBol.ShipToLocation != null)
                        {
                            if (getBol.ShipToLocation.LocationId != null)
                            {
                                // Set the Location ComboBox, and make the panel visible
                                if (comboBoxCustomerLocations.Items.Count > 1)
                                {
                                    comboBoxCustomerLocations.SelectedValue = getBol.ShipToLocation.LocationId;
                                    panelShipToLocations.Visible = true;
                                }
                                else
                                {
                                    // We need to make the locations
                                    // I tried to code around this, to elimiate duplicate code - jkirkerx on 07/07/2023
                                    var dtCustomerShippingLocations = new DataTable("dtCustomerShippingLocations");
                                    dtCustomerShippingLocations.Columns.Add(new DataColumn("Key"));
                                    dtCustomerShippingLocations.Columns.Add(new DataColumn("Value"));

                                    if (getBol.ShipToLocation != null)
                                    {
                                        var rsMainLocation = dtCustomerShippingLocations.NewRow();
                                        rsMainLocation[0] = getBol.ShipToLocation.Name;
                                        rsMainLocation[1] = getBol.ShipToLocation.LocationId;
                                        dtCustomerShippingLocations.Rows.Add(rsMainLocation);

                                        dtCustomerShippingLocations.AcceptChanges();

                                        comboBoxCustomerLocations.DataSource = dtCustomerShippingLocations;
                                        comboBoxCustomerLocations.DisplayMember = "Key";
                                        comboBoxCustomerLocations.ValueMember = "Value";
                                        comboBoxCustomerLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                                        comboBoxCustomerLocations.DropDownStyle = ComboBoxStyle.DropDown;
                                        comboBoxCustomerLocations.AutoCompleteMode = AutoCompleteMode.Suggest;
                                        comboBoxCustomerLocations.AutoCompleteSource = AutoCompleteSource.ListItems;
                                        comboBoxCustomerLocations.SelectedIndex = 0;

                                        panelShipToLocations.Visible = true;

                                        // Set the Ship From Address
                                        labelShipToName.Text = getBol.ShipToLocation.Name;
                                        labelShipToAddress.Text = getBol.ShipToLocation.Address1;
                                        labelShipToCity.Text = getBol.ShipToLocation.City;
                                        labelShipToRegion.Text = getBol.ShipToLocation.RegionName;
                                        labelShipToPostalCode.Text = getBol.ShipToLocation.PostalCode;
                                        labelShipToCountry.Text = getBol.ShipToLocation.CountryName;
                                        panelShipTo.Visible = true;

                                        // Liftgate Required
                                        if (getBol.ShipToLocation.LiftGateRequired != null)
                                        {
                                            if (getBol.ShipToLocation.LiftGateRequired == true)
                                            {
                                                checkBoxShipToLiftGateRequired.Checked = true;
                                            }
                                        }

                                        // Appointment Required
                                        if (getBol.ShipToLocation.AppointmentRequired != null)
                                        {
                                            if (getBol.ShipToLocation.AppointmentRequired == true)
                                            {
                                                checkBoxShipToAppointmentRequired.Checked = true;
                                                panelShipToAppointment.Visible = true;
                                            }

                                            if (getBol.ShipToLocation.AppointmentDate != null)
                                            {
                                                dateTimePickerShipToAppointmentDate.Value = (DateTime)getBol.ShipToLocation.AppointmentDate;
                                            }

                                            maskedTextBoxShipToAppointmentTime.Text = getBol.ShipToLocation.AppointmentTime;
                                        }

                                    }


                                }

                            }

                        }

                        // Payment and Values
                        textBoxBolEstimatedValue.Text = getBol.BolEstimatedValue.ToString();
                        textBoxEstimatedBolWeight.Text = getBol.BolEstimatedWeight.ToString();
                        checkBoxFreightPrepaid.Checked = getBol.FreightPrePaid;
                        checkBoxCustomerInvoice.Checked = getBol.FreightCustomerInvoiced;
                        checkBoxPaymentCOD.Checked = getBol.COD;
                        maskedTextBoxCodAmount.Text = getBol.CodAmount.ToString();

                        // References
                        maskedTextBoxShipperQuoteNumber.Text = getBol.ShipperQuoteNumber;
                        maskedTextBoxQuotedPrice.Text = getBol.ShipperQuotePrice.ToString();
                        maskedTextBoxActualPrice.Text = getBol.ShipperActualPrice.ToString();
                        maskedTextBoxReferenceNumber.Text = getBol.ShipperReferenceNumber;
                        maskedTextBoxOrderNumber.Text = getBol.OrderNumber;

                        // Transit
                        dateTimePickerShipDate.Text = getBol.ShipDate.ToLocalTime().ToString();
                        dateTimePickerDeliveryDate.Text = getBol.DeliveryDate.ToLocalTime().ToString();
                        textBoxEstimatedTransitDays.Text = getBol.TransitDaysEstimated.ToString();
                        textBoxActualTransitDays.Text = getBol.TransitDaysActual.ToString();

                        // Comments and Instructions
                        textBoxComments.Text = getBol.Comments;
                        textBoxSpecialInstructions.Text = getBol.SpecialInstructions;

                        // Containers
                        if (getBol.Containers != null)
                        {
                            this.Containers = getBol.Containers;
                            LoadListViewContainers(getBol.Containers);
                        }

                        // Pallets
                        if (getBol.Pallets != null)
                        {
                            this.Pallets = getBol.Pallets;
                            LoadListViewPallets(getBol.Pallets);
                        }

                        // Packages
                        if (getBol.Packages != null)
                        {
                            this.Packages = getBol.Packages;
                            LoadListViewPackages(getBol.Packages);
                        }

                        // Calculate the total weight
                        var estimatedPackageWeight = this.Packages.Sum(p => p.Weight);
                        var estimatedPalletWeight = this.Pallets.Sum(p => p.Weight);
                        var estimatedWeight = estimatedPackageWeight + estimatedPalletWeight;
                        textBoxEstimatedBolWeight.Text = estimatedWeight.ToString();
                        labelBolTotalWeight.Text = estimatedWeight.ToString();

                    }

                }

            }

        }

        private async Task<bool> SaveBol()
        {
            DisableEventHandlers();
            bool vFlag = true;

            if (this.Packages.Count == 0 && this.Pallets.Count == 0 && this.Containers.Count == 0)
            {
                DialogResult messageBoxResult = MessageBox.Show("You should create a Package, Pallet or Container,\r\nWould you like to save this BOL without one for now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (messageBoxResult == DialogResult.No) { return false; }
            }

            // Make sure the Bol Object is intialized
            if (this.Bol == null) { this.Bol = new BILLOFLADINGS(); }

            // Just check the controls, and grab the AccountId's of comboBoxes
            // If vFlag, then grab the data to fill in objects like ShipFromLocation, ShipToLocation

            // Shipper
            if (comboBoxShippers.SelectedIndex > 0)
            {
                this.Bol.ShipperId = (comboBoxShippers.SelectedValue as dynamic).ToString();
                comboBoxShippers.BackColor = SystemColors.Window;
            }
            else
            {
                comboBoxShippers.BackColor = Color.LightSalmon;
                vFlag = false;
            }

            // Ship From / Vendor
            if (comboBoxVendors.SelectedIndex > 0)
            {
                this.Bol.ShipFromId = (comboBoxVendors.SelectedValue as dynamic).ToString();
                comboBoxVendors.BackColor = SystemColors.Window;

                if (comboBoxVendorLocations.SelectedIndex > -1)
                {
                    comboBoxVendorLocations.BackColor = SystemColors.Window;
                }
                else
                {
                    comboBoxVendorLocations.BackColor = Color.LightSalmon;
                    vFlag = false;
                }

            }
            else
            {
                comboBoxVendors.BackColor = Color.LightSalmon;
                vFlag = false;
            }

            // ShipToLocation / Customer
            if (comboBoxCustomers.SelectedIndex > 0)
            {
                this.Bol.ShipToId = (comboBoxCustomers.SelectedValue as dynamic).ToString();
                comboBoxCustomers.BackColor = SystemColors.Window;

                if (comboBoxCustomerLocations.SelectedIndex > -1)
                {
                    comboBoxCustomerLocations.BackColor = SystemColors.Window;
                }
                else
                {
                    comboBoxCustomerLocations.BackColor = Color.LightSalmon;
                    vFlag = false;
                }

            }
            else
            {
                comboBoxCustomers.BackColor = Color.LightSalmon;
                vFlag = false;
            }


            if (vFlag)
            {
                // ShipmentType
                if (radioButtonLTL.Checked)
                {
                    this.Bol.ShipmentType = "LTL";
                }
                else if (radioButtonFTL.Checked)
                {
                    this.Bol.ShipmentType = "FTL";
                }
                else if (radioButtonAir.Checked)
                {
                    this.Bol.ShipmentType = "AIR";
                }
                else if (radioButtonOcean.Checked)
                {
                    this.Bol.ShipmentType = "OCEAN";
                }
                else if (radioButtonRailroad.Checked)
                {
                    this.Bol.ShipmentType = "RAIL";
                }
                else if (radioButtonLastMile.Checked)
                {
                    this.Bol.ShipmentType = "LAST MILE";
                }
                else if (radioButtonCourier.Checked)
                {
                    this.Bol.ShipmentType = "COURIER";
                }
                else if (radioButtonArmouredCar.Checked)
                {
                    this.Bol.ShipmentType = "ARMOURED CAR";
                }

                // SHIPPER
                if (shipperRepository != null && this.Bol.ShipperId != null)
                {
                    this.Bol.Shipper = await shipperRepository.GetOneShipperAsync(this.Bol.ShipperId);

                    // Trim the hyphen off the Postal Code if 5 chars
                    if (this.Bol.Shipper.PostalCode != null) { if (this.Bol.Shipper.PostalCode.Trim().Length == 6) { this.Bol.Shipper.PostalCode = this.Bol.Shipper.PostalCode[..5]; } }
                    this.Bol.ShipperName = this.Bol.Shipper.CompanyName;
                }

                // 3rd Party Billing
                if (billToAccountsRepository != null && this.Bol.BillToId != null)
                {
                    this.Bol.BillToAccount = await billToAccountsRepository.GetOneBillToAccountAsync(this.Bol.BillToId);
                }

                // SHIPFROM
                // ShipFromLocation or Vendors
                if (vendorRepository != null && this.Bol.ShipFromId != null)
                {
                    var vendor = await vendorRepository.GetOneVendorAsync(this.Bol.ShipFromId);
                    if (vendor != null)
                    {
                        string vendorLocationId = (comboBoxVendorLocations.SelectedValue as dynamic).ToString();

                        // Trim the Hyphen off the Postal Code
                        if (vendor.PostalCode != null) { if (vendor.PostalCode.Trim().Length == 6) { vendor.PostalCode = vendor.PostalCode[..5]; } }

                        this.Bol.ShipFromVendor = vendor;

                        // Checkbox Overrides
                        bool liftgateRequired = checkBoxShipFromLiftGateRequired.Checked == true ? true : false;
                        bool appointmentRequired = checkBoxShipFromAppointmentRequired.Checked == true ? true : false;

                        if (vendorLocationId != null && vendor.ShippingLocations != null)
                        {
                            if (vendor.ShippingLocations.Count > 0)
                            {

                                this.Bol.ShipFromLocationId = vendorLocationId;

                                // The vendor has multiple shipping locations created
                                var shipFromLocation = vendor.ShippingLocations.Where(sl => sl.LocationId == vendorLocationId).FirstOrDefault();
                                if (shipFromLocation != null)
                                {
                                    shipFromLocation.LiftGateRequired = liftgateRequired;
                                    shipFromLocation.AppointmentRequired = appointmentRequired;
                                    shipFromLocation.AppointmentDate = dateTimePickerShipToAppointmentDate.Value;
                                    shipFromLocation.AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim();
                                    shipFromLocation.UpdatedOnUtc = DateTime.UtcNow;

                                    // Trim the hyphen off the Postal Code if 5 chars
                                    if (shipFromLocation.PostalCode != null) { if (shipFromLocation.PostalCode.Trim().Length == 6) { shipFromLocation.PostalCode = shipFromLocation.PostalCode[..5]; } }

                                    this.Bol.ShipFromLocation = shipFromLocation;
                                }

                            }
                            else
                            {
                                string locationObjectId = ObjectId.GenerateNewId().ToString();
                                this.Bol.ShipFromLocationId = locationObjectId;

                                // Trim the hyphen off the Postal Code if 5 chars
                                if (vendor.PostalCode != null) { if (vendor.PostalCode.Trim().Length == 6) { vendor.PostalCode = vendor.PostalCode[..5]; } }

                                // The vendor has a single location
                                // The customer has a single location
                                this.Bol.ShipFromLocation = new SHIPPINGLOCATIONS
                                {
                                    Name = vendor.CompanyName,
                                    Address1 = vendor.Address1,
                                    Address2 = vendor.Address2,
                                    City = vendor.City,
                                    RegionCode = vendor.RegionCode,
                                    RegionName = vendor.RegionLongName,
                                    CountryCode = vendor.CountryCode,
                                    CountryName = vendor.CountryLongName,
                                    PostalCode = vendor.PostalCode,
                                    Phone = vendor.Phone1,
                                    MobilePhone = "",
                                    EmailAddress = vendor.EmailAddress1,
                                    LiftGateRequired = liftgateRequired,
                                    PalletJackRequired = vendor.PalletJackRequired,
                                    AppointmentRequired = appointmentRequired,
                                    AppointmentDate = dateTimePickerShipToAppointmentDate.Value,
                                    AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim(),
                                    LocationId = locationObjectId,
                                    LocationCode = "Main Location",
                                    ContactName = "",
                                    ContactEmailAddress = vendor.EmailAddress2,
                                    ContactPhone = vendor.Phone2,
                                    Comment = vendor.Comment,
                                    CreatedOnUtc = DateTime.UtcNow,
                                    UpdatedOnUtc = DateTime.UtcNow
                                };
                            }

                        }
                        else
                        {
                            string locationObjectId = ObjectId.GenerateNewId().ToString();
                            this.Bol.ShipFromLocationId = locationObjectId;

                            // Trim the hyphen off the Postal Code if 5 chars
                            if (vendor.PostalCode != null) { if (vendor.PostalCode.Trim().Length == 6) { vendor.PostalCode = vendor.PostalCode[..5]; } }

                            // The vendor has a single location
                            // The customer has a single location
                            this.Bol.ShipFromLocation = new SHIPPINGLOCATIONS
                            {
                                Name = vendor.CompanyName,
                                Address1 = vendor.Address1,
                                Address2 = vendor.Address2,
                                City = vendor.City,
                                RegionCode = vendor.RegionCode,
                                RegionName = vendor.RegionLongName,
                                CountryCode = vendor.CountryCode,
                                CountryName = vendor.CountryLongName,
                                PostalCode = vendor.PostalCode,
                                Phone = vendor.Phone1,
                                MobilePhone = "",
                                EmailAddress = vendor.EmailAddress1,
                                LiftGateRequired = liftgateRequired,
                                PalletJackRequired = vendor.PalletJackRequired,
                                AppointmentRequired = appointmentRequired,
                                AppointmentDate = dateTimePickerShipToAppointmentDate.Value,
                                AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim(),
                                LocationId = locationObjectId,
                                LocationCode = "Main Location",
                                ContactName = "",
                                ContactEmailAddress = vendor.EmailAddress2,
                                ContactPhone = vendor.Phone2,
                                Comment = vendor.Comment,
                                CreatedOnUtc = DateTime.UtcNow,
                                UpdatedOnUtc = DateTime.UtcNow
                            };

                        }

                    }

                }

                // SHIPTO
                // ShipTo or Customers
                if (customerRepository != null && this.Bol.ShipToId != null)
                {
                    var customer = await customerRepository.GetOneCustomerAsync(this.Bol.ShipToId);
                    if (customer != null)
                    {
                        // Trim the Hyphen off the Postal Code
                        if (customer.PostalCode != null) { if (customer.PostalCode.Trim().Length == 6) { customer.PostalCode = customer.PostalCode[..5]; } }

                        // Set the Customer
                        this.Bol.CustomerId = customer.CustomerId;
                        this.Bol.CustomerName = customer.CompanyName;
                        this.Bol.Customer = customer;
                        this.Bol.ShipToCustomer = customer;

                        string customerLocationId = (comboBoxCustomerLocations.SelectedValue as dynamic).ToString();
                        if (customerLocationId != null && customer.ShippingLocations != null)
                        {
                            this.Bol.ShipToLocationId = customerLocationId;

                            // Checkbox Overrides
                            bool liftgateRequired = checkBoxShipToLiftGateRequired.Checked == true ? true : false;
                            bool appointmentRequired = checkBoxShipToAppointmentRequired.Checked == true ? true : false;

                            if (customer.ShippingLocations.Count > 0)
                            {
                                // The Customer has multiple shipping locations created
                                var shipToLocation = customer.ShippingLocations.Where(sl => sl.LocationId == customerLocationId).FirstOrDefault();

                                if (shipToLocation != null)
                                {
                                    shipToLocation.LiftGateRequired = liftgateRequired;
                                    shipToLocation.AppointmentRequired = appointmentRequired;
                                    shipToLocation.AppointmentDate = dateTimePickerShipToAppointmentDate.Value;
                                    shipToLocation.AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim();

                                    // Trim the hyphen off the Postal Code if 5 chars
                                    if (shipToLocation.PostalCode != null) { if (shipToLocation.PostalCode.Trim().Length == 6) { shipToLocation.PostalCode = shipToLocation.PostalCode[..5]; } }

                                    this.Bol.ShipToLocation = shipToLocation;
                                }

                            }
                            else
                            {
                                string locationObjectId = ObjectId.GenerateNewId().ToString();
                                this.Bol.ShipToLocationId = locationObjectId;

                                // Trim the hyphen off the Postal Code if 5 chars
                                if (customer.PostalCode != null) { if (customer.PostalCode.Trim().Length == 6) { customer.PostalCode = customer.PostalCode[..5]; } }

                                // The customer has a single location
                                this.Bol.ShipToLocation = new SHIPPINGLOCATIONS
                                {
                                    Name = customer.CompanyName,
                                    Address1 = customer.Address1,
                                    Address2 = customer.Address2,
                                    City = customer.City,
                                    RegionCode = customer.RegionCode,
                                    RegionName = customer.RegionLongName,
                                    CountryCode = customer.CountryCode,
                                    CountryName = customer.CountryLongName,
                                    PostalCode = customer.PostalCode,
                                    Phone = customer.Phone1,
                                    MobilePhone = "",
                                    EmailAddress = customer.EmailAddress1,
                                    LiftGateRequired = liftgateRequired,
                                    PalletJackRequired = customer.PalletJackRequired,
                                    AppointmentRequired = appointmentRequired,
                                    AppointmentDate = dateTimePickerShipToAppointmentDate.Value,
                                    AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim(),
                                    LocationId = locationObjectId,
                                    LocationCode = "Main Location",
                                    ContactName = "",
                                    ContactEmailAddress = customer.EmailAddress2,
                                    ContactPhone = customer.Phone2,
                                    Comment = customer.Comment,
                                    CreatedOnUtc = DateTime.UtcNow,
                                    UpdatedOnUtc = DateTime.UtcNow,
                                };

                            }
                        }
                        else
                        {

                            bool liftgateRequired = checkBoxShipToLiftGateRequired.Checked == true ? true : false;
                            bool appointmentRequired = checkBoxShipToAppointmentRequired.Checked == true ? true : false;

                            string locationObjectId = ObjectId.GenerateNewId().ToString();
                            this.Bol.ShipFromLocationId = locationObjectId;

                            // Trim the hyphen off the Postal Code if 5 chars
                            if (customer.PostalCode != null) { if (customer.PostalCode.Trim().Length == 6) { customer.PostalCode = customer.PostalCode[..5]; } }

                            // The customer has a single location
                            this.Bol.ShipToLocation = new SHIPPINGLOCATIONS
                            {
                                Name = customer.CompanyName,
                                Address1 = customer.Address1,
                                Address2 = customer.Address2,
                                City = customer.City,
                                RegionCode = customer.RegionCode,
                                RegionName = customer.RegionLongName,
                                CountryCode = customer.CountryCode,
                                CountryName = customer.CountryLongName,
                                PostalCode = customer.PostalCode,
                                Phone = customer.Phone1,
                                MobilePhone = "",
                                EmailAddress = customer.EmailAddress1,
                                LiftGateRequired = liftgateRequired,
                                PalletJackRequired = customer.PalletJackRequired,
                                AppointmentRequired = appointmentRequired,
                                AppointmentDate = dateTimePickerShipToAppointmentDate.Value,
                                AppointmentTime = maskedTextBoxShipToAppointmentTime.Text.Trim(),
                                LocationId = locationObjectId,
                                LocationCode = "Main Location",
                                ContactName = "",
                                ContactEmailAddress = customer.EmailAddress2,
                                ContactPhone = customer.Phone2,
                                Comment = customer.Comment,
                                CreatedOnUtc = DateTime.UtcNow,
                                UpdatedOnUtc = DateTime.UtcNow,
                            };

                        }
                    }

                }

                // Bill To 3rd Party
                if (comboBox3rdPartyBillling.SelectedIndex > 0)
                {
                    string billToAccountId = (comboBox3rdPartyBillling.SelectedValue as dynamic).ToString();
                    if (billToAccountId != null)
                    {
                        if (this.billToAccountsRepository != null)
                        {
                            var getBillTo = await billToAccountsRepository.GetOneBillToAccountAsync(billToAccountId);

                            // Trim the hyphen off the Postal Code if 5 chars
                            if (getBillTo.PostalCode != null) { if (getBillTo.PostalCode.Trim().Length == 6) { getBillTo.PostalCode = getBillTo.PostalCode[..5]; } }

                            this.Bol.BillToId = billToAccountId;
                            this.Bol.BillToAccount = getBillTo;
                        }
                    }
                }

                // PAYMENT AND VALUES
                // Estimated Bol Value - decimal
                bool eBolEstimatedValueResult = decimal.TryParse(textBoxBolEstimatedValue.Text.Trim(), out decimal eBolEstimatedValue);
                this.Bol.BolEstimatedValue = eBolEstimatedValueResult == true ? eBolEstimatedValue : 0.00m;

                // EstimatedBolWeight - int
                bool eBolEstimatedWeightResult = int.TryParse(textBoxEstimatedBolWeight.Text.Trim(), out int eBolEstimatedWeightValue);
                this.Bol.BolEstimatedWeight = eBolEstimatedWeightValue;

                // Freight Prepaid / Customer Invoice / use tenary for better accuracy
                this.Bol.FreightPrePaid = checkBoxFreightPrepaid.Checked == true;
                this.Bol.FreightCustomerInvoiced = checkBoxCustomerInvoice.Checked == true;

                // COD
                this.Bol.COD = checkBoxPaymentCOD.Checked == true;
                this.Bol.CodAmount = 0.00M;
                this.Bol.CodChargedTo = CODCHARGES.Shipper;

                if (this.Bol.COD == true)
                {
                    bool eBolCodAmountResult = decimal.TryParse(maskedTextBoxCodAmount.Text.Trim(), out decimal eBolCodAmount);
                    this.Bol.CodAmount = eBolCodAmountResult == true ? eBolCodAmount : 0.00m;
                }

                // References
                this.Bol.ShipperQuoteNumber = maskedTextBoxShipperQuoteNumber.Text.Trim();

                // Quoted Price - Should probably remove from model
                bool bolQuotedPriceResult = decimal.TryParse(maskedTextBoxQuotedPrice.Text.Trim(), out decimal bolQuotedPrice);
                this.Bol.ShipperQuotePrice = bolQuotedPriceResult == true ? bolQuotedPrice : 0.00m;

                // Estimated Shipping Price
                bool bolEstimatedShippingPriceResult = decimal.TryParse(maskedTextBoxQuotedPrice.Text.Trim(), out decimal bolEstimatedShippingPrice);
                this.Bol.EstimatedShippingPrice = bolEstimatedShippingPriceResult == true ? bolEstimatedShippingPrice : 0.00m;

                // Actual Price
                bool bolActualPriceResult = decimal.TryParse(maskedTextBoxActualPrice.Text.Trim(), out decimal bolActualPrice);
                this.Bol.ShipperActualPrice = bolActualPriceResult == true ? bolActualPrice : 0.00m;

                // Reference Number
                this.Bol.ShipperReferenceNumber = maskedTextBoxReferenceNumber.Text.Trim();

                // Bol Number
                this.BolNumber = textBoxBolNumber.Text.Trim();
                this.Bol.BolNumber = textBoxBolNumber.Text.Trim();

                // Order Number
                this.Bol.OrderNumber = maskedTextBoxOrderNumber.Text.Trim();

                // TRANSIT TIMES
                // Dates
                this.Bol.ShipDate = dateTimePickerShipDate.Value.ToUniversalTime();
                this.Bol.DeliveryDate = dateTimePickerDeliveryDate.Value.ToUniversalTime();

                // Estimated Transit Days
                bool bolEstimatedTransitDaysResult = int.TryParse(textBoxEstimatedTransitDays.Text.Trim(), out int bolEstimatedTransitDays);
                this.Bol.TransitDaysEstimated = bolEstimatedTransitDaysResult == true ? bolEstimatedTransitDays : 0;

                // Actual Transit Days
                bool bolActualTransitDaysResult = int.TryParse(textBoxActualTransitDays.Text.Trim(), out int bolActualTransitDays);
                this.Bol.TransitDaysActual = bolActualTransitDaysResult == true ? bolActualTransitDays : 0;

                // Comments and Special Instructions
                this.Bol.Comments = textBoxComments.Text.Trim();
                this.Bol.SpecialInstructions = textBoxSpecialInstructions.Text.Trim();

                // Weights
                this.Bol.TotalContainerWeight = 0;
                this.Bol.TotalPalletWeight = 0;
                this.Bol.TotalPackageWeight = 0;

                // Containers
                if (this.Containers != null)
                {
                    this.Bol.Containers = this.Containers;
                    this.Bol.TotalContainerWeight = this.Containers.Sum(w => w.Weight);
                }

                // Pallets
                if (this.Pallets != null)
                {
                    this.Bol.Pallets = this.Pallets;
                    this.Bol.TotalPalletWeight = this.Pallets.Sum(w => w.Weight);
                }

                // Packages
                if (this.Packages != null)
                {
                    this.Bol.Packages = this.Packages;
                    this.Bol.TotalPackageWeight = this.Packages.Sum(w => w.Weight);
                }

                // Looks like were good to go, save this BOL
                if (bolRepository != null)
                {
                    if (this.BolId == "ADD")
                    {
                        this.Bol.CreatedOnUtc = DateTime.UtcNow;
                        this.Bol.UpdatedOnUtc = DateTime.UtcNow;

                        // Disputed
                        this.Bol.Disputed = false;

                        // Printed
                        this.Bol.Printed = false;

                        vFlag = await bolRepository.AddBillOfLaddingAsync(this.Bol);
                    }
                    else
                    {
                        this.Bol.UpdatedOnUtc = DateTime.UtcNow;

                        vFlag = await bolRepository.UpdateBillOfLaddingAsync(this.Bol, this.BolId);
                    }
                }


            }

            EnableEventHandlers();

            return vFlag;

        }

        #endregion
        #region SystemLocale

        private void setUnitOfMeasurement()
        {
            labelUnitOfMeasurement.Text = Locale.GetRegionUnitOfMeasurement();
            this.bolUnitOfMeasurement = Locale.GetRegionUnitOfMeasurement();
        }

        #endregion
        #region Weight

        private void setCalculatedTotalWeight()
        {
            int pValue = 0;

            if (this.Containers != null)
            {
                int containerWeight = 0;
                pValue += containerWeight;
            }

            if (this.Pallets != null)
            {
                int palletWeight = this.Pallets.Sum(s => s.Weight);
                pValue += palletWeight;
            }

            if (this.Packages != null)
            {
                int packageWeight = this.Packages.Sum(s => s.Weight);
                pValue += packageWeight;
            }


            labelBolTotalWeight.Text = pValue.ToString();
            textBoxEstimatedBolWeight.Text = pValue.ToString();

        }


        #endregion
        #region BolValue

        private void setCalculatedBolValue()
        {
            decimal pValue = 0.00m;

            if (this.Containers != null)
            {
                decimal containerValue = 0.00m;
                pValue += containerValue;
            }

            if (this.Pallets != null)
            {
                decimal palletValue = this.Pallets.Sum(s => s.EstimatedValue);
                pValue += palletValue;
            }

            if (this.Packages != null)
            {
                decimal packageValue = this.Packages.Sum(s => s.EstimatedValue);
                pValue += packageValue;
            }


            textBoxBolEstimatedValue.Text = pValue.ToString();

        }


        #endregion
        #region TextBoxValidators

        private void TextBoxCurrencyAmount_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string amountText = textBox.Text;

            // Perform currency amount validation using a regular expression
            string pattern = @"^\d+(\.\d{1,2})?$"; // Matches positive decimal numbers with up to two decimal places
            bool isValid = Regex.IsMatch(amountText, pattern);

            // Set error provider or display an error message if the currency amount is invalid
            if (!isValid)
            {
                errorProvider1.SetError(textBox, "Invalid currency amount format. Please use a valid positive number with up to two decimal places.");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(textBox, ""); // Clear error if valid
            }
        }

        private void MaskedTextBoxCurrencyAmount_Validating(object sender, CancelEventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            string amountText = maskedTextBox.Text;

            // Perform currency amount validation using a regular expression
            string pattern = @"^\d+(\.\d{1,2})?$"; // Matches positive decimal numbers with up to two decimal places
            bool isValid = Regex.IsMatch(amountText, pattern);

            // Set error provider or display an error message if the currency amount is invalid
            if (!isValid)
            {
                errorProvider1.SetError(maskedTextBox, "Invalid currency amount format. Please use a valid positive number with up to two decimal places.");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(maskedTextBox, ""); // Clear error if valid
            }
        }


        #endregion
        #region TextBoxControlEvents

        protected void TextBoxControl_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox != null)
            {
                if (textBox.Text.Length > 0)
                {
                    _ = BeginInvoke((Action)delegate
                    {
                        textBox.SelectAll();
                    });
                }
            }
        }

        protected void MaskedTextBoxControl_Enter(object sender, EventArgs e)
        {
            MaskedTextBox maskedTextBox = (MaskedTextBox)sender;
            if (maskedTextBox != null)
            {
                if (maskedTextBox.Text.Length > 0)
                {
                    _ = BeginInvoke((Action)delegate
                    {
                        maskedTextBox.SelectAll();
                    });
                }
            }
        }

        #endregion
        #region Print

        private async void ButtonPrint_Click(global::System.Object sender, global::System.EventArgs e)
        {
            if (PrintValidator())
            {
                if (serviceProvider != null && bolRepository != null)
                {
                    // Get a fresh copy of the BOL, in case the user made changes and saved
                    var getBol = await bolRepository.GetOneBillOfLaddingAsync(this.BolId);
                    if (getBol != null)
                    {
                        using var printBolDialogDIOwned = serviceProvider.CreateOwnedForm<PrintBolDialog>();
                var printBolDialogDI = printBolDialogDIOwned.Form;
                        printBolDialogDI.StartPosition = FormStartPosition.CenterScreen;
                        printBolDialogDI.PrintBolId = this.BolId;
                        printBolDialogDI.PrintBol = getBol;

                        if (printBolDialogDI.ShowDialog() == DialogResult.OK)
                        {
                            var printBol = printBolDialogDI.PrintBol;
                            if (printBol != null)
                            {


                            }
                        }

                        Application.DoEvents();

                    }


                }

            }
        }

        private bool PrintValidator()
        {

            return true;
        }

        #endregion

    }
}
