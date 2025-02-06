using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatching(string? searchPhrase,int pageSize,int pageIndex,
            string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var restaurantsBaseQuery = dbContext
                .Restaurants
                .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                       || r.Description.ToLower().Contains(searchPhraseLower)));
                
            var numOfRestaurants = await restaurantsBaseQuery.CountAsync(); //total count

            if(sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r=>r.Name },
                    {nameof(Restaurant.Description), r=>r.Description },
                    {nameof(Restaurant.Category), r=>r.Category },
                };

                var selectedColumn = columnsSelector[sortBy]; //r => r.Name => lambda expression => Expression<Func<Restaurant, object>>

                restaurantsBaseQuery = sortDirection == SortDirection.Ascending ?
                    restaurantsBaseQuery.OrderBy(selectedColumn) : restaurantsBaseQuery.OrderByDescending(selectedColumn);
            }


            var restaurants = await restaurantsBaseQuery
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (restaurants,numOfRestaurants);
        }
    }
}
