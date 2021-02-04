using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Services
{
    public interface IHousingCartService : IModelService<CartHouse>
    {
        Task<CartHouse> GetFromCartByIds(long ownerId, long houseId);
    }
}
