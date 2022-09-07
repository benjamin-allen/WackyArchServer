using Microsoft.EntityFrameworkCore;

namespace WackyArchServer.Model
{
    public class WAContext : DbContext
    {
        public WAContext(DbContextOptions<WAContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}
