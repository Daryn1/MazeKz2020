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
    public class ModelRepository<T> : IModelRepository<T> where T : class
    {
        protected HousingContext Context;
        public ModelRepository(HousingContext context)
        {
            Context = context;
        }
        public virtual async Task<T> Create(T model)
        {
            Context.Set<T>().Add(model);
            await Context.SaveChangesAsync();
            return model;
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

        public virtual async Task<bool> DeleteById(long id)
        {
            var model = await GetById(id);
            Context.Set<T>().Remove(model);
            return await Context.SaveChangesAsync() > 0;
        }

        public virtual async Task<T> GetById(long id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public virtual async Task<ICollection<T>> GetAll()
        {
           return await Context.Set<T>().ToListAsync();
        }

        public virtual async Task<bool> HasEntity(T model)
        {
            return await Context.Set<T>().ContainsAsync(model);
        }
    }
}