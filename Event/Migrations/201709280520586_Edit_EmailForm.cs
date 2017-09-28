namespace Event.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_EmailForm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmailForm", "PlanEvent_ID", "dbo.PlanEvent");
            DropIndex("dbo.EmailForm", new[] { "PlanEvent_ID" });
            CreateTable(
                "dbo.EmailFormPlanEvent",
                c => new
                    {
                        EmailForm_ID = c.Int(nullable: false),
                        PlanEvent_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmailForm_ID, t.PlanEvent_ID })
                .ForeignKey("dbo.EmailForm", t => t.EmailForm_ID, cascadeDelete: true)
                .ForeignKey("dbo.PlanEvent", t => t.PlanEvent_ID, cascadeDelete: true)
                .Index(t => t.EmailForm_ID)
                .Index(t => t.PlanEvent_ID);
            
            DropColumn("dbo.EmailForm", "PlanEvent_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailForm", "PlanEvent_ID", c => c.Int());
            DropForeignKey("dbo.EmailFormPlanEvent", "PlanEvent_ID", "dbo.PlanEvent");
            DropForeignKey("dbo.EmailFormPlanEvent", "EmailForm_ID", "dbo.EmailForm");
            DropIndex("dbo.EmailFormPlanEvent", new[] { "PlanEvent_ID" });
            DropIndex("dbo.EmailFormPlanEvent", new[] { "EmailForm_ID" });
            DropTable("dbo.EmailFormPlanEvent");
            CreateIndex("dbo.EmailForm", "PlanEvent_ID");
            AddForeignKey("dbo.EmailForm", "PlanEvent_ID", "dbo.PlanEvent", "ID");
        }
    }
}
