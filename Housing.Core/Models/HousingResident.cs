using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Models
{
    public class HousingResident
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public virtual HousingOwner Owner { get; set; }
        public long? HouseId { get; set; }
        public virtual House House { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<HousingResidentRequest> ResidentRequests { get; set; }
    }
}
