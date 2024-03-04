using Microsoft.AspNetCore.Components;
using SpeechLibrary.Enums;
using SpeechLibrary.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SpeechWASM.Pages.Speech
{
    public partial class SpeechPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }
        private int order { get; set; }
        private LanguageEnum targetLanguage { get; set; }
        private LanguageEnum sourceLanguage { get; set; }
        private List<SpeechModel> speechModels { get; set; }
        private List<SpeechEnumModel> sourceLanguages { get; set; }
        private List<SpeechEnumModel> targetLanguages { get; set; }

        protected override async Task OnInitializedAsync()
        {
            speechModels = new List<SpeechModel>();
            sourceLanguages = new List<SpeechEnumModel>();
            targetLanguages = new List<SpeechEnumModel>();
            await GetEnums();
            order = 1;
        }

        private async Task Translate()
        {
            LanguageEnum source, target;
            SpeechReq req;
            if (Enum.TryParse(targetLanguage.ToString(), out target) && Enum.TryParse(sourceLanguage.ToString(), out source))
            {
                req = new SpeechReq { SourceLanguage = source, TargetLanguage = target };
            }
            else
            {
                req = null;
            }
            var result = await httpClient.PostAsJsonAsync("Speech/TranslateFromMicrophoneAndPlay", req);
            if (result != null && result.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<SpeechModel>(await result.Content.ReadAsStringAsync());
                if (content != null)
                {
                    speechModels.Add(new SpeechModel
                    {
                        Id = content.Id,
                        Order = order++,
                        Text = content.Text,
                        TextLocale = content.TextLocale,
                        Translation = content.Translation,
                        TranslationLocale = content.TranslationLocale,
                    });
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
        }
    }
}
