namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SocialContacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Platform = c.String(unicode: false),
                        Image = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Posts", "date", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Posts", "postDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "postDate", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Posts", "date");
            DropTable("dbo.SocialContacts");
        }
    }
}
