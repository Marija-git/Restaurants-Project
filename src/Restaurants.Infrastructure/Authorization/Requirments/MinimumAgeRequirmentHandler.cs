using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirmentHandler(IUserContext userContext,
        ILogger<MinimumAgeRequirmentHandler> logger
        ) : AuthorizationHandler<MinimumAgeRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirment requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("User: {Email}, date of birth {DoB} - Handling MinimumAgeRequirment",
                currentUser.Email,
                currentUser.DateOfBirth);

            if(currentUser.DateOfBirth == null)
            {
                logger.LogInformation("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }
            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
