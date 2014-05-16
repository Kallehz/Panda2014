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
            // To have a list of subtitles on the media profile
            MediaAndSubtitles ms = new MediaAndSubtitles();

            ms.m = db.GetMediaById(id);
            ms.subs = db.GetSubtitlesForMedia(ms.m.ID);

            if (ms.m != null)
            {
                return View(ms);
            }

            return View("NotFound");
        }


        // For future options, if we want to
        // create an empty subtitle for a profile.
        // We can create it ourselves.
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