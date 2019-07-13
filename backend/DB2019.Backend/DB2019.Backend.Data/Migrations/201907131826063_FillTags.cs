using System.Data.Entity.Migrations;

namespace DB2019.Backend.Data.Migrations
{
    public partial class FillTags : DbMigration
    {
        public override void Up()
        {
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'������'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'���'), (@id, N'�������'), (@id, N'�������� ���'), (@id, N'���� (������� ����)')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'��������'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'���'), (@id, N'��� ������'), (@id, N'��� ��������'), (@id, N'������ �������� ��� �����������')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'�����'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'������'), (@id, N'����������� ����������'), (@id, N'������')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'���������'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'���������� ������'), (@id, N'���������� �����������')");
            Sql(
                "declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'��������� (� ������ ������)'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'��� �����'), (@id, N'�������� �����'), (@id, N'������ ���������')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'����������� �����'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'��� ���� � ������ � ������'), (@id, N'��� ���� � ����� � ������')");
        }

        public override void Down()
        {
        }
    }
}