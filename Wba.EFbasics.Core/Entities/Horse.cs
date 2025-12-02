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
        //[Required]
        //[MaxLength(100)]
        public string Name { get; set; }
                
        public decimal Weight { get; set; }
        //a horse has one race
        //navigation property
        //one to many
        public Race Race { get; set; }
        public int? RaceId { get; set; } //unshadowed foreign key property
        public DateTime DateOfBirth { get; set; }
        //[Required]
        //[MaxLength(100)]
        public string Country { get; set; }
        //one to one relation
        public Identification Identification { get; set; }
        public decimal Price { get; set; }
        public int IdentificationId { get; set; }
        //horse does many contests
        //many to many by convention
        public ICollection<Contest> Contests { get; set; }
        //image
        public string ImageFilename { get; set; }
    }
}
