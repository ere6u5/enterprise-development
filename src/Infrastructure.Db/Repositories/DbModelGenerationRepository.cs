using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

public class DbModelGenerationRepository : IRepository<ModelGeneration>
{
    private readonly CarRentalDbContext _context;

    public DbModelGenerationRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(ModelGeneration entity)
    {
        _context.ModelGenerations.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<List<ModelGeneration>> ReadAsync()
    {
        return await _context.ModelGenerations
            .Include(mg => mg.Model)
            .ToListAsync();
    }

    public async Task<ModelGeneration?> ReadAsync(int id)
    {
        return await _context.ModelGenerations
            .Include(mg => mg.Model)
            .FirstOrDefaultAsync(mg => mg.Id == id);
    }

    public async Task<ModelGeneration?> UpdateAsync(int id, ModelGeneration entity)
    {
        var existing = await _context.ModelGenerations.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ModelGenerations.FindAsync(id);
        if (entity == null) return false;

        _context.ModelGenerations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}