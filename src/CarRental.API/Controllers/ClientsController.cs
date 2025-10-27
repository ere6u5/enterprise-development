using Microsoft.AspNetCore.Mvc;
using CarRental.Domain.Models;
using CarRental.Data;
using CarRental.Data.Dtos;

namespace CarRental.API.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _clientRepository;

    /// <summary>
    /// Конструктор контроллера клиентов
    /// </summary>
    /// <param name="clientRepository">Репозиторий клиентов</param>
    public ClientsController(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    /// <summary>
    /// Получить всех клиентов
    /// </summary>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetClients()
    {
        var clients = _clientRepository.GetAll();
        var dtos = clients.Select(c => new ClientDto
        {
            Id = c.Id,
            LicenseNumber = c.LicenseNumber,
            FullName = c.FullName,
            BirthDate = c.BirthDate
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Данные клиента</returns>
    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetClient(int id)
    {
        var client = _clientRepository.GetById(id);
        if (client == null) return NotFound();

        var dto = new ClientDto
        {
            Id = client.Id,
            LicenseNumber = client.LicenseNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate
        };
        return Ok(dto);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="createDto">Данные для создания клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    public ActionResult<ClientDto> CreateClient(CreateClientDto createDto)
    {
        var client = new Client
        {
            LicenseNumber = createDto.LicenseNumber,
            FullName = createDto.FullName,
            BirthDate = createDto.BirthDate
        };

        _clientRepository.Add(client);

        var dto = new ClientDto
        {
            Id = client.Id,
            LicenseNumber = client.LicenseNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate
        };

        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, dto);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="updateDto">Новые данные клиента</param>
    /// <returns>Результат операции</returns>
    [HttpPut("{id}")]
    public IActionResult UpdateClient(int id, CreateClientDto updateDto)
    {
        var existingClient = _clientRepository.GetById(id);
        if (existingClient == null) return NotFound();

        existingClient.LicenseNumber = updateDto.LicenseNumber;
        existingClient.FullName = updateDto.FullName;
        existingClient.BirthDate = updateDto.BirthDate;

        _clientRepository.Update(existingClient);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Результат операции</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteClient(int id)
    {
        var client = _clientRepository.GetById(id);
        if (client == null) return NotFound();

        _clientRepository.Delete(id);
        return NoContent();
    }

    // Аналитические запросы

    /// <summary>
    /// Получить клиентов, бравших в аренду автомобили указанной модели
    /// </summary>
    /// <param name="modelName">Название модели автомобиля</param>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet("by-model/{modelName}")]
    public ActionResult<IEnumerable<ClientDto>> GetClientsByCarModel(string modelName)
    {
        var clients = _clientRepository.GetClientsByCarModel(modelName);
        var dtos = clients.Select(c => new ClientDto
        {
            Id = c.Id,
            LicenseNumber = c.LicenseNumber,
            FullName = c.FullName,
            BirthDate = c.BirthDate
        });
        return Ok(dtos);
    }

    /// <summary>
    /// Получить топ клиентов по сумме аренд
    /// </summary>
    /// <param name="count">Количество клиентов в топе (по умолчанию 5)</param>
    /// <returns>Коллекция клиентов с общей суммой аренд</returns>
    [HttpGet("top-by-rental/{count}")]
    public ActionResult<IEnumerable<ClientWithRentalSumDto>> GetTopClientsByRentalSum(int count = 5)
    {
        var clients = _clientRepository.GetTopClientsByRentalSum(count);
        // Здесь нужно вычислить сумму для каждого клиента
        var dtos = clients.Select(c => new ClientWithRentalSumDto
        {
            Client = new ClientDto
            {
                Id = c.Id,
                LicenseNumber = c.LicenseNumber,
                FullName = c.FullName,
                BirthDate = c.BirthDate
            },
            TotalRentalSum = 0
        });
        return Ok(dtos);
    }
}