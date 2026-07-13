using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IBillToAccountsRepository
    {
        Task<List<BILLTOACCOUNTS>> GetAllBillToAccountsAsync();
        Task<List<BILLTOACCOUNTS>> GetBillToAccountsByCustomerIdAsync(string customerId);
        Task<BILLTOACCOUNTS?> GetOneBillToAccountAsync(string billToAccountId);
        Task AddBillToAccountAsync(BILLTOACCOUNTS billToAccount);
        Task<bool> UpdateBillToAccountAsync(BILLTOACCOUNTS billToAccount, string billToAccountId);
        Task<bool> RemoveBillToAccountAsync(string billToAccountId);
    }

    public class BillToAccountsRepository : IBillToAccountsRepository
    {
        private readonly MongoDbContext _context;

        public BillToAccountsRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<BILLTOACCOUNTS>> GetAllBillToAccountsAsync()
        {
            try
            {
                return await _context.BillToAccounts.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllBillToAccountsAsync");
                throw;
            }

        }

        public async Task<List<BILLTOACCOUNTS>> GetBillToAccountsByCustomerIdAsync(string customerId)
        {
            try
            {
                return await _context.BillToAccounts.Find(c => c.BindToCustomerId == customerId).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllBillToAccountsAsync");
                throw;
            }

        }

        public async Task<BILLTOACCOUNTS?> GetOneBillToAccountAsync(string billToAccountId)
        {
            try
            {
                return await _context.BillToAccounts.Find(bta => bta.BillToAccountId == billToAccountId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneBillToAccountAsync");
                throw;
            }
        }

        public async Task AddBillToAccountAsync(BILLTOACCOUNTS billToAccount)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(billToAccount);
                await EnrichLocationNamesAsync(billToAccount);
                billToAccount.BillToAccountId = string.IsNullOrWhiteSpace(billToAccount.BillToAccountId)
                    ? ObjectId.GenerateNewId().ToString()
                    : billToAccount.BillToAccountId;
                billToAccount.CreatedOnUtc = billToAccount.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : billToAccount.CreatedOnUtc;
                billToAccount.UpdatedOnUtc = DateTime.UtcNow;
                await _context.BillToAccounts.InsertOneAsync(billToAccount);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddBillToAccountAsync");
                throw;
            }

        }

        public async Task<bool> UpdateBillToAccountAsync(BILLTOACCOUNTS billToAccount, string billToAccountId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(billToAccount);
                await EnrichLocationNamesAsync(billToAccount);

                var filter = Builders<BILLTOACCOUNTS>.Filter.Eq(s => s.BillToAccountId, billToAccountId);
                var update = Builders<BILLTOACCOUNTS>.Update
                    .Set(s => s.CompanyName, billToAccount.CompanyName)
                    .Set(s => s.Address1, billToAccount.Address1)
                    .Set(s => s.Address2, billToAccount.Address2)
                    .Set(s => s.City, billToAccount.City)
                    .Set(s => s.RegionCode, billToAccount.RegionCode)
                    .Set(s => s.RegionAbbr, billToAccount.RegionAbbr)
                    .Set(s => s.RegionLongName, billToAccount.RegionLongName)
                    .Set(s => s.CountryCode, billToAccount.CountryCode)
                    .Set(s => s.CountryAbbr, billToAccount.CountryAbbr)
                    .Set(s => s.CountryLongName, billToAccount.CountryLongName)
                    .Set(s => s.PostalCode, billToAccount.PostalCode)
                    .Set(s => s.ContactName, billToAccount.ContactName)
                    .Set(s => s.ContactPhone1, billToAccount.ContactPhone1)
                    .Set(s => s.ContactEmailAddress1, billToAccount.ContactEmailAddress1)
                    .Set(s => s.AccountNumber, billToAccount.AccountNumber)
                    .Set(s => s.Comment, billToAccount.Comment)  
                    .Set(s => s.BindToCustomerId, billToAccount.BindToCustomerId)
                    .Set(s => s.MarkAsDeleted, billToAccount.MarkAsDeleted)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);                    

                UpdateResult actionResult = await _context.BillToAccounts.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateBillToAccountAsync");
                throw;
            }
        }

        public async Task<bool> RemoveBillToAccountAsync(string billToAccountId)
        {
            try
            {
                var filter = Builders<BILLTOACCOUNTS>.Filter.Eq(
                    account => account.BillToAccountId, billToAccountId);
                var update = Builders<BILLTOACCOUNTS>.Update
                    .Set(account => account.MarkAsDeleted, true)
                    .Set(account => account.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.BillToAccounts
                    .UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveBillToAccountAsync");
                throw;
            }
        }

        private async Task EnrichLocationNamesAsync(BILLTOACCOUNTS account)
        {
            if (!string.IsNullOrWhiteSpace(account.CountryCode))
            {
                var country = await _context.Countries
                    .Find(item => item.CountryId == account.CountryCode)
                    .FirstOrDefaultAsync();
                if (country is not null)
                {
                    account.CountryLongName = country.LongName;
                    account.CountryAbbr = country.ShortName;
                }
            }

            if (!string.IsNullOrWhiteSpace(account.RegionCode))
            {
                var region = await _context.Regions
                    .Find(item => item.RegionId == account.RegionCode)
                    .FirstOrDefaultAsync();
                if (region is not null)
                {
                    account.RegionLongName = region.LongName;
                    account.RegionAbbr = region.ShortName;
                }
            }
        }



    }
}
