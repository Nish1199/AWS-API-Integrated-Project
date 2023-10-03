using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using libCmpMgmt.Models;

namespace RestCmpMgmt.DTO
{
    public class JustUser
    {
        public string UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int Dept { get; set; }
        public int Office { get; set; }
        public string Position { get; set; }
    }
}
