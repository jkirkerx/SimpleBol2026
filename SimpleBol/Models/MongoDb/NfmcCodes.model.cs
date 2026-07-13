using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class NMFCCODES
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? NmfcCodeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? CodeNumber { get; set; }
        public double? FreightClass {  get; set; }
        public bool? Enabled { get; set; }
        public string? Comment { get; set; }
        public bool? IsDeleted { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

    }
}
