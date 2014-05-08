using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class History
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public int SubtitleId { get; set; }
    }
}