using System.Data.Entity;
using DB2019.Backend.Data.Entities;

namespace DB2019.Backend.Data
{
    public class Db2019DbContext : DbContext
    {
        static Db2019DbContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<Db2019DbContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}