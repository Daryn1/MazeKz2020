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
        public HousingOwner Owner { get; set; }
        public long? HouseId { get; set; }
        public House House { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
