using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var dateOfBirth = new DateOnly(1999, 1, 1);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,"1"),
                new(ClaimTypes.Email,"test@test.com"),
                new(ClaimTypes.Role,UserRoles.Admin),
                new(ClaimTypes.Role,UserRoles.User),
                new("Nationality","Serbian"),
                new("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd")),
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "authTypeTest"));  //kreiranje user-a sa prethodno kreiranim claim-ovima
            httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(user); //postavka user-a u httpcontext
            var userContext = new UserContext(httpContextAccessorMock.Object); //kreiranje usercontext objekta pomocu user-a iz httpcontext-a

            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin,UserRoles.User);
            currentUser.Nationality.Should().Be("Serbian");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);

        }

        [Fact()]        
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);
            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            Action act = () => userContext.GetCurrentUser(); //ocekujem InvalidOperationException

            //assert
            act.Should().Throw<InvalidOperationException>().WithMessage("User context is not present.");
        }
    }
}