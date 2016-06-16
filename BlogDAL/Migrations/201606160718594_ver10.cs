namespace BlogDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ver10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Labels", "LabelTypes_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Labels", "LabelTypes_ID");
            AddForeignKey("dbo.Labels", "LabelTypes_ID", "dbo.LabelTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Labels", "LabelTypes_ID", "dbo.LabelTypes");
            DropIndex("dbo.Labels", new[] { "LabelTypes_ID" });
            DropColumn("dbo.Labels", "LabelTypes_ID");
        }
    }
}
