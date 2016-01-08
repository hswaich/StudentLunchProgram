namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m28 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberIncomeResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Guid(nullable: false),
                        IncomeQuestionId = c.Int(nullable: false),
                        Response = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncomeQuestions", t => t.IncomeQuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.IncomeQuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberIncomeResponses", "MemberId", "dbo.Members");
            DropForeignKey("dbo.MemberIncomeResponses", "IncomeQuestionId", "dbo.IncomeQuestions");
            DropIndex("dbo.MemberIncomeResponses", new[] { "IncomeQuestionId" });
            DropIndex("dbo.MemberIncomeResponses", new[] { "MemberId" });
            DropTable("dbo.MemberIncomeResponses");
        }
    }
}
