using AutoMapper;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
        
            CreateMap<ApplicationUser, VMGetUserList>().ReverseMap();
            CreateMap<LoginHistory,VMGetLoginHistories>().ReverseMap();
            CreateMap<LoginHistory,VMAddLoginHistory>().ReverseMap();
           
        }
    }
}
