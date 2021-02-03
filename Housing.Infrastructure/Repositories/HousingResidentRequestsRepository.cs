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
        public override async Task<HousingResidentRequest> Create(HousingResidentRequest request)
        {
            var resident = _context.HouseResidents.Select(r => new HousingResident
            {
                Id = r.Id,
                OwnerId = r.OwnerId
            }).FirstOrDefault(r => r.OwnerId == request.ResidentId);
            request.ResidentId = resident.Id;
            _context.HousingResidentRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<HousingResidentRequest> GetByIds(long residentId, long houseId)
        {
            var resident = _context.HouseResidents.Select(r => new HousingResident
            {
                Id = r.Id,
                OwnerId = r.OwnerId
            }).FirstOrDefault(r => r.OwnerId == residentId);
            residentId = resident.Id;
            return await _context.HousingResidentRequests.FirstOrDefaultAsync(r => r.ResidentId == residentId && r.HouseId == houseId);
        }

        public override async Task<bool> HasEntity(HousingResidentRequest request)
        {
            return await _context.HousingResidentRequests.AnyAsync(r => r.HouseId == request.HouseId && r.Resident.OwnerId == request.ResidentId);
        }

        public async Task<ICollection<HousingResidentRequest>> GetRequests(long houseId)
        {
            return await _context.HousingResidentRequests.
                //AsNoTracking().
                Where(r => r.HouseId == houseId && r.IsApplied == false).
               // Include(r => r.Resident).
               // ThenInclude(r => r.Owner).ThenInclude(r => r.User).
                OrderByDescending(r => r.SentAt).
                ToListAsync();
        }

        public async Task<bool> ApplyRequest(long userId, long houseId)
        {
            var request = await _context.HousingResidentRequests.FirstOrDefaultAsync(r => r.ResidentId == userId && r.HouseId == houseId);
            request.IsApplied = true;
            var resident = await _context.HouseResidents.FindAsync(userId);
            resident.HouseId = houseId;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
