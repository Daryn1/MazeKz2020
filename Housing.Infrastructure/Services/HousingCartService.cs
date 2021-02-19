using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class HousingCartService : ModelService<CartHouse>, IHousingCartService
    {
        private readonly IHousingCartsRepository _carts;
        public HousingCartService(IHousingCartsRepository carts) : base(carts)
        {
            _carts = carts;
        }

        public async Task<CartHouse> GetFromCartByIds(long ownerId, long houseId)
        {
            if (ownerId < 0 || houseId < 0) return null;
            return await _carts.GetFromCartByIds(ownerId, houseId);
        }
    }
}
