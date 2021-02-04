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
    public class HousingOwnerRequestsRepository : ModelRepository<HousingOwnerRequest>, IHousingRequestsRepository<HousingOwnerRequest>
    {
        private readonly HousingContext _context;
        public HousingOwnerRequestsRepository(HousingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HousingOwnerRequest> GetByIds(long userId, long houseId)
        {
            return await _context.HousingOwnerRequests.FirstOrDefaultAsync(r => r.OwnerId == userId && r.HouseId == houseId);
        }

        public override async Task<bool> HasEntity(HousingOwnerRequest request)
        {
            return await _context.HousingOwnerRequests.AnyAsync(r => r.HouseId == request.HouseId && r.OwnerId == request.OwnerId);
        }

        public async Task<ICollection<HousingOwnerRequest>> GetRequests(long houseId)
        {
            return await _context.HousingOwnerRequests.
                Where(r => r.HouseId == houseId && r.IsApplied == false).
                OrderByDescending(r => r.SentAt).
                ToListAsync();
        }
    }
}
