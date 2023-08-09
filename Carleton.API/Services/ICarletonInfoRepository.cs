using Carleton.API.Entities;

namespace Carleton.API.Services
{
    public interface ICarletonInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<IEnumerable<City>> GetCitiesAsync(string? name);
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId,
            int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
        Task<bool> UserExistsAsync(int id);
        Task<User?> GetUserAsync(int userId);
        void DeleteUser(User user);
        Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(
string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<User?> GetUserAsync(string email, string password);
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);
    }
   
}
