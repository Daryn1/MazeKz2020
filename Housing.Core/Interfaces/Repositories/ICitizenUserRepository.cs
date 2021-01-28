using Housing.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace Housing.Core.Interfaces.Repositories
{
    public interface ICitizenUserRepository : IModelRepository<CitizenUser>
    {
        Task<CitizenUser> GetByLogin(string login);
    }
}
