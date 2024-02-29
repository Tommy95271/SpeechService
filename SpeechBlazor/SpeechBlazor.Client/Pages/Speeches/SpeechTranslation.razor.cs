using Microsoft.AspNetCore.Components;
using Microsoft.CognitiveServices.Speech;
using SpeechLibrary.Services;

namespace SpeechBlazor.Client.Pages.Speeches
{
    public partial class SpeechTranslation
    {
        [Inject]
        private SpeechService _speechService { get; set; }

        public async Task TranslateFromMicrophone()
        {
            var speechTranslationConfig = SpeechTranslationConfig.FromSubscription("key", "region");
            speechTranslationConfig.SpeechRecognitionLanguage = "zh-TW";
            speechTranslationConfig.AddTargetLanguage("ja");

            var text = await _speechService.TranslateFromMicrophone(speechTranslationConfig);
            await _speechService.PlayTextAsAudio(speechTranslationConfig, text);
        }
    }
}
