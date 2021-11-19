using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Services
{
    public class PokemonService : IPokemonService
    {
        IFuntranslationHttpClient _funtranslationHttpClient;
        IPokeApiHttpClient _pokeApiHttpClient;
        public PokemonService(IPokeApiHttpClient pokeApiHttpClient, IFuntranslationHttpClient funtranslationHttpClient)
        {
            _funtranslationHttpClient = funtranslationHttpClient;
            _pokeApiHttpClient = pokeApiHttpClient;
        }

        public string GetTranslationType(bool isLegendary, string habitat)
        {
            if (isLegendary || habitat == "cave")
                return "yoda";

            return "shakespeare";
        }

        public async Task<PokemonInfo> GetTranslated(string name)
        {
            var pokemonResponse = await _pokeApiHttpClient.GetPokemonInfo(name);

            if (pokemonResponse.Content == null)
                return null;

            var pokemon = pokemonResponse.Content.Map();

            var translation = await _funtranslationHttpClient.Translate(GetTranslationType(pokemon.IsLegendary, pokemon.Habitat), new TranslationRequest()
            {
                Text = pokemon.Description
            });

            if (translation.Content != null)
                pokemon.Description = translation.Content.Contents.Translated;

            return pokemon;
        }
    }
}
