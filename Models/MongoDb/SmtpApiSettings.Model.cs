using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.MongoDb
{
    public class SMTPAPISETTINGS
    {
        public BsonObjectId? Id { get; set; }
        public string? SmtpId { get; set; }
        public SendGrid? SendGrid { get; set; }
        public Gmail? Gmail { get; set; }
        public Outlook365? Outlook365 { get; set; }
        public CompanyInfo? CompanyInfo { get; set; }
        public string? DefaultId { get; set; }
        public string? SecureToken { get; set; }
    }

    public class SendGrid
    {
        public BsonObjectId? Id { get; set; }
        public string? ApiKey { get; set; }
        public string? SentFrom { get; set; }
        public byte[]? Salt { get; set; }
    }

    public class Gmail
    {
        public BsonObjectId? Id { get; set; }
        public string? ServiceId { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? ApiKey { get; set; }        
        public string? SentFrom { get; set; }
        public byte[]? Salt { get; set; }
    }

    public class  Outlook365 
    {
        public BsonObjectId? Id { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
        public string? SentFrom { get; set; }
        public byte[]? Salt { get; set; }
    }

    public class CompanyInfo
    {
        public string? CompanyName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set;}
        public string? City { get; set; }
        public string? RegionId { get; set; }
        public string? RegionName { get; set; }    
        public string? PostalCode { get; set; }
        public string? CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Phone { get; set; }

    }
    
}
