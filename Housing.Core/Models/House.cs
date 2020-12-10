using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Core.Models
{
    public class House
    {
        public Guid HouseId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Info { get; set; }
        public bool IsBought { get; set; }
        public double Price { get; set; }
        public ICollection<HouseResident> HouseResidents { get; set; }
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
