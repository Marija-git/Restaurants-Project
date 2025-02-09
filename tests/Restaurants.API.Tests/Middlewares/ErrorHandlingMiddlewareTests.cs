using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurants.API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_NoException_ShouldCallNextDelegate()
        {
            //arrange
            var nextDelegateMock = new Mock<RequestDelegate>();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();

            //act
            await middleware.InvokeAsync(context,nextDelegateMock.Object);

            //assert
            nextDelegateMock.Verify(nextDelegate=>nextDelegate.Invoke(context),Times.Once);
        }

        [Fact()]
        public async Task InvokeAsync_NotFoundException_SetsStatusCode404()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var notFoundEx = new NotFoundException(nameof(Restaurant),"1");

            //act
            await middleware.InvokeAsync(context, _ => throw notFoundEx);

            //assert
            context.Response.StatusCode.Should().Be(404);
        }


        [Fact()]
        public async Task InvokeAsync_ForbidException_SetsStatusCode403()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var forbidEx = new ForbidException();

            //act
            await middleware.InvokeAsync(context, _ => throw forbidEx);

            //assert
            context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async Task InvokeAsync_Exception_SetsStatusCode500()
        {
            //arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var ex = new Exception();

            //act
            await middleware.InvokeAsync(context, _ => throw ex);

            //assert
            context.Response.StatusCode.Should().Be(500);
        }

    }
}