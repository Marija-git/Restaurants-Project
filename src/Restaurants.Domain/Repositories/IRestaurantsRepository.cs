using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant?> GetById(int id);
        Task<int> Create(Restaurant restaurant);
        Task Delete(Restaurant restaurant);
        Task Update();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatching(string? searchPhrase, int pageSize, int pageIndex, string? sortBy, SortDirection sortDirection);
    }
}
