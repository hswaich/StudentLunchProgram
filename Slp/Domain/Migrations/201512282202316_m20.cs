namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "EarningsWorkFrequencyId", "dbo.Frequencies");
            DropForeignKey("dbo.Members", "pa_cs_Al_IncomeFrequencyId", "dbo.Frequencies");
            DropForeignKey("dbo.Members", "pn_rt_ao_IncomeFrequencyId", "dbo.Frequencies");
            DropForeignKey("dbo.Children", "SlpApplicationId", "dbo.SlpApplications");
            DropIndex("dbo.Children", new[] { "SlpApplicationId" });
            DropIndex("dbo.Members", new[] { "EarningsWorkFrequencyId" });
            DropIndex("dbo.Members", new[] { "pa_cs_Al_IncomeFrequencyId" });
            DropIndex("dbo.Members", new[] { "pn_rt_ao_IncomeFrequencyId" });
            CreateTable(
                "dbo.ChildAttributeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        Member_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.MemberChildAttributes",
                c => new
                    {
                        MemberId = c.Guid(nullable: false),
                        ChildAttributeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MemberId, t.ChildAttributeTypeId })
                .ForeignKey("dbo.ChildAttributeTypes", t => t.ChildAttributeTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.ChildAttributeTypeId);
            
            AddColumn("dbo.Members", "IsChild", c => c.Boolean(nullable: false));
            DropColumn("dbo.Members", "EarningsWork");
            DropColumn("dbo.Members", "EarningsWorkFrequencyId");
            DropColumn("dbo.Members", "pa_cs_Al_Income");
            DropColumn("dbo.Members", "pa_cs_Al_IncomeFrequencyId");
            DropColumn("dbo.Members", "pn_rt_ao_Income");
            DropColumn("dbo.Members", "pn_rt_ao_IncomeFrequencyId");
            DropTable("dbo.Children");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Children",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SlpApplicationId = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 100, unicode: false),
                        MiddleInitial = c.String(maxLength: 1, unicode: false),
                        LastName = c.String(maxLength: 100, unicode: false),
                        IsStudent = c.Boolean(nullable: false),
                        IsFosterChild = c.Boolean(nullable: false),
                        IsHMR = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Members", "pn_rt_ao_IncomeFrequencyId", c => c.Int());
            AddColumn("dbo.Members", "pn_rt_ao_Income", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Members", "pa_cs_Al_IncomeFrequencyId", c => c.Int());
            AddColumn("dbo.Members", "pa_cs_Al_Income", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Members", "EarningsWorkFrequencyId", c => c.Int());
            AddColumn("dbo.Members", "EarningsWork", c => c.Decimal(precision: 18, scale: 2));
            DropForeignKey("dbo.MemberChildAttributes", "MemberId", "dbo.Members");
            DropForeignKey("dbo.ChildAttributeTypes", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.MemberChildAttributes", "ChildAttributeTypeId", "dbo.ChildAttributeTypes");
            DropIndex("dbo.MemberChildAttributes", new[] { "ChildAttributeTypeId" });
            DropIndex("dbo.MemberChildAttributes", new[] { "MemberId" });
            DropIndex("dbo.ChildAttributeTypes", new[] { "Member_Id" });
            DropColumn("dbo.Members", "IsChild");
            DropTable("dbo.MemberChildAttributes");
            DropTable("dbo.ChildAttributeTypes");
            CreateIndex("dbo.Members", "pn_rt_ao_IncomeFrequencyId");
            CreateIndex("dbo.Members", "pa_cs_Al_IncomeFrequencyId");
            CreateIndex("dbo.Members", "EarningsWorkFrequencyId");
            CreateIndex("dbo.Children", "SlpApplicationId");
            AddForeignKey("dbo.Children", "SlpApplicationId", "dbo.SlpApplications", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Members", "pn_rt_ao_IncomeFrequencyId", "dbo.Frequencies", "Id");
            AddForeignKey("dbo.Members", "pa_cs_Al_IncomeFrequencyId", "dbo.Frequencies", "Id");
            AddForeignKey("dbo.Members", "EarningsWorkFrequencyId", "dbo.Frequencies", "Id");
        }
    }
}
