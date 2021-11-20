namespace Pokedex_Ultra.Models
{
    public class PokemonInfo
    {
        public PokemonInfo(){}
        public PokemonInfo(string name, string habitat, string description, bool isLegendary)
        {
            Name = name;
            Habitat = habitat;
            Description = description;
            IsLegendary = isLegendary;
        }
        public string Name { get; set; }

        public string Habitat { get; set; }

        public string Description { get; set; }

        public bool IsLegendary { get; set; }

        public override bool Equals(object obj) => this.Equals(obj as PokemonInfo);
        public bool Equals(PokemonInfo p)
        {
            if (p is null) return false;

            return Name == p.Name
                && Habitat == p.Habitat
                && Description == p.Description
                && IsLegendary == p.IsLegendary;
        }

        public override int GetHashCode() => (Name, Habitat, Description, IsLegendary).GetHashCode();
    }
}
