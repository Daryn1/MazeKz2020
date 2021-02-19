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
            var heroes = new List<HeroViewModel>();
            HeroViewModel knight = new HeroViewModel() { Name = "Knight", HP = 625, Mana = 150.0, Properties = new List<string>() { "Solid steel", "Sharp sword" } };
            heroes.Add(knight);
            HeroViewModel mage = new HeroViewModel() { Name = "Mage", HP = 587, Mana = 175.5, Properties = new List<string>() { "Sorcerer's miracle", "Cataclysm" } };
            heroes.Add(mage);
            HeroViewModel alchemist = new HeroViewModel() { Name = "Alchemist", HP = 804, Mana = 100, Properties = new List<string>() { "Poison rage", "Unshattered explosion" } };
            heroes.Add(alchemist);

            return View(heroes);
        }
    }
}
