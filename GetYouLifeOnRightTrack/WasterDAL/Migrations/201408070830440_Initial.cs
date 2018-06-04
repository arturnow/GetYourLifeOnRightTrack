namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goal",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        DueDateTime = c.DateTime(nullable: false),
                        Progress = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Identity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        Url = c.String(),
                        CreateDatetime = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        IdentityId = c.Int(nullable: false),
                        Type = c.String(),
                        RequireConfirmation = c.Boolean(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identity", t => t.IdentityId, cascadeDelete: true)
                .Index(t => t.IdentityId);
            
            CreateTable(
                "dbo.Pattern",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Value = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        IsWorking = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        LoginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identity", t => t.LoginId, cascadeDelete: true)
                .Index(t => t.LoginId);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Duration = c.Int(nullable: false),
                        GoalId = c.Guid(nullable: false),
                        Name = c.String(),
                        Priority = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goal", t => t.GoalId, cascadeDelete: true)
                .Index(t => t.GoalId);
            
            CreateTable(
                "dbo.TrackRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PatternValue = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(),
                        IsWorking = c.Boolean(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        LoginId = c.Int(nullable: false),
                        Pattern_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identity", t => t.LoginId, cascadeDelete: true)
                .ForeignKey("dbo.Pattern", t => t.Pattern_Id)
                .Index(t => t.LoginId)
                .Index(t => t.Pattern_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackRecord", "Pattern_Id", "dbo.Pattern");
            DropForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity");
            DropForeignKey("dbo.Task", "GoalId", "dbo.Goal");
            DropForeignKey("dbo.Pattern", "LoginId", "dbo.Identity");
            DropForeignKey("dbo.Message", "IdentityId", "dbo.Identity");
            DropIndex("dbo.TrackRecord", new[] { "Pattern_Id" });
            DropIndex("dbo.TrackRecord", new[] { "LoginId" });
            DropIndex("dbo.Task", new[] { "GoalId" });
            DropIndex("dbo.Pattern", new[] { "LoginId" });
            DropIndex("dbo.Message", new[] { "IdentityId" });
            DropTable("dbo.TrackRecord");
            DropTable("dbo.Task");
            DropTable("dbo.Pattern");
            DropTable("dbo.Message");
            DropTable("dbo.Identity");
            DropTable("dbo.Goal");
        }
    }
}
