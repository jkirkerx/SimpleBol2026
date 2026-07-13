using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class FREIGHTCLASSCODES
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? FreightClassCodeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? CodeNumber { get; set; }
        public string? WeightType { get; set; }
        public double? MinWeightPerfoot { get; set; }
        public double? MaxWeightPerFoot { get; set; }
        public bool? Enabled { get; set; }
        public string? Comment { get; set; }
        public bool? IsDeleted { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

    }
}
