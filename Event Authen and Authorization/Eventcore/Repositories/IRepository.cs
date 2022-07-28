using Eventcore.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventcore.Specifications;

namespace Eventcore.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Add(T item);
        T Update(T item);
        T Remove(T item);
        IReadOnlyCollection<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<int> SaveAsync();
        IReadOnlyCollection<T> GetBySpec(SpecificationBase<T> spec);
    }
}
