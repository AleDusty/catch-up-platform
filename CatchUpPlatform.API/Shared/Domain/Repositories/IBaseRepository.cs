namespace CatchUpPlatform.API.Shared.Domain.Repositories;


/// <summary>
/// Base repository interface for all repositories
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IBaseRepository<TEntity>
{
    // Command (Modify data)
    
    /// <summary>
    /// Add entity to the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> A task representing the asynchronous operation</returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken=default);
    
    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"> The entity object to update</param>
    
    void Update(TEntity entity);
    
    /// <summary>
    /// Remove entity
    /// </summary>
    /// <param name="entity">The entity object to remove</param>
    
    void Remove(TEntity entity);
    
    //Queries (Get Data)
    
    /// <summary>
    /// Find Entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"> Token to cancel  to asynchronous operation</param>
    /// <returns>task representing the asynchronous read operation, containing the entity if found, or null otherwise</returns>
    
    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken=default);
    
    
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <param name="cancellationToken">Token to cancel to asynchronous operation</param>
    /// <returns>An enumerable containing all entities objects</returns>
    Task<IEnumerable<TEntity>> ListAllAsync(CancellationToken cancellationToken=default);
}