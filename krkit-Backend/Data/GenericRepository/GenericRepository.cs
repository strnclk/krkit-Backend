using krkit_Backend.Data;
using krkit_Backend.Data.GenericRepository;
using krkit_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly KrKitDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(KrKitDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
    }
    // User Tablosuna Özel GetByUserNameAsync
    public async Task<T?> GetByNameAsync(string name)
    {
        if (typeof(T) == typeof(User)) // Eğer model User ise Username üzerinden ara
        {
            return await _dbSet.FirstOrDefaultAsync(e =>
                EF.Property<string>(e, "Username") == name &&
                EF.Property<bool>(e, "IsDeleted") == false) as T;
        }

        // Eğer model User değilse ve Name property’si varsa Name üzerinden ara
        var nameProperty = typeof(T).GetProperty("Name");
        var isDeletedProperty = typeof(T).GetProperty("IsDeleted");

        if (nameProperty != null && isDeletedProperty != null) // Eğer Name ve IsDeleted varsa sorguyu çalıştır
        {
            return await _dbSet.FirstOrDefaultAsync(e =>
                EF.Property<string>(e, "Name") == name &&
                EF.Property<bool>(e, "IsDeleted") == false) as T;
        }

        return null; // Eğer Name alanı yoksa null dön
    }

    public async Task<bool> IsExistByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id) != null;
    }
    public async Task<bool> IsExistByUserNameAsync(string userName)
    {

        return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName && !u.IsDeleted) != null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            entity.IsDeleted = true; // Soft delete
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
