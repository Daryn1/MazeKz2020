using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Core.Enums;
using Housing.Core.Models;

namespace Housing.Core.DTOs
{
    public class HouseDto
    {
        public long HouseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Info { get; set; }
        public bool IsBought { get; set; }
        [Required]
        public double Price { get; set; }
        public HouseType Type { get; set; }
        public ICollection<HousingUserDto> HouseResidents { get; set; }
        public long OwnerId { get; set; }
        public HousingOwnerDto Owner { get; set; }
    }
}