﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Subtitle
    {
        public int ID { get; set; }

        [Display(Name = "Language")]
        public int LanguageID { get; set; }
        public int AuthorID { get; set; }

        [Display(Name = "Movie Title")]
        public int MediaID { get; set; }
        public DateTime DateCreated { get; set; }
        public Subtitle()
		{
			DateCreated = DateTime.Now;
		}
    }
}