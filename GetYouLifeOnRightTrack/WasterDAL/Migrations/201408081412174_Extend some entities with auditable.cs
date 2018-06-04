namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Extendsomeentitieswithauditable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Identity", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Identity", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Identity", "UpdateDate");
            DropColumn("dbo.Identity", "CreateDate");
        }
    }
}
