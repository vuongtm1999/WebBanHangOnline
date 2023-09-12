namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct1209 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Category_Id", c => c.Int());
            CreateIndex("dbo.tb_Order", "Category_Id");
            AddForeignKey("dbo.tb_Order", "Category_Id", "dbo.tb_ProductCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_Order", "Category_Id", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Order", new[] { "Category_Id" });
            DropColumn("dbo.tb_Order", "Category_Id");
        }
    }
}
