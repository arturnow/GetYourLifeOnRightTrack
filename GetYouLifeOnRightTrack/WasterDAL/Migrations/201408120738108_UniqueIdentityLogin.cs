namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueIdentityLogin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Identity", "Login", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Identity", "Login", unique: true, name: "IX_IdentityLogin");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Identity", "IX_IdentityLogin");
            AlterColumn("dbo.Identity", "Login", c => c.String());
        }
    }
}
