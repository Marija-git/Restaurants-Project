using Xunit;
using Restaurants.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace Restaurants.API.Controllers.Tests
{
    //interfjes koji omogucava deljenje instance factory medju svim testovima u klasi
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        //omocuvanje pokretanja app unutar testa,pravljenje http clienta i slanje zahteva ka stvarnom serveru kao da je app u produkciji
        private readonly WebApplicationFactory<Program> _factory;

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
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