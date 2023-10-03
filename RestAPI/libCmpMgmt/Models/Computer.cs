using System;
using System.Collections.Generic;

#nullable disable

namespace libCmpMgmt.Models
{
    public partial class Computer
    {
        public string Sn { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Ram { get; set; }
        public int? Hdd { get; set; }
        public string Formfactor { get; set; }
        public int? Size { get; set; }
        public string UserId { get; set; }

        public virtual CompUser User { get; set; }
    }
}
