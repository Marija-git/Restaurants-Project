using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(IRestaurantsRepository restaurantsRepository,
        IMapper mapper,
        ILogger<GetDishByIdForRestaurantQueryHandler> logger) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id {RestaurantId}",request.DishId,request.RestaurantId);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if (restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
            }
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if(dish == null)
            {
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            }

            return mapper.Map<DishDto>(dish);
        }
    }
}
