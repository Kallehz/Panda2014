using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class PandaRepo
    {
        PandaBase db = new PandaBase();

        public IEnumerable<Subtitle> GetAllSubtitles()
        {
            return db.Subtitles;
        }

        public Subtitle GetSubtitleById(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s).SingleOrDefault();
            return result;
        }
        //TODO: Language and title linq requests
        public EditViewModel GetEditViewModel(int subtitleID)
        {
            EditViewModel viewModel = new EditViewModel();
            viewModel.SubtitleID = subtitleID;

            viewModel.Title = "test";

            viewModel.Language = "English";

            viewModel.Lines = (from item in db.SubtitleLines
                               orderby item.index descending
                               where db.ID = SubtitleID
                               select item);
            return viewModel;
        }

        public Request GetRequestById(int id)
        {
            var result = (from s in db.Requests
                          where s.ID == id
                          select s).SingleOrDefault();
            return result;
        }

        public void AddSubtitle(Subtitle sub)
        {
            db.Subtitles.Add(sub);
            db.SaveChanges();
        }

        public void AddRequest(Request req)
        {
            db.Requests.Add(req);
            db.SaveChanges();
        }

        public void UpdateSubtitle(SubtitleLine sl)
        {
            SubtitleLine line = new SubtitleLine();
            var subtitle = (from s in GetAllSubtitles()
                            where s.ID == sl.SubtitleID
                            select s);
            
            if (sl != null)
            {
                line.Text = sl.Text;
                
            }
        }

        public void Save()
        {
            m_db.SaveChanges();
        }
    }
}