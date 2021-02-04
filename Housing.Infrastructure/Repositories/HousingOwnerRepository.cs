using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
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
    public class HousingOwnerRepository : ModelRepository<HousingOwner>, IHousingOwnerRepository
    {
        public HousingOwnerRepository(HousingContext context) : base(context)
        {
        }
        public override async Task<HousingOwner> Create(HousingOwner model)
        {
            model = await base.Create(model);
            //???? Lazy loading not works 
            model.User = await Context.Users.Select(u => new CitizenUser 
            { Login = u.Login, Id = u.Id, AvatarUrl = u.AvatarUrl, PhoneNumber = u.PhoneNumber }).FirstAsync(u => u.Id == model.UserId);
            return model;
        }
        public override async Task<HousingOwner> GetById(long id)
        {
            var model = await base.GetById(id);
            model.HousingUser = await Context.HouseResidents.FirstAsync(u => u.OwnerId == id);
            return model;
        }

        public async Task<HousingOwner> GetByLogin(string login)
        {
           return await Context.HouseOwners.
                FirstOrDefaultAsync(o => o.User.Login.Equals(login));
        }

        public override async Task<bool> HasEntity(HousingOwner model)
        {
            return await Context.HouseOwners.AnyAsync(o => o.UserId == model.UserId);
        }
    }
}
