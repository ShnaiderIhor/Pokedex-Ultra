using Microsoft.AspNetCore.Mvc;
using Pokedex_Ultra.Models;
using Pokedex_Ultra.Services;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<PokemonInfo>> Get(string pokemonName)
        {
            var response = await _pokemonService.GetInfoAsync(pokemonName);

            if (response == null)
                return NotFound();

            return response;
        }

        [HttpGet("translated/{pokemonName}")]
        public async Task<ActionResult<PokemonInfo>> GetTranslated(string pokemonName)
        {
            var pokemonInfo = await _pokemonService.GetTranslatedAsync(pokemonName);

            if (pokemonInfo == null)
                return NotFound();

            return pokemonInfo;

        }
    }
}
