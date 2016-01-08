namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberIncomeResponseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberIncomeResponseId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FrequencyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Frequencies", t => t.FrequencyId, cascadeDelete: true)
                .ForeignKey("dbo.MemberIncomeResponses", t => t.MemberIncomeResponseId, cascadeDelete: true)
                .Index(t => t.MemberIncomeResponseId)
                .Index(t => t.FrequencyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberIncomeResponseDetails", "MemberIncomeResponseId", "dbo.MemberIncomeResponses");
            DropForeignKey("dbo.MemberIncomeResponseDetails", "FrequencyId", "dbo.Frequencies");
            DropIndex("dbo.MemberIncomeResponseDetails", new[] { "FrequencyId" });
            DropIndex("dbo.MemberIncomeResponseDetails", new[] { "MemberIncomeResponseId" });
            DropTable("dbo.MemberIncomeResponseDetails");
        }
    }
}
