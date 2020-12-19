using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Repositories
{
    public class HousingOwnerRepository : ModelRepository<HousingOwner, HousingOwnerDto>, IHousingOwnerRepository
    {
        public HousingOwnerRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public override async Task<HousingOwner> Create(HousingOwner model)
        {
            model.Balance = new Random().Next(5000000, 10000000) * 10;
            Context.HouseOwners.Add(model);
            await Context.SaveChangesAsync();
            HousingResident user = new HousingResident { OwnerId = model.Id, HouseId = null };
            Context.HouseResidents.Add(user);
            await Context.SaveChangesAsync();
            return model;
        }
        public override async Task<HousingOwner> GetById(long id)
        {
            return await Context.HouseOwners.AsNoTracking().
                Include(o => o.User).
                Include(o => o.Houses).
                Include(o => o.HousingUser).
                FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<HousingOwner> GetByLogin(string login)
        {
           return await Context.HouseOwners.AsNoTracking().
                Include(o => o.User).
                FirstOrDefaultAsync(o => o.User.Login == login);
        }

        public override async Task<bool> HasEntity(HousingOwner model)
        {
            return await Context.HouseOwners.AnyAsync(o => o.UserId == model.UserId);
        }
    }
}
