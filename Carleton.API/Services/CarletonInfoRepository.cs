using Carleton.API.DbContexts;
using Carleton.API.Entities;
using Carleton.API.Helpers;
using Microsoft.EntityFrameworkCore;


namespace Carleton.API.Services
{
    public class CarletonInfoRepository : ICarletonInfoRepository
    {
        private readonly CarletonInfoContext _context;

        public CarletonInfoRepository(CarletonInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
    string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from using iqueriable which causes deleayed execution.
            var collection = _context.Cities as IQueryable<City>;

            //matched name
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            //search
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        //This is not being used it was replace by public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
        //string? name, string? searchQuery, int pageNumber, int pageSize) This just filters on name
        public async Task<IEnumerable<City>> GetCitiesAsync(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetCitiesAsync();
            }

            name = name.Trim();
            return await _context.Cities
                .Where(c => c.Name == name)
                .OrderBy(c => c.Name)
                .ToListAsync();

        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities
                  .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.User.AnyAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

        public async Task<User?> GetUserAsync(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            password = Helpers.HashPassword.Create(password);
            return await _context.User
                  .Where(c => c.Email.ToLower() == email.ToLower() && c.Password == password)
                  .FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserAsync(int userId)
        {
            return await _context.User
                  .Where(c => c.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(
            int cityId,
            int pointOfInterestId)
        {
            return await _context.PointsOfInterest
               .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(
            int cityId)
        {
            return await _context.PointsOfInterest
                           .Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId,
            PointOfInterest pointOfInterest)
        {
            var City = await GetCityAsync(cityId, false);
            if (City != null)
            {
                City.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task AddCityAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task AddUserAsync(User user)
        {
            //If user does not exit add.
            if (!await UserExistsAsync(user.Email))
            {
                user.Email = user.Email.ToLower();
                user.Password = HashPassword.Create(user.Password);
                user.RegistrationDate = DateTime.UtcNow;
                user.AuthenticationString = RandomNumber.Create();
                await _context.User.AddAsync(user);
            }
        }

        public async Task UpdateCityAsync(City city)
        {
            var City = await GetCityAsync(city.Id, false);
            if (City != null)
            {

                _context.Cities.Update(City);
            }
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
        }

        public void DeleteUser(User user)
        {
            _context.User.Remove(user);
        }


        //filter firstname or search lastname or both or paginate results.
        public async Task<(IEnumerable<User>, PaginationMetadata)> GetUsersAsync(
string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from using iqueriable which causes deleayed execution.
            var collection = _context.User as IQueryable<User>;

            //matched name
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.FirstName == name);
            }

            //search
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(searchQuery)
                    || (a.LastName != null && a.LastName.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.FirstName)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<bool> CityNameMatchesCityId(string? cityName, int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId && c.Name == cityName);
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

    }
}
