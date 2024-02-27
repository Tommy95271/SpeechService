using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech.Translation;

namespace SpeechServices.Services
{
    internal class SpeechService
    {
        internal async Task Execute()
        {
            var speechConfig = SpeechConfig.FromSubscription("key", "region");
            var speechTranslationConfig = SpeechTranslationConfig.FromSubscription("key", "region");
            speechTranslationConfig.SpeechRecognitionLanguage = "zh-TW";
            speechTranslationConfig.AddTargetLanguage("en");
            speechConfig.SpeechRecognitionLanguage = "zh-TW";
            speechConfig.SetProperty(PropertyId.Speech_SegmentationSilenceTimeoutMs, "1000");
            speechConfig.SetProperty(PropertyId.Speech_LogFilename, "./mylog2");
            //await FromFile(speechConfig);
            //await FromMicrophone(speechConfig);
            await TranslateFromMicrophone(speechTranslationConfig);
        }

        internal async Task FromFile(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromWavFileInput("D:\\Personal\\recordings\\20230504P.wav");
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            var stopRecognition = new TaskCompletionSource<int>();

            //speechRecognizer.Recognizing += (s, e) =>
            //{
            //    Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
            //};

            speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                    File.AppendAllLinesAsync("./output.txt", new List<string> { e.Result.Text });
                }
                else if (e.Result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
            };

            speechRecognizer.Canceled += (s, e) =>
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

            speechRecognizer.SessionStopped += (s, e) =>
            {
                Console.WriteLine("\n    Session stopped event.");
                stopRecognition.TrySetResult(0);
            };

            await speechRecognizer.StartContinuousRecognitionAsync();

            // Waits for completion. Use Task.WaitAny to keep the task rooted.
            Task.WaitAny(new[] { stopRecognition.Task });

            // Make the following call at some point to stop recognition:
            // await speechRecognizer.StopContinuousRecognitionAsync();


            //var result = await speechRecognizer.RecognizeOnceAsync();
            //Console.WriteLine($"RECOGNIZED: Text={result}");
        }

        internal async Task FromMicrophone(SpeechConfig speechConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

            Console.WriteLine("Speak into your microphone.");
            var stopRecognition = new TaskCompletionSource<int>();

            speechRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.RecognizedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                    File.AppendAllLinesAsync("./output.txt", new List<string> { e.Result.Text });
                }
                else if (e.Result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                }
            };

            speechRecognizer.Canceled += (s, e) =>
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

            speechRecognizer.SessionStopped += (s, e) =>
            {
                Console.WriteLine("\n    Session stopped event.");
                stopRecognition.TrySetResult(0);
            };

            await speechRecognizer.StartContinuousRecognitionAsync();

            // Waits for completion. Use Task.WaitAny to keep the task rooted.
            Task.WaitAny(new[] { stopRecognition.Task });

            // Make the following call at some point to stop recognition:
            // await speechRecognizer.StopContinuousRecognitionAsync();


            //var result = await speechRecognizer.RecognizeOnceAsync();
            //Console.WriteLine($"RECOGNIZED: Text={result}");
        }

        internal async Task TranslateFromMicrophone(SpeechTranslationConfig speechTranslationConfig)
        {
            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var translationRecognizer = new TranslationRecognizer(speechTranslationConfig, audioConfig);

            Console.WriteLine("Speak into your microphone.");
            var stopRecognition = new TaskCompletionSource<int>();

            translationRecognizer.Recognized += (s, e) =>
            {
                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                {
                    Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                    Console.WriteLine($"RECOGNIZED: Translation={e.Result.Translations.Values.FirstOrDefault()}");
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

        internal async Task OutputSpeechRecognitionResult(TranslationRecognitionResult translationRecognitionResult)
        {
            switch (translationRecognitionResult.Reason)
            {
                case ResultReason.TranslatedSpeech:
                    Console.WriteLine($"RECOGNIZED: Text={translationRecognitionResult.Text}");
                    foreach (var element in translationRecognitionResult.Translations)
                    {
                        Console.WriteLine($"TRANSLATED into '{element.Key}': {element.Value}");
                    }
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(translationRecognitionResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
            }

        }
    }
}
