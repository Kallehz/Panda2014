using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class EditViewModel
    {
        public int SubtitleID { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public List<SubtitleLine> Lines { get; set; }
    }
}