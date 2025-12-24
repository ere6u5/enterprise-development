using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="rentalService">Сервис аренды</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticController(
    IRentalService rentalService,
    ILogger<AnalyticController> logger) : ControllerBase
{
    /// <summary>
    /// Получить клиентов, арендовавших автомобили указанной модели, отсортированных по ФИО
    /// </summary>
    /// <param name="modelId">ID модели</param>
    /// <returns>Список клиентов</returns>
    [HttpGet("clients-by-model/{modelId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientResponseDto>>> GetClientsByModel(int modelId)
    {
        logger.LogInformation("Получение клиентов, арендовавших автомобили модели {modelId}", modelId);
        var clients = await rentalService.GetClientsByModelAsync(modelId);
        return Ok(clients);
    }

    /// <summary>
    /// Получить автомобили, находящиеся в текущей аренде
    /// </summary>
    /// <returns>Список автомобилей</returns>
    [HttpGet("rented-cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarResponseDto>>> GetRentedCars()
    {
        logger.LogInformation("Получение автомобилей, находящихся в текущей аренде");
        var cars = await rentalService.GetRentedCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Получить топ-5 самых арендуемых автомобилей
    /// </summary>
    /// <returns>Список автомобилей с количеством аренд</returns>
    [HttpGet("top5-most-rented-cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarRentalCountDto>>> GetTop5MostRentedCars()
    {
        logger.LogInformation("Получение топ-5 самых арендуемых автомобилей");
        var cars = await rentalService.GetTop5MostRentedCarsAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Получить количество аренд для каждого автомобиля
    /// </summary>
    /// <returns>Список автомобилей с количеством аренд</returns>
    [HttpGet("rental-count-per-car")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarRentalCountDto>>> GetRentalCountPerCar()
    {
        logger.LogInformation("Получение количества аренд для каждого автомобиля");
        var counts = await rentalService.GetRentalCountPerCarAsync();
        return Ok(counts);
    }

    /// <summary>
    /// Получить топ-5 клиентов по сумме аренд
    /// </summary>
    /// <returns>Список клиентов с суммой аренд</returns>
    [HttpGet("top5-clients-by-rental-sum")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientRentalSumDto>>> GetTop5ClientsByRentalSum()
    {
        logger.LogInformation("Получение топ-5 клиентов по сумме аренд");
        var clients = await rentalService.GetTop5ClientsByRentalSumAsync();
        return Ok(clients);
    }
}