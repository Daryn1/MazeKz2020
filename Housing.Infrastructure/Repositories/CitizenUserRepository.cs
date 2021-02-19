using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;

namespace Housing.Infrastructure.Repositories
{
    public class CitizenUserRepository : ModelRepository<CitizenUser>, ICitizenUserRepository
    {
        public CitizenUserRepository(HousingContext context) : base(context)
        {
        }

        public async Task<CitizenUser> GetByLogin(string login)
        {
            return await Context.Users.Select(u => new CitizenUser
            {
                Id = u.Id,
                Login = u.Login,
                Password = u.Password,
                Balance = u.Balance,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email
            }).FirstOrDefaultAsync(u => u.Login == login);
        }

        public override async Task<bool> HasEntity(CitizenUser model)
        {
            return await Context.Users.AnyAsync(user => user.Login == model.Login);
        }
    }
}
