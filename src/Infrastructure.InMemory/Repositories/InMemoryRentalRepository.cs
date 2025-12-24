using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория для сущностей Rental
/// </summary>
/// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
public class InMemoryRentalRepository : IRepository<Rental>
{
    private readonly List<Rental> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Инициализирует новый экземпляр in-memory репозитория аренд
    /// </summary>
    /// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
    public InMemoryRentalRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Rentals;
        _currentId = seeder.Rentals.Count;
    }

    /// <summary>
    /// Создает новую сущность аренды в памяти
    /// </summary>
    /// <param name="entity">Сущность аренды для создания</param>
    /// <returns>ID созданной аренды</returns>
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
    /// Получает все аренды из памяти
    /// </summary>
    /// <returns>Список всех аренд</returns>
    public async Task<List<Rental>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Получает аренду по ID из памяти
    /// </summary>
    /// <param name="id">ID аренды</param>
    /// <returns>Сущность аренды или null, если не найдена</returns>
    public async Task<Rental?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Обновляет существующую сущность аренды в памяти
    /// </summary>
    /// <param name="id">ID аренды</param>
    /// <param name="entity">Обновленные данные аренды</param>
    /// <returns>Обновленная сущность аренды или null, если не найдена</returns>
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
    /// Удаляет сущность аренды из памяти
    /// </summary>
    /// <param name="id">ID аренды</param>
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