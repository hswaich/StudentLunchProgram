namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChildAttributeTypes", "Member_Id", "dbo.Members");
            DropIndex("dbo.ChildAttributeTypes", new[] { "Member_Id" });
            DropColumn("dbo.ChildAttributeTypes", "Member_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChildAttributeTypes", "Member_Id", c => c.Guid());
            CreateIndex("dbo.ChildAttributeTypes", "Member_Id");
            AddForeignKey("dbo.ChildAttributeTypes", "Member_Id", "dbo.Members", "Id");
        }
    }
}
