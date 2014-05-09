using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class SubAndReq
    {
        public IEnumerable<Subtitle> Subtitles { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}