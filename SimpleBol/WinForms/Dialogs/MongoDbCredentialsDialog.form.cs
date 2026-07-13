
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MongoDB.Driver;
using SimpleBol.Classes.Common;
using SimpleBol.Models;
using SimpleBol.NewtonSoft;
using SimpleBol.Repository;
using SimpleBol.Repository.MongoDb;
using SimpleBol.Setup;

namespace SimpleBol.WinForms
{
    public partial class MongoDbCredentialsDialog : Form
    {
        public bool FirstTime { get; set; }
        public bool ActiveProfileChanged { get; private set; }
        public readonly IServiceScopeFactory serviceProvider;
        public readonly IMongoDbRepository mongoDbRepository;
        private RootObject? settingsRoot;
        private DbConnection? selectedConnection;
        private bool loadingProfile;
        private string? testedProfileId;
        private string? originalActiveProfileId;
        private string? originalActiveConnectionSignature;
        public MongoDbCredentialsDialog(
            IServiceScopeFactory serviceProvider,
            IMongoDbRepository databaseRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.mongoDbRepository = databaseRepository;

            ComboBoxAuthMechanism.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxConnections.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxConnections.DisplayMember = nameof(DbConnection.ProfileName);
            comboBoxConnections.ValueMember = nameof(DbConnection.ProfileId);
            comboBoxConnections.SelectedIndexChanged += ComboBoxConnections_SelectedIndexChanged;
            buttonAdd.Click += ButtonAdd_Click;
            buttonDelete.Click += ButtonDelete_Click;
            foreach (var textBox in new[]
            {
                textBoxProfileName, Txt_ServerAddress, Txt_PortNumber, Txt_UserName,
                Txt_Password, Txt_AuthDatabase, Txt_DatabaseName, Txt_ConnString
            })
            {
                textBox.TextChanged += ProfilePropertyChanged;
            }
            ComboBoxAuthMechanism.SelectedIndexChanged += ProfilePropertyChanged;
            OK_Button.Enabled = false;
        }

        private void ProfilePropertyChanged(object? sender, EventArgs e)
        {
            if (loadingProfile)
                return;

            testedProfileId = null;
            OK_Button.Enabled = false;
            Btn_Test.Text = "Build and Test";
            Btn_Test.BackColor = Color.FromArgb(60, 60, 60);
            Txt_ConnString.ForeColor = SystemColors.WindowText;
        }

