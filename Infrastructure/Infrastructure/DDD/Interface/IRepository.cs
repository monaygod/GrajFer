using System;
using System.Threading.Tasks;

namespace Infrastructure.DDD.Interface
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        T Add(T id);
    }
}