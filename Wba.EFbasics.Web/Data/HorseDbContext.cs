using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Core.Entities;

namespace Wba.EFbasics.Web.Data
{
    public class HorseDbContext : DbContext
    {
        public HorseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configure database constraints using fluent api
            #region Horse configuration
            modelBuilder.Entity<Horse>()
                .Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Horse>()
                .Property(h => h.Country)
                .IsRequired()
                .HasMaxLength(100);
            #endregion
            //configure Race entity
            #region Race config
            modelBuilder.Entity<Race>()
                .Property(h => h.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion
            //define combined key for ContestHorse
            #region ContestHorse
            //create combined key
            modelBuilder.Entity<ContestHorse>()
                .HasKey(ch => new {ch.ContestId,ch.HorseId });
            #endregion
            base.OnModelCreating(modelBuilder);
        }

        //Define Dbsets => Tables
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Contest> Contests { get; set; }
        public DbSet<ContestHorse> ContestHorse { get; set; }
        public DbSet<Identification> Identifications { get; set; }
    }
}
