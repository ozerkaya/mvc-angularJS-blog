namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver17 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Other", "Fiyat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Other", "Fiyat", c => c.String(unicode: false));
        }
    }
}
