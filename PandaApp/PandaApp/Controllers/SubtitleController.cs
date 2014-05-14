using System;
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

        [Authorize]
        public ActionResult Edit(int subtitleID)
        {
            return View(new EditViewModel(subtitleID));
        }

        [Authorize]
        public void UpdateSubtitleLine(int id, string text, string timeStart, string timeStop)
        {
            using (var context = new PandaBase())
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

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.Languages = db.GetLanguageListItems();
            return View(new Subtitle());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file)
        {
            Media med = db.GetMediaByName(item.Title);
            Media newMedia = new Media();
            if (med == null)
            {
                newMedia.Title = item.Title;
                db.AddMedia(newMedia);
                med = newMedia;
            }

            if (ModelState.IsValid)
            {
                item.MediaID = med.ID;
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
                SubtitleLine srtLine = new SubtitleLine();

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

        public ActionResult SubtitleSearch(string title, string language)
        {
            IEnumerable<Subtitle> sub;

            if (db.GetMediaByName(title) != null)
            {
                var med = db.GetMediaByName(title);
                return RedirectToAction("MediaProfile", "Media", new { id = med.ID });
            }

            if (language == "" || language == null)
            {
                sub = (from item in db.GetAllSubtitles()
                       where item.Title.ToLower().Contains(title.ToLower())
                       orderby item.DateCreated descending
                       select item).Take(15);
            }
            else
            {
                sub = (from item in db.GetAllSubtitles()
                       where (item.Title.ToLower().Contains(title.ToLower()) &&
                       (item.Language == language))
                       orderby item.DateCreated descending
                       select item).Take(15);
            }


            ViewBag.Languages = db.GetLanguageListItems();
            return View(sub);
        }
        [HttpPost]
        public ActionResult PostComment(int subtitleId, string comment)
        {
            if (ModelState.IsValid)
            {
                Comment newComment = new Comment();
                newComment.Text = comment;
                newComment.Author = User.Identity.Name;
                newComment.SubtitleId = subtitleId;
                db.AddComment(newComment);
                return RedirectToAction("Details", "Subtitle", new { id = subtitleId });
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

        [HttpPost]
        public ActionResult Download(int subtitleID)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateSrt(SubtitleLine srtLine, int id)
        {
            PandaBase db = new PandaBase();
            var index = (from item in db.SubtitleLines
                         where item.ID == id
                         select item);

            Debug.Write(index);

            return View("Home");
        }
	}
}