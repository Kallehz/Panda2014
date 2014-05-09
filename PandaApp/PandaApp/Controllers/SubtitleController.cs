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
        PandaBase db = new PandaBase();
        public EditViewModel GetSubtitle(int subtitleID)
        {
            EditViewModel viewModel = new EditViewModel();
            viewModel.SubtitleID = subtitleID;

            viewModel.Title = "test";

            viewModel.Language = "English";

            viewModel.Lines =  (from item in db.SubtitleLines
                                orderby item.index descending
                                where db.ID = SubtitleID
                                select item);
            return viewModel;
        }

        public ActionResult Edit(int subtitleID)
        {
            EditViewModel editViewModel = GetSubtitle(subtitleID);
            return View(editViewModel);
        }
	}

}