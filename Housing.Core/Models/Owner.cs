using System;
using System.Collections.Generic;

namespace Housing.Core.Models
{
    public class Owner
    {
        public Guid OwnerId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public ICollection<House> Houses { get; set; }
    }
}