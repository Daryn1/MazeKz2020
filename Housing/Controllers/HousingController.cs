using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Enums;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Controller]
    public class HousingController : Controller
    {
        private readonly IHouseRepository _repos;
        private readonly IMapper _mapper;
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
            string error;
            if (!ModelState.IsValid)
            {
                error = "Заполните все поля";
                return Redirect("/profile?houseCreateErrorMessage=" + error);
            }
            var houseModel = _mapper.Map<House>(house);
            houseModel.OwnerId = long.Parse(HttpContext.Session.GetString("Id"));
            if (await _repos.Create(houseModel) != null) return Redirect("/profile");
            error = "Не удалось опубликовать недвижимость";
            return Redirect("/profile?houseCreateErrorMessage=" + error);
        }
        public async Task<IActionResult> Houses(string errorMessage)
        {
            ViewBag.LoginErrorMessage = errorMessage;
            ViewBag.Houses = await _repos.GetAll();
            return View();
        }
        [Route("/Housing/Houses/id={id}")]
        public async Task<IActionResult> HousePage(long id, string errorMessage)
        {
           var house = await _repos.GetById(id);
            ViewBag.UpdateHouseErrorMessage = errorMessage;
            return View(_mapper.Map<HouseDto>(house));
        }

        [HttpPost("/Housing/Houses/id={id}/update/name")]
        public async Task<IActionResult> UpdateHouseNameAsync(string houseName, long id)
        {
            if (!string.IsNullOrEmpty(houseName))
            {
                var house = await _repos.GetById(id);
                house.Name = houseName;
                if (await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить название" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/street")]
        public async Task<IActionResult> UpdateHouseStreetAsync(string houseStreet, long id)
        {
            if (!string.IsNullOrEmpty(houseStreet))
            {
                var house = await _repos.GetById(id);
                house.Street = houseStreet;
                if (await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить улицу" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/info")]
        public async Task<IActionResult> UpdateHouseInfoAsync(string houseInfo, long id)
        {
            if (!string.IsNullOrEmpty(houseInfo))
            {
                var house = await _repos.GetById(id);
                house.Info = houseInfo;
                if (await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить информацию" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/price")]
        public async Task<IActionResult> UpdateHousePriceAsync(double housePrice, long id)
        {
            if (housePrice != default)
            {
                var house = await _repos.GetById(id);
                house.Price = housePrice;
                if (await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/type")]
        public async Task<IActionResult> UpdateHouseTypeAsync(string houseType, long id)
        {
            if (houseType != default)
            {
                var house = await _repos.GetById(id);
                house.Type = (HouseType)Enum.Parse(typeof(HouseType), houseType);
                if (await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }
    }
}
