using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IBolsRepository
    {
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsAsync();
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsPaginatedAsync(int page, int show);
        Task<BILLOFLADINGS?> GetOneBillOfLaddingAsync(string? bolId);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsForTodayPaginatedAsync(DateTime shipDate, int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromLast7DaysPaginatedAsync(DateTime shipDate, int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromThisWeekPaginatedAsync(int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromLastWeekPaginatedAsync(int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsUpdateRateAsync(int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsAuditRateAsync(int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipDateAsync(DateTime shipDate);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipDatePaginatedAsync(DateTime shipDate, int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperAsync(string shipperId);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperForLast90DaysAsync(string shipperId);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperPaginatedAsync(string shipperId, int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByVendorAsync(string vendorId);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByVendorPaginatedAsync(string vendorId, int page, int show);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByCustomerAsync(string customerId);
        Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByCustomerPaginatedAsync(string customerId, int page, int show);
        Task<bool> AddBillOfLaddingAsync(BILLOFLADINGS bol);
        Task<bool> UpdateBillOfLaddingAsync(BILLOFLADINGS bol, string bolId);
        Task<bool> UpdateBillOfLaddingDisputedFlagAsync(string bolId, bool state);
        Task<bool> RemoveBillOfLaddingAsync(string bolId);
    }

    public class BolsRepository : IBolsRepository
    {
        private readonly MongoDbContext _context;
        private static readonly FilterDefinition<BILLOFLADINGS> ActiveFilter =
            Builders<BILLOFLADINGS>.Filter.Ne(bol => bol.MarkAsDeleted, true);
        private static readonly SortDefinition<BILLOFLADINGS> LatestFirst =
            Builders<BILLOFLADINGS>.Sort
                .Descending(bol => bol.ShipDate)
                .Descending(bol => bol.BolId);

        public BolsRepository(MongoDbContext context)
        {
            _context = context;
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsAsync() =>
            FindAsync(ActiveFilter);

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsPaginatedAsync(int page, int show) =>
            FindPageAsync(ActiveFilter, page, show);

        public async Task<BILLOFLADINGS?> GetOneBillOfLaddingAsync(string? bolId)
        {
            if (string.IsNullOrWhiteSpace(bolId))
                return null;

            try
            {
                var filter = Builders<BILLOFLADINGS>.Filter.And(
                    ActiveFilter,
                    Builders<BILLOFLADINGS>.Filter.Eq(bol => bol.BolId, bolId));
                return await _context.BillOfLadings.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_GetOneBillOfLaddingAsync");
                throw;
            }
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsForTodayPaginatedAsync(
            DateTime shipDate, int page, int show)
        {
            var (startUtc, endUtc) = GetLocalDayUtcRange(shipDate);
            return FindPageAsync(DateRangeFilter(startUtc, endUtc), page, show);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromLast7DaysPaginatedAsync(
            DateTime shipDate, int page, int show)
        {
            var (_, endUtc) = GetLocalDayUtcRange(shipDate);
            var (startUtc, _) = GetLocalDayUtcRange(shipDate.Date.AddDays(-6));
            return FindPageAsync(DateRangeFilter(startUtc, endUtc), page, show);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromThisWeekPaginatedAsync(
            int page, int show)
        {
            var today = DateTime.Today;
            var daysSinceMonday = ((int)today.DayOfWeek + 6) % 7;
            var monday = today.AddDays(-daysSinceMonday);
            return FindPageAsync(
                DateRangeFilter(LocalToUtc(monday), LocalToUtc(monday.AddDays(7))),
                page, show);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsFromLastWeekPaginatedAsync(
            int page, int show)
        {
            var today = DateTime.Today;
            var daysSinceMonday = ((int)today.DayOfWeek + 6) % 7;
            var thisMonday = today.AddDays(-daysSinceMonday);
            var lastMonday = thisMonday.AddDays(-7);
            return FindPageAsync(
                DateRangeFilter(LocalToUtc(lastMonday), LocalToUtc(thisMonday)),
                page, show);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsUpdateRateAsync(int page, int show)
        {
            var filter = Builders<BILLOFLADINGS>.Filter.And(
                ActiveFilter,
                Builders<BILLOFLADINGS>.Filter.Gt(bol => bol.EstimatedShippingPrice, 0m),
                Builders<BILLOFLADINGS>.Filter.Lte(bol => bol.ActualShippingPrice, 0m));
            return FindPageAsync(filter, page, show);
        }

        public async Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsAuditRateAsync(int page, int show)
        {
            try
            {
                var filter = Builders<BILLOFLADINGS>.Filter.And(
                    ActiveFilter,
                    Builders<BILLOFLADINGS>.Filter.Where(
                        bol => bol.ShipperActualPrice > bol.ShipperQuotePrice));
                return await FindPageAsync(filter, page, show);
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_GetAllBillOfLaddingsAuditRateAsync");
                throw;
            }
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipDateAsync(DateTime shipDate)
        {
            var (startUtc, endUtc) = GetLocalDayUtcRange(shipDate);
            return FindAsync(DateRangeFilter(startUtc, endUtc));
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipDatePaginatedAsync(
            DateTime shipDate, int page, int show)
        {
            var (startUtc, endUtc) = GetLocalDayUtcRange(shipDate);
            return FindPageAsync(DateRangeFilter(startUtc, endUtc), page, show);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperAsync(string shipperId) =>
            FindAsync(WithActive(bol => bol.ShipperId == shipperId));

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperForLast90DaysAsync(
            string shipperId)
        {
            var filter = Builders<BILLOFLADINGS>.Filter.And(
                ActiveFilter,
                Builders<BILLOFLADINGS>.Filter.Eq(bol => bol.ShipperId, shipperId),
                Builders<BILLOFLADINGS>.Filter.Gte(bol => bol.ShipDate, DateTime.UtcNow.AddDays(-90)));
            return FindAsync(filter);
        }

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByShipperPaginatedAsync(
            string shipperId, int page, int show) =>
            FindPageAsync(WithActive(bol => bol.ShipperId == shipperId), page, show);

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByVendorAsync(string vendorId) =>
            FindAsync(WithActive(bol => bol.ShipFromId == vendorId));

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByVendorPaginatedAsync(
            string vendorId, int page, int show) =>
            FindPageAsync(WithActive(bol => bol.ShipFromId == vendorId), page, show);

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByCustomerAsync(string customerId) =>
            FindAsync(WithActive(bol => bol.ShipToId == customerId));

        public Task<List<BILLOFLADINGS>> GetAllBillOfLaddingsByCustomerPaginatedAsync(
            string customerId, int page, int show) =>
            FindPageAsync(WithActive(bol => bol.ShipToId == customerId), page, show);

        public async Task<bool> AddBillOfLaddingAsync(BILLOFLADINGS bol)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(bol);
                await EnrichEmbeddedAddressesAsync(bol);
                bol.BolId = string.IsNullOrWhiteSpace(bol.BolId)
                    ? ObjectId.GenerateNewId().ToString()
                    : bol.BolId;
                bol.CreatedOnUtc = bol.CreatedOnUtc == default ? DateTime.UtcNow : bol.CreatedOnUtc;
                bol.UpdatedOnUtc = DateTime.UtcNow;
                await _context.BillOfLadings.InsertOneAsync(bol);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_AddBillOfLaddingAsync");
                throw;
            }
        }

        public async Task<bool> UpdateBillOfLaddingAsync(BILLOFLADINGS bol, string bolId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(bol);
                await EnrichEmbeddedAddressesAsync(bol);
                var filter = Builders<BILLOFLADINGS>.Filter.Eq(item => item.BolId, bolId);
                var existing = await _context.BillOfLadings.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                bol._id = existing._id;
                bol.BolId = existing.BolId;
                bol.CreatedOnUtc = existing.CreatedOnUtc;
                bol.UpdatedOnUtc = DateTime.UtcNow;

                var result = await _context.BillOfLadings.ReplaceOneAsync(filter, bol);
                return result.IsAcknowledged && result.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_UpdateBillOfLaddingAsync");
                throw;
            }
        }

        public async Task<bool> UpdateBillOfLaddingDisputedFlagAsync(string bolId, bool state)
        {
            try
            {
                var filter = Builders<BILLOFLADINGS>.Filter.Eq(bol => bol.BolId, bolId);
                var update = Builders<BILLOFLADINGS>.Update
                    .Set(bol => bol.Disputed, state)
                    .Set(bol => bol.UpdatedOnUtc, DateTime.UtcNow);
                var result = await _context.BillOfLadings.UpdateOneAsync(filter, update);
                return result.IsAcknowledged && result.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_UpdateBillOfLaddingDisputedFlagAsync");
                throw;
            }
        }

        public async Task<bool> RemoveBillOfLaddingAsync(string bolId)
        {
            try
            {
                var filter = Builders<BILLOFLADINGS>.Filter.Eq(bol => bol.BolId, bolId);
                var update = Builders<BILLOFLADINGS>.Update
                    .Set(bol => bol.MarkAsDeleted, true)
                    .Set(bol => bol.UpdatedOnUtc, DateTime.UtcNow);
                var result = await _context.BillOfLadings.UpdateOneAsync(filter, update);
                return result.IsAcknowledged && result.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                ErrorLogging.NLogException(ex, "MongoDb_RemoveBillOfLaddingAsync");
                throw;
            }
        }

        private Task<List<BILLOFLADINGS>> FindAsync(FilterDefinition<BILLOFLADINGS> filter) =>
            _context.BillOfLadings.Find(filter).Sort(LatestFirst).ToListAsync();

        private Task<List<BILLOFLADINGS>> FindPageAsync(
            FilterDefinition<BILLOFLADINGS> filter, int page, int show)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "Page must be at least 1.");
            if (show < 1)
                throw new ArgumentOutOfRangeException(nameof(show), "Page size must be at least 1.");

            return _context.BillOfLadings.Find(filter)
                .Sort(LatestFirst)
                .Skip((page - 1) * show)
                .Limit(show)
                .ToListAsync();
        }

        private static FilterDefinition<BILLOFLADINGS> WithActive(
            System.Linq.Expressions.Expression<Func<BILLOFLADINGS, bool>> predicate) =>
            Builders<BILLOFLADINGS>.Filter.And(
                ActiveFilter,
                Builders<BILLOFLADINGS>.Filter.Where(predicate));

        private static FilterDefinition<BILLOFLADINGS> DateRangeFilter(
            DateTime startUtc, DateTime endUtc) =>
            Builders<BILLOFLADINGS>.Filter.And(
                ActiveFilter,
                Builders<BILLOFLADINGS>.Filter.Gte(bol => bol.ShipDate, startUtc),
                Builders<BILLOFLADINGS>.Filter.Lt(bol => bol.ShipDate, endUtc));

        private static (DateTime StartUtc, DateTime EndUtc) GetLocalDayUtcRange(DateTime date)
        {
            var localStart = DateTime.SpecifyKind(date.Date, DateTimeKind.Local);
            return (localStart.ToUniversalTime(), localStart.AddDays(1).ToUniversalTime());
        }

        private static DateTime LocalToUtc(DateTime localDate) =>
            DateTime.SpecifyKind(localDate, DateTimeKind.Local).ToUniversalTime();

        private async Task EnrichEmbeddedAddressesAsync(BILLOFLADINGS bol)
        {
            await EnrichLocationAsync(bol.ShipFromLocation);
            await EnrichLocationAsync(bol.ShipToLocation);

            if (bol.ShipFromVendor is not null)
            {
                var (countryName, countryAbbr, regionName, regionAbbr) = await GetLocationNamesAsync(
                    bol.ShipFromVendor.CountryCode, bol.ShipFromVendor.RegionCode);
                bol.ShipFromVendor.CountryLongName = countryName;
                bol.ShipFromVendor.CountryShortName = countryAbbr;
                bol.ShipFromVendor.RegionLongName = regionName;
                bol.ShipFromVendor.RegionShortName = regionAbbr;
            }

            if (bol.ShipToCustomer is not null)
            {
                var (countryName, countryAbbr, regionName, regionAbbr) = await GetLocationNamesAsync(
                    bol.ShipToCustomer.CountryCode, bol.ShipToCustomer.RegionCode);
                bol.ShipToCustomer.CountryLongName = countryName;
                bol.ShipToCustomer.CountryAbbr = countryAbbr;
                bol.ShipToCustomer.RegionLongName = regionName;
                bol.ShipToCustomer.RegionAbbr = regionAbbr;
            }

            if (bol.BillToAccount is not null)
            {
                var (countryName, countryAbbr, regionName, regionAbbr) = await GetLocationNamesAsync(
                    bol.BillToAccount.CountryCode, bol.BillToAccount.RegionCode);
                bol.BillToAccount.CountryLongName = countryName;
                bol.BillToAccount.CountryAbbr = countryAbbr;
                bol.BillToAccount.RegionLongName = regionName;
                bol.BillToAccount.RegionAbbr = regionAbbr;
            }
        }

        private async Task EnrichLocationAsync(SHIPPINGLOCATIONS? location)
        {
            if (location is null)
                return;

            var (countryName, countryAbbr, regionName, regionAbbr) = await GetLocationNamesAsync(
                location.CountryCode, location.RegionCode);
            location.CountryName = countryName;
            location.CountryAbbr = countryAbbr;
            location.RegionName = regionName;
            location.RegionAbbr = regionAbbr;
        }

        private async Task<(string? CountryName, string? CountryAbbr, string? RegionName, string? RegionAbbr)>
            GetLocationNamesAsync(string? countryCode, string? regionCode)
        {
            var country = string.IsNullOrWhiteSpace(countryCode)
                ? null
                : await _context.Countries.Find(item => item.CountryId == countryCode).FirstOrDefaultAsync();
            var region = string.IsNullOrWhiteSpace(regionCode)
                ? null
                : await _context.Regions.Find(item => item.RegionId == regionCode).FirstOrDefaultAsync();

            return (country?.LongName, country?.ShortName, region?.LongName, region?.ShortName);
        }
    }
}
