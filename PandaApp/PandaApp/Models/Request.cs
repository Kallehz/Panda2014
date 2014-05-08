using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PandaApp.Models
{
    public class Request
    {
        public int ID { get; set; }
        
        public int MediaID { get; set; }
        
        public int AuthorID { get; set; }
        
        [Display(Name = "Language")]
        public int LanguageID { get; set; }

        [Display(Name = "YouTube/Vimeo")]
        public string ExternalVideoLink { get; set; }
        
        public int Upvotes { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public Request()
		{
			DateCreated = DateTime.Now;
		}
    }
}