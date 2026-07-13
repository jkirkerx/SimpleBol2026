using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBol.Models.MongoDb
{
    public class BILLINGDISPUTES
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? BillingDisputeId { get; set; }

        [BsonDateTimeOptions]
        public DateTime DisputeDate { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Argument { get; set; }
        public string? Remarks { get; set; }
        public bool Resolved { get; set; }
        public string? Resolution { get; set; }

        [BsonDateTimeOptions]
        public DateTime ResolutionDate { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal QuotedPrice { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal ActualPrice { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal CreditedAmount { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal AdjustedPrice { get; set; }

        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? ShipperInvoiceNumber { get; set; }
        public string? ShipperQuoteNumber { get; set; }
        public string? ShipperReferenceNumber { get; set; }

        [BsonDateTimeOptions]
        public DateTime? ShipperShipDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime? ShipperDeliveryDate { get; set; }

        public SHIPPER? Shipper { get; set; }
        public string? ShipperContactId { get; set; }
        public List<ShipperContacts>? ShipperContacts { get; set; }
        public string? OrderNumber { get; set; }
        public string? DisputedBolId { get; set; }
        public BILLOFLADINGS? DisputedBol { get; set; }
        public string? CustomerId { get; set; }
        public CUSTOMERS? Customer { get; set; }
        
        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        // Mark as Deleted
        public bool MarkAsDeleted { get; set; }

    }
}
