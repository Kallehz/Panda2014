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
        PandaBase db = new PandaBase();

        public ActionResult Index()
        {
            SubAndReq SandR = new SubAndReq();

            SandR.Requests = (from item in db.Requests
                              orderby item.Upvotes descending
                              select item).Take(15);

            SandR.Subtitles = (from item in db.Subtitles
                               orderby item.DateCreated descending
                               select item).Take(15);

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
        public ActionResult Upload(Subtitle item)
        {
            if (ModelState.IsValid)
            {
                db.Subtitles.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);

        }

        [HttpGet]
        public ActionResult Requests()
        {
            ViewBag.Message = "View requests";

            IEnumerable<Request> requests = (from item in db.Requests
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
                db.Requests.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
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