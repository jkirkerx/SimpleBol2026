using SimpleBol.Classes.DI;
using SimpleBol.WinForms;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.Classes.Common;
using SimpleBol.Classes.Errors;
using SimpleBol.Models;
using SimpleBol.Properties;
using SimpleBol.Repository.MongoDb;
using SimpleBol.Setup;
using System.IO;
using System.Net.Http;
using System.ServiceProcess;

namespace SimpleBol.WinForms
{
    public partial class SplashScreenForm : Form
    {
        public bool StartupAborted { get; private set; }

        private Models.Settings _settings { get; set; } = null!;
        private readonly IServiceScopeFactory serviceProvider;
        private readonly IMongoDbRepository mongoDbRepository;

        public SplashScreenForm(
            IServiceScopeFactory serviceProvider,
            IMongoDbRepository mongoDbRepository)
        {
            InitializeComponent();
            lblStatus.Text = $"initializing {AppInfo.Name}";
            this.serviceProvider = serviceProvider;
            this.mongoDbRepository = mongoDbRepository;

        }

        private void FormSplashScreenLoad(object sender, EventArgs e)
        {
            applicationTitle.Text = AppInfo.Name;
            applicationVersion.Text = AppInfo.Version;
            copyright.Text = AppInfo.Copyright;
        }

        private async void FormSplashScreenShown(object sender, EventArgs e)
        {
            var minimumDisplayTime = Task.Delay(TimeSpan.FromSeconds(2));

            // Create the required folders
            lblStatus.Text = "Setting up program folders";

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(appData + Resources.PathSimpleBol))
                Directory.CreateDirectory(appData + Resources.PathSimpleBol);
            if (!Directory.Exists(appData + Resources.PathErrorLogs))
                Directory.CreateDirectory(appData + Resources.PathErrorLogs);

            // Create the MongoDb Data folder
            if (!Directory.Exists(appData + Resources.MongoDbDataPath))
                Directory.CreateDirectory(appData + Resources.MongoDbDataPath);

            // Create the MongoDb Logs folder
            if (!Directory.Exists(appData + Resources.MongoDbLogsPath))
                Directory.CreateDirectory(appData + Resources.MongoDbLogsPath);

            // Check if MongoDb is Active
            lblStatus.Text = "Looking for MongoDB.....";

