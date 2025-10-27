using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Базовый интерфейс репозитория для работы с сущностями
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
/// Интерфейс репозитория для работы с автомобилями
/// </summary>
public interface ICarRepository : IRepository<Car>
{
    /// <summary>
    /// Получить автомобили, находящиеся в аренде в данный момент
    /// </summary>
    /// <returns>Коллекция арендованных автомобилей</returns>
    IEnumerable<Car> GetCarsCurrentlyRented();

    /// <summary>
    /// Получить топ наиболее часто арендуемых автомобилей
    /// </summary>
    /// <param name="count">Количество автомобилей в топе</param>
    /// <returns>Коллекция автомобилей</returns>
    IEnumerable<Car> GetTopRentedCars(int count);

    /// <summary>
    /// Получить автомобили по названию модели
    /// </summary>
    /// <param name="modelName">Название модели</param>
    /// <returns>Коллекция автомобилей</returns>
    IEnumerable<Car> GetCarsByModel(string modelName);

    /// <summary>
    /// Получить количество аренд для автомобиля
    /// </summary>
    /// <param name="carId">Идентификатор автомобиля</param>
    /// <returns>Количество аренд</returns>
    int GetRentalCount(int carId);
}

/// <summary>
/// Интерфейс репозитория для работы с клиентами
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    /// <summary>
    /// Получить клиентов, бравших в аренду автомобили указанной модели
    /// </summary>
    /// <param name="modelName">Название модели автомобиля</param>
    /// <returns>Коллекция клиентов</returns>
    IEnumerable<Client> GetClientsByCarModel(string modelName);

    /// <summary>
    /// Получить топ клиентов по сумме аренд
    /// </summary>
    /// <param name="count">Количество клиентов в топе</param>
    /// <returns>Коллекция клиентов</returns>
    IEnumerable<Client> GetTopClientsByRentalSum(int count);
}

/// <summary>
/// Интерфейс репозитория для работы с арендами
/// </summary>
public interface IRentalRepository : IRepository<Rental>
{
    /// <summary>
    /// Получить активные аренды (текущие аренды)
    /// </summary>
    /// <returns>Коллекция активных аренд</returns>
    IEnumerable<Rental> GetActiveRentals();

    /// <summary>
    /// Получить количество аренд для автомобиля
    /// </summary>
    /// <param name="carId">Идентификатор автомобиля</param>
    /// <returns>Количество аренд</returns>
    int GetRentalCountForCar(int carId);

    /// <summary>
    /// Получить аренды клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <returns>Коллекция аренд</returns>
    IEnumerable<Rental> GetRentalsByClient(int clientId);
}