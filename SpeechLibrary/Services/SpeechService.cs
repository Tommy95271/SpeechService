using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;
using SpeechLibrary.Models;

namespace SpeechLibrary.Services
{
    public class SpeechService
    {
        public SpeechTranslationConfig translationConfig;
        public TranslationRecognizer translationRecognizer;
        public SpeechSynthesizer speechSynthesizer;

        public async Task<SpeechModel> TranslateFromMicrophoneAsync(SpeechTranslationConfig speechTranslationConfig)
        {
            var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            var result = await translationRecognizer.RecognizeOnceAsync();
            if (result.Reason == ResultReason.TranslatedSpeech)
            {
                return new SpeechModel
                {
                    Id = result.ResultId,
                    Text = result.Text,
                    Translation = result.Translations.Values.FirstOrDefault()
                };

            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                return new SpeechModel
                {
                    Id = "Nothing",
                    Text = "Nothing",
                    Translation = "Nothing"
                };
            }
            else
            {
                return new SpeechModel
                {
                    Id = "Nothing",
                    Text = "Nothing",
                    Translation = "Nothing"
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

        public async Task StopAudioAsync(SpeechTranslationConfig speechTranslationConfig)
        {
            speechSynthesizer = new SpeechSynthesizer(speechTranslationConfig);
            await translationRecognizer.StopContinuousRecognitionAsync();
            await speechSynthesizer.StopSpeakingAsync();
        }
    }
}
