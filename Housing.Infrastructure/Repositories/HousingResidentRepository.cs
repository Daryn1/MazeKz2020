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

namespace Housing.Infrastructure.Repositories
{
    public class HousingResidentRepository : ModelRepository<HousingResident, HousingResidentDto>, IHousingResidentRepository
    {
        public HousingResidentRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<HousingResident> GetById(long id)
        {
            return await Context.HouseResidents.AsNoTracking().
                Include(r => r.House).Include(r => r.Owner).FirstOrDefaultAsync(r => r.OwnerId == id);
        }

        public override async Task<ICollection<HousingResidentDto>> GetAll()
        {
           return await Context.HouseResidents.AsNoTracking().Include(r => r.House).Include(r => r.Owner).
                Select(r => Mapper.Map<HousingResidentDto>(r)).ToListAsync();
        }
    }
}
