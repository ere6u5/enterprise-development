using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

/// <summary>
/// Репозиторий для работы с поколениями моделей в базе данных
/// </summary>
public class DbModelGenerationRepository(CarRentalDbContext context) : IRepository<ModelGeneration>
{
    private readonly CarRentalDbContext _context = context;

    /// <summary>
    /// Создание нового поколения модели
    /// </summary>
    public async Task<int> CreateAsync(ModelGeneration entity)
    {
        _context.ModelGenerations.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    /// <summary>
    /// Получение всех поколений моделей с включением связанных данных
    /// </summary>
    public async Task<List<ModelGeneration>> ReadAsync()
    {
        return await _context.ModelGenerations
            .Include(mg => mg.Model)
            .ToListAsync();
    }

    /// <summary>
    /// Получение поколения модели по идентификатору
    /// </summary>
    public async Task<ModelGeneration?> ReadAsync(int id)
    {
        return await _context.ModelGenerations
            .Include(mg => mg.Model)
            .FirstOrDefaultAsync(mg => mg.Id == id);
    }

    /// <summary>
    /// Обновление данных поколения модели
    /// </summary>
    public async Task<ModelGeneration?> UpdateAsync(int id, ModelGeneration entity)
    {
        var existing = await _context.ModelGenerations.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    /// <summary>
    /// Удаление поколения модели по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ModelGenerations.FindAsync(id);
        if (entity == null) return false;

        _context.ModelGenerations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}