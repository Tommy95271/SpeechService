using Microsoft.AspNetCore.Components;

namespace SpeechBlazor.Client.Pages.Speeches
{
    public partial class Card
    {
        [Parameter]
        public int Order { get; set; }
        [Parameter]
        public string? Text { get; set; }
        [Parameter]
        public string? Translation { get; set; }
    }
}
