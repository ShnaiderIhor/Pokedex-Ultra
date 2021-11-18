using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Pokedex_Ultra.HttpClients;
using Pokedex_Ultra.Models;
using Refit;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex_Ultra.Tests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Remove implementation
                var clientDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IPokeApiHttpClient));
                services.Remove(clientDescriptor);

                //Prepare mock
                var response = new ApiResponse<PokeApiResponse>(new System.Net.Http.HttpResponseMessage(), new PokeApiResponse()
                {
                    
                }, null);
                var client = Substitute.For<IPokeApiHttpClient>();
                client.GetPokemonInfo(Arg.Any<string>()).Returns(Task.FromResult(response));

                //Inject
                services.AddTransient(s => client);
            });
        }
    }
}