using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechServices.Services
{
    internal class SpeechService
    {
        internal async Task Execute()
        {
            var speechConfig = SpeechConfig.FromSubscription("key", "region");
            speechConfig.SpeechRecognitionLanguage = "zh-TW";
            speechConfig.SetProperty(PropertyId.Speech_SegmentationSilenceTimeoutMs, "5000");
            speechConfig.SetProperty(PropertyId.Speech_LogFilename, "./mylog2");
            await FromFile(speechConfig);
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
    }
}
