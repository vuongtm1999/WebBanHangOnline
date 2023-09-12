namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1309 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Customers", newName: "tb_Customer");
            CreateTable(
                "dbo.tb_CodeDiscount",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        code = c.String(nullable: false, maxLength: 20),
                        OrderId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.tb_Product", "Discount", c => c.Int(nullable: false));
            DropColumn("dbo.tb_Product", "PriceSale");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
            DropForeignKey("dbo.tb_CodeDiscount", "CustomerId", "dbo.tb_Customer");
            DropIndex("dbo.tb_CodeDiscount", new[] { "CustomerId" });
            DropColumn("dbo.tb_Product", "Discount");
            DropTable("dbo.tb_CodeDiscount");
            RenameTable(name: "dbo.tb_Customer", newName: "Customers");
        }
    }
}
