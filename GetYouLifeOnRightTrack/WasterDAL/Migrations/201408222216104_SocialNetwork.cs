namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SocialNetwork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Relation",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ParentId = c.Long(nullable: false),
                        ChildId = c.Long(nullable: false),
                        Type = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SocialProfile", t => t.ChildId, cascadeDelete: true)
                .ForeignKey("dbo.SocialProfile", t => t.ParentId)
                .Index(t => new { t.ParentId, t.ChildId }, unique: true, name: "IX_Relation");
            
            CreateTable(
                "dbo.SocialProfile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        Nickname = c.String(maxLength: 30),
                        Age = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        Identity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identity", t => t.Identity_Id)
                .Index(t => t.Identity_Id);
            Sql("ALTER TABLE dbo.Relation ADD CONSTRAINT CK_Relation_DifferentChildParent CHECK (ParentId <> ChildId)");  
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE Task DROP CONSTRAINT CK_Relation_DifferentChildParent");
            DropForeignKey("dbo.Relation", "ParentId", "dbo.SocialProfile");
            DropForeignKey("dbo.Relation", "ChildId", "dbo.SocialProfile");
            DropForeignKey("dbo.SocialProfile", "Identity_Id", "dbo.Identity");
            DropIndex("dbo.SocialProfile", new[] { "Identity_Id" });
            DropIndex("dbo.Relation", "IX_Relation");
            DropTable("dbo.SocialProfile");
            DropTable("dbo.Relation");
        }
    }
}
