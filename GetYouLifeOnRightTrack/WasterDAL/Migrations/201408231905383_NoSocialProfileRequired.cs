namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoSocialProfileRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SocialProfile", "Identity_Id", "dbo.Identity");
            DropColumn("dbo.SocialProfile", "IdentityId");
            RenameColumn(table: "dbo.SocialProfile", name: "Identity_Id", newName: "IdentityId");
            RenameIndex(table: "dbo.SocialProfile", name: "IX_Identity_Id", newName: "IX_IdentityId");
            AddForeignKey("dbo.SocialProfile", "IdentityId", "dbo.Identity", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SocialProfile", "IdentityId", "dbo.Identity");
            RenameIndex(table: "dbo.SocialProfile", name: "IX_IdentityId", newName: "IX_Identity_Id");
            RenameColumn(table: "dbo.SocialProfile", name: "IdentityId", newName: "Identity_Id");
            AddColumn("dbo.SocialProfile", "IdentityId", c => c.Int(nullable: false));
            AddForeignKey("dbo.SocialProfile", "Identity_Id", "dbo.Identity", "Id");
        }
    }
}
