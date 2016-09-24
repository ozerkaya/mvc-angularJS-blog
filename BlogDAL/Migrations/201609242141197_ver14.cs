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
                        Title = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ViewLogs");
        }
    }
}
