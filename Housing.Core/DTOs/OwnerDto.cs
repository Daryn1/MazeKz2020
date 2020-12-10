using System;
using System.Collections.Generic;

namespace Housing.Core.DTOs
{
    public class OwnerDto
    {
        public Guid OwnerId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public ICollection<HouseResidentDto> Houses { get; set; } 
    }
}