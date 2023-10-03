using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCmpMgmt.DTO
{
    public class ComputersDTO
    {
        public string Sn { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Ram { get; set; }
        public int? Hdd { get; set; }
        public string Formfactor { get; set; }
        public int? Size { get; set; }
        public string UserId { get; set; }
    }
}
