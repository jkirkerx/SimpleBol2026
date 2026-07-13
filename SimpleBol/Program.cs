using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SimpleBol.Classes.Errors;
using SimpleBol.NewtonSoft;
using SimpleBol.Properties;
using SimpleBol.Repository.MongoDb;
using SimpleBol.WinForms;
using SimpleBol.WinForms.Dialogs;
using SimpleBol.WinForms.Forms;
using SimpleBol.Services.sendEngine;
using SimpleBol.Classes.DI;
using MongoDB.Driver;
using SimpleBol.Context.MongoDb;
using SimpleBol.Setup;
using SimpleBol.Services;
using NLog;

namespace SimpleBol
{
    internal static class Program
    {

        public static SplashScreenForm? SplashScreenDI;

        public static IServiceProvider? ServiceProvider { get; private set; }
        private static void ConfigureServices(IServiceCollection services)
        {
            // Scoped so first-run setup can replace the connection settings
            // before the main application scope is created.
            services.AddScoped<IMongoClient>(_ =>
            {
                var rootObject = AppSettingsJson.GetSettings();
                var settings = rootObject?.Settings?.DbConnection
                    ?? throw new InvalidOperationException("The MongoDB connection is not configured.");

                return MongoDbConnectionFactory.CreateClient(settings);
            });
            services.AddScoped<MongoDbContext>();

            services.AddTransient<IMongoDbRepository, MongoDbRepository>();
            services.AddTransient<IAccountsRepository, AccountsRepository>();
            services.AddTransient<IBillingDisputesRepository, BillingDisputeRepository>();
            services.AddTransient<IBillToAccountsRepository, BillToAccountsRepository>();
            services.AddTransient<IBolsRepository, BolsRepository>();
            services.AddTransient<IFreightClassCodesRepository, FreightClassCodesRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();            
            services.AddTransient<INmfcCodesRepository, NmfcCodesRepository>();
            services.AddTransient<IShipperRepository, ShipperRepository>();
            services.AddTransient<ISmtpApiSettingsRepository, SmtpApiSettingsRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();

            services.AddSingleton<ISendGridSender, SendGridSender>();
            services.AddSingleton<IGmailSender, GmailSender>();            
            services.AddSingleton<IOutlook365Sender, Outlook365Sender>();
            services.AddSingleton<ICurrentUserSession, CurrentUserSession>();

            services.AddScoped<MainMdiForm>();
            services.AddTransient<SplashScreenForm>();
            services.AddTransient<MongoDbCredentialsDialog>();
            services.AddTransient<SmtpApiSettingsDialog>();
            services.AddTransient<AboutForm>();
            services.AddTransient<ProgramMenuForm>();
            services.AddTransient<ShippersForm>();
            services.AddTransient<ShipperDialog>();
            services.AddTransient<ShipperContactEditorDialog>();
            services.AddTransient<BolsForm>();
            services.AddTransient<BolDialog>();
            services.AddTransient<AccountsForm>();
            services.AddTransient<AccountDialog>();
            services.AddTransient<VendorsForm>();
            services.AddTransient<VendorDialog>();            
            services.AddTransient<CustomersForm>();
            services.AddTransient<CustomerDialog>();
            services.AddTransient<LocationDialog>();
            services.AddTransient<PackageDialog>();
            services.AddTransient<PalletDialog>();
            services.AddTransient<BillToAccountsForm>();
            services.AddTransient<BillToAccountDialog>();            
            services.AddTransient<LoginDialog>();
            services.AddTransient<PrintBolDialog>();
            services.AddTransient<ProgressDialog>();
            services.AddTransient<NmfcFreightCodesForm>();
            services.AddTransient<NmfcFreightCodeDialog>();
            services.AddTransient<FreightClassCodesForm>();
            services.AddTransient<FreightClassCodeDialog>();
            services.AddTransient<BillingDisputesForm>();
            services.AddTransient<BillingDisputeDialog>();

        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            NLogConfiguration.Configure();
            AppDomain.CurrentDomain.ProcessExit += (_, _) => LogManager.Shutdown();
            ErrorLogging.NLogInformation("Application Startup - " + AppInfo.Version);

            // Register Services for Direct Injection
            AppSettingsJson.CreateAppSettings();
            AppSettingsJson.MigrateMongoDbConnectionProfiles();
            AppSettingsJson.ProtectMongoDbCredentials();
            var services = new ServiceCollection();
            ConfigureServices(services);
            using var serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateScopes = true,
                ValidateOnBuild = true
            });
            ServiceProvider = serviceProvider;
            var formScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            AppDomain.CurrentDomain.UnhandledException += MyUnhandledExceptionHandler;
            Application.ThreadException += MyThreadExceptionHandler;

