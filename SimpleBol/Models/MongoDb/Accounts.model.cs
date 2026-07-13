using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace SimpleBol.Models.MongoDb
{
    public class ACCOUNTS
    {
        [BsonId]
        public BsonObjectId? _id { get; set; }
        public string? AccountId { get; set; }
        public string? LoginId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? EmailAddress { get; set; }
        public string? Location { get; set; }
        public string? PasswordHashed { get; set; }
        public byte[]? PasswordSalt { get; set; }        
        public DateTime PasswordCreatedOnUtc { get; set; }
        public string? Comments { get; set; }
        public string? TimeZone { get; set; }
        public byte[]? SecureToken { get; set; }
        public byte[]? Secret { get; set; }
        public string? SecurityLevel { get; set; }
        public DateTime LastLogin { get; set; }
              

        [BsonDateTimeOptions]
        public DateTime CreatedOnUtc { get; set; }        

        [BsonDateTimeOptions]
        public DateTime UpdatedOnUtc { get; set; }

        // Mark as Deleted
        public bool MarkAsDeleted {  get; set; }
    }

    enum SecurityLevels
    {
        User = 0,
        Admin = 1
    }
}
