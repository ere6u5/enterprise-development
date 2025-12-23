using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с клиентами в базе данных
/// </summary>
public class DbClientRepository : IRepository<Client>
{
    private readonly CarRentalDbContext _context;

    /// <summary>
    /// Конструктор репозитория клиентов
    /// </summary>
    public DbClientRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Создание нового клиента
    /// </summary>
    public async Task<int> CreateAsync(Client entity)
    {
        _context.Clients.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех клиентов
    /// </summary>
    public async Task<List<Client>> ReadAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    /// <summary>
    /// Получение клиента по идентификатору
    /// </summary>
    public async Task<Client?> ReadAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    /// <summary>
    /// Обновление данных клиента
    /// </summary>
    public async Task<Client?> UpdateAsync(int id, Client entity)
    {
        var existing = await _context.Clients.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление клиента по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Clients.FindAsync(id);
        if (entity == null) return false;

        _context.Clients.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}