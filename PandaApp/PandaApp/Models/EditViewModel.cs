using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class EditViewModel
    {
        public int SubtitleID { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public IPagedList<SubtitleLine> Lines { get; set; }
        public EditViewModel(int subtitleID)
        {
            SubtitleID = subtitleID;
            Title = PandaRepo.GetTitleBySubID(subtitleID);
            Language = PandaRepo.GetLanguageBySubID(subtitleID);
        }
    }
}