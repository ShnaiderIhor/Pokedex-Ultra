using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApiHttpClient _pokeApiHttpClient;
        private readonly IFuntranslationHttpClient _funtranslationHttpClient;

        public PokemonController(IPokeApiHttpClient pokeApiHttpClient,
            IFuntranslationHttpClient funtranslationHttpClient)
        {
            _pokeApiHttpClient = pokeApiHttpClient;
            _funtranslationHttpClient = funtranslationHttpClient;
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<PokemonResponse>> Get(string pokemonName)
        {
            var response = await _pokeApiHttpClient.GetPokemonInfo(pokemonName);
            return Map(response.Content);
        }
        private PokemonResponse Map(PokeApiResponse pokeApiResponse) => new()
        {
            Name = pokeApiResponse.Name,
            Habitat = pokeApiResponse.Habitat.Name,
            IsLegendary = pokeApiResponse.Is_Legendary,
            Description = pokeApiResponse.FlavorTextEntries.FirstOrDefault(t => t.Language.Name == "en").FlavorText
        };
    }
}
