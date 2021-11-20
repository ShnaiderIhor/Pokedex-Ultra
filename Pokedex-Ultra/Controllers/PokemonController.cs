using Microsoft.AspNetCore.Mvc;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApiHttpClient _pokeApiHttpClient;
        private readonly IPokemonService _translationService;

        public PokemonController(IPokeApiHttpClient pokeApiHttpClient,
            IPokemonService translationService)
        {
            _pokeApiHttpClient = pokeApiHttpClient;
            _translationService = translationService;
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<PokemonInfo>> Get(string pokemonName)
        {
            var response = await _pokeApiHttpClient.GetPokemonInfoAsync(pokemonName);

            if (response == null)
                return NotFound();

            return response.Map();
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<ActionResult<PokemonInfo>> GetTranslated(string pokemonName)
        {
            var pokemonInfo = await _translationService.GetTranslatedAsync(pokemonName);

            if (pokemonInfo == null)
                return NotFound();

            return pokemonInfo;

        }
    }
}
