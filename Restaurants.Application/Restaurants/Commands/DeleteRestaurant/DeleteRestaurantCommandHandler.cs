using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository
        ) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetById(request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(restaurant), request.Id.ToString());
            await restaurantsRepository.Delete(restaurant);

        }
    }
}
