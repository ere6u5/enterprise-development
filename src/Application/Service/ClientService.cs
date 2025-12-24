using Application.Dto;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Service;

/// <summary>
/// Сервис для работы с клиентами
/// </summary>
/// <param name="clientRepository">Репозиторий клиентов</param>
public class ClientService(IRepository<Client> clientRepository) : IClientService
{
    /// <summary>
    /// Маппинг Dto в доменную модель
    /// </summary>
    public static Client MapToDomain(ClientDto entity)
    {
        return new Client
        {
            Id = 0,
            DriverLicenseNumber = entity.DriverLicenseNumber,
            FullName = entity.FullName,
            BirthDate = entity.BirthDate
        };
    }
    
    /// <summary>
    /// Маппинг доменной модели в Dto
    /// </summary>
    public static ClientDto MapToDto(Client client)
    {
        return new ClientDto
        {
            DriverLicenseNumber = client.DriverLicenseNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate
        };
    }
    
    /// <summary>
    /// Маппинг доменной модели в Response Dto
    /// </summary>
    public static ClientResponseDto MapToResponseDto(Client client)
    {
        return new ClientResponseDto
        {
            Id = client.Id,
            DriverLicenseNumber = client.DriverLicenseNumber,
            FullName = client.FullName,
            BirthDate = client.BirthDate
        };
    }
    
    /// <inheritdoc />
    public async Task<int> CreateClientAsync(ClientDto entity)
    {
        var client = MapToDomain(entity);
        return await clientRepository.CreateAsync(client);
    }
    
    /// <inheritdoc />
    public async Task<List<ClientResponseDto>> GetAllClientsAsync()
    {
        var clients = await clientRepository.ReadAsync();
        return [.. clients.Select(MapToResponseDto)];
    }
    
    /// <inheritdoc />
    public async Task<ClientResponseDto?> GetClientAsync(int id)
    {
        var client = await clientRepository.ReadAsync(id);
        return client != null ? MapToResponseDto(client) : null;
    }
    
    /// <inheritdoc />
    public async Task<ClientDto?> UpdateClientAsync(int id, ClientDto entity)
    {
        var clientToUpdate = MapToDomain(entity);
        var updatedClient = await clientRepository.UpdateAsync(id, clientToUpdate);
        return updatedClient != null ? MapToDto(updatedClient) : null;
    }
    
    /// <inheritdoc />
    public async Task<bool> DeleteClientAsync(int id)
    {
        return await clientRepository.DeleteAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<List<Client>> GetAllClientsWithIdAsync()
    {
        return await clientRepository.ReadAsync();
    }
}