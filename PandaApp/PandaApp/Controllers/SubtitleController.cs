﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaApp.Models;
using System.Text.RegularExpressions;
using System.IO;


namespace PandaApp.Controllers
{
    public class SubtitleController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Edit(int subtitleID)
        {
            return View(new EditViewModel(subtitleID));
        }
        public void UpdateSubtitleLine(int id, string text)
        {
            using(var context = new PandaBase())
            {
                SubtitleLine line = context.SubtitleLines.Where(l => l.ID == id).FirstOrDefault<SubtitleLine>();
                line.Text = text;
                context.Entry(line).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();  
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.Languages = db.GetLanguageListItems();
            return View(new Subtitle());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file, SubtitleLine srtLine)
        {
            if (ModelState.IsValid)
            {
                item.Author = User.Identity.Name;
                db.AddSubtitle(item);
                db.Save();

                //Code that checks if uploaded file has content.
                if ((file != null) && (file.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(file.FileName);
                    string SaveLocation = Server.MapPath("~/App_Data/" + fn);
                    try
                    {
                        file.SaveAs(SaveLocation);
                        Debug.Write("The file has been uploaded.");
                    }
                    catch (Exception ex)
                    {
                        Debug.Write("Error: " + ex.Message);
                        //Note: Exception.Message returns detailed message that describes the current exception. 
                        //For security reasons, we do not recommend you return Exception.Message to end users in 
                        //production environments. It would be better just to put a generic error message. 
                    }
                }
                else
                {
                    Debug.Write("Please select a file to upload.");
                }

                //Turn file to string
                string srtString = new StreamReader(file.InputStream).ReadToEnd();

                //regex for srt files from http://www.codeproject.com/Articles/32834/Subtitle-Synchronization-with-C
                string pattern =
                  @"(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> " +
                  @"(?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)";

                //parse string and send to database

                int counter = 1;
                foreach (string result in Regex.Split(srtString, pattern))
                {
                    //first instance in the str format is always empty
                    if (counter == 1)
                    {
                        srtLine.Index = 0;
                        srtLine.TimeFrom = null;
                        srtLine.TimeTo = null;
                        srtLine.Text = null;
                    }

                    //second instance is "subtitle number this"
                    if (counter == 2)
                    {
                        srtLine.Index = Convert.ToInt32(result);
                    }

                    //tird instance is TC in
                    if (counter == 3)
                    {
                        srtLine.TimeFrom = result;
                    }

                    //fourth is TC out
                    if (counter == 4)
                    {
                        srtLine.TimeTo = result;
                    }

                    //fifth is the actual onscreen text
                    if (counter == 5)
                    {
                        srtLine.Text = result;
                        srtLine.SubtitleID = item.ID;
                        counter = 0;
                    }

                    counter++;

                    // checks to see if all columns in srtLine have been populated before adding a line to the database.
                    if (srtLine.Index != 0 && srtLine.TimeFrom != null && srtLine.TimeTo != null
                        && srtLine.Text != null && srtLine.SubtitleID != 0)
                    {
                        db.AddSubtitleLine(srtLine);
                        db.Save();
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Languages = db.GetLanguageListItems();
            return View(item);
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
        public ActionResult Details()
        {
            if (ModelState.IsValid)
            {
                
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