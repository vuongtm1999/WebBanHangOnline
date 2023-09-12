namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCodediscoun1309 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_CodeDiscount", "isUsed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_CodeDiscount", "isUsed");
        }
    }
}
