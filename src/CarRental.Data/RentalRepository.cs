using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Репозиторий для работы с арендами с хранением в памяти
/// </summary>
public class RentalRepository : InMemoryRepository<Rental>, IRentalRepository
{
    /// <summary>
    /// Получить активные аренды (текущие аренды)
    /// </summary>
    /// <returns>Коллекция активных аренд</returns>
    public IEnumerable<Rental> GetActiveRentals()
    {
        var now = DateTime.Now;
        return _entities
            .Where(r => r.RentalDate.AddHours(r.RentalHours) > now)
            .ToList();
    }

    /// <summary>
    /// Получить количество аренд для автомобиля
    /// </summary>
    /// <param name="carId">Идентификатор автомобиля</param>
    /// <returns>Количество аренд</returns>
    public int GetRentalCountForCar(int carId)
    {
        return _entities.Count(r => r.CarId == carId);
    }

    /// <summary>
    /// Получить аренды клиента
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <returns>Коллекция аренд</returns>
    public IEnumerable<Rental> GetRentalsByClient(int clientId)
    {
        return _entities
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.RentalDate)
            .ToList();
    }
}