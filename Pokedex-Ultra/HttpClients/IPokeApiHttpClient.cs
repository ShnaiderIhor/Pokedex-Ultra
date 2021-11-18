using Pokedex_Ultra.Models;
using Refit;
using System.Threading.Tasks;

namespace Pokedex_Ultra.HttpClients
{
    public interface IPokeApiHttpClient
    {
        [Get("/pokemon-species/{name}")]
        public Task<ApiResponse<PokeApiResponse>> GetPokemonInfo(string name);
    }
}
