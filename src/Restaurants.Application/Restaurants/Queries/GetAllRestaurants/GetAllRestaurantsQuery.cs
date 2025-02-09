using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PaginatedResult<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; } 
    }
}
