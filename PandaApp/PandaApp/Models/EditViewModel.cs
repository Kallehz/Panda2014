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
        public IEnumerable<SubtitleLine> Lines { get; set; }
        public EditViewModel(int subtitleID)
        {
            SubtitleID = subtitleID;
            //TODO: title and language
            Title = PandaRepo.GetTitleBySubID(subtitleID);

            Language = PandaRepo.GetLanguageBySubID(subtitleID);

            Lines = PandaRepo.GetLines(subtitleID);
        }
    }
}