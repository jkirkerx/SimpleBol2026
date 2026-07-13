using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface INmfcCodesRepository
    {

        Task<List<NMFCCODES>> GetAllNmfcCodesAsync();
        Task<List<NMFCCODES>> GetAvailableNmfcCodesAsync();
        Task<NMFCCODES?> GetOneNmfcCodeAsync(string nmfcCodeId);
        Task AddNmfcCodeAsync(NMFCCODES nmfcCode);        
        Task<bool> UpdateNmfcCodeAsync(NMFCCODES nmfcCode, string nmfcCodeId);
        Task<bool> RemoveNmfcCodeAsync(string nmfcCodeId);
        Task<bool> EnableNmfcFreightCodeAsync(string nmfcCodeId);
        Task<bool> DisableNmfcFreightCodeAsync(string nmfcCodeId);
        Task<List<NMFCCODES>> SearchForNmfcCodesInDescription(string searchString);

    }

    public class NmfcCodesRepository : INmfcCodesRepository
    {
        private readonly MongoDbContext _context;

        public NmfcCodesRepository(MongoDbContext context)
        {
            _context = context;
        }

        #region CRUD

        public async Task<List<NMFCCODES>> GetAllNmfcCodesAsync()
        {
            try
            {
                return await _context.NmfcCodes.Find(_ => true).SortBy(s => s.CodeNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllNmfcCodesAsync");
                throw;
            }

        }

        public async Task<List<NMFCCODES>> GetAvailableNmfcCodesAsync()
        {
            return await _context.NmfcCodes
                .Find(code => code.Enabled == true && code.IsDeleted != true)
                .SortBy(code => code.CodeNumber)
                .ToListAsync();
        }

        public async Task<NMFCCODES?> GetOneNmfcCodeAsync(string nmfcCodeId)
        {
            try
            {
                return await _context.NmfcCodes.Find(nmfcCode => nmfcCode.NmfcCodeId == nmfcCodeId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneNmfcCodeAsync");
                throw;
            }
        }

        public async Task AddNmfcCodeAsync(NMFCCODES nmfcCode)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nmfcCode);
                nmfcCode.NmfcCodeId = string.IsNullOrWhiteSpace(nmfcCode.NmfcCodeId)
                    ? ObjectId.GenerateNewId().ToString()
                    : nmfcCode.NmfcCodeId;
                nmfcCode.CreatedOnUtc = nmfcCode.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : nmfcCode.CreatedOnUtc;
                nmfcCode.UpdatedOnUtc = DateTime.UtcNow;
                await _context.NmfcCodes.InsertOneAsync(nmfcCode);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddNmfcCodeAsync");
                throw;
            }

        }        

        public async Task<bool> UpdateNmfcCodeAsync(NMFCCODES nmfcCode, string nmfcCodeId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(nmfcCode);
                var filter = Builders<NMFCCODES>.Filter.Eq(s => s.NmfcCodeId, nmfcCodeId);
                var existing = await _context.NmfcCodes.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                nmfcCode._id = existing._id;
                nmfcCode.NmfcCodeId = existing.NmfcCodeId;
                nmfcCode.CreatedOnUtc = existing.CreatedOnUtc;
                nmfcCode.UpdatedOnUtc = DateTime.UtcNow;
                ReplaceOneResult actionResult = await _context.NmfcCodes
                    .ReplaceOneAsync(filter, nmfcCode);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateNmfcCodeAsync");
                throw;
            }
        }

        public async Task<bool> RemoveNmfcCodeAsync(string nmfcCodeId)
        {
            try
            {
                var filter = Builders<NMFCCODES>.Filter.Eq(
                    code => code.NmfcCodeId, nmfcCodeId);
                var update = Builders<NMFCCODES>.Update
                    .Set(code => code.IsDeleted, true)
                    .Set(code => code.Enabled, false)
                    .Set(code => code.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.NmfcCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveNmfcCodeAsync");
                throw;
            }
        }

        #endregion
        #region EnableDisable

        public async Task<bool> EnableNmfcFreightCodeAsync(string nmfcCodeId)
        {
            try
            {
                var filter = Builders<NMFCCODES>.Filter.Eq(s => s.NmfcCodeId, nmfcCodeId);
                var update = Builders<NMFCCODES>.Update
                    .Set(s => s.Enabled, true)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.NmfcCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_EnableNmfcFreightCodeAsync");
                throw;
            }

        }

        public async Task<bool> DisableNmfcFreightCodeAsync(string nmfcCodeId)
        {
            try
            {
                var filter = Builders<NMFCCODES>.Filter.Eq(s => s.NmfcCodeId, nmfcCodeId);
                var update = Builders<NMFCCODES>.Update
                    .Set(s => s.Enabled, false)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.NmfcCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_DisableNmfcFreightCodeAsync");
                throw;
            }

        }

        #endregion
        #region Search

        public async Task<List<NMFCCODES>> SearchForNmfcCodesInDescription(string searchString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return await GetAllNmfcCodesAsync();

                var searchFilter = Builders<NMFCCODES>.Filter.Text(searchString.Trim());
                return await _context.NmfcCodes.Find(searchFilter)
                    .SortBy(code => code.CodeNumber)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_SearchForNmfcCodes");
                throw;
            }            

        }


        #endregion




    }
}
