using krkit_Backend.Data.GenericRepository;
using krkit_Backend.Data.Models;

namespace krkit_Backend.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Product> Products { get; }
        IGenericRepository<ToDoList> ToDoLists { get; }

        Task<int> SaveChangesAsync();
    }

}
