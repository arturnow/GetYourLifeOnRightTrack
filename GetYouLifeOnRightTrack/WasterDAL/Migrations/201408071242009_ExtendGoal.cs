namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendGoal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goal", "LoginId", c => c.Int(nullable: false));
            CreateIndex("dbo.Goal", "LoginId");
            AddForeignKey("dbo.Goal", "LoginId", "dbo.Identity", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goal", "LoginId", "dbo.Identity");
            DropIndex("dbo.Goal", new[] { "LoginId" });
            DropColumn("dbo.Goal", "LoginId");
        }
    }
}
