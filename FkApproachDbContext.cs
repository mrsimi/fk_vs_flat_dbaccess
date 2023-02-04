using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flat_vs_fk.Models;
using Microsoft.EntityFrameworkCore;

namespace flat_vs_fk
{
    public class FkApproachDbContext : DbContext
    {
        string connectionString = "server=localhost;user=root;password=Adegoke1234#;database=Chinook";
        MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Track>(b =>
            {
                b.HasKey(e => e.TrackId);
                b.Property(e => e.TrackId).ValueGeneratedOnAdd();
            });
        }

        public DbSet<Track> Track {get; set;}
        public DbSet<Genre> Genre {get; set;}
        public DbSet<Album> Album {get; set;}
        public DbSet<Artist> Artist {get; set;}
        public DbSet<MediaType> MediaType {get; set;}
    }
}