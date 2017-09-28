namespace Event.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEmailForm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailForm",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FromName = c.String(nullable: false),
                        FromEmail = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        PlanEvent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlanEvent", t => t.PlanEvent_ID)
                .Index(t => t.PlanEvent_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailForm", "PlanEvent_ID", "dbo.PlanEvent");
            DropIndex("dbo.EmailForm", new[] { "PlanEvent_ID" });
            DropTable("dbo.EmailForm");
        }
    }
}
