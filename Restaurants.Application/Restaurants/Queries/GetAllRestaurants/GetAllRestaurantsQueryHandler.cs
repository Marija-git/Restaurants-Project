using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants {searchQuery}", request.SearchPhrase);
            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(await restaurantsRepository.GetAllMatching(request.SearchPhrase));
            return restaurantsDtos;
        }
    }
}
