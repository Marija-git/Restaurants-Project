using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        ILogger<DeleteDishesForRestaurantCommandHandler> logger,
        IRestaurantAuthorizationService restaurantAuthorizationService)
        : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if(restaurant == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            //check authorization
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            await dishesRepository.Delete(restaurant.Dishes);
        }
    }
}
