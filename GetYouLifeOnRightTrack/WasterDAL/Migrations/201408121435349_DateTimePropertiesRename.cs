namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimePropertiesRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goal", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Goal", "EndDate", c => c.DateTime());
            AddColumn("dbo.Task", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Task", "EndDate", c => c.DateTime());
            AddColumn("dbo.TrackRecord", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TrackRecord", "EndDate", c => c.DateTime());
            DropColumn("dbo.Goal", "StartTime");
            DropColumn("dbo.Goal", "EndTime");
            DropColumn("dbo.Task", "StartDateTime");
            DropColumn("dbo.Task", "EndDateTime");
            DropColumn("dbo.TrackRecord", "StartTime");
            DropColumn("dbo.TrackRecord", "EndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrackRecord", "EndTime", c => c.DateTime());
            AddColumn("dbo.TrackRecord", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Task", "EndDateTime", c => c.DateTime());
            AddColumn("dbo.Task", "StartDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Goal", "EndTime", c => c.DateTime());
            AddColumn("dbo.Goal", "StartTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.TrackRecord", "EndDate");
            DropColumn("dbo.TrackRecord", "StartDate");
            DropColumn("dbo.Task", "EndDate");
            DropColumn("dbo.Task", "StartDate");
            DropColumn("dbo.Goal", "EndDate");
            DropColumn("dbo.Goal", "StartDate");
        }
    }
}
