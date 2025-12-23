using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Db;

public class CarRentalDbContext : DbContext
{
    public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) 
        : base(options)
    {
    }

    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<ModelGeneration> ModelGenerations { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Rental> Rentals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
                  .OnDelete(DeleteBehavior.Restrict);
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
                  .OnDelete(DeleteBehavior.Restrict);
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
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasOne(e => e.Client)
                  .WithMany()
                  .HasForeignKey(e => e.ClientId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}