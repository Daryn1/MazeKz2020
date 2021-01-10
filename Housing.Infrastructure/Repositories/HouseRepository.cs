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
            return await Context.Houses.AsNoTracking().Where(h => h.IsSelling).Select(o => Mapper.Map<HouseDto>(o)).ToListAsync();
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
            var filteredHouses = Context.Houses.AsNoTracking().Select(h => Mapper.Map<HouseDto>(h));
            double bound = 5000000;
            if (hasPrice && hasStreet && hasType)
                filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.Type == house.Type && h.IsSelling);

            else if (hasPrice && hasType)
                filteredHouses = filteredHouses.Where(h => (h.Price >= house.Price - bound && h.Price <= house.Price + bound) &&
                h.Type == house.Type && h.IsSelling);

            else if (hasStreet && hasType)
                filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                h.Type == house.Type && h.IsSelling);

            else if (hasPrice && hasStreet)
                filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) &&
                (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.IsSelling);

            else if (hasPrice)
                filteredHouses = filteredHouses.Where(h =>
                (h.Price >= house.Price - bound && h.Price <= house.Price + bound) && h.IsSelling);

            else if (hasStreet)
                filteredHouses = filteredHouses.Where(h => EF.Functions.Like(h.Street, house.Street) && h.IsSelling);

            else if (hasType)
                filteredHouses = filteredHouses.Where(h => h.Type == house.Type && h.IsSelling);

            else
                filteredHouses = filteredHouses.Where(h => h.IsSelling);

            return await filteredHouses.ToListAsync();
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