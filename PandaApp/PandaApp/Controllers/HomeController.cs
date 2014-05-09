using PandaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PandaApp.Controllers
{
    public class HomeController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index()
        {
            SubAndReq SandR = new SubAndReq();

            SandR.Requests = (from item in db.GetAllRequests()
                              orderby item.Upvotes descending
                              select item).Take(15);

            SandR.Subtitles = (from item in db.GetAllSubtitles()
                               orderby item.DateCreated descending
                               select item).Take(15);
            // Comment.
            return View(SandR);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload subtitle";

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file)
        {
            //TODO send file to database table!
            if (ModelState.IsValid)
            {
                db.AddSubtitle(item);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(item);

        }

        [HttpGet]
        public ActionResult Requests()
        {
            ViewBag.Message = "View requests";

            IQueryable<Request> requests = (from item in db.GetAllRequests()
                                             orderby item.DateCreated descending
                                             select item).Take(15);
            return View(requests);
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(Request item)
        {
            ViewBag.Message = "Create requests";

            if (ModelState.IsValid)
            {
                db.AddRequest(item);
                db.Save();
                return RedirectToAction("Requests");
            }

            return View();
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ";

            return View();
        }
    }
}