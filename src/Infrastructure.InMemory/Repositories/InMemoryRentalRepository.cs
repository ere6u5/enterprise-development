using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for Rental entities
/// </summary>
/// <param name="seeder">Optional data seeder for initial population</param>
public class InMemoryRentalRepository : IRepository<Rental>
{
    private readonly List<Rental> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Initializes a new instance of the in-memory rental repository
    /// </summary>
    /// <param name="seeder">Optional data seeder for initial population</param>
    public InMemoryRentalRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Rentals;
        _currentId = seeder.Rentals.Count;
    }

    /// <summary>
    /// Creates a new rental entity in memory
    /// </summary>
    /// <param name="entity">Rental entity to create</param>
    /// <returns>ID of the created rental</returns>
    public async Task<int> CreateAsync(Rental entity)
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
    /// Retrieves all rentals from memory
    /// </summary>
    /// <returns>List of all rentals</returns>
    public async Task<List<Rental>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Retrieves a rental by ID from memory
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <returns>Rental entity or null if not found</returns>
    public async Task<Rental?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Updates an existing rental entity in memory
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <param name="entity">Updated rental data</param>
    /// <returns>Updated rental entity or null if not found</returns>
    public async Task<Rental?> UpdateAsync(int id, Rental entity)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return null;

            existingEntity.CarId = entity.CarId;
            existingEntity.Car = entity.Car;
            existingEntity.ClientId = entity.ClientId;
            existingEntity.Client = entity.Client;
            existingEntity.RentalStart = entity.RentalStart;
            existingEntity.RentalHours = entity.RentalHours;

            return existingEntity;
        });
    }

    /// <summary>
    /// Deletes a rental entity from memory
    /// </summary>
    /// <param name="id">Rental ID</param>
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