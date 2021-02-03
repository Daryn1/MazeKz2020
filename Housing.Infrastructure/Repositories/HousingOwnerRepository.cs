﻿using AutoMapper;
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

namespace Housing.Infrastructure.Repositories
{
    public class HousingOwnerRepository : ModelRepository<HousingOwner>, IHousingOwnerRepository
    {
        public HousingOwnerRepository(HousingContext context) : base(context)
        {
        }
        public override async Task<HousingOwner> Create(HousingOwner model)
        {
            Context.HouseOwners.Add(model);
            if (await Context.SaveChangesAsync() > 0)
            {
                HousingResident user = new HousingResident { OwnerId = model.Id, HouseId = null };
                Context.HouseResidents.Add(user);
                await Context.SaveChangesAsync();
            }
            return model;
        }
        public override async Task<HousingOwner> GetById(long id)
        {
            return await Context.HouseOwners.
                //AsNoTracking().
                //Include(o => o.User).
                //Include(o => o.Houses).
               // Include(o => o.HousingUser).ThenInclude(u => u.ResidentRequests).ThenInclude(r => r.House).
               // Include(o => o.CartHouses).ThenInclude(c => c.House).
                /*Include(o => o.OwnerRequests).ThenInclude(r => r.House).Select(o =>
                {
                    o.User = new WebMaze.DbStuff.Model.CitizenUser
                    {
                        Login = o.User.Login,
                        Password = o.User.Password,
                        Balance = o.User.Balance,
                        AvatarUrl = o.User.AvatarUrl
                    };
                    return o;
                }).*/
                FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<HousingOwner> GetByLogin(string login)
        {
           return await Context.HouseOwners.
               // AsNoTracking().
               // Include(o => o.User).
                FirstOrDefaultAsync(o => o.User.Login.Equals(login));
        }

        public override async Task<bool> HasEntity(HousingOwner model)
        {
            return await Context.HouseOwners.AnyAsync(o => o.UserId == model.UserId);
        }
    }
}