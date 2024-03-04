using Microsoft.AspNetCore.Components;
using SpeechLibrary.Enums;
using SpeechLibrary.Models;
using System.Net.Http.Json;
using System.Text.Json;

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
            LanguageEnum source, target;
            SpeechReq req;
            if (Enum.TryParse("0", out source) && Enum.TryParse("2", out target))
            {
                req = new SpeechReq { SourceLanguage = source, TargetLanguage = target };
            }
            else
            {
                req = null;
            }
            var result = await httpClient.PostAsJsonAsync("Speech/TranslateFromMicrophoneAndPlay", req);
            if (result != null && result.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<SpeechModel>(await result.Content.ReadAsStringAsync());
                if (content != null)
                {
                    SpeechModels.Add(new SpeechModel
                    {
                        Id = content.Id,
                        Order = Order++,
                        Text = content.Text,
                        Translation = content.Translation,
                    });
                }
            }
        }
    }
}
