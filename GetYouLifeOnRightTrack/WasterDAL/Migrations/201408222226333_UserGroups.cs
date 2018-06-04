namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.SocialProfile", t => t.UserId)
                .Index(t => new { t.UserId, t.GroupId, t.Type }, unique: true, name: "IX_Relation");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInGroup", "UserId", "dbo.SocialProfile");
            DropForeignKey("dbo.UserInGroup", "GroupId", "dbo.Group");
            DropIndex("dbo.UserInGroup", "IX_Relation");
            DropTable("dbo.UserInGroup");
            DropTable("dbo.Group");
        }
    }
}
