namespace Person.Core.Application.Abstractions;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetByIdOrDefaultAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<int> InsertAsync(TEntity entity);
}
