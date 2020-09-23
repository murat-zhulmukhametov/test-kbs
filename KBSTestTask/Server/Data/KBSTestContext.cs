using KBSTestTask.Shared;
using Microsoft.EntityFrameworkCore;
using System;

namespace KBSTestTask.Server.Data
{
    public class KBSTestContext : DbContext
    {
        public KBSTestContext (DbContextOptions<KBSTestContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(new[] {
                new City()
            {
                Id = Guid.NewGuid(),
                Name = "Екатеринбург",
                CitizensCount = 1493749,
                FoundationDate = new DateTime(1723, 11, 18)
            },
                new City()
            {
                Id = Guid.NewGuid(),
                Name = "Челябинск",
                CitizensCount = 1196680,
                FoundationDate = new DateTime(1736, 9, 13)
            }
            });
        }

        public DbSet<City> Cities { get; set; }
    }
}
