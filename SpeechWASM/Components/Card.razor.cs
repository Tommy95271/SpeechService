using Microsoft.AspNetCore.Components;
using SpeechWASM.Models;

namespace SpeechWASM.Components
{
    public partial class Card
    {
        [Parameter]
        public CardModel Model { get; set; }
    }
}
