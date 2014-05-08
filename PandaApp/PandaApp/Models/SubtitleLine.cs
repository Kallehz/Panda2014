using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class SubtitleLine
    {
        public int ID { get; set; }
        public int subtitleID { get; set; }
        public string text { get; set; }
        TimeSpan timeFrom { get; set; }
        TimeSpan timeTo { get; set; }
    }
}