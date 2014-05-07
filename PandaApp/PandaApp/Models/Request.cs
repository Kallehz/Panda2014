using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Request
    {
        public string title { get; set; }
        public string language { get; set; }
        public string text { get; set; }
        public DateTime dateCreated { get; set; }

        public Request()
		{
			dateCreated = DateTime.Now;
		}
    }
}