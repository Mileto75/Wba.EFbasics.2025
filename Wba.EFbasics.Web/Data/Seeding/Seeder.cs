using Microsoft.EntityFrameworkCore;
using Wba.EFbasics.Core.Entities;

namespace Wba.EFbasics.Web.Data.Seeding
{
    public static class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //seed the data
            #region Contests
            var contests = new Contest[]
            {
                new Contest{Id = 1,Name= "Poperingse Regatta",Distance = 12.2M,Location = "kortestraat 56, Poperinge" },
                new Contest{Id = 2,Name= "Veurnse Pannenkoekenrace",Distance = 8.2M,Location = "Langestraat 12, Veurne" },

            };
            #endregion
            #region Races
            var races = new Race[] 
            {
                new Race{Id = 1,Name = "Arabian FullBlood"},
                new Race{Id = 2,Name = "Brabants FarmerHorse"},
                new Race{Id = 3,Name = "Schoorse Shetlander Pony"}
            };
            #endregion
            #region Identifications
            var identifications = new Identification[]
            {
                new Identification {Id = 1,IdentificationCode = "Alfa56"},
                new Identification {Id = 2,IdentificationCode = "Tango95"},
                new Identification {Id = 3,IdentificationCode = "Papa44"},
            };
            #endregion
            #region Horses
            var horses = new Horse[] 
            {
                new Horse{Id = 1,IdentificationId = 1,Name = "Mighty Mouse",RaceId = 1,Country = "Belgium",Price = 8000M,Weight=250.3M,DateOfBirth = new DateTime(1975,2,7)},
                new Horse{Id = 2,IdentificationId = 2,Name = "Superbad",RaceId = 2,Country = "Italy",Price = 5000M,Weight=200.3M,DateOfBirth = new DateTime(2022,2,7)},
                new Horse{Id = 3,IdentificationId = 3,Name = "StrudelWasser",RaceId = 3,Price = 23000M,Country = "Germany",Weight=260.3M,DateOfBirth = new DateTime(2019,4,6)},
            };
            #endregion
            #region ContestHorses
            //use anonymous objects
            var contestHorses = new[]
            {
                new {ContestsId = 1,HorsesId = 1},
                new {ContestsId = 1,HorsesId = 2},
                new {ContestsId = 1,HorsesId = 3},
                new {ContestsId = 2,HorsesId = 1},
                new {ContestsId = 2,HorsesId = 2},
                new {ContestsId = 2,HorsesId = 3},
            };
            #endregion
            #region Call Hasdata
            //let op de volgorde!
            modelBuilder.Entity<Contest>().HasData(contests);
            modelBuilder.Entity<Race>().HasData(races);
            modelBuilder.Entity<Identification>().HasData(identifications);
            modelBuilder.Entity<Horse>().HasData(horses);
            //the many to many by convention
            modelBuilder.
                Entity($"{nameof(Contest)}{nameof(Horse)}") // = "ContestHorse"
                .HasData(contestHorses);
            #endregion
        }
    }
}
