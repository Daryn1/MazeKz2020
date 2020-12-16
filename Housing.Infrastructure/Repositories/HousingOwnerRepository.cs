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
        public override async Task<HousingOwner> GetById(long id)
        {
            return await Context.HouseOwners.AsNoTracking().Include(o => o.User).Include(o => o.Houses).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<HousingOwner> GetByLogin(string login)
        {
            var owner = await Context.HouseOwners.AsNoTracking().Include(o => o.User).Include(o => o.Houses).FirstOrDefaultAsync(o => o.User.Login == login);
            return owner;
        }

        public override async Task<bool> HasEntity(HousingOwner model)
        {
            return await Context.HouseOwners.AnyAsync(o => o.UserId == model.UserId);
        }
    }
}
