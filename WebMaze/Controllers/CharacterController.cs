using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMaze.Models;

namespace WebMaze.Controllers
{
    public class CharacterController : Controller
    {
        public IActionResult Khassenov()
        {
            var heroes = new List<HeroViewModel>()
            {
                new HeroViewModel(){Name = "Knight", HP = 625, Mana = 150.0, Properties = new List<string>(){"Solid steel", "Sharp sword"}},
                new HeroViewModel(){Name = "Mage", HP = 587, Mana = 175.5, Properties = new List<string>(){"Metal fists"}},
                new HeroViewModel(){Name = "Alchemist",HP = 804, Mana = 100, Properties = new List<string>(){"Brutal view", "Axe attacks"}},
            };
            return View(heroes);
        }
    }
}
