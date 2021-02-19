using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace Housing.Infrastructure.Services
{
    public class HousingOwnerService : ModelService<HousingOwner>, IHousingOwnerService
    {
        private readonly IHousingOwnerRepository _owners;
        private readonly IHousingResidentRepository _residents;
        private readonly ICitizenUserRepository _users;

        public HousingOwnerService(IHousingOwnerRepository owners, IHousingResidentRepository residents,
            ICitizenUserRepository users) : base(owners)
        {
            _owners = owners;
            _residents = residents;
            _users = users;
        }
        public override async Task<HousingOwner> Create(HousingOwner model)
        {
            model = await base.Create(model);
            if(model != null)
            {
                HousingResident resident = new HousingResident { OwnerId = model.Id, HouseId = null };
                if (await _residents.Create(resident) != null) return model;
            }
            return null;
        }
        public async Task<HousingOwner> GetByLogin(string login)
        {
            if (string.IsNullOrEmpty(login)) return null;
            return await _owners.GetByLogin(login);
        }

        public async Task<CitizenUser> AuthentificatedUser(CitizenUserDto user)
        {
            string login = user.Login, password = user.Password;
            var userModel = await _users.GetByLogin(login);
            if(userModel != null)
            {
                if (userModel.Password == password) return userModel;
                throw new Exception("Неправильный пароль или логин. Попробуйте еще раз");
            }
            throw new Exception("Этого пользователя не существует. Пожалуйста, зарегистрируйтесь.");
        }
    }
}
