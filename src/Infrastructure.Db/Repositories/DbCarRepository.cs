using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

public class DbCarRepository : IRepository<Car>
{
    private readonly CarRentalDbContext _context;

    public DbCarRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Car entity)
    {
        _context.Cars.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<List<Car>> ReadAsync()
    {
        return await _context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .ToListAsync();
    }

    public async Task<Car?> ReadAsync(int id)
    {
        return await _context.Cars
            .Include(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Car?> UpdateAsync(int id, Car entity)
    {
        var existing = await _context.Cars.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Cars.FindAsync(id);
        if (entity == null) return false;

        _context.Cars.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}