using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.DTOs
{
    public class CommentDto
    {
        public long CommentId { get; set; }
        public DateTime LeavedAt { get; set; }
        public string Text { get; set; }
        public long HousingUserId { get; set; }
        public HousingUserDto HousingUser { get; set; }
    }
}
