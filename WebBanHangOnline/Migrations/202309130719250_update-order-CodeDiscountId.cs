namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderCodeDiscountId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.tb_Order", name: "CodeDiscount_Id", newName: "CodeDiscountId");
            RenameIndex(table: "dbo.tb_Order", name: "IX_CodeDiscount_Id", newName: "IX_CodeDiscountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tb_Order", name: "IX_CodeDiscountId", newName: "IX_CodeDiscount_Id");
            RenameColumn(table: "dbo.tb_Order", name: "CodeDiscountId", newName: "CodeDiscount_Id");
        }
    }
}
