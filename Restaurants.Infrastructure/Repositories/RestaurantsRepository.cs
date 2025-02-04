using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            return await dbContext.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetById(int id)
        {
            return await dbContext.Restaurants.Include(r=>r.Dishes).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Delete(Restaurant restaurant)
        {
            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public Task Update() => dbContext.SaveChangesAsync();

        public async Task<IEnumerable<Restaurant>> GetAllMatching(string? searchPhrase)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var restaurants = await dbContext
                .Restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)))
                .ToListAsync();
            return restaurants;
        }
    }
}
