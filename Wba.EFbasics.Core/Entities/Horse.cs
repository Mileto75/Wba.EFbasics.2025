using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class Horse : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public decimal Weight { get; set; }
        //a horse has one race
        //navigation property
        public Race Race { get; set; }
        public int? RaceId { get; set; } //unshadowed foreign key property
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
