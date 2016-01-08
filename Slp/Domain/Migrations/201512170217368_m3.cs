namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Frequencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SlpApplications", "ChildIncome", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SlpApplications", "ChildIncomeFrequencyId", c => c.Int());
            CreateIndex("dbo.SlpApplications", "ChildIncomeFrequencyId");
            AddForeignKey("dbo.SlpApplications", "ChildIncomeFrequencyId", "dbo.Frequencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlpApplications", "ChildIncomeFrequencyId", "dbo.Frequencies");
            DropIndex("dbo.SlpApplications", new[] { "ChildIncomeFrequencyId" });
            DropColumn("dbo.SlpApplications", "ChildIncomeFrequencyId");
            DropColumn("dbo.SlpApplications", "ChildIncome");
            DropTable("dbo.Frequencies");
        }
    }
}
