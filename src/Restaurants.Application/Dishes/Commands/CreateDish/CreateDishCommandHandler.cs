﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IMapper mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService
        ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new dish {@Dish}", request);
            var restaurant = await restaurantsRepository.GetById(request.RestaurantId);
            if(restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());
            }

            //check authorization
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }

            return await dishesRepository.Create(mapper.Map<Dish>(request));
        }
    }
}
