namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => new { t.CategoryId, t.Name }, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Tags", new[] { "CategoryId", "Name" });
            DropIndex("dbo.Categories", new[] { "Name" });
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
        }
    }
}
