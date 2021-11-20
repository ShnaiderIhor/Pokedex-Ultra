using NSubstitute;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex_Ultra.Tests.UnitTests
{
    public class PokemonService_GetInfoTests : PokemonService_Base
    {
        [Fact]
        public async Task GetInfo_Should_ReturnPokemon_WhenExist()
        {
            //Arrange
            var pokemonMock = new PokemonInfo("mewtwo", "rare", "description", true);
            ConfigurePokeApiClientMock(pokemonMock);
            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, null);

            //Act
            var pokemon = await pokemonService.GetInfoAsync(pokemonMock.Name);

            //Assert
            Assert.Equal(pokemonMock, pokemon);
        }

        [Fact]
        public async Task GetInfo_Should_ReturnNull_WhenNotExist()
        {
            //Arrange
            _pokeApiHttpClient.GetPokemonInfoAsync(Arg.Any<string>()).Returns<PokeApiResponse>(t => throw new Exception());

            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, null);

            //Act
            var pokemon = await pokemonService.GetInfoAsync("NotExist");

            //Assert
            Assert.Null(pokemon);
        }
    }
}
