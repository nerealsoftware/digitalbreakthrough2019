namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Categories", new[] { "Name" });
            DropIndex("dbo.Tags", new[] { "CategoryId", "Name" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Categories", "Name", unique: true);
            CreateIndex("dbo.Tags", new[] { "CategoryId", "Name" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "CategoryId", "Name" });
            DropIndex("dbo.Categories", new[] { "Name" });
            AlterColumn("dbo.Tags", "Name", c => c.String(maxLength: 128));
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tags", new[] { "CategoryId", "Name" }, unique: true);
            CreateIndex("dbo.Categories", "Name", unique: true);
        }
    }
}
