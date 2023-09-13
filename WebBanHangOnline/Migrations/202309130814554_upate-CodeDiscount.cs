namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upateCodeDiscount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_CodeDiscount", "OrderId", c => c.Int());
            AlterColumn("dbo.tb_CodeDiscount", "CustomerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_CodeDiscount", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_CodeDiscount", "OrderId", c => c.Int(nullable: false));
        }
    }
}
