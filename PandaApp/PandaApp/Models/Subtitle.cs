using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Subtitle
    {
        public int ID { get; set; }
        public int LanguageID { get; set; }
        public int AuthorID { get; set; }
        public int MediaID { get; set; }
        public DateTime DateCreated { get; set; }

        public Subtitle()
		{
			dateCreated = DateTime.Now;
		}
    }
}