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

            return View(new Media());
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(Subtitle item, HttpPostedFileBase file)
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

            //parse string
            foreach (string result in Regex.Split(srtString, pattern))
            {
                Debug.WriteLine(result);
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

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Media());
        }

        [HttpPost]
        public ActionResult Create(FormCollection formData)
        {
            Media m = new Media();
            UpdateModel(m);
            db.AddMedia(m);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Requests()
        {
            ViewBag.Message = "View requests";

            IQueryable<Request> requests = (from item in db.GetAllRequests()
                                             orderby item.DateCreated descending
                                             select item).Take(15);
            return View(requests);
        }
        /*
        [HttpGet]
        public ActionResult Profile()
        {

            IQueryable<Request> UserProfile = (from item in db.GetAllRequests()
                                               where item.Author == User.Identity.Name
                                               orderby item.DateCreated descending
                                               select item).Take(10);
            return View(UserProfile);
        }
        */
        [HttpGet]
        public ActionResult NewRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewRequest(Request item)
        {
            ViewBag.Message = "Create requests";

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
                return RedirectToAction("Requests");
            }

            return View();
        }


        public ActionResult FAQ()
        {
            ViewBag.Message = "FAQ";

            return View();
        }
    }
}