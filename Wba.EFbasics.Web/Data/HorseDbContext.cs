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
            base.OnModelCreating(modelBuilder);
        }

        //Define Dbsets => Tables
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Race> Races { get; set; }
    }
}
