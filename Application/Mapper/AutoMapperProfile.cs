using AutoMapper;

using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDTO, AppUser>().ReverseMap();

            CreateMap<GetAllUserDTO, AppUser>().ReverseMap();
           



           


        }
    }
}
