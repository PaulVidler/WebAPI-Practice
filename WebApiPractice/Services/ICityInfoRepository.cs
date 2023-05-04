using WebApiPractice.Entities;

namespace WebApiPractice.Services
{
    public interface ICityInfoRepository
    {
         //IQueryable<City> GetCities(); // IQueryable is used to allow for deferred execution of the query so you can add to it as needed by using logic to select what you want to filter etc
        Task<IEnumerable<City>> GetCitiesAsync(); // using this because it is a fairly straight forward method, no need yet for fancy filtering etc.

        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest); //  we make this nullable as it's possible to request a city that doesn't exist

        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);

        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int CityId, int pointOfInterestId);

    }
}
