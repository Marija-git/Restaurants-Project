
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async void Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();
            var restaurantRepositoyMock = new Mock<IRestaurantsRepository>();
            var userContextMock = new Mock<IUserContext>();
            var request = new CreateRestaurantCommand();

            var currentUser = new CurrentUser("owner-id", "test@test.com", [],null,null);
            userContextMock.Setup(u=>u.GetCurrentUser()).Returns(currentUser);

            var restaurant = new Restaurant();
            mapperMock.Setup(m => m.Map<Restaurant>(request)).Returns(restaurant);

            restaurantRepositoyMock.Setup(rep => rep.Create(It.IsAny<Restaurant>())).ReturnsAsync(1); //create je async/vraca task<int>

            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object
                , mapperMock.Object
                , restaurantRepositoyMock.Object,
                userContextMock.Object);

            //act
            var result = await commandHandler.Handle(request, CancellationToken.None);

            //assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be(currentUser.Id); //owner-id
            restaurantRepositoyMock.Verify(r => r.Create(restaurant), Times.Once);

        }
    }
}