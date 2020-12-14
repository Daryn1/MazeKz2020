using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    public class HousingController : Controller
    {
        private readonly IHouseRepository _repos;
        private IMapper _mapper;
        public HousingController(IHouseRepository repos, IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }
        public IActionResult AddHouse()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddHouse(HouseDto house)
        {
            var houseModel = _mapper.Map<House>(house);
            if (await _repos.Create(houseModel)) return RedirectToAction("Houses");
            ViewBag.ErrorMessage = "Failed to add house";
            return View();
        }
        public IActionResult Houses()
        {
            return View(_repos.GetAll());
        }
    }
}
