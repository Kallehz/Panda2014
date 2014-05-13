using PandaApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PagedList;


namespace PandaApp.Controllers
{
    public class HomeController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index(int? page)
        {
            var subs = (from item in db.GetAllSubtitles()
                               orderby item.DateCreated descending
                               select item);

            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.Languages = db.GetLanguageListItems();
            return View(subs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FAQ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upvote(string s)
        {
            int id = Convert.ToInt32(s);
            PandaBase panda = new PandaBase();
            Request req = panda.Requests.Single(re => re.ID == id);
            req.Upvotes++;

            Upvoter upvoter = new Upvoter() { RequestID = id, UserID = db.GetUserByName(User.Identity.Name).ID };
            panda.Upvoters.Add(upvoter);

            if (!db.GetReqUpBool(id, db.GetUserByName(User.Identity.Name).ID))
            {
                panda.SaveChanges();
            }

            ReqUp r = new ReqUp();
            r.request = db.GetRequestById(id);
            Account acc = db.GetUserByName(User.Identity.Name);

            r.upvoted = db.GetReqUpBool(id, acc.ID);

            return RedirectToAction("Index");
        }
    }
}