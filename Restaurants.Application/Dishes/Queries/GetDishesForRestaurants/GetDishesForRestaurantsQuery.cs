using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurants
{
    public class GetDishesForRestaurantsQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>    
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
