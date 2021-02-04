using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Interfaces.Services
{
    public interface IHousingOwnerService : IModelService<HousingOwner>
    {
        Task<HousingOwner> GetByLogin(string login);
        Task<CitizenUser> AuthentificatedUser(CitizenUserDto user);
    }
}
