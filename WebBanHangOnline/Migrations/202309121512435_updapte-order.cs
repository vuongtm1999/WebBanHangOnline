namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updapteorder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_Order", "Category_Id", "dbo.tb_ProductCategory");
            DropIndex("dbo.tb_Order", new[] { "Category_Id" });
            DropColumn("dbo.tb_Order", "Category_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "Category_Id", c => c.Int());
            CreateIndex("dbo.tb_Order", "Category_Id");
            AddForeignKey("dbo.tb_Order", "Category_Id", "dbo.tb_ProductCategory", "Id");
        }
    }
}
