namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "postDate", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Posts", "date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "date", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Posts", "postDate");
        }
    }
}
