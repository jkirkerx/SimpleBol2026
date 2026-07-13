using System.Windows.Forms;

namespace SimpleBol.Models
{
    public class ProductInfo
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
    }

    public class DbConnection
    {
        public string? ProfileId { get; set; }
        public string? ProfileName { get; set; }
        public string? Connection { get; set; }
        public string? Database { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? User { get; set; }
        public string? Pass { get; set; }
        public bool PasswordProtected { get; set; }
        public string? AuthMechanism { get; set; }
        public string? AuthDatabase {  get; set; }        
    }

    public class AwsConnection
    {

    }    

    public class SoftwareUpdate
    {
        public string? UpdateFile { get; set; }
        public string? ReleaseFile { get; set; }
        public bool? AutoUpdate { get; set; }
    }

    public class ScreenParameters
    {
        public FormWindowState WindowState { get; set; }
        public System.Drawing.Rectangle WindowPosition { get; set; }        
    }

    public class SendGrid
    {        
        public string? ApiKey { get; set; }
        public string? SentFromEmailAddress { get; set; }
        public string? SentFromName { get; set; }
        public byte[]? Salt { get; set; }
    }

    public class Gmail
    {        
        public string? ServiceId { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? ApiKey { get; set; }
        public string? SentFromEmailAddress { get; set; }
        public string? SentFromName {  get; set; }
        public byte[]? Salt { get; set; }
    }

    public class Outlook365
    {        
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }        
        public string? SentFromEmailAddress { get; set; }
        public string? SentFromName {  get; set; }
        public string? UserAccount { get; set; }
        public bool SaveToSentItems { get; set; }
        public byte[]? Salt { get; set; }
    }

    public class CompanyInfo
    {
        public string? CompanyName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? RegionId { get; set; }
        public string? RegionName { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Phone { get; set; }
    }

    public class Settings
    {
        public string? Version { get; set; }
        // DbConnection remains as a compatibility pointer to the active profile.
        public DbConnection? DbConnection { get; set; }
        public List<DbConnection>? DbConnections { get; set; }
        public string? ActiveDbConnectionId { get; set; }
        public SoftwareUpdate? SoftwareUpdate { get; set; }
        public ScreenParameters? ScreenParameters { get; set; }

        // Newtonsoft.Json still reads this legacy property during migration, but new
        // settings files store each connection only once in DbConnections.
        public bool ShouldSerializeDbConnection() => false;
    }

    public class SmtpApiSettings
    {
        public string? DefaultId { get; set; }
        public string? SecureToken { get; set; }
        public SendGrid? SendGrid { get; set; }
        public Gmail? Gmail { get; set; }
        public Outlook365? Outlook365 { get; set; }
        public CompanyInfo? CompanyInfo { get; set; }
    }

    public class RootObject
    {
        public ProductInfo? ProductInfo { get; set; }
        public Settings? Settings { get; set; }
        public SmtpApiSettings? SmtpApiSettings { get; set; }
    }
}
