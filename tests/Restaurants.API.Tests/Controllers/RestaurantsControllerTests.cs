using Xunit;
using Restaurants.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Repositories;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Authorization.Policy;
using Moq;
using Restaurants.API.Tests;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.Dtos;
using System.Net.Http.Json;

namespace Restaurants.API.Controllers.Tests
{
    //interfjes koji omogucava deljenje instance factory medju svim testovima u klasi
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        //omocuvanje pokretanja app unutar testa,pravljenje http clienta i slanje zahteva ka stvarnom serveru kao da je app u produkciji
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    //jedinstvena instanca za sve testove/za fejk auth
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>(); 

                    //zamena postojeceg IRestaurantRepositor koji pristupa bazi sa mock/simulacijom tog repozitorija
                    //koji vraca predefinisane rezultate
                    //posebna instanca za svavki test
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository),
                                                _ => _restaurantsRepositoryMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {
            //arrange
            var id = 1111;
            _restaurantsRepositoryMock.Setup(rep => rep.GetById(id)).ReturnsAsync((Restaurant?)null);
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForExistingId_ShouldReturn200OK()
        {
            //arrange
            var restaurant = new Restaurant()
            {
                Id = 1111
            };
            _restaurantsRepositoryMock.Setup(rep => rep.GetById(restaurant.Id)).ReturnsAsync(restaurant);
            var client = _factory.CreateClient();

            //act
            var response = await client.GetAsync($"/api/restaurants/{restaurant.Id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
        }

        [Fact]
        public async Task GetAllRestaurants_ValidRequest_Returns200OK()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants?pageSize=3&pageIndex=1");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        [Fact]
        public async Task GetAllRestaurants_InvalidRequest_Returns400BadRequest()
        {
            //arrange
            var client = _factory.CreateClient();

            //act
            var result = await client.GetAsync("/api/restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}