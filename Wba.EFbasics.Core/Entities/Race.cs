using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class Race : BaseEntity
    {
        public string Name { get; set; }
        //a race has many horses
        //navigation property
        public ICollection<Horse> Horses { get; set; }
    }
}
