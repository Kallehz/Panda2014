using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class MediaAndSubtitles
    {
        public Media m { get; set; }
        public IEnumerable<Subtitle> subs { get; set; }
    }
}