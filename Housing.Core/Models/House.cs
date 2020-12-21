using Housing.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Housing.Core.Models
{
    public class House
    {
        public long HouseId { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Info { get; set; }
        public bool IsBought { get; set; }
        public double Price { get; set; }
        public HouseType Type { get; set; }
        public int MaxResidentsCount { get; set; }
        public List<HousingResident> HousingUsers { get; set; }
        public List<Comment> Comments { get; set; }
        public long OwnerId { get; set; }
        public HousingOwner Owner { get; set; }
    }
}
