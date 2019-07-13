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
                "(N'Дороги'), (N'Тротуары'), (N'Мусор'), (N'Транспорт'), (N'Отопление (в зимний период)'), (N'Образование детей')");
        }
        
        public override void Down()
        {
        }
    }
}
