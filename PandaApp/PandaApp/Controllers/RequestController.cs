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
            string username = User.Identity.Name;

            IEnumerable<Request> requests = (from item in db.GetAllRequests()
                                             orderby item.DateCreated descending
                                             select item);

            if (!page.HasValue)
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            if (username.Length != 0)
            {
                foreach (Request req in requests)
                {
                    // If you have upvoted this request it will return true, false otherwise.
                    req.UpvotedByUser = db.GetReqUpBool(req.ID, db.GetUserByName(username).ID);
                }
            }
            /*else
            {
                foreach (Request req in requests)
                {
                    // This is doing nothing, it is already false
                    req.UpvotedByUser = false;
                }
            }*/

            ViewBag.Languages = db.GetLanguageListItems();
            return View(requests.ToPagedList(pageNumber, pageSize));
        }        

        public ActionResult RequestSearch(int? page, string title, string language)
        {
            IEnumerable<Request> req;

            if (language.Length == 0)
            {
                // Gets all requests sorted by upvote count
                req = (from item in db.GetAllRequests()
                       where item.Title.ToLower().Contains(title.ToLower())
                       orderby item.Upvotes descending
                       select item);
            }
            else
            {
                // Gets all requests with the specified language
                req = (from item in db.GetAllRequests()
                       where (item.Title.ToLower().Contains(title.ToLower()) &&
                       (item.Language == language))
                       orderby item.Upvotes descending
                       select item);
            }

            // This is for the upvote system
            string username = User.Identity.Name;
            if (username.Length != 0)
            {
                foreach (Request item in req)
                {
                    // If you have upvoted this request it will return true, false otherwise.
                    item.UpvotedByUser = db.GetReqUpBool(item.ID, db.GetUserByName(username).ID);
                }
            }

            // This is the for the paging
            if (!page.HasValue)
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.Languages = db.GetLanguageListItems();
            return View(req.ToPagedList(pageNumber, pageSize));
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
        public void Upvote(int id)
        {
            //Búið til eintak af PandaBase
            PandaBase panda = new PandaBase();
            
            //Tékkað á því hvort það sé til Upvote með þessari ákveðnu beiðni og notanda. Ef 'true' þá er það ekki til og haldið er áfram.
            if(db.GetReqUpBool(id, db.GetUserByName(User.Identity.Name).ID))
            {
                //fundið viðeigandi beiðni og hækkað 'upvotes' um 1.
                Request req = panda.Requests.Single(re => re.ID == id);
                req.Upvotes++;

                //Bætt við línu í Upvoters töfluna sem inniheldur ID frá beiðni og notanda
                Upvoter upvoter = new Upvoter() { RequestID = id, UserID = db.GetUserByName(User.Identity.Name).ID };
                panda.Upvoters.Add(upvoter);

                //PandaBase er uppfært
                panda.SaveChanges();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult FillRequest(int requestID, string subtitleLink)
        {
            db.FillReq(requestID, subtitleLink);

            return RedirectToAction("Details/" + requestID);
        }
	}
}