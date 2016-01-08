namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m22 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MemberChildAttributes", name: "ChildAttributeTypeId", newName: "AttributeTypeId");
            RenameIndex(table: "dbo.MemberChildAttributes", name: "IX_ChildAttributeTypeId", newName: "IX_AttributeTypeId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.MemberChildAttributes", name: "IX_AttributeTypeId", newName: "IX_ChildAttributeTypeId");
            RenameColumn(table: "dbo.MemberChildAttributes", name: "AttributeTypeId", newName: "ChildAttributeTypeId");
        }
    }
}
