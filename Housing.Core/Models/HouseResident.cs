using System;

namespace Housing.Core.Models
{
    public class HouseResident
    {
        public Guid HouseResidentId { get; set; }
        public Guid HouseId { get; set; }
        public House House { get; set; }
    }
}