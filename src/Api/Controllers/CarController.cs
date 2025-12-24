using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for cars
/// </summary>
/// <param name="service">Car service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("[controller]")]
public class CarController(ICarService service, ILogger<CarController> logger) : ControllerBase
{
    /// <summary>
    /// Get all cars
    /// </summary>
    /// <returns>List of cars</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarResponseDto>>> Get()
    {
        logger.LogInformation("Getting all cars");
        var cars = await service.GetAllCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Get car by ID
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <returns>Car</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarResponseDto>> GetCar(int id)
    {
        logger.LogInformation("Getting car with ID {id}", id);
        var car = await service.GetCarAsync(id);
        if (car != null) return Ok(car);
        return NotFound();
    }

    /// <summary>
    /// Create car
    /// </summary>
    /// <param name="car">Car data</param>
    /// <returns>Created car ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateCar([FromBody] CarDto car)
    {
        logger.LogInformation("Creating car with license plate {LicensePlate}", car.LicensePlate);
        var id = await service.CreateCarAsync(car);
        return Created($"/car/{id}", id);
    }

    /// <summary>
    /// Update car
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <param name="entity">Updated car data</param>
    /// <returns>Updated car</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarDto?>> UpdateCar(int id, [FromBody] CarDto entity)
    {
        logger.LogInformation("Updating car with ID {id}", id);
        var car = await service.UpdateCarAsync(id, entity);
        if (car != null) return Ok(car);
        return NotFound();
    }

    /// <summary>
    /// Delete car
    /// </summary>
    /// <param name="id">Car ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCar(int id)
    {
        logger.LogInformation("Deleting car with ID {id}", id);
        await service.DeleteCarAsync(id);
        return NoContent();
    }
}