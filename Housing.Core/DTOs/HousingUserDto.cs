using System;
using System.Collections.Generic;
using Housing.Core.Models;
using WebMaze.DbStuff.Model;

namespace Housing.Core.DTOs
{
    public class HousingUserDto
    {
        public long Id { get; set; }
        public double Balance { get; set; }
        public long HouseId { get; set; }
        public HouseDto House { get; set; }
        public long UserId { get; set; }
        public CitizenUser User { get; set; }
        public ICollection<HouseDto> Houses { get; set; }
    }
}