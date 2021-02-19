using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class HousingResidentRequestService : ModelService<HousingResidentRequest>, IHousingRequestService<HousingResidentRequest>
    {
        private readonly IHousingRequestsRepository<HousingResidentRequest> _requests;
        private readonly IHousingResidentRepository _residents;
        private readonly IHousingOwnerRepository _owners;
        public HousingResidentRequestService(IHousingRequestsRepository<HousingResidentRequest> requests,
            IHousingResidentRepository residents, IHousingOwnerRepository owners) : base(requests)
        {
            _requests = requests;
            _residents = residents;
            _owners = owners;
        }

        public async override Task<HousingResidentRequest> Create(HousingResidentRequest model)
        {
            var ownerId = model.ResidentId;
            var owner = await _owners.GetById(ownerId);
            model.ResidentId = owner.HousingUser.Id;
            return await base.Create(model);
        }

        public async Task<bool> ApplyRequest(long userId, long houseId)
        {
            var request = await _requests.GetByIds(userId, houseId);
            request.IsApplied = true;
            var resident = await _residents.GetById(userId);
            resident.HouseId = houseId;
            return await _repos.Update(request);
        }

        public async Task<HousingResidentRequest> GetByIds(long userId, long houseId)
        {
            var owner = await _owners.GetById(userId);
            return await _requests.GetByIds(owner.HousingUser.Id, houseId);
        }

        public async Task<ICollection<HousingResidentRequest>> GetRequests(long houseId)
        {
            return await _requests.GetRequests(houseId);
        }
    }
}
