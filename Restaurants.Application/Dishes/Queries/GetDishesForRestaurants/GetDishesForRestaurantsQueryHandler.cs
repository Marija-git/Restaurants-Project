using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurants
{
    public class GetDishesForRestaurantsQueryHandler(IRestaurantsRepository restaurantsRepository,
        IMapper mapper,ILogger<GetDishesForRestaurantsQueryHandler> logger) 
        : IRequestHandler<GetDishesForRestaurantsQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dishes for restaurant with id: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            return mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
        }
    }
}
