namespace Domain.Repositories;

/// <summary>
/// Generic repository interface defining basic CRUD operations
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Creates a new entity
    /// </summary>
    /// <param name="entity">Entity to create</param>
    /// <returns>ID of the created entity</returns>
    public Task<int> CreateAsync(T entity);
    
    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    public Task<List<T>> ReadAsync();
    
    /// <summary>
    /// Retrieves entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity or null if not found</returns>
    public Task<T?> ReadAsync(int id);
    
    /// <summary>
    /// Updates an existing entity
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="entity">Updated entity data</param>
    /// <returns>Updated entity or null if not found</returns>
    public Task<T?> UpdateAsync(int id, T entity);
    
    /// <summary>
    /// Deletes an entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>True if deleted successfully, false if not found</returns>
    public Task<bool> DeleteAsync(int id);
}