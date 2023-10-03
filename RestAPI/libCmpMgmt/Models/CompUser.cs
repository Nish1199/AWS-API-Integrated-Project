using System;
using System.Collections.Generic;

#nullable disable

namespace libCmpMgmt.Models
{
    public partial class CompUser
    {
        public CompUser()
        {
            Computers = new HashSet<Computer>();
        }

        public string UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Dept { get; set; }
        public int Office { get; set; }
        public string Position { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
    }
}
