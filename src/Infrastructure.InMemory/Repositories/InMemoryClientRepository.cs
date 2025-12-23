using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for Client entities
/// </summary>
/// <param name="seeder">Optional data seeder for initial population</param>
public class InMemoryClientRepository : IRepository<Client>
{
    private readonly List<Client> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Initializes a new instance of the in-memory client repository
    /// </summary>
    /// <param name="seeder">Optional data seeder for initial population</param>
    public InMemoryClientRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Clients;
        _currentId = seeder.Clients.Count;
    }

    /// <summary>
    /// Creates a new client entity in memory
    /// </summary>
    /// <param name="entity">Client entity to create</param>
    /// <returns>ID of the created client</returns>
    public async Task<int> CreateAsync(Client entity)
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
    /// Retrieves all clients from memory
    /// </summary>
    /// <returns>List of all clients</returns>
    public async Task<List<Client>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Retrieves a client by ID from memory
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <returns>Client entity or null if not found</returns>
    public async Task<Client?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Updates an existing client entity in memory
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <param name="entity">Updated client data</param>
    /// <returns>Updated client entity or null if not found</returns>
    public async Task<Client?> UpdateAsync(int id, Client entity)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return null;

            existingEntity.DriverLicenseNumber = entity.DriverLicenseNumber;
            existingEntity.FullName = entity.FullName;
            existingEntity.BirthDate = entity.BirthDate;

            return existingEntity;
        });
    }

    /// <summary>
    /// Deletes a client entity from memory
    /// </summary>
    /// <param name="id">Client ID</param>
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