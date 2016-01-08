namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SlpApplicationRaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationId = c.Guid(nullable: false),
                        RaceId = c.Int(nullable: false),
                        SlpApplication_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Races", t => t.RaceId, cascadeDelete: true)
                .ForeignKey("dbo.SlpApplications", t => t.SlpApplication_Id)
                .Index(t => t.RaceId)
                .Index(t => t.SlpApplication_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlpApplicationRaces", "SlpApplication_Id", "dbo.SlpApplications");
            DropForeignKey("dbo.SlpApplicationRaces", "RaceId", "dbo.Races");
            DropIndex("dbo.SlpApplicationRaces", new[] { "SlpApplication_Id" });
            DropIndex("dbo.SlpApplicationRaces", new[] { "RaceId" });
            DropTable("dbo.SlpApplicationRaces");
        }
    }
}
