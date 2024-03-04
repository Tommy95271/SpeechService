using SpeechLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpeechLibrary.Models
{
    public class SpeechEnumModel
    {
        [JsonPropertyName("value")]
        public LanguageEnum Value { get; set; }
        [JsonPropertyName("locale")]
        public string Locale { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
