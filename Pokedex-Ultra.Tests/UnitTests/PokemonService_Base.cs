using Microsoft.Extensions.Logging;
using NSubstitute;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Tests.UnitTests
{
    public class PokemonService_Base
    {
        protected readonly ILogger<PokemonService> _logger = Substitute.For<ILogger<PokemonService>>();
        protected readonly IPokeApiHttpClient _pokeApiHttpClient = Substitute.For<IPokeApiHttpClient>();
        protected readonly IFuntranslationHttpClient _funtranslationHttpClient = Substitute.For<IFuntranslationHttpClient>();

        protected void ConfigurePokeApiClientMock(PokemonInfo pokemonInfo)
        {
            var response = new PokeApiResponse()
            {
                Name = pokemonInfo.Name,
                Habitat = new PokeApiResponse.HabitatData()
                {
                    Name = pokemonInfo.Habitat
                },
                Is_Legendary = pokemonInfo.IsLegendary,
                FlavorTextEntries = new List<PokeApiResponse.FlavorTextData>
                {
                    new PokeApiResponse.FlavorTextData()
                    {
                        FlavorText = pokemonInfo.Description,
                        Language= new PokeApiResponse.FlavorTextLanguage()
                        {
                            Name = "en"
                        }
                    }
                }
            };

            _pokeApiHttpClient
                .GetPokemonInfoAsync(pokemonInfo.Name)
                .Returns(Task.FromResult(response));

        }
        protected void ConfigureTranslationsClientMock(string type, string input, string output)
        {
            var response = new TranslationResponse { Contents = new TranslationResponse.TranslatedContent() { Translated = output } };

            _funtranslationHttpClient.TranslateAsync(type, Arg.Is((TranslationRequest r) => r.Text == input))
                .Returns(Task.FromResult(response));

        }
    }
}
