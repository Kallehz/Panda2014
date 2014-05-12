using System;
using System.Diagnostics;
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

        public ActionResult Edit(int subtitleID)
        {
            return View(new EditViewModel(subtitleID));
        }
        public void UpdateSubtitleLine(string text)
        {
            Debug.WriteLine(text);
            /*
            Debug.WriteLine("Adding line " + ID + ": " + newText);
            PandaBase DB = new PandaBase();
            PandaApp.Models.SubtitleLine newLine = DB.SubtitleLines.Find(ID);
            DB.SubtitleLines.Remove(newLine);
            newLine.Text = newText;
            DB.SubtitleLines.Add(newLine);
            DB.SaveChanges();
            */
        }

        public ActionResult SearchResult(string title, string language)
        {
            SubAndReq SandR = new SubAndReq();

            if(language != "all")
            {
                SandR.Subtitles = (from item in db.GetAllSubtitles()
                                   where (item.Title.Contains(title) &&
                                   (item.Language == language))
                                   orderby item.DateCreated descending
                                   select item).Take(15);

                SandR.Requests = (from item in db.GetAllRequests()
                                  where (item.Title.Contains(title) &&
                                  (item.Language == language))
                                  orderby item.Upvotes descending
                                  select item).Take(15);
            }
            else
            {
                SandR.Subtitles = (from item in db.GetAllSubtitles()
                                   where (item.Title.Contains(title))
                                   orderby item.DateCreated descending
                                   select item).Take(15);

                SandR.Requests = (from item in db.GetAllRequests()
                                  where (item.Title.Contains(title))
                                  orderby item.Upvotes descending
                                  select item).Take(15);
            }
            

            return View(SandR);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Subtitle r = db.GetSubtitleById(id);
            if (r != null)
            {
                return View(r);
            }
            return View("NotFound");
        }
	}

}