namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThemeOptions", "BlogHeaderPhoto", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThemeOptions", "BlogHeaderPhoto");
        }
    }
}
