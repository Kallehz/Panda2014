namespace PandaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        Text = c.String(),
                        SubtitleId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubtitleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        Description = c.String(),
                        SubtitleID = c.Int(nullable: false),
                        RequestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IMDB = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        ExternalVideoLink = c.String(),
                        Upvotes = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubtitleLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        SubtitleID = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subtitles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LanguageID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                        MediaID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subtitles");
            DropTable("dbo.SubtitleLines");
            DropTable("dbo.Requests");
            DropTable("dbo.Media");
            DropTable("dbo.Languages");
            DropTable("dbo.Histories");
            DropTable("dbo.Contributors");
            DropTable("dbo.Comments");
            DropTable("dbo.Accounts");
        }
    }
}
