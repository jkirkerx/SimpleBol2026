using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface IShipperRepository
    {
        Task<List<SHIPPER>> GetAllShippersAsync();
        Task<List<SHIPPER>> GetShippersByServiceCodeAsync(ShipperServicesEnum? serviceCode);
        Task<List<SHIPPER>> GetShippersByDestinationCountryAsync(string? countryCode);
        Task<SHIPPER?> GetOneShipperAsync(string shipperId);
        Task<List<ShipperContacts>> GetShipperContactsList(string shipperId);
        Task AddShipperAsync(SHIPPER model);
        Task<bool> UpdateShipperAsync(SHIPPER shipper, string shipperId);
        Task<bool> UpdateShipperContactsAsync(List<ShipperContacts> contacts, string shipperId);
        Task<bool> RemoveShipperAsync(string shipperId);

    }

    public class ShipperRepository: IShipperRepository
    {
        private readonly MongoDbContext _context;

        public ShipperRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<SHIPPER>> GetAllShippersAsync()
        {
            return await _context.Shippers
                .Find(shipper => shipper.MarkAsDeleted != true)
                .SortBy(shipper => shipper.CompanyName)
                .ToListAsync();
        }

        public async Task<List<SHIPPER>> GetShippersByServiceCodeAsync(ShipperServicesEnum? serviceCode)
        {
            var active = Builders<SHIPPER>.Filter.Ne(shipper => shipper.MarkAsDeleted, true);
            var services = Builders<SHIPPER>.Filter;
            FilterDefinition<SHIPPER> serviceFilter;

            switch (serviceCode)
            {
                case ShipperServicesEnum.LTL:
                    serviceFilter = services.Eq("ShipperServices.ServiceLTL", true);
                    break;

                case ShipperServicesEnum.FTL:
                    serviceFilter = services.Eq("ShipperServices.ServiceFTL", true);
                    break;
                    

                case ShipperServicesEnum.AIR:
                    serviceFilter = services.Eq("ShipperServices.ServiceAirplane", true);
                    break;
                    

                case ShipperServicesEnum.OCEAN:
                    serviceFilter = services.Eq("ShipperServices.ServiceOcean", true);
                    break;
                    

                case ShipperServicesEnum.RAIL:
                    serviceFilter = services.Eq("ShipperServices.ServiceRailroad", true);
                    break;
                    

                case ShipperServicesEnum.LASTMILE:
                    serviceFilter = services.Eq("ShipperServices.ServiceLastMile", true);
                    break;
                    

                case ShipperServicesEnum.COURIER:
                    serviceFilter = services.Eq("ShipperServices.ServiceCourier", true);
                    break;
                    

                case ShipperServicesEnum.ARMOURED:
                    serviceFilter = services.Eq("ShipperServices.ServiceArmouredCar", true);
                    break;

                case ShipperServicesEnum.SHOWALL:
                default:
                    return await _context.Shippers.Find(active)
                        .SortBy(shipper => shipper.CompanyName).ToListAsync();
            }

            return await _context.Shippers
                .Find(Builders<SHIPPER>.Filter.And(active, serviceFilter))
                .SortBy(shipper => shipper.CompanyName)
                .ToListAsync();

        }

        public async Task<List<SHIPPER>> GetShippersByDestinationCountryAsync(string? countryCode)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(countryCode))
                    return new List<SHIPPER>();

                var filter = Builders<SHIPPER>.Filter.And(
                    Builders<SHIPPER>.Filter.Ne(shipper => shipper.MarkAsDeleted, true),
                    Builders<SHIPPER>.Filter.Eq("ServiceCountries.CountryCode", countryCode));
                var query = _context.Shippers.Find(filter).SortBy(s => s.CompanyName).ToListAsync();
                return await query;
                
            }            
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetShippersByDestinationCountryAsync");
                throw;
            }

        }

        // query after body text, updated time, and header image size
        //
        public async Task<SHIPPER?> GetOneShipperAsync(string shipperId)
        {
            try
            {
                var shippers = _context.Shippers;
                var shipper = await shippers
                    .Find(shipper => shipper.ShipperId == shipperId && shipper.MarkAsDeleted != true)
                    .FirstOrDefaultAsync();
                if (shipper != null)
                {
                    // Double check that we filled in the Long Names for the shipper                    
                    var region = await _context.Regions.Find(region => region.RegionId == shipper.RegionCode).FirstOrDefaultAsync();
                    if (region is not null)
                    {
                        shipper.RegionShortName = region.ShortName;
                        shipper.RegionLongName = region.LongName;
                    }
                    
                    var country = await _context.Countries.Find(country => country.CountryId == shipper.CountryCode).FirstOrDefaultAsync();
                    if (country is not null)
                    {
                        shipper.CountryShortName = country.ShortName;
                        shipper.CountryLongName = country.LongName;
                    }

                    return shipper;
                }

                return null;
                
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneShipper");
                throw;
            }
        }

        public async Task<List<ShipperContacts>> GetShipperContactsList(string shipperId)
        {
            var filter1 = Builders<SHIPPER>.Filter.Eq(s => s.ShipperId, shipperId);
            var shipper = await _context.Shippers.Find(filter1).FirstOrDefaultAsync();

            if (shipper?.ShipperContacts != null)
            {
                return shipper.ShipperContacts;
            }

            // Return an empty list or null, depending on your use case
            return new List<ShipperContacts>(); // Or return null;
        }


        public async Task AddShipperAsync(SHIPPER shipper)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(shipper);
                await EnrichLocationAsync(shipper);
                shipper.ShipperId = string.IsNullOrWhiteSpace(shipper.ShipperId)
                    ? ObjectId.GenerateNewId().ToString()
                    : shipper.ShipperId;
                shipper.CreatedOnUtc = shipper.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : shipper.CreatedOnUtc;
                shipper.UpdatedOnUtc = DateTime.UtcNow;

                await _context.Shippers.InsertOneAsync(shipper);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddShipperAsync");
                throw;
            }

        }

        public async Task<bool> UpdateShipperAsync(SHIPPER shipper, string shipperId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(shipper);
                await EnrichLocationAsync(shipper);
                var filter = Builders<SHIPPER>.Filter.Eq(s => s.ShipperId, shipperId);
                var existing = await _context.Shippers.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                shipper._id = existing._id;
                shipper.ShipperId = existing.ShipperId;
                shipper.CreatedOnUtc = existing.CreatedOnUtc;
                shipper.UpdatedOnUtc = DateTime.UtcNow;
                ReplaceOneResult actionResult = await _context.Shippers.ReplaceOneAsync(filter, shipper);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateShipperAsync");
                throw;
            }
        }

        public async Task<bool> UpdateShipperContactsAsync(List<ShipperContacts> contacts, string shipperId)
        {
            try
            {
                var filter = Builders<SHIPPER>.Filter.Eq(s => s.ShipperId, shipperId);
                var update = Builders<SHIPPER>.Update
                    .Set(s => s.ShipperContacts, contacts)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow);
                    

                UpdateResult actionResult
                    = await _context.Shippers.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateShipperContactsAsync");
                throw;
            }


        }

        public async Task<bool> RemoveShipperAsync(string shipperId)
        {
            try
            {
                var filter = Builders<SHIPPER>.Filter.Eq(shipper => shipper.ShipperId, shipperId);
                var update = Builders<SHIPPER>.Update
                    .Set(shipper => shipper.MarkAsDeleted, true)
                    .Set(shipper => shipper.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.Shippers.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveShipperAsync");
                throw;
            }
        }

        private async Task EnrichLocationAsync(SHIPPER shipper)
        {
            var region = string.IsNullOrWhiteSpace(shipper.RegionCode)
                ? null
                : await _context.Regions.Find(item => item.RegionId == shipper.RegionCode)
                    .FirstOrDefaultAsync();
            if (region is not null)
            {
                shipper.RegionShortName = region.ShortName;
                shipper.RegionLongName = region.LongName;
            }

            var country = string.IsNullOrWhiteSpace(shipper.CountryCode)
                ? null
                : await _context.Countries.Find(item => item.CountryId == shipper.CountryCode)
                    .FirstOrDefaultAsync();
            if (country is not null)
            {
                shipper.CountryShortName = country.ShortName;
                shipper.CountryLongName = country.LongName;
            }
        }


    }
}
