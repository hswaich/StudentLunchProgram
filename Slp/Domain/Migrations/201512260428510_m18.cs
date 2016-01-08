namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "FirstName", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Members", "MiddleInitial", c => c.String(maxLength: 1, unicode: false));
            AddColumn("dbo.Members", "LastName", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.Members", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Name", c => c.String(maxLength: 200, unicode: false));
            DropColumn("dbo.Members", "LastName");
            DropColumn("dbo.Members", "MiddleInitial");
            DropColumn("dbo.Members", "FirstName");
        }
    }
}
