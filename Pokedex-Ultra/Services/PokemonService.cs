using Microsoft.Extensions.Logging;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using System;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Services
{
    public class PokemonService : IPokemonService
    {
        ILogger<PokemonService> _logger;
        IFuntranslationHttpClient _funtranslationHttpClient;
        IPokeApiHttpClient _pokeApiHttpClient;
        public PokemonService(ILogger<PokemonService> logger, IPokeApiHttpClient pokeApiHttpClient, IFuntranslationHttpClient funtranslationHttpClient)
        {
            _logger = logger;
            _funtranslationHttpClient = funtranslationHttpClient;
            _pokeApiHttpClient = pokeApiHttpClient;
        }

        public async Task<PokemonInfo> GetInfoAsync(string pokemonName)
        {
            try
            {
                return (await _pokeApiHttpClient.GetPokemonInfoAsync(pokemonName)).Map();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Pokemon {pokemonName} is not found", ex);
                return null;
            }
        }
        public async Task<PokemonInfo> GetTranslatedAsync(string pokemonName)
        {
            PokemonInfo pokemon = await GetInfoAsync(pokemonName);

            try
            {
                var translationType = GetTranslationType(pokemon.IsLegendary, pokemon.Habitat);

                var translation = await _funtranslationHttpClient.TranslateAsync(translationType, new TranslationRequest()
                {
                    Text = pokemon.Description
                });

                pokemon.Description = translation.Contents.Translated;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Translation couldn't be done for pokemon {pokemonName}", ex);
            }

            return pokemon;
        }

        private string GetTranslationType(bool isLegendary, string habitat)
        {
            if (isLegendary || habitat == "cave")
                return "yoda";

            return "shakespeare";
        }
    }
}
