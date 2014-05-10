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

            return View(SandR);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload subtitle";    

            return View(new Media());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file)
        {
            //TODO send file to database table!
            if (ModelState.IsValid)
            {
                item.Author = User.Identity.Name;
                db.AddSubtitle(item);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(item);
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
        public ActionResult Profile()
        {

            IQueryable<Request> UserProfile = (from item in db.GetAllRequests()
                                               where item.Author == User.Identity.Name
                                               orderby item.DateCreated descending
                                               select item).Take(10);
            return View(UserProfile);
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
                if (User.Identity.Name.Length == 0)
                {
                    item.Author = "Guest";
                }
                else
                {
                    item.Author = User.Identity.Name;
                }
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