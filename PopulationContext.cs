using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Population.IO.Config;
using Serilog;

namespace Population.IO
{
    public class PopulationContext : DbContext
    {
        private readonly string _sqliteDb;

        private ILogger Logger { get; }

        public PopulationContext(InOutConfig config, ILogger logger)
        {
            _sqliteDb = config.OutputSqlLiteDbFile;
            Logger = logger;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Logger.Information("Population context configuring database options");
            Logger.Information("Data Source=" + _sqliteDb);
            optionsBuilder.UseSqlite("Data Source=" + _sqliteDb);
        }

        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Actual> Actuals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estimate>().HasKey(k => new { k.State, k.Districts });
            modelBuilder.Entity<Actual>().ToTable("Actuals");
            modelBuilder.Entity<Estimate>().ToTable("Estimates");
        }
    }
}
