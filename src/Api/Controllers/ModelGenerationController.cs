using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for model generations
/// </summary>
/// <param name="service">Model generation service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("[controller]")]
public class ModelGenerationController(IModelGenerationService service, ILogger<ModelGenerationController> logger) : ControllerBase
{
    /// <summary>
    /// Get all model generations
    /// </summary>
    /// <returns>List of model generations</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ModelGenerationResponseDto>>> Get()
    {
        logger.LogInformation("Getting all model generations");
        var modelGenerations = await service.GetAllModelGenerationsAsync();
        return Ok(modelGenerations);
    }

    /// <summary>
    /// Get model generation by ID
    /// </summary>
    /// <param name="id">Model generation ID</param>
    /// <returns>Model generation</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelGenerationResponseDto>> GetModelGeneration(int id)
    {
        logger.LogInformation("Getting model generation with ID {id}", id);
        var modelGeneration = await service.GetModelGenerationAsync(id);
        if (modelGeneration != null) return Ok(modelGeneration);
        return NotFound();
    }

    /// <summary>
    /// Create model generation
    /// </summary>
    /// <param name="modelGeneration">Model generation data</param>
    /// <returns>Created model generation ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateModelGeneration([FromBody] ModelGenerationDto modelGeneration)
    {
        logger.LogInformation("Creating model generation for model ID {ModelId}", modelGeneration.ModelId);
        var id = await service.CreateModelGenerationAsync(modelGeneration);
        return Created($"/modelgeneration/{id}", id);
    }

    /// <summary>
    /// Update model generation
    /// </summary>
    /// <param name="id">Model generation ID</param>
    /// <param name="entity">Updated model generation data</param>
    /// <returns>Updated model generation</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModelGenerationDto?>> UpdateModelGeneration(int id, [FromBody] ModelGenerationDto entity)
    {
        logger.LogInformation("Updating model generation with ID {id}", id);
        var modelGeneration = await service.UpdateModelGenerationAsync(id, entity);
        if (modelGeneration != null) return Ok(modelGeneration);
        return NotFound();
    }

    /// <summary>
    /// Delete model generation
    /// </summary>
    /// <param name="id">Model generation ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteModelGeneration(int id)
    {
        logger.LogInformation("Deleting model generation with ID {id}", id);
        await service.DeleteModelGenerationAsync(id);
        return NoContent();
    }
}