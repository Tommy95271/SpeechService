using SpeechLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLibrary.Models
{
    public class SpeechRequest
    {
        public LanguageEnum SourceLanguage { get; set; }
        public LanguageEnum TargetLanguage { get; set; }
    }
}
