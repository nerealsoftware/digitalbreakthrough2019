using System.Data.Entity.Migrations;

namespace DB2019.Backend.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Db2019DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Db2019DbContext context)
        {
        }
    }
}