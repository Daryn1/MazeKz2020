using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.DTOs
{
    public class CartHouseDto
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public HousingOwnerDto Owner { get; set; }
        public long HouseId { get; set; }
        public HouseDto House { get; set; }
    }
}
