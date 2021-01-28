using Housing.Core.DTOs;
using Housing.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface ICartHouseRepository : IModelRepository<CartHouse>
    {
       // Task<bool> AddToCart(long ownerId, long houseId);
      //  Task<bool> DeleteFromCart(long ownerId, long houseId);
        Task<CartHouse> GetFromCartByIds(long ownerId, long houseId);
       // Task<bool> HasHouseInCart(long ownerId, long houseId);
    }
}
