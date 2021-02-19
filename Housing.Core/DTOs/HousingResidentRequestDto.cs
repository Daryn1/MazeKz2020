using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Housing.Core.DTOs
{
    public class HousingResidentRequestDto
    {
        public long Id { get; set; }
        public DateTime SentAt { get; set; }
        [Required]
        public string ExtraInfo { get; set; }
        [Required]
        public long HouseId { get; set; }
        public bool IsApplied { get; set; }
        public HouseDto House { get; set; }
        [Required]
        public long ResidentId { get; set; }
        public HousingResidentDto Resident { get; set; }
    }
}
