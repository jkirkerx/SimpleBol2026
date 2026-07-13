using System.IO;
using System.Text;
using Newtonsoft.Json;
using SimpleBol.Classes.Errors;
using SimpleBol.Models;
using SimpleBol.Properties;
using System.Drawing.Printing;

namespace SimpleBol.Classes.NewtonSoft
{
    public class PrintSettingsJson
    {
        public static PrintObject? GetSettings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.PrintSettingsJsonPath;
            if (!File.Exists(appPath))
            {
                CreatePrintSettings();
            }
            return JsonConvert.DeserializeObject<PrintObject>(File.ReadAllText(appPath));

        }

        public static void WriteSettings(PrintObject settings)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.PrintSettingsJsonPath;
            
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
                ErrorLogging.NLogException(ex, "Write printSettings.json");
            }

        }

        public static async Task WritePrintSettingsAsync(PrintObject settings)
        {
            await Task.Run(() =>
            {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var appPath = appData + Properties.Resources.PrintSettingsJsonPath;
                
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
                    ErrorLogging.NLogException(ex, "Updating the printSettings.json crashed, must be that file permission error");
                }
            });
        }

        public static bool CreatePrintSettings()
        {
            var pValue = false;
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Properties.Resources.PrintSettingsJsonPath;

            if (!File.Exists(appPath))
            {
                var printObject = new PrintObject()
                {
                    PrintSettings = new PrintSettings()
                    {
                        ListViewIconSize = "LARGE",
                        PrintToMethod = PrintMethod.PrintDirect,
                        PrintNumberOfCopies = 3,
                        DefaultPrinter = null,
                        DefaultDocument = "Squares",
                        PrintDuplex = Duplex.Simplex
                    },                   
                    
                };

                try
                {

                    var oJs = JsonConvert.SerializeObject(printObject);
                    File.WriteAllText(appPath, oJs);
                    pValue = true;
                }
                catch (Exception ex)
                {
                    ErrorLogging.NLogException(ex, "CreatePrintSettings Failed");
                }
            }

            return pValue;
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

        public static void BackupPrintSettings()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appPath = appData + Resources.PrintSettingsJsonPath;
            var destPath = appData + Resources.PrintSettingsJsonPath_BU;
            if ((File.Exists(appPath)))
                File.Copy(appPath, destPath, true);
        }        

    }
}
