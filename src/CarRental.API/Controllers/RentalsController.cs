using Microsoft.AspNetCore.Mvc;
using CarRental.Domain.Models;
using CarRental.Data;
using CarRental.Data.Dtos;

namespace CarRental.API.Controllers;

/// <summary>
/// Контроллер для управления арендами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRentalRepository _rentalRepository;
    private readonly ICarRepository _carRepository;
    private readonly IClientRepository _clientRepository;

    /// <summary>
    /// Конструктор контроллера аренд
    /// </summary>
    /// <param name="rentalRepository">Репозиторий аренд</param>
    /// <param name="carRepository">Репозиторий автомобилей</param>
    /// <param name="clientRepository">Репозиторий клиентов</param>
    public RentalsController(
        IRentalRepository rentalRepository,
        ICarRepository carRepository,
        IClientRepository clientRepository)
    {
        _rentalRepository = rentalRepository;
        _carRepository = carRepository;
        _clientRepository = clientRepository;
    }

    /// <summary>
    /// Получить все аренды
    /// </summary>
    /// <returns>Коллекция аренд</returns>
    [HttpGet]
    public ActionResult<IEnumerable<RentalDto>> GetRentals()
    {
        var rentals = _rentalRepository.GetAll();
        var dtos = rentals.Select(r => new RentalDto
        {
            Id = r.Id,
            RentalDate = r.RentalDate,
            RentalHours = r.RentalHours,
            CarId = r.CarId,
            CarLicensePlate = r.Car.LicensePlate,
            CarModel = r.Car.ModelGeneration.Model.Name,
            ClientId = r.ClientId,
            ClientName = r.Client.FullName,
            TotalCost = r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour,
            IsActive = r.RentalDate.AddHours(r.RentalHours) > DateTime.Now
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить аренду по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <returns>Данные аренды</returns>
    [HttpGet("{id}")]
    public ActionResult<RentalDto> GetRental(int id)
    {
        var rental = _rentalRepository.GetById(id);
        if (rental == null) return NotFound();

        var dto = new RentalDto
        {
            Id = rental.Id,
            RentalDate = rental.RentalDate,
            RentalHours = rental.RentalHours,
            CarId = rental.CarId,
            CarLicensePlate = rental.Car.LicensePlate,
            CarModel = rental.Car.ModelGeneration.Model.Name,
            ClientId = rental.ClientId,
            ClientName = rental.Client.FullName,
            TotalCost = rental.RentalHours * rental.Car.ModelGeneration.RentalPricePerHour,
            IsActive = rental.RentalDate.AddHours(rental.RentalHours) > DateTime.Now
        };
        return Ok(dto);
    }

    /// <summary>
    /// Создать новую аренду
    /// </summary>
    /// <param name="createDto">Данные для создания аренды</param>
    /// <returns>Созданная аренда</returns>
    [HttpPost]
    public ActionResult<RentalDto> CreateRental(CreateRentalDto createDto)
    {
        var car = _carRepository.GetById(createDto.CarId);
        var client = _clientRepository.GetById(createDto.ClientId);

        if (car == null || client == null) return BadRequest("Car or client not found");

        var rental = new Rental
        {
            RentalDate = DateTime.Now,
            RentalHours = createDto.RentalHours,
            CarId = createDto.CarId,
            Car = car,
            ClientId = createDto.ClientId,
            Client = client
        };

        _rentalRepository.Add(rental);

        var dto = new RentalDto
        {
            Id = rental.Id,
            RentalDate = rental.RentalDate,
            RentalHours = rental.RentalHours,
            CarId = rental.CarId,
            CarLicensePlate = rental.Car.LicensePlate,
            CarModel = rental.Car.ModelGeneration.Model.Name,
            ClientId = rental.ClientId,
            ClientName = rental.Client.FullName,
            TotalCost = rental.RentalHours * rental.Car.ModelGeneration.RentalPricePerHour,
            IsActive = true
        };

        return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, dto);
    }

    /// <summary>
    /// Удалить аренду
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteRental(int id)
    {
        var rental = _rentalRepository.GetById(id);
        if (rental == null) return NotFound();

        _rentalRepository.Delete(id);
        return NoContent();
    }

    // Аналитические запросы

    /// <summary>
    /// Получить активные аренды (текущие аренды)
    /// </summary>
    /// <returns>Коллекция активных аренд</returns>
    [HttpGet("active")]
    public ActionResult<IEnumerable<RentalDto>> GetActiveRentals()
    {
        var rentals = _rentalRepository.GetActiveRentals();
        var dtos = rentals.Select(r => new RentalDto
        {
            Id = r.Id,
            RentalDate = r.RentalDate,
            RentalHours = r.RentalHours,
            CarId = r.CarId,
            CarLicensePlate = r.Car.LicensePlate,
            CarModel = r.Car.ModelGeneration.Model.Name,
            ClientId = r.ClientId,
            ClientName = r.Client.FullName,
            TotalCost = r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour,
            IsActive = true
        });
        return Ok(dtos);
    }
}