using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория для сущностей ModelGeneration
/// </summary>
/// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
public class InMemoryModelGenerationRepository : IRepository<ModelGeneration>
{
    private readonly List<ModelGeneration> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Инициализирует новый экземпляр in-memory репозитория поколений моделей
    /// </summary>
    /// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
    public InMemoryModelGenerationRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.ModelGenerations;
        _currentId = seeder.ModelGenerations.Count;
    }

    /// <summary>
    /// Создает новую сущность поколения модели в памяти
    /// </summary>
    /// <param name="entity">Сущность поколения модели для создания</param>
    /// <returns>ID созданного поколения модели</returns>
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
    /// Получает все поколения моделей из памяти
    /// </summary>
    /// <returns>Список всех поколений моделей</returns>
    public async Task<List<ModelGeneration>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Получает поколение модели по ID из памяти
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <returns>Сущность поколения модели или null, если не найдена</returns>
    public async Task<ModelGeneration?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Обновляет существующую сущность поколения модели в памяти
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <param name="entity">Обновленные данные поколения модели</param>
    /// <returns>Обновленная сущность поколения модели или null, если не найдена</returns>
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
    /// Удаляет сущность поколения модели из памяти
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <returns>True, если успешно удалена, false, если не найдена</returns>
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