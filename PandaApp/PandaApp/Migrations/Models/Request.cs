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

        [Display(Name = "Movie Title")]
        public int MediaID { get; set; }
        public int AuthorID { get; set; }
        [Display(Name = "Requested Language")]
        public int LanguageID { get; set; }

        [Display(Name = "Youtube/Vimeo")]
        public string ExternalVideoLink { get; set; }
        public int Upvotes { get; set; }
        public DateTime DateCreated { get; set; }
        public Request()
		{
			DateCreated = DateTime.Now;
		}
    }
}