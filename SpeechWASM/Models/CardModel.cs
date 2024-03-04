namespace SpeechWASM.Models
{
    public class CardModel
    {
        public int Order { get; set; }
        public string? Text { get; set; }
        public string? TextLocale { get; set; }
        public string? Translation { get; set; }
        public string? TranslationLocale { get; set; }
    }
}
