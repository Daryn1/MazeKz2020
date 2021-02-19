using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IHousingRequestsRepository<T> : IModelRepository<T> where T : class
    {
        Task<ICollection<T>> GetRequests(long houseId);
        Task<T> GetByIds(long userId, long houseId);
    }
}
