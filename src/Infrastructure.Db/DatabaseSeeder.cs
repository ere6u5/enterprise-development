using Domain.Entities;
using Domain.Enums;
using Domain.Seeder;

namespace Infrastructure.Db;

public static class DatabaseSeeder
{
    public static void Seed(CarRentalDbContext context)
    {
        if (!context.CarModels.Any())
        {
            var seeder = new DataSeeder();
            
            context.CarModels.AddRange(seeder.CarModels);
            context.SaveChanges();
            
            context.ModelGenerations.AddRange(seeder.ModelGenerations);
            context.SaveChanges();
            
            context.Cars.AddRange(seeder.Cars);
            context.SaveChanges();
            
            context.Clients.AddRange(seeder.Clients);
            context.SaveChanges();
            
            context.Rentals.AddRange(seeder.Rentals);
            context.SaveChanges();
        }
    }
}