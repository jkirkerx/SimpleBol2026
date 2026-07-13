using MongoDB.Driver;
using SimpleBol.Models.MongoDb;
using SimpleBol.NewtonSoft;
using SimpleBol.Setup;

namespace SimpleBol.Context.MongoDb
{
    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }
        
        public MongoDbContext(IMongoClient client)
        {
            
            // Grab the MongoDB Connection and Database
            var rootObject = AppSettingsJson.GetSettings();
            var dbConnection = rootObject?.Settings?.DbConnection;
            if (string.IsNullOrWhiteSpace(dbConnection?.Connection))
                throw new InvalidOperationException("The MongoDB connection string is not configured.");

            if (string.IsNullOrWhiteSpace(dbConnection.Database))
                throw new InvalidOperationException("The MongoDB database name is not configured.");

            Database = client.GetDatabase(dbConnection.Database);

        }        

        // Simple Bol Database Models
        public IMongoCollection<ACCOUNTS> Accounts => Database.GetCollection<ACCOUNTS>(MongoDbCollectionNames.Accounts);

        public IMongoCollection<SHIPPER> Shippers => Database.GetCollection<SHIPPER>(MongoDbCollectionNames.Shippers);

        public IMongoCollection<CONTACTS> Contacts => Database.GetCollection<CONTACTS>(MongoDbCollectionNames.Contacts);

        public IMongoCollection<PALLETS> Pallets => Database.GetCollection<PALLETS>(MongoDbCollectionNames.Pallets);

        public IMongoCollection<CUSTOMERS> Customers => Database.GetCollection<CUSTOMERS>(MongoDbCollectionNames.Customers);

        public IMongoCollection<VENDORS> Vendors => Database.GetCollection<VENDORS>(MongoDbCollectionNames.Vendors);

        public IMongoCollection<BILLOFLADINGS> BillOfLadings => Database.GetCollection<BILLOFLADINGS>(MongoDbCollectionNames.BillOfLadings);

        public IMongoCollection<COUNTRIES> Countries => Database.GetCollection<COUNTRIES>(MongoDbCollectionNames.Countries);

        public IMongoCollection<REGIONS> Regions => Database.GetCollection<REGIONS>(MongoDbCollectionNames.Regions);

        public IMongoCollection<NMFCCODES> NmfcCodes => Database.GetCollection<NMFCCODES>(MongoDbCollectionNames.NmfcCodes);
        
        public IMongoCollection<FREIGHTCLASSCODES> ClassCodes => Database.GetCollection<FREIGHTCLASSCODES>(MongoDbCollectionNames.FreightClassCodes);

        public IMongoCollection<REGIONS_ABBR> RegionsAbbr => Database.GetCollection<REGIONS_ABBR>(MongoDbCollectionNames.RegionsAbbr);

        public IMongoCollection<SHIPPINGLOCATIONS> ShippingAddresses => Database.GetCollection<SHIPPINGLOCATIONS>(MongoDbCollectionNames.ShippingLocations);

        public IMongoCollection<BILLTOACCOUNTS> BillToAccounts => Database.GetCollection<BILLTOACCOUNTS>(MongoDbCollectionNames.BillToAccounts);

        public IMongoCollection<SMTPAPISETTINGS> SmtpCredentials => Database.GetCollection<SMTPAPISETTINGS>(MongoDbCollectionNames.SmtpCredentials);

        public IMongoCollection<BILLINGDISPUTES> BillingDisputes => Database.GetCollection<BILLINGDISPUTES>(MongoDbCollectionNames.BillingDisputes);

    }
}