        private void DbCredentialsDialog_Load(object sender, EventArgs e)
        {
            settingsRoot = AppSettingsJson.GetSettings();
            originalActiveProfileId = settingsRoot?.Settings?.ActiveDbConnectionId;
            originalActiveConnectionSignature = CreateConnectionSignature(
                settingsRoot?.Settings?.DbConnection);
            var connections = settingsRoot?.Settings?.DbConnections;
            if (connections == null || connections.Count == 0)
            {
                MessageBox.Show("No MongoDB connection profiles are configured.", "MongoDB Connections",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            RefreshConnectionList(settingsRoot!.Settings!.ActiveDbConnectionId);

            if (FirstTime)
            {
                Txt_ServerAddress.ReadOnly = true;
                Txt_PortNumber.ReadOnly = true;
                Txt_ServerAddress.BackColor = Color.FromArgb(255, 200, 200, 200);
                Txt_PortNumber.BackColor = Color.FromArgb(255, 200, 200, 200);
            }
        }

        private void RefreshConnectionList(string? selectedProfileId)
        {
            var connections = settingsRoot?.Settings?.DbConnections ?? new List<DbConnection>();
            loadingProfile = true;
            comboBoxConnections.DataSource = null;
            comboBoxConnections.DataSource = connections;
            var selectedIndex = connections.FindIndex(connection => connection.ProfileId == selectedProfileId);
            comboBoxConnections.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
            selectedConnection = comboBoxConnections.SelectedItem as DbConnection;
            LoadSelectedProfile();
            loadingProfile = false;
            UpdateProfileButtons();
        }

        private void ComboBoxConnections_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (loadingProfile)
                return;

            StageSelectedProfile();
            selectedConnection = comboBoxConnections.SelectedItem as DbConnection;
            LoadSelectedProfile();
            testedProfileId = null;
            OK_Button.Enabled = false;
            UpdateProfileButtons();
        }

        private void LoadSelectedProfile()
        {
            if (selectedConnection == null)
                return;

            loadingProfile = true;
            textBoxProfileName.Text = selectedConnection.ProfileName;
            Txt_ServerAddress.Text = selectedConnection.Host;
            Txt_PortNumber.Text = selectedConnection.Port;
            Txt_DatabaseName.Text = string.IsNullOrWhiteSpace(selectedConnection.Database)
                ? MongoDbDefaults.DatabaseName
                : selectedConnection.Database;
            Txt_Password.Text = selectedConnection.PasswordProtected
                ? WindowsDataProtection.Unprotect(selectedConnection.Pass ?? string.Empty)
                : selectedConnection.Pass;
            Txt_UserName.Text = selectedConnection.User;
            ComboBoxAuthMechanism.Text = ComboBoxAuthMechanism.Items.Contains(selectedConnection.AuthMechanism)
                ? selectedConnection.AuthMechanism
                : MongoDbDefaults.ScramSha256;
            Txt_AuthDatabase.Text = selectedConnection.AuthDatabase;
            Txt_ConnString.Text = selectedConnection.Connection;
            loadingProfile = false;
        }

        private void StageSelectedProfile()
        {
            if (selectedConnection == null || loadingProfile)
                return;

            var authenticationEnabled = !ComboBoxAuthMechanism.Text.Equals(
                MongoDbDefaults.NoAuthentication, StringComparison.OrdinalIgnoreCase);
            selectedConnection.ProfileName = textBoxProfileName.Text.Trim();
            selectedConnection.Host = Txt_ServerAddress.Text.Trim().ToLowerInvariant();
            selectedConnection.Port = Txt_PortNumber.Text.Trim();
            selectedConnection.Database = Txt_DatabaseName.Text.Trim();
            selectedConnection.User = authenticationEnabled ? Txt_UserName.Text.Trim() : string.Empty;
            selectedConnection.Pass = authenticationEnabled
                ? WindowsDataProtection.Protect(Txt_Password.Text.Trim())
                : string.Empty;
            selectedConnection.PasswordProtected = authenticationEnabled;
            selectedConnection.AuthMechanism = ComboBoxAuthMechanism.Text.Trim();
            selectedConnection.AuthDatabase = authenticationEnabled ? Txt_AuthDatabase.Text.Trim() : string.Empty;

            try
            {
                var builder = new MongoUrlBuilder(Txt_ConnString.Text.Trim())
                {
                    Username = null,
                    Password = null
                };
                selectedConnection.Connection = builder.ToString();
            }
            catch (MongoConfigurationException)
            {
                // Build and Test owns connection-string validation and user feedback.
            }
        }

        private void ButtonAdd_Click(object? sender, EventArgs e)
        {
            StageSelectedProfile();
            var connections = settingsRoot?.Settings?.DbConnections;
            if (connections == null)
                return;

            var number = 2;
            string profileName;
            do
            {
                profileName = $"Connection {number++}";
            }
            while (connections.Any(connection => string.Equals(
                connection.ProfileName, profileName, StringComparison.OrdinalIgnoreCase)));

            var connection = new DbConnection
            {
                ProfileId = Guid.NewGuid().ToString("N"),
                ProfileName = profileName,
                Host = MongoDbDefaults.Host,
                Port = MongoDbDefaults.Port.ToString(),
                Database = MongoDbDefaults.DatabaseName,
                AuthMechanism = MongoDbDefaults.ScramSha256,
                AuthDatabase = MongoDbDefaults.DatabaseName,
                Connection = $"mongodb://{MongoDbDefaults.Host}:{MongoDbDefaults.Port}/{MongoDbDefaults.DatabaseName}"
            };
            connections.Add(connection);
            testedProfileId = null;
            OK_Button.Enabled = false;
            RefreshConnectionList(connection.ProfileId);
            textBoxProfileName.Focus();
            textBoxProfileName.SelectAll();
        }

        private void ButtonDelete_Click(object? sender, EventArgs e)
        {
            var connections = settingsRoot?.Settings?.DbConnections;
            if (connections == null || selectedConnection == null || connections.Count <= 1)
                return;

            var result = MessageBox.Show(
                $"Delete the '{selectedConnection.ProfileName}' connection profile?",
                "Delete MongoDB Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            connections.Remove(selectedConnection);
            testedProfileId = null;
            OK_Button.Enabled = false;
            RefreshConnectionList(connections[0].ProfileId);
        }

        private void UpdateProfileButtons()
        {
            buttonDelete.Enabled = (settingsRoot?.Settings?.DbConnections?.Count ?? 0) > 1;
        }

        private void DbCredentialsDialog_Deactivate(object sender, EventArgs e)
        {
            // Check if the form is active and bring it to the front
            if (this.ContainsFocus)
            {
                this.Activate();
            }
        }

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            var profileName = textBoxProfileName.Text.Trim();
            var hostName = Txt_ServerAddress.Text.ToLower().Trim();
            var portNumber = Txt_PortNumber.Text.Trim();
            var userName = Txt_UserName.Text.Trim();
            var password = Txt_Password.Text.Trim();
            var databaseName = Txt_DatabaseName.Text.Trim();
            var authMechanism = ComboBoxAuthMechanism.Text.Trim();
            var authDatabase = Txt_AuthDatabase.Text.Trim();
            var connStr = Txt_ConnString.Text.Trim();

            var vFlag = ValidateForm(profileName, ref hostName, ref portNumber, ref userName, ref password,
                ref databaseName, ref connStr, authMechanism);
            if (vFlag && selectedConnection != null && settingsRoot?.Settings != null &&
                testedProfileId == selectedConnection.ProfileId)
            {
                StageSelectedProfile();
                ActiveProfileChanged = originalActiveProfileId != selectedConnection.ProfileId ||
                    originalActiveConnectionSignature != CreateConnectionSignature(selectedConnection);
                settingsRoot.Settings.ActiveDbConnectionId = selectedConnection.ProfileId;
                settingsRoot.Settings.DbConnection = selectedConnection;
                await AppSettingsJson.WriteSettingsAsync(settingsRoot);

                // Close the Dialog
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private static string CreateConnectionSignature(DbConnection? connection)
        {
            if (connection == null)
                return string.Empty;

            var password = connection.PasswordProtected
                ? WindowsDataProtection.Unprotect(connection.Pass ?? string.Empty)
                : connection.Pass;
            return string.Join("\u001f", connection.Host, connection.Port, connection.Database,
                connection.User, password,
                connection.AuthDatabase, connection.AuthMechanism, connection.Connection);
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void Btn_Test_Click(object sender, EventArgs e)
        {
            var profileName = textBoxProfileName.Text.Trim();
            var hostName = Txt_ServerAddress.Text.ToLower().Trim();
            var portNumber = Txt_PortNumber.Text.ToLower().Trim();
            var userName = Txt_UserName.Text.Trim();
            var password = Txt_Password.Text.Trim();
            var databaseName = Txt_DatabaseName.Text.Trim();
            var authMechanism = ComboBoxAuthMechanism.Text.Trim();
            var authDatabase = Txt_AuthDatabase.Text.Trim();
            var connStr = Txt_ConnString.Text.Trim();
            var vFlag = ValidateForm(profileName, ref hostName, ref portNumber, ref userName, ref password,
                ref databaseName, ref connStr, authMechanism, false);
            if (vFlag)
            {
                var connectionBuilder = new MongoUrlBuilder
                {
                    Server = new MongoServerAddress(hostName, int.Parse(portNumber)),
                    DatabaseName = databaseName
                };

                var authenticationEnabled = !authMechanism.Equals(
                    MongoDbDefaults.NoAuthentication, StringComparison.OrdinalIgnoreCase);
                if (authenticationEnabled)
                {
                    connectionBuilder.Username = userName;
                    connectionBuilder.Password = password;
                    connectionBuilder.AuthenticationMechanism = authMechanism;

                    if (!string.IsNullOrWhiteSpace(authDatabase))
                        connectionBuilder.AuthenticationSource = authDatabase;
                }

                var testConn = connectionBuilder.ToString();

                Txt_ConnString.Text = testConn;
                var testResult = await mongoDbRepository.TestConnectionStringAsync(testConn, databaseName);
                if (!testResult.Success)
                {
                    testedProfileId = null;
                    OK_Button.Enabled = false;
                    Btn_Test.BackColor = Color.FromArgb(255, 220, 20, 60);
                    Btn_Test.ForeColor = Color.White;
                    Btn_Test.Text = "Failed";
                    Txt_ConnString.ForeColor = Color.FromArgb(255, 220, 20, 60);
                    Lbl_Header.Text = "MongoDB connection failed";
                    MessageBox.Show(testResult.Message, "MongoDB Connection",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Btn_Test.BackColor = Color.FromArgb(255, 34, 139, 34);
                    Btn_Test.ForeColor = Color.White;
                    Btn_Test.Text = "Passed";
                    Txt_ConnString.ForeColor = Color.FromArgb(255, 0, 100, 0);
                    Lbl_Header.Text = "You got this, Lets Go!";
                    StageSelectedProfile();
                    testedProfileId = selectedConnection?.ProfileId;
                    OK_Button.Enabled = selectedConnection != null;
                    Application.DoEvents();
                }

                Application.DoEvents();
            }
        }

        private bool ValidateForm(
            string profileName,
            ref string hostName, ref string portNumber, ref string userName, ref string password,
            ref string databaseName, ref string connStr,
            string authMechanism,
            bool requireConnectionString = true)
        {
            var pValue = true;
            var duplicateProfileName = settingsRoot?.Settings?.DbConnections?.Any(connection =>
                connection.ProfileId != selectedConnection?.ProfileId &&
                string.Equals(connection.ProfileName, profileName, StringComparison.OrdinalIgnoreCase)) == true;
            if (string.IsNullOrWhiteSpace(profileName) || profileName.Length > 100 || duplicateProfileName)
            {
                textBoxProfileName.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                textBoxProfileName.ResetBackColor();
            }

            if (hostName.Length == 0)
            {
                Txt_ServerAddress.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_ServerAddress.ResetBackColor();
            }

            if (!int.TryParse(portNumber, out var port) || port is < 1 or > 65535)
            {
                Txt_PortNumber.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_PortNumber.ResetBackColor();
            }

            var invalidDatabaseCharacters = new[] { '/', '\\', '.', ' ', '"', '$', '*', '<', '>', ':', '|', '?' };
            if (string.IsNullOrWhiteSpace(databaseName) ||
                System.Text.Encoding.UTF8.GetByteCount(databaseName) > 63 ||
                databaseName.IndexOfAny(invalidDatabaseCharacters) >= 0)
            {
                Txt_DatabaseName.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_DatabaseName.ResetBackColor();
            }

            var authenticationEnabled = !authMechanism.Equals(
                MongoDbDefaults.NoAuthentication, StringComparison.OrdinalIgnoreCase);

            if (authenticationEnabled && userName.Length == 0)
            {
                Txt_UserName.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_UserName.ResetBackColor();
            }

            if (authenticationEnabled && password.Length == 0)
            {
                Txt_Password.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_Password.ResetBackColor();
            }

            if (requireConnectionString && connStr.Length == 0)
            {
                Txt_ConnString.BackColor = Color.FromArgb(255, 255, 192, 203);
                pValue = false;
            }
            else
            {
                Txt_ConnString.ResetBackColor();
            }

            return pValue;
        }



    }
}
