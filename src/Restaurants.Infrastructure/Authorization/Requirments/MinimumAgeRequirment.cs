using Microsoft.AspNetCore.Authorization;


namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirment(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
