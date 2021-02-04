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
    public class HousingResidentRequestsRepository : ModelRepository<HousingResidentRequest>, IHousingRequestsRepository<HousingResidentRequest>
    {
        private readonly HousingContext _context;
        public HousingResidentRequestsRepository(HousingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HousingResidentRequest> GetByIds(long userId, long houseId)
        {
            return await _context.HousingResidentRequests.FirstOrDefaultAsync(r => r.ResidentId == userId && r.HouseId == houseId);
        }

        public override async Task<bool> HasEntity(HousingResidentRequest request)
        {
            return await _context.HousingResidentRequests.AnyAsync(r => r.HouseId == request.HouseId && r.Resident.OwnerId == request.ResidentId);
        }

        public async Task<ICollection<HousingResidentRequest>> GetRequests(long houseId)
        {
            return await _context.HousingResidentRequests.
                Where(r => r.HouseId == houseId && r.IsApplied == false).
                OrderByDescending(r => r.SentAt).
                ToListAsync();
        }
    }
}
