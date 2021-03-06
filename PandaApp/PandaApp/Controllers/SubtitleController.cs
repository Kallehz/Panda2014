﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaApp.Models;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using PagedList;


namespace PandaApp.Controllers
{
    public class SubtitleController : Controller
    {
        PandaRepo db = new PandaRepo();

        [Authorize]
        public ActionResult Edit(int? page, int id)
        {
            EditViewModel mdl = new EditViewModel();

            // This is for PagingList
            if (!page.HasValue)
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            // Put correct values into the model
            mdl.SubtitleID = id;
            mdl.Title = db.GetTitleBySubID(id);
            mdl.Language = db.GetLanguageBySubID(id);

            // For PagingList
            mdl.Lines = db.GetLines(id).ToPagedList(pageNumber, pageSize);

            return View(mdl);
        }

        [Authorize]
        public void UpdateSubtitleLine(int id, string text, string timeStart, string timeStop)
        {
            using (var context = new PandaBase())
            {
                // Locates a SubtitleLine in the database with
                // a given id
                SubtitleLine line = (context.SubtitleLines.Where(l => l.ID == id)
                                    .FirstOrDefault<SubtitleLine>());
                // This adds two line breaks at the end of the text,
                // this is necessary for the .srt standard
                line.Text = text + "\r\n" + "\r\n";
                line.TimeFrom = timeStart;
                line.TimeTo = timeStop;
                // Marks the entry 'line' in the database as modified,
                // this way code-first knows it should update this entry in the database
                context.Entry(line).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            // Gets language dropdown
            ViewBag.Languages = db.GetLanguageListItems();
            return View(new Subtitle());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file)
        {

            var filename = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(filename);
            
            //Checks if the file extension is correct and redirects user to error page if required.
            if ((extension != ".srt" && extension != ".txt"))
            {
                return View("UploadError");
            }
            
            Media med = db.GetMediaByName(item.Title);
            Media newMedia = new Media();

            // If the media doesnt exist it
            // will be created automatically
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

                //Code that checks if uploaded file has content
                //before the file is saved to the server.
                if ((file != null) && (file.ContentLength > 0))
                {
                    string fn = System.IO.Path.GetFileName(file.FileName);
                    string SaveLocation = Server.MapPath("~/App_Data/" + fn);
                    file.SaveAs(SaveLocation);
                }
                
                SubtitleLine srtLine = new SubtitleLine();

                //Turn file to string
                string srtString = new StreamReader(file.InputStream, Encoding.Default, true).ReadToEnd();

                //regex for srt files from http://www.codeproject.com/Articles/32834/Subtitle-Synchronization-with-C
                string pattern =  @"(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> " +
                                  @"(?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)";
                
                //parse string and send to database
                int counter = 1;
                bool srtTrue = false;

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

                    // checks to see if all columns in srtLine have been populated 
                    // before adding a line to the database.
                    if (   srtLine.Index != 0 
                        && srtLine.TimeFrom != null 
                        && srtLine.TimeTo != null
                        && srtLine.Text != null 
                        && srtLine.SubtitleID != 0)
                    {
                        db.AddSubtitleLine(srtLine);
                        srtTrue = true;
                    }

                }

                //If columns have been populated the format is fine and file created. 
                //Else entires that have been created are deleted and user directed to error page.
                if (srtTrue)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    db.DeleteSubtitle(item);
                    db.DeleteMedia(med);
                    return View("UploadError");
                }
            }

            // Gets language dropdown
            ViewBag.Languages = db.GetLanguageListItems();
            return View(item);
        }

        public ActionResult SubtitleSearch(int? page, string title, string language)
        {
            IEnumerable<Subtitle> sub;

            // If media with that exact name exists
            // you get redirected to the existing profile
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
                       select item);
            }
            else
            {
                sub = (from item in db.GetAllSubtitles()
                       where (item.Title.ToLower().Contains(title.ToLower()) &&
                       (item.Language == language))
                       orderby item.DateCreated descending
                       select item);
            }

            if (!page.HasValue)
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            // Gets language dropdown
            ViewBag.Languages = db.GetLanguageListItems();
            return View(sub.ToPagedList(pageNumber, pageSize));
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
            // Gets the subtitle model with 'id'
            Subtitle p = db.GetSubtitleById(id);
            if (p != null)
            {
                return View(p);
            }
            return View("NotFound");
        }

        [HttpPost]
        public ActionResult Download(int id, string view)
        {
            PandaBase db = new PandaBase();

            //Calling the variables we need from the database tables.

            var subtitleLines = (from line in db.SubtitleLines
                                 where id == line.SubtitleID
                                 select line);

            var filename = (from title in db.Subtitles
                                where id == title.ID
                                select title.Title).SingleOrDefault();
            
            var language = (from lang in db.Subtitles
                                where id == lang.ID
                                select lang.Language).SingleOrDefault();


            //Building string in the right srt format.
            StringBuilder output = new StringBuilder();
          
            foreach (SubtitleLine line in subtitleLines)
            {
                int indexInt = line.Index;
                string index = indexInt.ToString();
                output.Append(index);
                output.Append("\r\n");
                string tcIn = line.TimeFrom.ToString();
                output.Append(tcIn);
                output.Append(" --> ");
                string tcOut = line.TimeTo.ToString();
                output.Append(tcOut);
                output.Append("\r\n");
                string onScreen = line.Text;
                output.Append(onScreen);
            }

            //Makes a string from the stringbuilder. Closing the string
            var finalOutput = output.ToString();

            //making the string that contains the filename and language.
            var finalname = filename + "_" + language + ".srt";

            //creating the file from the string.
            var byteArray = Encoding.UTF8.GetBytes(finalOutput);
            var stream = new MemoryStream(byteArray);
           
            //return file to user.
            return File(stream, "text/plain", finalname);
        }
	}
}