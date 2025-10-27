using Microsoft.AspNetCore.Mvc;
using CarRental.Domain.Models;
using CarRental.Data;
using CarRental.Data.Dtos;

namespace CarRental.API.Controllers;

/// <summary>
/// Контроллер для управления автомобилями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarRepository _carRepository;

    /// <summary>
    /// Конструктор контроллера автомобилей
    /// </summary>
    /// <param name="carRepository">Репозиторий автомобилей</param>
    public CarsController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    /// <summary>
    /// Получить все автомобили
    /// </summary>
    /// <returns>Коллекция автомобилей</returns>
    [HttpGet]
    public ActionResult<IEnumerable<CarDto>> GetCars()
    {
        var cars = _carRepository.GetAll();
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
    /// Получить автомобиль по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <returns>Данные автомобиля</returns>
    [HttpGet("{id}")]
    public ActionResult<CarDto> GetCar(int id)
    {
        var car = _carRepository.GetById(id);
        if (car == null) return NotFound();

        var dto = new CarDto
        {
            Id = car.Id,
            LicensePlate = car.LicensePlate,
            Color = car.Color,
            ModelGenerationId = car.ModelGenerationId,
            ModelName = car.ModelGeneration.Model.Name,
            ModelClass = car.ModelGeneration.Model.Class,
            RentalPricePerHour = car.ModelGeneration.RentalPricePerHour
        };
        return Ok(dto);
    }

    /// <summary>
    /// Создать новый автомобиль
    /// </summary>
    /// <param name="createDto">Данные для создания автомобиля</param>
    /// <returns>Созданный автомобиль</returns>
    [HttpPost]
    public ActionResult<CarDto> CreateCar(CreateCarDto createDto)
    {
        // В реальном приложении здесь была бы валидация и получение реального ModelGeneration
        // Сейчас создаем заглушку с минимальными данными
        var model = new CarModel
        {
            Id = 1,
            Name = "Unknown",
            DriveType = "FWD",
            SeatsCount = 5,
            BodyType = "Sedan",
            Class = "Economy"
        };

        var modelGeneration = new ModelGeneration
        {
            Id = createDto.ModelGenerationId,
            Year = 2020,
            EngineVolume = 1.6,
            Transmission = "Manual",
            RentalPricePerHour = 1000,
            ModelId = 1,
            Model = model
        };

        var car = new Car
        {
            LicensePlate = createDto.LicensePlate,
            Color = createDto.Color,
            ModelGenerationId = createDto.ModelGenerationId,
            ModelGeneration = modelGeneration
        };

        _carRepository.Add(car);

        var dto = new CarDto
        {
            Id = car.Id,
            LicensePlate = car.LicensePlate,
            Color = car.Color,
            ModelGenerationId = car.ModelGenerationId,
            ModelName = car.ModelGeneration.Model.Name,
            ModelClass = car.ModelGeneration.Model.Class,
            RentalPricePerHour = car.ModelGeneration.RentalPricePerHour
        };

        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, dto);
    }

    /// <summary>
    /// Обновить данные автомобиля
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <param name="updateDto">Новые данные автомобиля</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateCar(int id, CreateCarDto updateDto)
    {
        var existingCar = _carRepository.GetById(id);
        if (existingCar == null) return NotFound();

        existingCar.LicensePlate = updateDto.LicensePlate;
        existingCar.Color = updateDto.Color;
        existingCar.ModelGenerationId = updateDto.ModelGenerationId;

        _carRepository.Update(existingCar);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteCar(int id)
    {
        var car = _carRepository.GetById(id);
        if (car == null) return NotFound();

        _carRepository.Delete(id);
        return NoContent();
    }

    // Аналитические запросы

    /// <summary>
    /// Получить автомобили, находящиеся в аренде
    /// </summary>
    /// <returns>Коллекция арендованных автомобилей</returns>
    [HttpGet("rented")]
    public ActionResult<IEnumerable<CarDto>> GetRentedCars()
    {
        var cars = _carRepository.GetCarsCurrentlyRented();
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
    /// Получить топ наиболее часто арендуемых автомобилей
    /// </summary>
    /// <param name="count">Количество автомобилей в топе (по умолчанию 5)</param>
    /// <returns>Коллекция автомобилей с количеством аренд</returns>
    [HttpGet("top-rented/{count}")]
    public ActionResult<IEnumerable<CarWithRentalCountDto>> GetTopRentedCars(int count = 5)
    {
        var cars = _carRepository.GetTopRentedCars(count);
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
            RentalCount = _carRepository.GetRentalCount(c.Id)
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить количество аренд для автомобиля
    /// </summary>
    /// <param name="id">Идентификатор автомобиля</param>
    /// <returns>Количество аренд</returns>
    [HttpGet("{id}/rental-count")]
    public ActionResult<int> GetRentalCount(int id)
    {
        var count = _carRepository.GetRentalCount(id);
        return Ok(count);
    }
}