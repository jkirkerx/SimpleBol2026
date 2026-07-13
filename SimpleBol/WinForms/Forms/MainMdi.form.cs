using SimpleBol.Classes.DI;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.Classes.Errors;
using SimpleBol.Classes.Timed_Events;
using SimpleBol.Classes.UI;
using SimpleBol.WinForms;
using SimpleBol.Models;
using SimpleBol.NewtonSoft;
using SimpleBol.Properties;
using System.Diagnostics;
using System.Drawing.Configuration;
using System.IO;
using System.Reflection;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms.Dialogs;
using SimpleBol.WinForms.Forms;
using SimpleBol.Classes.NewtonSoft;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Setup;

namespace SimpleBol.WinForms
{
    public partial class MainMdiForm : Form
    {
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IMongoDbRepository? mongoDbRepository;
        private readonly ICommonRepository? commonRepository;
        private readonly ISmtpApiSettingsRepository? smtpCredentialsRepository;

        private List<MainMdiForm> mainMdiForms = new();
        private Point newRestoredLocation = Point.Empty;

        public MainMdiForm(
            IServiceScopeFactory serviceProvider,
            IMongoDbRepository mongoDbRepository,
            ICommonRepository commonRepository,
            ISmtpApiSettingsRepository smtpCredentialsRepository)
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.mongoDbRepository = mongoDbRepository;
            this.commonRepository = commonRepository;
            this.smtpCredentialsRepository = smtpCredentialsRepository;
            BuildCompanyConnectionMenu();

            // Make sure that the folder in AppData Exist first so we can store text files
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Resources.PathMarketPlace;
            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);

            // Create the folder for the PDF to be stored in
            var pdfPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Resources.PathBolDocuments;
            if (!Directory.Exists(pdfPath))
                Directory.CreateDirectory(pdfPath);

