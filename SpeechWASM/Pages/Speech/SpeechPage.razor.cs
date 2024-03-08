using Microsoft.AspNetCore.Components;
using SpeechLibrary.Enums;
using SpeechLibrary.Models;
using SpeechWASM.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SpeechWASM.Pages.Speech
{
    public partial class SpeechPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        private int order { get; set; }
        private bool isSuccess { get; set; }
        private bool isCancelled { get; set; }
        private bool isStopped { get; set; }
        private bool isSpeaking { get; set; }
        private bool targetEqualsSource { get; set; }
        private string? message { get; set; }
        private LanguageEnum targetLanguage { get; set; }
        private LanguageEnum sourceLanguage { get; set; }
        private List<CardModel> Models { get; set; }
        private List<SpeechEnumModel> sourceLanguages { get; set; }
        private List<SpeechEnumModel> targetLanguages { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Models = new List<CardModel>();
            sourceLanguages = new List<SpeechEnumModel>();
            targetLanguages = new List<SpeechEnumModel>();
            await GetEnums();
            order = 1;
            isStopped = true;
            isSpeaking = true;
            targetLanguage = LanguageEnum.English;
        }

        private async Task Translate()
        {
            isStopped = false;
            isSuccess = true;
            LanguageEnum source, target;
            SpeechRequest req;
            if (Enum.TryParse(targetLanguage.ToString(), out target) && Enum.TryParse(sourceLanguage.ToString(), out source))
            {
                req = new SpeechRequest { SourceLanguage = source, TargetLanguage = target };
            }
            else
            {
                req = null;
            }
            var result = await httpClient.PostAsJsonAsync("Speech/TranslateFromMicrophoneAndPlay", req);
            if (result != null && result.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<SpeechResponse>(await result.Content.ReadAsStringAsync());
                if (content != null)
                {
                    isSuccess = content.IsSuccess;
                    if (isSuccess)
                    {
                        var model = content.Model;
                        Models.Add(new CardModel
                        {
                            Order = order++,
                            Text = model.Text,
                            TextLocale = model.TextLocale,
                            Translation = model.Translation,
                            TranslationLocale = model.TranslationLocale,
                        });
                    }
                    else
                    {
                        if (!isCancelled)
                        {
                            message = content.Message;
                        }
                    }
                }
            }
            isStopped = true;
        }

        private async Task Stop()
        {
            var result = await httpClient.GetAsync("Speech/StopPlayingAudio");
            if (result != null && result.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<SpeechResponse>(await result.Content.ReadAsStringAsync());
                if (content != null)
                {
                    isSuccess = content.IsSuccess;
                    isCancelled = content.IsCancelled;
                    message = content.Message;
                }
            }
        }

        private async Task GetEnums()
        {
            var result = await httpClient.GetAsync("Speech/GetLanguageEnums");
            if (result != null && result.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<List<SpeechEnumModel>>(await result.Content.ReadAsStringAsync());
                if (content != null)
                {
                    sourceLanguages = content;
                    targetLanguages = content;
                }
            }
        }

        private void SetLocale(LanguageEnum value, int languageOrder)
        {
            if (languageOrder == 0)
            {
                sourceLanguage = value;
            }
            else
            {
                targetLanguage = value;
            }
            if (targetLanguage == sourceLanguage)
            {
                targetEqualsSource = true;
                isSpeaking = false;
            }
            else
            {
                targetEqualsSource = false;
                isSpeaking = true;
            }
        }
    }
}
