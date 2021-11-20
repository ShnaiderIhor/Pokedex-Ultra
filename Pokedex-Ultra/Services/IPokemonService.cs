using Pokedex_Ultra.Models;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Services
{
    public interface IPokemonService
    {
        Task<PokemonInfo> GetInfoAsync(string pokemonName);
        Task<PokemonInfo> GetTranslatedAsync(string pokemonName);
    }
}
