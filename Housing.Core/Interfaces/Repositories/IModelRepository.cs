using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Repositories
{
    public interface IModelRepository<T, D> where T : class where D : class
    {
        Task<T> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
        Task<bool> HasEntity(T model);
        Task<bool> DeleteById(long id);
        Task<T> GetById(long id);
        Task<ICollection<D>> GetAll();
    }
}