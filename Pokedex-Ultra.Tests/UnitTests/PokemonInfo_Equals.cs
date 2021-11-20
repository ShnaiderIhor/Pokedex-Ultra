using Pokedex_Ultra.Models;
using Xunit;

namespace Pokedex_Ultra.Tests.UnitTests
{
    public class PokemonInfo_Equals 
    {
        [Fact]
        public void Equals_Should_ReturnTrue_WhenPropertiesAreEqual()
        {
            //Arrange
            var pokemon1 = new PokemonInfo("mewtwo", "rare", "description", true);
            var pokemon2 = new PokemonInfo("mewtwo", "rare", "description", true);

            //Act
            var result = pokemon1.Equals(pokemon2);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_Should_ReturnFalse_WhenPropertiesAreNotEqueal()
        {
            //Arrange
            var pokemon1 = new PokemonInfo("mewtwo", "rare", "description", true);
            var pokemon2 = new PokemonInfo("mewtwo", "rare", "description", false);

            //Act
            var result = pokemon1.Equals(pokemon2);

            //Assert
            Assert.False(result);
        }
    }
}
