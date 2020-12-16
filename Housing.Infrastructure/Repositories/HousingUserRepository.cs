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
    public class HousingUserRepository : ModelRepository<HousingUser, HousingUserDto>, IHousingUserRepository
    {
        public HousingUserRepository(ModelContext context, IMapper mapper) : base(context, mapper)
        {
        }

        /*public override async Task<HousingUserDto> GetById(long id)
        {
            var resident = await Context.HousingUsers.AsNoTracking().
                Include(r => r.House).Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
            return Mapper.Map<HousingUserDto>(resident);
        }

        public override async Task<ICollection<HousingUserDto>> GetAll()
        {
            var users = await Context.HousingUsers.AsNoTracking().Include(r => r.House).Include(r => r.User).Select(r => Mapper.Map<HousingUserDto>(r)).ToListAsync();
            return users;
        }*/
    }
}
