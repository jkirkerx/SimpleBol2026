using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class SHIPPER
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? ShipperId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionShortName {  get; set; }
        public string? RegionLongName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryShortName { get; set; }
        public string? CountryLongName { get; set; }
        public string? PostalCode { get; set; }        
        public string? Phone1 { get; set; }
        public string? EmailAddress1 { get; set; }
        public string? Phone2 { get; set; }
        public string? EmailAddress2 { get; set; }
        public bool ElectronicQuotesAvailable { get; set; }
        public bool TrackingService { get; set; }
        public bool Liftgate { get; set; }
        public bool Favorite { get; set; }
        public List<ShipperContacts>? ShipperContacts { get; set; }
        public ShipperServices? ShipperServices { get; set; }
        public List<ServiceCountries>? ServiceCountries { get; set; }

        public string? Comment { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        public bool? MarkAsDeleted { get; set; }

    }

    public class ShipperContacts
    {
        
        public string? ShipperId {  get; set; }        
        public string? ContactId { get; set; }
        public string? ContactName { get; set;}
        public string? ContactPhone { get; set;}
        public string? ContactEmailAddress { get; set; }

    }

    public class ShipperServices
    {
        public bool ServiceLTL { get; set; }
        public bool ServiceFTL { get; set; }
        public bool ServiceOcean { get; set; }
        public bool ServiceRailroad { get; set; }
        public bool ServiceAirplane { get; set; }
        public bool ServiceLastMile { get; set; }
        public bool ServiceCourier { get; set; }
        public bool ServiceArmouredCar { get; set; }
    }

    public enum ShipperServicesEnum
    {
        LTL,
        FTL,        
        AIR,
        OCEAN,
        RAIL,
        LASTMILE,
        COURIER,
        ARMOURED,
        SHOWALL
    }

 
}
