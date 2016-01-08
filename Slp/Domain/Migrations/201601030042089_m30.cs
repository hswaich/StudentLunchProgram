namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberIncomeResponseDetails", "DetailError", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberIncomeResponseDetails", "DetailError");
        }
    }
}
