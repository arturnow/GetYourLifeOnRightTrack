namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Message", "IdentityId", "dbo.Identity");
            DropForeignKey("dbo.Pattern", "LoginId", "dbo.Identity");
            DropForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity");
            DropPrimaryKey("dbo.Identity");
            AlterColumn("dbo.Identity", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Identity", "Id");
            AddForeignKey("dbo.Message", "IdentityId", "dbo.Identity", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pattern", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity");
            DropForeignKey("dbo.Pattern", "LoginId", "dbo.Identity");
            DropForeignKey("dbo.Message", "IdentityId", "dbo.Identity");
            DropPrimaryKey("dbo.Identity");
            AlterColumn("dbo.Identity", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Identity", "Id");
            AddForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Pattern", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Message", "IdentityId", "dbo.Identity", "Id", cascadeDelete: true);
        }
    }
}
