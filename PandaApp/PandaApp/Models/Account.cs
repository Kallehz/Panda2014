using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public Account()
		{
			DateCreated = DateTime.Now;
		}
    }
}