namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeOptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BlogBossName = c.String(unicode: false),
                        BlogBossTitle = c.String(unicode: false),
                        BlogFooterText = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThemeOptions");
        }
    }
}
