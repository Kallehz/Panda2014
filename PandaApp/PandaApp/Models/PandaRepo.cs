using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

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
            var result = (from l in db.Languages
                          orderby l.LanguageName ascending
                          select l);

            return result;
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

       public IEnumerable<SubtitleLine> GetLines(int subtitleID)
       {
           var result = (from item in db.SubtitleLines
                         where item.SubtitleID == subtitleID
                         orderby item.Index ascending
                         select item);

           return result;
       }

       public string GetLanguageBySubID(int subtitleID)
       {
           var result = (from t in db.Subtitles
                         where t.ID == subtitleID
                         select t.Language).FirstOrDefault();

           return result;
       }

       public string GetTitleBySubID(int subtitleID)
       {
           var result = (from t in db.Subtitles
                         where t.ID == subtitleID
                         select t.Title).FirstOrDefault();

           return result;
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

        public void AddAccount(Account acc)
        {
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

        public void FillReq(int id, string link)
        {
            Request r = GetRequestById(id);

            if (r != null)
            {
                r.SubtitleLink = link;
                db.SaveChanges();
            }
        }

        public void AddSubtitleLine(SubtitleLine sl)
        {
            db.SubtitleLines.Add(sl);
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
                          where m.Title.ToLower() == title.ToLower()
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

        // Never used, profiles use the username, not id.
        // Available if we want to change that.
        public Account GetUserById(int id)
        {
            var result = (from user in db.Accounts
                          where user.ID == id
                          select user).FirstOrDefault();
            return result;
        }

        public bool GetReqUpBool(int rId, int uId)
        {
            var item = (from r in db.Upvoters
                        where r.RequestID == rId && r.UserID == uId
                        select r.ID).FirstOrDefault();

            return !Convert.ToBoolean(item);
        }

        public void DeleteSubtitle(Subtitle sub)
        {
            db.Subtitles.Remove(sub);
            db.SaveChanges();
        }

        public void DeleteMedia(Media med)
        {
            db.Medias.Remove(med);
            db.SaveChanges();
        }
    }
}