            try
            {
                // Check for our named instance of MongoDb
                var mongoFlag = false;
                var serviceController = new ServiceController(MongoDbDefaults.ServiceName, Environment.MachineName);

                try
                {
                    var status = serviceController.Status;
                    lblStatus.Text = "Service MongoDb is " + status;
                    switch (status)
                    {
                        case ServiceControllerStatus.Running:
                            mongoFlag = true;
                            break;

                        case ServiceControllerStatus.Stopped:
                            try
                            {
                                serviceController.Start();
                                await Task.Run(() => serviceController.WaitForStatus(
                                    ServiceControllerStatus.Running,
                                    TimeSpan.FromSeconds(15)));
                                mongoFlag = serviceController.Status == ServiceControllerStatus.Running;
                                Console.Write(status);
                            }
                            catch (Exception ex)
                            {
                                lblStatus.Text = ex.ToString();
                            }

                            break;

                        case ServiceControllerStatus.Paused:
                            mongoFlag = true;
                            Console.Write(status);
                            break;

                        case ServiceControllerStatus.StartPending:
                            mongoFlag = true;
                            Console.Write(status);
                            break;

                        case ServiceControllerStatus.StopPending:
                            mongoFlag = true;
                            Console.Write(status);
                            break;
                    }

                    serviceController.Dispose();

                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                lblStatus.Text = "Attaching to MongoDB";

                // Test if our conn str to MongoDb is good                
                var connectionResult = await mongoDbRepository.TestConnectionAsync();
                if (connectionResult.Success)
                {
                    lblStatus.Text = "Initializing MongoDB collections and indexes";
                    await mongoDbRepository.InitializeDatabaseAsync();
                    lblStatus.Text = "MongoDB connected successfully!";
                    Global360.GOnline = true;
                }
                else
                {
                    // Oh no, big trouble in China town.
                    lblStatus.Text = "MongoDB failed to connect ....";
                    MessageBox.Show(connectionResult.Message, "MongoDB Connection",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mongoFlag = false;
                }
                lblStatus.Text = "Launching, let's go!";

                // Give this more thought, I just tested the connection ....
                if (!mongoFlag)
                {
                    var result = MessageBox.Show(
                        "MongoDb as a service was detected," + Environment.NewLine +
                        " Would you like to setup the username and password?",
                    "MongoDB", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        using var dbCredentialsFormDIOwned = serviceProvider.CreateOwnedForm<MongoDbCredentialsDialog>();
                var dbCredentialsFormDI = dbCredentialsFormDIOwned.Form;
                        dbCredentialsFormDI.FirstTime = false;
                        dbCredentialsFormDI.Dock = DockStyle.None;
                        dbCredentialsFormDI.StartPosition = FormStartPosition.CenterScreen;

                        var authResult = dbCredentialsFormDI.ShowDialog();
                        if (authResult != DialogResult.OK)
                        {
                            ShutdownApplication();
                            return;
                        }
                    }
                    else
                    {
                        ShutdownApplication();
                        return;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Check and make sure were online
                Console.Write(ex);
                var onlineStatus = await HttpTest.TestHomePageAsync();
                if (!onlineStatus)
                {
                    MessageBox.Show("We don't appear to be online," + Environment.NewLine +
                        "Establish an internet connection and try again", "Internet Connection",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShutdownApplication();
                    return;
                }

                try
                {
                    // Service Not Found, We must install it
                    lblStatus.Text = $"Downloading MongoDB Community Edition {MongoDbInstallerInfo.ServerVersion}";
                    PanelDownload.Visible = true;

                    var downloadDirectory = appData + Resources.PathDownloads;
                    Directory.CreateDirectory(downloadDirectory);
                    var downloadPath = Path.Combine(
                        downloadDirectory,
                        MongoDbInstallerInfo.InstallerFileName);

                    await DownloadFileWithProgressAsync(
                        MongoDbInstallerInfo.InstallerDownloadUrl, downloadPath);
                    lblStatus.Text = $"Installing MongoDB Community Edition {MongoDbInstallerInfo.ServerVersion}";
                    _settings = await MongoDbCommunityEdition.SetupAsync();
                }
                catch (Exception setupException)
                {
                    ErrorLogging.NLogException(setupException, "MongoDB first-run setup");
                    MessageBox.Show(
                        setupException.Message + Environment.NewLine + Environment.NewLine +
                        "You can start SimpleBol again to retry setup.",
                        "MongoDB Setup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShutdownApplication();
                    return;
                }

            }

            await minimumDisplayTime;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ShutdownApplication()
        {
            var exitResult = MessageBox.Show(
                "This is no reason to continue if we can't access the database" + Environment.NewLine +
                $"{AppInfo.Name} will shut down now",
                AppInfo.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (exitResult == DialogResult.OK)
            {
                StartupAborted = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private async Task DownloadFileWithProgressAsync(string downloadUrl, string downloadPath)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength ?? 0;
            var bytesRead = 0L;
            var buffer = new byte[81920];

            await using var contentStream = await response.Content.ReadAsStreamAsync();
            await using var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, true);

            int read;
            while ((read = await contentStream.ReadAsync(buffer)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, read));
                bytesRead += read;

                lblDownloadUrl.Text = downloadUrl;
                lblDownloadBytes.Text = totalBytes > 0
                    ? "Downloaded " + BytesToString(bytesRead) + " of " + BytesToString(totalBytes)
                    : "Downloaded " + BytesToString(bytesRead);

                if (totalBytes > 0)
                {
                    pbDownload.Value = Math.Min(100, (int)(bytesRead * 100 / totalBytes));
                }

            }

            PanelDownload.Visible = false;
        }

        // I know this should be in a class and made reusable
        private static string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);

            return (Math.Sign(byteCount) * num) + suf[place];
        }


    }

}
