using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db.Repositories;

public class DbCarModelRepository : IRepository<CarModel>
{
    private readonly CarRentalDbContext _context;

    public DbCarModelRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateAsync(CarModel entity)
    {
        _context.CarModels.Add(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<List<CarModel>> ReadAsync()
    {
        return await _context.CarModels.ToListAsync();
    }

    public async Task<CarModel?> ReadAsync(int id)
    {
        return await _context.CarModels.FindAsync(id);
    }

    public async Task<CarModel?> UpdateAsync(int id, CarModel entity)
    {
        var existing = await _context.CarModels.FindAsync(id);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.CarModels.FindAsync(id);
        if (entity == null) return false;

        _context.CarModels.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}