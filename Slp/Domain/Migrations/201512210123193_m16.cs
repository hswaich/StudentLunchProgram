namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m16 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SlpApplicationRaces");
            AddPrimaryKey("dbo.SlpApplicationRaces", new[] { "SlpApplicationId", "RaceId" });
            DropColumn("dbo.SlpApplicationRaces", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlpApplicationRaces", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.SlpApplicationRaces");
            AddPrimaryKey("dbo.SlpApplicationRaces", "Id");
        }
    }
}
