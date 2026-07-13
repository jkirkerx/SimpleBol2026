using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using SimpleBol.Repository;

using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.Classes.Common;
using MongoDB.Bson;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class AccountDialog : Form
    {
        public string AccountId { get; set; } = null!;
        public ACCOUNTS Account { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IAccountsRepository? accountsRepository;

        public AccountDialog(
            IServiceScopeFactory serviceProvider,
            IAccountsRepository accountsRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.accountsRepository = accountsRepository;


        }

        #region Dialog

        protected void AccountDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // Load the ComboBoxes
            LoadComboBoxSecurityLevels();
            LoadComboBoxTimeZones();

            if (this.AccountId != "ADD")
            {
                LoadAccountAsync(this.AccountId);

            }
            else
            {
                this.Account = new ACCOUNTS();
            }
        }

        protected void AccountDialog_Shown(object sender, EventArgs e)
        {
            

            this.textBoxLoginId.Focus();

            Cursor = Cursors.Default;
        }

        #endregion
        #region TextBoxes

        private void MaskedTextBoxPhone_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber.SelectAll();
            });
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
        #region Buttons


        private async void OK_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var validate = await SaveAccountAsync();

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
            catch (InvalidOperationException ex)
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show(
                    ex.Message,
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region ComboBoxes

        private void LoadComboBoxSecurityLevels()
        {
            // New method of clearing the ComboBox, which is not working                
            DataTable currentDataTable = (DataTable)comboBoxSecurityLevels.DataSource;
            if (currentDataTable != null)
            {
                currentDataTable.Clear();
                comboBoxSecurityLevels.DataSource = currentDataTable;
            }

            var dtSecurityLevels = new DataTable("dtSecurityLevels");
            dtSecurityLevels.Columns.Add(new DataColumn("Key"));
            dtSecurityLevels.Columns.Add(new DataColumn("Value"));

            var rsDefault = dtSecurityLevels.NewRow();
            rsDefault[0] = "-- Make a selection --";
            rsDefault[1] = "0";
            dtSecurityLevels.Rows.Add(rsDefault);

            var rsUser = dtSecurityLevels.NewRow();
            rsUser[0] = "User";
            rsUser[1] = "0";
            dtSecurityLevels.Rows.Add(rsUser);

            var rsAdmin = dtSecurityLevels.NewRow();
            rsAdmin[0] = "Admin";
            rsAdmin[1] = "1";
            dtSecurityLevels.Rows.Add(rsAdmin);

            dtSecurityLevels.AcceptChanges();

            comboBoxSecurityLevels.DataSource = dtSecurityLevels;
            comboBoxSecurityLevels.DisplayMember = "Key";
            comboBoxSecurityLevels.ValueMember = "Value";
            comboBoxSecurityLevels.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSecurityLevels.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSecurityLevels.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxSecurityLevels.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSecurityLevels.SelectedIndex = 0;

        }

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
                comboBoxTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBoxTimeZone.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBoxTimeZone.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBoxTimeZone.SelectedIndex = 0;

            }

        }


        #endregion
        #region LoadSave

        private async void LoadAccountAsync(string accountId)
        {
            if (this.accountsRepository != null)
            {
                this.Account = await accountsRepository.GetOneAccountAsync(accountId);
                if (this.Account != null)
                {
                    textBoxLoginId.Text = this.Account.LoginId;
                    textBoxAccountName.Text = this.Account.Name;
                    maskedTextBoxPhoneNumber.Text = this.Account.Phone;
                    textBoxEmailAddress.Text = this.Account.EmailAddress;
                    comboBoxTimeZone.SelectedValue = this.Account.TimeZone;
                    comboBoxSecurityLevels.SelectedValue = this.Account.SecurityLevel;
                    textBoxLocation.Text = this.Account.Location;
                    textBoxPassword.Text = "";
                    textBoxComments.Text = this.Account.Comments;
                    checkBoxMarkedAsDeleted.Checked = Account.MarkAsDeleted;

                }
            }
        }

        private async Task<bool> SaveAccountAsync()
        {
            var vFlag = true;

            // Login AccountId or User AccountId
            if (textBoxLoginId.Text.Length == 0)
            {
                vFlag = false;
                textBoxLoginId.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxLoginId.BackColor = System.Drawing.SystemColors.Window;
            }

            // User or Account Name
            if (textBoxAccountName.Text.Length == 0)
            {
                vFlag = false;
                textBoxAccountName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxAccountName.BackColor = System.Drawing.SystemColors.Window;
            }

            // User or Account Phone Number
            if (maskedTextBoxPhoneNumber.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxPhoneNumber.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxPhoneNumber.BackColor = System.Drawing.SystemColors.Window;
            }

            // User or Account Email Address
            if (textBoxEmailAddress.Text.Length == 0)
            {
                vFlag = false;
                textBoxEmailAddress.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxEmailAddress.BackColor = System.Drawing.SystemColors.Window;
            }

            // TimeZone
            if (comboBoxTimeZone.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxTimeZone.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxTimeZone.BackColor = System.Drawing.SystemColors.Window;
            }

            // Security Level
            if (comboBoxSecurityLevels.SelectedIndex == 0)
            {
                vFlag = false;
                comboBoxSecurityLevels.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxSecurityLevels.BackColor = System.Drawing.SystemColors.Window;
            }

            // User or Account Location or City
            if (textBoxLocation.Text.Length == 0)
            {
                vFlag = false;
                textBoxLocation.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxLocation.BackColor = System.Drawing.SystemColors.Window;
            }

            if (this.AccountId == "ADD")
            {
                // User Password
                if (textBoxPassword.Text.Length == 0)
                {
                    vFlag = false;
                    textBoxPassword.BackColor = Color.LightSalmon;
                }
                else
                {
                    textBoxPassword.BackColor = System.Drawing.SystemColors.Window;
                }

            }

            if (vFlag)
            {
                if (this.Account == null) { this.Account = new ACCOUNTS(); }

                // Gather the form Information
                if (this.AccountId == "ADD")
                {
                    this.Account.AccountId = ObjectId.GenerateNewId().ToString();
                    this.Account.CreatedOnUtc = DateTime.UtcNow;
                }
                this.Account.LoginId = textBoxLoginId.Text.Trim();
                this.Account.Name = textBoxAccountName.Text.Trim();
                this.Account.Phone = maskedTextBoxPhoneNumber.Text.Trim();
                this.Account.EmailAddress = textBoxEmailAddress.Text.Trim();
                this.Account.TimeZone = (comboBoxTimeZone.SelectedValue as dynamic).ToString();
                this.Account.SecurityLevel = (comboBoxSecurityLevels.SelectedValue as dynamic).ToString();
                this.Account.Location = textBoxLocation.Text.Trim();
                this.Account.Comments = textBoxComments.Text.Trim();

                // Convert the Password to a hashed array of bytes
                if (textBoxPassword.Text.Length > 4)
                {
                    var passwordSalt = EncryptDecryptAes.CreateSecret();
                    string passwordRaw = textBoxPassword.Text.Trim();
                    string passwordHashed = EncryptDecryptAes.HashPasword(passwordRaw, out passwordSalt);

                    this.Account.PasswordHashed = passwordHashed;
                    this.Account.PasswordSalt = passwordSalt;
                }

                this.Account.UpdatedOnUtc = DateTime.UtcNow;
                this.Account.MarkAsDeleted = checkBoxMarkedAsDeleted.Checked == true ? true : false;

                if (accountsRepository != null)
                {
                    if (this.AccountId == "ADD")
                    {
                        await accountsRepository.AddAccountAsync(this.Account);
                    }
                    else
                    {
                        await accountsRepository.UpdateAccountAsync(this.Account, this.AccountId);
                        if (textBoxPassword.Text.Length > 4 &&
                            this.Account.PasswordHashed is not null &&
                            this.Account.PasswordSalt is not null)
                        {
                            await accountsRepository.UpdateAccountPasswordOnlyAsync(
                                this.AccountId,
                                this.Account.PasswordHashed,
                                this.Account.PasswordSalt);
                        }
                    }

                }

            }

            return vFlag;

        }

        #endregion
        #region Passwords

        private async void ButtonSetPassword_Click(object sender, EventArgs e)
        {
            bool vFlag = true;

            // User Password
            if (textBoxPassword.Text.Length == 0)
            {
                vFlag = false;
                textBoxPassword.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxPassword.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {
                // Convert the Password to a hashed array of bytes
                if (textBoxPassword.Text.Length > 4)
                {
                    var passwordSalt = EncryptDecryptAes.CreateSecret();
                    string passwordRaw = textBoxPassword.Text.Trim();
                    string passwordHashed = EncryptDecryptAes.HashPasword(passwordRaw, out passwordSalt);

                    if (accountsRepository != null)
                    {
                        var taskResult = await accountsRepository.UpdateAccountPasswordOnlyAsync(this.AccountId, passwordHashed, passwordSalt);
                        if (taskResult)
                        {
                            MessageBox.Show("This password has been updated successfully, Don't forget to keep a record of this", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                }
                else
                {
                    MessageBox.Show("You must enter a secure password that is at least 5 chars in length", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }

        }

        #endregion


    }

}
