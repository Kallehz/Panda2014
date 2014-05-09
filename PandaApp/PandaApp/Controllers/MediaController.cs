using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaApp.Controllers
{
    public class MediaController : Controller
    {
        // GET: /Media/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayMediaProfile(int mediaID)
        {
            return View();
        }

        public void GetAvailableSubtitles(int mediaID)
        {

        }

        public void GetIMDBInfo(int mediaID)
        {
            // USE IMDB API
        }
	}
}