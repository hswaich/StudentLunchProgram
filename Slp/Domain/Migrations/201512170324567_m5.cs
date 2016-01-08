namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Races", "SlpApplication_Id", "dbo.SlpApplications");
            DropIndex("dbo.Races", new[] { "SlpApplication_Id" });
            DropColumn("dbo.Races", "SlpApplication_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Races", "SlpApplication_Id", c => c.Guid());
            CreateIndex("dbo.Races", "SlpApplication_Id");
            AddForeignKey("dbo.Races", "SlpApplication_Id", "dbo.SlpApplications", "Id");
        }
    }
}
