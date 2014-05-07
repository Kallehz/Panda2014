using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Subtitle
    {
        public string title { get; set; }
        public string language { get; set; }
        public string year { get; set; }
        public DateTime dateCreated { get; set; }

        public Subtitle()
		{
			dateCreated = DateTime.Now;
		}
    }
}