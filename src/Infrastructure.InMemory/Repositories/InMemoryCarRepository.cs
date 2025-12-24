using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория для сущностей Car
/// </summary>
/// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
public class InMemoryCarRepository : IRepository<Car>
{
    private readonly List<Car> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Инициализирует новый экземпляр in-memory репозитория автомобилей
    /// </summary>
    /// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
    public InMemoryCarRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Cars;
        _currentId = seeder.Cars.Count;
    }

    /// <summary>
    /// Создает новую сущность автомобиля в памяти
    /// </summary>
    /// <param name="entity">Сущность автомобиля для создания</param>
    /// <returns>ID созданного автомобиля</returns>
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
    /// Получает все автомобили из памяти
    /// </summary>
    /// <returns>Список всех автомобилей</returns>
    public async Task<List<Car>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Получает автомобиль по ID из памяти
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>Сущность автомобиля или null, если не найден</returns>
    public async Task<Car?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Обновляет существующую сущность автомобиля в памяти
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <param name="entity">Обновленные данные автомобиля</param>
    /// <returns>Обновленная сущность автомобиля или null, если не найден</returns>
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
    /// Удаляет сущность автомобиля из памяти
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>True, если успешно удален, false, если не найден</returns>
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