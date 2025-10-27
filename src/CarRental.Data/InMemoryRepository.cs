using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Базовый класс репозитория с хранением данных в памяти
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class InMemoryRepository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// Коллекция сущностей в памяти
    /// </summary>
    protected readonly List<T> _entities = new();

    /// <summary>
    /// Счетчик для генерации идентификаторов
    /// </summary>
    protected int _nextId = 1;

    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <returns>Коллекция всех сущностей</returns>
    public virtual IEnumerable<T> GetAll() => _entities;

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    public virtual T? GetById(int id)
    {
        var property = typeof(T).GetProperty("Id");
        return _entities.FirstOrDefault(e => (int)property!.GetValue(e)! == id);
    }

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    public virtual void Add(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null && idProperty.PropertyType == typeof(int))
        {
            idProperty.SetValue(entity, _nextId++);
        }
        _entities.Add(entity);
    }

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    public virtual void Update(T entity)
    {
        var idProperty = typeof(T).GetProperty("Id");
        if (idProperty != null)
        {
            var id = (int)idProperty.GetValue(entity)!;
            var existing = GetById(id);
            if (existing != null)
            {
                _entities.Remove(existing);
                _entities.Add(entity);
            }
        }
    }

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    public virtual void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _entities.Remove(entity);
        }
    }
}