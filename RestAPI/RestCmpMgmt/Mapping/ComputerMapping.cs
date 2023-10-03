using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using libCmpMgmt.Models;
using RestCmpMgmt.DTO;

namespace RestCmpMgmt.Mapping
{
    public class ComputerMapping : Profile
    {
        public ComputerMapping()
        {
            CreateMap<CompUser, JustUser>();
            CreateMap<JustUser, CompUser>();
            CreateMap<IEnumerable<CompUser>, IEnumerable<JustUser>>();
            CreateMap<Computer, ComputersDTO>();
            CreateMap<ComputersDTO, Computer>();
        }
    }
}
