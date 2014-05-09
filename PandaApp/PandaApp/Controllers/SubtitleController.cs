using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaApp.Models;


namespace PandaApp.Controllers
{
    public class SubtitleController : Controller
    {
        public ActionResult DisplayEditPage(int subtitleID)
        {
            return View(new EditViewModel(subtitleID));
        }
	}

}