using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Models.MongoDb;
using SimpleBol.Setup;

namespace SimpleBol.MongoDb.Seeds
{
    public class SeedCountries
    {
        /// <summary>
        /// Generate Website Countries for MongoDb
        /// </summary>
        /// <param name="_database"></param>
        /// <returns></returns>
        public static async Task<bool> SeedAsync(IMongoDatabase _database)
        {
            var countries = _database.GetCollection<COUNTRIES>(MongoDbCollectionNames.Countries);
            var seedCountries = new[]
            {
                new COUNTRIES
                {
                    CountryId = ObjectId.GenerateNewId().ToString(),
                    LongName = "Canada",
                    ShortName = "CA",
                    Enabled = true
                },
                new COUNTRIES
                {
                    CountryId = ObjectId.GenerateNewId().ToString(),
                    LongName = "Mexico",
                    ShortName = "MX",
                    Enabled = true
                },
                new COUNTRIES
                {
                    CountryId = ObjectId.GenerateNewId().ToString(),
                    LongName = "United States",
                    ShortName = "US",
                    Enabled = true
                }
            };

            var writes = seedCountries.Select(country =>
                new UpdateOneModel<COUNTRIES>(
                    Builders<COUNTRIES>.Filter.Eq(item => item.ShortName, country.ShortName),
                    Builders<COUNTRIES>.Update
                        .SetOnInsert(item => item.CountryId, country.CountryId)
                        .SetOnInsert(item => item.Enabled, country.Enabled)
                        .Set(item => item.LongName, country.LongName)
                        .Set(item => item.ShortName, country.ShortName))
                { IsUpsert = true });

            await countries.BulkWriteAsync(writes, new BulkWriteOptions { IsOrdered = false });
            return true;
        }
    }
}
