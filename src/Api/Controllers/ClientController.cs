using Application.Service;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Controller for clients
/// </summary>
/// <param name="service">Client service instance</param>
/// <param name="logger">Logger instance</param>
[ApiController]
[Route("[controller]")]
public class ClientController(IClientService service, ILogger<ClientController> logger) : ControllerBase
{
    /// <summary>
    /// Get all clients
    /// </summary>
    /// <returns>List of clients</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ClientResponseDto>>> Get()
    {
        logger.LogInformation("Getting all clients");
        var clients = await service.GetAllClientsAsync();
        return Ok(clients);
    }

    /// <summary>
    /// Get client by ID
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <returns>Client</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientResponseDto>> GetClient(int id)
    {
        logger.LogInformation("Getting client with ID {id}", id);
        var client = await service.GetClientAsync(id);
        if (client != null) return Ok(client);
        return NotFound();
    }

    /// <summary>
    /// Create client
    /// </summary>
    /// <param name="client">Client data</param>
    /// <returns>Created client ID</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> CreateClient([FromBody] ClientDto client)
    {
        logger.LogInformation("Creating client with full name {FullName}", client.FullName);
        var id = await service.CreateClientAsync(client);
        return Created($"/client/{id}", id);
    }

    /// <summary>
    /// Update client
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <param name="entity">Updated client data</param>
    /// <returns>Updated client</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto?>> UpdateClient(int id, [FromBody] ClientDto entity)
    {
        logger.LogInformation("Updating client with ID {id}", id);
        var client = await service.UpdateClientAsync(id, entity);
        if (client != null) return Ok(client);
        return NotFound();
    }

    /// <summary>
    /// Delete client
    /// </summary>
    /// <param name="id">Client ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteClient(int id)
    {
        logger.LogInformation("Deleting client with ID {id}", id);
        await service.DeleteClientAsync(id);
        return NoContent();
    }
}