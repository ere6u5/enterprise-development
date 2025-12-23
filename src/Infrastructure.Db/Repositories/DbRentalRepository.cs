using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

public class DbRentalRepository : IRepository<Rental>
{
    private readonly CarRentalDbContext _context;

    public DbRentalRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(Rental entity)
    {
        _context.Rentals.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<List<Rental>> ReadAsync()
    {
        return await _context.Rentals
            .Include(r => r.Car)
            .ThenInclude(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .Include(r => r.Client)
            .ToListAsync();
    }

    public async Task<Rental?> ReadAsync(int id)
    {
        return await _context.Rentals
            .Include(r => r.Car)
            .ThenInclude(c => c.ModelGeneration)
            .ThenInclude(mg => mg.Model)
            .Include(r => r.Client)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Rental?> UpdateAsync(int id, Rental entity)
    {
        var existing = await _context.Rentals.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Rentals.FindAsync(id);
        if (entity == null) return false;

        _context.Rentals.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}