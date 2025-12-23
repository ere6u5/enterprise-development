using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory repository implementation for ModelGeneration entities
/// </summary>
/// <param name="seeder">Optional data seeder for initial population</param>
public class InMemoryModelGenerationRepository : IRepository<ModelGeneration>
{
    private readonly List<ModelGeneration> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Initializes a new instance of the in-memory model generation repository
    /// </summary>
    /// <param name="seeder">Optional data seeder for initial population</param>
    public InMemoryModelGenerationRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.ModelGenerations;
        _currentId = seeder.ModelGenerations.Count;
    }

    /// <summary>
    /// Creates a new model generation entity in memory
    /// </summary>
    /// <param name="entity">Model generation entity to create</param>
    /// <returns>ID of the created model generation</returns>
    public async Task<int> CreateAsync(ModelGeneration entity)
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
    /// Retrieves all model generations from memory
    /// </summary>
    /// <returns>List of all model generations</returns>
    public async Task<List<ModelGeneration>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Retrieves a model generation by ID from memory
    /// </summary>
    /// <param name="id">Model generation ID</param>
    /// <returns>Model generation entity or null if not found</returns>
    public async Task<ModelGeneration?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Updates an existing model generation entity in memory
    /// </summary>
    /// <param name="id">Model generation ID</param>
    /// <param name="entity">Updated model generation data</param>
    /// <returns>Updated model generation entity or null if not found</returns>
    public async Task<ModelGeneration?> UpdateAsync(int id, ModelGeneration entity)
    {
        return await Task.Run(() =>
        {
            var existingEntity = _items.FirstOrDefault(item => item.Id == id);
            if (existingEntity == null) return null;

            existingEntity.Year = entity.Year;
            existingEntity.EngineVolume = entity.EngineVolume;
            existingEntity.TransmissionType = entity.TransmissionType;
            existingEntity.ModelId = entity.ModelId;
            existingEntity.RentalCostPerHour = entity.RentalCostPerHour;

            return existingEntity;
        });
    }

    /// <summary>
    /// Deletes a model generation entity from memory
    /// </summary>
    /// <param name="id">Model generation ID</param>
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