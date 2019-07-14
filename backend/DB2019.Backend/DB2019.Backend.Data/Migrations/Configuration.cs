using System;
using System.Data.Entity.Migrations;
using DB2019.Backend.Data.Entities;

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
            var rnd = new Random(10);

            var issues = new Issue[120];
            var dt = new DateTime(2019,07,13,23,30,0);

            context.Users.AddOrUpdate(x=>x.Id, 
                new Entities.User() { DeviceId = "12345", Id = 1 });

            for (int index = 0; index < issues.Length; index++)
            {
                var issue = new Issue()
                {
                    Id = index + 1,
                    CategoryId = 1 + (index % 6),
                    UserId = 1,
                    Comment = $"Автоматически сгенерированное обращение {index + 1}",
                    Latitude = 58.604 + (rnd.NextDouble() - 0.5) / 10,
                    Longitude = 49.640 + (rnd.NextDouble() - 0.5) / 10,
                    CreatedTime = dt.AddSeconds(-rnd.NextDouble()*300)
                };
                issues[index] = issue;
            }

            context.Issues.AddOrUpdate(x=>x.Id, issues);

        }
    }
}