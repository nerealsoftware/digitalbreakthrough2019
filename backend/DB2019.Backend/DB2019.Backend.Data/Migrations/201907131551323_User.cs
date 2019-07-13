namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.String(maxLength: 256),
                        SessionId = c.Guid(),
                        LastLoginTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DeviceId)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "SessionId" });
            DropIndex("dbo.Users", new[] { "DeviceId" });
            DropTable("dbo.Users");
        }
    }
}
