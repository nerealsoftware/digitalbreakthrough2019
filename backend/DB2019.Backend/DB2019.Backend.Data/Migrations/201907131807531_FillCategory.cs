namespace DB2019.Backend.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FillCategory : DbMigration
    {
        public override void Up()
        {
            Sql(
                "insert into [dbo].[Categories] ([Name]) values " +
                "(N'������'), (N'��������'), (N'�����'), (N'���������'), (N'��������� (� ������ ������)'), (N'����������� �����')");
        }
        
        public override void Down()
        {
        }
    }
}
