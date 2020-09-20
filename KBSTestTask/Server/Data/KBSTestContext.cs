using KBSTestTask.Shared;
using Microsoft.EntityFrameworkCore;

namespace KBSTestTask.Server.Data
{
    public class KBSTestContext : DbContext
    {
        public KBSTestContext (DbContextOptions<KBSTestContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
