using Pokedex_Ultra.Models;
using Refit;
using System.Threading.Tasks;

namespace Pokedex_Ultra.HttpClients
{
    public interface IFuntranslationHttpClient
    {
        [Post("/{type}")]
        public Task<ApiResponse<TranslationResponse>> Translate(string type, TranslationRequest text);
    }
}
