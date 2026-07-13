using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.MongoDb
{
    [BsonIgnoreExtraElements]
    public class CUSTOMERS
    {
        public BsonObjectId? Id { get; set; }
        public string? CustomerId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionAbbr {  get; set; }
        public string? RegionLongName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryAbbr {  get; set; }
        public string? CountryLongName { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone1 { get; set; }
        public string? EmailAddress1 { get; set; }
        public string? Phone2 { get; set; }
        public string? EmailAddress2 { get; set; }
        public bool LiftgateRequired { get; set; }
        public bool PalletJackRequired { get; set; }
        public List<SHIPPINGLOCATIONS>? ShippingLocations { get; set; }
        public string? Comment { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        public bool MarkAsDeleted { get; set; }


    }

}
