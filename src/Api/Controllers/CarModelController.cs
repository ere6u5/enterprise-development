using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for car models
/// </summary>
/// <param name="service">Car model service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("[controller]")]
public class CarModelController(ICarModelService service, ILogger<CarModelController> logger) : ControllerBase
{
    /// <summary>
    /// Get all car models
    /// </summary>
    /// <returns>List of car models</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarModelResponseDto>>> Get()
    {
        logger.LogInformation("Getting all car models");
        var carModels = await service.GetAllCarModelsAsync();
        return Ok(carModels);
    }

    /// <summary>
    /// Get car model by ID
    /// </summary>
    /// <param name="id">Car model ID</param>
    /// <returns>Car model</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarModelResponseDto>> GetCarModel(int id)
    {
        logger.LogInformation("Getting car model with ID {id}", id);
        var carModel = await service.GetCarModelAsync(id);
        if (carModel != null) return Ok(carModel);
        return NotFound();
    }

    /// <summary>
    /// Create car model
    /// </summary>
    /// <param name="carModel">Car model data</param>
    /// <returns>Created car model ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateCarModel([FromBody] CarModelDto carModel)
    {
        logger.LogInformation("Creating car model with name {Name}", carModel.Name);
        var id = await service.CreateCarModelAsync(carModel);
        return Created($"/carmodel/{id}", id);
    }

    /// <summary>
    /// Update car model
    /// </summary>
    /// <param name="id">Car model ID</param>
    /// <param name="entity">Updated car model data</param>
    /// <returns>Updated car model</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarModelDto?>> UpdateCarModel(int id, [FromBody] CarModelDto entity)
    {
        logger.LogInformation("Updating car model with ID {id}", id);
        var carModel = await service.UpdateCarModelAsync(id, entity);
        if (carModel != null) return Ok(carModel);
        return NotFound();
    }

    /// <summary>
    /// Delete car model
    /// </summary>
    /// <param name="id">Car model ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCarModel(int id)
    {
        logger.LogInformation("Deleting car model with ID {id}", id);
        await service.DeleteCarModelAsync(id);
        return NoContent();
    }
}