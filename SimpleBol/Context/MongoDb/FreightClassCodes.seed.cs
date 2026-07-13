using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Models.MongoDb;
using SimpleBol.Setup;
using System.Threading.Tasks;

namespace SimpleBol.Context.MongoDb
{
    public class SeedFreightClassCodes
    {
        public static async Task<bool> SeedAsync(IMongoDatabase _database)
        {
            var classCodes = _database.GetCollection<FREIGHTCLASSCODES>(MongoDbCollectionNames.FreightClassCodes);

            List<FREIGHTCLASSCODES> classCodeObjects = new List<FREIGHTCLASSCODES>()
                {   
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "50",
                        Description = "Durable freight that fits on a standard 4' × 4' pallet",
                        CodeNumber = 50,
                        WeightType = "LBS",
                        MinWeightPerfoot = 50,
                        MaxWeightPerFoot = null,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "55",
                        Description = "Bricks, Cement, Fresh Fruits, etc.",
                        CodeNumber = 55,
                        WeightType = "LBS",
                        MinWeightPerfoot = 35,
                        MaxWeightPerFoot = 50,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "60",
                        Description = "Dense freight such as machinery parts and steel products",
                        CodeNumber = 60,
                        WeightType = "LBS",
                        MinWeightPerfoot = 30,
                        MaxWeightPerFoot = 35,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "65",
                        Description = "Car accessories and parts, boxed books, bottled drinks",
                        CodeNumber = 65,
                        WeightType = "LBS",
                        MinWeightPerfoot = 22.5,
                        MaxWeightPerFoot = 30,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "70",
                        Description = "Car accessories and parts, auto engines, food items",
                        CodeNumber = 70,
                        WeightType = "LBS",
                        MinWeightPerfoot = 15,
                        MaxWeightPerFoot = 22.5,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "77.5",
                        Description = "Tires, bathroom fixtures",
                        CodeNumber = 77.5,
                        WeightType = "LBS",
                        MinWeightPerfoot = 13.5,
                        MaxWeightPerFoot = 15,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "85",
                        Description = "Crated machinery, cast iron stoves",
                        CodeNumber = 85,
                        WeightType = "LBS",
                        MinWeightPerfoot = 12,
                        MaxWeightPerFoot = 13.5,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "92.5",
                        Description = "Computers, monitors, refrigerators",
                        CodeNumber = 92.5,
                        WeightType = "LBS",
                        MinWeightPerfoot = 10.5,
                        MaxWeightPerFoot = 12,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "100",
                        Description = "Car covers, canvas, boat covers, wine cases, caskets",
                        CodeNumber = 100,
                        WeightType = "LBS",
                        MinWeightPerfoot = 9,
                        MaxWeightPerFoot = 10.5,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "110",
                        Description = "Cabinets, framed art, table saws",
                        CodeNumber = 110,
                        WeightType = "LBS",
                        MinWeightPerfoot = 8,
                        MaxWeightPerFoot = 9,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "125",
                        Description = "Small home appliances",
                        CodeNumber = 125,
                        WeightType = "LBS",
                        MinWeightPerfoot = 7,
                        MaxWeightPerFoot = 8,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "150",
                        Description = "Auto sheet metal, bookcases",
                        CodeNumber = 150,
                        WeightType = "LBS",
                        MinWeightPerfoot = 6,
                        MaxWeightPerFoot = 7,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "175",
                        Description = "Clothing, couches, stuffed furniture",
                        CodeNumber = 175,
                        WeightType = "LBS",
                        MinWeightPerfoot = 5,
                        MaxWeightPerFoot = 6,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "200",
                        Description = "Sheet metal parts, aluminum tables, packaged mattresses, aircraft parts",
                        CodeNumber = 200,
                        WeightType = "LBS",
                        MinWeightPerfoot = 4,
                        MaxWeightPerFoot = 5,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "250",
                        Description = "Sheet metal parts, aluminum tables, packaged mattresses, aircraft parts",
                        CodeNumber = 250,
                        WeightType = "LBS",
                        MinWeightPerfoot = 3,
                        MaxWeightPerFoot = 4,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "300",
                        Description = "Mattresses and box springs, plasma TVs, bamboo furniture",
                        CodeNumber = 300,
                        WeightType = "LBS",
                        MinWeightPerfoot = 2,
                        MaxWeightPerFoot = 3,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "400",
                        Description = "Deer antlers",
                        CodeNumber = 400,
                        WeightType = "LBS",
                        MinWeightPerfoot = 1,
                        MaxWeightPerFoot = 2,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new FREIGHTCLASSCODES
                    {
                        FreightClassCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "500",
                        Description = "Gold dust, ping pong balls",
                        CodeNumber = 500,
                        WeightType = "LBS",
                        MinWeightPerfoot = 0,
                        MaxWeightPerFoot = 1,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    }
                };

            var writes = classCodeObjects.Select(classCode =>
                new UpdateOneModel<FREIGHTCLASSCODES>(
                    Builders<FREIGHTCLASSCODES>.Filter.Eq(item => item.CodeNumber, classCode.CodeNumber),
                    Builders<FREIGHTCLASSCODES>.Update
                        .SetOnInsert(item => item.FreightClassCodeId, classCode.FreightClassCodeId)
                        .SetOnInsert(item => item.Enabled, classCode.Enabled)
                        .SetOnInsert(item => item.Comment, classCode.Comment)
                        .SetOnInsert(item => item.IsDeleted, classCode.IsDeleted)
                        .SetOnInsert(item => item.CreatedOnUtc, classCode.CreatedOnUtc)
                        .Set(item => item.Name, classCode.Name)
                        .Set(item => item.Description, classCode.Description)
                        .Set(item => item.WeightType, classCode.WeightType)
                        .Set(item => item.MinWeightPerfoot, classCode.MinWeightPerfoot)
                        .Set(item => item.MaxWeightPerFoot, classCode.MaxWeightPerFoot)
                        .Set(item => item.UpdatedOnUtc, DateTime.UtcNow))
                { IsUpsert = true });

            await classCodes.BulkWriteAsync(writes, new BulkWriteOptions { IsOrdered = false });

            return true;

        }

    }
}
