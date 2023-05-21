using Microsoft.EntityFrameworkCore;
using WebApiPractice.DbContexts;
using WebApiPractice.Entities;

namespace WebApiPractice.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context) // we need to inject the context into this class with DI
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            var test = await _context.Cities.ToListAsync();
            return test;
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if(includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest).FirstOrDefaultAsync(c => c.Id == cityId); // includes the points of interest for the city. Might need to be setup for lazy loading?
            }
            else
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        // this one is checking if the city exists,
        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int CityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest.FirstOrDefaultAsync(p => p.CityId == CityId && p.Id == pointOfInterestId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest.Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int CityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(CityId, false);
            city?.PointsOfInterest.Add(pointOfInterest);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0); // returns true if 1 or more entities were changed
        }

    }
}
