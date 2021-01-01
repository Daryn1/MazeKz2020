using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.Models
{
    public class HousingResidentRequest
    {
        public long Id { get; set; }
        public DateTime SentAt { get; set; }
        public string ExtraInfo { get; set; }
        public bool IsApplied { get; set; }
        public long HouseId { get; set; }
        public House House { get; set; }
        public long ResidentId { get; set; }
        public HousingResident Resident { get; set; }
    }
}
