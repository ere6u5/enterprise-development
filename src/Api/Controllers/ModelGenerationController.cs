using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для управления поколениями моделей
/// </summary>
/// <param name="service">Сервис поколений моделей</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("[controller]")]
public class ModelGenerationController(IModelGenerationService service, ILogger<ModelGenerationController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все поколения моделей
    /// </summary>
    /// <returns>Список поколений моделей</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ModelGenerationResponseDto>>> Get()
    {
        logger.LogInformation("Получение всех поколений моделей");
        var modelGenerations = await service.GetAllModelGenerationsAsync();
        return Ok(modelGenerations);
    }

    /// <summary>
    /// Получить поколение модели по ID
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <returns>Поколение модели</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelGenerationResponseDto>> GetModelGeneration(int id)
    {
        logger.LogInformation("Получение поколения модели с ID {id}", id);
        var modelGeneration = await service.GetModelGenerationAsync(id);
        if (modelGeneration != null) return Ok(modelGeneration);
        return NotFound();
    }

    /// <summary>
    /// Создать поколение модели
    /// </summary>
    /// <param name="modelGeneration">Данные поколения модели</param>
    /// <returns>ID созданного поколения модели</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateModelGeneration([FromBody] ModelGenerationDto modelGeneration)
    {
        logger.LogInformation("Создание поколения модели для модели ID {ModelId}", modelGeneration.ModelId);
        var id = await service.CreateModelGenerationAsync(modelGeneration);
        return Created($"/modelgeneration/{id}", id);
    }

    /// <summary>
    /// Обновить поколение модели
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <param name="entity">Обновленные данные поколения модели</param>
    /// <returns>Обновленное поколение модели</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelGenerationDto?>> UpdateModelGeneration(int id, [FromBody] ModelGenerationDto entity)
    {
        logger.LogInformation("Обновление поколения модели с ID {id}", id);
        var modelGeneration = await service.UpdateModelGenerationAsync(id, entity);
        if (modelGeneration != null) return Ok(modelGeneration);
        return NotFound();
    }

    /// <summary>
    /// Удалить поколение модели
    /// </summary>
    /// <param name="id">ID поколения модели</param>
    /// <returns>Результат без содержимого</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteModelGeneration(int id)
    {
        logger.LogInformation("Удаление поколения модели с ID {id}", id);
        await service.DeleteModelGenerationAsync(id);
        return NoContent();
    }
}