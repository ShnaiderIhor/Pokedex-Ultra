using Microsoft.AspNetCore.Mvc;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System.Linq;
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
            var response = await _pokeApiHttpClient.GetPokemonInfo(pokemonName);

            if (response.Content == null)
                return NotFound();

            return response.Content.Map();
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<ActionResult<PokemonInfo>> GetTranslated(string pokemonName)
        {
            var pokemonInfo = await _translationService.GetTranslated(pokemonName);

            if (pokemonInfo == null)
                return NotFound();

            return pokemonInfo;

        }
    }
}
