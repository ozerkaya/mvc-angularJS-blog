namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ip = c.String(unicode: false),
                        Post_ID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewLogs", "Post_ID", "dbo.Posts");
            DropIndex("dbo.ViewLogs", new[] { "Post_ID" });
            DropTable("dbo.ViewLogs");
        }
    }
}
