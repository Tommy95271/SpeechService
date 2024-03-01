using Microsoft.CognitiveServices.Speech;
using SpeechLibrary.Models;
using SpeechLibrary.Services;

namespace SpeechAPI.Services
{
    public class SpeechAPIService
    {
        private string Key { get; set; }
        private string Region { get; set; }
        private readonly SpeechService _speechService;

        public SpeechTranslationConfig _speechTranslationConfig { get; set; }

        public SpeechAPIService(SpeechService speechService)
        {
            Key = "key";
            Region = "region";
            _speechService = speechService;
            _speechTranslationConfig = SpeechTranslationConfig.FromSubscription(Key, Region);
        }

        public async Task<SpeechModel> TranslateFromMicrophoneAsync()
        {
            _speechTranslationConfig.SpeechRecognitionLanguage = "zh-TW";
            _speechTranslationConfig.AddTargetLanguage("ja-JP");
            _speechTranslationConfig.SpeechSynthesisLanguage = "ja-JP";
            var result = await _speechService.TranslateFromMicrophoneAsync(_speechTranslationConfig);
            await PlayTextAsAudioAsync(result.Translation ?? "test");
            return result;
        }

        public async Task PlayTextAsAudioAsync(string text)
        {
            await _speechService.PlayTextAsAudioAsync(_speechTranslationConfig, text);
        }

        public async Task StopAudioAsync()
        {
            await _speechService.StopAudioAsync(_speechTranslationConfig);
        }
    }
}
