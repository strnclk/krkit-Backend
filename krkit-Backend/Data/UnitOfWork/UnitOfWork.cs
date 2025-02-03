using krkit_Backend.Data.GenericRepository;
using krkit_Backend.Data.Models;

namespace krkit_Backend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KrKitDbContext _context;
        private bool _disposed = false;

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<ToDoList> ToDoLists { get; }

        public UnitOfWork(KrKitDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User>(_context);
            Products = new GenericRepository<Product>(_context);
            ToDoLists = new GenericRepository<ToDoList>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        // Bellek yönetimi için Dispose metodu eklenmeli
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
