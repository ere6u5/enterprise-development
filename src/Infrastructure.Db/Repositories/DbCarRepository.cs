using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с автомобилями в базе данных
/// </summary>
public class DbCarRepository(CarRentalDbContext context) : IRepository<Car>
{
    /// <summary>
    /// Создание нового автомобиля
    /// </summary>
    public async Task<int> CreateAsync(Car entity)
    {
        context.Cars.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех автомобилей с включением связанных данных
    /// </summary>
    public async Task<List<Car>> ReadAsync()
    {
        return await context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .ToListAsync();
    }

    /// <summary>
    /// Получение автомобиля по идентификатору с включением связанных данных
    /// </summary>
    public async Task<Car?> ReadAsync(int id)
    {
        return await context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <summary>
    /// Обновление данных автомобиля
    /// </summary>
    public async Task<Car?> UpdateAsync(int id, Car entity)
    {
        var existing = await context.Cars.FindAsync(id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление автомобиля по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Cars.FindAsync(id);
        if (entity == null) return false;

        context.Cars.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}