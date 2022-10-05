using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WackyArchServer.Model
{
    public class WAContext : IdentityDbContext
    {
        public WAContext(DbContextOptions<WAContext> options) : base(options) { }

        public DbSet<AlphaChallenge> AlphaChallenges { get; set; }
        public DbSet<AlphaChallengeTest> AlphaChallengeTests { get; set; }
        public DbSet<CompletedChallenge> CompletedChallenges { get; set; }
        public DbSet<RunLog> RunLogs { get; set; }
        public DbSet<BetaChallenge> BetaChallenges { get; set; }
    }
}
