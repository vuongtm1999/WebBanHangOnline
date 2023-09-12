namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatediscountModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_CodeDiscount", "CustomerId", "dbo.tb_Customer");
            DropIndex("dbo.tb_CodeDiscount", new[] { "CustomerId" });
            AddColumn("dbo.tb_CodeDiscount", "isActive", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Customer", "CodeDiscount_Id", c => c.Int());
            AddColumn("dbo.tb_Order", "CodeDiscount_Id", c => c.Int());
            CreateIndex("dbo.tb_Customer", "CodeDiscount_Id");
            CreateIndex("dbo.tb_Order", "CodeDiscount_Id");
            AddForeignKey("dbo.tb_Customer", "CodeDiscount_Id", "dbo.tb_CodeDiscount", "Id");
            AddForeignKey("dbo.tb_Order", "CodeDiscount_Id", "dbo.tb_CodeDiscount", "Id");
            DropColumn("dbo.tb_CodeDiscount", "isUsed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_CodeDiscount", "isUsed", c => c.Int(nullable: false));
            DropForeignKey("dbo.tb_Order", "CodeDiscount_Id", "dbo.tb_CodeDiscount");
            DropForeignKey("dbo.tb_Customer", "CodeDiscount_Id", "dbo.tb_CodeDiscount");
            DropIndex("dbo.tb_Order", new[] { "CodeDiscount_Id" });
            DropIndex("dbo.tb_Customer", new[] { "CodeDiscount_Id" });
            DropColumn("dbo.tb_Order", "CodeDiscount_Id");
            DropColumn("dbo.tb_Customer", "CodeDiscount_Id");
            DropColumn("dbo.tb_CodeDiscount", "isActive");
            CreateIndex("dbo.tb_CodeDiscount", "CustomerId");
            AddForeignKey("dbo.tb_CodeDiscount", "CustomerId", "dbo.tb_Customer", "CustomerId", cascadeDelete: true);
        }
    }
}
