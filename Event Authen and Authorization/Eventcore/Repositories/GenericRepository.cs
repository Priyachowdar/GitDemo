using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventcore.Models;
using Eventcore.Specifications;

namespace Eventcore.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly EventContext context;
        public GenericRepository(EventContext context)
        {
            this.context = context;
        }
        public T Add(T item)
        {
            return context.Add(item).Entity;
        }

        public T Update(T item)
        {
            return context.Update(item).Entity;
        }

        public T Remove(T item)
        {
            return context.Remove(item).Entity;
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            var Data = context.Set<T>().ToList();
            return Data.AsReadOnly();
        }

        public IReadOnlyCollection<T> GetBySpec(SpecificationBase<T> spec)
        {
            throw new NotImplementedException();
        }
    }
}
