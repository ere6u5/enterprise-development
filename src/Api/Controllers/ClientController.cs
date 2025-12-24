using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
/// <param name="service">Сервис клиентов</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("[controller]")]
public class ClientController(IClientService service, ILogger<ClientController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientResponseDto>>> Get()
    {
        logger.LogInformation("Получение всех клиентов");
        var clients = await service.GetAllClientsAsync();
        return Ok(clients);
    }

    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientResponseDto>> GetClient(int id)
    {
        logger.LogInformation("Получение клиента с ID {id}", id);
        var client = await service.GetClientAsync(id);
        if (client != null) return Ok(client);
        return NotFound();
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    /// <param name="client">Данные клиента</param>
    /// <returns>ID созданного клиента</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateClient([FromBody] ClientDto client)
    {
        logger.LogInformation("Создание клиента с ФИО {FullName}", client.FullName);
        var id = await service.CreateClientAsync(client);
        return Created($"/client/{id}", id);
    }

    /// <summary>
    /// Обновить клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="entity">Обновленные данные клиента</param>
    /// <returns>Обновленный клиент</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto?>> UpdateClient(int id, [FromBody] ClientDto entity)
    {
        logger.LogInformation("Обновление клиента с ID {id}", id);
        var client = await service.UpdateClientAsync(id, entity);
        if (client != null) return Ok(client);
        return NotFound();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Результат без содержимого</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteClient(int id)
    {
        logger.LogInformation("Удаление клиента с ID {id}", id);
        await service.DeleteClientAsync(id);
        return NoContent();
    }
}