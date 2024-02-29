using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;
using SpeechLibrary.Models;

namespace SpeechLibrary.Services
{
    public class SpeechService
    {
        public async Task<SpeechModel> TranslateFromMicrophone(SpeechTranslationConfig speechTranslationConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            var result = await translationRecognizer.RecognizeOnceAsync();

            return new SpeechModel
            {
                Id = result.ResultId,
                Text = result.Text,
                Translation = result.Translations.Values.FirstOrDefault()
            };
        }

        public async Task PlayTextAsAudio(SpeechTranslationConfig speechTranslationConfig, string? text)
        {
            using var speechSynthesizer = new SpeechSynthesizer(speechTranslationConfig);
            using var result = await speechSynthesizer.SpeakTextAsync(text);
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
    }
}
