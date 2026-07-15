using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleBol.Models.Smtp
{
    /// <summary>
    /// Durable audit record for an email submission to any delivery provider.
    /// This records the provider handoff, not final inbox delivery.
    /// </summary>
    public class EmailTransmissionLog
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string TransmissionId { get; set; } = Guid.NewGuid().ToString("N");
        public string Provider { get; set; } = EmailProviders.Unknown;
        public string Status { get; set; } = EmailTransmissionStatuses.Pending;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? SubmittedUtc { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? FailedUtc { get; set; }

        public int AttemptNumber { get; set; } = 1;
        public string? ComputerName { get; set; }
        public string? UserId { get; set; }

        // Optional link back to the business document that caused the email.
        public string? RelatedDocumentType { get; set; }
        public string? RelatedDocumentId { get; set; }
        public string? RelatedDocumentNumber { get; set; }

        public EmailLogAddress? From { get; set; }
        public List<EmailLogAddress> To { get; set; } = [];
        public List<EmailLogAddress> Cc { get; set; } = [];
        public List<EmailLogAddress> Bcc { get; set; } = [];
        public string? Subject { get; set; }
        public List<EmailLogAttachment> Attachments { get; set; } = [];

        // IDs and status returned by Gmail, SendGrid, Outlook, or a future provider.
        public string? ProviderMessageId { get; set; }
        public string? ProviderRequestId { get; set; }
        public string? ProviderStatus { get; set; }
        public int? HttpStatusCode { get; set; }

        public string? ErrorCategory { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class EmailLogAddress
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
    }

    public class EmailLogAttachment
    {
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long? SizeBytes { get; set; }
    }

    public static class EmailProviders
    {
        public const string Unknown = "UNKNOWN";
        public const string SendGrid = "SENDGRID";
        public const string Gmail = "GMAIL";
        public const string Outlook365 = "OUTLOOK365";
    }

    public static class EmailTransmissionStatuses
    {
        public const string Pending = "PENDING";
        public const string Accepted = "ACCEPTED";
        public const string Failed = "FAILED";
    }
}
