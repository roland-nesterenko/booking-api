using Booking.Infrastructure.Database.Specifications;

namespace Booking.Infrastructure.Database.Abstractions;

public interface IRepository<in TKey, TEntity>
{
    Task<List<TEntity>> GetAll();
    Task<TEntity?> GetById(TKey id);
    Task<List<TEntity>> GetBySpecification(Specification<TEntity> spec);
    Task<bool> ExistsBySpecification(Specification<TEntity> spec);
    Task<TEntity> Create(TEntity entity);
    Task Update(TKey id, TEntity entity);
    Task Delete(TKey id);
}