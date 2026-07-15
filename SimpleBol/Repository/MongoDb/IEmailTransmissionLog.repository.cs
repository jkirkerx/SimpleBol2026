using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.Smtp;

namespace SimpleBol.Repository.MongoDb;

public interface IEmailTransmissionLogRepository
{
    Task<EmailTransmissionLog> AddAsync(EmailTransmissionLog log);
    Task<EmailTransmissionLog?> GetByTransmissionIdAsync(string transmissionId);
    Task<List<EmailTransmissionLog>> GetRecentAsync(int page, int pageSize);
    Task<List<EmailTransmissionLog>> GetByDocumentIdAsync(string documentId);
    Task<bool> MarkAcceptedAsync(
        string transmissionId,
        string? providerMessageId = null,
        string? providerRequestId = null,
        string? providerStatus = null,
        int? httpStatusCode = null);
    Task<bool> MarkFailedAsync(
        string transmissionId,
        string? errorMessage,
        string? errorCategory = null,
        string? errorCode = null,
        string? providerStatus = null,
        int? httpStatusCode = null);
}

public sealed class EmailTransmissionLogRepository : IEmailTransmissionLogRepository
{
    private readonly MongoDbContext _context;
    private static readonly SortDefinition<EmailTransmissionLog> LatestFirst =
        Builders<EmailTransmissionLog>.Sort.Descending(log => log.CreatedUtc);

    public EmailTransmissionLogRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<EmailTransmissionLog> AddAsync(EmailTransmissionLog log)
    {
        ArgumentNullException.ThrowIfNull(log);

        try
        {
            log.Id = log.Id == ObjectId.Empty ? ObjectId.GenerateNewId() : log.Id;
            log.TransmissionId = string.IsNullOrWhiteSpace(log.TransmissionId)
                ? Guid.NewGuid().ToString("N")
                : log.TransmissionId;
            log.CreatedUtc = log.CreatedUtc == default ? DateTime.UtcNow : log.CreatedUtc.ToUniversalTime();
            log.ComputerName ??= Environment.MachineName;

            await _context.EmailTransmissionLogs.InsertOneAsync(log);
            return log;
        }
        catch (Exception ex)
        {
            ErrorLogging.NLogException(ex, "MongoDb_AddEmailTransmissionLogAsync");
            throw;
        }
    }

    public async Task<EmailTransmissionLog?> GetByTransmissionIdAsync(string transmissionId)
    {
        if (string.IsNullOrWhiteSpace(transmissionId))
            return null;

        try
        {
            return await _context.EmailTransmissionLogs
                .Find(log => log.TransmissionId == transmissionId)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            ErrorLogging.NLogException(ex, "MongoDb_GetEmailTransmissionLogAsync");
            throw;
        }
    }

    public async Task<List<EmailTransmissionLog>> GetRecentAsync(int page, int pageSize)
    {
        ValidatePage(page, pageSize);

        try
        {
            return await _context.EmailTransmissionLogs.Find(_ => true)
                .Sort(LatestFirst)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            ErrorLogging.NLogException(ex, "MongoDb_GetRecentEmailTransmissionLogsAsync");
            throw;
        }
    }

    public async Task<List<EmailTransmissionLog>> GetByDocumentIdAsync(string documentId)
    {
        if (string.IsNullOrWhiteSpace(documentId))
            return [];

        try
        {
            return await _context.EmailTransmissionLogs
                .Find(log => log.RelatedDocumentId == documentId)
                .Sort(LatestFirst)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            ErrorLogging.NLogException(ex, "MongoDb_GetEmailTransmissionLogsByDocumentAsync");
            throw;
        }
    }

    public Task<bool> MarkAcceptedAsync(
        string transmissionId,
        string? providerMessageId = null,
        string? providerRequestId = null,
        string? providerStatus = null,
        int? httpStatusCode = null)
    {
        var update = Builders<EmailTransmissionLog>.Update
            .Set(log => log.Status, EmailTransmissionStatuses.Accepted)
            .Set(log => log.SubmittedUtc, DateTime.UtcNow)
            .Set(log => log.ProviderMessageId, providerMessageId)
            .Set(log => log.ProviderRequestId, providerRequestId)
            .Set(log => log.ProviderStatus, providerStatus)
            .Set(log => log.HttpStatusCode, httpStatusCode)
            .Unset(log => log.FailedUtc)
            .Unset(log => log.ErrorCategory)
            .Unset(log => log.ErrorCode)
            .Unset(log => log.ErrorMessage);

        return UpdateStatusAsync(transmissionId, update, "MongoDb_MarkEmailTransmissionAcceptedAsync");
    }

    public Task<bool> MarkFailedAsync(
        string transmissionId,
        string? errorMessage,
        string? errorCategory = null,
        string? errorCode = null,
        string? providerStatus = null,
        int? httpStatusCode = null)
    {
        var update = Builders<EmailTransmissionLog>.Update
            .Set(log => log.Status, EmailTransmissionStatuses.Failed)
            .Set(log => log.FailedUtc, DateTime.UtcNow)
            .Set(log => log.ErrorMessage, errorMessage)
            .Set(log => log.ErrorCategory, errorCategory)
            .Set(log => log.ErrorCode, errorCode)
            .Set(log => log.ProviderStatus, providerStatus)
            .Set(log => log.HttpStatusCode, httpStatusCode);

        return UpdateStatusAsync(transmissionId, update, "MongoDb_MarkEmailTransmissionFailedAsync");
    }

    private async Task<bool> UpdateStatusAsync(
        string transmissionId,
        UpdateDefinition<EmailTransmissionLog> update,
        string logOperation)
    {
        if (string.IsNullOrWhiteSpace(transmissionId))
            return false;

        try
        {
            var result = await _context.EmailTransmissionLogs.UpdateOneAsync(
                log => log.TransmissionId == transmissionId,
                update);
            return result.IsAcknowledged && result.MatchedCount > 0;
        }
        catch (Exception ex)
        {
            ErrorLogging.NLogException(ex, logOperation);
            throw;
        }
    }

    private static void ValidatePage(int page, int pageSize)
    {
        if (page < 1)
            throw new ArgumentOutOfRangeException(nameof(page), "Page must be at least 1.");
        if (pageSize < 1)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be at least 1.");
    }
}
