using Microsoft.AspNetCore.Components;

namespace SpeechWASM.Components
{
    public partial class Card
    {
        [Parameter]
        public int Order { get; set; }
        [Parameter]
        public string? Text { get; set; }
        [Parameter]
        public string? TextLocale { get; set; }
        [Parameter]
        public string? Translation { get; set; }
        [Parameter]
        public string? TranslationLocale { get; set; }
    }
}
