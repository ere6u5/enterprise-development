using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db;

/// <summary>
/// Контекст базы данных для системы аренды автомобилей
/// </summary>
public class CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : DbContext(options)
{

    // Наборы данных (таблицы)

    /// <summary>
    /// Таблица моделей автомобилей
    /// </summary>
    public DbSet<CarModel> CarModels { get; set; }

    /// <summary>
    /// Таблица поколений моделей
    /// </summary>
    public DbSet<ModelGeneration> ModelGenerations { get; set; }

    /// <summary>
    /// Таблица автомобилей
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Таблица аренд
    /// </summary>
    public DbSet<Rental> Rentals { get; set; }

    /// <summary>
    /// Настройка моделей базы данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфигурация именования в snake_case
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }
        }

        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DriveType).IsRequired().HasConversion<string>();
            entity.Property(e => e.SeatCount).IsRequired();
            entity.Property(e => e.BodyType).IsRequired().HasConversion<string>();
            entity.Property(e => e.CarClass).IsRequired().HasConversion<string>();

            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ModelGeneration>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Year).IsRequired();
            entity.Property(e => e.EngineVolume).IsRequired().HasPrecision(3, 1);
            entity.Property(e => e.TransmissionType).IsRequired().HasConversion<string>();
            entity.Property(e => e.RentalCostPerHour).IsRequired().HasPrecision(10, 2);

            entity.HasOne(e => e.Model)
                  .WithMany()
                  .HasForeignKey(e => e.ModelId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LicensePlate).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(50);

            entity.HasIndex(e => e.LicensePlate).IsUnique();

            entity.HasOne(e => e.ModelGeneration)
                  .WithMany()
                  .HasForeignKey(e => e.ModelGenerationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DriverLicenseNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.BirthDate).IsRequired();

            entity.HasIndex(e => e.DriverLicenseNumber).IsUnique();
        });

        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.RentalStart).IsRequired();
            entity.Property(e => e.RentalHours).IsRequired();

            entity.HasOne(e => e.Car)
                  .WithMany()
                  .HasForeignKey(e => e.CarId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Client)
                  .WithMany()
                  .HasForeignKey(e => e.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        return string.Concat(
            input.Select((c, i) =>
                i > 0 && char.IsUpper(c)
                    ? "_" + char.ToLower(c)
                    : char.ToLower(c).ToString()))
            .ToLower();
    }
}