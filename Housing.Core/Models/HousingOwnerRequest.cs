using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.Models
{
    public class HousingOwnerRequest
    {
        public long Id { get; set; }
        public DateTime SentAt { get; set; }
        public string ExtraInfo { get; set; }
        public long HouseId { get; set; }
        public House House { get; set; }
        public long OwnerId { get; set; }
        public HousingOwner Owner { get; set; }
    }
}
