namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SlpApplications", "ChildIncomeFrequencyId", "dbo.Frequencies");
            DropIndex("dbo.SlpApplications", new[] { "ChildIncomeFrequencyId" });
            DropColumn("dbo.SlpApplications", "ChildIncome");
            DropColumn("dbo.SlpApplications", "ChildIncomeFrequencyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlpApplications", "ChildIncomeFrequencyId", c => c.Int());
            AddColumn("dbo.SlpApplications", "ChildIncome", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.SlpApplications", "ChildIncomeFrequencyId");
            AddForeignKey("dbo.SlpApplications", "ChildIncomeFrequencyId", "dbo.Frequencies", "Id");
        }
    }
}
