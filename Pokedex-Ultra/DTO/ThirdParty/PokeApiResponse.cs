using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Pokedex_Ultra.Models
{
    public class PokeApiResponse
    {
        public string Name { get; set; }

        public bool Is_Legendary { get; set; }

        public HabitatData Habitat { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public IEnumerable<FlavorTextData> FlavorTextEntries{ get; set; }



        public class HabitatData
        {
            public string Name { get; set; }
        }

        public class FlavorTextData
        {
            [JsonPropertyName("flavor_text")]
            public string FlavorText { get; set; }

            public FlavorTextLanguage Language { get; set; }
        }


        public class FlavorTextLanguage
        {
            public string Name { get; set; }
        }

        public PokemonInfo Map() => new()
        {
            Name = Name,
            Habitat = Habitat.Name,
            IsLegendary = Is_Legendary,
            Description = FlavorTextEntries.FirstOrDefault(t => t.Language.Name == "en").FlavorText
        };
    }
}
