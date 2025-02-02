using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization
{
    //klasa za dodavanje specificnih claim-ova
    public class RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager,options)
    {
        //kreiranje objekta ClaimsPrincipal koji sadrzi info o korisniku (id,role,podaci...)
        //objekat sadrzi sve claim-ove korisnika
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var id = await GenerateClaimsAsync(user); //generisanje osnovnih claim-ova
            if (user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
            }
            if (user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }
            return new ClaimsPrincipal(id);
        }
    }
}
