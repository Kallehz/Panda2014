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
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

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