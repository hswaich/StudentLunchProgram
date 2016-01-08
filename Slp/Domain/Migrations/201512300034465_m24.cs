namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChildAttributeTypes", "Visible", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChildAttributeTypes", "Visible");
        }
    }
}
