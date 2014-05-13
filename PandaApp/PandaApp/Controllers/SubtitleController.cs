﻿using System;
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
        public void UpdateSubtitleLine(int id, string text, string timeStart, string timeStop)
        {
            using(var context = new PandaBase())
            {
                SubtitleLine line = context.SubtitleLines.Where(l => l.ID == id).FirstOrDefault<SubtitleLine>();
                line.Text = text;
                line.TimeFrom = timeStart;
                line.TimeTo = timeStop;
                Debug.Write(timeStart + "-" + timeStop);
                context.Entry(line).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();  
            }
        }

        public ActionResult SearchResult(string title, string language)
        {
            SubAndReq SandR = new SubAndReq();

            if(language != "all")
            {
                // This should redirect to the proper mediaID
                // of the media that was searched for.
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
        [HttpPost]
        public ActionResult Details(Comment comment)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine("This is a comment test");
                db.Save();
                return RedirectToAction("Index");
            }
            return View();
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