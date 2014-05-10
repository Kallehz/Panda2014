using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Text {get; set; }
        public int SubtitleId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Subtitle Subtitle { get; set; }
        public Comment()
		{
			DateCreated = DateTime.Now;
		}
    }
}