namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIssue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Photo = c.Binary(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.IssueTags",
                c => new
                    {
                        IssueId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IssueId, t.TagId })
                .ForeignKey("dbo.Issues", t => t.IssueId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: false)
                .Index(t => t.IssueId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "UserId", "dbo.Users");
            DropForeignKey("dbo.IssueTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.IssueTags", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Issues", "CategoryId", "dbo.Categories");
            DropIndex("dbo.IssueTags", new[] { "TagId" });
            DropIndex("dbo.IssueTags", new[] { "IssueId" });
            DropIndex("dbo.Issues", new[] { "CategoryId" });
            DropIndex("dbo.Issues", new[] { "UserId" });
            DropTable("dbo.IssueTags");
            DropTable("dbo.Issues");
        }
    }
}
