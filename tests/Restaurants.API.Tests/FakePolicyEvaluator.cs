using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Restaurants.API.Tests
{
    internal class FakePolicyEvaluator : IPolicyEvaluator
    {
        //fejkamo autentifikaciju - metoda vraca uspesnu autentifikaciju bez provere
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            //pravimo random identity sa klejmovima 
            claimsPrincipal.AddIdentity(new ClaimsIdentity( //kreiranje novog identiteta
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Role, "Admin"),
                }));

            var ticket = new AuthenticationTicket(claimsPrincipal, "Test"); //kreiranje tiketa sa info o korisniku
            var result = AuthenticateResult.Success(ticket); //autentifikacija uspesno obavljena
            return Task.FromResult(result);
        }

        //simuliranje uspesne autorizacije
        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            var result = PolicyAuthorizationResult.Success(); //return usepsan rezultat bez ikakve provere
            return Task.FromResult(result);
        }
    }
}
