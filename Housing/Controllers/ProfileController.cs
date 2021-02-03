using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Housing.Controllers
{
    [Route("{controller}/")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IHousingOwnerRepository _owners;
        private readonly IMapper _mapper;
        public ProfileController(IHousingOwnerRepository owners, IMapper mapper)
        {
            _owners = owners;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> ProfilePageAsync(string error, string deleteError, string balanceError, string requestDeleteError)
        {
            ViewBag.ErrorMessage = error;
            ViewBag.DeleteHouseErrorMessage = deleteError;
            ViewBag.BalanceErrorMessage = balanceError;
            ViewBag.RequestDeleteError = requestDeleteError;
            var owner = await _owners.GetById(long.Parse(HttpContext.Session.GetString("Id")));
            return View(_mapper.Map<HousingOwnerDto>(owner));
        }

        [HttpPost("id={id}/update/balance")]
        public async Task<IActionResult> UpdateOwnerBalance(double balance, long id)
        {
            if(balance <= 0)
                return RedirectToAction("ProfilePage", new { balanceError = "Баланс должен быть больше нуля" });
            var owner = await _owners.GetById(id);
            owner.Balance += balance;
            if (await _owners.Update(owner)) return RedirectToAction("ProfilePage");
            return RedirectToAction("ProfilePage", new { balanceError = "Не удалось пополнить баланс" });
        }
    }
}
