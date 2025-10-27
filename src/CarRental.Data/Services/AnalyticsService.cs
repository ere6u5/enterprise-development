using CarRental.Domain.Models;

namespace CarRental.Data.Services;

/// <summary>
/// Сервис для аналитических запросов
/// </summary>
public class AnalyticsService
{
    private readonly ICarRepository _carRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IRentalRepository _rentalRepository;

    public AnalyticsService(
        ICarRepository carRepository,
        IClientRepository clientRepository,
        IRentalRepository rentalRepository)
    {
        _carRepository = carRepository;
        _clientRepository = clientRepository;
        _rentalRepository = rentalRepository;
    }

    /// <summary>
    /// Получить клиентов по модели автомобиля с сортировкой по ФИО
    /// </summary>
    public IEnumerable<Client> GetClientsByCarModel(string modelName)
    {
        return _clientRepository.GetClientsByCarModel(modelName);
    }

    /// <summary>
    /// Получить автомобили в аренде
    /// </summary>
    public IEnumerable<Car> GetCurrentlyRentedCars()
    {
        return _carRepository.GetCarsCurrentlyRented();
    }

    /// <summary>
    /// Получить топ арендуемых автомобилей
    /// </summary>
    public IEnumerable<Car> GetTopRentedCars(int count)
    {
        return _carRepository.GetTopRentedCars(count);
    }

    /// <summary>
    /// Получить количество аренд для каждого автомобиля
    /// </summary>
    public Dictionary<int, int> GetRentalCountPerCar()
    {
        return _carRepository.GetAll()
            .ToDictionary(car => car.Id, car => _carRepository.GetRentalCount(car.Id));
    }

    /// <summary>
    /// Получить топ клиентов по сумме аренд
    /// </summary>
    public IEnumerable<Client> GetTopClientsByRentalSum(int count)
    {
        return _clientRepository.GetTopClientsByRentalSum(count);
    }

    /// <summary>
    /// Получить детальную информацию о топ клиентах с суммами
    /// </summary>
    public IEnumerable<ClientRentalSummary> GetTopClientsWithRentalSum(int count)
    {
        var rentals = _rentalRepository.GetAll();

        return rentals
            .GroupBy(r => r.Client)
            .Select(g => new ClientRentalSummary
            {
                Client = g.Key,
                TotalRentalAmount = g.Sum(r => r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour),
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.TotalRentalAmount)
            .Take(count)
            .ToList();
    }
}

/// <summary>
/// DTO для сводки по арендам клиента
/// </summary>
public class ClientRentalSummary
{
    public required Client Client { get; set; }
    public decimal TotalRentalAmount { get; set; }
    public int RentalCount { get; set; }
}