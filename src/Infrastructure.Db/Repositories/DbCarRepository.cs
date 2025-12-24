using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с автомобилями в базе данных
/// </summary>
public class DbCarRepository(CarRentalDbContext context) : IRepository<Car>
{
    private readonly CarRentalDbContext _context = context;

    /// <summary>
    /// Конструктор репозитория автомобилей
    /// </summary>
    /// <param name="context">Контекст базы данных</param>


    /// <summary>
    /// Создание нового автомобиля
    /// </summary>
    public async Task<int> CreateAsync(Car entity)
    {
        _context.Cars.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех автомобилей с включением связанных данных
    /// </summary>
    public async Task<List<Car>> ReadAsync()
    {
        return await _context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .ToListAsync();
    }

    /// <summary>
    /// Получение автомобиля по идентификатору с включением связанных данных
    /// </summary>
    public async Task<Car?> ReadAsync(int id)
    {
        return await _context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Обновление данных автомобиля
    /// </summary>
    public async Task<Car?> UpdateAsync(int id, Car entity)
    {
        var existing = await _context.Cars.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление автомобиля по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Cars.FindAsync(id);
        if (entity == null) return false;

        _context.Cars.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}