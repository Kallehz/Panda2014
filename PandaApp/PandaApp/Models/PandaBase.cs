using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PandaApp.Models
{
    public class PandaBase : DbContext
    {
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<SubtitleLine> SubtitleLines { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Upvoter> Upvoters { get; set; }

    }
}