using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Repository.MongoDb
{
    public interface ICustomerRepository
    {

        Task<List<CUSTOMERS>> GetAllCustomersAsync();
        Task<CUSTOMERS?> GetOneCustomerAsync(string customerId);
        Task AddCustomerAsync(CUSTOMERS customer);
        Task<bool> UpdateCustomerAsync(CUSTOMERS customer, string customerId);
        Task<bool> RemoveCustomerAsync(string customerId);
        Task<SHIPPINGLOCATIONS?> GetCustomerShippingLocationByLocationId(string? customerId, string? locationId);

    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly MongoDbContext _context;

        public CustomerRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<CUSTOMERS>> GetAllCustomersAsync()
        {
            try
            {
                return await _context.Customers
                    .Find(customer => !customer.MarkAsDeleted)
                    .SortBy(customer => customer.CompanyName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllCustomersAsync");
                throw;
            }

            
        }

        public async Task<CUSTOMERS?> GetOneCustomerAsync(string customerId)
        {
            try
            {
                return await _context.Customers
                    .Find(customer => customer.CustomerId == customerId && !customer.MarkAsDeleted)
                    .FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneCustomerAsync");
                throw;
            }
        }

        public async Task AddCustomerAsync(CUSTOMERS customer)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(customer);
                await EnrichLocationsAsync(customer);
                customer.CustomerId = string.IsNullOrWhiteSpace(customer.CustomerId)
                    ? ObjectId.GenerateNewId().ToString()
                    : customer.CustomerId;
                customer.CreatedOnUtc = customer.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : customer.CreatedOnUtc;
                customer.UpdatedOnUtc = DateTime.UtcNow;
                await _context.Customers.InsertOneAsync(customer);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddCustomerAsync");
                throw;
            }

        }

        public async Task<bool> UpdateCustomerAsync(CUSTOMERS customer, string customerId)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(customer);
                await EnrichLocationsAsync(customer);
                var filter = Builders<CUSTOMERS>.Filter.Eq(s => s.CustomerId, customerId);
                var existing = await _context.Customers.Find(filter).FirstOrDefaultAsync();
                if (existing is null)
                    return false;

                customer.Id = existing.Id;
                customer.CustomerId = existing.CustomerId;
                customer.CreatedOnUtc = existing.CreatedOnUtc;
                customer.UpdatedOnUtc = DateTime.UtcNow;
                ReplaceOneResult actionResult = await _context.Customers
                    .ReplaceOneAsync(filter, customer);

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateCustomerAsync");
                throw;
            }
        }

        public async Task<bool> RemoveCustomerAsync(string customerId)
        {
            try
            {
                var filter = Builders<CUSTOMERS>.Filter.Eq(
                    customer => customer.CustomerId, customerId);
                var update = Builders<CUSTOMERS>.Update
                    .Set(customer => customer.MarkAsDeleted, true)
                    .Set(customer => customer.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.Customers.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveCustomerAsync");
                throw;
            }
        }

        public async Task<SHIPPINGLOCATIONS?> GetCustomerShippingLocationByLocationId(string? customerId, string? locationId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerId) || string.IsNullOrWhiteSpace(locationId))
                    return null;

                var customer = await _context.Customers
                    .Find(item => item.CustomerId == customerId && !item.MarkAsDeleted)
                    .FirstOrDefaultAsync();
                return customer?.ShippingLocations?
                    .FirstOrDefault(location =>
                        location.LocationId == locationId && location.MarkAsDeleted != true);

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetCustomerShippingLocationByLocationId");
                throw;
            }

        }

        private async Task EnrichLocationsAsync(CUSTOMERS customer)
        {
            var country = string.IsNullOrWhiteSpace(customer.CountryCode)
                ? null
                : await _context.Countries
                    .Find(item => item.CountryId == customer.CountryCode)
                    .FirstOrDefaultAsync();
            if (country is not null)
            {
                customer.CountryAbbr = country.ShortName;
                customer.CountryLongName = country.LongName;
            }

            var region = string.IsNullOrWhiteSpace(customer.RegionCode)
                ? null
                : await _context.Regions
                    .Find(item => item.RegionId == customer.RegionCode)
                    .FirstOrDefaultAsync();
            if (region is not null)
            {
                customer.RegionAbbr = region.ShortName;
                customer.RegionLongName = region.LongName;
            }

            if (customer.ShippingLocations is null)
                return;

            foreach (var location in customer.ShippingLocations)
            {
                var locationCountry = string.IsNullOrWhiteSpace(location.CountryCode)
                    ? null
                    : await _context.Countries
                        .Find(item => item.CountryId == location.CountryCode)
                        .FirstOrDefaultAsync();
                if (locationCountry is not null)
                {
                    location.CountryAbbr = locationCountry.ShortName;
                    location.CountryName = locationCountry.LongName;
                }

                var locationRegion = string.IsNullOrWhiteSpace(location.RegionCode)
                    ? null
                    : await _context.Regions
                        .Find(item => item.RegionId == location.RegionCode)
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
