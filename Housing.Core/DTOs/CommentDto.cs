using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Housing.Core.DTOs
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public DateTime LeavedAt { get; set; }
        public string LeavedAtString => LeavedAt.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"));
        [Required]
        public string Text { get; set; }
        public long HouseId { get; set; }
        public HouseDto House { get; set; }
        public long UserId { get; set; }
        public HousingResidentDto User { get; set; }
    }
}
