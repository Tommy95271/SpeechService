﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLibrary.Models
{
    public class SpeechModel
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public string? Text { get; set; }
        public string? Translation { get; set; }
    }
}
