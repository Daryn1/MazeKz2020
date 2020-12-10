using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Repositories
{
    public class HouseResidentRepository : ModelRepository<HouseResident, HouseResidentDto>, IHouseResidentRepository
    {
        public HouseResidentRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
