using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Models
{
    public class HousingOwner
    {
        public long Id { get; set; }
        public double Balance { get; set; }
        public long UserId { get; set; }
        public virtual CitizenUser User { get; set; }
        public virtual HousingResident HousingUser { get; set; }
        public virtual List<House> Houses { get; set; }
        public virtual List<CartHouse> CartHouses { get; set; }
        public virtual List<HousingOwnerRequest> OwnerRequests { get; set; }
    }
}
