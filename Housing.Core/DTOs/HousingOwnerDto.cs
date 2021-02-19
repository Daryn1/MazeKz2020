using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace Housing.Core.DTOs
{
   public class HousingOwnerDto
    {
        public long Id { get; set; }
        public double Balance { get; set; }
        public long UserId { get; set; }
        public CitizenUser User { get; set; }
        public HousingResidentDto HousingUser { get; set; }
        public List<HouseDto> Houses { get; set; }
        public List<CartHouseDto> CartHouses { get; set; }
        public List<HousingOwnerRequestDto> OwnerRequests { get; set; }
    }
}
