using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Users.Commands
{
    public class UpdateUserDetailsCommandHandler(IUserContext userContext,
        ILogger<UpdateUserDetailsCommandHandler> logger, IUserStore<User> userStore
        ) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

            // "!"  da bi se izbegle upozorenja u vezi sa mogućim null vrednostima
            //cancellationToken kako bi se omogućilo otkazivanje operacije ako to bude potrebno
            // na primer, ako korisnik pređe na drugu stranu aplikacije ili ako se završi neka dugotrajna operacija
            var dataBaseUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);
            if(dataBaseUser is null)
            {
                throw new NotFoundException(nameof(user),user!.Id);
            }

            dataBaseUser.DateOfBirth = request.DateOfBirth;
            dataBaseUser.Nationality = request.Nationality;           
            await userStore.UpdateAsync(dataBaseUser, cancellationToken);
        }
    }
}
