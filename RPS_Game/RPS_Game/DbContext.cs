using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace RPS_Game
{
    public class RPS_DbContext : DbContext
    {
        // set up for each class in project
        public DbSet<Game> Games { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Player> Players { get; set; }

        public RPS_DbContext() { }

        public RPS_DbContext(DbContextOptions<RPS_DbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options) // options is name of context builder
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite("Data Source = rpsGame.db");
            }
        }
    }
}
