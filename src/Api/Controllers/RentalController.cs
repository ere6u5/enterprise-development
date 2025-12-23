using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для управления арендами
/// </summary>
/// <param name="service">Сервис аренды</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("[controller]")]
public class RentalController(IRentalService service, ILogger<RentalController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все аренды
    /// </summary>
    /// <returns>Список аренд</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RentalResponseDto>>> Get()
    {
        logger.LogInformation("Получение всех аренд");
        var rentals = await service.GetAllRentalsAsync();
        return Ok(rentals);
    }

    /// <summary>
    /// Получить аренду по ID
    /// </summary>
    /// <param name="id">ID аренды</param>
    /// <returns>Аренда</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponseDto>> GetRental(int id)
    {
        logger.LogInformation("Получение аренды с ID {id}", id);
        var rental = await service.GetRentalAsync(id);
        if (rental != null) return Ok(rental);
        return NotFound();
    }

    /// <summary>
    /// Создать аренду
    /// </summary>
    /// <param name="rental">Данные аренды</param>
    /// <returns>ID созданной аренды</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateRental([FromBody] RentalDto rental)
    {
        logger.LogInformation("Создание аренды для автомобиля ID {CarId} и клиента ID {ClientId}", rental.CarId, rental.ClientId);
        var id = await service.CreateRentalAsync(rental);
        return Created($"/rental/{id}", id);
    }

    /// <summary>
    /// Обновить аренду
    /// </summary>
    /// <param name="id">ID аренды</param>
    /// <param name="entity">Обновленные данные аренды</param>
    /// <returns>Обновленная аренда</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponseDto?>> UpdateRental(int id, [FromBody] RentalDto entity)
    {
        logger.LogInformation("Обновление аренды с ID {id}", id);
        var rental = await service.UpdateRentalAsync(id, entity);
        if (rental != null) return Ok(rental);
        return NotFound();
    }

    /// <summary>
    /// Удалить аренду
    /// </summary>
    /// <param name="id">ID аренды</param>
    /// <returns>Результат без содержимого</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRental(int id)
    {
        logger.LogInformation("Удаление аренды с ID {id}", id);
        await service.DeleteRentalAsync(id);
        return NoContent();
    }
}