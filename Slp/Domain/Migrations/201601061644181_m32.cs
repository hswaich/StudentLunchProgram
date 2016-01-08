namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlpApplications", "Steps", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SlpApplications", "Steps");
        }
    }
}
