namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m19 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SlpApplications", "TotalMembers", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SlpApplications", "TotalMembers", c => c.Int(nullable: false));
        }
    }
}
