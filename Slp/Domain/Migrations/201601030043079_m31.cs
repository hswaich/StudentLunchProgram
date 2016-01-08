namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m31 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MemberIncomeResponseDetails", "DetailError");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MemberIncomeResponseDetails", "DetailError", c => c.String());
        }
    }
}
