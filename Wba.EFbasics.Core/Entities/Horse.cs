using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class Horse : BaseEntity
    {
        public string Name { get; set; }
        public decimal Weight { get; set; }
        //a horse has one race
        public Race Race { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
    }
}
