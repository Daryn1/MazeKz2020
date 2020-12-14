using System;
using System.Collections.Generic;
using Housing.Core.Enums;
using Housing.Core.Models;

namespace Housing.Core.DTOs
{
    public class HouseDto
    {
        public long HouseId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Info { get; set; }
        public bool IsBought { get; set; }
        public double Price { get; set; }
        public HouseType Type { get; set; }
        public ICollection<HousingUserDto> HouseResidents { get; set; }
        public long OwnerId { get; set; }
        public HousingUserDto Owner { get; set; }
    }
}