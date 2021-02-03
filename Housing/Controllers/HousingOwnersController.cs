
using System.Security.Claims;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
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
        private readonly IHousingOwnerRepository _owners;
        private readonly ICitizenUserRepository _users;
        public HousingOwnersController(ICitizenUserRepository users, IHousingOwnerRepository owners)
        {
            _users = users;
            _owners = owners;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(CitizenUserDto user)
        {
            var userModel = await _users.GetByLogin(user.Login);
            if (userModel == null) {
                return RedirectToAction("Houses", "Housing", new { errorMessage = "Этого пользователя не существует. Пожалуйста, зарегистрируйтесь." });
            }
            if (user.Password != userModel.Password) {
                return RedirectToAction("Houses", "Housing", new { errorMessage = "Неправильный пароль или логин. Попробуйте еще раз" });
            }
            var owner = new HousingOwner { UserId = userModel.Id, User = userModel, Balance = (double) userModel.Balance };
            if (!await _owners.HasEntity(owner)) owner = await _owners.Create(owner);
            else owner = await _owners.GetByLogin(user.Login);
            var identity = AuthentificationOptions.CreateIdentity(owner);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            HttpContext.Session.SetString("Id", owner.Id.ToString());
            HttpContext.Session.SetString("Username", user.Login);
            HttpContext.Session.SetString("Role", "Owner");
            return RedirectToAction("Houses", "Housing");
        }

        public async Task<IActionResult> SignoutUserAsync()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Houses", "Housing");
        }
    }
}
