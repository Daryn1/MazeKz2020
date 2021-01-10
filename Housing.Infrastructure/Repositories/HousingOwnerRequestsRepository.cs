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

        public async Task<HousingOwnerRequest> GetByIds(long userId, long houseId)
        {
            return await _context.HousingOwnerRequests.FirstOrDefaultAsync(r => r.OwnerId == userId && r.HouseId == houseId);
        }

        public async Task<bool> HasRequest(HousingOwnerRequest request)
        {
            return await _context.HousingOwnerRequests.AnyAsync(r => r.HouseId == request.HouseId && r.OwnerId == request.OwnerId);
        }

        public async Task<ICollection<HousingOwnerRequestDto>> GetRequests(long houseId)
        {
            return await _context.HousingOwnerRequests.AsNoTracking().Where(r => r.HouseId == houseId && r.IsApplied == false).Include(r => r.Owner).
                ThenInclude(o => o.User).
                Select(r => _mapper.Map<HousingOwnerRequestDto>(r)).
                ToListAsync();
        }

        public async Task<bool> ApplyRequest(long userId, long houseId)
        {
            var house = await _context.Houses.FindAsync(houseId);
            var user = await _context.HouseOwners.FindAsync(userId);
            if (user.Balance < house.Price) return false;
            user.Balance -= house.Price;
            house.OwnerId = userId;
            var request = await GetByIds(userId, houseId);
            request.IsApplied = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
