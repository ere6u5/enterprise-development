using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Репозиторий для работы с автомобилями с хранением в памяти
/// </summary>
public class CarRepository : InMemoryRepository<Car>, ICarRepository
{
    private IRentalRepository? _rentalRepository;

    /// <summary>
    /// Установить зависимость от репозитория аренд
    /// </summary>
    /// <param name="rentalRepository">Репозиторий аренд</param>
    public void SetRentalRepository(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    /// <summary>
    /// Получить автомобили, находящиеся в аренде в данный момент
    /// </summary>
    /// <returns>Коллекция арендованных автомобилей</returns>
    public IEnumerable<Car> GetCarsCurrentlyRented()
    {
        if (_rentalRepository == null) return Enumerable.Empty<Car>();

        var now = DateTime.Now;
        var activeRentals = _rentalRepository.GetActiveRentals();

        return activeRentals
            .Select(r => r.Car)
            .Distinct()
            .ToList();
    }

    /// <summary>
    /// Получить топ наиболее часто арендуемых автомобилей
    /// </summary>
    /// <param name="count">Количество автомобилей в топе</param>
    /// <returns>Коллекция автомобилей</returns>
    public IEnumerable<Car> GetTopRentedCars(int count)
    {
        if (_rentalRepository == null) return Enumerable.Empty<Car>();

        var rentalCounts = _rentalRepository.GetAll()
            .GroupBy(r => r.CarId)
            .Select(g => new { CarId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(count)
            .ToList();

        return rentalCounts
            .Select(rc => GetById(rc.CarId))
            .Where(car => car != null)
            .Select(car => car!)
            .ToList();
    }

    /// <summary>
    /// Получить автомобили по названию модели
    /// </summary>
    /// <param name="modelName">Название модели</param>
    /// <returns>Коллекция автомобилей</returns>
    public IEnumerable<Car> GetCarsByModel(string modelName)
    {
        return _entities
            .Where(c => c.ModelGeneration.Model.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Получить количество аренд для автомобиля
    /// </summary>
    /// <param name="carId">Идентификатор автомобиля</param>
    /// <returns>Количество аренд</returns>
    public int GetRentalCount(int carId)
    {
        return _rentalRepository?.GetRentalCountForCar(carId) ?? 0;
    }
}