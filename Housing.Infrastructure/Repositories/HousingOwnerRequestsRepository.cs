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
    public class HousingOwnerRequestsRepository : IHousingRequestsRepository<HousingOwnerRequest, HousingOwnerRequestDto>
    {
        private readonly ModelContext _context;
        private readonly IMapper _mapper;
        public HousingOwnerRequestsRepository(ModelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> AddRequest(HousingOwnerRequest request)
        {
            _context.HousingOwnerRequests.Add(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRequest(HousingOwnerRequest request)
        {
            _context.HousingOwnerRequests.Remove(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<HousingOwnerRequest> GetByIds(long ownerId, long houseId)
        {
            return await _context.HousingOwnerRequests.FirstOrDefaultAsync(r => r.OwnerId == ownerId && r.HouseId == houseId);
        }

        public async Task<bool> HasRequest(HousingOwnerRequest request)
        {
            return await _context.HousingOwnerRequests.AnyAsync(r => r.HouseId == request.HouseId && r.OwnerId == request.OwnerId);
        }

        public async Task<ICollection<HousingOwnerRequestDto>> GetRequests(long houseId)
        {
            return await _context.HousingOwnerRequests.AsNoTracking().Where(r => r.HouseId == houseId).Include(r => r.Owner).
                ThenInclude(o => o.User).
                Select(r => _mapper.Map<HousingOwnerRequestDto>(r)).
                ToListAsync();
        }
    }
}
