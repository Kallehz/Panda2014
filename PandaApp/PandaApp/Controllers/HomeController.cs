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

            if(db.GetUserByName(User.Identity.Name) != null)
            {
                foreach (Request req in SandR.Requests)
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
                foreach (Request req in SandR.Requests)
                {
                    req.UpvotedByUser = false;
                }
            }
            

            ViewBag.Languages = db.GetLanguageListItems();
            return View(SandR);
        }

        public ActionResult FAQ()
        {
            return View();
        }
    }
}