using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Restaurants.Infrastructure.Tests")]

//InternalVisibleTo atribut omogucava koriscenje internal klasa i drugih tipova
// u test projektima iz infrastructure sloja
//konkretno : internal class MinimumRestaurantsCreatedRequirementHandler