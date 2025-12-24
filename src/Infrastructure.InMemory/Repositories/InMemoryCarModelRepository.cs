using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория для сущностей CarModel
/// </summary>
/// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
public class InMemoryCarModelRepository : IRepository<CarModel>
{
    private readonly List<CarModel> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Инициализирует новый экземпляр in-memory репозитория моделей автомобилей
    /// </summary>
    /// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
    public InMemoryCarModelRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.CarModels;
        _currentId = seeder.CarModels.Count;
    }

    /// <summary>
    /// Создает новую сущность модели автомобиля в памяти
    /// </summary>
    /// <param name="entity">Сущность модели автомобиля для создания</param>
    /// <returns>ID созданной модели автомобиля</returns>
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
    /// Получает все модели автомобилей из памяти
    /// </summary>
    /// <returns>Список всех моделей автомобилей</returns>
    public async Task<List<CarModel>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Получает модель автомобиля по ID из памяти
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
    /// <returns>Сущность модели автомобиля или null, если не найдена</returns>
    public async Task<CarModel?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Обновляет существующую сущность модели автомобиля в памяти
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
    /// <param name="entity">Обновленные данные модели автомобиля</param>
    /// <returns>Обновленная сущность модели автомобиля или null, если не найдена</returns>
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
    /// Удаляет сущность модели автомобиля из памяти
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
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