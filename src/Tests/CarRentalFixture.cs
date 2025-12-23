using Domain.Entities;
using Domain.Seeder;

namespace Tests;

/// <summary>
/// Test fixture for car rental tests
/// </summary>
public class CarRentalFixture
{
    /// <summary>
    /// Data seeder
    /// </summary>
    private readonly DataSeeder _seeder = new();

    /// <summary>
    /// List of all car models
    /// </summary>
    public List<CarModel> CarModels => _seeder.CarModels;

    /// <summary>
    /// List of all model generations
    /// </summary>
    public List<ModelGeneration> ModelGenerations => _seeder.ModelGenerations;

    /// <summary>
    /// List of all cars
    /// </summary>
    public List<Car> Cars => _seeder.Cars;

    /// <summary>
    /// List of all clients
    /// </summary>
    public List<Client> Clients => _seeder.Clients;

    /// <summary>
    /// List of all rentals
    /// </summary>
    public List<Rental> Rentals => _seeder.Rentals;
}