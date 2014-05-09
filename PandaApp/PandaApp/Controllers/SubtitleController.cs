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
        public ActionResult Edit(int subtitleID)
        {
            //EditViewModel editViewModel = GetEditViewModel(subtitleID);
            //return View(editViewModel);
            return View();
        }
	}

}