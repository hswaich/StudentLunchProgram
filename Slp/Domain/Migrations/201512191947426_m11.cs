namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SlpApplicationRaces", "SlpApplication_Id", "dbo.SlpApplications");
            DropIndex("dbo.SlpApplicationRaces", new[] { "SlpApplication_Id" });
            RenameColumn(table: "dbo.SlpApplicationRaces", name: "SlpApplication_Id", newName: "SlpApplicationId");
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SlpApplicationId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200, unicode: false),
                        EarningsWork = c.Decimal(precision: 18, scale: 2),
                        EarningsWorkFrequencyId = c.Int(),
                        pa_cs_Al_Income = c.Decimal(precision: 18, scale: 2),
                        pa_cs_Al_IncomeFrequencyId = c.Int(),
                        pn_rt_ao_Income = c.Decimal(precision: 18, scale: 2),
                        pn_rt_ao_IncomeFrequencyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Frequencies", t => t.EarningsWorkFrequencyId)
                .ForeignKey("dbo.Frequencies", t => t.pa_cs_Al_IncomeFrequencyId)
                .ForeignKey("dbo.Frequencies", t => t.pn_rt_ao_IncomeFrequencyId)
                .ForeignKey("dbo.SlpApplications", t => t.SlpApplicationId, cascadeDelete: true)
                .Index(t => t.SlpApplicationId)
                .Index(t => t.EarningsWorkFrequencyId)
                .Index(t => t.pa_cs_Al_IncomeFrequencyId)
                .Index(t => t.pn_rt_ao_IncomeFrequencyId);
            
            AlterColumn("dbo.SlpApplicationRaces", "SlpApplicationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SlpApplicationRaces", "SlpApplicationId");
            AddForeignKey("dbo.SlpApplicationRaces", "SlpApplicationId", "dbo.SlpApplications", "Id", cascadeDelete: true);
            DropColumn("dbo.SlpApplicationRaces", "ApplicationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlpApplicationRaces", "ApplicationId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.SlpApplicationRaces", "SlpApplicationId", "dbo.SlpApplications");
            DropForeignKey("dbo.Members", "SlpApplicationId", "dbo.SlpApplications");
            DropForeignKey("dbo.Members", "pn_rt_ao_IncomeFrequencyId", "dbo.Frequencies");
            DropForeignKey("dbo.Members", "pa_cs_Al_IncomeFrequencyId", "dbo.Frequencies");
            DropForeignKey("dbo.Members", "EarningsWorkFrequencyId", "dbo.Frequencies");
            DropIndex("dbo.SlpApplicationRaces", new[] { "SlpApplicationId" });
            DropIndex("dbo.Members", new[] { "pn_rt_ao_IncomeFrequencyId" });
            DropIndex("dbo.Members", new[] { "pa_cs_Al_IncomeFrequencyId" });
            DropIndex("dbo.Members", new[] { "EarningsWorkFrequencyId" });
            DropIndex("dbo.Members", new[] { "SlpApplicationId" });
            AlterColumn("dbo.SlpApplicationRaces", "SlpApplicationId", c => c.Guid());
            DropTable("dbo.Members");
            RenameColumn(table: "dbo.SlpApplicationRaces", name: "SlpApplicationId", newName: "SlpApplication_Id");
            CreateIndex("dbo.SlpApplicationRaces", "SlpApplication_Id");
            AddForeignKey("dbo.SlpApplicationRaces", "SlpApplication_Id", "dbo.SlpApplications", "Id");
        }
    }
}
