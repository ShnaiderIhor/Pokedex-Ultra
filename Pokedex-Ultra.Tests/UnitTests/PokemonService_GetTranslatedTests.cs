using NSubstitute;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex_Ultra.Tests.UnitTests
{
    public class PokemonService_GetTranslatedTests : PokemonService_Base
    {
        [Fact]
        public async Task GetTranslated_Should_ReturnPokemon_WhenExist()
        {
            //Arrange
            var pokemonMock = new PokemonInfo("mewtwo", "rare", "description", true);
            ConfigurePokeApiClientMock(pokemonMock);
            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, null);

            //Act
            var pokemon = await pokemonService.GetTranslatedAsync(pokemonMock.Name);

            //Assert
            Assert.Equal(pokemonMock, pokemon);
        }

        [Fact]
        public async Task GetTranslated_Should_ReturnNull_WhenNotExist()
        {
            //Arrange
            _pokeApiHttpClient.GetPokemonInfoAsync(Arg.Any<string>()).Returns<PokeApiResponse>(t => throw new Exception());

            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, null);

            //Act
            var pokemon = await pokemonService.GetTranslatedAsync("NotExist");

            //Assert
            Assert.Null(pokemon);
        }

        [Fact]
        public async Task GetTranslated_Should_ReturnYodaTranslation_WhenPokemonIsRareAndLegendary()
        {
            //Arrange
            var pokemonMock = new PokemonInfo("mewtwo", "rare", "description", true);
            ConfigurePokeApiClientMock(pokemonMock);

            var translation = "yoda description";
            ConfigureTranslationsClientMock("yoda", pokemonMock.Description, translation);

            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, _funtranslationHttpClient);


            //Act
            var pokemon = await pokemonService.GetTranslatedAsync(pokemonMock.Name);

            //Assert
            Assert.Equal(pokemonMock.Name, pokemon.Name);
            Assert.Equal(pokemonMock.IsLegendary, pokemon.IsLegendary);
            Assert.Equal(pokemonMock.Habitat, pokemon.Habitat);
            Assert.Equal(translation, pokemon.Description);
        }

        [Fact]
        public async Task GetTranslated_Should_ReturnShakespeareTranslation_WhenPokemonIsNotRareOrNotLegendary()
        {
            //Arrange
            var pokemonMock = new PokemonInfo("mewtwo", "rare", "description", true);
            ConfigurePokeApiClientMock(pokemonMock);
            var translation = "shakespeare description";
            ConfigureTranslationsClientMock("shakespeare", pokemonMock.Description, translation);

            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, _funtranslationHttpClient);


            //Act
            var pokemon = await pokemonService.GetTranslatedAsync(pokemonMock.Name);

            //Assert
            Assert.Equal(pokemonMock, pokemon);
        }

        [Fact]
        public async Task GetTranslated_Should_ReturnDefaultDescription_WhenTranslationFailed()
        {
            //Arrange
            var pokemonMock = new PokemonInfo("mewtwo", "rare", "description", true);
            ConfigurePokeApiClientMock(pokemonMock);

            _funtranslationHttpClient.TranslateAsync(Arg.Any<string>(), Arg.Any<TranslationRequest>()).Returns<TranslationResponse>(x => throw new Exception());

            IPokemonService pokemonService = new PokemonService(_logger, _pokeApiHttpClient, _funtranslationHttpClient);

            //Act
            var pokemon = await pokemonService.GetTranslatedAsync(pokemonMock.Name);

            //Assert
            Assert.Equal(pokemonMock, pokemon);
        }
    }
}
