using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex_Ultra.Tests
{
    public class BasicTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BasicTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/pokemon/mewtwo", HttpStatusCode.OK)]
        [InlineData("/pokemon/notexist", HttpStatusCode.NotFound)]
        public async Task GetEndpointsReturnCorrectStatusCode(string url, HttpStatusCode httpStatusCode)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(httpStatusCode, response.StatusCode);
        }

        [Theory]
        [InlineData("/pokemon/translated/mewtwo", HttpStatusCode.OK)]
        [InlineData("/pokemon/translated/notexist", HttpStatusCode.NotFound)]
        public async Task GetTranslatedEndpointsReturnCorrectStatusCode(string url, HttpStatusCode httpStatusCode)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(httpStatusCode, response.StatusCode);
        }
    }
}
