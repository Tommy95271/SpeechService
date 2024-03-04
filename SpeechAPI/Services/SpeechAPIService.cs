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

        public async Task<SpeechResponse> TranslateFromMicrophoneAsync(SpeechRequest request)
        {
            var result = await _speechService.TranslateFromMicrophoneAsync(_speechTranslationConfig, request);
            await PlayTextAsAudioAsync(result.Model.Translation ?? "test");
            return result;
        }

        public async Task PlayTextAsAudioAsync(string text)
        {
            await _speechService.PlayTextAsAudioAsync(_speechTranslationConfig, text);
        }

        public List<SpeechEnumModel> GetLanguageEnumsAsync()
        {
            var result = _speechService.GetLanguageEnums();
            return result;
        }

        public async Task<SpeechResponse> StopAudioAsync()
        {
            return await _speechService.StopAudioAsync(_speechTranslationConfig);
        }
    }
}
