using Microsoft.AspNetCore.Components;
using Microsoft.CognitiveServices.Speech;
using SpeechLibrary.Models;
using SpeechLibrary.Services;

namespace SpeechBlazor.Client.Pages.Speeches
{
    public partial class SpeechTranslation
    {
        [Inject]
        private SpeechService _speechService { get; set; }

        public int Order { get; set; }

        public List<SpeechModel> SpeechModels { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SpeechModels = new List<SpeechModel>();
            Order = 1;
        }
        public async Task TranslateFromMicrophone()
        {
            var speechTranslationConfig = SpeechTranslationConfig.FromSubscription("key", "region");
            speechTranslationConfig.SpeechRecognitionLanguage = "zh-TW";
            speechTranslationConfig.AddTargetLanguage("ja-JP");
            speechTranslationConfig.SpeechSynthesisLanguage = "ja-JP";

            var result = await _speechService.TranslateFromMicrophone(speechTranslationConfig);
            SpeechModels.Add(new SpeechModel
            {
                Id = result.Id,
                Order = Order++,
                Text = result.Text,
                Translation = result.Translation,
            });
            StateHasChanged();
            await _speechService.PlayTextAsAudio(speechTranslationConfig, result.Translation);
        }
    }
}
