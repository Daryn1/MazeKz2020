using System;
using System.Collections.Generic;
using Housing.Core.Models;

namespace Housing.Core.DTOs
{
    public class HouseResidentDto
    {
        public Guid HouseId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Info { get; set; }
        public double Price { get; set; }
        public ICollection<HouseResidentDto> HouseResidents { get; set; }
        public Guid OwnerId { get; set; }
        public OwnerDto Owner { get; set; }
    }
}