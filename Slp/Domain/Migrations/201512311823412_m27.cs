namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IncomeQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 100, unicode: false),
                        Text = c.String(maxLength: 1000, unicode: false),
                        IsChild = c.Boolean(nullable: false),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IncomeQuestions");
        }
    }
}
