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
        
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string Author { get; set; }
        public int MediaID { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "YouTube/Vimeo")]
        public string ExternalVideoLink { get; set; }
        public int Upvotes { get; set; }

        [Display(Name = "Added")]
        public DateTime DateCreated { get; set; }

        public bool UpvotedByUser { get; set; }
        
        [Display(Name = "Subtitle link")]
        public string SubtitleLink { get; set; }
        
        public Request()
		{
			DateCreated = DateTime.Now;
		}
    }
}