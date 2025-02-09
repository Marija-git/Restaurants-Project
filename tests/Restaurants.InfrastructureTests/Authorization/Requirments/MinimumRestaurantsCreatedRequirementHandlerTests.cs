using Xunit;


using Restaurants.Application.Users;
using Moq;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;

namespace Restaurants.Infrastructure.Authorization.Requirments.Tests
{
    public class MinimumRestaurantsCreatedRequirementHandlerTests
    {
        [Fact]
        public async void HandleRequirementAsyncTest_UserHasNotCreatedMultipleRestaurants_SholuldFail()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var restaturants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId = "3"
                }
               
            };
            var requirment = new MinimumRestaurantsCreatedRequirement(2);
            var context = new AuthorizationHandlerContext([requirment], null, null);
            
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
            restaurantsRepositoryMock.Setup(repo => repo.GetAllRestaurants()).ReturnsAsync(restaturants);

            var handler = new MinimumRestaurantsCreatedRequirementHandler(userContextMock.Object,
                restaurantsRepositoryMock.Object);

            //act

            //AuthorizationHandler class interno poziva HandleRequirmentAsync (koju smo override)
            //pozivanjem HandleAsync pozivamo i logiku untar HandleRequrimentAsync metode u unit testovima
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();

        }

        [Fact]
        public async void HandleRequirementAsyncTest_UserHasCreatedMultipleRestaurants_SholuldSucceed()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
            var restaturants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.Id
                },
                 new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId = "3"
                }

            };
            var requirment = new MinimumRestaurantsCreatedRequirement(2);
            var context = new AuthorizationHandlerContext([requirment], null, null);

            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);
            restaurantsRepositoryMock.Setup(repo => repo.GetAllRestaurants()).ReturnsAsync(restaturants);

            var handler = new MinimumRestaurantsCreatedRequirementHandler(userContextMock.Object,
                restaurantsRepositoryMock.Object);

            //act

            //AuthorizationHandler class interno poziva HandleRequirmentAsync (koju smo override)
            //pozivanjem HandleAsync pozivamo i logiku untar HandleRequrimentAsync metode u unit testovima
            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeTrue();
            context.HasFailed.Should().BeFalse();

        }
    }
}