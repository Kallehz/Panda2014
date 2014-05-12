using PandaApp.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaApp.Controllers
{
    public class RequestController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index()
        {
            IEnumerable<Request> requests = (from item in db.GetAllRequests()
                                            orderby item.Upvotes descending
                                            select item).Take(15);
            return View(requests);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Request());
        }

        [HttpPost]
        public ActionResult Create(Request item)
        {
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
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Request r = db.GetRequestById(id);
            if (r != null)
            {
                return View(r);
            }
            return View("NotFound");
        }

        [HttpPost]
        public ActionResult Upvote(string s)
        {
            int id = Convert.ToInt32(s);
            PandaBase panda = new PandaBase();
            Request req = panda.Requests.Single(r => r.ID == id);
            req.Upvotes++;
            panda.SaveChanges();
            Request re = db.GetRequestById(id);
            return View(re);
        }
	}
}