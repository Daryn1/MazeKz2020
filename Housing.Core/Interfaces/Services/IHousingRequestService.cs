using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Services
{
    public interface IHousingRequestService<T> : IModelService<T>
    {
        Task<ICollection<T>> GetRequests(long houseId);
        Task<bool> ApplyRequest(long userId, long houseId);
        Task<T> GetByIds(long userId, long houseId);
    }
}
