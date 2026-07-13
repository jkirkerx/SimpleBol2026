using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Models;
using SimpleBol.NewtonSoft;
using SimpleBol.Properties;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.ServiceProcess;

namespace SimpleBol.Setup
{
    public class MongoDbCommunityEdition
    {
        public static async Task<Settings> SetupAsync()
        {
            // Get our path
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var downloadDirectory = appData + Resources.PathDownloads;
            var installerPath = Path.Combine(
                downloadDirectory,
                MongoDbInstallerInfo.InstallerFileName);

            if (!File.Exists(installerPath))
            {
                throw new FileNotFoundException(
                    "The MongoDB installer was not downloaded.", installerPath);
            }

            string? mongodPathForRollback = null;
            var serviceInstalled = false;
            try
            {

            // End of Folder Path Calculators
            ///////////////////////////////////////////////////////////////////////////////////////
            // Setup a watch to monitor the process

            var mDbStartInfo = new ProcessStartInfo()
            {
                WorkingDirectory = downloadDirectory,
                FileName = "msiexec.exe",
                UseShellExecute = true,
                Verb = "runas"
            };

            mDbStartInfo.ArgumentList.Add("/i");
            mDbStartInfo.ArgumentList.Add(installerPath);
            mDbStartInfo.ArgumentList.Add("/qn");
            mDbStartInfo.ArgumentList.Add("/norestart");
            mDbStartInfo.ArgumentList.Add("ADDLOCAL=ServerNoService");
            mDbStartInfo.ArgumentList.Add("SHOULD_INSTALL_COMPASS=0");

            using var mDbProcess = Process.Start(mDbStartInfo)
                ?? throw new InvalidOperationException("Unable to start the MongoDB installer.");
            await mDbProcess.WaitForExitAsync();

            // 0 = success; 3010 = success with a restart recommended.
            if (mDbProcess.ExitCode is not 0 and not 3010)
            {
                throw new InvalidOperationException(
                    $"MongoDB installation failed with Windows Installer exit code {mDbProcess.ExitCode}.");
            }

            // End of Program installation
            ///////////////////////////////////////////////////////////////////////////////////////
            // Create the MongoDb Service using Mongod
            var mongodPath = Path.Combine(MongoDbInstallerInfo.InstallPath, "bin", "mongod.exe");
            mongodPathForRollback = mongodPath;
            if (!File.Exists(mongodPath))
            {
                throw new FileNotFoundException(
                    "MongoDB was installed, but mongod.exe was not found.", mongodPath);
            }

            var dataPath = ResolveAppDataPath(appData, Resources.MongoDbDataPath);
            var logsPath = ResolveAppDataPath(appData, Resources.MongoDbLogsPath);
            var logFilePath = Path.Combine(logsPath, "mongod.log");
            Directory.CreateDirectory(dataPath);
            Directory.CreateDirectory(logsPath);

            var serviceInfo = new ProcessStartInfo()
            {
                WorkingDirectory = Path.GetDirectoryName(mongodPath)!,
                FileName = mongodPath,
                UseShellExecute = true,
                Verb = "runas"
            };

            serviceInfo.ArgumentList.Add("--install");
            serviceInfo.ArgumentList.Add("--serviceName");
            serviceInfo.ArgumentList.Add(MongoDbDefaults.ServiceName);
            serviceInfo.ArgumentList.Add("--serviceDisplayName");
            serviceInfo.ArgumentList.Add(MongoDbDefaults.ServiceDisplayName);
            serviceInfo.ArgumentList.Add("--dbpath");
            serviceInfo.ArgumentList.Add(dataPath);
            serviceInfo.ArgumentList.Add("--logpath");
            serviceInfo.ArgumentList.Add(logFilePath);
            serviceInfo.ArgumentList.Add("--bind_ip");
            serviceInfo.ArgumentList.Add(MongoDbDefaults.Host);
            serviceInfo.ArgumentList.Add("--port");
            serviceInfo.ArgumentList.Add(MongoDbDefaults.Port.ToString());

            using var serviceProcess = Process.Start(serviceInfo)
                ?? throw new InvalidOperationException("Unable to install the MongoDB Windows service.");
            await serviceProcess.WaitForExitAsync();
            if (serviceProcess.ExitCode != 0)
            {
                throw new InvalidOperationException(
                    $"MongoDB service installation failed with exit code {serviceProcess.ExitCode}.");
            }
            serviceInstalled = true;

            // End of creating service
            ///////////////////////////////////////////////////////////////////////////////////////
            // Start the Service and wait for it to run
            var serviceStarted = false;
            using (var serviceController = new ServiceController(MongoDbDefaults.ServiceName, Environment.MachineName))
            {
                serviceController.Refresh();

                if (serviceController.Status == ServiceControllerStatus.StopPending)
                {
                    await Task.Run(() => serviceController.WaitForStatus(
                        ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30)));
                }

                if (serviceController.Status == ServiceControllerStatus.Paused)
                    serviceController.Continue();
                else if (serviceController.Status == ServiceControllerStatus.Stopped)
                    serviceController.Start();

                if (serviceController.Status != ServiceControllerStatus.Running)
                {
                    await Task.Run(() => serviceController.WaitForStatus(
                        ServiceControllerStatus.Running, TimeSpan.FromSeconds(30)));
                }

                serviceController.Refresh();
                serviceStarted = serviceController.Status == ServiceControllerStatus.Running;
            }

            if (!serviceStarted)
                throw new InvalidOperationException("The MongoDB service did not reach the Running state.");

            // End of starting service
            ///////////////////////////////////////////////////////////////////////////////////////
            // Bootstrap a unique, least-privileged application user while the
            // new server is reachable only from this computer.
            const string applicationUser = MongoDbDefaults.ApplicationUser;
            var applicationPassword = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var bootstrapUrl = new MongoUrlBuilder
            {
                Server = new MongoServerAddress(MongoDbDefaults.Host, MongoDbDefaults.Port),
                DirectConnection = true,
                ServerSelectionTimeout = MongoDbDefaults.ConnectionTimeout
            }.ToMongoUrl();

            var applicationDatabase = new MongoClient(bootstrapUrl)
                .GetDatabase(MongoDbDefaults.DatabaseName);
            var createUser = new BsonDocument
            {
                { "createUser", applicationUser },
                { "pwd", applicationPassword },
                { "roles", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "role", "readWrite" },
                            { "db", MongoDbDefaults.DatabaseName }
                        }
                    }
                }
            };
            applicationDatabase.RunCommand<BsonDocument>(createUser);

            // Restart the service with authorization enforced.
            await StopServiceAsync();
            await RunMongodServiceCommandAsync(
                mongodPath, "remove", "--remove", "--serviceName", MongoDbDefaults.ServiceName);
            serviceInstalled = false;

            serviceInfo.ArgumentList.Add("--auth");
            using (var securedServiceProcess = Process.Start(serviceInfo)
                ?? throw new InvalidOperationException("Unable to secure the MongoDB Windows service."))
            {
                await securedServiceProcess.WaitForExitAsync();
                if (securedServiceProcess.ExitCode != 0)
                    throw new InvalidOperationException(
                        $"Securing the MongoDB service failed with exit code {securedServiceProcess.ExitCode}.");
            }
            serviceInstalled = true;

            await StartServiceAsync();

            var connectionBuilder = new MongoUrlBuilder
            {
                Server = new MongoServerAddress(MongoDbDefaults.Host, MongoDbDefaults.Port),
                DatabaseName = MongoDbDefaults.DatabaseName,
                Username = applicationUser,
                Password = applicationPassword,
                AuthenticationSource = MongoDbDefaults.DatabaseName,
                AuthenticationMechanism = MongoDbDefaults.ScramSha256,
                DirectConnection = true,
                ServerSelectionTimeout = MongoDbDefaults.ConnectionTimeout
            };
            var connString = connectionBuilder.ToString();
            var securedDatabase = new MongoClient(connString).GetDatabase(MongoDbDefaults.DatabaseName);
            securedDatabase
                .RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            await MongoDbInitializer.InitializeAsync(securedDatabase);

            // Load the default appSettings.json
            var rootObject = AppSettingsJson.GetSettings();
            if (serviceStarted && rootObject != null)
            {
                // Rewrite the program Settings in appSettings.json
                var dbConn = rootObject?.Settings?.DbConnection;
                if (dbConn != null)
                {
                    dbConn.Connection = connString;
                    dbConn.Database = MongoDbDefaults.DatabaseName;
                    dbConn.Host = MongoDbDefaults.Host;
                    dbConn.Port = MongoDbDefaults.Port.ToString();
                    dbConn.User = applicationUser;
                    dbConn.Pass = Classes.Common.WindowsDataProtection.Protect(applicationPassword);
                    dbConn.PasswordProtected = true;
                    dbConn.AuthDatabase = MongoDbDefaults.DatabaseName;
                    dbConn.AuthMechanism = MongoDbDefaults.ScramSha256;
                    // Never persist a connection string containing credentials.
                    connectionBuilder.Username = null;
                    connectionBuilder.Password = null;
                    dbConn.Connection = connectionBuilder.ToString();

                    // Copy the new credentials back to the root object
                    if (rootObject?.Settings != null)
                    {
                        rootObject.Settings.DbConnection = dbConn;
                        await AppSettingsJson.WriteSettingsAsync(rootObject);
                    }
                    
                }

            }

            if (rootObject?.Settings != null)
            {
                return rootObject.Settings;
            } else
            {
                return new Settings();
            }
            }
            catch (Exception setupException)
            {
                Exception? rollbackException = null;
                if (serviceInstalled && !string.IsNullOrWhiteSpace(mongodPathForRollback))
                {
                    try
                    {
                        await RollbackServiceAsync(mongodPathForRollback);
                    }
                    catch (Exception ex)
                    {
                        rollbackException = ex;
                    }
                }

                if (rollbackException != null)
                {
                    throw new AggregateException(
                        "MongoDB setup failed and the incomplete service could not be fully removed.",
                        setupException, rollbackException);
                }

                throw new InvalidOperationException(
                    "MongoDB setup failed. The incomplete Windows service was removed so setup can be retried.",
                    setupException);
            }
        }

        private static async Task RollbackServiceAsync(string mongodPath)
        {
            try
            {
                await StopServiceAsync();
            }
            catch (InvalidOperationException)
            {
                // The service may not have completed registration.
            }

            await RunMongodServiceCommandAsync(
                mongodPath, "remove", "--remove", "--serviceName", MongoDbDefaults.ServiceName);
        }

        private static string ResolveAppDataPath(string appData, string configuredPath)
        {
            return Path.Combine(appData, configuredPath.TrimStart('\\', '/'));
        }

        private static async Task StopServiceAsync()
        {
            using var controller = new ServiceController(MongoDbDefaults.ServiceName, Environment.MachineName);
            controller.Refresh();
            if (controller.Status == ServiceControllerStatus.Stopped)
                return;

            if (controller.Status == ServiceControllerStatus.StopPending)
            {
                await Task.Run(() => controller.WaitForStatus(
                    ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30)));
                return;
            }

            controller.Stop();
            await Task.Run(() => controller.WaitForStatus(
                ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30)));
        }

        private static async Task StartServiceAsync()
        {
            using var controller = new ServiceController(MongoDbDefaults.ServiceName, Environment.MachineName);
            controller.Refresh();
            if (controller.Status == ServiceControllerStatus.Running)
                return;

            controller.Start();
            await Task.Run(() => controller.WaitForStatus(
                ServiceControllerStatus.Running, TimeSpan.FromSeconds(30)));
        }

        private static async Task RunMongodServiceCommandAsync(
            string mongodPath, string operation, params string[] arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = Path.GetDirectoryName(mongodPath)!,
                FileName = mongodPath,
                UseShellExecute = true,
                Verb = "runas"
            };
            foreach (var argument in arguments)
                startInfo.ArgumentList.Add(argument);

            using var process = Process.Start(startInfo)
                ?? throw new InvalidOperationException($"Unable to {operation} the MongoDB service.");
            await process.WaitForExitAsync();
            if (process.ExitCode != 0)
                throw new InvalidOperationException(
                    $"MongoDB service {operation} failed with exit code {process.ExitCode}.");
        }
    }
}
