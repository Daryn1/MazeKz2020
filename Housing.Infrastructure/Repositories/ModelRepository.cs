using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Housing.Core.Interfaces.Repositories;
using Housing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Housing.Infrastructure.Repositories
{
    public class ModelRepository<T,D> : IModelRepository<T, D> where T : class where D : class
    {
        protected ModelContext Context;
        protected IMapper Mapper;
        public ModelRepository(ModelContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public virtual async Task<bool> Create(T model)
        {
            await Context.Set<T>().AddAsync(model);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(T model)
        {
            Context.Set<T>().Update(model);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Delete(T model)
        {
            Context.Set<T>().Remove(model);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteById(Guid id)
        {
            Context.Set<T>().Remove(await Context.Set<T>().FindAsync(id));
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<D> GetById(Guid id)
        {
            var model = await Context.Set<T>().FindAsync(id);
            return Mapper.Map<D>(model);
        }

        public virtual async Task<ICollection<D>> GetAll()
        {
           return await Context.Set<T>().AsNoTracking().Select(m => Mapper.Map<D>(m)).ToListAsync();
        }
    }
}