using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class HousingCartsRepository : ModelRepository<CartHouse>, IHousingCartsRepository
    {
        private readonly HousingContext _context;
        public HousingCartsRepository(HousingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartHouse> GetFromCartByIds(long ownerId, long houseId)
        {
            return await _context.HouseCarts.
                //Include(c => c.House).
                FirstOrDefaultAsync(c => c.OwnerId == ownerId
            && c.HouseId == houseId);
        }
        public override async Task<bool> HasEntity(CartHouse model)
        {
            return await _context.HouseCarts.AnyAsync(c => c.OwnerId == model.OwnerId && c.HouseId == model.HouseId);
        }
    }
}
