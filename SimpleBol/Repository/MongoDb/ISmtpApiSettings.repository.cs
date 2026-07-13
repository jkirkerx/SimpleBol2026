using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface ISmtpApiSettingsRepository
    {
        Task<SMTPAPISETTINGS?> GetSmtpCredentialsAsync();
        Task AddSmtpCredentialsAsync(SMTPAPISETTINGS smtp);
        Task<bool> UpdateSmtpCredentialsAsync(SMTPAPISETTINGS smtp, string? smtpId);
    }

    public class SmtpApiSettingsRepository : ISmtpApiSettingsRepository
    {
        private readonly MongoDbContext _context;

        public SmtpApiSettingsRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<SMTPAPISETTINGS?> GetSmtpCredentialsAsync()
        {
            try
            {
                return await _context.SmtpCredentials.Find(_ => true).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetSmtpCredentialsAsync");
                throw;
            }

        }

        public async Task AddSmtpCredentialsAsync(SMTPAPISETTINGS smtp)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(smtp);
                await EnrichCompanyLocationAsync(smtp);

                var existing = await _context.SmtpCredentials.Find(_ => true).FirstOrDefaultAsync();
                if (existing is not null)
                {
                    smtp.Id = existing.Id;
                    smtp.SmtpId = existing.SmtpId;
                    smtp.SecureToken ??= existing.SecureToken;
                    await _context.SmtpCredentials.ReplaceOneAsync(
                        item => item.SmtpId == existing.SmtpId, smtp);
                    return;
                }

                smtp.SmtpId = string.IsNullOrWhiteSpace(smtp.SmtpId)
                    ? ObjectId.GenerateNewId().ToString()
                    : smtp.SmtpId;
                await _context.SmtpCredentials.InsertOneAsync(smtp);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddSmtpCredentialsAsync");
                throw;
            }

        }

        public async Task<bool> UpdateSmtpCredentialsAsync(SMTPAPISETTINGS smtp, string? smtpId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(smtp);
                if (string.IsNullOrWhiteSpace(smtpId))
                    return false;

                await EnrichCompanyLocationAsync(smtp);
                var filter = Builders<SMTPAPISETTINGS>.Filter.Eq(x => x.SmtpId, smtpId);
                var existing = await _context.SmtpCredentials.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                smtp.Id = existing.Id;
                smtp.SmtpId = existing.SmtpId;
                smtp.SecureToken ??= existing.SecureToken;
                ReplaceOneResult actionResult = await _context.SmtpCredentials
                    .ReplaceOneAsync(filter, smtp);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateSmtpCredentialsAsync");
                throw;
            }
        }

        private async Task EnrichCompanyLocationAsync(SMTPAPISETTINGS smtp)
        {
            if (smtp.CompanyInfo is null)
                return;

            if (!string.IsNullOrWhiteSpace(smtp.CompanyInfo.CountryId))
            {
                var country = await _context.Countries
                    .Find(item => item.CountryId == smtp.CompanyInfo.CountryId)
                    .FirstOrDefaultAsync();
                if (country is not null)
                    smtp.CompanyInfo.CountryName = country.LongName;
            }

            if (!string.IsNullOrWhiteSpace(smtp.CompanyInfo.RegionId))
            {
                var region = await _context.Regions
                    .Find(item => item.RegionId == smtp.CompanyInfo.RegionId)
                    .FirstOrDefaultAsync();
                if (region is not null)
                    smtp.CompanyInfo.RegionName = region.LongName;
            }
        }

    }

}
