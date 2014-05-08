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
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}