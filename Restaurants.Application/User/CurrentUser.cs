using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.User
{
    //record tip u C# je specijalni tip koji se koristi za čuvanje podataka sa automatski generisanim metodama za poređenje i kopiranje
    public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
    {
        //metoda koja ce omocugiciti da proverimo da li korisnik ima odredjenu rolu iz liste
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
