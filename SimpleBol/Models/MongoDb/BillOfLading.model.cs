using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace SimpleBol.Models.MongoDb
{    

    public class BILLOFLADINGS
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? BolId { get; set; }
        public string? BolNumber {  get; set; }

        [BsonDateTimeOptions]
        public DateTime BolDate { get; set; }

        public string? ShipmentType { get; set; }
        public string? OrderNumber { get; set; }
        public bool ThirdPartyBilling { get; set; }
        public bool PrintHoursOfOperation { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public CUSTOMERS? Customer { get; set; }
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? ShipperQuoteNumber { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal ShipperQuotePrice { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal ShipperActualPrice { get; set; }

        public string? ShipperReferenceNumber { get; set; }
        public SHIPPER? Shipper { get; set; }        
        public string? BillToId { get; set; }
        public BILLTOACCOUNTS? BillToAccount { get; set; }
        public string? ShipFromId { get; set; }
        public VENDORS? ShipFromVendor { get; set; }
        public string? ShipFromLocationId { get; set; }
        public SHIPPINGLOCATIONS? ShipFromLocation { get; set; }        
        public string? ShipToId { get; set; }
        public CUSTOMERS? ShipToCustomer { get; set; }
        public string? ShipToLocationId { get; set; }
        public SHIPPINGLOCATIONS? ShipToLocation { get; set; }
        public bool COD { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal CodAmount { get; set; }

        public CODCHARGES? CodChargedTo { get; set; }
        public bool FreightCustomerInvoiced { get; set; }
        public bool FreightPrePaid { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal BolEstimatedValue { get; set; }

        public int BolEstimatedWeight { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal EstimatedShippingPrice { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal ActualShippingPrice { get; set; }

        public bool ShippingPriceOverCharged { get; set; }
        public List<PALLETS>? Pallets { get; set; }        
        public decimal TotalPalletWeight { get; set; }
        public List<PACKAGES>? Packages { get; set; }
        public decimal TotalPackageWeight { get; set; }
        public List<CONTAINERS>? Containers { get; set; }
        public decimal TotalContainerWeight { get; set; }
        public string? Comments { get; set; }
        public string? SpecialInstructions { get; set; }
        public DateTime PickupDatePromised { get; set; }

        [BsonDateTimeOptions]
        public DateTime ShipDate { get; set; }

        [BsonDateTimeOptions]
        public DateTime DeliveryDate { get; set; }        
        public int TransitDaysEstimated { get; set; }
        public int TransitDaysActual { get; set; }

        [BsonDateTimeOptions]
        public DateTime AppointmentPickup { get; set; }

        [BsonDateTimeOptions]
        public DateTime AppointmentDelivery { get; set; }

        public bool Disputed { get; set; }
        public bool Printed { get; set; }
        public bool Emailed { get; set; }
        
        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        public bool? MarkAsDeleted { get; set; }

    }

    public enum CODCHARGES
    {
        Shipper,
        Consignee
    }

}
