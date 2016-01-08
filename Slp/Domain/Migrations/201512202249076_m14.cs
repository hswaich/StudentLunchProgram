namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlpApplications", "CompletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SlpApplications", "CompletedDate");
        }
    }
}
