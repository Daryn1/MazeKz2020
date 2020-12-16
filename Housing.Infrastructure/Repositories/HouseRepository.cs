using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Housing.Infrastructure.Repositories
{
    public class HouseRepository : ModelRepository<House, HouseDto>, IHouseRepository
    {
        public HouseRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<ICollection<HouseDto>> GetAll()
        {
            return await Context.Houses.AsNoTracking().Include(h => h.Owner).Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
        }
        public override async Task<House> GetById(long id)
        {
           return await Context.Houses.AsNoTracking().Include(h => h.HousingUsers).Include(h => h.Owner).ThenInclude(o => o.User).
                FirstOrDefaultAsync(o => o.HouseId == id);
        }

        public Task<bool> UpdateName(House model, string name)
        {
            model.Name = name;
            return Update(model);
        }

        public Task<bool> UpdateStreet(House model, string street)
        {
            model.Street = street;
            return Update(model);
        }

        public Task<bool> UpdateInfo(House model, string info)
        {
            model.Info = info;
            return Update(model);
        }

        public Task<bool> UpdatePrice(House model, double price)
        {
            model.Price = price;
            return Update(model);
        }

        public Task<bool> UpdateStatus(House model, bool isBought)
        {
            model.IsBought = isBought;
            return Update(model);
        }
    }
}