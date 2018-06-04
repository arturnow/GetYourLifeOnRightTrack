
namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class customSQL : DbMigration
    {
        public override void Up()
        {
            Sql("ALTER TABLE dbo.Goal ADD CONSTRAINT CK_Goal_Progress CHECK (Progress >= 0 AND Progress <=10)");
            Sql("ALTER TABLE dbo.Task ADD CONSTRAINT CK_Task_Progress CHECK (Progress >= 0 AND Progress <=100)");
            Sql("ALTER TABLE dbo.Task ADD CONSTRAINT CK_Task_Priority CHECK (Priority >= 0 AND Priority <=10)");
            Sql("ALTER TABLE TrackRecord ADD CONSTRAINT CK_TrackRecord_StartBeforeEnd CHECK (StartDate <= EndDate)");
            Sql("ALTER TABLE Task ADD CONSTRAINT CK_Task_StartBeforeEnd CHECK (StartDate <= EndDate)");
            Sql("ALTER TABLE Goal ADD CONSTRAINT CK_Goal_StartBeforeEnd CHECK (StartDate <= EndDate)");
            Sql("ALTER TABLE Goal ADD CONSTRAINT CK_Task_StartBeforeDueDate CHECK (StartDate <= DueDateTime)");

        }

        public override void Down()
        {
            Sql("ALTER TABLE dbo.Goal DROP CONSTRAINT CK_Goal_Progress");
            Sql("ALTER TABLE dbo.Task DROP CONSTRAINT CK_Task_Progress");
            Sql("ALTER TABLE dbo.Task DROP CONSTRAINT CK_Task_Priority");
            Sql("ALTER TABLE TrackRecord DROP CONSTRAINT CK_TrackRecord_StartBeforeEnd");

            Sql("ALTER TABLE Task DROP CONSTRAINT CK_Task_StartBeforeEnd");
            Sql("ALTER TABLE Goal DROP CONSTRAINT CK_Goal_StartBeforeEnd");
            Sql("ALTER TABLE Goal DROP CONSTRAINT CK_Task_StartBeforeDueDate");
        }
    }
}


