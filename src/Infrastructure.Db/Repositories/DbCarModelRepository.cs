using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с моделями автомобилей в базе данных
/// </summary>
public class DbCarModelRepository : IRepository<CarModel>
{
    private readonly CarRentalDbContext _context;

    /// <summary>
    /// Конструктор репозитория моделей автомобилей
    /// </summary>
    public DbCarModelRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Создание новой модели автомобиля
    /// </summary>
    public async Task<int> CreateAsync(CarModel entity)
    {
        _context.CarModels.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех моделей автомобилей
    /// </summary>
    public async Task<List<CarModel>> ReadAsync()
    {
        return await _context.CarModels.ToListAsync();
    }

    /// <summary>
    /// Получение модели автомобиля по идентификатору
    /// </summary>
    public async Task<CarModel?> ReadAsync(int id)
    {
        return await _context.CarModels.FindAsync(id);
    }

    /// <summary>
    /// Обновление данных модели автомобиля
    /// </summary>
    public async Task<CarModel?> UpdateAsync(int id, CarModel entity)
    {
        var existing = await _context.CarModels.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление модели автомобиля по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.CarModels.FindAsync(id);
        if (entity == null) return false;

        _context.CarModels.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}