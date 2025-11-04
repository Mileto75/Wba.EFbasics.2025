using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class ContestHorse
    {
        //one to Contest
        public int ContestId { get; set; }
        public Contest Contest { get; set; }
        //one to Horse
        public int HorseId { get; set; }
        public Horse Horse { get; set; }
        public int Ranking { get; set; }
    }
}
