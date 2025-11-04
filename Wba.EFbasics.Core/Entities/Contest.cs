using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class Contest : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Distance { get; set; }
        //one contest has many
        //many to many by convention
        public ICollection<Horse> Horses { get; set; }
    }
}
