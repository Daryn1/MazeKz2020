using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Core.DTOs
{
    public class HouseComments
    {
        public long HouseId { get; set; }
        public HouseDto House{ get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
