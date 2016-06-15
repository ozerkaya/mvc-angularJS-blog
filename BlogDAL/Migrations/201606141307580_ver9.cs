namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabelTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LabelTypes");
        }
    }
}
