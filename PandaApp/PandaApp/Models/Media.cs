using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PandaApp.Models
{
    public class Media
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string IMDB { get; set; }
    }
}