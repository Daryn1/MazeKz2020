
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Housing.Controllers
{
    [Controller]
    public class HousingOwnersController : Controller
    {
        private readonly IHousingOwnerService _owners;
        public HousingOwnersController(IHousingOwnerService owners)
        {
            _owners = owners;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(CitizenUserDto user)
        {
            try
            {
                var userModel = await _owners.AuthentificatedUser(user);
                var owner = new HousingOwner { UserId = userModel.Id, Balance = (double)userModel.Balance };
                if (!await _owners.HasEntity(owner)) owner = await _owners.Create(owner);
                else owner = await _owners.GetByLogin(user.Login);
                var identity = AuthentificationOptions.CreateIdentity(owner);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                HttpContext.Session.SetString("Id", owner.Id.ToString());
                HttpContext.Session.SetString("Username", user.Login);
                HttpContext.Session.SetString("Role", "HousingOwner");
                return RedirectToAction("Houses", "Housing");
            } catch (Exception e)
            {
               return RedirectToAction("Houses", "Housing", new { errorMessage = e.Message });
            }
        }

        public async Task<IActionResult> SignoutUserAsync()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Houses", "Housing");
        }
    }
}
