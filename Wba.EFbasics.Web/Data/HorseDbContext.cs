using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Core.Entities;

namespace Wba.EFbasics.Web.Data
{
    public class HorseDbContext : DbContext
    {
        public HorseDbContext(DbContextOptions options) : base(options)
        {
        }

        //Define Dbsets => Tables
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Race> Races { get; set; }


    }
}
