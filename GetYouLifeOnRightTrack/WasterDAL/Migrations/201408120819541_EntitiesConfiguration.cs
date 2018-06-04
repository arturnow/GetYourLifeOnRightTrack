namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesConfiguration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity");
            RenameColumn(table: "dbo.Goal", name: "LoginId", newName: "IdentityId");
            RenameColumn(table: "dbo.Pattern", name: "LoginId", newName: "IdentityId");
            RenameIndex(table: "dbo.Goal", name: "IX_LoginId", newName: "IX_IdentityId");
            RenameIndex(table: "dbo.Pattern", name: "IX_LoginId", newName: "IX_IdentityId");
            AlterColumn("dbo.Goal", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Identity", "Email", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Identity", "Password", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Message", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Pattern", "Value", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Task", "Name", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Identity", "Email", unique: true, name: "IX_IdentityEmail");
            AddForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity", "Id");
            DropColumn("dbo.TrackRecord", "PatternValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackRecord", "PatternValue", c => c.String());
            DropForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity");
            DropIndex("dbo.Identity", "IX_IdentityEmail");
            AlterColumn("dbo.Task", "Name", c => c.String());
            AlterColumn("dbo.Pattern", "Value", c => c.String());
            AlterColumn("dbo.Message", "Title", c => c.String());
            AlterColumn("dbo.Identity", "Password", c => c.String());
            AlterColumn("dbo.Identity", "Email", c => c.String());
            AlterColumn("dbo.Goal", "Name", c => c.String());
            RenameIndex(table: "dbo.Pattern", name: "IX_IdentityId", newName: "IX_LoginId");
            RenameIndex(table: "dbo.Goal", name: "IX_IdentityId", newName: "IX_LoginId");
            RenameColumn(table: "dbo.Pattern", name: "IdentityId", newName: "LoginId");
            RenameColumn(table: "dbo.Goal", name: "IdentityId", newName: "LoginId");
            AddForeignKey("dbo.TrackRecord", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
        }
    }
}
