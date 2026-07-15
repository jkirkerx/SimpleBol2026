namespace SimpleBol.Setup;

internal static class MongoDbDefaults
{
    public const string Host = "127.0.0.1";
    public const int Port = 27017;
    public const string DatabaseName = "SimpleBolDb";
    public const string ServiceName = "MongoDb";
    public const string ServiceDisplayName = "SimpleBol MongoDB";
    public const string ApplicationUser = "simpleBolApp";
    public const string ScramSha256 = "SCRAM-SHA-256";
    public const string ScramSha1 = "SCRAM-SHA-1";
    public const string NoAuthentication = "None";
    public static readonly TimeSpan ConnectionTimeout = TimeSpan.FromSeconds(10);
}

internal static class MongoDbCollectionNames
{
    public const string Accounts = "Accounts";
    public const string Shippers = "Shippers";
    public const string Contacts = "Contacts";
    public const string Pallets = "Pallets";
    public const string Customers = "Customers";
    public const string Vendors = "Vendors";
    public const string BillOfLadings = "BillOfLadings";
    public const string Countries = "Countries";
    public const string Regions = "Regions";
    public const string NmfcCodes = "NmfcFreightCodes";
    public const string FreightClassCodes = "FreightClassCodes";
    public const string RegionsAbbr = "RegionsAbbr";
    public const string ShippingLocations = "ShippingLocations";
    public const string BillToAccounts = "BillToAccounts";
    public const string SmtpCredentials = "SmtpCredentials";
    public const string BillingDisputes = "BillingDisputes";
    public const string EmailTransmissionLogs = "EmailTransmissionLogs";

    public static readonly string[] All =
    {
        Accounts, Shippers, Contacts, Pallets, Customers, Vendors, BillOfLadings,
        Countries, Regions, NmfcCodes, FreightClassCodes, RegionsAbbr,
        ShippingLocations, BillToAccounts, SmtpCredentials, BillingDisputes,
        EmailTransmissionLogs
    };
}
