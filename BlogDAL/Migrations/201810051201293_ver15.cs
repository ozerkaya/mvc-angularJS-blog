namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Other",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Caption = c.String(unicode: false),
                        Fiyat = c.String(unicode: false),
                        Site = c.String(unicode: false),
                        Link = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Other");
        }
    }
}
