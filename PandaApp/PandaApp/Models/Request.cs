using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public string Language { get; set; }
        public string VideoLink { get; set; }
        public DateTime DateCreated { get; set; }

        public Request()
		{
			DateCreated = DateTime.Now;
		}
    }
}