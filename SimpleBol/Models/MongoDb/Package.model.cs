using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class PACKAGES
    {
        public string? PackageID { get; set; }
        public string? PackageCode { get; set; }
        public string? PackageDescription { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public decimal Girth { get; set; }
        public float Volume { get; set; }
        public string? VolumeType { get; set; }
        public int Weight { get; set; }
        public string? WeightType { get; set; }
        public int UnitCount { get; set; }
        public string? NmfcCodeId { get; set; }
        public NMFCCODES? NmfcCode { get; set; }
        public string? ClassCodeId { get; set; }
        public FREIGHTCLASSCODES? ClassCode { get; set; }
        public DateTime DateShipped { get; set; }
        public DateTime DateReceived { get; set; }
        public string? MeasurementCode { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EstimatedValue { get; set; }

        public string? CurrencyCode { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }
                
        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

    }
}
