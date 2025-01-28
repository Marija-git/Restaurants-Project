﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        //metoda za dobijanje trenutnog korisnika
        public CurrentUser? GetCurrentUser()
        {
            //info o trenutnom korisniku pomocu http context-a
            //sa "?" osiguravamo da nece biti bacena greska vec ce user biti null
            var user = httpContextAccessor?.HttpContext?.User; 
            if(user is null)
            {
                throw new InvalidOperationException("User context is not presetn.");
            }

            //ako korisnik ne postoji ili nije autentifikovan
            if (user.Identity is null || !user.Identity.IsAuthenticated) 
            {
                return null;
            }

            //pretazuje sve claims od user-a da bi nasla NameIndetifier obicno oznacava Id, na isti nacin trazi email i role
            // sa "!" osiguravamo da ovo nikada ne bude null
            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value); //sve role koje korisnik ima

            return new CurrentUser(userId, email, roles);

        }
    }
}
