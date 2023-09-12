namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecodeDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_CodeDiscount", "Discount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_CodeDiscount", "Discount");
        }
    }
}
