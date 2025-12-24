using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с арендами в базе данных
/// </summary>
public class DbRentalRepository(CarRentalDbContext context) : IRepository<Rental>
{

    /// <summary>
    /// Создание новой аренды
    /// </summary>
    public async Task<int> CreateAsync(Rental entity)
    {
        context.Rentals.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех аренд с включением связанных данных
    /// </summary>
    public async Task<List<Rental>> ReadAsync()
    {
        return await context.Rentals
            .Include(r => r.Car)
            .ThenInclude(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .Include(r => r.Client)
            .ToListAsync();
    }

    /// <summary>
    /// Получение аренды по идентификатору с включением связанных данных
    /// </summary>
    public async Task<Rental?> ReadAsync(int id)
    {
        return await context.Rentals
            .Include(r => r.Car)
            .ThenInclude(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Обновление данных аренды
    /// </summary>
    public async Task<Rental?> UpdateAsync(int id, Rental entity)
    {
        var existing = await context.Rentals.FindAsync(id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление аренды по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Rentals.FindAsync(id);
        if (entity == null) return false;

        context.Rentals.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}