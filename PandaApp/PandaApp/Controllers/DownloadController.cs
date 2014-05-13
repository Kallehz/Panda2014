using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using PandaApp.Models;
using System.Text;
using System.Diagnostics;


namespace PandaApp.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/
      
        [HttpGet]
        public ActionResult Download(SubtitleLine srtLine, int subID)
        {
            int subId = 114;

            PandaBase db = new PandaBase();
            var index = (from item in db.SubtitleLines
                         where item.ID == subId
                         select item);

            Debug.Write(index);

            return View(index);
        }
        
	}
}