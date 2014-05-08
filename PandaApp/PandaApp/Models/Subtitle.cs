using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Subtitle
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string subText { get; set; }
        public string Year { get; set; }
        public DateTime dateCreated { get; set; }

        public Subtitle()
		{
			dateCreated = DateTime.Now;
		}
    }
}