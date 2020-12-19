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
            return await Context.Houses.AsNoTracking().Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
        }
        public override async Task<House> GetById(long id)
        {
           return await Context.Houses.AsNoTracking().
                Include(h => h.Owner).ThenInclude(o => o.User).
                FirstOrDefaultAsync(o => o.HouseId == id);
        }

        public async Task<ICollection<HouseDto>> GetFilteredHouses(FilteredHouseDto house)
        {
            bool hasPrice = house.Price != default, hasStreet = !string.IsNullOrEmpty(house.Street), 
                hasType = house.Type != Core.Enums.HouseType.Ничего;
            double bound = 5000000;
                if (hasPrice && hasStreet && hasType)
                    return await Context.Houses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.Type == house.Type).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if (hasPrice && hasType)
                    return await Context.Houses.Where(h => (h.Price >= house.Price - bound && h.Price <= house.Price + bound) &&
                    h.Type == house.Type).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if (hasStreet && hasType)
                    return await Context.Houses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    h.Type == house.Type).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if(hasPrice && hasStreet)
                    return await Context.Houses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound)).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if(hasPrice)
                    return await Context.Houses.Where(h => 
                    (h.Price >= house.Price - bound && h.Price <= house.Price + bound)).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if(hasStreet)
                    return await Context.Houses.Where(h => EF.Functions.Like(h.Street, house.Street)).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else if(hasType)
                    return await Context.Houses.Where(h => h.Type == house.Type).
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
                else
                    return await Context.Houses.
                    AsNoTracking().
                    //Include(h => h.Owner).
                    Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
        }

        public async Task<double> GetMaxHousePrice()
        {
            return await Context.Houses.MaxAsync(h => h.Price);
        }

        public async Task<double> GetMinHousePrice()
        {
            return await Context.Houses.MinAsync(h => h.Price);
        }
    }
}