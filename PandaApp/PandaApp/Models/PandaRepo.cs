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
       public  static IEnumerable<SubtitleLine> GetLines(int subtitleID)
       {
           PandaBase db = new PandaBase();
           return  (from item in db.SubtitleLines
                               where item.SubtitleID == subtitleID
                               orderby item.Index ascending
                               select item);
       }
        //TODO: Language and title linq requests
        /*public EditViewModel GetEditViewModel(int subtitleID)
        {
            
            EditViewModel viewModel = new EditViewModel();
            viewModel.SubtitleID = subtitleID;
            viewModel.Title = "test";
            viewModel.Language = "English";

            viewModel.Lines = (from item in db.SubtitleLines
                               orderby item. descending
                               where db. = SubtitleID
                               select item);
            return viewModel;
            return EditViewModel();
        }*/

        public Media GetMediaById(int id)
        {
            var result = (from m in db.Medias
                          where m.ID == id
                          select m).SingleOrDefault();

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

        public Account GetUserByName(string username)
        {
            var result = (from user in db.Accounts
                          where user.Username == username
                          select user).SingleOrDefault();
            return result;
        }
    }
}