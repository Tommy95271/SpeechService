using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLibrary.Enums
{
    public enum LanguageEnum
    {
        [Description("zh-TW")]
        Taiwan,
        [Description("en-US")]
        English,
        [Description("ja-JP")]
        Japan,
        [Description("ko-KR")]
        Korea,
    }
}
