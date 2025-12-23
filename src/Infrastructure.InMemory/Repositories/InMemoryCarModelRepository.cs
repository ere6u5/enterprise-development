using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for CarModel entities
/// </summary>
/// <param name="seeder">Optional data seeder for initial population</param>
public class InMemoryCarModelRepository : IRepository<CarModel>
{
    private readonly List<CarModel> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Initializes a new instance of the in-memory car model repository
    /// </summary>
    /// <param name="seeder">Optional data seeder for initial population</param>
    public InMemoryCarModelRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.CarModels;
        _currentId = seeder.CarModels.Count;
    }

    /// <summary>
    /// Creates a new car model entity in memory
    /// </summary>
    /// <param name="entity">Car model entity to create</param>
    /// <returns>ID of the created car model</returns>
    public async Task<int> CreateAsync(CarModel entity)
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
    /// Retrieves all car models from memory
    /// </summary>
    /// <returns>List of all car models</returns>
    public async Task<List<CarModel>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Retrieves a car model by ID from memory
    /// </summary>
    /// <param name="id">Car model ID</param>
    /// <returns>Car model entity or null if not found</returns>
    public async Task<CarModel?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Updates an existing car model entity in memory
    /// </summary>
    /// <param name="id">Car model ID</param>
    /// <param name="entity">Updated car model data</param>
    /// <returns>Updated car model entity or null if not found</returns>
    public async Task<CarModel?> UpdateAsync(int id, CarModel entity)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return null;

            existingEntity.Name = entity.Name;
            existingEntity.DriveType = entity.DriveType;
            existingEntity.SeatCount = entity.SeatCount;
            existingEntity.BodyType = entity.BodyType;
            existingEntity.CarClass = entity.CarClass;

            return existingEntity;
        });
    }

    /// <summary>
    /// Deletes a car model entity from memory
    /// </summary>
    /// <param name="id">Car model ID</param>
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