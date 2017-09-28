namespace Event.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_EmailForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "ActivationCode", c => c.Guid());
            AddColumn("dbo.EmailForm", "ToEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailForm", "ToEmail");
            DropColumn("dbo.Person", "ActivationCode");
        }
    }
}
