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
    IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PaginatedResult<RestaurantDto>>
    {
        public async Task<PaginatedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants {searchQuery}", request.SearchPhrase);

            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatching(request.SearchPhrase,
                request.PageSize,
                request.PageIndex,
                request.SortBy,
                request.SortDirection);

            var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return new PaginatedResult<RestaurantDto>(restaurantsDtos,totalCount,request.PageSize,request.PageIndex);
        }
    }
}
