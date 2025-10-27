using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Generic интерфейс репозитория для работы с сущностями
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Найденная сущность или null</returns>
    T? GetById(int id);

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    /// <param name="entity">Добавляемая сущность</param>
    void Add(T entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    /// <param name="entity">Обновляемая сущность</param>
    void Update(T entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности</param>
    void Delete(int id);
}

/// <summary>
/// Специфичный интерфейс для автомобилей
/// </summary>
public interface ICarRepository : IRepository<Car>
{
    IEnumerable<Car> GetCarsCurrentlyRented();
    IEnumerable<Car> GetTopRentedCars(int count);
    IEnumerable<Car> GetCarsByModel(string modelName);
    int GetRentalCount(int carId);
}

/// <summary>
/// Специфичный интерфейс для клиентов
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    IEnumerable<Client> GetClientsByCarModel(string modelName);
    IEnumerable<Client> GetTopClientsByRentalSum(int count);
}

/// <summary>
/// Специфичный интерфейс для аренд
/// </summary>
public interface IRentalRepository : IRepository<Rental>
{
    IEnumerable<Rental> GetActiveRentals();
    int GetRentalCountForCar(int carId);
    IEnumerable<Rental> GetRentalsByClient(int clientId);
}