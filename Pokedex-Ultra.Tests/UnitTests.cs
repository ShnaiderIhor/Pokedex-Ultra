using Pokedex_Ultra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex_Ultra.Tests
{
    public class UnitTests
    {
        [Fact]
        public void GetTranslatedEndpointsReturnCorrectStatusCode()
        {
            //Arrange
            var pokemonService = new PokemonService(null, null);

            //Act

            //Assert

        }
    }
}
