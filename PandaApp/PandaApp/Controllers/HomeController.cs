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
using PagedList;


namespace PandaApp.Controllers
{
    public class HomeController : Controller
    {
        PandaRepo db = new PandaRepo();

        public ActionResult Index(int? page)
        {
            var subs = (from item in db.GetAllSubtitles()
                        orderby item.DateCreated descending
                        select item);

            /*if (Request.HttpMethod != "GET")
            {
                page = 1;
            }*/

            if (!page.HasValue)
            {
                page = 1;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.Languages = db.GetLanguageListItems();
            return View(subs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FAQ()
        {
            return View();
        }
    }
}