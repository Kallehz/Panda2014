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
        public ActionResult Index()
        {
            //PandaBase db = new PandaBase();
            //var subtitle = from subtitles in db.Subtitles where subtitles.ID < 10 select subtitles;
            return View();
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
               /* Vista í grunn */ 
               return RedirectToAction("Index");
            }
            return View(item);

        }

        [HttpGet]
        public ActionResult Requests()
        {
            ViewBag.Message = "View requests";

            return View();
        }

        [HttpGet]
        public ActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(Request item)
        {
            ViewBag.Message = "View requests";
            if (ModelState.IsValid)
            {
                /* Vista í grunn */                
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