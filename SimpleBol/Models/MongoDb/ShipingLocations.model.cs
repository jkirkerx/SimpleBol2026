using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.MongoDb
{
    public class SHIPPINGLOCATIONS
    {
        public string? LocationId { get; set; }
        public string? LocationCode { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionAbbr { get; set; }
        public string? RegionName { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryAbbr {  get; set; }
        public string? CountryName { get; set; }
        public string? Phone { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobilePhone { get; set; }
        public string? TimeZoneCode { get; set; }
        public string? TimeZoneName { get; set; }
        public bool? LiftGateRequired { get; set; }
        public bool? PalletJackRequired { get; set; }
        public bool? AppointmentRequired { get; set; }
        public DateTime? AppointmentDate { get; set;  }
        public string? AppointmentTime { get; set; }
        public HoursOfOperation? HoursOfOperation { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmailAddress { get; set; }
        public string? Comment { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        public bool? MarkAsDeleted { get; set; }

    }
    
}
