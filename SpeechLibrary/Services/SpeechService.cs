using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

namespace SpeechLibrary.Services
{
    public class SpeechService
    {
        public async Task<string?> TranslateFromMicrophone(SpeechTranslationConfig speechTranslationConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            var result = await translationRecognizer.RecognizeOnceAsync();
            return result.Translations.Values.FirstOrDefault();
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
