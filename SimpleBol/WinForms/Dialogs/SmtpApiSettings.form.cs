using SimpleBol.Classes.Common;
using SimpleBol.Models;
using SimpleBol.Models.MongoDb;
using SimpleBol.NewtonSoft;
using SimpleBol.Repository.MongoDb;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Windows.Forms;
using SimpleBol.Services.sendEngine;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Setup;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class SmtpApiSettingsDialog : Form
    {

        public string? SmtpId { get; set; }
        public SMTPAPISETTINGS SmtpApiCloudSettings { get; set; } = null!;
        public SmtpApiSettings SmtpApiRootSettings { get; set; } = null!;

        public readonly IServiceScopeFactory serviceProvider;
        public readonly ICommonRepository commonRepository;
        public readonly ISmtpApiSettingsRepository smtpRepository;
        private readonly IGmailSender gmailSender;
        private readonly IOutlook365Sender outlook365Sender;
        private CancellationTokenSource? gmailAuthorizationCancellation;
        private CancellationTokenSource? outlookAuthorizationCancellation;
        private bool changingEmailConnection;

        public SmtpApiSettingsDialog(
            IServiceScopeFactory serviceProvider,
            ICommonRepository commonRepository,
            ISmtpApiSettingsRepository smtpRepository,
            IGmailSender gmailSender,
            IOutlook365Sender outlook365Sender)
        {

            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.commonRepository = commonRepository;
            this.smtpRepository = smtpRepository;
            this.gmailSender = gmailSender;
            this.outlook365Sender = outlook365Sender;
            ConfigureEmailConnectionControls();

        }

        #region Dialog

        private async void SmtpCredentialsDialogLoad(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            DisableEventHandlers();

            if (this.SmtpApiCloudSettings == null) { this.SmtpApiCloudSettings = new SMTPAPISETTINGS(); }
            if (this.SmtpApiRootSettings == null) { this.SmtpApiRootSettings = new SmtpApiSettings(); }
            SelectNoEmailProvider();

            var countries = LoadComboBoxCountriesAsync();
            await Task.WhenAll(countries);

            BindEmailConnections();
            bool appSettingsTask = await LoadAppSettingsRootObject();
            labelGmailConnectionStatus.Text = await gmailSender.HasAuthorizationAsync(
                rootObjectGmailSettings())
                ? CreateGmailConnectionSummary()
                : "Not connected";
            labelOutlookConnectionStatus.Text = await outlook365Sender.HasAuthorizationAsync(
                rootObjectOutlookSettings())
                ? CreateOutlookConnectionSummary()
                : "Not connected";
            if (!appSettingsTask)
            {
                // Ask if the user if they need to load the settings from the cloud storage
                DialogResult dialogResult = MessageBox.Show("AppSettings configuration file not found," + Environment.NewLine + " Would you like to try and get it from your cloud storage?",
                    "SMTP API Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    _ = await LoadSmtpApiSettingsFromCloud();
                }
            }

            EnableEventHandlers();

            Cursor = Cursors.Default;

        }

        private async void SmtpCredentialsDialogShown(object sender, EventArgs e)
        {

        }

        #endregion
        #region EmailConnections

        private void ConfigureEmailConnectionControls()
        {
            comboBoxEmailConnection.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEmailConnection.SelectedIndexChanged += ComboBoxEmailConnection_SelectedIndexChanged;
        }

        private void BindEmailConnections()
        {
            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject?.Settings?.DbConnections == null)
                return;

            changingEmailConnection = true;
            comboBoxEmailConnection.DataSource = null;
            comboBoxEmailConnection.DisplayMember = nameof(DbConnection.ProfileName);
            comboBoxEmailConnection.ValueMember = nameof(DbConnection.ProfileId);
            comboBoxEmailConnection.DataSource = rootObject.Settings.DbConnections;
            comboBoxEmailConnection.SelectedValue = rootObject.ActiveEmailConnectionId;
            changingEmailConnection = false;
        }

        private async void ComboBoxEmailConnection_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (changingEmailConnection || comboBoxEmailConnection.SelectedValue is not string profileId)
                return;

            var rootObject = AppSettingsJson.GetSettings();
            var profile = rootObject?.EmailConnections?.FirstOrDefault(item => item.ProfileId == profileId);
            if (rootObject == null || profile == null)
                return;

            rootObject.ActiveEmailConnectionId = profileId;
            rootObject.SmtpApiSettings = profile;
            await AppSettingsJson.WriteSettingsAsync(rootObject);
            ClearEmailConnectionControls();
            await LoadAppSettingsRootObject();
            await RefreshAuthorizationStatusAsync();
        }

        private void ClearEmailConnectionControls()
        {
            textBoxSendGridApiKey.Clear();
            textBoxSendGridSentFrom.Clear();
            textBoxGmailClientId.Clear();
            textBoxGmailClientSecret.Clear();
            textBoxGmailSentFromAddress.Clear();
            textBoxOutlookClientId.Clear();
            textBoxOutlookTenantId.Text = "common";
            textBoxOutlookSentFromAddress.Clear();
            textBoxCompanyName.Clear();
            textBoxAddress1.Clear();
            textBoxAddress2.Clear();
            textBoxCity.Clear();
            maskedTextBoxPostalCode.Clear();
            SelectNoEmailProvider();
        }

        private Models.Gmail rootObjectGmailSettings() => new()
        {
            ClientId = textBoxGmailClientId.Text.Trim(),
            SentFromEmailAddress = textBoxGmailSentFromAddress.Text.Trim()
        };

        private Models.Outlook365 rootObjectOutlookSettings() => new()
        {
            ClientId = textBoxOutlookClientId.Text.Trim(),
            TenantId = NormalizeOutlookTenant(),
            SentFromEmailAddress = textBoxOutlookSentFromAddress.Text.Trim()
        };

        private async Task RefreshAuthorizationStatusAsync()
        {
            labelGmailConnectionStatus.Text = await gmailSender.HasAuthorizationAsync(rootObjectGmailSettings())
                ? CreateGmailConnectionSummary()
                : "Not connected";
            labelOutlookConnectionStatus.Text = await outlook365Sender.HasAuthorizationAsync(rootObjectOutlookSettings())
                ? CreateOutlookConnectionSummary()
                : "Not connected";
        }

        #endregion
        #region Buttons

        private async void ButtonConnectGmail_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxGmailClientId.Text) ||
                string.IsNullOrWhiteSpace(textBoxGmailClientSecret.Text) ||
                string.IsNullOrWhiteSpace(textBoxGmailSentFromAddress.Text))
            {
                MessageBox.Show("Enter the OAuth Client ID, Client Secret, and Gmail account address first.",
                    "Connect Gmail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            buttonConnectGmail.Enabled = false;
            labelGmailConnectionStatus.Text = "Waiting for Google authorization...";
            gmailAuthorizationCancellation?.Cancel();
            gmailAuthorizationCancellation?.Dispose();
            gmailAuthorizationCancellation = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            try
            {
                await gmailSender.AuthorizeAsync(new Models.Gmail
                {
                    ClientId = textBoxGmailClientId.Text.Trim(),
                    ClientSecret = textBoxGmailClientSecret.Text.Trim(),
                    SentFromEmailAddress = textBoxGmailSentFromAddress.Text.Trim()
                }, gmailAuthorizationCancellation.Token);
                labelGmailConnectionStatus.Text = CreateGmailConnectionSummary();
                checkBoxGmailDefault.Checked = true;
                comboBoxDefaultApi.SelectedIndex = 2;
                MessageBox.Show("Gmail authorization was completed successfully.", "Connect Gmail",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                labelGmailConnectionStatus.Text = "Not connected";
                MessageBox.Show("Gmail authorization was canceled or timed out. If Google denied access, " +
                    "confirm the selected account is listed as an OAuth test user.",
                    "Connect Gmail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                UseWaitCursor = false;
                Cursor = Cursors.Default;
                Cursor.Current = Cursors.Default;
                labelGmailConnectionStatus.Text = "Not connected";
                MessageBox.Show("Gmail authorization failed:" + Environment.NewLine + ex.Message,
                    "Connect Gmail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                Cursor = Cursors.Default;
                Cursor.Current = Cursors.Default;
                buttonConnectGmail.Enabled = true;
                gmailAuthorizationCancellation?.Dispose();
                gmailAuthorizationCancellation = null;
            }
        }

        private string CreateGmailConnectionSummary()
        {
            var emailAddress = textBoxGmailSentFromAddress.Text.Trim();
            return $"Connected: {emailAddress}{Environment.NewLine}" +
                "Permission: Send Gmail only (gmail.send)" + Environment.NewLine +
                "Refresh token: stored securely for this Windows user";
        }

        private async void ButtonConnectOutlook_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxOutlookClientId.Text) ||
                string.IsNullOrWhiteSpace(textBoxOutlookSentFromAddress.Text))
            {
                MessageBox.Show("Enter the Application Client ID and Microsoft account email first.",
                    "Connect Outlook", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            buttonConnectOutlook.Enabled = false;
            labelOutlookConnectionStatus.Text = "Waiting for Microsoft authorization...";
            outlookAuthorizationCancellation?.Cancel();
            outlookAuthorizationCancellation?.Dispose();
            outlookAuthorizationCancellation = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            try
            {
                await outlook365Sender.AuthorizeAsync(new Models.Outlook365
                {
                    ClientId = textBoxOutlookClientId.Text.Trim(),
                    TenantId = NormalizeOutlookTenant(),
                    SentFromEmailAddress = textBoxOutlookSentFromAddress.Text.Trim()
                }, outlookAuthorizationCancellation.Token);
                labelOutlookConnectionStatus.Text = CreateOutlookConnectionSummary();
                checkBoxOutlookDefault.Checked = true;
                comboBoxDefaultApi.SelectedIndex = 3;
                MessageBox.Show("Microsoft authorization was completed successfully.",
                    "Connect Outlook", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                labelOutlookConnectionStatus.Text = "Not connected";
                MessageBox.Show("Microsoft authorization was canceled or timed out.",
                    "Connect Outlook", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                labelOutlookConnectionStatus.Text = "Not connected";
                MessageBox.Show("Microsoft authorization failed:" + Environment.NewLine + ex.Message,
                    "Connect Outlook", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                Cursor = Cursors.Default;
                Cursor.Current = Cursors.Default;
                buttonConnectOutlook.Enabled = true;
                outlookAuthorizationCancellation?.Dispose();
                outlookAuthorizationCancellation = null;
            }
        }

        private string CreateOutlookConnectionSummary()
        {
            return $"Connected: {textBoxOutlookSentFromAddress.Text.Trim()}{Environment.NewLine}" +
                "Permission: Send mail only (Mail.Send)" + Environment.NewLine +
                "Token: stored securely for this Windows user";
        }

        private string NormalizeOutlookTenant() =>
            string.IsNullOrWhiteSpace(textBoxOutlookTenantId.Text)
                ? "common"
                : textBoxOutlookTenantId.Text.Trim();

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            OK_Button.Enabled = false;
            try
            {
                bool savedLocally = await SaveAppSettingsToRootObject();
                if (!savedLocally)
                    return;

                bool backedUp = await SaveSmtpApiSettingsToCloud();
                if (!backedUp)
                    return;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "Save SMTP API Settings");
                MessageBox.Show(
                    "Saving the email settings could not be completed:" +
                    Environment.NewLine + ex.Message,
                    "SMTP API Settings",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                Cursor.Current = Cursors.Default;
                OK_Button.Enabled = true;
            }
        }

        #endregion
        #region LoadSave

        private async Task<bool> LoadSmtpApiSettingsFromCloud()
        {
            bool pValue = true;

            if (smtpRepository != null)
            {
                var getSmtp = await smtpRepository.GetSmtpCredentialsAsync();
                if (getSmtp != null)
                {
                    // Make a copy of the API Settings
                    this.SmtpApiCloudSettings = getSmtp;

                    // Store the Id
                    this.SmtpId = getSmtp.SmtpId;

                    if (getSmtp.SendGrid != null)
                    {
                        // Check if we have both the hashed key and salt, and decrypt the key
                        if (getSmtp.SendGrid.Salt != null && getSmtp.SendGrid.ApiKey != null)
                        {
                            var apiKey = EncryptDecryptAes.DecryptText(getSmtp.SendGrid.ApiKey, getSmtp.SendGrid.Salt);
                            textBoxSendGridApiKey.Text = apiKey;
                        }

                        textBoxSendGridSentFrom.Text = getSmtp.SendGrid.SentFrom;
                    }

                    if (getSmtp.Gmail != null)
                    {
                        // Check if we have both the hashed key and salt, and decrypt the key
                        if (getSmtp.Gmail.Salt != null && getSmtp.Gmail.ClientSecret != null)
                        {
                            var apiKey = EncryptDecryptAes.DecryptText(getSmtp.Gmail.ClientSecret, getSmtp.Gmail.Salt);
                            textBoxGmailClientSecret.Text = apiKey;
                        }

                        textBoxGmailClientId.Text = getSmtp.Gmail.ClientId;
                        textBoxGmailSentFromAddress.Text = getSmtp.Gmail.SentFrom;
                    }

                    if (getSmtp.Outlook365 != null)
                    {
                        textBoxOutlookClientId.Text = getSmtp.Outlook365.ClientId;
                        textBoxOutlookTenantId.Text = string.IsNullOrWhiteSpace(getSmtp.Outlook365.TenantId)
                            ? "common"
                            : getSmtp.Outlook365.TenantId;
                        textBoxOutlookSentFromAddress.Text = getSmtp.Outlook365.SentFrom;
                    }

                    if (getSmtp.CompanyInfo != null)
                    {
                        textBoxCompanyName.Text = getSmtp.CompanyInfo.CompanyName;
                        textBoxAddress1.Text = getSmtp.CompanyInfo.Address1;
                        textBoxAddress2.Text = getSmtp.CompanyInfo.Address2;
                        textBoxCity.Text = getSmtp.CompanyInfo.City;
                        maskedTextBoxPostalCode.Text = getSmtp.CompanyInfo.PostalCode;

                        // Make sure we load the Regions first, before selecting a value
                        if (getSmtp.CompanyInfo != null)
                        {
                            if (getSmtp.CompanyInfo.CountryId != null)
                            {
                                if (comboBoxCountry.Items.Count > 0)
                                {
                                    comboBoxCountry.SelectedValue = getSmtp.CompanyInfo.CountryId;
                                    Task taskAwait = LoadComboBoxRegionsAsync(getSmtp.CompanyInfo.CountryId);
                                    await Task.WhenAll(taskAwait);
                                }
                            }

                            if (getSmtp.CompanyInfo.RegionId != null)
                            {
                                if (comboBoxRegion.Items.Count > 0)
                                {
                                    comboBoxRegion.SelectedValue = getSmtp.CompanyInfo.RegionId;
                                }
                            }
                        }

                    }

                    // Set the ComboBox to the Default Api
                    if (getSmtp.DefaultId != null)
                    {
                        switch (getSmtp.DefaultId)
                        {
                            case "SENDGRID":
                                comboBoxDefaultApi.SelectedIndex = 1;
                                break;

                            case "GMAIL":
                                comboBoxDefaultApi.SelectedIndex = 2;
                                break;

                            case "OUTLOOK365":
                                comboBoxDefaultApi.SelectedIndex = 3;
                                break;

                            case "DISABLED":
                                comboBoxDefaultApi.SelectedIndex = 0;
                                break;

                            default:
                                comboBoxDefaultApi.SelectedIndex = 0;
                                break;
                        }

                    }
                    else
                    {
                        SelectNoEmailProvider();
                    }

                    // Set the CheckBoxes to the Default Api
                    if (getSmtp.DefaultId != null)
                    {
                        if (getSmtp.DefaultId == "SENDGRID")
                        {
                            checkBoxSendGridDefault.Checked = true;
                            checkBoxGmailDefault.Checked = false;
                            checkBoxOutlookDefault.Checked = false;
                        }

                        if (getSmtp.DefaultId == "GMAIL")
                        {
                            checkBoxSendGridDefault.Checked = false;
                            checkBoxGmailDefault.Checked = true;
                            checkBoxOutlookDefault.Checked = false;
                        }

                        if (getSmtp.DefaultId == "OUTLOOK365")
                        {
                            checkBoxSendGridDefault.Checked = false;
                            checkBoxGmailDefault.Checked = false;
                            checkBoxOutlookDefault.Checked = true;
                        }

                        if (getSmtp.DefaultId == "DISABLED")
                        {
                            checkBoxSendGridDefault.Checked = false;
                            checkBoxGmailDefault.Checked = false;
                            checkBoxOutlookDefault.Checked = false;
                        }
                    }

                }

            }

            return pValue;

        }

        private bool ValidateSelectedTab()
        {

            bool vFlag = true;

            TabPage? selectedTab = tabControlSmtpApiSettings.SelectedTab;
            if (selectedTab == tabPageSendGrid)
            {
                    // Twilio SendGrid

                    // SendGrid Api Key
                    if (textBoxSendGridApiKey.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxSendGridApiKey.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxSendGridApiKey.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // SendGrid Sent From Email Address
                    if (textBoxSendGridSentFrom.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxSendGridSentFrom.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxSendGridSentFrom.BackColor = System.Drawing.SystemColors.Window;
                    }

            }
            else if (selectedTab == tabPageGmail)
            {
                    // Gmail

                    // Gmail Client Id
                    if (textBoxGmailClientId.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxGmailClientId.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxGmailClientId.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Gmail Client Secret
                    if (textBoxGmailClientSecret.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxGmailClientSecret.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxGmailClientSecret.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Gmail Sent From Email Address
                    if (textBoxGmailSentFromAddress.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxGmailSentFromAddress.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxGmailSentFromAddress.BackColor = System.Drawing.SystemColors.Window;
                    }

            }
            else if (selectedTab == tabPageOutlook)
            {
                    // Outlook 365

                    // Outlook 365 Client Id
                    if (textBoxOutlookClientId.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxOutlookClientId.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxOutlookClientId.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Outlook 365 Tenant Id
                    if (textBoxOutlookTenantId.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxOutlookTenantId.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxOutlookTenantId.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Outlook 365 Sent From Email Address
                    if (textBoxOutlookSentFromAddress.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxOutlookSentFromAddress.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxOutlookSentFromAddress.BackColor = System.Drawing.SystemColors.Window;
                    }

            }
            else if (selectedTab == tabPageCompanyInfo)
            {
                    // CompanyInfo

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

                    // Street Address 1
                    if (textBoxAddress1.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxAddress1.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxAddress1.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Street Address 2
                    if (textBoxAddress2.Text.Length == 0)
                    {
                        vFlag = false;
                        textBoxAddress2.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        textBoxAddress2.BackColor = System.Drawing.SystemColors.Window;
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

                    // CountryName
                    if (comboBoxCountry.SelectedIndex == 0)
                    {
                        vFlag = false;
                        comboBoxCountry.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        comboBoxCountry.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // RegionName
                    if (comboBoxRegion.SelectedIndex == 0)
                    {
                        vFlag = false;
                        comboBoxRegion.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        comboBoxRegion.BackColor = System.Drawing.SystemColors.Window;
                    }

                    // Postal Code
                    if (maskedTextBoxPostalCode.Text.Length == 0)
                    {
                        vFlag = false;
                        maskedTextBoxPostalCode.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        maskedTextBoxPostalCode.BackColor = System.Drawing.SystemColors.Window;
                    }

            }

            return vFlag;
        }

        private async Task<bool> SaveSmtpApiSettingsToCloud()
        {

            bool vFlag = ValidateSelectedTab();
            if (vFlag)
            {
                // Verify we have a valid object
                if (this.SmtpApiCloudSettings == null) { this.SmtpApiCloudSettings = new SMTPAPISETTINGS(); }

                // Verify we have a valid SendGrid Object
                if (this.SmtpApiCloudSettings.SendGrid == null) { this.SmtpApiCloudSettings.SendGrid = new Models.MongoDb.SendGrid(); }

                // Verify we have a valid Gmail Object
                if (this.SmtpApiCloudSettings.Gmail == null) { this.SmtpApiCloudSettings.Gmail = new Models.MongoDb.Gmail(); }

                // Verify we have a valid Outlook 365 Object
                if (this.SmtpApiCloudSettings.Outlook365 == null) { this.SmtpApiCloudSettings.Outlook365 = new Models.MongoDb.Outlook365(); }

                // Verify we have a valid CompanyInfo Object
                if (this.SmtpApiCloudSettings.CompanyInfo == null) { this.SmtpApiCloudSettings.CompanyInfo = new Models.MongoDb.CompanyInfo(); }

                // We are going to save all the data, instead of just the selected Tab Panel


                // Twilio SendGrid
                if (this.SmtpApiCloudSettings.SendGrid != null)
                {
                    this.SmtpApiCloudSettings.SendGrid.ApiKey = textBoxSendGridApiKey.Text.Trim();
                    this.SmtpApiCloudSettings.SendGrid.SentFrom = textBoxSendGridSentFrom.Text.Trim().ToLower();

                    // Encrypt this API Key for Storage in the Cloud
                    byte[] salt = EncryptDecryptAes.CreateSecret();
                    this.SmtpApiCloudSettings.SendGrid.Salt = salt;
                    this.SmtpApiCloudSettings.SendGrid.ApiKey = EncryptDecryptAes.EncryptText(this.SmtpApiCloudSettings.SendGrid.ApiKey, salt);

                }

                // Gmail
                if (this.SmtpApiCloudSettings.Gmail != null)
                {
                    this.SmtpApiCloudSettings.Gmail.ClientId = textBoxGmailClientId.Text.Trim();
                    this.SmtpApiCloudSettings.Gmail.ClientSecret = textBoxGmailClientSecret.Text.Trim();
                    this.SmtpApiCloudSettings.Gmail.SentFrom = textBoxGmailSentFromAddress.Text.Trim().ToLower();

                    // Encrypt this API Key for Storage in the Cloud
                    byte[] salt = EncryptDecryptAes.CreateSecret();
                    this.SmtpApiCloudSettings.Gmail.Salt = salt;
                    this.SmtpApiCloudSettings.Gmail.ClientSecret = EncryptDecryptAes.EncryptText(this.SmtpApiCloudSettings.Gmail.ClientSecret, salt);
                }

                // Outlook 365
                if (this.SmtpApiCloudSettings.Outlook365 != null)
                {
                    this.SmtpApiCloudSettings.Outlook365.ClientId = textBoxOutlookClientId.Text.Trim();
                    this.SmtpApiCloudSettings.Outlook365.ClientSecret = null;
                    this.SmtpApiCloudSettings.Outlook365.TenantId = NormalizeOutlookTenant();
                    this.SmtpApiCloudSettings.Outlook365.SentFrom = textBoxOutlookSentFromAddress.Text.Trim().ToLower();
                    this.SmtpApiCloudSettings.Outlook365.Salt = null;

                }

                // Company Info
                if (this.SmtpApiCloudSettings.CompanyInfo != null)
                {
                    this.SmtpApiCloudSettings.CompanyInfo.CompanyName = textBoxCompanyName.Text.Trim();
                    this.SmtpApiCloudSettings.CompanyInfo.Address1 = textBoxAddress1.Text.Trim();
                    this.SmtpApiCloudSettings.CompanyInfo.Address2 = textBoxAddress2.Text.Trim();
                    this.SmtpApiCloudSettings.CompanyInfo.City = textBoxCity.Text.Trim();
                    this.SmtpApiCloudSettings.CompanyInfo.PostalCode = maskedTextBoxPostalCode.Text.Trim();

                    if (comboBoxCountry.SelectedIndex > 0)
                    {
                        this.SmtpApiCloudSettings.CompanyInfo.CountryId = (comboBoxCountry.SelectedValue as dynamic).ToString();
                    }
                    else
                    {
                        this.SmtpApiCloudSettings.CompanyInfo.CountryId = null;
                    }

                    if (comboBoxRegion.SelectedIndex > 0)
                    {
                        this.SmtpApiCloudSettings.CompanyInfo.RegionId = (comboBoxRegion.SelectedValue as dynamic).ToString();
                    }
                    else
                    {
                        this.SmtpApiCloudSettings.CompanyInfo.RegionId = null;
                    }
                }

                // Figure out the default API to use
                if (checkBoxSendGridDefault.Checked == true)
                {
                    this.SmtpApiCloudSettings.DefaultId = "SENDGRID";
                }
                else if (checkBoxGmailDefault.Checked == true)
                {
                    this.SmtpApiCloudSettings.DefaultId = "GMAIL";
                }
                else if (checkBoxOutlookDefault.Checked == true)
                {
                    this.SmtpApiCloudSettings.DefaultId = "OUTLOOK365";
                }
                else
                {
                    this.SmtpApiCloudSettings.DefaultId = "DISABLED";
                }


                var rootObject = AppSettingsJson.GetSettings();
                var selectedConnectionId = comboBoxEmailConnection.SelectedValue as string;
                var selectedConnection = rootObject?.Settings?.DbConnections?.FirstOrDefault(connection =>
                    connection.ProfileId == selectedConnectionId);
                if (selectedConnection == null || string.IsNullOrWhiteSpace(selectedConnection.Database))
                    throw new InvalidOperationException("The selected MongoDB connection is not configured.");

                var client = MongoDbConnectionFactory.CreateClient(selectedConnection);
                var context = new MongoDbContext(client, selectedConnection.Database);
                var selectedRepository = new SmtpApiSettingsRepository(context);
                await selectedRepository.AddSmtpCredentialsAsync(this.SmtpApiCloudSettings);
            }

            return vFlag;

        }

        #endregion
        #region LoadSaveAppSettings

        private async Task<bool> LoadAppSettingsRootObject()
        {
            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject != null)
            {
                if (rootObject.SmtpApiSettings != null)
                {
                    // The rootObject and Cloud Object are compatible with each other
                    this.SmtpApiRootSettings = rootObject.SmtpApiSettings;

                    // Twilio SendGrid
                    if (rootObject.SmtpApiSettings.SendGrid != null)
                    {
                        // Decode the secret value
                        if (rootObject.SmtpApiSettings.SendGrid.Salt != null)
                        {
                            byte[] salt = rootObject.SmtpApiSettings.SendGrid.Salt;
                            if (rootObject.SmtpApiSettings.SendGrid.ApiKey != null)
                            {
                                textBoxSendGridApiKey.Text = EncryptDecryptAes.DecryptText(rootObject.SmtpApiSettings.SendGrid.ApiKey, salt);
                            }
                        }

                        textBoxSendGridSentFrom.Text = rootObject.SmtpApiSettings.SendGrid?.SentFromEmailAddress;
                    }

                    // Google Gmail
                    if (rootObject.SmtpApiSettings.Gmail != null)
                    {
                        // Decode the secret value
                        if (rootObject.SmtpApiSettings.Gmail.Salt != null)
                        {
                            if (rootObject.SmtpApiSettings.Gmail.ClientSecret != null)
                            {
                                byte[] salt = rootObject.SmtpApiSettings.Gmail.Salt;
                                textBoxGmailClientSecret.Text = EncryptDecryptAes.DecryptText(rootObject.SmtpApiSettings.Gmail.ClientSecret, salt);
                            }
                        }

                        textBoxGmailClientId.Text = rootObject.SmtpApiSettings.Gmail?.ClientId;
                        textBoxGmailSentFromAddress.Text = rootObject.SmtpApiSettings.Gmail?.SentFromEmailAddress;
                    }

                    // Outlook 365
                    if (rootObject.SmtpApiSettings.Outlook365 != null)
                    {
                        textBoxOutlookClientId.Text = rootObject.SmtpApiSettings.Outlook365?.ClientId;
                        textBoxOutlookTenantId.Text = string.IsNullOrWhiteSpace(rootObject.SmtpApiSettings.Outlook365?.TenantId)
                            ? "common"
                            : rootObject.SmtpApiSettings.Outlook365.TenantId;
                        textBoxOutlookSentFromAddress.Text = rootObject.SmtpApiSettings.Outlook365?.SentFromEmailAddress;
                    }

                    // Company Info
                    if (rootObject.SmtpApiSettings.CompanyInfo != null)
                    {
                        textBoxCompanyName.Text = rootObject.SmtpApiSettings.CompanyInfo?.CompanyName;
                        textBoxAddress1.Text = rootObject.SmtpApiSettings.CompanyInfo?.Address1;
                        textBoxAddress2.Text = rootObject.SmtpApiSettings.CompanyInfo?.Address2;
                        textBoxCity.Text = rootObject.SmtpApiSettings.CompanyInfo?.City;
                        maskedTextBoxPostalCode.Text = rootObject.SmtpApiSettings.CompanyInfo?.PostalCode;

                        if (comboBoxCountry.Items.Count > 1)
                        {
                            if (rootObject.SmtpApiSettings.CompanyInfo?.CountryId != null)
                            {
                                string countryId = rootObject.SmtpApiSettings.CompanyInfo.CountryId;
                                if (countryId != null)
                                {
                                    Task taskAwait = LoadComboBoxRegionsAsync(countryId);
                                    await taskAwait;
                                    comboBoxCountry.SelectedValue = countryId;

                                    if (rootObject.SmtpApiSettings.CompanyInfo?.RegionId != null)
                                    {
                                        string regionId = rootObject.SmtpApiSettings.CompanyInfo.RegionId;
                                        if (regionId != null)
                                        {
                                            comboBoxRegion.SelectedValue = regionId;
                                        }
                                    }


                                }
                            }
                        }

                    }

                    // Set the CheckBoxes, and the ComboBox
                    if (rootObject.SmtpApiSettings.DefaultId != null)
                    {
                        switch (rootObject.SmtpApiSettings.DefaultId)
                        {
                            case "SENDGRID":
                                checkBoxSendGridDefault.Checked = true;
                                checkBoxGmailDefault.Checked = false;
                                checkBoxOutlookDefault.Checked = false;
                                comboBoxDefaultApi.SelectedIndex = 1;
                                break;

                            case "GMAIL":
                                checkBoxSendGridDefault.Checked = false;
                                checkBoxGmailDefault.Checked = true;
                                checkBoxOutlookDefault.Checked = false;
                                comboBoxDefaultApi.SelectedIndex = 2;
                                break;

                            case "OUTLOOK365":
                                checkBoxSendGridDefault.Checked = false;
                                checkBoxGmailDefault.Checked = false;
                                checkBoxOutlookDefault.Checked = true;
                                comboBoxDefaultApi.SelectedIndex = 3;
                                break;

                            case "DISABLED":
                                checkBoxSendGridDefault.Checked = false;
                                checkBoxGmailDefault.Checked = false;
                                checkBoxOutlookDefault.Checked = false;
                                comboBoxDefaultApi.SelectedIndex = 0;
                                break;

                            default:
                                SelectNoEmailProvider();
                                break;

                        }


                    }
                    else
                    {
                        SelectNoEmailProvider();
                    }

                }

                return true;

            }

            return false;

        }

        private async Task<bool> SaveAppSettingsToRootObject()
        {
            bool vFlag = ValidateSelectedTab();
            if (vFlag)
            {
                var rootObject = AppSettingsJson.GetSettings();
                if (rootObject != null)
                {
                    // Initialize the SmtpApiSettings if null
                    if (rootObject.SmtpApiSettings == null)
                    {
                        rootObject.SmtpApiSettings = new SmtpApiSettings();

                        if (rootObject.SmtpApiSettings.SendGrid == null)
                        {
                            rootObject.SmtpApiSettings.SendGrid = new Models.SendGrid();
                        }

                        if (rootObject.SmtpApiSettings.Gmail == null)
                        {
                            rootObject.SmtpApiSettings.Gmail = new Models.Gmail();
                        }

                        if (rootObject.SmtpApiSettings.Outlook365 == null)
                        {
                            rootObject.SmtpApiSettings.Outlook365 = new Models.Outlook365();
                        }

                        if (rootObject.SmtpApiSettings.CompanyInfo == null)
                        {
                            rootObject.SmtpApiSettings.CompanyInfo = new Models.CompanyInfo();
                        }

                    }

                    if (rootObject.SmtpApiSettings != null)
                    {
                        // Twilio SendGrid
                        if (rootObject.SmtpApiSettings.SendGrid != null)
                        {
                            byte[] salt = EncryptDecryptAes.CreateSecret();
                            rootObject.SmtpApiSettings.SendGrid.Salt = salt;
                            rootObject.SmtpApiSettings.SendGrid.ApiKey = EncryptDecryptAes.EncryptText(textBoxSendGridApiKey.Text.Trim(), salt);
                            rootObject.SmtpApiSettings.SendGrid.SentFromEmailAddress = textBoxSendGridSentFrom.Text.Trim().ToLower();
                        }

                        // Google Gmail
                        if (rootObject.SmtpApiSettings.Gmail != null)
                        {
                            byte[] salt = EncryptDecryptAes.CreateSecret();
                            rootObject.SmtpApiSettings.Gmail.Salt = salt;
                            rootObject.SmtpApiSettings.Gmail.ClientId = textBoxGmailClientId.Text.Trim();
                            rootObject.SmtpApiSettings.Gmail.ClientSecret = EncryptDecryptAes.EncryptText(textBoxGmailClientSecret.Text.Trim(), salt);
                            rootObject.SmtpApiSettings.Gmail.SentFromEmailAddress = textBoxGmailSentFromAddress.Text.Trim().ToLower();
                        }

                        // Microsoft Outlook 365
                        if (rootObject.SmtpApiSettings.Outlook365 != null)
                        {
                            rootObject.SmtpApiSettings.Outlook365.Salt = null;
                            rootObject.SmtpApiSettings.Outlook365.ClientId = textBoxOutlookClientId.Text.Trim();
                            rootObject.SmtpApiSettings.Outlook365.ClientSecret = null;
                            rootObject.SmtpApiSettings.Outlook365.TenantId = NormalizeOutlookTenant();
                            rootObject.SmtpApiSettings.Outlook365.SentFromEmailAddress = textBoxOutlookSentFromAddress.Text.Trim().ToLower();
                        }

                        // Company Info
                        if (rootObject.SmtpApiSettings.CompanyInfo != null)
                        {
                            rootObject.SmtpApiSettings.CompanyInfo.CompanyName = textBoxCompanyName.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.Address1 = textBoxAddress1.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.Address2 = textBoxAddress2.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.City = textBoxCity.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.PostalCode = maskedTextBoxPostalCode.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.CountryId = (comboBoxCountry.SelectedValue as dynamic).ToString();
                            rootObject.SmtpApiSettings.CompanyInfo.RegionId = (comboBoxRegion.SelectedValue as dynamic).ToString();

                            // See if we can get the test of the Country and Region
                            rootObject.SmtpApiSettings.CompanyInfo.CountryName = comboBoxCountry.Text.Trim();
                            rootObject.SmtpApiSettings.CompanyInfo.RegionName = comboBoxRegion.Text.Trim();
                        }

                        // Set the Default API Service
                        int defaultId = comboBoxDefaultApi.SelectedIndex;
                        switch (defaultId)
                        {
                            case 0:
                                rootObject.SmtpApiSettings.DefaultId = "DISABLED";
                                break;

                            case 1:
                                rootObject.SmtpApiSettings.DefaultId = "SENDGRID";
                                break;

                            case 2:
                                rootObject.SmtpApiSettings.DefaultId = "GMAIL";
                                break;

                            case 3:
                                rootObject.SmtpApiSettings.DefaultId = "OUTLOOK365";
                                break;
                        }

                        await AppSettingsJson.WriteSettingsAsync(rootObject);
                        return true;

                    }

                    return false;

                }

            }

            return vFlag;

        }

        #endregion


        #region ComboBoxes

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

        private void ComboBoxDefaultApi_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBoxDefaultApi.SelectedItem != null)
            {
                Cursor = Cursors.WaitCursor;
                DisableEventHandlers();

                var selectedItem = comboBoxDefaultApi.SelectedItem.ToString();
                if (selectedItem != null)
                {
                    if (selectedItem.Contains("None", StringComparison.OrdinalIgnoreCase) ||
                        selectedItem.Contains("Disabled", StringComparison.OrdinalIgnoreCase))
                    {
                        // Handle the case when the selected item contains "Disabled"
                        checkBoxSendGridDefault.Checked = false;
                        checkBoxGmailDefault.Checked = false;
                        checkBoxOutlookDefault.Checked = false;
                        this.SmtpApiCloudSettings.DefaultId = "DISABLED";
                    }
                    else if (selectedItem.ToString().Contains("SendGrid"))
                    {
                        // Handle the case when the selected item contains "SendGrid"
                        checkBoxSendGridDefault.Checked = true;
                        checkBoxGmailDefault.Checked = false;
                        checkBoxOutlookDefault.Checked = false;
                        this.SmtpApiCloudSettings.DefaultId = "SENDGRID";
                    }
                    else if (selectedItem.ToString().Contains("Gmail"))
                    {
                        // Handle the case when the selected item contains "Gmail"
                        checkBoxSendGridDefault.Checked = false;
                        checkBoxGmailDefault.Checked = true;
                        checkBoxOutlookDefault.Checked = false;
                        this.SmtpApiCloudSettings.DefaultId = "GMAIL";
                    }
                    else if (selectedItem.ToString().Contains("Outlook"))
                    {
                        // Handle the case when the selected item contains "Outlook 365"
                        checkBoxSendGridDefault.Checked = false;
                        checkBoxGmailDefault.Checked = false;
                        checkBoxOutlookDefault.Checked = true;
                        this.SmtpApiCloudSettings.DefaultId = "OUTLOOK365";
                    }
                }

                EnableEventHandlers();
                Cursor = Cursors.Default;
            }
        }

        private void SelectNoEmailProvider()
        {
            comboBoxDefaultApi.SelectedIndex = 0;
            checkBoxSendGridDefault.Checked = false;
            checkBoxGmailDefault.Checked = false;
            checkBoxOutlookDefault.Checked = false;
            SmtpApiRootSettings.DefaultId = "DISABLED";
            SmtpApiCloudSettings.DefaultId = "DISABLED";
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
        #region EventHandlers

        private void DisableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged -= ComboBoxCountry_SelectedIndexChanged;
            checkBoxSendGridDefault.CheckedChanged -= CheckBoxSendGridDefault_CheckedChanged;
            checkBoxGmailDefault.CheckedChanged -= CheckBoxGmailDefault_CheckedChanged;
            checkBoxOutlookDefault.CheckedChanged -= CheckBoxOutlookDefault_CheckedChanged;
            comboBoxDefaultApi.SelectedIndexChanged -= ComboBoxDefaultApi_SelectedIndexChanged;
        }

        private void EnableEventHandlers()
        {
            comboBoxCountry.SelectedIndexChanged += ComboBoxCountry_SelectedIndexChanged;
            checkBoxSendGridDefault.CheckedChanged += CheckBoxSendGridDefault_CheckedChanged;
            checkBoxGmailDefault.CheckedChanged += CheckBoxGmailDefault_CheckedChanged;
            checkBoxOutlookDefault.CheckedChanged += CheckBoxOutlookDefault_CheckedChanged;
            comboBoxDefaultApi.SelectedIndexChanged += ComboBoxDefaultApi_SelectedIndexChanged;
        }

        #endregion
        #region CheckBoxes

        private void CheckBoxSendGridDefault_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBoxSendGridDefault.Checked == true)
            {

                Cursor = Cursors.WaitCursor;
                DisableEventHandlers();

                checkBoxGmailDefault.Checked = false;
                checkBoxOutlookDefault.Checked = false;
                comboBoxDefaultApi.SelectedIndex = 1;

                if (this.SmtpApiCloudSettings != null)
                {
                    this.SmtpApiCloudSettings.DefaultId = "SENDGRID";
                }

                EnableEventHandlers();
                Cursor = Cursors.Default;
            }
        }

        private void CheckBoxGmailDefault_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBoxGmailDefault.Checked == true)
            {

                Cursor = Cursors.WaitCursor;
                DisableEventHandlers();

                checkBoxSendGridDefault.Checked = false;
                checkBoxOutlookDefault.Checked = false;
                comboBoxDefaultApi.SelectedIndex = 2;

                if (this.SmtpApiCloudSettings != null)
                {
                    this.SmtpApiCloudSettings.DefaultId = "GMAIL";
                }

                EnableEventHandlers();
                Cursor = Cursors.Default;
            }
        }

        private void CheckBoxOutlookDefault_CheckedChanged(object? sender, EventArgs e)
        {
            if (checkBoxOutlookDefault.Checked == true)
            {

                Cursor = Cursors.WaitCursor;
                DisableEventHandlers();

                checkBoxSendGridDefault.Checked = false;
                checkBoxGmailDefault.Checked = false;
                comboBoxDefaultApi.SelectedIndex = 3;

                if (this.SmtpApiCloudSettings != null)
                {
                    this.SmtpApiCloudSettings.DefaultId = "OUTLOOK365";
                }

                EnableEventHandlers();
                Cursor = Cursors.Default;
            }
        }

        #endregion

    }
}
