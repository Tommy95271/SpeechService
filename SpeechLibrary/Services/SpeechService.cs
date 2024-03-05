using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;
using SpeechLibrary.Enums;
using SpeechLibrary.Helpers;
using SpeechLibrary.Models;

namespace SpeechLibrary.Services
{
    public class SpeechService
    {
        public TranslationRecognizer translationRecognizer { get; set; }
        public SpeechSynthesizer speechSynthesizer { get; set; }

        public async Task<SpeechResponse> TranslateFromMicrophoneAsync(SpeechTranslationConfig speechTranslationConfig, SpeechRequest request)
        {
            speechTranslationConfig.SpeechRecognitionLanguage = request.SourceLanguage.GetLanguageDescription();
            speechTranslationConfig.AddTargetLanguage(request.TargetLanguage.GetLanguageDescription());
            speechTranslationConfig.SpeechSynthesisLanguage = request.TargetLanguage.GetLanguageDescription();
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            var result = await translationRecognizer.RecognizeOnceAsync();
            if (result.Reason == ResultReason.TranslatedSpeech)
            {
                return new SpeechResponse
                {
                    IsSuccess = true,
                    Message = "翻譯及語音成功",
                    Model = new SpeechModel
                    {
                        Id = result.ResultId,
                        Text = result.Text,
                        TextLocale = request.SourceLanguage.GetLanguageName(),
                        Translation = result.Translations.Values.FirstOrDefault(),
                        TranslationLocale = request.TargetLanguage.GetLanguageName(),
                    }
                };
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                return new SpeechResponse
                {
                    IsSuccess = false,
                    Message = "翻譯及語音失敗",
                    Model = new SpeechModel
                    {
                        Id = string.Empty,
                        Text = string.Empty,
                        TextLocale = string.Empty,
                        Translation = string.Empty,
                        TranslationLocale = string.Empty,
                    }
                };
            }
            else
            {
                return new SpeechResponse
                {
                    IsSuccess = false,
                    Message = "翻譯及語音失敗",
                    Model = new SpeechModel
                    {
                        Id = string.Empty,
                        Text = string.Empty,
                        TextLocale = string.Empty,
                        Translation = string.Empty,
                        TranslationLocale = string.Empty,
                    }
                };
            }
        }

        public async Task PlayTextAsAudioAsync(SpeechTranslationConfig speechTranslationConfig, string text)
        {
            speechSynthesizer = new SpeechSynthesizer(speechTranslationConfig);
            var result = await speechSynthesizer.SpeakTextAsync(text);
            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                // Audio playback completed
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                // Handle cancellation
            }
        }

        public async Task<SpeechResponse> StopAudioAsync()
        {
            await translationRecognizer.StopContinuousRecognitionAsync();
            await speechSynthesizer.StopSpeakingAsync();
            return new SpeechResponse { IsSuccess = true, IsCancelled = true, Message = "翻譯及語音已取消" };
        }

        public List<SpeechEnumModel> GetLanguageEnums()
        {
            return TextHelper.GetEnumDescriptions(typeof(LanguageEnum))
                .Select(l => new SpeechEnumModel
                {
                    Value = (LanguageEnum)l.Value,
                    Locale = l.Locale,
                    Text = l.Name,
                }).ToList();
        }
    }
}
