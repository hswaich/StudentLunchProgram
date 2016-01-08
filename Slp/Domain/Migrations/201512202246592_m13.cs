namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlpApplications", "Phone", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.SlpApplications", "Email", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SlpApplications", "Email");
            DropColumn("dbo.SlpApplications", "Phone");
        }
    }
}
