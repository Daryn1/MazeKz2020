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

        public async Task<HousingResident> GetByOwnerId(long ownerId)
        {
            return await Context.HouseResidents.FirstOrDefaultAsync(r => r.OwnerId == ownerId);
        }
    }
}
