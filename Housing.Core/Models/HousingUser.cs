using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Models
{
    public class HousingUser
    {
        public long Id { get; set; }
        public double Balance { get; set; }
        public long UserId { get; set; }
        public CitizenUser User { get; set; }
        public long HouseId { get; set; }
        public House House { get; set; }
    }
}
