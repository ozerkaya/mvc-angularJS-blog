namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Other", "Caption", c => c.String(maxLength: 100, storeType: "nvarchar"));
            CreateIndex("dbo.Other", "Caption", name: "IX_CAPTION");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Other", "IX_CAPTION");
            AlterColumn("dbo.Other", "Caption", c => c.String(unicode: false));
        }
    }
}
