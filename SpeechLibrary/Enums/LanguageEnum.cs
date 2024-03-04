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
        Taiwanese,
        [Description("en-US")]
        English,
        [Description("ja-JP")]
        Japanese,
        [Description("ko-KR")]
        Korean,
        [Description("vi-VN")]
        Vietnamese,
    }
}
