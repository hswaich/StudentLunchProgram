namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberChildAttributes", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberChildAttributes", "IsSelected");
        }
    }
}
