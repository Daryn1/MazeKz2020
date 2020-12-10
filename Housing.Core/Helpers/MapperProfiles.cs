using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Models;

namespace Housing.Core.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<House, HouseResidentDto>();
            CreateMap<HouseResident, HouseResidentDto>();
            CreateMap<Owner, OwnerDto>();
        }
    }
}
