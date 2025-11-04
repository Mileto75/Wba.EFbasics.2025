using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wba.EFbasics.Core.Entities
{
    public class Identification : BaseEntity
    {
        public string IdentificationCode { get; set; }
        public Horse Horse { get; set; }
    }
}
