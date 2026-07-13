using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.MongoDb
{
    public class COUNTRIES
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? CountryId { get; set; }
        public string? LongName { get; set; }
        public string? ShortName { get; set; }
        public bool? Enabled { get; set; }
        public List<REGIONS_ABBR>? Regions { get; set; }

    }

    public class ServiceCountries
    {
        public string? CountryCode { get; set; }
        public string? LongName { get; set; }
        public string? ShortName { get; set; }
    }
}
