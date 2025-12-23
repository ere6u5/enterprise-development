using Application.DTO;
using Domain.Entities;

namespace Application.Service;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Создание нового клиента
    /// </summary>
    /// <param name="entity">Данные клиента</param>
    /// <returns>Идентификатор созданного клиента</returns>
    public Task<int> CreateClientAsync(ClientDto entity);
    
    /// <summary>
    /// Получение всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    public Task<List<ClientResponseDto>> GetAllClientsAsync();
    
    /// <summary>
    /// Получение клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент</returns>
    public Task<ClientResponseDto?> GetClientAsync(int id);
    
    /// <summary>
    /// Обновление клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="entity">Новые данные клиента</param>
    /// <returns>Обновленный клиент</returns>
    public Task<ClientDto?> UpdateClientAsync(int id, ClientDto entity);
    
    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>True, если удаление прошло успешно</returns>
    public Task<bool> DeleteClientAsync(int id);
    
    /// <summary>
    /// Получение всех клиентов с идентификаторами
    /// </summary>
    /// <returns>Список клиентов</returns>
    public Task<List<Client>> GetAllClientsWithIdAsync();
}