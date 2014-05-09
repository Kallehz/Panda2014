using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PandaApp.Models
{
    public class PandaRepo
    {
        PandaBase db = new PandaBase();

        public IQueryable<Subtitle> GetAllSubtitles()
        {
            return db.Subtitles;
        }

        public IQueryable<Request> GetAllRequests()
        {
            return db.Requests;
        }

        public Subtitle GetSubtitleById(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s).SingleOrDefault();
            return result;
        }
        /*public static IEnumerable<SubtitleLine> GetLines(int subtitleID)
        {
            
            IEnumerable<SubtitleLine> lines = (from item in db.SubtitleLines
                                            where item.ID == subtitleID
                                             orderby item.Index descending
                               select item);
            return lines;
        }*/

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
        public void AddAccount(Account acc)
        {
            db.Accounts.Add(acc);
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
                db.SaveChanges();
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}