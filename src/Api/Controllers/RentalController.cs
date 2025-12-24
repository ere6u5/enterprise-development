using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for rentals
/// </summary>
/// <param name="service">Rental service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("[controller]")]
public class RentalController(IRentalService service, ILogger<RentalController> logger) : ControllerBase
{
    /// <summary>
    /// Get all rentals
    /// </summary>
    /// <returns>List of rentals</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RentalResponseDto>>> Get()
    {
        logger.LogInformation("Getting all rentals");
        var rentals = await service.GetAllRentalsAsync();
        return Ok(rentals);
    }

    /// <summary>
    /// Get rental by ID
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <returns>Rental</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponseDto>> GetRental(int id)
    {
        logger.LogInformation("Getting rental with ID {id}", id);
        var rental = await service.GetRentalAsync(id);
        if (rental != null) return Ok(rental);
        return NotFound();
    }

    /// <summary>
    /// Create rental
    /// </summary>
    /// <param name="rental">Rental data</param>
    /// <returns>Created rental ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateRental([FromBody] RentalDto rental)
    {
        logger.LogInformation("Creating rental for car ID {CarId} and client ID {ClientId}", rental.CarId, rental.ClientId);
        var id = await service.CreateRentalAsync(rental);
        return Created($"/rental/{id}", id);
    }

    /// <summary>
    /// Update rental
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <param name="entity">Updated rental data</param>
    /// <returns>Updated rental</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RentalResponseDto?>> UpdateRental(int id, [FromBody] RentalDto entity)
    {
        logger.LogInformation("Updating rental with ID {id}", id);
        var rental = await service.UpdateRentalAsync(id, entity);
        if (rental != null) return Ok(rental);
        return NotFound();
    }

    /// <summary>
    /// Delete rental
    /// </summary>
    /// <param name="id">Rental ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRental(int id)
    {
        logger.LogInformation("Deleting rental with ID {id}", id);
        await service.DeleteRentalAsync(id);
        return NoContent();
    }
}