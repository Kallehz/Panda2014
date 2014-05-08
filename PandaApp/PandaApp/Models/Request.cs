using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Request
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
        public DateTime dateCreated { get; set; }

        public Request()
		{
			dateCreated = DateTime.Now;
		}
    }
}