            // Access the MenuStrip and disable the 2 collection items
            var strVersion = AppInfo.Version;

#if DEBUG
            errorLoggingToolStripMenuItem.Enabled = true;
            ErrorLogging.NLogInformation("Running Debug Version " + strVersion);
#else
            ErrorLogging.NLogInformation("Running Production Version " + strVersion);
            errorLoggingToolStripMenuItem.Enabled = false;
#endif

        }

        #region Form

        private void MainMdiFormLoad(object sender, EventArgs e)
        {
            Application.DoEvents();

            this.Text = AppInfo.BuildTitle;

            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;

            // Register the Cete Dynamic PDF License
            ceTe.DynamicPDF.Document.AddLicense("GEN80NPDMLPGLCJv4IyDHFmIgoCAW/4Yl5hq3uUQrTXYd7eBrDOTIAhO6pmzMNpuEjx+JkUtx4VBnbd1PECHsuW4k71/nU6KA6vg");

            // All the Configuration Requirements have been met, so just run normal
            RestoreFormSizeNPosition(this);

            // Remove the 3D effects or bevel from MdiContainer
            MdiBevel.SetMdiBevel(this, false);

            // Turn off the menu items in Programs, until an account is selected
            //GetDataToolStripMenuItem.Enabled = false;
            //ViewDataToolStripMenuItem.Enabled = false;

            // Force a fatal exception to test the nLog
            // FatalEvent.ForceException();

            if (serviceProvider is not null)
            {
                var menuFormDIOwned = serviceProvider.CreateOwnedForm<ProgramMenuForm>();
                var menuFormDI = menuFormDIOwned.Form;
                menuFormDI.MdiParent = this;
                menuFormDI.Dock = DockStyle.Fill;
                menuFormDIOwned.Show();
            }

            Application.DoEvents();
        }

        private async void MainMdiFormShown(object sender, EventArgs e)
        {
            // Test and see if we can reach the website
            var gOnlineTask = await Task.Run(() => InitializeTimer());
            if (!gOnlineTask)
            {
                toolStripStatusLabelConnectivity.Text = "Offline";
                timerEvent.Tick += new EventHandler(TimerEventTick);
                MessageBox.Show("Your computer is offline?" + Environment.NewLine + "To work online, establish a connection to the internet, then proceed", "Wireless Resellser", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                toolStripStatusLabelConnectivity.Text = "Online";
            }
        }

        private void MainMdiFormClosed(object sender, FormClosedEventArgs e)
        {

            Cursor = Cursors.WaitCursor;

            // Save the Screen Parameters
            var mainMdiForm = sender as MainMdiForm;
            if (mainMdiForm is not null)
            {
                SaveFormSizeNPosition(mainMdiForm);
            }

            AppSettingsJson.BackupSettings();
            PrintSettingsJson.BackupPrintSettings();
            ErrorLogging.NLogInformation("Application Closing");

        }

        #endregion
        #region ToolStrip

        private void BolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var createBolFormDIOwned = serviceProvider.CreateOwnedForm<BolsForm>();
                var createBolFormDI = createBolFormDIOwned.Form;
                createBolFormDI.MdiParent = MainMdiForm.ActiveForm;
                createBolFormDI.Dock = DockStyle.Fill;
                createBolFormDIOwned.Show();
                Application.DoEvents();
            }

        }

        private void ShippersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var shippersFormDIOwned = serviceProvider.CreateOwnedForm<ShippersForm>();
                var shippersFormDI = shippersFormDIOwned.Form;
                shippersFormDI.MdiParent = MainMdiForm.ActiveForm;
                shippersFormDI.Dock = DockStyle.Fill;
                shippersFormDIOwned.Show();
                Application.DoEvents();
            }

        }

        private void VendorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var vendorsFormDIOwned = serviceProvider.CreateOwnedForm<VendorsForm>();
                var vendorsFormDI = vendorsFormDIOwned.Form;
                vendorsFormDI.MdiParent = MainMdiForm.ActiveForm;
                vendorsFormDI.Dock = DockStyle.Fill;
                vendorsFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void CustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var customersFormDIOwned = serviceProvider.CreateOwnedForm<CustomersForm>();
                var customersFormDI = customersFormDIOwned.Form;
                customersFormDI.MdiParent = MainMdiForm.ActiveForm;
                customersFormDI.Dock = DockStyle.Fill;
                customersFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void NMFCCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var nmfcFreightCodesFormDIOwned = serviceProvider.CreateOwnedForm<NmfcFreightCodesForm>();
                var nmfcFreightCodesFormDI = nmfcFreightCodesFormDIOwned.Form;
                nmfcFreightCodesFormDI.MdiParent = MainMdiForm.ActiveForm;
                nmfcFreightCodesFormDI.Dock = DockStyle.Fill;
                nmfcFreightCodesFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void FreightClassCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var freightClassCodesFormDIOwned = serviceProvider.CreateOwnedForm<FreightClassCodesForm>();
                var freightClassCodesFormDI = freightClassCodesFormDIOwned.Form;
                freightClassCodesFormDI.MdiParent = MainMdiForm.ActiveForm;
                freightClassCodesFormDI.Dock = DockStyle.Fill;
                freightClassCodesFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void BillingDisputesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var billingDisputesFormDIOwned = serviceProvider.CreateOwnedForm<BillingDisputesForm>();
                var billingDisputesFormDI = billingDisputesFormDIOwned.Form;
                billingDisputesFormDI.MdiParent = MainMdiForm.ActiveForm;
                billingDisputesFormDI.Dock = DockStyle.Fill;
                billingDisputesFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void BillTo3rdPartyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var billToAccountsFormDIOwned = serviceProvider.CreateOwnedForm<BillToAccountsForm>();
                var billToAccountsFormDI = billToAccountsFormDIOwned.Form;
                billToAccountsFormDI.MdiParent = MainMdiForm.ActiveForm;
                billToAccountsFormDI.Dock = DockStyle.Fill;
                billToAccountsFormDIOwned.Show();
                Application.DoEvents();
            }


        }

        private void ErrorLoggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Process.Start("explorer.exe", appData + Resources.PathErrorLogs);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm()
            {
                MdiParent = this,
                Dock = DockStyle.None,
                StartPosition = FormStartPosition.CenterScreen
            };
            aboutForm.Show();
            Application.DoEvents();
        }

        private void MongoDbCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null)
            {
                using var ownedDialog = serviceProvider.CreateOwnedForm<MongoDbCredentialsDialog>();
                var mongoDbCredentialsDialog = ownedDialog.Form;
                mongoDbCredentialsDialog.Dock = DockStyle.None;
                mongoDbCredentialsDialog.StartPosition = FormStartPosition.CenterScreen;

                var result = mongoDbCredentialsDialog.ShowDialog(this);
                if (result == DialogResult.OK && mongoDbCredentialsDialog.ActiveProfileChanged)
                {
                    RestartForMongoConnectionChange();
                }
            }
        }

        private void BuildCompanyConnectionMenu()
        {
            companyConnectionToolStripMenuItem.DropDownItems.Clear();
            var rootObject = AppSettingsJson.GetSettings();
            var settings = rootObject?.Settings;
            var connections = settings?.DbConnections ?? new List<DbConnection>();
            var activeConnection = connections.FirstOrDefault(connection =>
                connection.ProfileId == settings?.ActiveDbConnectionId);
            var companyName = rootObject?.SmtpApiSettings?.CompanyInfo?.CompanyName;
            var contextName = string.IsNullOrWhiteSpace(companyName)
                ? activeConnection?.ProfileName ?? "Not configured"
                : $"{companyName} — {activeConnection?.ProfileName ?? "Not configured"}";

            companyConnectionToolStripMenuItem.Text = "Connection";
            companyConnectionToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(
                $"Current: {contextName}") { Enabled = false });
            companyConnectionToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());

            foreach (var connection in connections)
            {
                var profileItem = new ToolStripMenuItem(connection.ProfileName ?? "Unnamed connection")
                {
                    Tag = connection.ProfileId,
                    Checked = connection.ProfileId == settings?.ActiveDbConnectionId
                };
                profileItem.Click += SwitchConnectionProfile_Click;
                companyConnectionToolStripMenuItem.DropDownItems.Add(profileItem);
            }

            companyConnectionToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            var manageItem = new ToolStripMenuItem("Manage Connections…");
            manageItem.Click += MongoDbCredentialsToolStripMenuItem_Click;
            companyConnectionToolStripMenuItem.DropDownItems.Add(manageItem);

            var companySettingsItem = new ToolStripMenuItem("Company Settings…");
            companySettingsItem.Click += SmtpCredentialsToolStripMenuItem_Click;
            companyConnectionToolStripMenuItem.DropDownItems.Add(companySettingsItem);
        }

        private async void SwitchConnectionProfile_Click(object? sender, EventArgs e)
        {
            if (sender is not ToolStripMenuItem { Tag: string profileId })
                return;

            var rootObject = AppSettingsJson.GetSettings();
            var settings = rootObject?.Settings;
            if (rootObject == null || settings?.DbConnections == null ||
                settings.ActiveDbConnectionId == profileId)
                return;

            var selectedProfile = settings.DbConnections.FirstOrDefault(connection =>
                connection.ProfileId == profileId);
            if (selectedProfile == null || string.IsNullOrWhiteSpace(selectedProfile.Database))
                return;

            try
            {
                var client = MongoDbConnectionFactory.CreateClient(selectedProfile);
                await client.GetDatabase(selectedProfile.Database)
                    .RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));

                settings.ActiveDbConnectionId = selectedProfile.ProfileId;
                settings.DbConnection = selectedProfile;
                await AppSettingsJson.WriteSettingsAsync(rootObject);
                RestartForMongoConnectionChange();
            }
            catch (Exception ex) when (ex is MongoException or TimeoutException or FormatException)
            {
                var result = MongoDbRepository.DescribeConnectionFailure(ex);
                MessageBox.Show(result.Message, "MongoDB Connection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static void RestartForMongoConnectionChange()
        {
            MessageBox.Show(
                "The active MongoDB connection changed. SimpleBol will restart so every open form uses the new database.",
                "MongoDB Connection Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
            Application.ExitThread();
        }

        private void SmtpCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (serviceProvider != null && commonRepository != null && smtpCredentialsRepository != null)
            {
                var smtpApiSettingsDialog = new SmtpApiSettingsDialog(serviceProvider, commonRepository, smtpCredentialsRepository)
                {
                    Dock = DockStyle.None,
                    StartPosition = FormStartPosition.CenterScreen
                };
                smtpApiSettingsDialog.Show();
                Application.DoEvents();
            }

        }

        private void AccountsCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                if (form.Name != "MainMdiForm")
                    form.Close();
            }

            if (serviceProvider != null)
            {
                var accountsFormDIOwned = serviceProvider.CreateOwnedForm<AccountsForm>();
                var accountsFormDI = accountsFormDIOwned.Form;
                accountsFormDI.MdiParent = MainMdiForm.ActiveForm;
                accountsFormDI.Dock = DockStyle.Fill;
                accountsFormDIOwned.Show();
                Application.DoEvents();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFormSizeNPosition(this);

            // Close all the open forms
            for (var i = Application.OpenForms.Count - 1; i >= 1; i += -1)
            {
                var form = Application.OpenForms[i];
                form.Close();
            }
        }

        #endregion
        #region Form Location

        protected void MainMdiFormLocationChanged(object sender, EventArgs e)
        {
            if (sender is MainMdiForm mainMdiForm)
            {
                if (mainMdiForm.WindowState == FormWindowState.Maximized)
                {
                    // get the center location of the form in case like RibbonForm will be bigger and maximized Location wll be negative value that Screen.FromPoint(mainMdiForm.Location) will going to the other monitor resides on the left or top of primary monitor.
                    // Another thing, you might consider the form is in the monitor even if the location (top left corner) is on another monitor because majority area is on the monitor, so center point is the best way.
                    var centerFormMaximized =
                        new Point(mainMdiForm.DesktopBounds.Left + mainMdiForm.DesktopBounds.Width / 2,
                            mainMdiForm.DesktopBounds.Top + mainMdiForm.DesktopBounds.Height / 2);
                    var centerFormRestored = new Point(mainMdiForm.RestoreBounds.Left + mainMdiForm.RestoreBounds.Width / 2,
                        mainMdiForm.RestoreBounds.Top + mainMdiForm.RestoreBounds.Height / 2);
                    var screenMaximized = Screen.FromPoint(centerFormMaximized);
                    var screenRestored = Screen.FromPoint(centerFormRestored);
                    // we need to change the Location of mainMdiForm.RestoreBounds to the new screen where the form currently maximized.
                    // RestoreBounds does not update the Location if you change the screen but never restore to FormWindowState.Normal
                    if (screenMaximized.DeviceName != screenRestored.DeviceName)
                    {
                        newRestoredLocation = mainMdiForm.RestoreBounds.Location;
                        var screenOffsetX = screenMaximized.Bounds.Location.X - screenRestored.Bounds.Location.X;
                        var screenOffsetY = screenMaximized.Bounds.Location.Y - screenRestored.Bounds.Location.Y;
                        newRestoredLocation.Offset(screenOffsetX, screenOffsetY);
                        return;
                    }
                }

                if (mainMdiForm.WindowState == FormWindowState.Minimized)
                {
                    Global360.MfRectangle = Rectangle.Empty;
                }
                else
                {
                    Global360.MfRectangle = new Rectangle(Left, Top, Width, Height);
                }
            }

            newRestoredLocation = Point.Empty;
            Application.DoEvents();
        }

        /// <summary>
        /// Restore the form size and position with multi monitor support.
        /// </summary>
        /// <param name="mainMdiForm"></param>
        private void RestoreFormSizeNPosition(MainMdiForm mainMdiForm)
        {
            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject is not null)
            {
                if (rootObject.Settings is not null)
                {
                    if (rootObject.Settings.ScreenParameters is not null)
                    {
                        // Check if the saved bounds are nonzero and visible on any screen
                        if (rootObject.Settings.ScreenParameters.WindowPosition != Rectangle.Empty || IsVisibleOnAnyScreen((Rectangle)rootObject.Settings.ScreenParameters.WindowPosition))
                        {

                            // First set the bounds
                            mainMdiForm.StartPosition = FormStartPosition.Manual;

                            if (rootObject.Settings.ScreenParameters.WindowPosition != Rectangle.Empty)
                            {
                                mainMdiForm.DesktopBounds = rootObject.Settings.ScreenParameters.WindowPosition;
                            }

                            // Then set the window state
                            mainMdiForm.WindowState = rootObject.Settings.ScreenParameters.WindowState;
                        }
                        else
                        {
                            // This resets the upper left corner of the window to windows standards
                            mainMdiForm.StartPosition = FormStartPosition.WindowsDefaultLocation;

                            // We can still apply the saved size if not empty
                            if (rootObject.Settings.ScreenParameters.WindowPosition != Rectangle.Empty)
                            {
                                if ((Size)rootObject.Settings.ScreenParameters.WindowPosition.Size != System.Drawing.Size.Empty)
                                {
                                    mainMdiForm.Size = rootObject.Settings.ScreenParameters.WindowPosition.Size;
                                }
                            }

                        }
                    }


                }
                else
                {
                    // this is the default
                    mainMdiForm.WindowState = FormWindowState.Normal;
                    mainMdiForm.StartPosition = FormStartPosition.WindowsDefaultBounds;
                }
            }

        }
        private void PreventSameLocation(MainMdiForm mainMdiForm)
        {
            const int distance = 20;
            foreach (var otherMainForm in mainMdiForms)
            {
                if (Math.Abs(otherMainForm.Location.X - mainMdiForm.Location.X) < distance &&
                    Math.Abs(otherMainForm.Location.Y - mainMdiForm.Location.Y) < distance)
                    mainMdiForm.Location = new Point(mainMdiForm.Location.X + distance, mainMdiForm.Location.Y + distance);
            }
        }

        private bool IsVisibleOnAnyScreen(Rectangle rectangle)
        {

            foreach (var screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith((Rectangle)rectangle))
                {
                    return true;
                }

            }

            return false;


        }

        #endregion
        #region Form Size

        /// <summary>
        /// Write the Screen Settings to the appSettings Json File
        /// </summary>
        /// <param name="mainForm"></param>
        private async void SaveFormSizeNPosition(MainMdiForm mainForm)
        {
            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject != null)
            {
                if (rootObject.Settings != null)
                {
                    if (rootObject.Settings.ScreenParameters is not null)
                    {
                        rootObject.Settings.ScreenParameters.WindowState = FormWindowState.Normal;

                        if (mainForm.WindowState == FormWindowState.Normal || mainForm.WindowState == FormWindowState.Maximized)
                        {
                            rootObject.Settings.ScreenParameters.WindowState = mainForm.WindowState;
                        }


                        if (mainForm.WindowState == FormWindowState.Normal)
                        {
                            rootObject.Settings.ScreenParameters.WindowPosition = mainForm.DesktopBounds;
                        }
                        else if (newRestoredLocation == Point.Empty)
                        {
                            rootObject.Settings.ScreenParameters.WindowPosition = mainForm.RestoreBounds;
                        }
                        else
                        {
                            rootObject.Settings.ScreenParameters.WindowPosition = new Rectangle(newRestoredLocation, mainForm.RestoreBounds.Size);
                        }

                        AppSettingsJson.WriteSettings(rootObject);

                        await Task.Delay(100);
                    }
                }

            }

        }

        #endregion
        #region Timers

        private async Task<bool> InitializeTimer()
        {
            // Fire this once to get a read
            var pValue = true;
            await Task.Delay(2000);
            Global360.GOnline = WatchDogTimer.OnlineCheck();
            if (!Global360.GOnline)
            {
                ErrorLogging.NLogInformation("Application Offline on startup");
                pValue = false;
            }
            return pValue;
        }

        private void TimerEventTick(object? Sender, EventArgs e)
        {
            // Check and see if were online  
            Global360.GOnline = WatchDogTimer.OnlineCheck();
            if (Global360.GOnline)
            {
                ErrorLogging.NLogInformation("Application is now Online");
            }
            else
            {
                timerEvent.Interval = 360000;
                timerEvent.Enabled = true;
                timerEvent.Start();
            }

        }

        #endregion





        
    }
}
