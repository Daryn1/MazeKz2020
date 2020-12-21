using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.Models
{
   public class CartHouse
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public HousingOwner Owner { get; set; }
        public long HouseId { get; set; }
        public House House { get; set; }
    }
}
