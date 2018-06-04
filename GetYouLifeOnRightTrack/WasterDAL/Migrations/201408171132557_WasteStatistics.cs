namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WasteStatistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Period",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WasteStatistic",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdentityId = c.Int(nullable: false),
                        PeriodId = c.Long(nullable: false),
                        EntryCount = c.Int(nullable: false),
                        TimeSumInSecond = c.Long(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Identity", t => t.IdentityId, cascadeDelete: true)
                .ForeignKey("dbo.Period", t => t.PeriodId, cascadeDelete: true)
                .Index(t => t.IdentityId)
                .Index(t => t.PeriodId);


            Sql("ALTER TABLE dbo.Period ADD CONSTRAINT CK_Period_StartDateGreaterThanEndDate CHECK (StartDate <= EndDate)");
            Sql("ALTER TABLE dbo.Period ADD CONSTRAINT CK_Period_DurationPositive CHECK (Duration >= 0)");

            Sql("ALTER TABLE dbo.WasteStatistic ADD CONSTRAINT CK_WasteStatistic_EntryCountPositive CHECK (EntryCount >= 0)");
            Sql("ALTER TABLE dbo.WasteStatistic ADD CONSTRAINT CK_WasteStatistic_TotalTimePositive CHECK (TimeSumInSecond >= 0)");
            
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE dbo.Period DROP CONSTRAINT CK_Period_StartDateGreaterThanEndDate");
            Sql("ALTER TABLE dbo.Period DROP CONSTRAINT CK_Period_DurationPositive");
            Sql("ALTER TABLE dbo.WasteStatistic DROP CONSTRAINT CK_WasteStatistic_EntryCountPositive");
            Sql("ALTER TABLE dbo.WasteStatistic DROP CONSTRAINT CK_WasteStatistic_TotalTimePositive");

            DropForeignKey("dbo.WasteStatistic", "PeriodId", "dbo.Period");
            DropForeignKey("dbo.WasteStatistic", "IdentityId", "dbo.Identity");
            DropIndex("dbo.WasteStatistic", new[] { "PeriodId" });
            DropIndex("dbo.WasteStatistic", new[] { "IdentityId" });
            DropTable("dbo.WasteStatistic");
            DropTable("dbo.Period");
        }
    }
}
