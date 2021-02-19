using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHousingCartsRepository : IModelRepository<CartHouse>
    {
        Task<CartHouse> GetFromCartByIds(long ownerId, long houseId);
    }
}
