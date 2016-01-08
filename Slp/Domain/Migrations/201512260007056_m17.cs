namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SlpApplications", "MemberLastFourSSN", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SlpApplications", "MemberLastFourSSN", c => c.Int(nullable: false));
        }
    }
}
