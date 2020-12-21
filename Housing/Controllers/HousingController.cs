using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Enums;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Housing.Controllers
{
    [Controller]
    public class HousingController : Controller
    {
        private readonly IHouseRepository _repos;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;
        public HousingController(IHouseRepository repos, IMapper mapper, IWebHostEnvironment environment)
        {
            _repos = repos;
            _mapper = mapper;
            _environment = environment;
        }
        public IActionResult AddHouse()
        {
            return View();
        }
        [HttpGet("/housing/houses/maxprice")]
        public async Task<double> GetMaxHousePrice()
        {
            return await _repos.GetMaxHousePrice();
        }
        [HttpGet("/housing/houses/minprice")]
        public async Task<double> GetMinHousePrice()
        {
            return await _repos.GetMinHousePrice();
        }

        [HttpPost]
        public async Task<IActionResult> AddHouse(HouseDto house)
        {
            string error;
            if (!ModelState.IsValid)
            {
                error = "Заполните все поля";
                return RedirectToAction("ProfilePage", "Profile", new { error });
            }
            if(house.Type == HouseType.Ничего)
            {
                error = "Выберите тип недвижимости";
                return RedirectToAction("ProfilePage", "Profile", new { error });
            }
            string houseImagePath = "/HouseImages/" + house.ImageFile.FileName;
            house.ImagePath = houseImagePath;
            using (var stream = new FileStream(_environment.WebRootPath + houseImagePath, FileMode.Create))
            {
               await house.ImageFile.CopyToAsync(stream);
            }
            var houseModel = _mapper.Map<House>(house);
            houseModel.IsBought = true;
            houseModel.OwnerId = long.Parse(HttpContext.Session.GetString("Id"));
            if (await _repos.Create(houseModel) != null) return RedirectToAction("ProfilePage", "Profile");
            error = "Не удалось опубликовать недвижимость";
            return RedirectToAction("ProfilePage", "Profile", new { error });
        }
        public async Task<IActionResult> Houses(string errorMessage, FilteredHouseDto house)
        {
            if (!house.HasAllDefaultValues())
            {
                ViewBag.Houses = await _repos.GetFilteredHouses(house);
            }
            else
            {
                ViewBag.LoginErrorMessage = errorMessage;
                ViewBag.Houses = await _repos.GetAll();
            }
            return View();
        }
        [Route("/Housing/Houses/id={id}")]
        public async Task<IActionResult> HousePage(long id, string errorMessage, string cartError)
        {
           var house = await _repos.GetById(id);
            ViewBag.UpdateHouseErrorMessage = errorMessage;
            ViewBag.CartError = cartError;
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

        [HttpPost("/Housing/Houses/id={id}/update/image")]
        public async Task<IActionResult> UpdateHouseImageAsync(IFormFile houseFile, long id)
        {
            if(houseFile != null)
            {
                var house = await _repos.GetById(id);
                var newHouseImagePath = "/HouseImages/" + houseFile.FileName;
                System.IO.File.Delete(_environment.WebRootPath + house.ImagePath);
                using(var stream = new FileStream(_environment.WebRootPath + newHouseImagePath, FileMode.Create))
                {
                    houseFile.CopyToAsync(stream);
                }
                house.ImagePath = newHouseImagePath;
                if(await _repos.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }

        [HttpPost("/Housing/Houses/id={id}/delete")]
        public async Task<IActionResult> DeleteHouseAsync(long id)
        {
            if (await _repos.DeleteById(id)) return RedirectToAction("ProfilePage", "Profile");
            string deleteError = "Не удалось удалить недвижимость";
            return RedirectToAction("ProfilePage", "Profile", new { deleteError });
        }
    }
}
