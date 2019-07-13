namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIssueCreatedTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "CreatedTime", c => c.DateTime(nullable: false, defaultValue: new DateTime(2019, 7, 13)));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "CreatedTime");
        }
    }
}
