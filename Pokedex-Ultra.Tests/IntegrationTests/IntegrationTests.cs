using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex_Ultra.Tests
{
    public class IntegrationTests
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public IntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/pokemon/mewtwo", HttpStatusCode.OK)]
        [InlineData("/pokemon/notexist", HttpStatusCode.NotFound)]
        public async Task Get_Should_Return_CorrectStatusCode(string url, HttpStatusCode httpStatusCode)
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
        public async Task GetTranslated_Should_Return_CorrectStatusCode(string url, HttpStatusCode httpStatusCode)
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
