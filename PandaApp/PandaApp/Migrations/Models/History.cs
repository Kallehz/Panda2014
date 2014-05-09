using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class History
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Description { get; set; }
        public int SubtitleID { get; set; }
        public int RequestID { get; set; }
    }
}