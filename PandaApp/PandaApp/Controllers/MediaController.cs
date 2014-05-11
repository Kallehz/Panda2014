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

        public ActionResult MediaProfile(int id)
        {
            Media m = db.GetMediaById(id);
            if (m != null)
            {
                return View(m);
            }

            return View("NotFound");
        }

        public void GetAvailableSubtitles(int mediaID)
        {

        }

        public void GetIMDBInfo(int mediaID)
        {
            // USE IMDB API
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Media());
        }

        [HttpPost]
        public ActionResult Create(FormCollection formData)
        {
            Media m = new Media();
            UpdateModel(m);
            db.AddMedia(m);
            return RedirectToAction("Index");
        }
	}
}