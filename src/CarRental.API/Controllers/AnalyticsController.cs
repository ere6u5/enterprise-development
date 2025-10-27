using Microsoft.AspNetCore.Mvc;
using CarRental.Data.Services;
using CarRental.Data.Dtos;

namespace CarRental.API.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly AnalyticsService _analyticsService;

    public AnalyticsController(AnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Получить клиентов по модели автомобиля
    /// </summary>
    [HttpGet("clients-by-model/{modelName}")]
    public ActionResult<IEnumerable<ClientDto>> GetClientsByCarModel(string modelName)
    {
        var clients = _analyticsService.GetClientsByCarModel(modelName);
        var dtos = clients.Select(c => new ClientDto
        {
            Id = c.Id,
            LicenseNumber = c.LicenseNumber,
            FullName = c.FullName,
            BirthDate = c.BirthDate
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить автомобили в аренде
    /// </summary>
    [HttpGet("rented-cars")]
    public ActionResult<IEnumerable<CarDto>> GetRentedCars()
    {
        var cars = _analyticsService.GetCurrentlyRentedCars();
        var dtos = cars.Select(c => new CarDto
        {
            Id = c.Id,
            LicensePlate = c.LicensePlate,
            Color = c.Color,
            ModelGenerationId = c.ModelGenerationId,
            ModelName = c.ModelGeneration.Model.Name,
            ModelClass = c.ModelGeneration.Model.Class,
            RentalPricePerHour = c.ModelGeneration.RentalPricePerHour
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить топ арендуемых автомобилей
    /// </summary>
    [HttpGet("top-rented-cars/{count}")]
    public ActionResult<IEnumerable<CarWithRentalCountDto>> GetTopRentedCars(int count = 5)
    {
        var cars = _analyticsService.GetTopRentedCars(count);
        var rentalCounts = _analyticsService.GetRentalCountPerCar();

        var dtos = cars.Select(c => new CarWithRentalCountDto
        {
            Car = new CarDto
            {
                Id = c.Id,
                LicensePlate = c.LicensePlate,
                Color = c.Color,
                ModelGenerationId = c.ModelGenerationId,
                ModelName = c.ModelGeneration.Model.Name,
                ModelClass = c.ModelGeneration.Model.Class,
                RentalPricePerHour = c.ModelGeneration.RentalPricePerHour
            },
            RentalCount = rentalCounts.GetValueOrDefault(c.Id, 0)
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить количество аренд для каждого автомобиля
    /// </summary>
    [HttpGet("rental-counts")]
    public ActionResult<Dictionary<int, int>> GetRentalCounts()
    {
        var counts = _analyticsService.GetRentalCountPerCar();
        return Ok(counts);
    }

    /// <summary>
    /// Получить топ клиентов по сумме аренд
    /// </summary>
    [HttpGet("top-clients/{count}")]
    public ActionResult<IEnumerable<ClientWithRentalSumDto>> GetTopClients(int count = 5)
    {
        var clientSummaries = _analyticsService.GetTopClientsWithRentalSum(count);

        var dtos = clientSummaries.Select(cs => new ClientWithRentalSumDto
        {
            Client = new ClientDto
            {
                Id = cs.Client.Id,
                LicenseNumber = cs.Client.LicenseNumber,
                FullName = cs.Client.FullName,
                BirthDate = cs.Client.BirthDate
            },
            TotalRentalSum = cs.TotalRentalAmount
        });
        return Ok(dtos);
    }
}