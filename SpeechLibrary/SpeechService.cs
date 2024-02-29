using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;

namespace SpeechLibrary
{
    public class SpeechService
    {
        public async Task TranslateFromMicrophoneAndPlay(SpeechTranslationConfig speechTranslationConfig)
        {
            using var speechSynthesizer = new SpeechSynthesizer(speechTranslationConfig);

            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            Console.WriteLine("Speak into your microphone.");
            var stopRecognition = new TaskCompletionSource<int>();

            translationRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                {
                    var translation = e.Result.Translations.Values.FirstOrDefault();
                    Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                    Console.WriteLine($"RECOGNIZED: Translation={translation}");
                    speechSynthesizer.SpeakTextAsync(translation);
                    File.AppendAllLinesAsync("./output.txt", new List<string> { e.Result.Text });
                }
                else if (e.Result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
            };

            translationRecognizer.Canceled += (s, e) =>
            {
                Console.WriteLine($"CANCELED: Reason={e.Reason}");

                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }

                stopRecognition.TrySetResult(0);
            };

            translationRecognizer.SessionStopped += (s, e) =>
            {
                Console.WriteLine("\n    Session stopped event.");
                stopRecognition.TrySetResult(0);
            };

            await translationRecognizer.StartContinuousRecognitionAsync();

            // Waits for completion. Use Task.WaitAny to keep the task rooted.
            Task.WaitAny(new[] { stopRecognition.Task });

            // Make the following call at some point to stop recognition:
            // await speechRecognizer.StopContinuousRecognitionAsync();


            //var result = await speechRecognizer.RecognizeOnceAsync();
            //Console.WriteLine($"RECOGNIZED: Text={result}");
            //var translationRecognitionResult = await translationRecognizer.RecognizeOnceAsync();
            //await OutputSpeechRecognitionResult(translationRecognitionResult);
        }

    }
}
