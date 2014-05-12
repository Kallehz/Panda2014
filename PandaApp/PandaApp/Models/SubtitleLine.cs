using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class SubtitleLine
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public int SubtitleID { get; set; }
        public string Text { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
}