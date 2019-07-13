using System.Data.Entity;
using DB2019.Backend.Data.Entities;
using DB2019.Backend.Data.Migrations;
using Category = DB2019.Backend.Data.Entities.Category;
using User = DB2019.Backend.Data.Entities.User;

namespace DB2019.Backend.Data
{
    public class Db2019DbContext : DbContext
    {
        static Db2019DbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db2019DbContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}