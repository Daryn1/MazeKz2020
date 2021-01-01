using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Housing.Core.Enums;
using Housing.Core.Models;
using Microsoft.AspNetCore.Http;

namespace Housing.Core.DTOs
{
    public class HouseDto
    {
        public long HouseId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Info { get; set; }
        public bool IsSelling { get; set; }
        [Required]
        public double Price { get; set; }
        public IFormFile ImageFile { get; set; }
        public string ImagePath { get; set; }
        public HouseType Type { get; set; }
        public List<HousingResidentDto> HouseResidents { get; set; }
        public long OwnerId { get; set; }
        public HousingOwnerDto Owner { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}