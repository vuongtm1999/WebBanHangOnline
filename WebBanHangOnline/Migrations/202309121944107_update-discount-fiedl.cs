namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatediscountfiedl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_CodeDiscount", "Discount", c => c.Single());
            AlterColumn("dbo.tb_Product", "Discount", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Product", "Discount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.tb_CodeDiscount", "Discount", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
