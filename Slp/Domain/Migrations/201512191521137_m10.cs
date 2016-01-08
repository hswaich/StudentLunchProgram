namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssistancePrograms", "Visible", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Ethnicities", "Visible", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Frequencies", "Visible", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.Races", "Visible", c => c.Boolean(nullable: false, defaultValue: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "Visible");
            DropColumn("dbo.Frequencies", "Visible");
            DropColumn("dbo.Ethnicities", "Visible");
            DropColumn("dbo.AssistancePrograms", "Visible");
        }
    }
}
