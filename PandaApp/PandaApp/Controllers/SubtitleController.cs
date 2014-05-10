using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaApp.Models;


namespace PandaApp.Controllers
{
    public class SubtitleController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult DisplayEditPage(int subtitleID)
        {
            return View(new EditViewModel(subtitleID));
        }

        public ActionResult SearchResult(string title, string language)
        {
            SubAndReq SandR = new SubAndReq();

            SandR.Requests = (from item in db.GetAllRequests()
                              where (item.Title.Contains(title) && 
                              (item.Language == language))
                              orderby item.Upvotes descending
                              select item).Take(15);

            SandR.Subtitles = (from item in db.GetAllSubtitles()
                               where (item.Title.Contains(title) &&
                               (item.Language == language))
                               orderby item.DateCreated descending
                               select item).Take(15);

            return View(SandR);
        }
	}

}