namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 100, unicode: false),
                        MiddleInitial = c.String(maxLength: 1, unicode: false),
                        LastName = c.String(maxLength: 100, unicode: false),
                        IsStudent = c.Boolean(nullable: false),
                        IsFosterChild = c.Boolean(nullable: false),
                        IsHMR = c.Boolean(nullable: false),
                        SlpApplication_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SlpApplications", t => t.SlpApplication_Id)
                .Index(t => t.SlpApplication_Id);
            
            CreateTable(
                "dbo.SlpApplications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Children", "SlpApplication_Id", "dbo.SlpApplications");
            DropIndex("dbo.Children", new[] { "SlpApplication_Id" });
            DropTable("dbo.SlpApplications");
            DropTable("dbo.Children");
        }
    }
}
