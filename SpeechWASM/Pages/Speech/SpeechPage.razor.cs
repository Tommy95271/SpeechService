using Microsoft.AspNetCore.Components;
using SpeechLibrary.Models;
using System.Net.Http.Json;

namespace SpeechWASM.Pages.Speech
{
    public partial class SpeechPage
    {
        [Inject]
        public HttpClient httpClient { get; set; }
        public int Order { get; set; }

        public List<SpeechModel> SpeechModels { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SpeechModels = new List<SpeechModel>();
            Order = 1;
        }

        private async Task Translate()
        {
            var result = await httpClient.GetFromJsonAsync<SpeechModel>("Speech/TranslateFromMicrophoneAndPlay");
            SpeechModels.Add(new SpeechModel
            {
                Id = result.Id,
                Order = Order++,
                Text = result.Text,
                Translation = result.Translation,
            });
        }
    }
}
