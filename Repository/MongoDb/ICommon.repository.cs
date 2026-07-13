using SimpleBol.Models.MongoDb;
using SimpleBol.Context.MongoDb;
using MongoDB.Driver;

namespace SimpleBol.Repository.MongoDb
{
    public interface ICommonRepository
    {
        
        Task<List<COUNTRIES>> GetAllCountriesAsync();
        Task<List<REGIONS>> GetAllRegionsByCountryAsync(string countryId);
        Task<COUNTRIES?> GetCountryAsync(string countryId);
        Task<REGIONS?> GetRegionAsync(string regionId);
    }

    public class CommonRepository : ICommonRepository
    {

        private readonly MongoDbContext _context;

        public CommonRepository(MongoDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<COUNTRIES>> GetAllCountriesAsync()
        {
            return await _context.Countries
                .Find(country => country.Enabled == true)
                .SortBy(country => country.LongName)
                .ToListAsync();
        }

        public async Task<List<REGIONS>> GetAllRegionsByCountryAsync(string countryId)
        {

            if (string.IsNullOrWhiteSpace(countryId))
                return new List<REGIONS>();

            return await _context.Regions
                .Find(region => region.CountryId == countryId && region.Enabled)
                .SortBy(region => region.LongName)
                .ToListAsync();

        }

        public async Task<COUNTRIES?> GetCountryAsync(string countryId)
        {
            if (string.IsNullOrWhiteSpace(countryId))
                return null;

            return await _context.Countries
                .Find(country => country.CountryId == countryId && country.Enabled == true)
                .FirstOrDefaultAsync();
        }

        public async Task<REGIONS?> GetRegionAsync(string regionId)
        {
            if (string.IsNullOrWhiteSpace(regionId))
                return null;

            return await _context.Regions
                .Find(region => region.RegionId == regionId && region.Enabled)
                .FirstOrDefaultAsync();
        }

    }
}
