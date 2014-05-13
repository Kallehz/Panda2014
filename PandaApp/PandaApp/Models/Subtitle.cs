using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Subtitle
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Language")]
        public string Language { get; set; }
        public string Author { get; set; }

        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        public int MediaID { get; set; }

        [Display(Name = "Date Posted")]
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Subtitle()
		{
			DateCreated = DateTime.Now;
		}
    }
}