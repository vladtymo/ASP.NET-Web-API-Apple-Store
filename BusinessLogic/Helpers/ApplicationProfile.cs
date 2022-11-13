using AutoMapper;
using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
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
