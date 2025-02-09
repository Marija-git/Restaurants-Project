using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation operation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user.Email, operation, restaurant.Name);

            if(operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }
            if (operation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Delete operation, admin user - successful authorization");
                return true;
            }
            if ((operation == ResourceOperation.Delete || operation==ResourceOperation.Update)
                && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Delete/Update operation, restaurant owner - successful authorization");
                return true;
            }
            return false;
        }
    }
}
