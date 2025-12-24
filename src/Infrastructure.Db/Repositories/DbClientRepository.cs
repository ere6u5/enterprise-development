using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с клиентами в базе данных
/// </summary>
public class DbClientRepository(CarRentalDbContext context) : IRepository<Client>
{

    /// <summary>
    /// Создание нового клиента
    /// </summary>
    public async Task<int> CreateAsync(Client entity)
    {
        context.Clients.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех клиентов
    /// </summary>
    public async Task<List<Client>> ReadAsync()
    {
        return await context.Clients.ToListAsync();
    }

    /// <summary>
    /// Получение клиента по идентификатору
    /// </summary>
    public async Task<Client?> ReadAsync(int id)
    {
        return await context.Clients.FindAsync(id);
    }

    /// <summary>
    /// Обновление данных клиента
    /// </summary>
    public async Task<Client?> UpdateAsync(int id, Client entity)
    {
        var existing = await context.Clients.FindAsync(id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Clients.FindAsync(id);
        if (entity == null) return false;

        context.Clients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}