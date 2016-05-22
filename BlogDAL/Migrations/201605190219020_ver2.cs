namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "date", c => c.DateTime(nullable: false, precision: 0));
            DropColumn("dbo.Users", "username");
            DropColumn("dbo.Users", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "password", c => c.String());
            AddColumn("dbo.Users", "username", c => c.String());
            DropColumn("dbo.Users", "date");
        }
    }
}
