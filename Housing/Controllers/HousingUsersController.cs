using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMaze.DbStuff.Model;

namespace Housing.Controllers
{
    [Controller]
    public class HousingOwnersController : Controller
    {
        private IHousingOwnerRepository _owners;
        private ICitizenUserRepository _users;
        public HousingOwnersController(ICitizenUserRepository users, IHousingOwnerRepository owners)
        {
            _users = users;
            _owners = owners;
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(CitizenUserDto user)
        {
            var userModel = await _users.GetByLogin(user.Login);
            if (userModel == null) {
                return RedirectToAction("Houses", "Housing", new { errorMessage = "Этого пользователя не существует. Пожалуйста, зарегистрируйтесь." });
            }
            if (user.Password != userModel.Password) {
                return RedirectToAction("Houses", "Housing", new { errorMessage = "Неправильный пароль или логин. Попробуйте еще раз" });
            }
            var owner = new HousingOwner { UserId = userModel.Id, User = userModel };
            if (!await _owners.HasEntity(owner)) owner = await _owners.Create(owner);
            else owner = await _owners.GetByLogin(user.Login);
            HttpContext.Session.SetString("Id", owner.Id.ToString());
            HttpContext.Session.SetString("Username", owner.User.Login);
            HttpContext.Session.SetString("Role", "Owner");
            return RedirectToAction("Houses", "Housing");
        }

        [HttpPost("signout")]
        public IActionResult SignoutUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Houses", "Housing");
        }
    }
}
