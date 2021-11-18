using Pokedex_Ultra.Models;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Services
{
    public interface IPokemonService
    {
        string GetTranslationType(bool isLegendary, string habitat);
        Task<PokemonInfo> GetTranslated(string name);
    }
}
