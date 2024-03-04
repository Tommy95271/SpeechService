using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpeechLibrary.Models
{
    public class SpeechModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string? Text { get; set; }
        [JsonPropertyName("textLocale")]
        public string? TextLocale { get; set; }
        [JsonPropertyName("translation")]
        public string? Translation { get; set; }
        [JsonPropertyName("translationLocale")]
        public string? TranslationLocale { get; set; }
    }
}
