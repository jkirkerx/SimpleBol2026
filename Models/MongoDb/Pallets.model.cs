using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class PALLETS
    {        
        public string? PalletID { get; set; }        
        public string? PalletCode { get; set; }
        public string? PalletDescription { get; set; }

        [BsonIgnore]
        public string? Description
        {
            get => PalletDescription;
            set => PalletDescription = value;
        }

        public int PalletType { get; set; }

        [BsonIgnore]
        public string? ShipmentType { get; set; }

        [BsonIgnore]
        public string? ClassText => ClassCode?.Name;
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Girth {  get; set; }
        public float Volume {  get; set; }
        public string? VolumeType { get; set; }
        public int Weight { get; set; }
        public string? WeightType { get; set; }
        public string? NmfcCodeId { get; set; }
        public NMFCCODES? NmfcCode { get; set; }
        public string? ClassCodeId {  get; set; }
        public FREIGHTCLASSCODES? ClassCode { get; set; }
        public int UnitCount { get; set; }
        public int BoxCount { get; set; }        
        public string? RfCodeNumber { get; set; }
        public DateTime DateShipped { get; set; }
        public DateTime DateReceived { get; set; }
        public string? MeasurementCode { get; set; }
        public int NonStackablePallet { get; set; }
        public int StackablePallet { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EstimatedValue { get; set; }

        public string? CurrencyCode { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

    }

    
}
