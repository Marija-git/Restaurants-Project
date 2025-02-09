
using Moq;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using AutoMapper;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using FluentAssertions;
using Restaurants.Domain.Exceptions;
using System.Security.AccessControl;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantsAuthorizationService;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
            _mapperMock = new Mock<IMapper>();
            _restaurantsAuthorizationService = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(_loggerMock.Object,
                _restaurantsRepositoryMock.Object,
                _mapperMock.Object,
                _restaurantsAuthorizationService.Object);
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ShouldUpadateRestaurant()
        {
            //arrage
            var request = new UpdateRestaurantCommand
            {
                Id = 1,
                Name = "TestUpdate",
                Description = "TestDescriptionUpdate",
                HasDelivery = true
            };
            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "TestRestaurant",
                Description = "TestDescription",
                HasDelivery = false
            };

            _restaurantsRepositoryMock.Setup(rep => rep.GetById(request.Id)).ReturnsAsync(restaurant);
            _restaurantsAuthorizationService.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(true);
            _mapperMock.Setup(m => m.Map<Restaurant>(request)).Returns(restaurant);

            //act
            await _handler.Handle(request, CancellationToken.None);

            //assert
            _mapperMock.Verify(m => m.Map(request, restaurant), Times.Once);
            _restaurantsRepositoryMock.Verify(r => r.Update(), Times.Once);
        }

        [Fact]
        public async Task Handle_ForInvalidCommand_ReturnsNotFoundException()
        {
            //arrange
            var request = new UpdateRestaurantCommand
            {
                Id = 1,
                Name = "TestUpdate",
                Description = "TestDescriptionUpdate",
                HasDelivery = true
            };
            _restaurantsRepositoryMock.Setup(rep => rep.GetById(request.Id)).ReturnsAsync((Restaurant?)null);

           //act
           Func<Task> action = async () =>  await _handler.Handle(request, CancellationToken.None);

            //assert
            await action.Should().ThrowAsync<NotFoundException>().WithMessage($"Restaurant with id: {request.Id} doesn't exist.");

        }

        [Fact]
        public async Task Handle_ForInvalidCommand_ReturnsNotForbidExceptionn()
        {
            //arrange
            var request = new UpdateRestaurantCommand
            {
                Id = 1,
            };
            var restaurant = new Restaurant()
            {
                Id = 1,
            };

            _restaurantsRepositoryMock.Setup(rep => rep.GetById(request.Id)).ReturnsAsync(restaurant);
            _restaurantsAuthorizationService.Setup(a => a.Authorize(restaurant, ResourceOperation.Update)).Returns(false);

            //act
            Func<Task> action = async () => await _handler.Handle(request, CancellationToken.None);

            //assert
            await action.Should().ThrowAsync<ForbidException>();

        }
    }
}