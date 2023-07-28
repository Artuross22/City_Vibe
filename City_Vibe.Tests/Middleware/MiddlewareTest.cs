using City_Vibe.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Moq;
namespace City_Vibe.Tests.Middleware
{
    public class MiddlewareTest
    {

        private readonly Mock<RequestDelegate> _mockRequestDelegate;
        private readonly CustomRequestValidationMiddleware _middleware;

        public MiddlewareTest()
        {
            _mockRequestDelegate = new Mock<RequestDelegate>();
            _middleware =  new CustomRequestValidationMiddleware(_mockRequestDelegate.Object);
        }

        [Fact]
        public async Task InvokeAsync_ReturnsBadRequest_WhenUserAgentIsYandex()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Headers[HeaderNames.UserAgent] = "YaBrowser";

            // Act
            await _middleware.InvokeAsync(context);

            // Assert
            var response = context.Items["text"];
            Assert.Equal(400, context.Response.StatusCode);
            Assert.Equal("Invalid request", response);
            //  var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
        }
    }
}

