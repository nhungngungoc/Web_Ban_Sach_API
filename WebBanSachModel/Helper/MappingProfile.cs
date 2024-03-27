using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSachModel.Dto;
using WebBanSachModel.Entity;

namespace WebBanSachModel.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        { 
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
