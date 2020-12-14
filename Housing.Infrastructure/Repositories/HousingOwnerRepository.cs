using AutoMapper;
using Housing.Core.DTOs;
using Housing.Core.Interfaces.Repositories;
using Housing.Core.Models;
using Housing.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Housing.Infrastructure.Repositories
{
    public class HousingOwnerRepository : ModelRepository<HousingOwner, HousingOwnerDto>, IHousingOwnerRepository
    {
        public HousingOwnerRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
