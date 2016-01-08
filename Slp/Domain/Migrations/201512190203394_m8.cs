namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Children", "SlpApplication_Id", "dbo.SlpApplications");
            DropIndex("dbo.Children", new[] { "SlpApplication_Id" });
            RenameColumn(table: "dbo.Children", name: "SlpApplication_Id", newName: "SlpApplicationId");
            AlterColumn("dbo.Children", "SlpApplicationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Children", "SlpApplicationId");
            AddForeignKey("dbo.Children", "SlpApplicationId", "dbo.SlpApplications", "Id", cascadeDelete: true);
            DropColumn("dbo.Children", "ApplicationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Children", "ApplicationId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Children", "SlpApplicationId", "dbo.SlpApplications");
            DropIndex("dbo.Children", new[] { "SlpApplicationId" });
            AlterColumn("dbo.Children", "SlpApplicationId", c => c.Guid());
            RenameColumn(table: "dbo.Children", name: "SlpApplicationId", newName: "SlpApplication_Id");
            CreateIndex("dbo.Children", "SlpApplication_Id");
            AddForeignKey("dbo.Children", "SlpApplication_Id", "dbo.SlpApplications", "Id");
        }
    }
}
