using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IMapper mapper
        ) : IRequestHandler<CreateDishCommand>
    {
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish {@Dish}", request);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if(restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
            }
            await dishesRepository.Create(mapper.Map<Dish>(request));
        }
    }
}
