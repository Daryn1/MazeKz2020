using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Core.Interfaces.Services
{
    public interface IModelService<T>
    {
        Task<T> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
        Task<bool> HasEntity(T model);
        Task<bool> DeleteById(long id);
        Task<T> GetById(long id);
        Task<ICollection<T>> GetAll();
    }
}
