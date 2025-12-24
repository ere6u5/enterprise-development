using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с моделями автомобилей в базе данных
/// </summary>
public class DbCarModelRepository(CarRentalDbContext context) : IRepository<CarModel>
{

    /// <summary>
    /// Создание новой модели автомобиля
    /// </summary>
    public async Task<int> CreateAsync(CarModel entity)
    {
        context.CarModels.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех моделей автомобилей
    /// </summary>
    public async Task<List<CarModel>> ReadAsync()
    {
        return await context.CarModels.ToListAsync();
    }

    /// <summary>
    /// Получение модели автомобиля по идентификатору
    /// </summary>
    public async Task<CarModel?> ReadAsync(int id)
    {
        return await context.CarModels.FindAsync(id);
    }

    /// <summary>
    /// Обновление данных модели автомобиля
    /// </summary>
    public async Task<CarModel?> UpdateAsync(int id, CarModel entity)
    {
        var existing = await context.CarModels.FindAsync(id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление модели автомобиля по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.CarModels.FindAsync(id);
        if (entity == null) return false;

        context.CarModels.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}