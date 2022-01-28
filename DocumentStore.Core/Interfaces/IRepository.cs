using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentStore.Core.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetById(string id);
        Task<List<T>> List();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<bool> RecordExists(string id);
    }
}