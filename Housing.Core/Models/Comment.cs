using System;
using System.Collections.Generic;
using System.Text;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Models
{
    public class Comment
    {
        public long CommentId { get; set; }
        public DateTime LeavedAt { get; set; }
        public string Text { get; set; }
        public long HousingUserId { get; set; }
        public HousingUser HousingUser { get; set; }
    }
}