            // Add the ceTe DynamicPdf license
            ceTe.DynamicPDF.Document.AddLicense("GEN80NPDMLPGLCJv4IyDHFmIgoCAW/4Yl5hq3uUQrTXYd7eBrDOTIAhO6pmzMNpuEjx+JkUtx4VBnbd1PECHsuW4k71/nU6KA6vg");

            // Initialize the Splash Screen
            using var splashScreenScope = formScopeFactory.CreateOwnedForm<SplashScreenForm>();
            SplashScreenDI = splashScreenScope.Form;
            SplashScreenDI.StartPosition = FormStartPosition.CenterScreen;
            SplashScreenDI.TopMost = true;
            var splashResult = SplashScreenDI.ShowDialog();

            // The splash screen performs the startup database check in its Shown
            // event. Do not create a main form when that check aborted startup.
            if (SplashScreenDI.StartupAborted || splashResult != DialogResult.OK)
            {
                return;
            }

            using (var accountCheckScope = serviceProvider.CreateScope())
            {
                var accountsRepository = accountCheckScope.ServiceProvider
                    .GetRequiredService<IAccountsRepository>();

                if (accountsRepository.AnyAccountsAsync().GetAwaiter().GetResult())
                {
                    var loginRequired = accountsRepository.AnySuccessfulLoginAsync()
                        .GetAwaiter().GetResult();

                    if (!loginRequired)
                    {
                        var setupChoice = MessageBox.Show(
                            "SimpleBol found accounts created before login was enabled, but no user has successfully signed in yet."
                            + Environment.NewLine + Environment.NewLine
                            + "Select Yes to sign in now, or No to continue to Account Setup and review/reset the legacy accounts.",
                            "Legacy Account Setup",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Information);

                        if (setupChoice == DialogResult.Cancel)
                            return;

                        loginRequired = setupChoice == DialogResult.Yes;
                    }

                    if (loginRequired)
                    {
                        using var loginScope = formScopeFactory.CreateOwnedForm<LoginDialog>();
                        var loginDialog = loginScope.Form;
                        loginDialog.StartPosition = FormStartPosition.CenterScreen;

                        if (loginDialog.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
            }

            using var mainFormScope = formScopeFactory.CreateOwnedForm<MainMdiForm>();
            var mainMdiForm = mainFormScope.Form;
            mainMdiForm.FormClosed += MainForm_FormClosed;

            // Run the Main Form
            Application.Run(mainMdiForm);
            
        }

        private static void MainForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // Hide the calling Form
            var form = sender as Form;
            form?.Hide();
        }

        public static void MyUnhandledExceptionHandler(object? sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception
                ?? new Exception("An unknown unhandled exception occurred.");
            HandleFatalException(exception);
        }

        private static void MyThreadExceptionHandler(object? sender, ThreadExceptionEventArgs e)
        {
            HandleFatalException(e.Exception);
        }

        private static void HandleFatalException(Exception exception)
        {
            ErrorLogging.NLogUnhandled(exception);

            // Load the SendError.exe
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var exePath = Path.Combine(appPath, Resources.SendError);
            if (File.Exists(exePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo(exePath) { UseShellExecute = true });
                }
                catch (Exception launchException)
                {
                    ErrorLogging.NLogException(launchException, "Launch SendError");
                }
            }

            Application.Exit();
        }
    }
}
