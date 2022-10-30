using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Helpers
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Phone, PhoneDTO>()
                .ForMember(dest => dest.ColorName, 
                           opt => opt.MapFrom(src => src.Color.Name));
            CreateMap<PhoneDTO, Phone>();
        }
    }
}
