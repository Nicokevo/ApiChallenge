using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsApi.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> SearchByEmailOrPhone(string searchTerm);
        Task<IEnumerable<T>> GetByLocation(string state, string city);
    }
}

