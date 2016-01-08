namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SlpApplications", "IsComplete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlpApplications", "IsComplete", c => c.Boolean(nullable: false));
        }
    }
}
