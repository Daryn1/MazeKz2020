using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMaze.Models
{
    public class HeroViewModel
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public double Mana { get; set; }
        public List<string> Properties { get; set; }
    }
}
