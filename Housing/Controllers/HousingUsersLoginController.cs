using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebMaze.DbStuff.Model;

namespace Housing.Controllers
{
    [ApiController]
    [Route("housing/login")]
    public class HousingUsersLoginController : ControllerBase
    {
        private IHousingUserRepository _housingUsers;
        private IHousingOwnerRepository _owners;
        private ICitizenUserRepository _users;
        public HousingUsersLoginController(IHousingUserRepository userRepos, ICitizenUserRepository users, IHousingOwnerRepository owners)
        {
            _housingUsers = userRepos;
            _users = users;
            _owners = owners;
        }

        public async Task<IActionResult> LoginUser(CitizenUserDto user)
        {
            var userModel = await _users.GetByLogin(user.Login);
            if (userModel == null) {
                return NotFound("Этого пользователя не существует. Пожалуйста, зарегистрируйтесь.");
            }
            var housingUser = new HousingUser { UserId = userModel.Id, User = userModel };
            if(!await _housingUsers.HasEntity(housingUser))
            _housingUsers.Create(housingUser);
            var owner = new HousingOwner { UserId = userModel.Id, User = userModel };
            if (!await _owners.HasEntity(owner))
            _owners.Create(owner);
            return RedirectToAction("Houses", "Housing");
        }
    }
}
