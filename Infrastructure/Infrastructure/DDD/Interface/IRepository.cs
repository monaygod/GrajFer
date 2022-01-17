using System.Threading.Tasks;

namespace Infrastructure.DDD.Interface
{
    public interface IRepository<T> where T: IAggregateRoot
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T Add(T id);
        
    }
}