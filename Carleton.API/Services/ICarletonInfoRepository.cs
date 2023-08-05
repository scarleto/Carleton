using Carleton.API.Entities;

namespace Carleton.API.Services
{
    public interface ICarletonInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, 
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task AddCityAsync(City city);
        Task UpdateCityAsync(City city);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
