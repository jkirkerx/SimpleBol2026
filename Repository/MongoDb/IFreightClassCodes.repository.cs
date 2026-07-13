using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IFreightClassCodesRepository
    {
        Task<List<FREIGHTCLASSCODES>> GetAllFreightClassCodesAsync();
        Task<List<FREIGHTCLASSCODES>> GetAvailableFreightClassCodesAsync();
        Task<FREIGHTCLASSCODES?> GetOneFreightClassCodeAsync(string freightClassCodeId);
        Task AddFreightClassCodeAsync(FREIGHTCLASSCODES freightClassCode);
        Task<bool> EnableFreightClassCodeAsync(string freightClassCodeId);
        Task<bool> DisableFreightClassCodeAsync(string freightClassCodeId);
        Task<bool> UpdateFreightClassCodeAsync(FREIGHTCLASSCODES freightClassCode, string freightClassCodeId);
        Task<bool> RemoveFreightClassCodeAsync(string freightClassCodeId);
        Task<FREIGHTCLASSCODES?> MatchCorrectCodeByLinearDensity(double linearDensity);

    }

    public class FreightClassCodesRepository : IFreightClassCodesRepository
    {

        private readonly MongoDbContext _context;

        public FreightClassCodesRepository(MongoDbContext context)
        {
            _context = context;
        }

        #region CRUD

        public async Task<List<FREIGHTCLASSCODES>> GetAllFreightClassCodesAsync()
        {
            try
            {
                return await _context.ClassCodes.Find(_ => true).SortBy(s => s.CodeNumber).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllClassCodesAsync");
                throw;
            }

        }

        public async Task<List<FREIGHTCLASSCODES>> GetAvailableFreightClassCodesAsync()
        {
            return await _context.ClassCodes
                .Find(code => code.Enabled == true && code.IsDeleted != true)
                .SortBy(code => code.CodeNumber)
                .ToListAsync();
        }

        public async Task<FREIGHTCLASSCODES?> GetOneFreightClassCodeAsync(string freightClassCodeId)
        {
            try
            {
                return await _context.ClassCodes.Find(classCode => classCode.FreightClassCodeId == freightClassCodeId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneClassCodeAsync");
                throw;
            }
        }

        public async Task AddFreightClassCodeAsync(FREIGHTCLASSCODES freightClassCode)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(freightClassCode);
                freightClassCode.FreightClassCodeId = string.IsNullOrWhiteSpace(freightClassCode.FreightClassCodeId)
                    ? ObjectId.GenerateNewId().ToString()
                    : freightClassCode.FreightClassCodeId;
                freightClassCode.CreatedOnUtc = freightClassCode.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : freightClassCode.CreatedOnUtc;
                freightClassCode.UpdatedOnUtc = DateTime.UtcNow;
                await _context.ClassCodes.InsertOneAsync(freightClassCode);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddClassCodeAsync");
                throw;
            }

        }        

        public async Task<bool> UpdateFreightClassCodeAsync(FREIGHTCLASSCODES freightClassCode, string freightClassCodeId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(freightClassCode);
                var filter = Builders<FREIGHTCLASSCODES>.Filter.Eq(s => s.FreightClassCodeId, freightClassCodeId);
                var existing = await _context.ClassCodes.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                freightClassCode._id = existing._id;
                freightClassCode.FreightClassCodeId = existing.FreightClassCodeId;
                freightClassCode.CreatedOnUtc = existing.CreatedOnUtc;
                freightClassCode.UpdatedOnUtc = DateTime.UtcNow;
                ReplaceOneResult actionResult = await _context.ClassCodes
                    .ReplaceOneAsync(filter, freightClassCode);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateClassCodeAsync");
                throw;
            }
        }

        public async Task<bool> RemoveFreightClassCodeAsync(string freightClassCodeId)
        {
            try
            {
                var filter = Builders<FREIGHTCLASSCODES>.Filter.Eq(
                    code => code.FreightClassCodeId, freightClassCodeId);
                var update = Builders<FREIGHTCLASSCODES>.Update
                    .Set(code => code.IsDeleted, true)
                    .Set(code => code.Enabled, false)
                    .Set(code => code.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.ClassCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveClassCodeAsync");
                throw;
            }
        }

        #endregion
        #region EnableDisable

        public async Task<bool> EnableFreightClassCodeAsync(string freightClassCodeId)
        {
            try
            {
                var filter = Builders<FREIGHTCLASSCODES>.Filter.Eq(s => s.FreightClassCodeId, freightClassCodeId);
                var update = Builders<FREIGHTCLASSCODES>.Update
                    .Set(s => s.Enabled, true)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.ClassCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_EnableFreightClassCode");
                throw;
            }
        }

        public async Task<bool> DisableFreightClassCodeAsync(string freightClassCodeId)
        {
            try
            {
                var filter = Builders<FREIGHTCLASSCODES>.Filter.Eq(s => s.FreightClassCodeId, freightClassCodeId);
                var update = Builders<FREIGHTCLASSCODES>.Update
                    .Set(s => s.Enabled, false)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);

                UpdateResult actionResult
                    = await _context.ClassCodes.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_DisableFreightClassCode");
                throw;
            }
        }

        #endregion
        #region Calculators

        public async Task<FREIGHTCLASSCODES?> MatchCorrectCodeByLinearDensity(double linearDensity)
        {
            if (linearDensity < 0)
                throw new ArgumentOutOfRangeException(nameof(linearDensity));

            var filter = Builders<FREIGHTCLASSCODES>.Filter.And(
                Builders<FREIGHTCLASSCODES>.Filter.Eq(code => code.Enabled, true),
                Builders<FREIGHTCLASSCODES>.Filter.Ne(code => code.IsDeleted, true),
                Builders<FREIGHTCLASSCODES>.Filter.Lte(code => code.MinWeightPerfoot, linearDensity),
                Builders<FREIGHTCLASSCODES>.Filter.Or(
                    Builders<FREIGHTCLASSCODES>.Filter.Gt(code => code.MaxWeightPerFoot, linearDensity),
                    Builders<FREIGHTCLASSCODES>.Filter.Eq(code => code.MaxWeightPerFoot, null)));
            return await _context.ClassCodes.Find(filter)
                .SortByDescending(code => code.MinWeightPerfoot)
                .FirstOrDefaultAsync();

        }

        #endregion


    }
}
