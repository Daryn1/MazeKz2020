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
        public bool IsApplied { get; set; }
        public long HouseId { get; set; }
        public virtual House House { get; set; }
        public long OwnerId { get; set; }
        public virtual HousingOwner Owner { get; set; }
    }
}
