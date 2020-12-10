using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IModelRepository<T, D> where T : class where D : class
    {
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
        Task<bool> DeleteById(Guid id);
        Task<D> GetById(Guid id);
        Task<ICollection<D>> GetAll();
    }
}