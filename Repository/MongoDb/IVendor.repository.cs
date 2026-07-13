using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IVendorRepository
    {
        Task<List<VENDORS>> GetAllVendorsAsync();
        Task<VENDORS?> GetOneVendorAsync(string vendorId);
        Task AddVendorAsync(VENDORS vendor);
        Task<bool> UpdateVendorAsync(VENDORS vendor, string vendorId);
        Task<bool> RemoveVendorAsync(string vendorId);
        Task<SHIPPINGLOCATIONS?> GetVendorShippingLocationByLocationId(string? vendorId, string? locationId);
    }

    public class VendorRepository : IVendorRepository
    {

        private readonly MongoDbContext _context;

        public VendorRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<VENDORS>> GetAllVendorsAsync()
        {
            try
            {
                return await _context.Vendors
                    .Find(vendor => vendor.MarkAsDeleted != true)
                    .SortBy(vendor => vendor.CompanyName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllVendorsAsync");
                throw;
            }
            
        }

        public async Task<VENDORS?> GetOneVendorAsync(string vendorId)
        {
            try
            {
                return await _context.Vendors
                    .Find(vendor => vendor.VendorId == vendorId && vendor.MarkAsDeleted != true)
                    .FirstOrDefaultAsync();                

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneVendorAsync");
                throw;
            }
        }

        public async Task AddVendorAsync(VENDORS vendor)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(vendor);
                await EnrichLocationsAsync(vendor);
                vendor.VendorId = string.IsNullOrWhiteSpace(vendor.VendorId)
                    ? ObjectId.GenerateNewId().ToString()
                    : vendor.VendorId;
                vendor.CreatedOnUtc = vendor.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : vendor.CreatedOnUtc;
                vendor.UpdatedOnUtc = DateTime.UtcNow;
                await _context.Vendors.InsertOneAsync(vendor);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddVendorAsync");
                throw;
            }

        }

        public async Task<bool> UpdateVendorAsync(VENDORS vendor, string vendorId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(vendor);
                await EnrichLocationsAsync(vendor);
                var filter = Builders<VENDORS>.Filter.Eq(s => s.VendorId, vendorId);
                var existing = await _context.Vendors.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                vendor.Id = existing.Id;
                vendor.VendorId = existing.VendorId;
                vendor.CreatedOnUtc = existing.CreatedOnUtc;
                vendor.UpdatedOnUtc = DateTime.UtcNow;
                ReplaceOneResult actionResult = await _context.Vendors
                    .ReplaceOneAsync(filter, vendor);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateVendorAsync");
                throw;
            }
        }

        public async Task<bool> RemoveVendorAsync(string vendorId)
        {
            try
            {
                var filter = Builders<VENDORS>.Filter.Eq(vendor => vendor.VendorId, vendorId);
                var update = Builders<VENDORS>.Update
                    .Set(vendor => vendor.MarkAsDeleted, true)
                    .Set(vendor => vendor.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.Vendors.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveVendorAsync");
                throw;
            }
        }

        public async Task<SHIPPINGLOCATIONS?> GetVendorShippingLocationByLocationId(string? vendorId, string? locationId)
        {
            try
            {                
                if (string.IsNullOrWhiteSpace(vendorId) || string.IsNullOrWhiteSpace(locationId))
                    return null;

                var vendor = await _context.Vendors
                    .Find(item => item.VendorId == vendorId && item.MarkAsDeleted != true)
                    .FirstOrDefaultAsync();
                return vendor?.ShippingLocations?
                    .FirstOrDefault(location =>
                        location.LocationId == locationId && location.MarkAsDeleted != true);

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetVendorShippingLocationByLocationId");
                throw;
            }

        }

        private async Task EnrichLocationsAsync(VENDORS vendor)
        {
            var country = string.IsNullOrWhiteSpace(vendor.CountryCode)
                ? null
                : await _context.Countries.Find(item => item.CountryId == vendor.CountryCode)
                    .FirstOrDefaultAsync();
            if (country is not null)
            {
                vendor.CountryShortName = country.ShortName;
                vendor.CountryLongName = country.LongName;
            }

            var region = string.IsNullOrWhiteSpace(vendor.RegionCode)
                ? null
                : await _context.Regions.Find(item => item.RegionId == vendor.RegionCode)
                    .FirstOrDefaultAsync();
            if (region is not null)
            {
                vendor.RegionShortName = region.ShortName;
                vendor.RegionLongName = region.LongName;
            }

            if (vendor.ShippingLocations is null)
                return;

            foreach (var location in vendor.ShippingLocations)
            {
                var locationCountry = string.IsNullOrWhiteSpace(location.CountryCode)
                    ? null
                    : await _context.Countries.Find(item => item.CountryId == location.CountryCode)
                        .FirstOrDefaultAsync();
                if (locationCountry is not null)
                {
                    location.CountryAbbr = locationCountry.ShortName;
                    location.CountryName = locationCountry.LongName;
                }

                var locationRegion = string.IsNullOrWhiteSpace(location.RegionCode)
                    ? null
                    : await _context.Regions.Find(item => item.RegionId == location.RegionCode)
                        .FirstOrDefaultAsync();
                if (locationRegion is not null)
                {
                    location.RegionAbbr = locationRegion.ShortName;
                    location.RegionName = locationRegion.LongName;
                }
            }
        }

    }
}
