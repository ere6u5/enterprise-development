using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Db;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarRentalDbContext>
{
    public CarRentalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CarRentalDbContext>();
        
        // Для миграций используем локальную базу данных
        var connectionString = "Server=localhost;Database=carrentaldb;User=root;Password=12345;";
        
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));
            
        return new CarRentalDbContext(optionsBuilder.Options);
    }
}