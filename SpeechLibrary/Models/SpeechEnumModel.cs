using SpeechLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLibrary.Models
{
    public class SpeechEnumModel
    {
        public LanguageEnum Value { get; set; }
        public string Locale { get; set; }
        public string Text { get; set; }
    }
}
