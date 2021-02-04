using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class HousingOwnerRequestService : ModelService<HousingOwnerRequest>, IHousingRequestService<HousingOwnerRequest>
    {
        private readonly IHousingRequestsRepository<HousingOwnerRequest> _requests;
        private readonly IHouseRepository _houses;
        private readonly IHousingOwnerRepository _owners;
        public HousingOwnerRequestService(IHousingRequestsRepository<HousingOwnerRequest> requests, IHouseRepository houses,
            IHousingOwnerRepository owners) : base(requests)
        {
            _requests = requests;
            _houses = houses;
            _owners = owners;
        }

        public async Task<bool> ApplyRequest(long userId, long houseId)
        {
            var house = await _houses.GetById(houseId);
            var user = await _owners.GetById(userId);
            if (user.Balance < house.Price) return false;
            user.Balance -= house.Price;
            house.OwnerId = userId;
            house.IsSelling = false;
            var request = await GetByIds(userId, houseId);
            request.IsApplied = true;
            return await _repos.Update(request);
        }

        public async Task<HousingOwnerRequest> GetByIds(long userId, long houseId)
        {
            return await _requests.GetByIds(userId, houseId);
        }

        public async Task<ICollection<HousingOwnerRequest>> GetRequests(long houseId)
        {
            return await _requests.GetRequests(houseId);
        }
    }
}
