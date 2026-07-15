using System.IO;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Driver;
using SimpleBol.Classes.Common;
using SimpleBol.Classes.Errors;
using SimpleBol.Models;
using SimpleBol.Properties;
using SimpleBol.Setup;
using DbConnection = SimpleBol.Models.DbConnection;

namespace SimpleBol.NewtonSoft
{
    public class AppSettingsJson
    {
        public static RootObject? GetSettings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.AppSettingsJsonPath;
            if (!File.Exists(appPath))
            {
                CreateAppSettings();
            }
            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(appPath));
            NormalizeMongoDbConnectionProfiles(rootObject, out _);
            NormalizeEmailConnectionProfiles(rootObject, out _);
            return rootObject;
            
        }

        public static void WriteSettings(RootObject settings)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.AppSettingsJsonPath;
            if (settings.ProductInfo != null)
            {
                settings.ProductInfo.Name = Application.ProductName;
                settings.ProductInfo.Version = AppInfo.Version;
            }

            if (settings.Settings != null)
            {
                settings.Settings.Version = AppInfo.Version;
            }

            NormalizeMongoDbConnectionProfiles(settings, out _);
            NormalizeEmailConnectionProfiles(settings, out _);

            try
            {
                var oJs = JsonConvert.SerializeObject(settings, Formatting.Indented);
                var bytes = new UTF8Encoding(true).GetBytes(oJs);
                var test = CloseIfFileInUse(appPath);
                using (var jsonStream = new FileStream(appPath, FileMode.Create, FileAccess.Write))
                {
                    jsonStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "Write appSettings.json");
            }

        }

        public static async Task WriteSettingsAsync(RootObject settings)
        {
            await Task.Run(() =>
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var appPath = appData + Properties.Resources.AppSettingsJsonPath;
                if (settings.ProductInfo != null)
                {
                    settings.ProductInfo.Name = Application.ProductName;
                    settings.ProductInfo.Version = AppInfo.Version;
                }

                if (settings.Settings != null)
                {
                    settings.Settings.Version = AppInfo.Version;
                }

                NormalizeMongoDbConnectionProfiles(settings, out _);
                NormalizeEmailConnectionProfiles(settings, out _);

                try
                {
                    var oJs = JsonConvert.SerializeObject(settings, Formatting.Indented);
                    var bytes = new UTF8Encoding(true).GetBytes(oJs);
                    var test = CloseIfFileInUse(appPath);
                    using (var jsonStream = new FileStream(appPath, FileMode.Create, FileAccess.Write))
                    {
                        jsonStream.Write(bytes, 0, bytes.Length);
                    }

                }
                catch (Exception ex)
                {
                    ErrorLogging.NLogException(ex, "Updating the appSettings.json crashed, must be that file permission error");
                }
            });
        }

        public static bool CreateAppSettings()
        {
            var pValue = false;
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.AppSettingsJsonPath;

            if (!File.Exists(appPath))
            {
                var defaultDbConnection = new DbConnection()
                {
                    ProfileId = Guid.NewGuid().ToString("N"),
                    ProfileName = "Default",
                    Connection = $"mongodb://{MongoDbDefaults.Host}:{MongoDbDefaults.Port}/{MongoDbDefaults.DatabaseName}",
                    Database = MongoDbDefaults.DatabaseName,
                    Host = MongoDbDefaults.Host,
                    Port = MongoDbDefaults.Port.ToString(),
                    User = "",
                    Pass = "",
                    PasswordProtected = false,
                    AuthDatabase = "",
                    AuthMechanism = MongoDbDefaults.NoAuthentication
                };

                var rootObject = new RootObject()
                {
                    ProductInfo = new ProductInfo()
                    {
                        Name = Application.ProductName,
                        Version = AppInfo.Version
                    },
                    Settings = new Models.Settings()
                    {
                        Version = AppInfo.Version,
                        PackageMeasurementCode = "English",
                        DbConnection = defaultDbConnection,
                        DbConnections = new List<DbConnection>() { defaultDbConnection },
                        ActiveDbConnectionId = defaultDbConnection.ProfileId,
                        SoftwareUpdate = new SoftwareUpdate()
                        {
                            UpdateFile = "",
                            ReleaseFile = "",
                            AutoUpdate = false
                        },
                        ScreenParameters = new ScreenParameters()
                        {
                            WindowPosition = new Rectangle()
                            {
                                Height = 600,
                                Width = 800,
                                Location = new Point() { X = 50, Y = 50 },
                                Size = new Size() { Width = 800, Height = 600 },
                                X = 50,
                                Y = 50
                            },
                            WindowState = FormWindowState.Normal
                        },
                    },
                    SmtpApiSettings = new SmtpApiSettings()
                    {
                        ProfileId = defaultDbConnection.ProfileId,
                        ProfileName = defaultDbConnection.ProfileName,
                        DefaultId = "DISABLED",
                        SendGrid = new Models.SendGrid()
                        {
                            ApiKey = "",
                            SentFromEmailAddress = "",
                            Salt = null
                        },
                        Gmail = new Gmail()
                        {
                            ClientId = "",
                            ServiceId = "",
                            ClientSecret = "",
                            ApiKey = "",
                            SentFromEmailAddress = "",
                            Salt = null                
                        },
                        Outlook365 = new Outlook365()
                        {
                            ClientId = "",
                            ClientSecret = "",
                            TenantId = "",
                            SentFromEmailAddress = "",
                            SaveToSentItems = true,
                            Salt = null
                        },
                        CompanyInfo = new CompanyInfo()
                        {
                            CompanyName = "",
                            Address1 = "",
                            Address2 = "",
                            City = "",
                            RegionId = "",
                            RegionName = "",
                            CountryId = "",
                            CountryName = "",
                            PostalCode = "",
                            Phone = "",
                        }
                    }
                };

                rootObject.EmailConnections = new List<SmtpApiSettings>() { rootObject.SmtpApiSettings };
                rootObject.ActiveEmailConnectionId = rootObject.SmtpApiSettings.ProfileId;

                try
                {

                    var oJs = JsonConvert.SerializeObject(rootObject);
                    File.WriteAllText(appPath, oJs);
                    pValue = true;
                }
                catch (Exception ex)
                {
                    ErrorLogging.NLogException(ex, "CreateAppSettings Failed");
                }
            }

            return pValue;
        }

        public static void ProtectMongoDbCredentials()
        {
            var rootObject = GetSettings();
            var dbConnections = rootObject?.Settings?.DbConnections;
            if (rootObject == null || dbConnections == null)
                return;

            var changed = false;
            foreach (var dbConnection in dbConnections.Where(connection => !connection.PasswordProtected))
            {
                var password = dbConnection.Pass ?? string.Empty;
                MongoUrlBuilder? builder = null;
                if (!string.IsNullOrWhiteSpace(dbConnection.Connection))
                {
                    builder = new MongoUrlBuilder(dbConnection.Connection);
                    if (string.IsNullOrEmpty(password))
                        password = builder.Password ?? string.Empty;
                }

                if (string.IsNullOrEmpty(password))
                    continue;

                dbConnection.Pass = WindowsDataProtection.Protect(password);
                dbConnection.PasswordProtected = true;
                changed = true;

                if (builder != null)
                {
                    builder.Username = null;
                    builder.Password = null;
                    dbConnection.Connection = builder.ToString();
                }
            }

            if (changed)
                WriteSettings(rootObject);
        }

        public static void MigrateMongoDbConnectionProfiles()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.AppSettingsJsonPath;
            if (!File.Exists(appPath))
                return;

            var rootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(appPath));
            NormalizeMongoDbConnectionProfiles(rootObject, out var changed);
            if (rootObject != null && changed)
                WriteSettings(rootObject);
        }

        private static void NormalizeMongoDbConnectionProfiles(
            RootObject? rootObject,
            out bool changed)
        {
            changed = false;
            var settings = rootObject?.Settings;
            if (settings == null)
                return;

            if (settings.DbConnections == null)
            {
                settings.DbConnections = new List<DbConnection>();
                changed = true;
            }

            if (settings.DbConnections.Count == 0 && settings.DbConnection != null)
            {
                EnsureProfileIdentity(settings.DbConnection, "Default", ref changed);
                settings.DbConnections.Add(settings.DbConnection);
                changed = true;
            }

            for (var index = 0; index < settings.DbConnections.Count; index++)
            {
                EnsureProfileIdentity(
                    settings.DbConnections[index],
                    index == 0 ? "Default" : $"Connection {index + 1}",
                    ref changed);
            }

            if (settings.DbConnections.Count == 0)
                return;

            var activeConnection = settings.DbConnections.FirstOrDefault(connection =>
                connection.ProfileId == settings.ActiveDbConnectionId);
            if (activeConnection == null)
            {
                activeConnection = settings.DbConnections[0];
                settings.ActiveDbConnectionId = activeConnection.ProfileId;
                changed = true;
            }

            // Existing application code reads DbConnection. Point it at the selected profile.
            settings.DbConnection = activeConnection;
        }

        private static void EnsureProfileIdentity(
            DbConnection connection,
            string defaultName,
            ref bool changed)
        {
            if (string.IsNullOrWhiteSpace(connection.ProfileId))
            {
                connection.ProfileId = Guid.NewGuid().ToString("N");
                changed = true;
            }

            if (string.IsNullOrWhiteSpace(connection.ProfileName))
            {
                connection.ProfileName = defaultName;
                changed = true;
            }
        }

        public static void NormalizeEmailConnectionProfiles(
            RootObject? rootObject,
            out bool changed)
        {
            changed = false;
            if (rootObject == null)
                return;

            rootObject.EmailConnections ??= new List<SmtpApiSettings>();
            if (rootObject.EmailConnections.Count == 0 && rootObject.SmtpApiSettings != null)
            {
                EnsureEmailProfileIdentity(rootObject.SmtpApiSettings, "Default", ref changed);
                rootObject.EmailConnections.Add(rootObject.SmtpApiSettings);
                changed = true;
            }

            var dbConnections = rootObject.Settings?.DbConnections ?? new List<DbConnection>();
            var activeDbConnection = dbConnections.FirstOrDefault(connection =>
                connection.ProfileId == rootObject.Settings?.ActiveDbConnectionId);

            if (rootObject.EmailConnections.Count == 1 && activeDbConnection?.ProfileId != null &&
                dbConnections.All(connection => connection.ProfileId != rootObject.EmailConnections[0].ProfileId))
            {
                // Migrate the original global email settings onto the active company/database.
                rootObject.EmailConnections[0].ProfileId = activeDbConnection.ProfileId;
                rootObject.EmailConnections[0].ProfileName = activeDbConnection.ProfileName;
                rootObject.ActiveEmailConnectionId = activeDbConnection.ProfileId;
                changed = true;
            }

            foreach (var dbConnection in dbConnections.Where(connection => connection.ProfileId != null))
            {
                var emailProfile = rootObject.EmailConnections.FirstOrDefault(profile =>
                    profile.ProfileId == dbConnection.ProfileId);
                if (emailProfile == null)
                {
                    emailProfile = CreateEmptyEmailConnection(dbConnection.ProfileName ?? "Email Connection");
                    emailProfile.ProfileId = dbConnection.ProfileId;
                    rootObject.EmailConnections.Add(emailProfile);
                    changed = true;
                }
                else if (emailProfile.ProfileName != dbConnection.ProfileName)
                {
                    emailProfile.ProfileName = dbConnection.ProfileName;
                    changed = true;
                }
            }

            if (rootObject.EmailConnections.Count == 0)
            {
                var defaultProfile = CreateEmptyEmailConnection("Default");
                rootObject.EmailConnections.Add(defaultProfile);
                changed = true;
            }

            for (var index = 0; index < rootObject.EmailConnections.Count; index++)
            {
                EnsureEmailProfileIdentity(rootObject.EmailConnections[index],
                    index == 0 ? "Default" : $"Email Connection {index + 1}", ref changed);
            }

            if (dbConnections.Count > 0 && dbConnections.All(connection =>
                    connection.ProfileId != rootObject.ActiveEmailConnectionId))
            {
                rootObject.ActiveEmailConnectionId = activeDbConnection?.ProfileId ?? dbConnections[0].ProfileId;
                changed = true;
            }

            var activeProfile = rootObject.EmailConnections.FirstOrDefault(profile =>
                profile.ProfileId == rootObject.ActiveEmailConnectionId);
            if (activeProfile == null)
            {
                activeProfile = rootObject.EmailConnections[0];
                rootObject.ActiveEmailConnectionId = activeProfile.ProfileId;
                changed = true;
            }

            rootObject.SmtpApiSettings = activeProfile;
        }

        public static SmtpApiSettings CreateEmptyEmailConnection(string profileName) => new()
        {
            ProfileId = Guid.NewGuid().ToString("N"),
            ProfileName = profileName,
            DefaultId = "DISABLED",
            SendGrid = new Models.SendGrid(),
            Gmail = new Gmail(),
            Outlook365 = new Outlook365 { TenantId = "common", SaveToSentItems = true },
            CompanyInfo = new CompanyInfo()
        };

        private static void EnsureEmailProfileIdentity(
            SmtpApiSettings profile,
            string defaultName,
            ref bool changed)
        {
            if (string.IsNullOrWhiteSpace(profile.ProfileId))
            {
                profile.ProfileId = Guid.NewGuid().ToString("N");
                changed = true;
            }

            if (string.IsNullOrWhiteSpace(profile.ProfileName))
            {
                profile.ProfileName = defaultName;
                changed = true;
            }
        }

        public static bool CheckMailSettings()
        {
            var rootObject = AppSettingsJson.GetSettings();
            if (rootObject?.SmtpApiSettings != null)
            {
                if (rootObject.SmtpApiSettings.DefaultId != null)
                {
                    if (rootObject.SmtpApiSettings.SecureToken == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CloseIfFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("'path' cannot be null or empty.", "path");

            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                    Application.DoEvents();
                }
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
                return true;
            }

            return false;
        }

        public static void BackupSettings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Resources.AppSettingsJsonPath;
            var destPath = appData + Resources.AppSettingsJsonPath_BU;
            if ((File.Exists(appPath)))
                File.Copy(appPath, destPath, true);
        }

        public static SmtpApiSettings? GetSmtpApiSettings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.AppSettingsJsonPath;
            
            var oRootObject = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(appPath));
            if (oRootObject != null)
            {
                return oRootObject.SmtpApiSettings;
            }

            return null;
            
        }
    }
}
