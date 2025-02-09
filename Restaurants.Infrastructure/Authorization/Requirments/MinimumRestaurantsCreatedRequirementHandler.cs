using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    internal class MinimumRestaurantsCreatedRequirementHandler(IUserContext userContext,
        IRestaurantsRepository restaurantsRepository)
        : AuthorizationHandler<MinimumRestaurantsCreatedRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinimumRestaurantsCreatedRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            var restaurants = await restaurantsRepository.GetAllRestaurants();
            var numOfUsersCreatedRestaurants = restaurants.Count(r => r.OwnerId == currentUser!.Id);
            if(numOfUsersCreatedRestaurants >= requirement.MinimumRestaurantCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
          
        }
    }
}
