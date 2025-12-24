using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для управления автомобилями
/// </summary>
/// <param name="service">Сервис автомобилей</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("[controller]")]
public class CarController(ICarService service, ILogger<CarController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все автомобили
    /// </summary>
    /// <returns>Список автомобилей</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarResponseDto>>> Get()
    {
        logger.LogInformation("Получение всех автомобилей");
        var cars = await service.GetAllCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Получить автомобиль по ID
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>Автомобиль</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarResponseDto>> GetCar(int id)
    {
        logger.LogInformation("Получение автомобиля с ID {id}", id);
        var car = await service.GetCarAsync(id);
        if (car != null) return Ok(car);
        return NotFound();
    }

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    /// <param name="car">Данные автомобиля</param>
    /// <returns>ID созданного автомобиля</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateCar([FromBody] CarDto car)
    {
        logger.LogInformation("Создание автомобиля с номером {LicensePlate}", car.LicensePlate);
        var id = await service.CreateCarAsync(car);
        return Created($"/car/{id}", id);
    }

    /// <summary>
    /// Обновить автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <param name="entity">Обновленные данные автомобиля</param>
    /// <returns>Обновленный автомобиль</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarDto?>> UpdateCar(int id, [FromBody] CarDto entity)
    {
        logger.LogInformation("Обновление автомобиля с ID {id}", id);
        var car = await service.UpdateCarAsync(id, entity);
        if (car != null) return Ok(car);
        return NotFound();
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>Результат без содержимого</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCar(int id)
    {
        logger.LogInformation("Удаление автомобиля с ID {id}", id);
        await service.DeleteCarAsync(id);
        return NoContent();
    }
}