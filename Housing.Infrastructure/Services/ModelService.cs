using Housing.Core.Interfaces.Repositories;
using Housing.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Housing.Infrastructure.Services
{
    public class ModelService<T> : IModelService<T> where T : class
    {
        protected IModelRepository<T> _repos;

        public ModelService(IModelRepository<T> repos)
        {
            _repos = repos;
        }

        public virtual async Task<T> Create(T model)
        {
            return await _repos.Create(model);
        }

        public virtual async Task<bool> Delete(T model)
        {
            return await _repos.Delete(model);
        }

        public virtual async Task<bool> DeleteById(long id)
        {
            return await _repos.DeleteById(id);
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
            return await _repos.GetAll();
        }

        public virtual async Task<T> GetById(long id)
        {
            return await _repos.GetById(id);
        }

        public virtual async Task<bool> HasEntity(T model)
        {
            return await _repos.HasEntity(model);
        }

        public virtual async Task<bool> Update(T model)
        {
            return await _repos.Update(model);
        }
    }
}
