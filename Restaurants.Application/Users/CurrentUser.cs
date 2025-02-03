

namespace Restaurants.Application.Users
{
    //record tip u C# je specijalni tip koji se koristi za čuvanje podataka sa automatski generisanim metodama za poređenje i kopiranje
    public record CurrentUser(string Id, string Email,
        IEnumerable<string> Roles,
        string? Nationality,
        DateOnly? DateOfBirth)
    {
        //metoda koja ce omocugiciti da proverimo da li korisnik ima odredjenu rolu iz liste
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
