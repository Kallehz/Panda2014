using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class EditViewModel
    {
        public int subtitleID { get; set; }
        public string title { get; set; }
        public string language { get; set; }
        public List<SubtitleLine> lines { get; set; }
    }
}