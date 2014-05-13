using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaApp.Models
{
    public class PandaRepo
    {
        PandaBase db = new PandaBase();

        public IEnumerable<Subtitle> GetAllSubtitles()
        {
            return db.Subtitles;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return db.Requests;
        }

        public IEnumerable<Language> GetAllLanguages()
        {
            return db.Languages;
        }

        public List<SelectListItem> GetLanguageListItems()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var allLanguages = GetAllLanguages();

            result.Add(new SelectListItem() { Value = "", Text = "--- Select ---" });

            foreach (var language in allLanguages)
            {
                result.Add(new SelectListItem() { Value = language.LanguageName, Text = language.LanguageName });
            }

            return result;
        }

        public Subtitle GetSubtitleById(int id)
        {
            var result = (from s in db.Subtitles
                          where s.ID == id
                          select s).SingleOrDefault();

            return result;
        }
       public static IEnumerable<SubtitleLine> GetLines(int subtitleID)
       {
           PandaBase db = new PandaBase();
           return  (from item in db.SubtitleLines
                    where item.SubtitleID == subtitleID
                    orderby item.Index ascending
                    select item);
       }
       public static string GetLanguageBySubID(int subtitleID)
       {
           PandaBase db = new PandaBase();
           return (from t in db.Subtitles
                   where t.ID == subtitleID
                   select t.Language).FirstOrDefault();
       }

       public static string GetTitleBySubID(int subtitleID)
       {
           PandaBase db = new PandaBase();
           return (from t in db.Subtitles
                   where t.ID == subtitleID
                   select t.Title).FirstOrDefault();
       }
        public Media GetMediaById(int id)
        {
            var result = (from m in db.Medias
                          where m.ID == id
                          select m).FirstOrDefault();

            return result;
        }

        public Request GetRequestById(int id)
        {
            var result = (from s in db.Requests
                          where s.ID == id
                          select s).SingleOrDefault();
            return result;
        }

        public static void AddAccount(Account acc)
        {
            PandaBase db = new PandaBase();
            db.Accounts.Add(acc);
            db.SaveChanges();
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

        public void AddMedia(Media med)
        {
            db.Medias.Add(med);
            db.SaveChanges();
        }
        public void AddComment(Comment c)
        {
            db.Comments.Add(c);
            db.SaveChanges();
        }

        public void AddSubtitleLine(SubtitleLine sl)
        {

            // Update-ar SubtitleLine með Sql skipun
            //  db.SubtitleLines.SqlQuery("UPDATE SubtitleLines SET Text = @NewText WHERE ID = @ID"
            //                              ,sl.Text, sl.SubtitleID);

            PandaBase db = new PandaBase();
            db.SubtitleLines.Add(sl);
            db.SaveChanges();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Subtitle> GetSubtitlesForMedia(int mediaID)
        {
            var result = (from s in db.Subtitles
                          where s.MediaID == mediaID
                          select s);

            return result;
        }

        public Media GetMediaByName(string title)
        {
            var result = (from m in db.Medias
                          where m.Title.ToLower().Contains(title.ToLower())
                          select m).FirstOrDefault();

            return result;
        }

        public Account GetUserByName(string username)
        {
            var result = (from user in db.Accounts
                          where user.Username == username
                          select user).SingleOrDefault();
            return result;
        }

        public Account GetUserById(int id)
        {
            var result = (from user in db.Accounts
                          where user.ID == id
                          select user).FirstOrDefault();
            return result;
        }

        public bool GetReqUpBool(int rId, int uId)
        {
            int item = 0;
            try
            {
                item = (from r in db.Upvoters
                        where r.RequestID == rId && r.UserID == uId
                        select r.ID).First();
            }
            catch(Exception)
            {

            }
            
            if(item == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool MediaExists(int mediaID)
        {
            foreach (var item in db.Medias)
            {
                if (item.ID == mediaID)
                {
                    return true;
                }
            }
            return false;
        }

    }
}