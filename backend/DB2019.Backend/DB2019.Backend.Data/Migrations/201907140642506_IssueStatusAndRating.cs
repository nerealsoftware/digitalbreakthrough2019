namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IssueStatusAndRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Status", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Issues", "Rating", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "Rating");
            DropColumn("dbo.Issues", "Status");
        }
    }
}
