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
        public string TimeFrom { get; set; }
        public string TImeTO { get; set; }

        public EditViewModel(int subtitleID)
        {
            SubtitleID = subtitleID;
            //TODO: title and language
            Title = "test";

            Language = "English";

            Lines = PandaRepo.GetLines(subtitleID);
        }
    }
}