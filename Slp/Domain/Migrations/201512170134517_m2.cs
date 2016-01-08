namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssistancePrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SlpApplications", "AssistanceProgramId", c => c.Int());
            AddColumn("dbo.SlpApplications", "AssistanceProgramCaseNumber", c => c.String());
            AddColumn("dbo.SlpApplications", "IsComplete", c => c.Boolean(nullable: false));
            CreateIndex("dbo.SlpApplications", "AssistanceProgramId");
            AddForeignKey("dbo.SlpApplications", "AssistanceProgramId", "dbo.AssistancePrograms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlpApplications", "AssistanceProgramId", "dbo.AssistancePrograms");
            DropIndex("dbo.SlpApplications", new[] { "AssistanceProgramId" });
            DropColumn("dbo.SlpApplications", "IsComplete");
            DropColumn("dbo.SlpApplications", "AssistanceProgramCaseNumber");
            DropColumn("dbo.SlpApplications", "AssistanceProgramId");
            DropTable("dbo.AssistancePrograms");
        }
    }
}
