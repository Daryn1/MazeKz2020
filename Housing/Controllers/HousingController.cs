using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Enums;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Housing.Controllers
{
    [Controller]
    public class HousingController : Controller
    {
        private readonly IHouseService _houses;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public HousingController(IHouseService houses, IMapper mapper, IWebHostEnvironment environment)
        {
            _houses = houses;
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
            return await _houses.GetMaxHousePrice();
        }
        [HttpGet("/housing/houses/minprice")]
        public async Task<double> GetMinHousePrice()
        {
            return await _houses.GetMinHousePrice();
        }

        [HttpGet("/housing/houses/count")]
        public async Task<int> GetHousesCount()
        {
            return await _houses.GetHousesCount();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddHouse(HouseDto house)
        {
            string error;
            if (!ModelState.IsValid)
            {
                error = "Заполните все поля";
                return RedirectToAction("ProfilePage", "HousingProfile", new { error });
            }
            if(house.Type == HouseType.Ничего)
            {
                error = "Выберите тип недвижимости";
                return RedirectToAction("ProfilePage", "HousingProfile", new { error });
            }
            string houseImagePath = "/HouseImages/" + house.ImageFile.FileName;
            house.ImagePath = houseImagePath;
            using (var stream = new FileStream(_environment.WebRootPath + houseImagePath, FileMode.Create))
            {
               await house.ImageFile.CopyToAsync(stream);
            }
            var houseModel = _mapper.Map<House>(house);
            houseModel.OwnerId = long.Parse(HttpContext.Session.GetString("Id"));
            if (await _houses.Create(houseModel) != null) return RedirectToAction("ProfilePage", "HousingProfile");
            error = "Не удалось опубликовать недвижимость";
            return RedirectToAction("ProfilePage", "HousingProfile", new { error });
        }
        public async Task<IActionResult> Houses(string errorMessage, FilteredHouseDto house, int? page)
        {
            if (!house.HasAllDefaultValues())
            {
                ViewBag.Houses = _mapper.Map<ICollection<HouseDto>>(await _houses.GetFilteredHouses(house));
            }
            else
            {
                ViewBag.LoginErrorMessage = errorMessage;
                if (!page.HasValue)
                    ViewBag.Houses = _mapper.Map<ICollection<HouseDto>>(await _houses.GetAll());
                else
                    ViewBag.Houses = _mapper.Map<ICollection<HouseDto>>(await _houses.GetHousesByPage(page.Value, 4));
            }
            return View();
        }
        [HttpGet("/Housing/Houses/id={id}")]
        public async Task<IActionResult> HousePage(long id, string errorMessage, string cartError, string requestError)
        {
           var house = await _houses.GetById(id);
            ViewBag.UpdateHouseErrorMessage = errorMessage;
            ViewBag.CartError = cartError;
            ViewBag.RequestError = requestError;
            return View(_mapper.Map<HouseDto>(house));
        }

        [HttpPost("/Housing/Houses/id={id}/update/name")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseNameAsync(string houseName, long id)
        {
            if (!string.IsNullOrEmpty(houseName))
            {
                var house = await _houses.GetById(id);
                house.Name = houseName;
                if (await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить название" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/street")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseStreetAsync(string houseStreet, long id)
        {
            if (!string.IsNullOrEmpty(houseStreet))
            {
                var house = await _houses.GetById(id);
                house.Street = houseStreet;
                if (await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить улицу" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/info")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseInfoAsync(string houseInfo, long id)
        {
            if (!string.IsNullOrEmpty(houseInfo))
            {
                var house = await _houses.GetById(id);
                house.Info = houseInfo;
                if (await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить информацию" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/price")]
        [Authorize]
        public async Task<IActionResult> UpdateHousePriceAsync(double housePrice, long id)
        {
            if (housePrice != default)
            {
                var house = await _houses.GetById(id);
                house.Price = housePrice;
                if (await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/type")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseTypeAsync(string houseType, long id)
        {
            if (houseType != default)
            {
                var house = await _houses.GetById(id);
                house.Type = (HouseType)Enum.Parse(typeof(HouseType), houseType);
                if (await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }

        [HttpPost("/Housing/Houses/id={id}/update/image")]
        [Authorize]
        public async Task<IActionResult> UpdateHouseImageAsync(IFormFile houseFile, long id)
        {
            if(houseFile != null)
            {
                var house = await _houses.GetById(id);
                var newHouseImagePath = "/HouseImages/" + houseFile.FileName;
                System.IO.File.Delete(_environment.WebRootPath + house.ImagePath);
                using(var stream = new FileStream(_environment.WebRootPath + newHouseImagePath, FileMode.Create))
                {
                    await houseFile.CopyToAsync(stream);
                }
                house.ImagePath = newHouseImagePath;
                if(await _houses.Update(house)) return Redirect("/Housing/Houses/id=" + id);
            }
            return RedirectToAction("HousePage", "Housing", new { id, errorMessage = "Не удалось обновить стоимость" });
        }

        [HttpPost("/Housing/Houses/id={houseId}/ownerId={ownerId}/status={isSelling}")]
        [Authorize]
        public async Task<IActionResult> SetHouseStatus(long ownerId, long houseId, bool isSelling)
        {
            var house = await _houses.GetById(houseId);
            if(house.OwnerId == ownerId)
            {
                house.IsSelling = isSelling;
                if (await _houses.Update(house)) return RedirectToAction("ProfilePage", "HousingProfile");
            }
            return RedirectToAction("ProfilePage", "HousingProfile", new { statusError = "Не удалось сменить статус" });
        }

        [HttpPost("/Housing/Houses/id={id}/delete")]
        [Authorize]
        public async Task<IActionResult> DeleteHouseAsync(long id)
        {
            if (await _houses.DeleteById(id)) return RedirectToAction("ProfilePage", "HousingProfile");
            string deleteError = "Не удалось удалить недвижимость";
            return RedirectToAction("ProfilePage", "HousingProfile", new { deleteError });
        }
    }
}
