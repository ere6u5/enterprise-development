using CarRental.Domain.Models;

namespace CarRental.Data;

/// <summary>
/// Репозиторий для работы с клиентами с хранением в памяти
/// </summary>
public class ClientRepository : InMemoryRepository<Client>, IClientRepository
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
    /// Получить клиентов, бравших в аренду автомобили указанной модели
    /// </summary>
    /// <param name="modelName">Название модели автомобиля</param>
    /// <returns>Коллекция клиентов</returns>
    public IEnumerable<Client> GetClientsByCarModel(string modelName)
    {
        if (_rentalRepository == null) return Enumerable.Empty<Client>();

        return _rentalRepository.GetAll()
            .Where(r => r.Car.ModelGeneration.Model.Name.Equals(modelName, StringComparison.OrdinalIgnoreCase))
            .Select(r => r.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();
    }

    /// <summary>
    /// Получить топ клиентов по сумме аренд
    /// </summary>
    /// <param name="count">Количество клиентов в топе</param>
    /// <returns>Коллекция клиентов</returns>
    public IEnumerable<Client> GetTopClientsByRentalSum(int count)
    {
        if (_rentalRepository == null) return Enumerable.Empty<Client>();

        return _rentalRepository.GetAll()
            .GroupBy(r => r.Client)
            .Select(g => new
            {
                Client = g.Key,
                TotalAmount = g.Sum(r => r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour)
            })
            .OrderByDescending(x => x.TotalAmount)
            .Take(count)
            .Select(x => x.Client)
            .ToList();
    }
}