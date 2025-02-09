using Microsoft.AspNetCore.Authorization;


namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumRestaurantsCreatedRequirement(int minimumRestaurantCreated) : IAuthorizationRequirement
    {
        public int MinimumRestaurantCreated { get; }= minimumRestaurantCreated;
    }
}
