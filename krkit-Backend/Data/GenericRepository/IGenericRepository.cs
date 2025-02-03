using System.Linq.Expressions;
using krkit_Backend.Data.Models;

namespace krkit_Backend.Data.GenericRepository
{


    public interface IGenericRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<T> GetByNameAsync(string userName);
        Task<bool> IsExistByIdAsync(int id);
        Task<bool> IsExistByUserNameAsync(string userName);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);  // IsDeleted will be set to true
    }

}
