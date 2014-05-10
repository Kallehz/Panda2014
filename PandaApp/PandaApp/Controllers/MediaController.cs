using PandaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaApp.Controllers
{
    public class MediaController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MediaProfile(int? id)
        {
            if (id != null)
            {
                object model = db.GetMediaById(id.Value);

                return View(model);
            }

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