using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Models.MongoDb;
using SimpleBol.Setup;
using System.Threading.Tasks;

namespace SimpleBol.Context.MongoDb
{
    public class SeedNmfcCodes
    {
        public static async Task<bool> SeedAsync(IMongoDatabase _database)
        {
            var nmfcCodes = _database.GetCollection<NMFCCODES>(MongoDbCollectionNames.NmfcCodes);
            
            List<NMFCCODES> popularNMFCodes = new List<NMFCCODES>()
                {
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "100240",
                        Description = "Furniture, Household Goods",
                        CodeNumber = 100240,
                        FreightClass = 50,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,                        
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "100620",
                        Description = "Appliances, Major, N.O.I.B.N.E.C.",
                        CodeNumber = 100620,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120420",
                        Description = "Automobiles or Trucks, N.O.S.",
                        CodeNumber = 120420,
                        FreightClass = 125,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "125000",
                        Description = "Building Materials, N.O.S.",
                        CodeNumber = 125000,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "150500",
                        Description = "Machinery, Large, N.O.I.B.N.E.C.",
                        CodeNumber = 150500,
                        FreightClass = 85,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "200500",
                        Description = "Meat, Frozen, N.O.I.B.N.E.C.",
                        CodeNumber = 200500,
                        FreightClass = 75,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "40010",
                        Description = "Beer",
                        CodeNumber = 40010,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "40030",
                        Description = "Carbonated Beverages",
                        CodeNumber = 40030,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "42140",
                        Description = "Clothing, Not Combined With Any Other Article",
                        CodeNumber = 42140,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "63000",
                        Description = "Lamps",
                        CodeNumber = 63000,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "80260",
                        Description = "Plywood, Building Products",
                        CodeNumber = 80260,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "84000",
                        Description = "Metal Sheets, N.O.I.B.N.E.C.",
                        CodeNumber = 84000,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "86000",
                        Description = "Pipe, N.O.I.B.N.E.C.",
                        CodeNumber = 86000,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "92350",
                        Description = "Machinery, N.O.S.",
                        CodeNumber = 92350,
                        FreightClass = 80,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "98500",
                        Description = "Printed Matter, N.O.I.B.N.E.C.",
                        CodeNumber = 98500,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120320",
                        Description = "Tools, Hand Operated, N.O.I.B.N.E.C.",
                        CodeNumber = 120320,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120330",
                        Description = "Tools, Power-Driven, N.O.I.B.N.E.C.",
                        CodeNumber = 120330,
                        FreightClass = 85,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120340",
                        Description = "Tools, Pneumatic, N.O.I.B.N.E.C.",
                        CodeNumber = 120340,
                        FreightClass = 75,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120350",
                        Description = "Tools, Hydraulic, N.O.I.B.N.E.C.",
                        CodeNumber = 120350,
                        FreightClass = 80,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120360",
                        Description = "Tools, Gasoline Powered, N.O.I.B.N.E.C.",
                        CodeNumber = 120360,
                        FreightClass = 85,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120370",
                        Description = "Tools, Electric Powered, N.O.I.B.N.E.C.",
                        CodeNumber = 120370,
                        FreightClass = 80,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120380",
                        Description = "Tools, Battery Powered, N.O.I.B.N.E.C.",
                        CodeNumber = 120380,
                        FreightClass = 75,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "120390",
                        Description = "Tools, Cordless, N.O.I.B.N.E.C.",
                        CodeNumber = 120390,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "260010",
                        Description = "Machinery Parts",
                        CodeNumber = 260010,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "303000",
                        Description = "Pharmaceuticals, N.O.S.",
                        CodeNumber = 303000,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "320510",
                        Description = "Computers, Laptops",
                        CodeNumber = 320510,
                        FreightClass = 85,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "370150",
                        Description = "Electronic Equipment, N.O.S.",
                        CodeNumber = 370150,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "480350",
                        Description = "Books, Hardcover",
                        CodeNumber = 480350,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "113750",
                        Description = "Alcoholic Beverages, N.O.S.",
                        CodeNumber = 113750,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "127840",
                        Description = "Doors, N.O.I.B.N.E.C.",
                        CodeNumber = 127840,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "132250",
                        Description = "Food Products, N.O.S.",
                        CodeNumber = 132250,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "139200",
                        Description = "Glassware, N.O.I.B.N.E.C.",
                        CodeNumber = 139200,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "141300",
                        Description = "Hardware, N.O.I.B.N.E.C.",
                        CodeNumber = 141300,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "156600",
                        Description = "Lumber, N.O.S.",
                        CodeNumber = 156600,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "162850",
                        Description = "Machinery, Small, N.O.I.B.N.E.C.",
                        CodeNumber = 162850,
                        FreightClass = 80,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "167560",
                        Description = "Pipes, Tubes, or Hoses, N.O.I.B.N.E.C.",
                        CodeNumber = 167560,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "171610",
                        Description = "Roofing Materials, N.O.I.B.N.E.C.",
                        CodeNumber = 171610,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "178430",
                        Description = "Steel Products, N.O.I.B.N.E.C.",
                        CodeNumber = 178430,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105400",
                        Description = "Safety Equipment, Personal, N.O.I.B.N.E.C.",
                        CodeNumber = 105400,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105410",
                        Description = "Helmets, Protective",
                        CodeNumber = 105410,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105420",
                        Description = "Gloves, Protective",
                        CodeNumber = 105420,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105430",
                        Description = "Eye Protection, Safety Glasses",
                        CodeNumber = 105430,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105440",
                        Description = "Face Shields, Protective",
                        CodeNumber = 105440,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105450",
                        Description = "Respirators, Protective",
                        CodeNumber = 105450,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105460",
                        Description = "Protective Clothing, N.O.I.B.N.E.C.",
                        CodeNumber = 105460,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105470",
                        Description = "Safety Harnesses",
                        CodeNumber = 105470,
                        FreightClass = 60,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105480",
                        Description = "Safety Shoes, Protective Footwear",
                        CodeNumber = 105480,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "105490",
                        Description = "Hearing Protection, Ear Plugs or Earmuffs",
                        CodeNumber = 105490,
                        FreightClass = 55,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109850",
                        Description = "Levels, Spirit or Bubble",
                        CodeNumber = 109850,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109860",
                        Description = "Laser Levels",
                        CodeNumber = 109860,
                        FreightClass = 85,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109870",
                        Description = "Transit Levels",
                        CodeNumber = 109870,
                        FreightClass = 80,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109880",
                        Description = "Builders' Levels",
                        CodeNumber = 109880,
                        FreightClass = 75,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109890",
                        Description = "Digital Levels",
                        CodeNumber = 109890,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109900",
                        Description = "Inclinometers, Leveling Instruments",
                        CodeNumber = 109900,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109910",
                        Description = "Line Levels",
                        CodeNumber = 109910,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109920",
                        Description = "Plumb Bobs",
                        CodeNumber = 109920,
                        FreightClass = 65,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109930",
                        Description = "Optical Levels",
                        CodeNumber = 109930,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },
                    new NMFCCODES
                    {
                        NmfcCodeId = ObjectId.GenerateNewId().ToString(),
                        Name = "109940",
                        Description = "Tiltmeters, Leveling Instruments",
                        CodeNumber = 109940,
                        FreightClass = 70,
                        Enabled = true,
                        Comment = "",
                        IsDeleted = false,
                        CreatedOnUtc = DateTime.UtcNow,
                        UpdatedOnUtc = DateTime.UtcNow,
                    },

                };

