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
        public bool IsSelling { get; set; }
        public double Price { get; set; }
        public HouseType Type { get; set; }
        public int MaxResidentsCount { get; set; }
        public virtual List<HousingResident> HousingUsers { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public long OwnerId { get; set; }
        public virtual HousingOwner Owner { get; set; }
    }
}
