namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniquePatternContraint : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Pattern", "Value", unique: true, name: "IX_PatternValue");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pattern", "IX_PatternValue");
        }
    }
}
