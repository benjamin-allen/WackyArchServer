﻿using Microsoft.EntityFrameworkCore;

namespace WackyArchServer.Model
{
    public class WAContext : DbContext
    {
        public WAContext(DbContextOptions<WAContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AlphaChallenge> AlphaChallenges { get; set; }
        public DbSet<AlphaChallengeTest> AlphaChallengeTests { get; set; }
        public DbSet<RunLog> RunLogs { get; set; }
    }
}
