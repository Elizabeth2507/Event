namespace Event.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlanEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StartDateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Description = c.String(),
                        MaxCountGuest = c.Int(nullable: false),
                        EventAuthor_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Person", t => t.EventAuthor_ID)
                .Index(t => t.EventAuthor_ID);
            
            CreateTable(
                "dbo.GuestPlanEvent",
                c => new
                    {
                        Guest_ID = c.Int(nullable: false),
                        PlanEvent_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Guest_ID, t.PlanEvent_ID })
                .ForeignKey("dbo.Person", t => t.Guest_ID, cascadeDelete: true)
                .ForeignKey("dbo.PlanEvent", t => t.PlanEvent_ID, cascadeDelete: true)
                .Index(t => t.Guest_ID)
                .Index(t => t.PlanEvent_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GuestPlanEvent", "PlanEvent_ID", "dbo.PlanEvent");
            DropForeignKey("dbo.GuestPlanEvent", "Guest_ID", "dbo.Person");
            DropForeignKey("dbo.PlanEvent", "EventAuthor_ID", "dbo.Person");
            DropIndex("dbo.GuestPlanEvent", new[] { "PlanEvent_ID" });
            DropIndex("dbo.GuestPlanEvent", new[] { "Guest_ID" });
            DropIndex("dbo.PlanEvent", new[] { "EventAuthor_ID" });
            DropTable("dbo.GuestPlanEvent");
            DropTable("dbo.PlanEvent");
            DropTable("dbo.Person");
        }
    }
}
