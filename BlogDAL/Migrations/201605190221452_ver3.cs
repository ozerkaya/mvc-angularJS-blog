namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "username", c => c.String(unicode: false));
            AddColumn("dbo.Users", "password", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "password");
            DropColumn("dbo.Users", "username");
        }
    }
}
