using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;

using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class BillToAccountDialog : Form
    {

        public string BillToAccountId { get; set; } = null!;
        public BILLTOACCOUNTS BillToAccount { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly ICommonRepository? commonRepository;
        private readonly IBillToAccountsRepository? billToAccountsRepository;
        private readonly ICustomerRepository? customerRepository;

        public BillToAccountDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            IBillToAccountsRepository billToAccountsRespository,
            ICustomerRepository? customerRepository)
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.billToAccountsRepository = billToAccountsRespository;
            this.customerRepository = customerRepository;
        }

        #region Form

        protected async void BillToAccountDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DisableEventHandlers();

            var customers = LoadBindToCustomersAsync();

            var countries = LoadComboBoxCountriesAsync();
            await Task.WhenAll(countries);            

            if (BillToAccountId != null)
            {
                if (BillToAccountId == "ADD")
                {
                    SetComboBoxesToWindowsSystemRegion();
                }
                else
                {
                    LoadBillToAccount(BillToAccountId);
                }
            }

            EnableEventHandlers();

        }

        protected void BillToAccountDialog_Shown(object sender, EventArgs e)
        {

            textBoxCompanyName.Focus();

            Cursor = Cursors.Default;
        }

        #endregion
        #region Events

        private void DisableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged -= ComboBoxCountry_SelectedIndexChanged;
            comboBoxBindToCustomer.SelectedIndexChanged -= ComboBoxBindToCustomer_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
            comboBoxBindToCustomer.SelectedIndexChanged += ComboBoxBindToCustomer_SelectedIndexChanged;
        }

        #endregion
        #region CompanyInformation

        private async Task<int> LoadComboBoxCountriesAsync()
        {
            int countryCount = 0;

            Cursor = Cursors.WaitCursor;

            if (commonRepository != null)
            {

                var getCountries = await commonRepository.GetAllCountriesAsync();
                if (getCountries != null)
                {
                    countryCount = getCountries.Count;

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

            return countryCount;

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
        #region BindToCustomer

        private async Task<int> LoadBindToCustomersAsync()
        {
            int customerCount = 0;

            if (customerRepository != null)
            {
                var getCustomers = await customerRepository.GetAllCustomersAsync();
                if (getCustomers != null)
                {

                    customerCount = getCustomers.Count;
                    comboBoxBindToCustomer.Items.Clear();

                    var dtCustomers = new DataTable("dtCustomers");
                    dtCustomers.Columns.Add(new DataColumn("Key"));
                    dtCustomers.Columns.Add(new DataColumn("Value"));

                    var rsDefault = dtCustomers.NewRow();
                    rsDefault[0] = "-- Available to all --";
                    rsDefault[1] = "0";
                    dtCustomers.Rows.Add(rsDefault);

                    foreach (var customerItem in getCustomers.OrderBy(ob => ob.CompanyName))
                    {
                        var rsCustomerItem = dtCustomers.NewRow();
                        rsCustomerItem[0] = customerItem.CompanyName;
                        rsCustomerItem[1] = customerItem.CustomerId;
                        dtCustomers.Rows.Add(rsCustomerItem);
                    }

                    dtCustomers.AcceptChanges();

                    comboBoxBindToCustomer.DataSource = dtCustomers;
                    comboBoxBindToCustomer.DisplayMember = "Key";
                    comboBoxBindToCustomer.ValueMember = "Value";
                    comboBoxBindToCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxBindToCustomer.DropDownStyle = ComboBoxStyle.DropDown;
                    comboBoxBindToCustomer.AutoCompleteMode = AutoCompleteMode.Suggest;
                    comboBoxBindToCustomer.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxBindToCustomer.SelectedIndex = 0;

                }

            }

            return customerCount;

        }

        private void ComboBoxBindToCustomer_SelectedIndexChanged(object? sender, EventArgs e)
        {

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


        #endregion
        #region LoadSave

        private async void LoadBillToAccount(string billToAccountId)
        {
            if (billToAccountId != null)
            {
                if (billToAccountsRepository != null)
                {
                    var getBillToAccount = await billToAccountsRepository.GetOneBillToAccountAsync(billToAccountId);
                    if (getBillToAccount != null)
                    {
                        this.BillToAccount = getBillToAccount;

                        textBoxCompanyName.Text = getBillToAccount.CompanyName;
                        textBoxAddress1.Text = getBillToAccount.Address1;
                        textBoxAddress2.Text = getBillToAccount.Address2;
                        textBoxCity.Text = getBillToAccount.City;
                        maskedTextBoxPostalCode.Text = getBillToAccount.PostalCode;
                        textBoxContactName.Text = getBillToAccount.ContactName;
                        maskedTextBoxPhoneNumber1.Text = getBillToAccount.ContactPhone1;
                        maskedTextBoxEmailAddress1.Text = getBillToAccount.ContactEmailAddress1;
                        textBoxAccountNumber.Text = getBillToAccount.AccountNumber;
                        textBoxComments.Text = getBillToAccount.Comment;

                        // Populate the ComboBoxes
                        string countryCode = getBillToAccount.CountryCode != null ? getBillToAccount.CountryCode : "0";
                        comboBoxCountry.SelectedValue = getBillToAccount.CountryCode;
                        if (comboBoxRegion.Items.Count > 0)
                        {
                            if (getBillToAccount.RegionCode != null)
                            {
                                comboBoxRegion.SelectedValue = getBillToAccount.RegionCode;
                            }                            
                        }

                        // Populate the Bind To Customer Option
                        if (getBillToAccount.BindToCustomerId != null)
                        {
                            comboBoxBindToCustomer.SelectedValue = getBillToAccount.BindToCustomerId;
                        }                        

                        // Load the RegionName ComboBox and Select it
                        var regionTask = LoadComboBoxRegionsAsync(countryCode);
                        await regionTask;

                        comboBoxRegion.SelectedValue = getBillToAccount.RegionCode;

                    }

                }

            }

        }

        private async Task<bool> SaveBillToAccount()
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

            // Contact Phone Number
            if (maskedTextBoxPhoneNumber1.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxPhoneNumber1.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxPhoneNumber1.BackColor = System.Drawing.SystemColors.Window;
            }

            // Contact EmailAddress 1
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
                string countryName = comboBoxCountry.Text;
                string regionCode = (comboBoxRegion.SelectedValue as dynamic).ToString();
                string regionName = comboBoxRegion.Text;
                string bindToCustomerId = (comboBoxBindToCustomer.SelectedValue as dynamic).ToString();
                
                // Trim the hyphen off the Postal Code if 5 chars
                if (maskedTextBoxPostalCode.Text != null) { if (maskedTextBoxPostalCode.Text.Length == 6) { maskedTextBoxPostalCode.Text = maskedTextBoxPostalCode.Text[..5]; } }


                if (BillToAccountId == "ADD")
                {

                    var newBillingAccount = new BILLTOACCOUNTS
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
                        ContactName = textBoxContactName.Text,
                        ContactPhone1 = maskedTextBoxPhoneNumber1.Text,
                        ContactEmailAddress1 = maskedTextBoxEmailAddress1.Text,
                        AccountNumber = textBoxAccountNumber.Text,
                        BindToCustomerId = bindToCustomerId,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                        Comment = textBoxComments.Text
                    };

                    if (billToAccountsRepository != null)
                    {
                        // Write the BillToDialogForm using the repository
                        await billToAccountsRepository.AddBillToAccountAsync(newBillingAccount);

                        // Assign the BillToDialogForm to the return object
                        this.BillToAccount = newBillingAccount;
                    }

                }
                else
                {

                    // Update the current 3rd Party Billing Account
                    this.BillToAccount.CompanyName = textBoxCompanyName.Text;
                    this.BillToAccount.Address1 = textBoxAddress1.Text;
                    this.BillToAccount.Address2 = textBoxAddress2.Text;
                    this.BillToAccount.City = textBoxCity.Text;
                    this.BillToAccount.CountryCode = countryCode;
                    this.BillToAccount.CountryLongName = countryName;
                    this.BillToAccount.RegionCode = regionCode;
                    this.BillToAccount.RegionLongName = regionName;
                    this.BillToAccount.PostalCode = maskedTextBoxPostalCode.Text;
                    this.BillToAccount.ContactName = textBoxContactName.Text;
                    this.BillToAccount.ContactPhone1 = maskedTextBoxPhoneNumber1.Text;
                    this.BillToAccount.ContactEmailAddress1 = maskedTextBoxEmailAddress1.Text;
                    this.BillToAccount.AccountNumber = textBoxAccountNumber.Text;
                    this.BillToAccount.BindToCustomerId = bindToCustomerId;
                    this.BillToAccount.UpdatedOnUtc = DateTime.UtcNow;
                    this.BillToAccount.Comment = textBoxComments.Text;

                    if (billToAccountsRepository != null)
                    {
                        // Write the Vendor using the vendorRepository
                        await billToAccountsRepository.UpdateBillToAccountAsync(this.BillToAccount, BillToAccountId);

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
            var validate = await SaveBillToAccount();
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
