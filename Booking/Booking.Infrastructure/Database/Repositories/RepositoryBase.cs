using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Database.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Database.Repositories;

public class RepositoryBase<TKey, TEntity, TContext>(TContext dbContext)
    : IRepository<TKey, TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
{
    public async Task<List<TEntity>> GetAll()
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetById(TKey id)
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id!.Equals(id));
    }
    
    // Метод для отримання списку екземплярів TEntity, які відповідають заданій специфікації
    public Task<List<TEntity>> GetBySpecification(Specification<TEntity> spec)
    {
        return dbContext.Set<TEntity>()
            .AsNoTracking()
            .Where(spec.ToExpression())
            .ToListAsync();
    }
    
// Метод для перевірки наявності екземпляра TEntity, який відповідає заданій специфікації
    public Task<bool> ExistsBySpecification(Specification<TEntity> spec)
    {
        return dbContext.Set<TEntity>()
            .AnyAsync(spec.ToExpression());
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(TKey id, TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(TKey id)
    {
        var entity = await GetById(id);
        if (entity is null)
        {
            return;
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}