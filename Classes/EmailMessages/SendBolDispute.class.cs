using MimeKit;
using SimpleBol.Models;
using SimpleBol.Models.MongoDb;
using SimpleBol.Models.Smtp;
using SimpleBol.NewtonSoft;
using System.Linq;
using System.Text;
using System.IO;

namespace SimpleBol.Classes.EmailMessages
{
    internal class BolDispute
    {
        public async Task<bool> SendBolBillingIssuesToShipper(SHIPPER shipper, List<BILLOFLADINGS> bols)
        {
            bool success = true;

            await Task.Delay(100);

            return success;

        }

        private string ReadBolBillingIssueToShipperTemplate(SHIPPER shipper, List<BILLOFLADINGS> bols)
        {
            var htmlTemplate = string.Empty;
            string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "EmailTemplates", "bolBillingIssueToShipper.html");
            var bytes = File.ReadAllBytes(appPath);
            var enc = new UTF8Encoding(true);
            var preamble = enc.GetPreamble();
            htmlTemplate = bytes.Length >= preamble.Length && !preamble.Where((p, i) => p != bytes[i]).Any()
                ? enc.GetString(bytes.Skip(preamble.Length).ToArray())
                : enc.GetString(bytes);



            htmlTemplate = htmlTemplate.Replace("<% smtp.Time.Stamp %>", DateTime.UtcNow + " UTC");

            htmlTemplate = htmlTemplate.Replace("<% smtp.TimeStamp_GMT %>", DateTime.UtcNow + " GMT");
            htmlTemplate = htmlTemplate.Replace("<% smtp.TimeStamp_UTC %>", DateTime.UtcNow + " UTC");
            return htmlTemplate.ToString();
        }

    }
}