            var writes = popularNMFCodes.Select(nmfcCode =>
                new UpdateOneModel<NMFCCODES>(
                    Builders<NMFCCODES>.Filter.Eq(item => item.CodeNumber, nmfcCode.CodeNumber),
                    Builders<NMFCCODES>.Update
                        .SetOnInsert(item => item.NmfcCodeId, nmfcCode.NmfcCodeId)
                        .SetOnInsert(item => item.Enabled, nmfcCode.Enabled)
                        .SetOnInsert(item => item.Comment, nmfcCode.Comment)
                        .SetOnInsert(item => item.IsDeleted, nmfcCode.IsDeleted)
                        .SetOnInsert(item => item.CreatedOnUtc, nmfcCode.CreatedOnUtc)
                        .Set(item => item.Name, nmfcCode.Name)
                        .Set(item => item.Description, nmfcCode.Description)
                        .Set(item => item.FreightClass, nmfcCode.FreightClass)
                        .Set(item => item.UpdatedOnUtc, DateTime.UtcNow))
                { IsUpsert = true });

            await nmfcCodes.BulkWriteAsync(writes, new BulkWriteOptions { IsOrdered = false });

            // MongoDB considers equivalent indexes with different names a conflict.
            // Older databases used the driver's default name, Description_text, so
            // inspect the index definition instead of relying on our preferred name.
            var existingIndexes = await (await nmfcCodes.Indexes.ListAsync()).ToListAsync();
            var hasDescriptionTextIndex = existingIndexes.Any(index =>
                (index.TryGetValue("name", out var name) &&
                    name.IsString && name.AsString == "Description_text") ||
                (index.TryGetValue("weights", out var weights) &&
                    weights.IsBsonDocument && weights.AsBsonDocument.Contains("Description")));

            if (!hasDescriptionTextIndex)
            {
                var indexKeysDefinition = Builders<NMFCCODES>.IndexKeys
                    .Text(item => item.Description);
                var indexModel = new CreateIndexModel<NMFCCODES>(indexKeysDefinition,
                    new CreateIndexOptions { Name = "ix_Description_Text" });
                await nmfcCodes.Indexes.CreateOneAsync(indexModel);
            }

            return true;
        }

    }
}
