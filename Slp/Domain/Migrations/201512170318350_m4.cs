namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ethnicities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                        SlpApplication_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SlpApplications", t => t.SlpApplication_Id)
                .Index(t => t.SlpApplication_Id);
            
            AddColumn("dbo.SlpApplications", "TotalMembers", c => c.Int(nullable: false));
            AddColumn("dbo.SlpApplications", "MemberLastFourSSN", c => c.Int(nullable: false));
            AddColumn("dbo.SlpApplications", "NoSSN", c => c.Boolean(nullable: false));
            AddColumn("dbo.SlpApplications", "StreetAddress", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.SlpApplications", "AptNo", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.SlpApplications", "City", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.SlpApplications", "State", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.SlpApplications", "Zip", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.SlpApplications", "AdultFilledByName", c => c.String(maxLength: 200, unicode: false));
            AddColumn("dbo.SlpApplications", "AdultFilledBySignature", c => c.String(maxLength: 200, unicode: false));
            AddColumn("dbo.SlpApplications", "EthnicityId", c => c.Int());
            AlterColumn("dbo.SlpApplications", "ChildIncome", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.SlpApplications", "EthnicityId");
            AddForeignKey("dbo.SlpApplications", "EthnicityId", "dbo.Ethnicities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Races", "SlpApplication_Id", "dbo.SlpApplications");
            DropForeignKey("dbo.SlpApplications", "EthnicityId", "dbo.Ethnicities");
            DropIndex("dbo.Races", new[] { "SlpApplication_Id" });
            DropIndex("dbo.SlpApplications", new[] { "EthnicityId" });
            AlterColumn("dbo.SlpApplications", "ChildIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SlpApplications", "EthnicityId");
            DropColumn("dbo.SlpApplications", "AdultFilledBySignature");
            DropColumn("dbo.SlpApplications", "AdultFilledByName");
            DropColumn("dbo.SlpApplications", "Zip");
            DropColumn("dbo.SlpApplications", "State");
            DropColumn("dbo.SlpApplications", "City");
            DropColumn("dbo.SlpApplications", "AptNo");
            DropColumn("dbo.SlpApplications", "StreetAddress");
            DropColumn("dbo.SlpApplications", "NoSSN");
            DropColumn("dbo.SlpApplications", "MemberLastFourSSN");
            DropColumn("dbo.SlpApplications", "TotalMembers");
            DropTable("dbo.Races");
            DropTable("dbo.Ethnicities");
        }
    }
}
