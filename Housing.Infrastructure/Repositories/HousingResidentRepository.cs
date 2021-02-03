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
    public class HousingResidentRepository : ModelRepository<HousingResident>, IHousingResidentRepository
    {
        public HousingResidentRepository(HousingContext context) : base(context)
        {
        }

        public override async Task<HousingResident> GetById(long id)
        {
            return await Context.HouseResidents.
                //AsNoTracking().
               // Include(r => r.House).Include(r => r.Owner).
                FirstOrDefaultAsync(r => r.OwnerId == id);
        }

        public override async Task<ICollection<HousingResident>> GetAll()
        {
           return await Context.HouseResidents
                //.AsNoTracking().Include(r => r.House).Include(r => r.Owner)
                .ToListAsync();
        }
    }
}
