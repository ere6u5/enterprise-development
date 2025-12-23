using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с арендами в базе данных
/// </summary>
public class DbRentalRepository : IRepository<Rental>
{
    private readonly CarRentalDbContext _context;

    /// <summary>
    /// Конструктор репозитория аренд
    /// </summary>
    public DbRentalRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Создание новой аренды
    /// </summary>
    public async Task<int> CreateAsync(Rental entity)
    {
        _context.Rentals.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех аренд с включением связанных данных
    /// </summary>
    public async Task<List<Rental>> ReadAsync()
    {
        return await _context.Rentals
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
        return await _context.Rentals
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
        var existing = await _context.Rentals.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление аренды по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Rentals.FindAsync(id);
        if (entity == null) return false;

        _context.Rentals.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}