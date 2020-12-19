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
            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<HousingResident, HousingResidentDto>();
            CreateMap<HousingResidentDto, HousingResident>();
            CreateMap<HousingOwner, HousingOwnerDto>();
            CreateMap<HousingOwnerDto, HousingOwner>();
        }
    }
}
