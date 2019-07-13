using System.Data.Entity.Migrations;

namespace DB2019.Backend.Data.Migrations
{
    public partial class FillTags : DbMigration
    {
        public override void Up()
        {
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Дороги'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'яма'), (@id, N'трещина'), (@id, N'открытый люк'), (@id, N'лужа (копится вода)')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Тротуары'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'яма'), (@id, N'нет съезда'), (@id, N'нет тротуара'), (@id, N'другие проблемы при прохождении')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Мусор'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'свалка'), (@id, N'переполнены контейнеры'), (@id, N'вороны')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Транспорт'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'постоянная пробка'), (@id, N'повышенная аварийность')");
            Sql(
                "declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Отопление (в зимний период)'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'нет тепла'), (@id, N'холодный трубы'), (@id, N'утечка отопления')");
            Sql("declare @id int; select @id = [Id] from [dbo].[Categories] where [Name] = N'Образование детей'; " +
                "insert into [dbo].[Tags] ([CategoryId], [Name]) values (@id, N'нет мест в садике в районе'), (@id, N'нет мест в школе в районе')");
        }

        public override void Down()
        {
        }
    }
}