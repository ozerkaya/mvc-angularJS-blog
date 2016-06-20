namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "date", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "date");
        }
    }
}
