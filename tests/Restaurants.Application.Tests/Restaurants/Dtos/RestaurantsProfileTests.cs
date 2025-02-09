using Xunit;
using Restaurants.Application.Restaurants.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Entities;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos.Tests
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;
        public RestaurantsProfileTests()
        {
            //dodavanje konfiguracije za mapiranje koja je definisana u klasu RestaurantsProfile
            var configuration = new MapperConfiguration(cfg=>cfg.AddProfile<RestaurantsProfile>());
            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange
            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "TestName",
                Description="TestDescription",
                Category="TestCategory",
                HasDelivery = true,
                ContactEmail="test@test.com",
                Address = new Address
                {
                    City="TestCity",
                    Street="TestStreet",
                    PostalCode="12-345"
                }

            };

           //act
           var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

           //assert
           restaurantDto.Should().NotBeNull();
           restaurantDto.Id.Should().Be(restaurant.Id);
           restaurantDto.Name.Should().Be(restaurant.Name);
           restaurantDto.Description.Should().Be(restaurant.Description);
           restaurantDto.Category.Should().Be(restaurant.Category);
           restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
           restaurantDto.City.Should().Be(restaurant.Address.City);
           restaurantDto.Street.Should().Be(restaurant.Address.Street);
           restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "TestName",
                Description = "TestDescription",
                Category = "TestCategory",
                HasDelivery = true,
                ContactNumber="12345670",
                ContactEmail = "test@test.com",
                City = "TestCity",
                Street = "TestStreet",
                PostalCode = "12-345"

            };

            //act
            var restaurant = _mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForUpadateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            var command = new UpdateRestaurantCommand()
            {
                Id = 1,
                Name = "TestName",
                Description = "TestDescription",
                HasDelivery = true,
            };

            //act
            var restaurant = _mapper.Map<Restaurant>(command);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);           
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
  
        }
    }
}