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
    public class REGIONS
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? RegionId { get; set; }
        public string? CountryId { get; set; }
        public string? CountryCode { get; set; }
        public string? LongName { get; set; }
        public string? ShortName { get; set; }
        public bool Enabled { get; set; }
    }

    public class REGIONS_ABBR
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Abbr { get; set; }

    }

}
