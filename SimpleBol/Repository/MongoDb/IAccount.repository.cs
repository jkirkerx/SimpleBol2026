using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IAccountsRepository
    {
        Task<List<ACCOUNTS>> GetAllAccountsAsync();
        Task<bool> AnyAccountsAsync();
        Task<bool> AnySuccessfulLoginAsync();
        Task<ACCOUNTS?> GetAccountByLoginIdAsync(string loginId);
        Task RecordSuccessfulLoginAsync(string accountId);
        Task<ACCOUNTS?> GetOneAccountAsync(string accountId);
        Task AddAccountAsync(ACCOUNTS account);
        Task<bool> UpdateAccountAsync(ACCOUNTS account, string accountId);
        Task<bool> UpdateAccountPasswordOnlyAsync(string accountId, string passwordHashed, byte[] passwordSalt);
        Task<bool> UpdateAccountSecureTokenAsync(string accountId, byte[] secureToken, byte[] secret);
        Task<bool> RemoveAccountAsync(string accountId);

    }

    public class AccountsRepository : IAccountsRepository
    {
        private readonly MongoDbContext _context;

        public AccountsRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<ACCOUNTS>> GetAllAccountsAsync()
        {
            try
            {
                return await _context.Accounts.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllAccountsAsync");
                throw;
            }

        }

        public Task<bool> AnyAccountsAsync()
        {
            return _context.Accounts
                .Find(account => !account.MarkAsDeleted)
                .AnyAsync();
        }

        public Task<bool> AnySuccessfulLoginAsync()
        {
            return _context.Accounts
                .Find(account => !account.MarkAsDeleted && account.LastLogin > DateTime.MinValue)
                .AnyAsync();
        }

        public async Task<ACCOUNTS?> GetAccountByLoginIdAsync(string loginId)
        {
            var loginPattern = "^" +
                System.Text.RegularExpressions.Regex.Escape(loginId.Trim()) + "$";
            var filter = Builders<ACCOUNTS>.Filter.And(
                Builders<ACCOUNTS>.Filter.Eq(account => account.MarkAsDeleted, false),
                Builders<ACCOUNTS>.Filter.Regex(
                    account => account.LoginId,
                    new BsonRegularExpression(loginPattern, "i")));

            return await _context.Accounts.Find(filter).FirstOrDefaultAsync();
        }

        public async Task RecordSuccessfulLoginAsync(string accountId)
        {
            var filter = Builders<ACCOUNTS>.Filter.Eq(account => account.AccountId, accountId);
            var update = Builders<ACCOUNTS>.Update
                .Set(account => account.LastLogin, DateTime.UtcNow);

            await _context.Accounts.UpdateOneAsync(filter, update);
        }

        public async Task<ACCOUNTS?> GetOneAccountAsync(string accountId)
        {
            try
            {
                return await _context.Accounts.Find(account => account.AccountId == accountId).FirstOrDefaultAsync();                

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneAccountAsync");
                throw;
            }
        }

        public async Task AddAccountAsync(ACCOUNTS account)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(account);
                if (string.IsNullOrWhiteSpace(account.LoginId))
                    throw new ArgumentException("A login ID is required.", nameof(account));
                if (await LoginIdExistsAsync(account.LoginId))
                    throw new InvalidOperationException(
                        $"The login ID '{account.LoginId.Trim()}' is already in use.");

                account.AccountId = string.IsNullOrWhiteSpace(account.AccountId)
                    ? ObjectId.GenerateNewId().ToString()
                    : account.AccountId;
                account.LoginId = account.LoginId.Trim();
                account.CreatedOnUtc = account.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : account.CreatedOnUtc;
                account.UpdatedOnUtc = DateTime.UtcNow;
                await _context.Accounts.InsertOneAsync(account);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddAccountAsync");
                throw;
            }

        }

        public async Task<bool> UpdateAccountAsync(ACCOUNTS account, string accountId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(account);
                if (string.IsNullOrWhiteSpace(accountId))
                    throw new ArgumentException("An account ID is required.", nameof(accountId));
                if (string.IsNullOrWhiteSpace(account.LoginId))
                    throw new ArgumentException("A login ID is required.", nameof(account));

                var normalizedLoginId = account.LoginId.Trim();
                var existingAccount = await _context.Accounts
                    .Find(existing => existing.AccountId == accountId)
                    .FirstOrDefaultAsync();

                if (existingAccount is null)
                    return false;

                var loginIdChanged = !string.Equals(
                    existingAccount.LoginId?.Trim(),
                    normalizedLoginId,
                    StringComparison.OrdinalIgnoreCase);

                if (loginIdChanged && await LoginIdExistsAsync(normalizedLoginId, accountId))
                    throw new InvalidOperationException(
                        $"The login ID '{normalizedLoginId}' is already in use.");

                var filter = Builders<ACCOUNTS>.Filter.Eq(s => s.AccountId, accountId);
                var updateFilter = Builders<ACCOUNTS>.Update
                    .Set(s => s.LoginId, normalizedLoginId)
                    .Set(s => s.Name, account.Name)
                    .Set(s => s.Phone, account.Phone)
                    .Set(s => s.EmailAddress, account.EmailAddress)
                    .Set(s => s.Location, account.Location)                    
                    .Set(s => s.TimeZone, account.TimeZone)                    
                    .Set(s => s.SecurityLevel, account.SecurityLevel)
                    .Set(s => s.Comments, account.Comments)
                    .Set(s => s.MarkAsDeleted, account.MarkAsDeleted)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.Accounts.UpdateOneAsync(filter, updateFilter);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateAccountAsync");
                throw;
            }
        }

        public async Task<bool> UpdateAccountPasswordOnlyAsync(string accountId, string passwordHashed, byte[] passwordSalt)
        {
            try
            {
                var filter = Builders<ACCOUNTS>.Filter.Eq(s => s.AccountId, accountId);
                var update = Builders<ACCOUNTS>.Update
                    .Set(s => s.PasswordHashed, passwordHashed)
                    .Set(s => s.PasswordSalt, passwordSalt)
                    .Set(s => s.PasswordCreatedOnUtc, DateTime.UtcNow)                    
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.Accounts.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateAccountPasswordOnlyAsync");
                throw;
            }

        }

        public async Task<bool> UpdateAccountSecureTokenAsync(string accountId, byte[] secureToken, byte[] secret)
        {
            try
            {
                var filter = Builders<ACCOUNTS>.Filter.Eq(s => s.AccountId, accountId);
                var update = Builders<ACCOUNTS>.Update
                    .Set(s => s.SecureToken, secureToken)
                    .Set(s => s.Secret, secret);

                UpdateResult actionResult
                    = await _context.Accounts.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateAccountSecureTokenAsync");
                throw;
            }

        }

        public async Task<bool> RemoveAccountAsync(string accountId)
        {
            try
            {
                var filter = Builders<ACCOUNTS>.Filter.Eq(s => s.AccountId, accountId);
                var update = Builders<ACCOUNTS>.Update
                    .Set(s => s.MarkAsDeleted, true)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.Accounts.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveAccountAsync");
                throw;
            }
        }

        private async Task<bool> LoginIdExistsAsync(string loginId, string? exceptAccountId = null)
        {
            var pattern = "^" +
                System.Text.RegularExpressions.Regex.Escape(loginId.Trim()) + "$";
            var filter = Builders<ACCOUNTS>.Filter.Regex(
                account => account.LoginId,
                new BsonRegularExpression(pattern, "i"));

            if (!string.IsNullOrWhiteSpace(exceptAccountId))
            {
                filter &= Builders<ACCOUNTS>.Filter.Ne(
                    account => account.AccountId, exceptAccountId);
            }

            return await _context.Accounts.Find(filter).AnyAsync();
        }

    }
}
