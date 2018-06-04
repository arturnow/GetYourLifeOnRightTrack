namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeInMapping : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TrackRecord", new[] { "Pattern_Id" });
            RenameColumn(table: "dbo.TrackRecord", name: "Pattern_Id", newName: "PatternId");
            AddColumn("dbo.Message", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Message", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.TrackRecord", "PatternId", c => c.Guid(nullable: false));
            CreateIndex("dbo.TrackRecord", "PatternId");
            DropColumn("dbo.Message", "CreateDatetime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Message", "CreateDatetime", c => c.DateTime(nullable: false));
            DropIndex("dbo.TrackRecord", new[] { "PatternId" });
            AlterColumn("dbo.TrackRecord", "PatternId", c => c.Guid());
            DropColumn("dbo.Message", "UpdateDate");
            DropColumn("dbo.Message", "CreateDate");
            RenameColumn(table: "dbo.TrackRecord", name: "PatternId", newName: "Pattern_Id");
            CreateIndex("dbo.TrackRecord", "Pattern_Id");
        }
    }
}
