namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Other", "Logo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Other", "Logo");
        }
    }
}
