namespace WasterWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BatchLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BatchLog",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: true),
                        InProgress = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            Sql("ALTER TABLE dbo.BatchLog ADD CONSTRAINT CK_BatchLog_StartBeforeEnd CHECK (StartDateTime <= EndDateTime)");
            
        }
        
        public override void Down()
        {
            Sql("ALTER TABLE Task DROP CONSTRAINT CK_BatchLog_StartBeforeEnd");
            DropTable("dbo.BatchLog");
        }
    }
}
