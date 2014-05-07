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
        public int Year { get; set; }
        public DateTime DateCreated { get; set; }

        public Subtitle()
		{
			DateCreated = DateTime.Now;
		}
    }
}