namespace Pokedex_Ultra.Models
{
    public class TranslationResponse
    {
        public TranslatedContent Contents { get; set; }

        public class TranslatedContent
        {
            public string Translated { get; set; }
        }
    }
}
