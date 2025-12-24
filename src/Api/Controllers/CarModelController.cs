using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для управления моделями автомобилей
/// </summary>
/// <param name="service">Сервис моделей автомобилей</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("[controller]")]
public class CarModelController(ICarModelService service, ILogger<CarModelController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все модели автомобилей
    /// </summary>
    /// <returns>Список моделей автомобилей</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarModelResponseDto>>> Get()
    {
        logger.LogInformation("Получение всех моделей автомобилей");
        var carModels = await service.GetAllCarModelsAsync();
        return Ok(carModels);
    }

    /// <summary>
    /// Получить модель автомобиля по ID
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
    /// <returns>Модель автомобиля</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarModelResponseDto>> GetCarModel(int id)
    {
        logger.LogInformation("Получение модели автомобиля с ID {id}", id);
        var carModel = await service.GetCarModelAsync(id);
        if (carModel != null) return Ok(carModel);
        return NotFound();
    }

    /// <summary>
    /// Создать модель автомобиля
    /// </summary>
    /// <param name="carModel">Данные модели автомобиля</param>
    /// <returns>ID созданной модели</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateCarModel([FromBody] CarModelDto carModel)
    {
        logger.LogInformation("Создание модели автомобиля с названием {Name}", carModel.Name);
        var id = await service.CreateCarModelAsync(carModel);
        return Created($"/carmodel/{id}", id);
    }

    /// <summary>
    /// Обновить модель автомобиля
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
    /// <param name="entity">Обновленные данные модели</param>
    /// <returns>Обновленная модель автомобиля</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarModelDto?>> UpdateCarModel(int id, [FromBody] CarModelDto entity)
    {
        logger.LogInformation("Обновление модели автомобиля с ID {id}", id);
        var carModel = await service.UpdateCarModelAsync(id, entity);
        if (carModel != null) return Ok(carModel);
        return NotFound();
    }

    /// <summary>
    /// Удалить модель автомобиля
    /// </summary>
    /// <param name="id">ID модели автомобиля</param>
    /// <returns>Результат без содержимого</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCarModel(int id)
    {
        logger.LogInformation("Удаление модели автомобиля с ID {id}", id);
        await service.DeleteCarModelAsync(id);
        return NoContent();
    }
}