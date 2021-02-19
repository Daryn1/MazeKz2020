using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.Models
{
   public class CartHouse
    {
        public long Id { get; set; }
        public  long OwnerId { get; set; }
        public virtual HousingOwner Owner { get; set; }
        public long HouseId { get; set; }
        public virtual House House { get; set; }
    }
}
