using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for Car entities
/// </summary>
/// <param name="seeder">Optional data seeder for initial population</param>
public class InMemoryCarRepository : IRepository<Car>
{
    private readonly List<Car> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Initializes a new instance of the in-memory car repository
    /// </summary>
    /// <param name="seeder">Optional data seeder for initial population</param>
    public InMemoryCarRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Cars;
        _currentId = seeder.Cars.Count;
    }

    /// <summary>
    /// Creates a new car entity in memory
    /// </summary>
    /// <param name="entity">Car entity to create</param>
    /// <returns>ID of the created car</returns>
    public async Task<int> CreateAsync(Car entity)
    {
        return await Task.Run(() =>
        {
            entity.Id = _currentId;
            _items.Add(entity);
            ++_currentId;
            return entity.Id;
        });
    }

    /// <summary>
    /// Retrieves all cars from memory
    /// </summary>
    /// <returns>List of all cars</returns>
    public async Task<List<Car>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Retrieves a car by ID from memory
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <returns>Car entity or null if not found</returns>
    public async Task<Car?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Updates an existing car entity in memory
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <param name="entity">Updated car data</param>
    /// <returns>Updated car entity or null if not found</returns>
    public async Task<Car?> UpdateAsync(int id, Car entity)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return null;

            existingEntity.ModelGenerationId = entity.ModelGenerationId;
            existingEntity.LicensePlate = entity.LicensePlate;
            existingEntity.Color = entity.Color;

            return existingEntity;
        });
    }

    /// <summary>
    /// Deletes a car entity from memory
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <returns>True if deleted successfully, false if not found</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return false;

            return _items.Remove(existingEntity);
        });
    }
}