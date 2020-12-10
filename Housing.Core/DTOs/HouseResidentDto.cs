using System;
using Housing.Core.Models;

namespace Housing.Core.DTOs
{
    public class HouseResidentDto
    {
        public Guid HouseResidentId { get; set; }
        public Guid HouseId { get; set; }
        public HouseResidentDto House { get; set; }
    }
}