using PandaApp.Models;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace PandaApp.Controllers
{
    public class RequestController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index(int? page)
        {
            IEnumerable<Request> requests = (from item in db.GetAllRequests()
                                             orderby item.DateCreated descending
                                             select item);

            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            if (db.GetUserByName(User.Identity.Name) != null)
            {
                foreach (Request req in requests)
                {
                    if (db.GetReqUpBool(req.ID, db.GetUserByName(User.Identity.Name).ID))
                    {
                        req.UpvotedByUser = true;
                    }
                    else
                    {
                        req.UpvotedByUser = false;
                    }
                }
            }
            else
            {
                foreach (Request req in requests)
                {
                    req.UpvotedByUser = false;
                }
            }

            ViewBag.Languages = db.GetLanguageListItems();
            return View(requests.ToPagedList(pageNumber, pageSize));
        }        

        public ActionResult RequestSearch(string title, string language)
        {
            IEnumerable<Request> req;

            if (language == "" || language == null)
            {
                req = (from item in db.GetAllRequests()
                       where item.Title.ToLower().Contains(title.ToLower())
                       orderby item.Upvotes descending
                       select item).Take(15);
            }
            else
            {
                req = (from item in db.GetAllRequests()
                       where (item.Title.ToLower().Contains(title.ToLower()) &&
                       (item.Language == language))
                       orderby item.Upvotes descending
                       select item).Take(15);
            }


            ViewBag.Languages = db.GetLanguageListItems();
            return View(req);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Languages = db.GetLanguageListItems();
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
            ViewBag.Languages = db.GetLanguageListItems();
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            ReqUp r = new ReqUp();
            r.request = db.GetRequestById(id);
            Account acc = db.GetUserByName(User.Identity.Name);

            if (db.GetUserByName(User.Identity.Name) != null)
            {
                r.upvoted = db.GetReqUpBool(id, acc.ID);
            }
            else
            {
                r.upvoted = false;
            }
            

            if (r != null)
            {
                return View(r);
            }
            return View("NotFound");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upvote(string s)
        {
            int id = Convert.ToInt32(s);
            PandaBase panda = new PandaBase();
            Debug.WriteLine("TESTING: " + id);
            Request req = panda.Requests.Single(re => re.ID == id);
            req.Upvotes++;

            Upvoter upvoter = new Upvoter() { RequestID = id, UserID = db.GetUserByName(User.Identity.Name).ID };
            panda.Upvoters.Add(upvoter);

            if(db.GetReqUpBool(id, db.GetUserByName(User.Identity.Name).ID))
            {
                panda.SaveChanges();
            }
            
            ReqUp r = new ReqUp();
            r.request = db.GetRequestById(id);
            Account acc = db.GetUserByName(User.Identity.Name);

            r.upvoted = db.GetReqUpBool(id, acc.ID);

            return View(r);
        }

        [HttpPost]
        public ActionResult Upvote2(string s)
        {
            int id = Convert.ToInt32(s);
            PandaBase panda = new PandaBase();
            Debug.WriteLine("TESTING: " + id);
            Request req = panda.Requests.Single(re => re.ID == id);
            req.Upvotes++;

            Upvoter upvoter = new Upvoter() { RequestID = id, UserID = db.GetUserByName(User.Identity.Name).ID };
            panda.Upvoters.Add(upvoter);

            if (db.GetReqUpBool(id, db.GetUserByName(User.Identity.Name).ID))
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