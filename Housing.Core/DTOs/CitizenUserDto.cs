using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Housing.Core.DTOs
{
   public class CitizenUserDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [MaxLength(40)]
        public string Password { get; set; }

        public string AvatarUrl { get; set; }
    }
}
