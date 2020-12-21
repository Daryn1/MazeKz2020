using System;
using System.Collections.Generic;
using Housing.Core.Models;
using WebMaze.DbStuff.Model;

namespace Housing.Core.DTOs
{
    public class HousingResidentDto
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public HousingOwnerDto Owner { get; set; }
        public long? HouseId { get; set; }
        public HouseDto House { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<HousingResidentRequestDto> ResidentRequests { get; set; }
    }
}