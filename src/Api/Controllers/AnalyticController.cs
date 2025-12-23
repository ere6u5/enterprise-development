using Application.Service;
using Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for analytical queries
/// </summary>
/// <param name="rentalService">Rental service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticController(
    IRentalService rentalService,
    ILogger<AnalyticController> logger) : ControllerBase
{
    /// <summary>
    /// Get clients who rented cars of specified model, ordered by full name
    /// </summary>
    /// <param name="modelId">Model ID</param>
    /// <returns>List of clients</returns>
    [HttpGet("clients-by-model/{modelId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientResponseDto>>> GetClientsByModel(int modelId)
    {
        logger.LogInformation("Getting clients who rented cars of model {modelId}", modelId);
        var clients = await rentalService.GetClientsByModelAsync(modelId);
        return Ok(clients);
    }

    /// <summary>
    /// Get cars currently rented
    /// </summary>
    /// <returns>List of cars</returns>
    [HttpGet("rented-cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarResponseDto>>> GetRentedCars()
    {
        logger.LogInformation("Getting currently rented cars");
        var cars = await rentalService.GetRentedCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Get top 5 most rented cars
    /// </summary>
    /// <returns>List of cars with rental count</returns>
    [HttpGet("top5-most-rented-cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarRentalCountDto>>> GetTop5MostRentedCars()
    {
        logger.LogInformation("Getting top 5 most rented cars");
        var cars = await rentalService.GetTop5MostRentedCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Get rental count per car
    /// </summary>
    /// <returns>List of cars with rental count</returns>
    [HttpGet("rental-count-per-car")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarRentalCountDto>>> GetRentalCountPerCar()
    {
        logger.LogInformation("Getting rental count per car");
        var counts = await rentalService.GetRentalCountPerCarAsync();
        return Ok(counts);
    }

    /// <summary>
    /// Get top 5 clients by rental sum
    /// </summary>
    /// <returns>List of clients with rental sum</returns>
    [HttpGet("top5-clients-by-rental-sum")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientRentalSumDto>>> GetTop5ClientsByRentalSum()
    {
        logger.LogInformation("Getting top 5 clients by rental sum");
        var clients = await rentalService.GetTop5ClientsByRentalSumAsync();
        return Ok(clients);
    }
}