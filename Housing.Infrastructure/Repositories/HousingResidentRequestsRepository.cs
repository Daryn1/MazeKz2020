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
    public class HousingResidentRequestsRepository : IHousingRequestsRepository<HousingResidentRequest, HousingResidentRequestDto>
    {
        private readonly ModelContext _context;
        private IMapper _mapper;
        public HousingResidentRequestsRepository(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddRequest(HousingResidentRequest request)
        {
            var resident = _context.HouseResidents.Select(r => new HousingResident
            {
                Id = r.Id,
                OwnerId = r.OwnerId
            }).FirstOrDefault(r => r.OwnerId == request.ResidentId);
            request.ResidentId = resident.Id;
            _context.HousingResidentRequests.Add(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRequest(HousingResidentRequest request)
        {
            _context.HousingResidentRequests.Remove(request);
            return await _context.SaveChangesAsync() > 0;
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

        public async Task<bool> HasRequest(HousingResidentRequest request)
        {
            return await _context.HousingResidentRequests.AnyAsync(r => r.HouseId == request.HouseId && r.Resident.OwnerId == request.ResidentId);
        }

        public async Task<ICollection<HousingResidentRequestDto>> GetRequests(long houseId)
        {
            return await _context.HousingResidentRequests.AsNoTracking().Where(r => r.HouseId == houseId).Include(r => r.Resident).
                ThenInclude(r => r.Owner).ThenInclude(r => r.User).
                Select(r => _mapper.Map<HousingResidentRequestDto>(r)).
                ToListAsync();
        }
    }
}
