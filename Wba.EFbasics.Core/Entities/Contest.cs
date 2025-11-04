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
        //custom many to many
        public ICollection<ContestHorse> Horses { get; set; }
    }
}
