using Domain.Entities;
using Domain.Repositories;
using Domain.Seeder;

namespace Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory реализация репозитория для сущностей Client
/// </summary>
/// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
public class InMemoryClientRepository : IRepository<Client>
{
    private readonly List<Client> _items = [];
    private int _currentId = 1;

    /// <summary>
    /// Инициализирует новый экземпляр in-memory репозитория клиентов
    /// </summary>
    /// <param name="seeder">Опциональный генератор данных для начального заполнения</param>
    public InMemoryClientRepository(DataSeeder? seeder)
    {
        if (seeder == null) return;

        _items = seeder.Clients;
        _currentId = seeder.Clients.Count;
    }

    /// <summary>
    /// Создает новую сущность клиента в памяти
    /// </summary>
    /// <param name="entity">Сущность клиента для создания</param>
    /// <returns>ID созданного клиента</returns>
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
    /// Получает всех клиентов из памяти
    /// </summary>
    /// <returns>Список всех клиентов</returns>
    public async Task<List<Client>> ReadAsync()
    {
        return await Task.Run(() => _items);
    }

    /// <summary>
    /// Получает клиента по ID из памяти
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Сущность клиента или null, если не найден</returns>
    public async Task<Client?> ReadAsync(int id)
    {
        return await Task.Run(() => _items.FirstOrDefault(item => item.Id == id));
    }

    /// <summary>
    /// Обновляет существующую сущность клиента в памяти
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="entity">Обновленные данные клиента</param>
    /// <returns>Обновленная сущность клиента или null, если не найден</returns>
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
    /// Удаляет сущность клиента из памяти
    /// </summary>
    /// <param name="id">ID клиента</param>
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