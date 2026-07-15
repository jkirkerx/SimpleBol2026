using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Context.MongoDb;
using SimpleBol.MongoDb.Seeds;

namespace SimpleBol.Setup;

internal static class MongoDbInitializer
{
    public static async Task InitializeAsync(
        IMongoDatabase database,
        CancellationToken cancellationToken = default)
    {
        var existingCollections = (await database.ListCollectionNamesAsync(
            cancellationToken: cancellationToken)).ToList(cancellationToken);
        foreach (var collectionName in MongoDbCollectionNames.All.Except(
                     existingCollections, StringComparer.Ordinal))
        {
            await database.CreateCollectionAsync(collectionName, cancellationToken: cancellationToken);
        }

        // These seeders are idempotent and only populate empty reference collections.
        await SeedCountries.SeedAsync(database);
        await SeedRegions.SeedAsync(database);
        await SeedFreightClassCodes.SeedAsync(database);
        await SeedNmfcCodes.SeedAsync(database);

        var identifiers = new Dictionary<string, string>
        {
            [MongoDbCollectionNames.Accounts] = "AccountId",
            [MongoDbCollectionNames.Shippers] = "ShipperId",
            [MongoDbCollectionNames.Contacts] = "ContactId",
            [MongoDbCollectionNames.Customers] = "CustomerId",
            [MongoDbCollectionNames.Vendors] = "VendorId",
            [MongoDbCollectionNames.BillOfLadings] = "BolId",
            [MongoDbCollectionNames.Countries] = "CountryId",
            [MongoDbCollectionNames.Regions] = "RegionId",
            [MongoDbCollectionNames.NmfcCodes] = "NmfcCodeId",
            [MongoDbCollectionNames.FreightClassCodes] = "FreightClassCodeId",
            [MongoDbCollectionNames.ShippingLocations] = "LocationId",
            [MongoDbCollectionNames.BillToAccounts] = "BillToAccountId",
            [MongoDbCollectionNames.SmtpCredentials] = "SmtpId",
            [MongoDbCollectionNames.BillingDisputes] = "BillingDisputeId",
            [MongoDbCollectionNames.EmailTransmissionLogs] = "TransmissionId"
        };

        foreach (var (collectionName, fieldName) in identifiers)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            if (await HasIndexOnFieldsAsync(collection, fieldName))
                continue;

            var model = new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending(fieldName),
                new CreateIndexOptions
                {
                    Name = $"ux_{fieldName}",
                    Unique = true,
                    Sparse = true
                });
            await collection.Indexes.CreateOneAsync(model);
        }

        // Login IDs identify people in audit records. Keep them unique regardless
        // of letter casing so "jsmith" and "JSmith" cannot become two users.
        var accounts = database.GetCollection<BsonDocument>(MongoDbCollectionNames.Accounts);
        if (!await HasIndexOnFieldsAsync(accounts, "LoginId"))
        {
            try
            {
                await accounts.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                    Builders<BsonDocument>.IndexKeys.Ascending("LoginId"),
                    new CreateIndexOptions
                    {
                        Name = "ux_LoginId",
                        Unique = true,
                        Sparse = true,
                        Collation = new Collation("en", strength: CollationStrength.Secondary)
                    }));
            }
            catch (MongoCommandException ex) when (
                ex.Code == 11000 || ex.Message.Contains("E11000", StringComparison.Ordinal))
            {
                // Preserve legacy accounts rather than deleting or renaming them
                // during startup. Repository validation prevents new duplicates.
                await accounts.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                    Builders<BsonDocument>.IndexKeys.Ascending("LoginId"),
                    new CreateIndexOptions
                    {
                        Name = "ix_LoginId_LegacyDuplicates",
                        Sparse = true,
                        Collation = new Collation("en", strength: CollationStrength.Secondary)
                    }));
            }
        }

        var freightClasses = database.GetCollection<BsonDocument>(
            MongoDbCollectionNames.FreightClassCodes);
        if (!await HasIndexOnFieldsAsync(freightClasses, "CodeNumber"))
        {
            await freightClasses.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CodeNumber"),
                new CreateIndexOptions
                {
                    Name = "ux_FreightClass_CodeNumber",
                    Unique = true,
                    Sparse = true
                }));
        }

        var nmfcCodes = database.GetCollection<BsonDocument>(MongoDbCollectionNames.NmfcCodes);
        if (!await HasIndexOnFieldsAsync(nmfcCodes, "CodeNumber"))
        {
            await nmfcCodes.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Ascending("CodeNumber"),
                new CreateIndexOptions
                {
                    Name = "ux_Nmfc_CodeNumber",
                    Unique = true,
                    Sparse = true
                }));
        }

        var regions = database.GetCollection<BsonDocument>(MongoDbCollectionNames.Regions);
        if (!await HasIndexOnFieldsAsync(regions, "CountryId", "ShortName"))
        {
            await regions.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys
                    .Ascending("CountryId")
                    .Ascending("ShortName"),
                new CreateIndexOptions { Name = "ix_CountryId_ShortName" }));
        }

        var emailLogs = database.GetCollection<BsonDocument>(
            MongoDbCollectionNames.EmailTransmissionLogs);
        if (!await HasIndexOnFieldsAsync(emailLogs, "RelatedDocumentId", "CreatedUtc"))
        {
            await emailLogs.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys
                    .Ascending("RelatedDocumentId")
                    .Descending("CreatedUtc"),
                new CreateIndexOptions { Name = "ix_EmailLog_Document_CreatedUtc" }));
        }

        if (!await HasIndexOnFieldsAsync(emailLogs, "CreatedUtc"))
        {
            await emailLogs.Indexes.CreateOneAsync(new CreateIndexModel<BsonDocument>(
                Builders<BsonDocument>.IndexKeys.Descending("CreatedUtc"),
                new CreateIndexOptions { Name = "ix_EmailLog_CreatedUtc" }));
        }
    }

    private static async Task<bool> HasIndexOnFieldsAsync(
        IMongoCollection<BsonDocument> collection,
        params string[] fields)
    {
        using var cursor = await collection.Indexes.ListAsync();
        var indexes = await cursor.ToListAsync();
        return indexes.Any(index =>
        {
            if (!index.TryGetValue("key", out var keyValue) || !keyValue.IsBsonDocument)
                return false;

            var keyFields = keyValue.AsBsonDocument.Names.ToArray();
            return keyFields.SequenceEqual(fields, StringComparer.Ordinal);
        });
    }
}
