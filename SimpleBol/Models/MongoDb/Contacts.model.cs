using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.MongoDb
{
    public class CONTACTS
    {
        public string? ContactId { get; set; }
        public string? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? EmailAddress { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        public bool? MarkAsDeleted { get; set; }

    }

}
