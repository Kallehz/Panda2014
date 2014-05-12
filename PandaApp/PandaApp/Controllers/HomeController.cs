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

            return View(SandR);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Upload()
        {
            ViewBag.Message = "Upload subtitle";    

            return View(new Subtitle());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file, SubtitleLine srtLine)
        {
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
            //TODO send file to database table!

            //Turn file to string
            string srtString = new StreamReader(file.InputStream).ReadToEnd();
           // Debug.Write(srtString);

            //regex for srt files from http://www.codeproject.com/Articles/32834/Subtitle-Synchronization-with-C
            string pattern =
              @"(?<sequence>\d+)\r\n(?<start>\d{2}\:\d{2}\:\d{2},\d{3}) --\> " +
              @"(?<end>\d{2}\:\d{2}\:\d{2},\d{3})\r\n(?<text>[\s\S]*?\r\n\r\n)";

            //parse string and send to database
            int coutner = 1;
            foreach (string result in Regex.Split(srtString, pattern))
            {
               // Debug.WriteLine("Number: ");
               // Debug.WriteLine(coutner);
               // Debug.WriteLine(result);

                if ( coutner == 1)
                {
                    //do nothing
                }

                if (coutner == 2)
                {
                    srtLine.Index = Convert.ToInt32(result);
                }

                if (coutner == 3)
                {
                    srtLine.TimeFrom = result;
                }

                if (coutner == 4)
                {
                    srtLine.TimeTo = result;
                }

                if (coutner == 5)
                {
                    srtLine.Text = result;
                    coutner = 0;
                }

                coutner++;
                // srtLine.SubtitleID = Convert.ToInt32(result);
                //srtLine.Text = result;
                db.UpdateSubtitleLine(srtLine);
                db.Save();
            }

            if (ModelState.IsValid)
            {
                item.Author = User.Identity.Name;
                db.AddSubtitle(item);
                db.Save();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ";

            return View();
        }
    }
}